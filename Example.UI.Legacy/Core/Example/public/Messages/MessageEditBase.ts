import { EditComponentBase } from "codeshell/baseComponents";
import { Injectable } from "@angular/core";
import { MessagesService } from "../Http";
import { QuillConfig } from "codeshell/utilities/defaultConfigs";
import { FilesHttpService } from "codeshell/http";
import { ListItem } from "codeshell/helpers";

@Injectable()
export abstract class MessageEditBase extends EditComponentBase{
	Service = new MessagesService();
	HideNav=true;

	quillModules=QuillConfig.Basic;

	get Attachments(): any[] { return this.model.attachments; }
	Files = new FilesHttpService("");


	Respond(){
		this.model.responseOn=new Date();
		this.model.statusId=1;
		super.Submit();
	}

	FilesChanged(ev: Event) {
		var el = ev.target as HTMLInputElement;

		this.Files.PostFiles("Upload", el.files).then(res => {
			console.log(res);
			el.value = "";
			for (var f of res) {
				var att = ListItem.Detached({ file: f });
				att.AddTo(this.Attachments);
			}
		});
	}
}