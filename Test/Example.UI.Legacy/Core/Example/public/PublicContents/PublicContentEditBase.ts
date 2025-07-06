import { EditComponentBase } from "codeshell/baseComponents";
import { Injectable } from "@angular/core";
import { PublicContentsService } from "../Http";
import { Shell } from "codeshell/core";
import { NoteType } from "codeshell/helpers";

@Injectable()
export abstract class PublicContentEditBase extends EditComponentBase {
	Service = new PublicContentsService();

	code: string = "AboutUs";
	lang: string = "ar";
	get HeaderExtra(): string { return " ( " + Shell.Page("PublicContents__" + this.code) + " ) "; }

	HideNav = true;
	navSection = "About";

	GetModelFromServerAsync(id: number): Promise<any> {
		return this.Service.GetByCode(this.code, this.lang);
	}

	StartComponent() {
		if (!this.IsEmbedded) {
			this.Route.params.subscribe(params => {
				this.RouteParams = params;
				this.IsNew = false;
				if (this.RouteParams.code)
					this.code = this.RouteParams.code;
				if (this.RouteParams.lang)
					this.lang = this.RouteParams.lang;
				this.Fill(0);

			});
		} else {
			this.IsInitialized = true;
			this.OnReady();

		}
	}

	OnSubmitSuccess() {
		this.NotifyTranslate("success_message", NoteType.Success);
	}
}