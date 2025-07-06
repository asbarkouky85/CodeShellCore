import { NgModule } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { ExampleBaseModule } from '@base/example-base.module';
import { ServerConfigBase, Registry } from "codeshell/main";
import { DomainTree } from './components/domain-tree.component';
import { DomainTreeSelect } from './components/domain-tree-select.component';
import { ServerTracerModal } from './components/server-tracer-modal.component';


@NgModule({
    declarations: [DomainTree,DomainTreeSelect,ServerTracerModal,],
    exports: [
		DomainTree,DomainTreeSelect,ServerTracerModal,
		ExampleBaseModule
	],
    imports: [
        ExampleBaseModule
    ],
	entryComponents:[DomainTree, DomainTreeSelect, ServerTracerModal, ]
})
export class SharedModule {

}

Registry.Register("Shared/DomainTree", DomainTree);
Registry.Register("Shared/DomainTreeSelect", DomainTreeSelect);
Registry.Register("Shared/ServerTracerModal", ServerTracerModal);
