import { NgModule } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { ExampleBaseModule } from 'Example/ExampleBaseModule';
import { ServerConfigBase, Registry } from "codeshell/core";
import { DomainTree } from "../Domains/DomainTree";
import { PageList } from "../Pages/PageList";
import { PageCategoryList } from "../PageCategories/PageCategoryList";
import { PageCategoryCreate } from "../PageCategories/PageCategoryCreate";
import { DomainTreeSelect } from "../Pages/DomainTreeSelect";
import { NaveList } from "../NavigationGroups/NaveList";
import { NavigationPageList } from "../NavigationGroups/NavigationPageList";
import { NavigationPageCreate } from "../NavigationGroups/NavigationPageCreate";
import { GenerationInofList } from "../Generations/GenerationInofList";
import { PageCategorySelect } from "../PageCategories/PageCategorySelect";
import { ResourceModal } from "../Resources/ResourceModal";
import { ServerTracerModal } from "../Generations/ServerTracerModal";


@NgModule({
    declarations: [DomainTree,PageList,PageCategoryList,PageCategoryCreate,DomainTreeSelect,NaveList,NavigationPageList,NavigationPageCreate,GenerationInofList,PageCategorySelect,ResourceModal,ServerTracerModal,],
    exports: [
		DomainTree,PageList,PageCategoryList,PageCategoryCreate,DomainTreeSelect,NaveList,NavigationPageList,NavigationPageCreate,GenerationInofList,PageCategorySelect,ResourceModal,ServerTracerModal,
		ExampleBaseModule
	],
    imports: [
        ExampleBaseModule
    ],
	entryComponents:[DomainTree,PageList,PageCategoryList,PageCategoryCreate,DomainTreeSelect,NaveList,NavigationPageList,NavigationPageCreate,GenerationInofList,PageCategorySelect,ResourceModal,ServerTracerModal,]
})
export class SharedModule {

	constructor(trans: TranslateService, conf: ServerConfigBase) {
        trans.setDefaultLang(conf.Locale);
        trans.use(conf.Locale);
    }
}

Registry.Register("Domains/DomainTree", DomainTree);
Registry.Register("Pages/PageList", PageList);
Registry.Register("PageCategories/PageCategoryList", PageCategoryList);
Registry.Register("PageCategories/PageCategoryCreate", PageCategoryCreate);
Registry.Register("Pages/DomainTreeSelect", DomainTreeSelect);
Registry.Register("NavigationGroups/NaveList", NaveList);
Registry.Register("NavigationGroups/NavigationPageList", NavigationPageList);
Registry.Register("NavigationGroups/NavigationPageCreate", NavigationPageCreate);
Registry.Register("Generations/GenerationInofList", GenerationInofList);
Registry.Register("PageCategories/PageCategorySelect", PageCategorySelect);
Registry.Register("Resources/ResourceModal", ResourceModal);
Registry.Register("Generations/ServerTracerModal", ServerTracerModal);
