import { ListComponentBase } from "codeshell/baseComponents";
import { Injectable, Injector } from "@angular/core";
import { MessagesService } from "../Http";
import { ActivatedRoute } from "@angular/router";
import { ListSelectionService } from "codeshell/helpers/ListSelectionService";

@Injectable()
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