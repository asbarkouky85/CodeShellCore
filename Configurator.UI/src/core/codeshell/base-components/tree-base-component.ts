import { BaseComponent } from "./base-component";
import { List_FindIdRecusive, List_RunRecursively, RecursionModel } from "../recursion";
import { EntityHttpService } from "../http";
import { SubmitResult, DeleteResult } from "../results";
import { ViewChild, EventEmitter, Input, Component } from "@angular/core";

import { Shell } from "../shell";
import { NgForm } from "@angular/forms";
import { Utils } from "../utilities/utils";
import { TreeEventArgs } from "../recursion/tree-event-args";
import { ActivatedRoute } from "@angular/router";
import { EditComponentBase } from "../base-components/edit-base-component";
import { Stored } from '../services';
import { ComponentRequest } from '../components';
import { List_RemoveItem } from 'codeshell/data';
import { ITreeOptions, TreeComponent, TreeNode } from "@circlon/angular-tree-component";

export class ExpandedItems {
    Items: number[] = [];
}

@Component({ template: '' })
export abstract class TreeComponentBase extends BaseComponent {

    @ViewChild(TreeComponent)
    treeComponent?: TreeComponent;
    Form?: NgForm;

    @Input()
    LoadOnStart: boolean = true;

    protected abstract get Service(): EntityHttpService;

    RouteParams: any = {};
    private ignore = false;
    private _expanded?: number[];
    private _loadedOnce: boolean = false;

    model: any = {};
    List: RecursionModel[] = [];

    Loader?: () => Promise<any[]>;
    CountLoader?: () => Promise<{ [key: number]: number }>;
    AddFunction?: (addTo: TreeNode | null) => Promise<{ model: RecursionModel, result: SubmitResult }>;
    EditFunction?: (editNode: TreeNode) => Promise<{ model: RecursionModel, result: SubmitResult }>;

    /** should be ComponentRequest<EditComponentBase> */
    editingModalRequest?: any;

    IsNew: boolean = true;
    AllowEdit: boolean = true;
    AllowMove: boolean = true;
    ShowOkButton: boolean = true;
    UseContentCounts: boolean = false;
    UseInTreeTextBoxes: boolean = true;

    get UseCheckBoxes(): boolean { return this.treeOptions.useCheckbox == true; }
    set UseCheckBoxes(val: boolean) { this.treeOptions.useCheckbox = val; }

    selectedNode?: TreeNode;
    selectedItem?: RecursionModel;
    DisplayItem?: RecursionModel;

    selectedId: number = 0;

    OnSelectedEvent?: (folder: RecursionModel | null) => void;
    OnTreeEvent: EventEmitter<TreeEventArgs> = new EventEmitter<TreeEventArgs>();
    OnLoadedEvent?: (items: any[]) => void;
    OnUnassigned: EventEmitter<TreeEventArgs> = new EventEmitter<TreeEventArgs>();
    OnTreeLoadedEvet: EventEmitter<TreeEventArgs> = new EventEmitter<TreeEventArgs>();

    get Expanded(): number[] {
        if (!this._expanded) {

            var x = Stored.Get(this.ComponentName + "_Expanded", ExpandedItems);
            if (x == null)
                this._expanded = [];
            else
                this._expanded = x.Items;
        }
        return (this._expanded as number[]);
    }

    treeOptions: ITreeOptions = {
        allowDrag: (node) => {
            return this.AllowEdit && this.AllowMove;
        },
        allowDrop: (node) => {
            return this.AllowEdit && this.AllowMove;
        },
    };

    ngOnInit(): void {
        super.ngOnInit();
        let opts = this.GetLookupOptions();

        if (opts != null) {
            this.LoadLookupsAsync(opts).then(l => {
                this.Lookups = l;
                this.OnReady();
            });
        } else {
            this.OnReady();
        }
    }

    ngAfterViewInit() {
        super.ngAfterViewInit();
        if (this.treeComponent)
            (this.treeComponent.event as EventEmitter<any>).subscribe((e: any) => this.OnEvent(e));

    }

    OnEvent(ev: any) {
        if (!ev.node)
            return;
        this.OnTreeEvent.emit(new TreeEventArgs(ev.eventName as string, ev.node as TreeNode));
        if (ev.eventName == "focus")
            this.setClickedRow(ev.node);
        if (this.UseCheckBoxes && ev.eventName == "select") {
            this.OnCheckBoxSelect(ev.node);
        } else if (this.UseCheckBoxes && ev.eventName == "deselect") {
            this.OnCheckBoxDeselect(ev.node);
        }
    }



