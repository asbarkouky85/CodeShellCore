(window.webpackJsonp=window.webpackJsonp||[]).push([[19],{BaWk:function(e,t,a){"use strict";a.r(t),a.d(t,"TemplatesModule",function(){return te});var n=a("iInd"),c=a("7OZ3"),o=a("U6Sh"),r=a("Gjc3"),i=a("SZAU"),s=a("tA7v"),b=a("mrSG"),l=a("M08Z"),d=a("uZok"),u=a("gyvI"),g=a("i/vM"),f=a("8Y7J");const m=["DomainTree"],p=["PageCategoryList"];let S=(()=>{class e extends u.b{constructor(){super(...arguments),this.Service=new d.d,this.DomainSrv=new d.b}ngOnInit(){c.d.Main.SideBarStatus.emit(!1)}ngAfterViewInit(){this.DomainTree&&(this.DomainTree.Loader=()=>this.DomainSrv.GetCategoriesTree(),this.DomainTree.LoadData(),this.DomainTree.AllowEdit=!1,this.DomainTree.HideHeader=!0,this.DomainTree.CountMode=l.a.Categories,this.DomainTree.LoadCounts(),this.DomainTree.OnSelectedEvent=e=>this.OnDomainSelected(e)),this.PageCategoriesList&&(this.PageCategoriesList.HideHeader=!0,this.PageCategoriesList.LoadData())}CategoryCreation(e){return Object(b.a)(this,void 0,void 0,function*(){var t=yield this.Service.CreatPageCategory(e.SelectedItems);return this.NotifyTranslate(t.message+" [ Affetcted : "+t.affectedRows+" ] ",0==t.code?g.c.Success:g.c.Error),this.PageCategoriesList&&this.PageCategoriesList.LoadData(),this.DomainTree&&this.DomainTree.LoadData(),!0})}AddPageCategory(){this.GetComponent({Identifier:"CreateModal"}).then(e=>{e.OnOk=e=>this.CategoryCreation(e),e.StartAsync().then(t=>{e.SelectedItems=[],e.Show=!0})}).catch(e=>console.log(e))}OnDomainSelected(e){this.PageCategoriesList&&e&&(this.PageCategoriesList.Domain=e,this.PageCategoriesList.LoadData())}OpenModules(){this.GetComponent({Identifier:"ModulesModal"}).then(e=>{e.BindAsync({}).then(t=>{e.Show=!0}),e.OnInstalled=()=>{this.DomainTree&&this.DomainTree.LoadData()}})}}return e.\u0275fac=function(t){return R(t||e)},e.\u0275cmp=f.Gb({type:e,selectors:[["ng-component"]],viewQuery:function(e,t){if(1&e&&(f.Kc(m,!0),f.Kc(p,!0)),2&e){let e;f.sc(e=f.ac())&&(t.DomainTree=e.first),f.sc(e=f.ac())&&(t.PageCategoriesList=e.first)}},features:[f.xb],decls:0,vars:0,template:function(e,t){},encapsulation:2}),e})();const R=f.Ub(S);var C=a("SVse"),v=a("NtII"),h=a("100G"),A=a("TSSN");const I=["Form"];function y(e,t){if(1&e&&(f.Sb(0,"span"),f.Gc(1),f.Rb()),2&e){const e=f.dc(2);f.Ab(1),f.Ic(" - ",e.HeaderExtra,"")}}function G(e,t){1&e&&(f.Sb(0,"div",3),f.Sb(1,"ol",22),f.Sb(2,"li"),f.Sb(3,"a",23),f.Gc(4),f.ec(5,"translate"),f.Rb(),f.Rb(),f.Sb(6,"li"),f.Gc(7),f.ec(8,"translate"),f.Rb(),f.Rb(),f.Rb()),2&e&&(f.Ab(4),f.Hc(f.fc(5,2,"Words.Main")),f.Ab(3),f.Ic(" ",f.fc(8,4,"Pages.Templates__PageCategoriesTreeList")," "))}const L=function(){return{p0:"PageCategory"}};function P(e,t){if(1&e){const e=f.Tb();f.Sb(0,"div",9),f.Sb(1,"div",3),f.Sb(2,"div",10),f.Sb(3,"h2"),f.Gc(4),f.ec(5,"translate"),f.Ec(6,y,2,1,"span",11),f.Rb(),f.Rb(),f.Sb(7,"div",12),f.Sb(8,"div",3),f.Sb(9,"div",13),f.Sb(10,"div",14),f.Sb(11,"button",15),f.Zb("click",function(){return f.vc(e),f.dc().OpenModules()}),f.Nb(12,"i",16),f.Gc(13),f.ec(14,"translate"),f.Rb(),f.Sb(15,"button",17),f.Zb("click",function(){return f.vc(e),f.dc().AddPageCategory()}),f.Nb(16,"i",18),f.Gc(17),f.ec(18,"translate"),f.Rb(),f.Sb(19,"button",19),f.Zb("click",function(){return f.vc(e),f.dc().Refresh()}),f.Nb(20,"i",20),f.Rb(),f.Rb(),f.Rb(),f.Rb(),f.Rb(),f.Rb(),f.Ec(21,G,9,6,"div",21),f.Rb()}if(2&e){const e=f.dc();f.Ab(4),f.Hc(f.fc(5,5,"Pages.Templates__PageCategoriesTreeList")),f.Ab(2),f.jc("ngIf",e.HeaderExtra),f.Ab(7),f.Ic(" ",f.fc(14,7,"Words.Modules")," "),f.Ab(4),f.Ic(" ",f.gc(18,9,"Words.AddEntity",f.mc(12,L))," "),f.Ab(4),f.jc("ngIf",!e.IsEmbedded)}}function T(e,t){if(1&e&&(f.Sb(0,"span"),f.Gc(1),f.Rb()),2&e){const e=f.dc(2);f.Ab(1),f.Ic(" - ",e.HeaderExtra,"")}}function E(e,t){if(1&e&&(f.Sb(0,"div",24),f.Sb(1,"div",2),f.Sb(2,"div",3),f.Sb(3,"div",25),f.Gc(4),f.ec(5,"translate"),f.Ec(6,T,2,1,"span",11),f.Rb(),f.Nb(7,"div",26),f.Rb(),f.Rb(),f.Rb()),2&e){const e=f.dc();f.Ab(4),f.Ic(" ",f.fc(5,2,"Pages.Templates__PageCategoriesTreeList"),""),f.Ab(2),f.jc("ngIf",e.HeaderExtra)}}let _=(()=>{class e extends S{constructor(){super(...arguments),this.ViewParams={AddUrl:null,EditUrl:null,DetailsUrl:null,ListUrl:null,Fields:[],Other:{DomainTree:"Shared/DomainTree",PageCategoryList:"Razor/PageCategoryList",CreateModal:"Razor/PageCategoryCreate",ModulesModal:"Razor/ModuleConfigModal"}},this.LookupOptions=null}get CollectionId(){return null}}return e.\u0275fac=function(t){return j(t||e)},e.\u0275cmp=f.Gb({type:e,selectors:[["app-page-categories-tree-list"]],viewQuery:function(e,t){if(1&e&&f.Kc(I,!0),2&e){let e;f.sc(e=f.ac())&&(t.Form=e.first)}},features:[f.xb],decls:10,vars:4,consts:[["class","page-header container-fluid",4,"ngIfHideHeader","ngIf"],["class","page-header section-header",4,"ngIf"],[1,"container-fluid"],[1,"row"],[1,"col-md-3"],[3,"IsEmbedded"],["DomainTree",""],[1,"col-md-9",2,"margin-top","11px","padding-left","0px"],["PageCategoryList",""],[1,"page-header","container-fluid"],[1,"col-sm-5","col-xs-12"],[4,"ngIf"],[1,"col-sm-7","col-xs-12"],[1,"col-md-12","col-xs-12","padTop","padBottom"],[1,"pull-last","btn-group"],[1,"btn","btn-primary",3,"click"],[1,"fa","fa-list","fa-lg"],[1,"btn","btn-success",3,"click"],[1,"fa","fa-plus","fa-lg"],[1,"btn","btn-info","margin-sides",3,"click"],[1,"fa","fa-redo"],["class","row",4,"ngIf"],[1,"breadcrumb"],["routerLink","/"],[1,"page-header","section-header"],[1,"col-md-8"],[1,"col-md-4"]],template:function(e,t){1&e&&(f.Ec(0,P,22,13,"div",0),f.Ec(1,E,8,4,"div",1),f.Sb(2,"div",2),f.Sb(3,"div",3),f.Sb(4,"div",4),f.Nb(5,"app-domain-tree",5,6),f.Rb(),f.Sb(7,"div",7),f.Nb(8,"app-page-category-list",5,8),f.Rb(),f.Rb(),f.Rb()),2&e&&(f.jc("ngIf",!t.IsEmbedded),f.Ab(1),f.jc("ngIf",t.IsEmbedded&&!t.HideHeader),f.Ab(4),f.jc("IsEmbedded",!0),f.Ab(3),f.jc("IsEmbedded",!0))},directives:[C.k,v.a,h.a,n.d],pipes:[A.c],encapsulation:2}),e})();const j=f.Ub(_);var k=a("yxSc");let H=(()=>{class e extends u.d{constructor(){super(...arguments),this.Service=new d.d,this.Gen=new k.a,this.ActiveTab="Controls",this.baseComponentList=[{name:"List"},{name:"Edit"},{name:"Tree"},{name:"Select"}]}LoadLookupsAsync(e){const t=Object.create(null,{LoadLookupsAsync:{get:()=>super.LoadLookupsAsync}});return Object(b.a)(this,void 0,void 0,function*(){var a=yield t.LoadLookupsAsync.call(this,e);return a.BaseComponent=this.baseComponentList,a})}Process(){this.Gen.CollectTemplateData(this.model.id).then(e=>{this.Fill(this.model.id)})}}return e.\u0275fac=function(t){return w(t||e)},e.\u0275cmp=f.Gb({type:e,selectors:[["ng-component"]],features:[f.xb],decls:0,vars:0,template:function(e,t){},encapsulation:2}),e})();const w=f.Ub(H);var D=a("s7LF"),M=a("XGJ5"),F=a("EuMQ");const x=["Form"];function O(e,t){if(1&e&&(f.Sb(0,"span"),f.Gc(1),f.Rb()),2&e){const e=f.dc(2);f.Ab(1),f.Ic(" - ",e.HeaderExtra,"")}}function N(e,t){1&e&&(f.Sb(0,"div",5),f.Sb(1,"ol",42),f.Sb(2,"li"),f.Sb(3,"a",43),f.Gc(4),f.ec(5,"translate"),f.Rb(),f.Rb(),f.Sb(6,"li"),f.Sb(7,"a",44),f.Gc(8),f.ec(9,"translate"),f.Rb(),f.Rb(),f.Sb(10,"li"),f.Gc(11),f.ec(12,"translate"),f.Rb(),f.Rb(),f.Rb()),2&e&&(f.Ab(4),f.Hc(f.fc(5,3,"Words.Main")),f.Ab(4),f.Hc(f.fc(9,5,"Pages.Templates__PageCategoriesTreeList")),f.Ab(3),f.Ic(" ",f.fc(12,7,"Pages.Templates__PageCategoryEdit")," "))}function V(e,t){if(1&e){const e=f.Tb();f.Sb(0,"div",31),f.Sb(1,"div",5),f.Sb(2,"div",32),f.Sb(3,"h2"),f.Gc(4),f.ec(5,"translate"),f.Ec(6,O,2,1,"span",13),f.Rb(),f.Rb(),f.Sb(7,"div",33),f.Sb(8,"div",5),f.Sb(9,"div",34),f.Sb(10,"div",35),f.Sb(11,"button",36),f.Zb("click",function(){return f.vc(e),f.dc().Process()}),f.Nb(12,"i",37),f.Gc(13),f.ec(14,"translate"),f.Rb(),f.Rb(),f.Rb(),f.Sb(15,"div",38),f.Sb(16,"div",39),f.Sb(17,"button",40),f.Zb("click",function(){return f.vc(e),f.dc().Refresh()}),f.Nb(18,"i",41),f.Rb(),f.Rb(),f.Rb(),f.Rb(),f.Rb(),f.Rb(),f.Ec(19,N,13,9,"div",3),f.Rb()}if(2&e){const e=f.dc();f.Ab(4),f.Hc(f.fc(5,4,"Pages.Templates__PageCategoryEdit")),f.Ab(2),f.jc("ngIf",e.HeaderExtra),f.Ab(7),f.Ic(" ",f.fc(14,6,"Words.Process")," "),f.Ab(6),f.jc("ngIf",!e.IsEmbedded)}}function Z(e,t){if(1&e&&(f.Sb(0,"span"),f.Gc(1),f.Rb()),2&e){const e=f.dc(2);f.Ab(1),f.Ic(" - ",e.HeaderExtra,"")}}function W(e,t){if(1&e&&(f.Sb(0,"div",45),f.Sb(1,"div",46),f.Sb(2,"div",5),f.Sb(3,"div",47),f.Gc(4),f.ec(5,"translate"),f.Ec(6,Z,2,1,"span",13),f.Rb(),f.Nb(7,"div",48),f.Rb(),f.Rb(),f.Rb()),2&e){const e=f.dc();f.Ab(4),f.Ic(" ",f.fc(5,2,"Pages.Templates__PageCategoryEdit"),""),f.Ab(2),f.jc("ngIf",e.HeaderExtra)}}function B(e,t){if(1&e){const e=f.Tb();f.Sb(0,"div",5),f.Sb(1,"div",49),f.Sb(2,"div",50),f.Sb(3,"button",51),f.Zb("click",function(){return f.vc(e),f.dc().CurrentLang="ar"}),f.Gc(4),f.ec(5,"translate"),f.Rb(),f.Sb(6,"button",51),f.Zb("click",function(){return f.vc(e),f.dc().CurrentLang="en"}),f.Gc(7),f.ec(8,"translate"),f.Rb(),f.Rb(),f.Rb(),f.Rb()}if(2&e){const e=f.dc();f.Ab(3),f.jc("ngClass","ar"==e.CurrentLang?"btn-primary":"btn-default"),f.Ab(1),f.Ic(" ",f.fc(5,4,"Words.ar")," "),f.Ab(2),f.jc("ngClass","en"==e.CurrentLang?"btn-primary":"btn-default"),f.Ab(1),f.Ic(" ",f.fc(8,6,"Words.en")," ")}}function U(e,t){if(1&e&&(f.Sb(0,"option",11),f.Gc(1),f.Rb()),2&e){const e=t.$implicit;f.jc("ngValue",e.id),f.Ab(1),f.Hc(e.name)}}function z(e,t){if(1&e&&(f.Sb(0,"p"),f.Gc(1),f.Rb()),2&e){f.dc();const e=f.tc(16);f.Ab(1),f.Hc(e.Value)}}function J(e,t){if(1&e&&(f.Sb(0,"option",11),f.Gc(1),f.Rb()),2&e){const e=t.$implicit;f.jc("ngValue",e.name),f.Ab(1),f.Hc(e.name)}}function K(e,t){if(1&e&&(f.Sb(0,"p"),f.Gc(1),f.Rb()),2&e){f.dc();const e=f.tc(27);f.Ab(1),f.Hc(e.Value)}}function $(e,t){if(1&e&&(f.Sb(0,"option",11),f.Gc(1),f.Rb()),2&e){const e=t.$implicit;f.jc("ngValue",e.name),f.Ab(1),f.Hc(e.name)}}function Q(e,t){if(1&e&&(f.Sb(0,"p"),f.Gc(1),f.Rb()),2&e){f.dc();const e=f.tc(39);f.Ab(1),f.Hc(e.Value)}}function X(e,t){if(1&e&&(f.Sb(0,"tr"),f.Sb(1,"td"),f.Gc(2),f.Nb(3,"div",52),f.Rb(),f.Sb(4,"td"),f.Gc(5),f.Nb(6,"div",52),f.Rb(),f.Rb()),2&e){const e=t.$implicit;f.Ab(2),f.Ic(" ",e.controlType," "),f.Ab(3),f.Ic(" ",e.identifier," ")}}function Y(e,t){if(1&e&&(f.Sb(0,"tr"),f.Sb(1,"td"),f.Gc(2),f.ec(3,"translate"),f.Nb(4,"div",52),f.Rb(),f.Sb(5,"td"),f.Gc(6),f.Nb(7,"div",52),f.Rb(),f.Sb(8,"td"),f.Gc(9),f.Nb(10,"div",52),f.Rb(),f.Rb()),2&e){const e=t.$implicit;f.Ab(2),f.Ic(" ",f.fc(3,3,"Words."+e.typeString)," "),f.Ab(4),f.Ic(" ",e.name," "),f.Ab(3),f.Ic(" ",e.defaultValue," ")}}let q=(()=>{class e extends H{constructor(){super(...arguments),this.ViewParams={AddUrl:null,EditUrl:null,DetailsUrl:null,ListUrl:"/razor/templates/page-categories-tree-list",Fields:[],Other:{}},this.LookupOptions={Resources:"C0",BaseComponent:"C0",layouts:"C0"}}get CollectionId(){return null}}return e.\u0275fac=function(t){return ee(t||e)},e.\u0275cmp=f.Gb({type:e,selectors:[["app-page-category-edit"]],viewQuery:function(e,t){if(1&e&&f.Kc(x,!0),2&e){let e;f.sc(e=f.ac())&&(t.Form=e.first)}},features:[f.xb],decls:93,vars:74,consts:[["class","page-header container-fluid",4,"ngIfHideHeader","ngIf"],["class","page-header section-header",4,"ngIf"],[3,"ngClass"],["class","row",4,"ngIf"],["Form","ngForm"],[1,"row"],[1,"form-group","col-md-6"],[1,""],["id","FG_resourceId","bs-group","",1,"form-group","col-md-6"],["FG_resourceId","bsFormGroup"],["name","Form__resourceId",1,"form-control",3,"li-watch","ngModel","ngModelChange"],[3,"ngValue"],[3,"ngValue",4,"ngFor","ngForOf"],[4,"ngIf"],["id","FG_baseComponent","bs-group","",1,"form-group","col-md-6"],["FG_baseComponent","bsFormGroup"],["name","Form__baseComponent",1,"form-control",3,"li-watch","ngModel","ngModelChange"],["id","FG_layout","bs-group","",1,"form-group","col-md-6"],["FG_layout","bsFormGroup"],["name","Form__layout",1,"form-control",3,"li-watch","ngModel","ngModelChange"],[1,"col-md-6"],[1,"col-md-12","section-header"],[1,"table","table-striped","informative"],[1,"cursorPointer",3,"ngClass"],[1,"fa","fa-sort",3,"click"],[4,"ngFor","ngForOf"],[1,"table","table-bordered"],[1,"row","submit-buttons"],[1,"col-md-12"],[1,"pull-last"],[1,"btn","btn-primary",3,"disabled","click"],[1,"page-header","container-fluid"],[1,"col-sm-5","col-xs-12"],[1,"col-sm-7","col-xs-12"],[1,"col-md-8","col-xs-12","text-last","padTop"],[1,"btn-group"],[1,"btn","btn-warning",3,"click"],[1,"fa","fa-cog"],[1,"col-md-4","col-xs-12","padTop","padBottom"],[1,"pull-last","btn-group"],[1,"btn","btn-info","margin-sides",3,"click"],[1,"fa","fa-redo"],[1,"breadcrumb"],["routerLink","/"],["routerLink","/razor/templates/page-categories-tree-list"],[1,"page-header","section-header"],[1,"container-fluid"],[1,"col-md-8"],[1,"col-md-4"],[1,"col-xs-12","col-md-6","col-xs-offset-0","col-md-offset-6","padBottom"],[1,"btn-group","btn-group-sm","pull-last"],[1,"btn","btn-sm",3,"ngClass","click"],[2,"position","relative"]],template:function(e,t){if(1&e&&(f.Ec(0,V,20,8,"div",0),f.Ec(1,W,8,4,"div",1),f.Sb(2,"div",2),f.Sb(3,"div",2),f.Ec(4,B,9,8,"div",3),f.Sb(5,"form",null,4),f.Sb(7,"div",5),f.Sb(8,"div",6),f.Sb(9,"label"),f.Gc(10),f.ec(11,"translate"),f.Rb(),f.Sb(12,"p",7),f.Gc(13),f.Rb(),f.Rb(),f.Rb(),f.Sb(14,"div",5),f.Sb(15,"div",8,9),f.Sb(17,"label"),f.Gc(18),f.ec(19,"translate"),f.Rb(),f.Sb(20,"select",10),f.Zb("ngModelChange",function(e){return t.model.resourceId=e}),f.Sb(21,"option",11),f.Gc(22),f.ec(23,"translate"),f.Rb(),f.Ec(24,U,2,2,"option",12),f.Rb(),f.Ec(25,z,2,1,"p",13),f.Rb(),f.Sb(26,"div",14,15),f.Sb(28,"label"),f.Gc(29),f.ec(30,"translate"),f.Rb(),f.Sb(31,"select",16),f.Zb("ngModelChange",function(e){return t.model.baseComponent=e}),f.Sb(32,"option",11),f.Gc(33),f.ec(34,"translate"),f.Rb(),f.Ec(35,J,2,2,"option",12),f.Rb(),f.Ec(36,K,2,1,"p",13),f.Rb(),f.Rb(),f.Sb(37,"div",5),f.Sb(38,"div",17,18),f.Sb(40,"label"),f.Gc(41),f.ec(42,"translate"),f.Rb(),f.Sb(43,"select",19),f.Zb("ngModelChange",function(e){return t.model.layout=e}),f.Sb(44,"option",11),f.Gc(45),f.ec(46,"translate"),f.Rb(),f.Ec(47,$,2,2,"option",12),f.Rb(),f.Ec(48,Q,2,1,"p",13),f.Rb(),f.Rb(),f.Rb(),f.Sb(49,"div",20),f.Sb(50,"div",21),f.Gc(51),f.ec(52,"translate"),f.Rb(),f.Sb(53,"table",22),f.Sb(54,"thead"),f.Sb(55,"tr"),f.Sb(56,"th",23),f.Gc(57),f.ec(58,"translate"),f.Sb(59,"i",24),f.Zb("click",function(){return t.SortBy("ControlType")}),f.Rb(),f.Rb(),f.Sb(60,"th",23),f.Gc(61),f.ec(62,"translate"),f.Sb(63,"i",24),f.Zb("click",function(){return t.SortBy("Identifier")}),f.Rb(),f.Rb(),f.Rb(),f.Rb(),f.Sb(64,"tbody"),f.Ec(65,X,7,2,"tr",25),f.Rb(),f.Rb(),f.Rb(),f.Sb(66,"div",20),f.Sb(67,"div",21),f.Gc(68),f.ec(69,"translate"),f.Rb(),f.Sb(70,"table",26),f.Sb(71,"thead"),f.Sb(72,"tr"),f.Sb(73,"th",23),f.Gc(74),f.ec(75,"translate"),f.Sb(76,"i",24),f.Zb("click",function(){return t.SortBy("TypeString")}),f.Rb(),f.Rb(),f.Sb(77,"th",23),f.Gc(78),f.ec(79,"translate"),f.Sb(80,"i",24),f.Zb("click",function(){return t.SortBy("Name")}),f.Rb(),f.Rb(),f.Sb(81,"th",23),f.Gc(82),f.ec(83,"translate"),f.Sb(84,"i",24),f.Zb("click",function(){return t.SortBy("DefaultValue")}),f.Rb(),f.Rb(),f.Rb(),f.Rb(),f.Sb(85,"tbody"),f.Ec(86,Y,11,5,"tr",25),f.Rb(),f.Rb(),f.Rb(),f.Sb(87,"div",27),f.Sb(88,"div",28),f.Sb(89,"div",29),f.Sb(90,"button",30),f.Zb("click",function(){return t.Submit()}),f.Gc(91),f.ec(92,"translate"),f.Rb(),f.Rb(),f.Rb(),f.Rb(),f.Rb(),f.Rb()),2&e){const e=f.tc(16),a=f.tc(27),n=f.tc(39);f.jc("ngIf",!t.IsEmbedded),f.Ab(1),f.jc("ngIf",t.IsEmbedded&&!t.HideHeader),f.Ab(1),f.jc("ngClass",t.IsEmbedded?"panel panel-default embedded":"container-fluid animated fadeInRight"),f.Ab(1),f.jc("ngClass",t.IsEmbedded?"panel-body":"content-block"),f.Ab(1),f.jc("ngIf",t.UseLocalization),f.Ab(6),f.Hc(f.fc(11,44,"Columns.PageCategory__ViewPath")),f.Ab(3),f.Ic(" ",t.model.viewPath," "),f.Ab(5),f.Ic("",f.fc(19,46,"Columns.PageCategory__ResourceId")," "),f.Ab(2),f.jc("li-watch",t.model)("ngModel",t.model.resourceId),f.Ab(1),f.jc("ngValue",null),f.Ab(1),f.Ic("--(",f.fc(23,48,"Words.Empty"),")--"),f.Ab(2),f.jc("ngForOf",t.Lookups.Resources),f.Ab(1),f.jc("ngIf",!e.Write),f.Ab(4),f.Ic("",f.fc(30,50,"Columns.PageCategory__BaseComponent")," "),f.Ab(2),f.jc("li-watch",t.model)("ngModel",t.model.baseComponent),f.Ab(1),f.jc("ngValue",null),f.Ab(1),f.Ic("--(",f.fc(34,52,"Words.Empty"),")--"),f.Ab(2),f.jc("ngForOf",t.Lookups.BaseComponent),f.Ab(1),f.jc("ngIf",!a.Write),f.Ab(5),f.Ic("",f.fc(42,54,"Columns.PageCategory__Layout")," "),f.Ab(2),f.jc("li-watch",t.model)("ngModel",t.model.layout),f.Ab(1),f.jc("ngValue",null),f.Ab(1),f.Ic("--(",f.fc(46,56,"Words.Empty"),")--"),f.Ab(2),f.jc("ngForOf",t.Lookups.layouts),f.Ab(1),f.jc("ngIf",!n.Write),f.Ab(3),f.Ic(" ",f.fc(52,58,"Words.Controls")," "),f.Ab(5),f.jc("ngClass",t.GetHeaderClass("ControlType")),f.Ab(1),f.Ic(" ",f.fc(58,60,"Columns.Control__ControlType")," "),f.Ab(3),f.jc("ngClass",t.GetHeaderClass("Identifier")),f.Ab(1),f.Ic(" ",f.fc(62,62,"Columns.Control__Identifier")," "),f.Ab(4),f.jc("ngForOf",t.model.controls),f.Ab(3),f.Ic(" ",f.fc(69,64,"Words.Parameters")," "),f.Ab(5),f.jc("ngClass",t.GetHeaderClass("TypeString")),f.Ab(1),f.Ic(" ",f.fc(75,66,"Columns.PageCategoryParameter__TypeString")," "),f.Ab(3),f.jc("ngClass",t.GetHeaderClass("Name")),f.Ab(1),f.Ic(" ",f.fc(79,68,"Columns.PageCategoryParameter__Name")," "),f.Ab(3),f.jc("ngClass",t.GetHeaderClass("DefaultValue")),f.Ab(1),f.Ic(" ",f.fc(83,70,"Columns.PageCategoryParameter__DefaultValue")," "),f.Ab(4),f.jc("ngForOf",t.model.pageCategoryParameters),f.Ab(4),f.jc("disabled",!t.CanSubmit),f.Ab(1),f.Ic(" ",f.fc(92,72,"Words.Save")," ")}},directives:[C.k,C.i,D.u,D.j,D.k,M.a,D.r,F.a,D.i,D.l,D.m,D.t,C.j,n.d],pipes:[A.c],encapsulation:2}),e})();const ee=f.Ub(q);let te=(()=>{class e{constructor(e){e.use(i.a.Current.Language),e.setDefaultLang(i.a.Current.Language)}}return e.\u0275mod=f.Kb({type:e}),e.\u0275inj=f.Jb({factory:function(t){return new(t||e)(f.Wb(A.d))},imports:[[r.SharedModule,s.RazorModule,n.e.forChild([{path:"page-categories-tree-list",component:_,canActivate:[o.a],data:{name:"Templates__PageCategoriesTreeList",navigate:!1,resource:"PageCategoriesTreeList",action:"anonymous",apps:null}},{path:"page-category-edit/:id",component:q,canActivate:[o.a],data:{name:"Templates__PageCategoryEdit",navigate:!1,resource:"",action:"allowAll",apps:null}}])]]}),e})();c.b.Register("Razor/Templates/PageCategoriesTreeList",_),c.b.Register("Razor/Templates/PageCategoryEdit",q)}}]);