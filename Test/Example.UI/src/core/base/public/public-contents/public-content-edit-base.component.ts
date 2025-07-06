import { Component } from "@angular/core";
import { Shell } from "codeshell";
import { EditComponentBase } from "codeshell/base-components";
import { NoteType } from "codeshell/results";
import { PublicContentsService } from "../http";

@Component({ template: '' })
export abstract class PublicContentEditBase extends EditComponentBase {
	Service = new PublicContentsService();

	code: string = "AboutUs";
	lang: string = "ar";

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
				this.HeaderExtra = " ( " + Shell.Page("PublicContents__" + this.code) + " ) ";
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