    SetSelected(id: number) {
        if (this.treeComponent) {
            var n = this.treeComponent.treeModel.getNodeById(id) as TreeNode;
            if (n) {
                n.focus();
            }
        }
    }

    ForceReload() {
        if (this._loadedOnce)
            this._loadedOnce = false;
    }

    protected LoadLookupsAsync(opts: any): Promise<any> {
        return this.Service.Get("GetListLookups", opts);
    }

    protected OnReady(): void {
        if (!this.IsEmbedded && this.LoadOnStart)
            this.LoadData();
    }


    async StartAsync(): Promise<TreeComponentBase> {
        if (!this._loadedOnce) {
            var lst = await this.LoadDataPromise();
            this.AfterLoad(lst);
            this._loadedOnce = true;
        }
        await this.ExpandSavedAsync();
        return this;
    }

    OnTreeLoaded: () => void = () => { };
    OnCheckBoxSelect(node: TreeNode) { }
    OnCheckBoxDeselect(nods: TreeNode) { }
    OnSelected(item: RecursionModel) { }

    LoadDataPromise(): Promise<any[]> {
        this.List = [];
        if (this.Loader)
            return this.Loader();
        return this.Service.Get("GetTree");
    }

    ContentCountAsync(): Promise<{ [key: number]: number }> {
        if (this.CountLoader) {

            return this.CountLoader();
        }

        return Promise.resolve({});
    }

    ShowCheckBoxes() {
        this.treeOptions.useCheckbox = true;
    }

    ClearSelection() {
        this.selectedId = 0;
        this.selectedItem = undefined;
        if (this.selectedNode) {

            this.selectedNode.setIsSelected(false);
            this.selectedNode.setIsActive(false);

        }

        this.selectedNode = undefined;
        if (this.treeComponent) {

            this.treeComponent.treeModel.setActiveNode(null, null);
            this.treeComponent.treeModel.setFocusedNode(null);
            this.treeComponent.treeModel.update();
        }

        if (this.OnSelectedEvent)
            this.OnSelectedEvent(null);
    }

    protected AfterLoad(lst: any[]) {
        this.List = [];
        for (var i of lst)
            this.List.push(RecursionModel.FromDB(i));

        if (this.UseContentCounts)
            this.LoadCounts();

        if (this.OnLoadedEvent)
            this.OnLoadedEvent(this.List);

        if (this.treeComponent)
            this.treeComponent.treeModel.update();

        this.onTreeInit();
    }

    LoadData() {
        this.List = [];
        this.LoadDataPromise().then(e => {
            this.AfterLoad(e);
            this.ExpandSavedAsync();
        });
    }

    LoadCounts() {

        List_RunRecursively(this.List, d => { d.contentCount = 0; d.hasContents = false; });
        this.ContentCountAsync().then(res => {
            for (var i in res) {
                var item = List_FindIdRecusive(this.List, parseInt(i));
                if (item) {
                    item.hasContents = true;
                    item.contentCount = res[i];
                }
            }
        });
    }

    private ExpandSavedAsync(): Promise<void> {
        return new Promise((res, rej) => {
            setTimeout(() => {
                this.ignore = true;
                if (this.treeComponent) {
                    for (let id of this.Expanded) {
                        if (id == null)
                            continue;

                        let node = this.treeComponent.treeModel.getNodeById(id)
                        if (node)
                            node.expand();

                    }
                }

                this.ignore = false;
                res();

            }, 500)
        })
    }

    onTreeInit() {
        setTimeout(() => {
            this.OnTreeLoaded();
        }, 300);
    }

    onExpanded(event: any) {
        if (this.ignore)
            return;
        let item = event.node.data as RecursionModel;
        if (event.isExpanded)
            this.Expanded.push(item.id)
        else {
            List_RemoveItem(this.Expanded, item.id);
        }

        this.SaveExpanded();
    }

    setClickedRow(node: TreeNode) {
        let index = node.data as RecursionModel;
        if (this.selectedId == index.id)
            return;
        this.selectedId = index.id;
        this.selectedNode = node;
        this.selectedItem = index;
        this.OnSelected(index);

        if (this.OnSelectedEvent)
            this.OnSelectedEvent(index);
    }

    protected OnDisplayNode() { }


