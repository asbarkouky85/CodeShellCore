import { Component } from "@angular/core";
import { TopBarBase } from "codeshell/baseComponents/topBarBase";

@Component({ templateUrl: "./topBar.html", selector: "top-bar", exportAs: "top-bar" })
export class TopBar extends TopBarBase {
    changePasswordComponent = "Shared/ChangePassword";

    editProfileComponent = "Auth/Users/EditProfile";
}