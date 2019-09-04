import { ListComponentBase } from "codeshell/baseComponents";
import { Injectable } from "@angular/core";
import { UsersService } from "ExampleProject/Auth/Users/Http";
import { Shell } from "codeshell/core";

@Injectable()
export abstract class UserListBase extends ListComponentBase{
	get Service(): UsersService { return Shell.Injector.get(UsersService); }
}