    protected InitializeModel(parent: TreeNode | null): RecursionModel {
        return new RecursionModel();
    }

    onMoveNode(ev: any) {

        let moved: RecursionModel = ev.node;
        let loc: any = ev.to.parent;

        if (loc.virtual)
            moved.parentId = null;
        else
            moved.parentId = loc.id;


        var m = Object.assign({}, moved);
        m.children = [];
        this.Service.Put("Put", m).catch(e => this.LoadData());
    }

    protected AddWithModalAsync(parentNode: TreeNode | null): Promise<{ model: RecursionModel, result: SubmitResult }> {

        return new Promise((resolve, reject) => {
            if (this.editingModalRequest) {
                var req = this.editingModalRequest as ComponentRequest<EditComponentBase>;
                this.GetComponent(req).then(comp => {
                    var model = this.InitializeModel(parentNode);
                    if (parentNode)
                        model.parentId = parentNode.data.id;
                    comp.IsNew = true;
                    comp.BindAsync(model).then(d => {
                        comp.Show = true;
                    });

                    comp.DataSubmitted = (model, res) => {
                        if (comp.UseLocalization)
                            this.LoadData();
                        else
                            resolve({ model: model, result: res });
                        comp.Show = false;
                    }

                }).catch(e => reject(e));
            } else {
                reject("no Editing Component")
            }
        })
    }

    protected EditWithModalAsync(node: TreeNode): Promise<{ model: RecursionModel, result: SubmitResult }> {

        return new Promise((resolve, reject) => {
            if (this.editingModalRequest) {
                var req = this.editingModalRequest as ComponentRequest<EditComponentBase>;
                this.GetComponent(req).then(comp => {
                    var model = node.data as RecursionModel;
                    if (node)
                        model.parentId = node.data.id;
                    comp.FillAsync(model.id).then(d => {
                        comp.Show = true;
                    });

                    comp.DataSubmitted = (model, res) => {
                        if (comp.UseLocalization)
                            this.LoadData();
                        else
                            resolve({ model: model, result: res });
                        comp.Show = false;
                    }

                }).catch(e => reject(e));
            } else {
                reject("no Editing Component")
            }
        })
    }

    protected EditWithTreeBoxAsync(node: TreeNode): Promise<{ model: RecursionModel, result: SubmitResult }> {
        return new Promise((resolve, reject) => {
            if (this.treeComponent) {
                var model = node.data as RecursionModel;
                model.editing = true;

                this.treeComponent.treeModel.virtualScroll.scrollIntoView(node, true);

                setTimeout(() => {
                    let el = document.getElementById('editor' + model.id);

                    if (el) {
                        el.addEventListener("keyup", ev => {
                            if (ev.key == "Enter") {
                                model.editing = false;
                                if (model.name && model.name.length > 1) {
                                    var ch = model.children;
                                    model.children = [];
                                    this.SubmitUpdateAsync(model).then(res => {
                                        model.children = ch;
                                        resolve({ model: model, result: res });
                                    }).catch(e => {
                                        model.editing = false;
                                    });
                                }
                            }
                        });

                        el.addEventListener("blur", ev => {
                            model.editing = false;
                            reject("blurred");
                        })
                        el.focus();
                    }
                }, 400);

            }
        })
    }

    protected AddWithTreeBoxAsync(addTo: TreeNode | null): Promise<{ model: RecursionModel, result: SubmitResult }> {
        return new Promise((resolve, reject) => {
            if (this.treeComponent) {
                var model = this.InitializeModel(addTo);
                if (addTo)
                    model.parentId = addTo.data.id;
                model.editing = true;
                addTo = addTo ? addTo : this.treeComponent.treeModel.virtualRoot;
                var nNode = this.AppendToNode(addTo, model, true);
                addTo.expand();
                this.treeComponent.treeModel.virtualScroll.scrollIntoView(nNode, true);

                setTimeout(() => {
                    let el = document.getElementById('editor0');

                    if (el) {
                        el.addEventListener("keyup", ev => {
                            if (ev.key == "Enter") {
                                model.editing = false;
                                if (model.name && model.name.length > 1) {
                                    this.SubmitNewAsync(model).then(res => {

                                        resolve({ model: model, result: res });
                                    }).catch(e => {
                                        Utils.HandleError(e, true);
                                        this.RemoveFromNode(addTo, model);
                                        reject(e);
                                    });
                                } else {

                                    this.RemoveFromNode(addTo, model);
                                    reject("blurred");
                                }
                            }
                        });

                        el.addEventListener("blur", ev => {
                            if (model.editing)
                                this.RemoveFromNode(addTo, model);
                            reject("blurred");
                        })
                        el.focus();
                    }
                }, 400);

            }
        })
    }

