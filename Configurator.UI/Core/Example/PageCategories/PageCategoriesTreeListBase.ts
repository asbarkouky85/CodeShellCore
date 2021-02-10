import { BaseComponent } from "codeshell/baseComponents";
import { Injectable, ViewChild } from "@angular/core";
import { PageCategoriesService } from "Example/Http";
import { Shell } from "codeshell/core";

import { DomainTreeBase, DomainCountMode } from "../Domains/DomainTreeBase";
import { PageCategoryListBase } from "./PageCategoryListBase";
import { RecursionModel, ComponentRequest, DTO, NoteType } from "../../codeshell/helpers";
import { PageCategoryCreateBase } from "./PageCategoryCreateBase";

@Injectable()
export abstract class PageCategoriesTreeListBase extends BaseComponent {
    get Service(): PageCategoriesService { return Shell.Injector.get(PageCategoriesService); }

    @ViewChild("DomainTree") DomainTree?: DomainTreeBase;
    @ViewChild("PageCategoryList") PageCategoriesList?: PageCategoryListBase;



    OnChanged?: () => void;
    ngOnInit() {
        Shell.Main.SideBarStatus.emit(false);
        if (this.DomainTree) {
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
        return true;
    }

    AddPageCategory() {
        let req: ComponentRequest<PageCategoryCreateBase> = {
            Identifier: "CreateModal",
            Init: comp => {
                comp.OnOk = c => this.CategoryCreation(c);
            }
        }

        this.GetComponent(req).then(comp => {
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

}