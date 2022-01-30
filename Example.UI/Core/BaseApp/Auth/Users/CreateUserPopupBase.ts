import { EditComponentBase } from "codeshell/baseComponents";
import { Injectable } from "@angular/core";
import { UsersService } from "../Http";
import { Shell } from "codeshell/core";

@Injectable()
export abstract class CreateUserPopupBase extends EditComponentBase {
    get Service(): UsersService {
        return Shell.Injector.get(UsersService);
    }
}