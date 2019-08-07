import { BaseComponent } from "./BaseComponent";
import { RecursionModel } from "CodeShell/Helpers/RecursionModel";
import { DataHttpService } from "CodeShell/Http";
import { Stored } from "CodeShell/Helpers";
import { ViewChild, EventEmitter, Input } from "@angular/core";
import { TreeComponent, ITreeOptions, TreeNode } from "angular-tree-component";
import { Shell } from "CodeShell/Shell";
import { NgForm } from "@angular/forms";
import "CodeShell/Utilities/Extensions";
import { List_RemoveItem, List_RunRecursively, List_FindIdRecusive } from "../Utilities/Utils";
import { TreeEventArgs } from "../Helpers/TreeEventArgs";

export class ExpandedItems {
    Items: number[] = [];
}

export abstract class TreeComponentBase extends BaseComponent {

    private ignore = false;
    private _expanded?: number[];

    protected abstract get Service(): DataHttpService;

    Tree: RecursionModel[] = [];
    Loader?: () => Promise<any[]>;
    CountLoader?: () => Promise<{ [key: number]: number }>;
    FoldersModel: any = {};

    IsNew: boolean = true;
    AllowEdit: boolean = true;
    AllowMove: boolean = true;
    ShowOkButton: boolean = true;
    UseContentCounts: boolean = false;
    @Input() LoadOnStart: boolean = true;

    editingItem?: RecursionModel;
    selectedNode?: TreeNode;
    selectedItem?: RecursionModel;

    selectedId: number = 0;
    editingIndex: number = -1;

    protected editingParent?: TreeNode;
    protected newNode?: TreeNode;

    @ViewChild(TreeComponent) treeComponent?: TreeComponent;
    @ViewChild("Form") form?: NgForm;

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
        let opts = this.GetLookupOptions();

        if (this.treeComponent)
            (this.treeComponent.event as EventEmitter<any>).subscribe((e: any) => this.OnEvent(e));