    AfterAddSubmit(parentNode: TreeNode | null, model: RecursionModel, res: SubmitResult) {
        if (this.treeComponent) {
            parentNode = parentNode ? parentNode : this.treeComponent.treeModel.virtualRoot;
            if (res.data.Id) {
                model.id = res.data.Id;
            }
            this.AppendToNode(parentNode, model);
        }
    }

    AfterEditSubmit(node: TreeNode, changedModel: RecursionModel) {
        var children = node.data.children;
        var lst = node.parent.data.children as RecursionModel[];
        var ind = lst.indexOf(node.data);
        changedModel.children = children;
        lst[ind] = changedModel;
        if (this.treeComponent)
            this.treeComponent.treeModel.update();
    }

    AddNode(parentNode: TreeNode | null) {

        if (!this.treeComponent)
            return;

        if (this.AddFunction) {
            this.AddFunction(parentNode).then(d => this.AfterAddSubmit(parentNode, d.model, d.result))
        } else if (this.editingModalRequest) {
            this.AddWithModalAsync(parentNode).then(d => this.AfterAddSubmit(parentNode, d.model, d.result));
        } else {
            this.AddWithTreeBoxAsync(parentNode).then(d => {
                if (d.result.data.Id)
                    d.model.id = d.result.data.Id;
            }).catch(e => { });
        }
    }

    EditNode(obj: TreeNode) {

        if (!this.treeComponent)
            return;


        if (this.EditFunction) {
            this.EditFunction(obj).then(d => this.AfterEditSubmit(obj, d.model));
        } else if (this.editingModalRequest) {
            this.EditWithModalAsync(obj).then(d => this.AfterEditSubmit(obj, d.model));
        } else {
            this.EditWithTreeBoxAsync(obj).then(d => { }).catch(e => { });
        }
    }

    async DeleteAsync(item: any): Promise<DeleteResult> {
        var c = await Shell.Main.ShowDeleteConfirm();
        if (!c) {
            return Promise.reject("user rejected");
        }
        return await this.Service.AttemptDelete(item);
    }

    DeleteNode(node: TreeNode) {
        let item = node.data as RecursionModel;
        this.DeleteAsync(item).then(d => {
            let parentModel = node.parent.data as RecursionModel;
            let i = parentModel.children.indexOf(item);

            if (i > -1)
                parentModel.children.splice(i, 1);
            if (this.treeComponent)
                this.treeComponent.treeModel.update();
        }).catch(d => {
            Utils.HandleError(d, true, true);
        })

    }

    protected SaveExpanded() {
        Stored.Set(this.ComponentName + "_Expanded", { Items: this.Expanded });
    }

    RemoveFromNode(from: TreeNode | null, model: RecursionModel) {
        if (this.treeComponent) {
            from = from ? from : this.treeComponent.treeModel.virtualRoot;
            var lst = from.data.children as RecursionModel[];
            List_RemoveItem(lst, model);
            if (this.treeComponent)
                this.treeComponent.treeModel.update();
        }

    }

    AppendToNode(addTo: TreeNode, model: RecursionModel, top: boolean = false): TreeNode {
        if (this.treeComponent) {
            var parentData = addTo.data.children as RecursionModel[];
            var newNode = new TreeNode(model, addTo, this.treeComponent.treeModel, 0);
            if (top)
                addTo.children.unshift(newNode);
            else
                addTo.children.push(newNode);
            parentData.push(model);
            addTo.expand();
            this.treeComponent.treeModel.update();
            return newNode;
        }
        throw "no tree component";
    }

    SelectItemAsync(): Promise<RecursionModel> {

        return new Promise((res, rej) => {
            this.OnOk = d => {
                res(this.selectedItem);
                return Promise.resolve(true);
            }
            this.OnCancel = d => {
                rej("user canceled");
                return Promise.resolve(false);
            }
            this.StartAsync().then(comp => {
                this.Show = true;

            })
        })
    }

    SubmitNewAsync(model: any): Promise<SubmitResult> {
        return this.Service.Post("Post", model);
    }

    SubmitUpdateAsync(model: any): Promise<SubmitResult> {
        return this.Service.Put("Put", model);
    }


}