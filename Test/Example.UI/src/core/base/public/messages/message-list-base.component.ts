import { Component, Injector } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { ListComponentBase } from "codeshell/base-components";
import { ListSelectionService } from "codeshell/services";
import { MessagesService } from "../http";

@Component({template:''})
export abstract class MessageListBase extends ListComponentBase{
	Service = new MessagesService();
	HideNav=true;

	Selection=new ListSelectionService();
	OnSelected:(item:any)=>void=e=>{};

	constructor(rt:ActivatedRoute,inj:Injector){
		super(rt,inj);
		
		this.Selection.SelectionChanged.subscribe(()=>{
			if(this.Selection.SelectedItems.length>0){
				this.OnSelected(this.Selection.SelectedItems[0]);
			}
		})
	}
}