        if (opts != null) {

            this.LoadLookupsAsync(opts).then(l => {
                this.Lookups = l;
                this.OnReady();
            });
        } else {
            this.OnReady();
        }

    }

    OnEvent(ev: any) {
        if (!ev.node)
            return;
        this.OnTreeEvent.emit(new TreeEventArgs(ev.eventName as string, ev.node as TreeNode));
        if (ev.eventName == "focus")
            this.setClickedRow(ev.node);

    }

    protected LoadLookupsAsync(opts: any): Promise<any> {
        return this.Service.Get("GetListLookups", opts);
    }

    protected OnReady(): void {
       

        if (!this.IsEmbedded && this.LoadOnStart)
            this.LoadData(); 
    }

    protected LoadDataPromise(): Promise<any[]> {
        this.Tree = [];
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
        this.editingIndex = -1;
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

    LoadData() {
        this.Tree = [];
        this.LoadDataPromise().then(e => {
            this.Tree = [];
            for (var i of e)
                this.Tree.push(RecursionModel.FromDB(i));

            if (this.UseContentCounts) 
                this.LoadCounts();
            

            if (this.OnLoadedEvent)
                this.OnLoadedEvent(this.Tree);
            
            if (this.treeComponent)
                this.treeComponent.treeModel.update();
            

            

            this.onTreeInit();
        });
    }

    LoadCounts() {
        List_RunRecursively(this.Tree, d => { d.contentCount = 0; d.hasContents = false; });
        this.ContentCountAsync().then(res => {
            this.Log(res);

            for (var i in res) {
                var item = List_FindIdRecusive(this.Tree, parseInt(i));
                if (item) {
                    item.hasContents = true;
                    item.contentCount = res[i];
                }
                    
            }
        });
    }


    onTreeInit() {
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
            this.OnTreeLoaded();
        }, 500)

    }

    OnTreeLoaded() { }

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

        this.CancelEdit();

        if (this.OnSelectedEvent)
            this.OnSelectedEvent(index);
    }

    protected OnSelected(item: RecursionModel) { }
    protected OnAddingNode() { }
    protected OnEditingNode() { }

    onMoveNode(ev: any) {

        let moved: RecursionModel = ev.node;
        let loc: any = ev.to.parent;

        if (loc.virtual)
            moved.parentId = null;
        else
            moved.parentId = loc.id;
        console.log(ev.to.parent);

        var m = Object.assign({}, moved);
        m.children = [];
        this.Service.Put("Put", m).catch(e => this.LoadData());
    }

    AddNode(parentNode: TreeNode | null) {

        if (!this.treeComponent)
            return;

        this.FoldersModel = this.InitializeModel(parentNode);
        this.FoldersModel.editing = true;

        let newItem = new RecursionModel;
        let addingLst: RecursionModel[] = [];
        let root = parentNode == null;

        newItem.editing = true;


        if (parentNode != null) {
            this.editingParent = parentNode;

            let parentData: RecursionModel = this.editingParent.data;
            this.FoldersModel.parentId = parentData.id;

            addingLst = parentData.children;


        } else {

            this.editingParent = this.treeComponent.treeModel.virtualRoot;
            this.FoldersModel.parentId = null;
            addingLst = this.Tree;
        }

        this.newNode = new TreeNode(newItem, this.editingParent, this.treeComponent.treeModel, 0);

        this.editingParent.children.unshift(this.newNode);

        addingLst.unshift(newItem);
        this.editingIndex = addingLst.indexOf(newItem);



        this.selectedId = 0;
        if (!root && parentNode)
            parentNode.expand();
        this.treeComponent.treeModel.update();

        this.newNode.focus();
        //this.newNode.setIsActive(true);
        let el = document.getElementById('editor0');
        this.treeComponent.treeModel.virtualScroll.scrollIntoView(this.newNode, true);
        if (el) {
            el.focus();

        }


        this.IsNew = true;
        this.OnAddingNode();
    }

    protected InitializeModel(parent: TreeNode | null): RecursionModel {
        return new RecursionModel();
    }

    EditNode(obj: TreeNode) {

        let item = obj.data as RecursionModel;
        item.editing = true;
        this.editingParent = obj.parent;
        let parentData = this.editingParent.data as RecursionModel;
        this.editingIndex = parentData.children.indexOf(item);


        this.FoldersModel = Object.assign(new RecursionModel, item);

        this.IsNew = false;

        this.OnEditingNode();
    }

    DeleteNode(node: TreeNode) {
        let item = node.data as RecursionModel;
        Shell.Main.ShowDeleteConfirm(e => {
            return this.Service.AttemptDelete(item.id);
        }, () => {
            let parentModel = node.parent.data as RecursionModel;
            let i = parentModel.children.indexOf(item);

            if (i > -1)
                parentModel.children.splice(i, 1);
            if (this.treeComponent)
                this.treeComponent.treeModel.update();
        });
    }

    protected SaveExpanded() {
        Stored.Set(this.ComponentName + "_Expanded", { Items: this.Expanded });
    }

    CancelEdit() {

        if (this.editingParent) {
            let par = this.editingParent.data as RecursionModel;

            if (this.newNode) {
                this.newNode.data.editing = false;

                let n = this.newNode.data as RecursionModel;

                let x = this.editingParent.children.indexOf(this.newNode);
                if (x > -1)
                    this.editingParent.children.splice(x, 1);

                x = par.children.indexOf(n);
                if (x > -1)
                    par.children.splice(x, 1);

                this.newNode = undefined;
                this.editingParent = undefined;
            } else if (this.editingIndex > -1) {
                par.children[this.editingIndex].editing = false;
            }

            if (this.treeComponent)
                this.treeComponent.treeModel.update();
        }
    }



    AfterPut(chil: RecursionModel[], showSuccess: boolean = true) {
        this.FoldersModel.children = chil;
        if (this.editingIndex > -1 && this.editingParent) {
            let par = this.editingParent.data as RecursionModel;
            par.children[this.editingIndex] = this.FoldersModel;
            console.log(this.editingParent, this.editingIndex)
            if (this.treeComponent)
                this.treeComponent.treeModel.update();
        }
        this.FoldersModel.editing = false;
        //this.FoldersModel = {};
        if (showSuccess)
            this.NotifyTranslate("success_message")
    }

    AfterPost(res: any, showSuccess: boolean = true) {
        console.log(res, this.editingParent);
        if (this.editingIndex > -1 && this.editingParent) {
            let par = this.editingParent.data as RecursionModel;
            par.children[this.editingIndex] = RecursionModel.FromDB(res.data.Data);

            if (this.treeComponent)
                this.treeComponent.treeModel.update();
        }
        this.FoldersModel.editing = false;
        this.FoldersModel = {};
        if (showSuccess)
            this.NotifyTranslate("success_message");

    }
    Submit() {

        if (this.form && this.form.invalid)
            return;

        if (!this.IsNew) {
            let chil: RecursionModel[] = []
            if (this.FoldersModel) {
                chil = this.FoldersModel.children;
                this.FoldersModel.children = null;
            }

            this.Service.Put("Put", this.FoldersModel).then((res) => {
                this.AfterPut(chil);
            });
        } else {
            this.FoldersModel.id = 0;
            this.Service.Post("Post", this.FoldersModel).then((res) => {
                this.AfterPost(res);
            }).catch(d => {

                this.CancelEdit();
            });
        }
    }
}