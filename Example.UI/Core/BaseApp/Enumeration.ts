import { ListItem } from 'codeshell/helpers';
export class UserListDTO {
	name?: null | string;
	logonName?: null | string;
	appName?: null | string;
	email?: null | string;
	personId?: null | number;
	mobile?: null | string;
	tenantName?: null | string;
	genderName?: null | string;
	birthDate?: null | Date;
	expression: any = {};
	id: number = 0;
	createdOn?: null | Date;
	createdBy?: null | number;
	state?: null | string;
}

export enum MessageTypes {
	Comment = 0 ,
	Complaint = 1 ,
};

