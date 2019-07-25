import { ListComponentBase } from "CodeShell/BaseComponents/ListComponentBase";
import { Injectable } from "@angular/core";
import { UsersService } from "ExampleProject/Http";
import { Shell } from "CodeShell/Shell";

@Injectable()
export abstract class UserEditBase extends ListComponentBase{
	get Service(): UsersService { return Shell.Injector.get(UsersService); }
}