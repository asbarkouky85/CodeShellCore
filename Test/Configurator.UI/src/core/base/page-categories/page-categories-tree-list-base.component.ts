import { Component, ViewChild } from "@angular/core";
import { DomainTreeBase, DomainCountMode } from "@base/domains/domain-tree-base.component";
import { PageCategoriesService, DomainsService } from "@base/http";
import { Shell } from "codeshell";
import { BaseComponent } from "codeshell/base-components";
import { ComponentRequest } from "codeshell/components";
import { RecursionModel } from "codeshell/recursion";
import { NoteType } from "codeshell/results";
import { ModulesConfigBase } from "./modules-config-base.component";
import { PageCategoryCreateBase } from "./page-category-create-base.component";
import { PageCategoryListBase } from "./page-category-list-base.component";

@Component({ template: '' })
export abstract class PageCategoriesTreeListBase extends BaseComponent {
    Service = new PageCategoriesService();
    DomainSrv = new DomainsService();

    @ViewChild("DomainTree") DomainTree?: DomainTreeBase;
    @ViewChild("PageCategoryList") PageCategoriesList?: PageCategoryListBase;

    OnChanged?: () => void;
    ngOnInit() {
        Shell.Main.SideBarStatus.emit(false);

    }

    ngAfterViewInit() {
        if (this.DomainTree) {
            this.DomainTree.Loader = () => this.DomainSrv.GetCategoriesTree();
            this.DomainTree.LoadData();
            this.DomainTree.AllowEdit = false;
            this.DomainTree.HideHeader = true;
            this.DomainTree.CountMode = DomainCountMode.Categories;

            this.DomainTree.LoadCounts();
            this.DomainTree.OnSelectedEvent = domain => this.OnDomainSelected(domain);
        }
        if (this.PageCategoriesList) {
            this.PageCategoriesList.HideHeader = true;
            this.PageCategoriesList.LoadData();
        }
    }

    async CategoryCreation(comp: PageCategoryCreateBase): Promise<boolean> {
        var res = await this.Service.CreatPageCategory(comp.SelectedItems);
        this.NotifyTranslate(res.message + " [ Affetcted : " + res.affectedRows + " ] ", res.code == 0 ? NoteType.Success : NoteType.Error);
        if (this.PageCategoriesList)
            this.PageCategoriesList.LoadData();
        if (this.DomainTree)
            this.DomainTree.LoadData();
        return true;
    }

    AddPageCategory() {
        let req: ComponentRequest<PageCategoryCreateBase> = { Identifier: "CreateModal" }

        this.GetComponent(req).then(comp => {
            comp.OnOk = c => this.CategoryCreation(c);
            comp.StartAsync().then(e => {
                comp.SelectedItems = [];
                comp.Show = true;
            })

        }).catch(error => console.log(error));;
    }

    OnDomainSelected(item: RecursionModel | null) {
        if (this.PageCategoriesList && item) {
            this.PageCategoriesList.Domain = item;
            this.PageCategoriesList.LoadData();
        }
    }

    OpenModules() {
        var req: ComponentRequest<ModulesConfigBase> = { Identifier: "ModulesModal" };
        this.GetComponent(req).then(d => {
            d.BindAsync({}).then(x => {
                d.Show = true;
            })
            d.OnInstalled = () => {
                if (this.DomainTree)
                    this.DomainTree.LoadData();
            }
        })
    }
}