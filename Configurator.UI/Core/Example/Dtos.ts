import { ListItem } from 'codeshell/helpers';
export class PageParameterDTO extends ListItem {
	name?: null | string;
	type: number = 0;
	typeString?: null | string;
	typeEnum: any = {};
	defaultValue?: null | string;
	viewPath?: null | string;
	id: number = 0;
	entity: any = {};
}

export class RenderDTO {
	mod?: null | string;
	domain?: null | string;
	lazy?: null | boolean;
	nameChain?: null | string;
	tenantId?: null | number;
}

export class DbCreationRequest {
	force?: null | boolean;
	environment?: null | string;
	tenantCode?: null | string;
	dbName?: null | string;
	id?: null | number;
}

export class BundlingTask {
	id: number = 0;
	tenantCode?: null | string;
	version?: null | string;
	status?: null | string;
	environment?: null | string;
	connectionId?: null | string;
	startedOn: Date = new Date() ;
	completedOn?: null | Date;
	message?: null | string;
	task: any = {};
}

export class PreviewData {
	tenantCode?: null | string;
	url?: null | string;
}

