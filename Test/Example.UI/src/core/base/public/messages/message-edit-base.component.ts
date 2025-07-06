import { Component } from "@angular/core";
import { EditComponentBase } from "codeshell/base-components";
import { ListItem } from "codeshell/data";
import { FilesHttpService } from "codeshell/http";
import { QuillConfig } from "codeshell/utilities/defaultConfigs";
import { MessagesService } from "../http";

@Component({template:''})
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