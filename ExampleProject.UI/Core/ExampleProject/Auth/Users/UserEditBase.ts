import { ListComponentBase, DTOEditComponentBase } from "codeshell/baseComponents";
import { Injectable } from "@angular/core";
import { UsersService } from "ExampleProject/Http";
import { Shell } from "codeshell/core";

@Injectable()
export abstract class UserEditBase extends DTOEditComponentBase{
	get Service(): UsersService { return Shell.Injector.get(UsersService); }
}