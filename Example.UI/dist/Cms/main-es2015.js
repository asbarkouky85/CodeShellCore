(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["main"],{

/***/ "+K43":
/*!************************************************************************!*\
  !*** ./src/core/codeshell/base-components/navigation-side-bar-base.ts ***!
  \************************************************************************/
/*! exports provided: NavigationSideBarBase */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "NavigationSideBarBase", function() { return NavigationSideBarBase; });
/* harmony import */ var codeshell_security__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! codeshell/security */ "U6Sh");
/* harmony import */ var codeshell_shell__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! codeshell/shell */ "UoIT");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/router */ "tyNb");
/* harmony import */ var codeshell_utilities_utils__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! codeshell/utilities/utils */ "VXkE");






class NavigationSideBarBase {
    constructor(Injector) {
        this.Injector = Injector;
        this.isLoggedIn = false;
        this.navs = [];
    }
    get Router() { return this.Injector.get(_angular_router__WEBPACK_IMPORTED_MODULE_3__["Router"]); }
    ngOnInit() {
        codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Session.LogStatus.subscribe((v) => {
            this.isLoggedIn = v;
            this.LoadNavigation();
        });
        codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Session.GetUserAsync()
            .then(user => {
            this.user = user;
            this.isLoggedIn = true;
            this.LoadNavigation();
        })
            .catch(d => {
            this.LoadNavigation();
        });
        this.OnReady();
    }
    OnReady() { }
    GetMainUrl() {
        return "/";
    }
    GotoMain() {
        var main = Object(codeshell_utilities_utils__WEBPACK_IMPORTED_MODULE_4__["absUrl"])(this.GetMainUrl());
        console.log(main);
        this.Router.navigateByUrl(main);
    }
    LoadNavigation() {
        var auth = codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Injector.get(codeshell_security__WEBPACK_IMPORTED_MODULE_0__["AuthorizationServiceBase"]);
        var doms = codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Injector.get(codeshell_security__WEBPACK_IMPORTED_MODULE_0__["DomainDataProvider"]);
        for (var dom of doms.Domains) {
            if (dom.name == "Main") {
                for (var c of dom.children) {
                    var r = Object.assign(new codeshell_security__WEBPACK_IMPORTED_MODULE_0__["RouteData"], c);
                    if (auth.IsAuthorized(this.user, r) && r.url) {
                        var item = {
                            name: r.name,
                            url: r.url
                        };
                        this.navs.push(item);
                    }
                }
            }
        }
    }
    Logout() {
        codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Session.EndSession();
        location.pathname = "/";
    }
}
NavigationSideBarBase.ɵfac = function NavigationSideBarBase_Factory(t) { return new (t || NavigationSideBarBase)(_angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_2__["Injector"])); };
NavigationSideBarBase.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵdefineComponent"]({ type: NavigationSideBarBase, selectors: [["ng-component"]], decls: 0, vars: 0, template: function NavigationSideBarBase_Template(rf, ctx) { }, encapsulation: 2 });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵsetClassMetadata"](NavigationSideBarBase, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_2__["Component"],
        args: [{ template: '' }]
    }], function () { return [{ type: _angular_core__WEBPACK_IMPORTED_MODULE_2__["Injector"] }]; }, null); })();


/***/ }),

/***/ 0:
/*!***************************!*\
  !*** multi ./src/main.ts ***!
  \***************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! C:\_abdelrahman\Personal\_gitHub\CodeShellCore\Example.UI\src\main.ts */"zUnb");


/***/ }),

/***/ "079c":
/*!**************************************************!*\
  !*** ./src/core/codeshell/base-module.module.ts ***!
  \**************************************************/
/*! exports provided: BaseModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "BaseModule", function() { return BaseModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "tyNb");
/* harmony import */ var _shell__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./shell */ "UoIT");
/* harmony import */ var _localization_translationService__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./localization/translationService */ "0ujZ");
/* harmony import */ var _serverConfigBase__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./serverConfigBase */ "6m0k");







class BaseModule {
    constructor(trans, router, conf) {
        trans.setDefaultLang(conf.Locale);
        trans.use(conf.Locale);
        router.events.subscribe(event => {
            if (event instanceof _angular_router__WEBPACK_IMPORTED_MODULE_1__["RouteConfigLoadStart"]) {
                _shell__WEBPACK_IMPORTED_MODULE_2__["Shell"].Main.ShowLoader = true;
            }
            if (event instanceof _angular_router__WEBPACK_IMPORTED_MODULE_1__["RouteConfigLoadEnd"]) {
                _shell__WEBPACK_IMPORTED_MODULE_2__["Shell"].Main.ShowLoader = false;
            }
        });
    }
}
BaseModule.ɵfac = function BaseModule_Factory(t) { return new (t || BaseModule)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵinject"](_localization_translationService__WEBPACK_IMPORTED_MODULE_3__["TranslationService"]), _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵinject"](_angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"]), _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵinject"](_serverConfigBase__WEBPACK_IMPORTED_MODULE_4__["ServerConfigBase"])); };
BaseModule.ɵprov = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineInjectable"]({ token: BaseModule, factory: BaseModule.ɵfac });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](BaseModule, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"]
    }], function () { return [{ type: _localization_translationService__WEBPACK_IMPORTED_MODULE_3__["TranslationService"] }, { type: _angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"] }, { type: _serverConfigBase__WEBPACK_IMPORTED_MODULE_4__["ServerConfigBase"] }]; }, null); })();


/***/ }),

/***/ "0Wec":
/*!*************************************************************!*\
  !*** ./src/core/codeshell/services/listSelectionService.ts ***!
  \*************************************************************/
/*! exports provided: ListSelectionService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ListSelectionService", function() { return ListSelectionService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var codeshell_main__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! codeshell/main */ "7OZ3");


class ListSelectionService {
    constructor() {
        this.List = [];
        this.Ids = [];
        this.itemsSelected = false;
        this._last = [];
        this.selectStart = -1;
        this._itemsSelectedChange = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this._selectionChange = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
    }
    get ItemsSelectedChange() {
        return this._itemsSelectedChange;
    }
    get SelectionChanged() {
        return this._selectionChange;
    }
    get SelectedItems() {
        return this.List.filter(d => d.rowSelected == true);
    }
    _updateSelectionState() {
        var anySelected = this.Ids.length > 0;
        if (anySelected != this.itemsSelected)
            this._itemsSelectedChange.emit(anySelected);
        this.itemsSelected = anySelected;
        if (JSON.stringify(this._last) != JSON.stringify(this.Ids))
            this._selectionChange.emit();
        this._last = this.Ids;
    }
    SetItemSelectionStatus(item, status, only = false) {
        if (only) {
            for (var i of this.List)
                i.rowSelected = false;
            status = true;
        }
        if (!status) {
            Object(codeshell_main__WEBPACK_IMPORTED_MODULE_1__["List_RemoveItem"])(this.Ids, item.id);
            item.rowSelected = false;
        }
        else {
            if (only) {
                this.Ids = [item.id];
            }
            else {
                this.Ids.push(item.id);
            }
            item.rowSelected = true;
        }
        this._updateSelectionState();
    }
    ItemClicked(item, event) {
        if (!event.shiftKey) {
            this.selectStart = this.List.indexOf(item);
            this.SetItemSelectionStatus(item, !item.rowSelected, !event.ctrlKey);
        }
        else {
            var current = this.List.indexOf(item);
            var selStart = current > this.selectStart ? this.selectStart : current;
            var selEnd = current > this.selectStart ? current : this.selectStart;
            for (var i = selStart; i <= selEnd; i++) {
                var it = this.List[i];
                this.SetItemSelectionStatus(it, true);
            }
        }
    }
    ClearSelection() {
        for (var i of this.List)
            i.rowSelected = false;
        this.Ids = [];
        this._updateSelectionState();
    }
}


/***/ }),

/***/ "0ujZ":
/*!***************************************************************!*\
  !*** ./src/core/codeshell/localization/translationService.ts ***!
  \***************************************************************/
/*! exports provided: TranslationService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "TranslationService", function() { return TranslationService; });
class TranslationService {
}


/***/ }),

/***/ "1Jgf":
/*!*********************************************!*\
  !*** ./src/core/codeshell/security/navs.ts ***!
  \*********************************************/
/*! exports provided: ModuleItem, FunctionItem */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ModuleItem", function() { return ModuleItem; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "FunctionItem", function() { return FunctionItem; });
class ModuleItem {
    constructor() {
        this.name = "";
        this.identifier = "";
        this.children = [];
        this.active = false;
    }
}
class FunctionItem {
    constructor() {
        this.name = "";
        this.url = "";
    }
}


/***/ }),

/***/ "2enV":
/*!***********************************************!*\
  !*** ./src/core/base/http/account.service.ts ***!
  \***********************************************/
/*! exports provided: AccountService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AccountService", function() { return AccountService; });
/* harmony import */ var codeshell_security__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! codeshell/security */ "U6Sh");

class AccountService extends codeshell_security__WEBPACK_IMPORTED_MODULE_0__["AccountServiceBase"] {
    constructor() {
        super(...arguments);
        this.BaseUrl = "/apiAction/Account";
    }
}


/***/ }),

/***/ "4N+8":
/*!************************************************!*\
  !*** ./src/core/codeshell/components/index.ts ***!
  \************************************************/
/*! exports provided: Paginate, SearchGroup, DurationInput */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _paginate_component__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./paginate.component */ "Rux6");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "Paginate", function() { return _paginate_component__WEBPACK_IMPORTED_MODULE_0__["Paginate"]; });

/* harmony import */ var _search_group_component__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./search-group.component */ "L7BY");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "SearchGroup", function() { return _search_group_component__WEBPACK_IMPORTED_MODULE_1__["SearchGroup"]; });

/* harmony import */ var _duration_input_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./duration-input.component */ "YNU7");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "DurationInput", function() { return _duration_input_component__WEBPACK_IMPORTED_MODULE_2__["DurationInput"]; });






/***/ }),

/***/ "5Qa7":
/*!******************************************************************!*\
  !*** ./src/core/codeshell/directives/image-preLoad.directive.ts ***!
  \******************************************************************/
/*! exports provided: ImagePreLoad */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ImagePreLoad", function() { return ImagePreLoad; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");


class ImagePreLoad {
    constructor(el) {
        this.el = el;
        this.isLoaded = false;
        let ht = el.nativeElement;
        this.awaiterDiv = document.createElement("div");
        this.awaiterDiv.classList.add("awaiter");
        this.awaiterDiv.style.width = "100%";
        this.awaiterDiv.style.display = "inline-block";
        this.awaiterDiv.style.height = "256px";
        this.awaiterDiv.style.position = "relative";
        for (let i = 0; i < ht.classList.length; i++) {
            let x = ht.classList.item(i);
            if (x)
                this.awaiterDiv.classList.add(x);
        }
        //$(ht).parent().append(this.awaiterDiv);
        //$(ht).hide();
        ht.addEventListener("load", (e) => {
            if (ht.complete && ht.naturalWidth !== 0) {
                //$(ht).fadeIn(500);
                //$(this.awaiterDiv).remove();
                this.isLoaded = true;
                return {};
            }
        });
        setTimeout(() => {
            if (!this.isLoaded) {
                //$(ht).fadeIn(500);
                ht.style.height = this.getClosestWidth(ht) + "px";
                // $(this.awaiterDiv).remove();
                this.isLoaded = true;
            }
        }, 2000);
    }
    getClosestWidth(el) {
        var w = el.getBoundingClientRect();
        if (w.width > 0)
            return w.width;
        else if (el.parentElement)
            return this.getClosestWidth(el.parentElement);
        else
            return 0;
    }
    ngOnInit() {
        if (this.awaiterDiv) {
            let ht = this.el.nativeElement;
            var w = this.getClosestWidth(ht);
            this.awaiterDiv.style.height = (w == 0) ? "256px" : w + "px";
        }
    }
}
ImagePreLoad.ɵfac = function ImagePreLoad_Factory(t) { return new (t || ImagePreLoad)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"])); };
ImagePreLoad.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({ type: ImagePreLoad, selectors: [["", "awaiter", ""]], exportAs: ["awaiter"] });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](ImagePreLoad, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
        args: [{ selector: "[awaiter]", exportAs: "awaiter" }]
    }], function () { return [{ type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"] }]; }, null); })();


/***/ }),

/***/ "5hkl":
/*!*******************************************************!*\
  !*** ./src/core/codeshell/helpers/localizablesDTO.ts ***!
  \*******************************************************/
/*! exports provided: LocalizablesDTO */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "LocalizablesDTO", function() { return LocalizablesDTO; });
/* harmony import */ var _listItem__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./listItem */ "JLZs");

class LocalizablesDTO extends _listItem__WEBPACK_IMPORTED_MODULE_0__["ListItem"] {
    constructor() {
        super(...arguments);
        this.langId = 0;
        this.data = {};
    }
}


/***/ }),

/***/ "62p0":
/*!*********************************************************!*\
  !*** ./src/core/codeshell/validators/modalValidator.ts ***!
  \*********************************************************/
/*! exports provided: ModalValidator */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ModalValidator", function() { return ModalValidator; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "3Pt+");




class ModalValidator {
    constructor(frm) {
        this.frm = frm;
        this._isValid = true;
        this.fieldName = "";
        this.control = new _angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormControl"]();
    }
    get IsValid() {
        return this._isValid;
    }
    set IsValid(val) {
        if (val) {
            this.control.setErrors({ required_modal: null });
            this.control.setValue("v");
            this.control.updateValueAndValidity();
        }
        else {
            this.control.setValue(null);
            this.control.setErrors({ required_modal: true });
            //this.control.updateValueAndValidity();
        }
        this._isValid = val;
    }
    ngOnInit() {
        this.frm.control.addControl(this.fieldName, this.control);
        this.frm.controls[this.fieldName] = this.control;
        this.control.markAsTouched();
        this.IsValid = false;
    }
    ngOnDestroy() {
        this.frm.control.removeControl(this.fieldName);
    }
}
ModalValidator.ɵfac = function ModalValidator_Factory(t) { return new (t || ModalValidator)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_forms__WEBPACK_IMPORTED_MODULE_1__["NgForm"])); };
ModalValidator.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({ type: ModalValidator, selectors: [["", "modal-required", ""]], inputs: { fieldName: "fieldName", IsValid: ["modal-required", "IsValid"] }, exportAs: ["modal-required"] });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](ModalValidator, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
        args: [{ selector: "[modal-required]", exportAs: "modal-required" }]
    }], function () { return [{ type: _angular_forms__WEBPACK_IMPORTED_MODULE_1__["NgForm"] }]; }, { fieldName: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["fieldName"]
        }], IsValid: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["modal-required"]
        }] }); })();


/***/ }),

/***/ "6Ipe":
/*!****************************************************************!*\
  !*** ./src/core/codeshell/directives/slim-scroll.directive.ts ***!
  \****************************************************************/
/*! exports provided: SlimScroll */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SlimScroll", function() { return SlimScroll; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");


class SlimScroll {
    constructor(el) {
        this.el = el;
        this.height = '250px';
    }
    ngOnInit() {
        // $(this.el.nativeElement).slimScroll({
        //     position: 'left',
        //     height: this.height,
        //     railColor: "white",
        //     railOpacity: 0.5
        // });
    }
    ngOnChanges() {
    }
}
SlimScroll.ɵfac = function SlimScroll_Factory(t) { return new (t || SlimScroll)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"])); };
SlimScroll.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({ type: SlimScroll, selectors: [["", "slimScroll", ""]], inputs: { height: ["slimScroll", "height"] }, exportAs: ["slimScroll"], features: [_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵNgOnChangesFeature"]] });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](SlimScroll, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
        args: [{
                selector: "[slimScroll]",
                exportAs: "slimScroll"
            }]
    }], function () { return [{ type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"] }]; }, { height: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["slimScroll"]
        }] }); })();


/***/ }),

/***/ "6XeC":
/*!*****************************************************!*\
  !*** ./src/core/codeshell/helpers/treeEventArgs.ts ***!
  \*****************************************************/
/*! exports provided: TreeEventArgs */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "TreeEventArgs", function() { return TreeEventArgs; });
class TreeEventArgs {
    constructor(EventName, Node) {
        this.EventName = EventName;
        this.Node = Node;
    }
}


/***/ }),

/***/ "6m0k":
/*!************************************************!*\
  !*** ./src/core/codeshell/serverConfigBase.ts ***!
  \************************************************/
/*! exports provided: ServerConfigBase */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ServerConfigBase", function() { return ServerConfigBase; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");


class ServerConfigBase {
    constructor() {
        this.DefaultLocale = "en";
        this.Locale = "en";
        this.Domain = "Auth";
        this.Version = null;
    }
}
ServerConfigBase.ɵfac = function ServerConfigBase_Factory(t) { return new (t || ServerConfigBase)(); };
ServerConfigBase.ɵprov = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineInjectable"]({ token: ServerConfigBase, factory: ServerConfigBase.ɵfac });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](ServerConfigBase, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"]
    }], null, null); })();


/***/ }),

/***/ "6mC2":
/*!*******************************************************************!*\
  !*** ./src/core/codeshell/base-components/selectComponentBase.ts ***!
  \*******************************************************************/
/*! exports provided: SelectComponentBase */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SelectComponentBase", function() { return SelectComponentBase; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "mrSG");
/* harmony import */ var _baseComponent__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./baseComponent */ "I5Ck");
/* harmony import */ var _helpers__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../helpers */ "GYDu");
/* harmony import */ var _services__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../services */ "RnJ/");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/core */ "fXoL");






class SelectComponentBase extends _baseComponent__WEBPACK_IMPORTED_MODULE_1__["BaseComponent"] {
    constructor() {
        super(...arguments);
        this.defaultLoader = (e) => Promise.resolve(new _helpers__WEBPACK_IMPORTED_MODULE_2__["LoadResult"]);
        this.Multi = true;
        this.Source = new _services__WEBPACK_IMPORTED_MODULE_3__["ListSource"](10, e => this.Loader(e));
        this._items = [];
        this.SelectHeight = "400px";
        this.SelectionChangedEvent = new _angular_core__WEBPACK_IMPORTED_MODULE_4__["EventEmitter"]();
    }
    get Loader() { return this.defaultLoader; }
    set Loader(val) { this.defaultLoader = val; }
    ;
    SelectionChanged() {
        this.SelectionChangedEvent.emit(this.Items);
    }
    get Items() { return this._items; }
    ;
    set Items(value) {
        this._items = value;
        this.Args.Source = this._items;
    }
    get Args() {
        if (!this.Source.TagArguments) {
            this.Source.TagArguments = {
                Data: this.Source.List,
                Source: this.Items,
                Comparer: (d, s) => s.id == d.id,
                CreateNew: d => {
                    return { id: d.id };
                }
            };
        }
        return this.Source.TagArguments;
    }
    Select(model) {
        model.Tag.SelectOnly(this.Items);
        this.Ok();
    }
    ClearSelection() {
        this.Items.length = 0;
        this.Ok();
    }
    LoadData() {
        this.Source.LoadData();
    }
    StartAsync() {
        return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, function* () {
            if (!this.Source.LoadedOnce) {
                yield this.Source.LoadDataAsync();
            }
            else {
                this.Source.Retag();
            }
            return this;
        });
    }
}
SelectComponentBase.ɵfac = function SelectComponentBase_Factory(t) { return ɵSelectComponentBase_BaseFactory(t || SelectComponentBase); };
SelectComponentBase.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵdefineComponent"]({ type: SelectComponentBase, selectors: [["ng-component"]], features: [_angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵInheritDefinitionFeature"]], decls: 0, vars: 0, template: function SelectComponentBase_Template(rf, ctx) { }, encapsulation: 2 });
const ɵSelectComponentBase_BaseFactory = /*@__PURE__*/ _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵgetInheritedFactory"](SelectComponentBase);
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵsetClassMetadata"](SelectComponentBase, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_4__["Component"],
        args: [{ template: '' }]
    }], null, null); })();


/***/ }),

/***/ "6qUw":
/*!*********************************************************!*\
  !*** ./src/core/codeshell/localization/localeLoader.ts ***!
  \*********************************************************/
/*! exports provided: LocaleLoader */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "LocaleLoader", function() { return LocaleLoader; });
class LocaleLoader {
}


/***/ }),

/***/ "7OZ3":
/*!************************************!*\
  !*** ./src/core/codeshell/main.ts ***!
  \************************************/
/*! exports provided: ServerConfigBase, Registry, absUrl, List_RemoveItem, List_RunRecursively_Nodes, List_OrderBy, List_OrderByDesc, List_FindIdRecusive, List_RunRecursively, List_RunRecursively_GEN, List_RunRecursivelyUp_GEN, List_ToggleItem, String_GetBeforeLast, String_GetAfterLast, Date_Get, Date_Elapsed, KeyValuePair, Utils, Shell, CodeShellModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _serverConfigBase__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./serverConfigBase */ "6m0k");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "ServerConfigBase", function() { return _serverConfigBase__WEBPACK_IMPORTED_MODULE_0__["ServerConfigBase"]; });

/* harmony import */ var _utilities_registry__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./utilities/registry */ "ppms");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "Registry", function() { return _utilities_registry__WEBPACK_IMPORTED_MODULE_1__["Registry"]; });

/* harmony import */ var _utilities_utils__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./utilities/utils */ "VXkE");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "absUrl", function() { return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["absUrl"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "List_RemoveItem", function() { return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["List_RemoveItem"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "List_RunRecursively_Nodes", function() { return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["List_RunRecursively_Nodes"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "List_OrderBy", function() { return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["List_OrderBy"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "List_OrderByDesc", function() { return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["List_OrderByDesc"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "List_FindIdRecusive", function() { return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["List_FindIdRecusive"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "List_RunRecursively", function() { return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["List_RunRecursively"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "List_RunRecursively_GEN", function() { return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["List_RunRecursively_GEN"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "List_RunRecursivelyUp_GEN", function() { return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["List_RunRecursivelyUp_GEN"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "List_ToggleItem", function() { return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["List_ToggleItem"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "String_GetBeforeLast", function() { return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["String_GetBeforeLast"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "String_GetAfterLast", function() { return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["String_GetAfterLast"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "Date_Get", function() { return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["Date_Get"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "Date_Elapsed", function() { return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["Date_Elapsed"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "KeyValuePair", function() { return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["KeyValuePair"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "Utils", function() { return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["Utils"]; });

/* harmony import */ var _shell__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./shell */ "UoIT");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "Shell", function() { return _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"]; });

/* empty/unused harmony star reexport *//* harmony import */ var _codeshell_module__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./codeshell.module */ "bT1W");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "CodeShellModule", function() { return _codeshell_module__WEBPACK_IMPORTED_MODULE_4__["CodeShellModule"]; });









/***/ }),

/***/ "8sze":
/*!***********************************************!*\
  !*** ./src/core/base/main/login.component.ts ***!
  \***********************************************/
/*! exports provided: Login */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Login", function() { return Login; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var codeshell_base_components__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! codeshell/base-components */ "gyvI");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "3Pt+");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/common */ "ofXK");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/router */ "tyNb");
/* harmony import */ var _ngx_translate_core__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @ngx-translate/core */ "sYmb");
/* harmony import */ var _codeshell_pipes_absoluteUrl__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ../../codeshell/pipes/absoluteUrl */ "WyvS");








const _c0 = function (a0) { return { p0: a0 }; };
function Login_span_21_small_1_Template(rf, ctx) { if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "small", 21);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](2, "translate");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](3, "translate");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
} if (rf & 2) {
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind2"](2, 1, "Messages.field_required", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpureFunction1"](6, _c0, _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind1"](3, 4, "Words.UserName"))));
} }
function Login_span_21_Template(rf, ctx) { if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "span");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](1, Login_span_21_small_1_Template, 4, 8, "small", 20);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
} if (rf & 2) {
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();
    const _r1 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵreference"](19);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", _r1.errors.required);
} }
function Login_div_35_Template(rf, ctx) { if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "div", 22);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](1, "a", 23);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](2, "absUrl");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](3);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](4, "translate");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
} if (rf & 2) {
    const ctx_r4 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("routerLink", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind1"](2, 2, ctx_r4.ForgotPasswordUrl));
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind1"](4, 4, "Words.ForgotPassword"));
} }
class Login extends codeshell_base_components__WEBPACK_IMPORTED_MODULE_1__["LoginBase"] {
    constructor() {
        super(...arguments);
        this.ForgotPasswordUrl = "/Auth/ForgotPassword";
    }
}
Login.ɵfac = function Login_Factory(t) { return ɵLogin_BaseFactory(t || Login); };
Login.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineComponent"]({ type: Login, selectors: [["ng-component"]], features: [_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵInheritDefinitionFeature"]], decls: 41, vars: 24, consts: [[1, "container"], [1, "row"], [1, "col-md-4", "col-md-offset-4", "col-sm-12", "col-sm-offset-0", "content-block", "animated", "fadeInDownBig"], [1, "not-mob"], [1, "title-header", "text-center", "page-header"], [1, "order-price"], ["Form", "ngForm"], [1, "col-md-12"], [1, "form-group"], ["type", "text", "name", "UserName", "required", "", 1, "form-control", 2, "direction", "ltr", 3, "ngModel", "placeholder", "ngModelChange"], ["UserName", "ngModel"], [4, "ngIf"], ["name", "Password", "required", "", "type", "password", 1, "form-control", 2, "direction", "ltr", 3, "ngModel", "placeholder", "ngModelChange"], ["Password", "ngModel"], [1, "col-md-7"], [1, "chiller_cb"], ["type", "checkbox", "name", "Remember", 3, "ngModel", "ngModelChange"], ["class", "col-md-5", 4, "ngIf"], ["type", "submit", 1, "btn", "btn-primary", "btn-block", "btn-lg", 3, "disabled", "click"], ["aria-hidden", "true", 1, "fa", "fa-sign-in-alt"], ["class", "form-text text-danger", 4, "ngIf"], [1, "form-text", "text-danger"], [1, "col-md-5"], [3, "routerLink"]], template: function Login_Template(rf, ctx) { if (rf & 1) {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "div", 0);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](1, "section", 1);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](2, "br");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](3, "br");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](4, "div", 2);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](5, "br", 3);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](6, "div", 4);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](7, "h2");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](8);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](9, "translate");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](10, "span");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](11);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](12, "translate");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](13, "form", 5, 6);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](15, "div", 1);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](16, "div", 7);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](17, "div", 8);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](18, "input", 9, 10);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("ngModelChange", function Login_Template_input_ngModelChange_18_listener($event) { return ctx.model.UserName = $event; });
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](20, "translate");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](21, Login_span_21_Template, 2, 1, "span", 11);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](22, "div", 8);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](23, "input", 12, 13);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("ngModelChange", function Login_Template_input_ngModelChange_23_listener($event) { return ctx.model.Password = $event; });
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](25, "translate");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](26, "div", 8);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](27, "div", 1);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](28, "div", 14);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](29, "label", 15);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](30, "input", 16);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("ngModelChange", function Login_Template_input_ngModelChange_30_listener($event) { return ctx.model.RememberMe = $event; });
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](31, "span");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](32, "strong");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](33);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](34, "translate");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](35, Login_div_35_Template, 5, 6, "div", 17);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](36, "div", 8);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](37, "button", 18);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function Login_Template_button_click_37_listener() { return ctx.Login(); });
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](38);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](39, "translate");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](40, "i", 19);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
    } if (rf & 2) {
        const _r0 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵreference"](14);
        const _r1 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵreference"](19);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](8);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate1"]("", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind1"](9, 12, "Words.Log__"), " ");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](3);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind1"](12, 14, "Words.In__"));
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](7);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpropertyInterpolate"]("placeholder", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind1"](20, 16, "Words.UserName"));
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngModel", ctx.model.UserName);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](3);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", _r1.invalid && (_r1.dirty || _r1.touched));
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpropertyInterpolate"]("placeholder", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind1"](25, 18, "Words.Password"));
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngModel", ctx.model.Password);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](7);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngModel", ctx.model.RememberMe);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](3);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind1"](34, 20, "Words.KeepMeLoggedIn"));
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", ctx.ForgotPasswordUrl);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("disabled", _r0.invalid);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate1"](" ", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind1"](39, 22, "Words.Login"), " ");
    } }, directives: [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["ɵangular_packages_forms_forms_y"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["NgControlStatusGroup"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["NgForm"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["DefaultValueAccessor"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["RequiredValidator"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["NgControlStatus"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["NgModel"], _angular_common__WEBPACK_IMPORTED_MODULE_3__["NgIf"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["CheckboxControlValueAccessor"], _angular_router__WEBPACK_IMPORTED_MODULE_4__["RouterLinkWithHref"]], pipes: [_ngx_translate_core__WEBPACK_IMPORTED_MODULE_5__["TranslatePipe"], _codeshell_pipes_absoluteUrl__WEBPACK_IMPORTED_MODULE_6__["AbsoluteUrl"]], encapsulation: 2 });
const ɵLogin_BaseFactory = /*@__PURE__*/ _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵgetInheritedFactory"](Login);
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](Login, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"],
        args: [{ templateUrl: "./login.component.html" }]
    }], null, null); })();


/***/ }),

/***/ "8v3h":
/*!*******************************************************!*\
  !*** ./src/core/codeshell/localization/translator.ts ***!
  \*******************************************************/
/*! exports provided: Translator */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Translator", function() { return Translator; });
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! rxjs */ "qCKp");

class Translator /* extends TranslateLoader*/ {
    static SetLoaders(loaders) {
        Translator.Loaders = loaders;
    }
    getTranslation(lang) {
        let res = {};
        if (Translator.Loaders[lang] != undefined) {
            res = Translator.Loaders[lang].Load();
        }
        else {
            res = {
                Columns: {},
                Words: {},
                Messages: {},
                Pages: {}
            };
        }
        return Object(rxjs__WEBPACK_IMPORTED_MODULE_0__["of"])(res);
    }
}
Translator.Loaders = {};


/***/ }),

/***/ "9loO":
/*!*********************************************************!*\
  !*** ./src/core/codeshell/validators/rangeValidator.ts ***!
  \*********************************************************/
/*! exports provided: NumberRangeValidator */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "NumberRangeValidator", function() { return NumberRangeValidator; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var _validatorBase__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./validatorBase */ "cPZL");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "3Pt+");




class NumberRangeValidator extends _validatorBase__WEBPACK_IMPORTED_MODULE_1__["ValidatorBase"] {
    constructor(el, mod) {
        super(el, mod);
        this.Identifier = "number_range";
    }
    get Max() { return this._max; }
    ;
    set Max(val) { this._max = val; }
    ;
    ngOnInit() {
        if (this.Max != undefined)
            this.Element.max = this.Max.toString();
    }
    RunIf(ch) {
        return ch.Min != undefined || ch.Max != undefined;
    }
    IsValid() {
        var v = true;
        if (this.Max) {
            v = v && this.Model.value < this.Max;
        }
        if (this.Min) {
            v = v && this.Model.value > this.Min;
        }
        return v;
    }
}
NumberRangeValidator.ɵfac = function NumberRangeValidator_Factory(t) { return new (t || NumberRangeValidator)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]), _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_forms__WEBPACK_IMPORTED_MODULE_2__["NgModel"])); };
NumberRangeValidator.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({ type: NumberRangeValidator, selectors: [["", "numberRange", "", "ngModel", ""]], inputs: { Min: ["min", "Min"], Max: ["max", "Max"] }, exportAs: ["numberRange"], features: [_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵInheritDefinitionFeature"]] });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](NumberRangeValidator, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
        args: [{ selector: '[numberRange][ngModel]', exportAs: 'numberRange' }]
    }], function () { return [{ type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"] }, { type: _angular_forms__WEBPACK_IMPORTED_MODULE_2__["NgModel"] }]; }, { Min: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["min"]
        }], Max: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["max"]
        }] }); })();


/***/ }),

/***/ "9uFN":
/*!*********************************************************!*\
  !*** ./src/core/codeshell/security/sessionTokenData.ts ***!
  \*********************************************************/
/*! exports provided: SessionTokenData */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SessionTokenData", function() { return SessionTokenData; });
/* harmony import */ var _tokenStorage__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./tokenStorage */ "yvMo");
/* harmony import */ var _models__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./models */ "gFuU");
/* harmony import */ var codeshell_services_stored__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! codeshell/services/stored */ "mHXC");



class SessionTokenData extends _tokenStorage__WEBPACK_IMPORTED_MODULE_0__["TokenStorage"] {
    LoadToken() {
        return codeshell_services_stored__WEBPACK_IMPORTED_MODULE_2__["Stored"].Get_SS("TokenData", _models__WEBPACK_IMPORTED_MODULE_1__["TokenData"]);
    }
    SaveToken(data) {
        codeshell_services_stored__WEBPACK_IMPORTED_MODULE_2__["Stored"].Set_SS("TokenData", data);
    }
    Clear() {
        codeshell_services_stored__WEBPACK_IMPORTED_MODULE_2__["Stored"].Clear_SS("TokenData");
        localStorage.removeItem("refresh");
    }
}


/***/ }),

/***/ "AytR":
/*!*****************************************!*\
  !*** ./src/environments/environment.ts ***!
  \*****************************************/
/*! exports provided: environment */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "environment", function() { return environment; });
const environment = {
    production: false
};


/***/ }),

/***/ "CJgx":
/*!***************************************************************!*\
  !*** ./src/core/codeshell/directives/selectable.directive.ts ***!
  \***************************************************************/
/*! exports provided: Selectable */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Selectable", function() { return Selectable; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");





class Selectable {
    constructor(ref) {
        this.model = {};
        this.Element = ref.nativeElement;
    }
    get ngClass() { return this.model.rowSelected ? 'list-item selected' : 'list-item'; }
    OnClick(event) {
        if (this.Service)
            this.Service.ItemClicked(this.model, event);
    }
}
Selectable.ɵfac = function Selectable_Factory(t) { return new (t || Selectable)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"])); };
Selectable.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({ type: Selectable, selectors: [["", "selectable", ""]], hostVars: 2, hostBindings: function Selectable_HostBindings(rf, ctx) { if (rf & 1) {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function Selectable_click_HostBindingHandler($event) { return ctx.OnClick($event); });
    } if (rf & 2) {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵclassMap"](ctx.ngClass);
    } }, inputs: { model: ["selectable", "model"], Service: ["select-service", "Service"] }, exportAs: ["selectable"] });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](Selectable, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
        args: [{ selector: "[selectable]", exportAs: "selectable" }]
    }], function () { return [{ type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"] }]; }, { model: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["selectable"]
        }], Service: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["select-service"]
        }], ngClass: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["HostBinding"],
            args: ["class"]
        }], OnClick: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"],
            args: ["click", ["$event"]]
        }] }); })();


/***/ }),

/***/ "DTUp":
/*!*******************************************************!*\
  !*** ./src/core/base/app-component-base.component.ts ***!
  \*******************************************************/
/*! exports provided: AppComponentBase */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppComponentBase", function() { return AppComponentBase; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var codeshell_base_components__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! codeshell/base-components */ "gyvI");
/* harmony import */ var codeshell_directives__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! codeshell/directives */ "XgJV");




class AppComponentBase extends codeshell_base_components__WEBPACK_IMPORTED_MODULE_1__["IAppComponent"] {
}
AppComponentBase.ɵfac = function AppComponentBase_Factory(t) { return ɵAppComponentBase_BaseFactory(t || AppComponentBase); };
AppComponentBase.ɵprov = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineInjectable"]({ token: AppComponentBase, factory: AppComponentBase.ɵfac });
const ɵAppComponentBase_BaseFactory = /*@__PURE__*/ _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵgetInheritedFactory"](AppComponentBase);
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](AppComponentBase, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"]
    }], null, { ModalLoader: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ViewChild"],
            args: [codeshell_directives__WEBPACK_IMPORTED_MODULE_2__["ComponentLoader"]]
        }] }); })();


/***/ }),

/***/ "EuMQ":
/*!**********************************************************************!*\
  !*** ./src/core/codeshell/directives/list-item-watcher.directive.ts ***!
  \**********************************************************************/
/*! exports provided: ListItemWatcher */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ListItemWatcher", function() { return ListItemWatcher; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var _helpers__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../helpers */ "GYDu");



class ListItemWatcher {
    constructor(cont) {
        this.item = null;
    }
    Change(ev) {
        if (this.item) {
            this.item.SetModified();
        }
    }
    ngOnInit() {
        if (this.mod instanceof _helpers__WEBPACK_IMPORTED_MODULE_1__["ListItem"]) {
            this.item = this.mod;
        }
    }
}
ListItemWatcher.ɵfac = function ListItemWatcher_Factory(t) { return new (t || ListItemWatcher)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"])); };
ListItemWatcher.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({ type: ListItemWatcher, selectors: [["", "acs-li-watch", ""]], hostBindings: function ListItemWatcher_HostBindings(rf, ctx) { if (rf & 1) {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("change", function ListItemWatcher_change_HostBindingHandler($event) { return ctx.Change($event); });
    } }, inputs: { mod: ["acs-li-watch", "mod"] }, exportAs: ["liWatch"] });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](ListItemWatcher, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
        args: [{ selector: "[acs-li-watch]", exportAs: "liWatch" }]
    }], function () { return [{ type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"] }]; }, { mod: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["acs-li-watch"]
        }], Change: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"],
            args: ["change", ['$event']]
        }] }); })();


/***/ }),

/***/ "F2JI":
/*!*************************************************************!*\
  !*** ./src/core/codeshell/directives/on-enter.directive.ts ***!
  \*************************************************************/
/*! exports provided: OnEnter */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "OnEnter", function() { return OnEnter; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");


class OnEnter {
    constructor() {
        this.out = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
    }
    OnKeyPress(e) {
        if (e.key == "Enter")
            this.out.emit();
    }
}
OnEnter.ɵfac = function OnEnter_Factory(t) { return new (t || OnEnter)(); };
OnEnter.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({ type: OnEnter, selectors: [["input", "onEnter", ""]], hostBindings: function OnEnter_HostBindings(rf, ctx) { if (rf & 1) {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("keypress", function OnEnter_keypress_HostBindingHandler($event) { return ctx.OnKeyPress($event); });
    } }, outputs: { out: "onEnter" } });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](OnEnter, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
        args: [{ selector: "input[onEnter]" }]
    }], null, { out: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"],
            args: ["onEnter"]
        }], OnKeyPress: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"],
            args: ["keypress", ['$event']]
        }] }); })();


/***/ }),

/***/ "G778":
/*!*****************************************************************!*\
  !*** ./src/core/codeshell/base-components/editComponentBase.ts ***!
  \*****************************************************************/
/*! exports provided: EditComponentBase */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EditComponentBase", function() { return EditComponentBase; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "mrSG");
/* harmony import */ var _baseComponent__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./baseComponent */ "I5Ck");
/* harmony import */ var _helpers__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../helpers */ "GYDu");
/* harmony import */ var _shell__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../shell */ "UoIT");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/router */ "tyNb");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var _helpers_localizablesDTO__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ../helpers/localizablesDTO */ "5hkl");
/* harmony import */ var codeshell_utilities_utils__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! codeshell/utilities/utils */ "VXkE");










class EditComponentBase extends _baseComponent__WEBPACK_IMPORTED_MODULE_1__["BaseComponent"] {
    constructor() {
        super(...arguments);
        this.model = {};
        this.CurrentLang = "ar";
        this.UI_Lang = "ar";
        this._formState = "VALID";
        this._formValidity = new _angular_core__WEBPACK_IMPORTED_MODULE_5__["EventEmitter"]();
        this.UseLocalization = false;
        this.Localizables = {};
        this.HideButtons = false;
        this._lookupsLoaded = false;
        this.isBound = false;
        this.LoadLookups = false;
        this.IsNew = true;
        this.FormIsValid = true;
        this.SubmitClicked = false;
        this.DataSubmittedEvent = new _angular_core__WEBPACK_IMPORTED_MODULE_5__["EventEmitter"]();
        this.RouteParams = {};
    }
    get ValidState() {
        return this._formValidity;
    }
    get CanSubmit() {
        return this.FormIsValid;
    }
    set CanSubmit(st) { }
    get Loc() {
        if (!this.Localizables[this.CurrentLang])
            this.Localizables[this.CurrentLang] = new _helpers_localizablesDTO__WEBPACK_IMPORTED_MODULE_6__["LocalizablesDTO"];
        return this.Localizables[this.CurrentLang];
    }
    ngOnInit() {
        super.ngOnInit();
        this.model = this._bound ? this._bound : this.DefaultModel();
        let lookupOpts = this.GetLookupOptions();
        if (!this.IsEmbedded) {
            if (lookupOpts == null && !this.LoadLookups) {
                this.StartComponent();
            }
            else {
                this.LoadLookupsAsync(lookupOpts).then(lookups => {
                    this.Lookups = lookups;
                    this.StartComponent();
                });
            }
        }
        this.UI_Lang = "en";
        this.CurrentLang = _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Main.Config.DefaultLocale == "ar" ? "en" : "ar";
        if (this.Form && this.Form.statusChanges) {
            this.Form.statusChanges.subscribe(d => {
                if (d != this._formState) {
                    this.FormIsValid = d != "INVALID";
                    this.OnFormValidityChange(this.FormIsValid);
                    this.EmitValidity();
                }
                this._formState = d;
            });
            setTimeout(() => this.EmitValidity(), 200);
        }
    }
    IsValid() {
        let valid = true;
        debugger;
        if (this.Form) {
            valid = valid && !this.Form.invalid;
        }
        if (this.FormGroup) {
            valid = valid && this.FormGroup.invalid;
        }
        return valid;
    }
    get ModelId() { return this.model.id; }
    set ModelId(val) { this.model.id = val; }
    DefaultModel() { return {}; }
    OnFormValidityChange(state) {
    }
    EmitValidity() {
        this._formValidity.emit(this.CanSubmit);
    }
    LoadLookupsAsync(opts) {
        return this.Service.GetEditLookups(opts);
    }
    StartEditOrCreate() {
        if (!this.IsNew) {
            this.modelId = Number.parseInt(this.RouteParams['id']);
            this.Fill(this.modelId);
        }
        else {
            this.StartNew();
        }
    }
    StartComponent() {
        if (!this.IsEmbedded) {
            this.Route.params.subscribe(params => {
                this.RouteParams = params;
                this.IsNew = this.RouteParams['id'] == undefined;
                this.StartEditOrCreate();
            });
        }
        else {
            this.IsInitialized = true;
            this.OnReady();
        }
    }
    _loadLookupsOnce() {
        return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, function* () {
            if (this._lookupsLoaded)
                return;
            let lookupOpts = this.GetLookupOptions();
            if (lookupOpts != null || this.LoadLookups) {
                this.Lookups = yield this.LoadLookupsAsync(lookupOpts);
            }
            this._lookupsLoaded = true;
        });
    }
    _componentStarted() {
        this.IsInitialized = true;
        setTimeout(() => this.SetAccessibility(), 700);
        if (this.UseLocalization && this.model && this.ModelId != 0) {
            this.Service.GetLocalizationData(this.ModelId).then(s => {
                this.Localizables = s;
            });
        }
        this.OnReady();
    }
    FillAsync(id) {
        return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, function* () {
            yield this._loadLookupsOnce();
            this.model = yield this.GetModelFromServerAsync(id);
            this.IsNew = false;
            this._componentStarted();
        });
    }
    Clear() {
        this.model = this.DefaultModel();
        this.Localizables = {};
        this.IsNew = true;
    }
    BindAsync(mod) {
        return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, function* () {
            yield this._loadLookupsOnce();
            this._bound = mod;
            this.model = mod;
            this._componentStarted();
        });
    }
    Fill(id) {
        this.GetModelFromServerAsync(id).then(v => {
            this.model = v;
            this.IsNew = false;
            this._componentStarted();
        });
    }
    Bind(item) {
        this._bound = item;
        if (this.IsInitialized) {
            this.model = item;
            setTimeout(() => this.SetAccessibility(), 500);
            this.OnReady();
        }
        this.Show = true;
        if (this.UseLocalization && this._bound && this._bound.id != 0) {
            this.Service.GetLocalizationData(this._bound.id).then(s => {
                this.Localizables = s;
            });
        }
    }
    StartNew() {
        this.InitializeNewModelAsync().then(() => {
            this.IsInitialized = true;
            setTimeout(() => this.SetAccessibility(), 500);
            this.OnReady();
        });
    }
    SetAccessibility() { }
    /**
     * Is called before submit to check if the form is valid
     * */
    Validate() {
        if (this.Form && this.Form.invalid) {
            return { success: false, message: "invalid_form", type: _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"].Error };
        }
        return { success: true, type: _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"].Success };
    }
    /**
     * Can be overridden to enable initializing the model on the server side
     * */
    InitializeNewModelAsync() {
        return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, function* () { });
    }
    /**
     * When an id is found in the route params this method is called to obtain model from server using that id
     * Can be overridden
     * @param id as obtaind from url
     */
    GetModelFromServerAsync(id) {
        return this.Service.GetSingle(id);
    }
    /**
     * Called after lookups and model is both loaded
     * */
    OnReady() { }
    ;
    SubmitLocalizablesAsync() {
        return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, function* () {
            var s = {};
            let submit = false;
            for (var i in this.Localizables) {
                if (this.Localizables[i].state == "Modified" || this.Localizables[i].state == "Added") {
                    s[i] = this.Localizables[i];
                    submit = true;
                }
            }
            if (submit) {
                var e = this.model;
                var res = yield this.Service.SetLocalizationData(this.ModelId, s);
            }
        });
    }
    SubmitNewAsync() {
        return this.Service.Post("Post", this.model);
    }
    SubmitUpdateAsync() {
        return this.Service.Put("Put", this.model);
    }
    SubmitAsync() {
        return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, function* () {
            let prom;
            if (this.IsNew) {
                prom = yield this.SubmitNewAsync();
                if (prom.data.Id) {
                    this.ModelId = prom.data.Id;
                    yield this.SubmitLocalizablesAsync();
                }
            }
            else {
                prom = yield this.SubmitUpdateAsync();
                yield this.SubmitLocalizablesAsync();
            }
            return prom;
        });
    }
    Submit() {
        if (!this.IsValid()) {
            this.SubmitClicked = true;
            return;
        }
        this.SubmitAsync().then(e => {
            this.OnSubmitSuccess(e);
            if (this.DataSubmitted)
                this.DataSubmitted(this.model, e);
            this.DataSubmittedEvent.emit(e);
        }).catch(x => {
            this.OnSubmitFailed(x);
        });
    }
    OnSubmitSuccess(res) {
        this.NotifyTranslate(res.message, _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"].Success);
        if (!this.IsEmbedded) {
            if (this.ViewParams.ListUrl) {
                let s = _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Injector.get(_angular_router__WEBPACK_IMPORTED_MODULE_4__["Router"]);
                s.navigateByUrl(this.ViewParams.ListUrl);
            }
            else {
                this.Navigation.back();
            }
        }
        else if (this.model.id) {
            //this.StartEditOrCreate();
        }
    }
    OnSubmitFailed(res) {
        codeshell_utilities_utils__WEBPACK_IMPORTED_MODULE_7__["Utils"].HandleError(res, true);
    }
    Delete(id) {
        _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Main.ShowDeleteConfirm().then(e => {
            if (e) {
                this.Service.Delete("Delete", id).then(e => {
                    if (!this.IsEmbedded) {
                        this.NotifyTranslate("delete_success");
                        if (this.ViewParams.ListUrl) {
                            this.NavigateToComponent(this.ViewParams.ListUrl);
                        }
                        else {
                            this.Navigation.back();
                        }
                    }
                }).catch(e => {
                    codeshell_utilities_utils__WEBPACK_IMPORTED_MODULE_7__["Utils"].HandleError(e, true, true);
                });
            }
        });
    }
}
EditComponentBase.ɵfac = function EditComponentBase_Factory(t) { return ɵEditComponentBase_BaseFactory(t || EditComponentBase); };
EditComponentBase.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵdefineComponent"]({ type: EditComponentBase, selectors: [["ng-component"]], outputs: { ValidState: "is-valid" }, features: [_angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵInheritDefinitionFeature"]], decls: 0, vars: 0, template: function EditComponentBase_Template(rf, ctx) { }, encapsulation: 2 });
const ɵEditComponentBase_BaseFactory = /*@__PURE__*/ _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵgetInheritedFactory"](EditComponentBase);
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵsetClassMetadata"](EditComponentBase, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_5__["Component"],
        args: [{ template: '' }]
    }], null, { ValidState: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_5__["Output"],
            args: ["is-valid"]
        }] }); })();


/***/ }),

/***/ "GYDu":
/*!*********************************************!*\
  !*** ./src/core/codeshell/helpers/index.ts ***!
  \*********************************************/
/*! exports provided: DTO, TmpFileData, Result, SubmitResult, DeleteResult, LoadResult, LoadResultGen, PropertyFilter, LoadOptions, NoteType, ListItem, BoundListItem, ViewParams, TaggedArgs, Tagged, Stored, RecursionModel, ComponentRequest, LocalizablesDTO, TreeEventArgs, EditablePairsDTO */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _interfaces__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./interfaces */ "OI30");
/* empty/unused harmony star reexport *//* harmony import */ var _models__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./models */ "dpcW");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "DTO", function() { return _models__WEBPACK_IMPORTED_MODULE_1__["DTO"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "TmpFileData", function() { return _models__WEBPACK_IMPORTED_MODULE_1__["TmpFileData"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "Result", function() { return _models__WEBPACK_IMPORTED_MODULE_1__["Result"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "SubmitResult", function() { return _models__WEBPACK_IMPORTED_MODULE_1__["SubmitResult"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "DeleteResult", function() { return _models__WEBPACK_IMPORTED_MODULE_1__["DeleteResult"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "LoadResult", function() { return _models__WEBPACK_IMPORTED_MODULE_1__["LoadResult"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "LoadResultGen", function() { return _models__WEBPACK_IMPORTED_MODULE_1__["LoadResultGen"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "PropertyFilter", function() { return _models__WEBPACK_IMPORTED_MODULE_1__["PropertyFilter"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "LoadOptions", function() { return _models__WEBPACK_IMPORTED_MODULE_1__["LoadOptions"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "NoteType", function() { return _models__WEBPACK_IMPORTED_MODULE_1__["NoteType"]; });

/* harmony import */ var _listItem__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./listItem */ "JLZs");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "ListItem", function() { return _listItem__WEBPACK_IMPORTED_MODULE_2__["ListItem"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "BoundListItem", function() { return _listItem__WEBPACK_IMPORTED_MODULE_2__["BoundListItem"]; });

/* harmony import */ var _viewParams__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./viewParams */ "jBBj");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "ViewParams", function() { return _viewParams__WEBPACK_IMPORTED_MODULE_3__["ViewParams"]; });

/* harmony import */ var _tagged__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./tagged */ "wGhu");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "TaggedArgs", function() { return _tagged__WEBPACK_IMPORTED_MODULE_4__["TaggedArgs"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "Tagged", function() { return _tagged__WEBPACK_IMPORTED_MODULE_4__["Tagged"]; });

/* harmony import */ var _services_stored__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../services/stored */ "mHXC");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "Stored", function() { return _services_stored__WEBPACK_IMPORTED_MODULE_5__["Stored"]; });

/* harmony import */ var _recursionModel__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./recursionModel */ "fYjR");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "RecursionModel", function() { return _recursionModel__WEBPACK_IMPORTED_MODULE_6__["RecursionModel"]; });

/* harmony import */ var _componentRequest__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./componentRequest */ "K+1B");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "ComponentRequest", function() { return _componentRequest__WEBPACK_IMPORTED_MODULE_7__["ComponentRequest"]; });

/* harmony import */ var _localizablesDTO__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./localizablesDTO */ "5hkl");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "LocalizablesDTO", function() { return _localizablesDTO__WEBPACK_IMPORTED_MODULE_8__["LocalizablesDTO"]; });

/* harmony import */ var _treeEventArgs__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ./treeEventArgs */ "6XeC");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "TreeEventArgs", function() { return _treeEventArgs__WEBPACK_IMPORTED_MODULE_9__["TreeEventArgs"]; });

/* harmony import */ var _editablePairsDTO__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ./editablePairsDTO */ "Se+h");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "EditablePairsDTO", function() { return _editablePairsDTO__WEBPACK_IMPORTED_MODULE_10__["EditablePairsDTO"]; });














/***/ }),

/***/ "I5Ck":
/*!*************************************************************!*\
  !*** ./src/core/codeshell/base-components/baseComponent.ts ***!
  \*************************************************************/
/*! exports provided: BaseComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "BaseComponent", function() { return BaseComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "mrSG");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/router */ "tyNb");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/common */ "ofXK");
/* harmony import */ var _shell__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../shell */ "UoIT");
/* harmony import */ var _security_models__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../security/models */ "gFuU");
/* harmony import */ var _helpers__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ../helpers */ "GYDu");
/* harmony import */ var codeshell_main__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! codeshell/main */ "7OZ3");










class BaseComponent {
    constructor(route, inj) {
        this.IsEmbedded = false;
        this._subs = {};
        this._modalOk = false;
        this._modalCancel = false;
        this.RouteData = new _security_models__WEBPACK_IMPORTED_MODULE_5__["RouteData"]();
        this.Lookups = {};
        this.ViewParams = new _helpers__WEBPACK_IMPORTED_MODULE_6__["ViewParams"]();
        this.UseLocalization = false;
        this.Debug = true;
        this.Permission = _security_models__WEBPACK_IMPORTED_MODULE_5__["Permission"].Anonymous;
        this.ScreenHeight = window.innerHeight + 'px';
        this.Title = "";
        this.IsInitialized = false;
        this.Show = false;
        this.ShowOnReady = false;
        this.HideHeader = false;
        this.SubmitClicked = false;
        this.IsChild = false;
        this.ModalWidth = 768;
        this.OnOk = e => Promise.resolve(true);
        this.OnCancel = e => Promise.resolve(true);
        this.Route = route;
        this.Injector = inj;
    }
    get Navigation() { return _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Injector.get(_angular_common__WEBPACK_IMPORTED_MODULE_3__["Location"]); }
    get Loc() { return new _helpers__WEBPACK_IMPORTED_MODULE_6__["LocalizablesDTO"](); }
    ;
    get ComponentName() { return this.constructor.name; }
    get Router() { return _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Injector.get(_angular_router__WEBPACK_IMPORTED_MODULE_2__["Router"]); }
    get Resolver() { return this.Injector.get(_angular_core__WEBPACK_IMPORTED_MODULE_1__["ComponentFactoryResolver"]); }
    ;
    ngOnInit() {
        this.construct();
    }
    construct() {
        let conf = this.Route.routeConfig;
        if (conf) {
            if (conf.data) {
                this.RouteData = Object.assign(new _security_models__WEBPACK_IMPORTED_MODULE_5__["RouteData"], conf.data);
            }
            else if (this.ComponentRouteData) {
                this.RouteData = Object.assign(new _security_models__WEBPACK_IMPORTED_MODULE_5__["RouteData"](), this.ComponentRouteData);
            }
            else {
                this.RouteData = Object.assign(new _security_models__WEBPACK_IMPORTED_MODULE_5__["RouteData"](), { action: "anonymous" });
            }
            _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Main.SetTitle(this.RouteData.name);
            if (this.RouteData.IsAnonymous) {
                this.Permission = _security_models__WEBPACK_IMPORTED_MODULE_5__["Permission"].FullAccess;
            }
            else if (!_shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Session.IsLoggedIn) {
                _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Injector.get(_angular_router__WEBPACK_IMPORTED_MODULE_2__["Router"]).navigate(["/Login"]);
            }
            else if (this.RouteData.AllowAll) {
                this.Permission = _security_models__WEBPACK_IMPORTED_MODULE_5__["Permission"].FullAccess;
            }
            else {
                this.Permission = _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Session.GetPermission(this.RouteData.resource);
            }
        }
        else {
            this.IsEmbedded = true;
        }
    }
    GetMainUrl() {
        return _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Main.GetMainUrl();
    }
    ngAfterViewInit() {
        _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].ViewLoaded.emit({});
    }
    Log(...params) {
        if (this.Debug) {
            console.log(this.ComponentName, ...params);
        }
    }
    SortBy(prop) { }
    GetHeaderClass(prop) { return null; }
    GetObjectFromHtml(ref) {
        let el = ref.nativeElement;
        var attr = el.attributes.getNamedItem("values");
        if (attr != null && attr.value.length > 0) {
            var s = eval('(' + attr.value + ')');
            if (s) {
                return s;
            }
        }
        return null;
    }
    GetObjectFromHtmlAs(ref, type) {
        var s = this.GetObjectFromHtml(ref);
        if (s != null)
            return Object.assign(new type, s);
        return null;
    }
    OnConstructed() { }
    GetComponent(opener, createNew = false) {
        return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, function* () {
            let comp;
            if (createNew)
                _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Main.ClearModalLoader();
            if (this._subs[opener.Identifier] == undefined) {
                comp = yield this.LoadComponentFromParams(opener.Identifier, opener.DefaultComponent);
                if (!comp) {
                    this._subs[opener.Identifier] = null;
                }
                else if (opener.Init) {
                    opener.Init(comp.instance);
                }
                if (!createNew)
                    this._subs[opener.Identifier] = comp;
            }
            else {
                comp = this._subs[opener.Identifier];
            }
            if (comp) {
                this.LoadComponentRef(comp);
                return Promise.resolve(comp.instance);
            }
            else
                return Promise.reject("failed to obtain");
        });
    }
    OpenModal(path) {
        //var comp = Shell.Main.GetDialogAs<T>(path);
        let e = codeshell_main__WEBPACK_IMPORTED_MODULE_7__["Registry"].Get(path);
        if (e) {
            console.log(this.Resolver);
            var fac = this.Resolver.resolveComponentFactory(e);
            let comp = fac.create(_shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Injector);
            if (comp != null && _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Main.ModalLoader) {
                _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Main.ModalLoader.UseComponent(comp);
                return Promise.resolve(comp);
            }
        }
        return Promise.reject("not found");
    }
    LoadComponentRef(ref) {
        if (_shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Main.ModalLoader) {
            _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Main.ModalLoader.UseComponent(ref);
        }
    }
    LoadComponentFromParams(fromOther, def) {
        var path = this.ViewParams.Other[fromOther];
        if (path) {
            return this.OpenModal(path);
        }
        else if (def) {
            return this.OpenModal(def);
        }
        return Promise.reject("No key '" + fromOther + "' in ViewParams.Other");
    }
    ModalSearch(modelId, term) { }
    GetLookupOptions() {
        return this.LookupOptions;
    }
    Refresh() {
        this.ngOnInit();
    }
    GetParameter(key, def = null) {
        var val = this.ViewParams.Other[key];
        if (val && val.length > 0)
            return this.ViewParams.Other[key];
        else
            return def;
    }
    GetParameterAsBoolean(key, def = false) {
        var val = this.GetParameter(key);
        if (val != null) {
            if (val.toLowerCase() == "true")
                return true;
            else if (val.toLowerCase() == "false")
                return false;
        }
        return def;
    }
    GetPermission(resource) {
        return _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Session.GetPermission(resource);
    }
    Notify(message, type = _helpers__WEBPACK_IMPORTED_MODULE_6__["NoteType"].Success, title = undefined) {
        _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Main.Notify(message, type, title);
    }
    NotifyCanNotDeleteRow(res) {
        //debugger
        let tableName = "";
        if (res.tableName)
            tableName = _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Word(res.tableName);
        let mess = _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Message("entity_has_children", tableName || "Unknown");
        this.Notify(mess, _helpers__WEBPACK_IMPORTED_MODULE_6__["NoteType"].Error, undefined);
    }
    NotifyTranslate(messageId, type = _helpers__WEBPACK_IMPORTED_MODULE_6__["NoteType"].Success, title = undefined) {
        _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Main.NotifyTranslate(messageId, type, title);
    }
    OnModalHide(ev = null) {
        if (!this._modalOk && !this._modalCancel)
            this.Cancel();
        this._modalOk = false;
        this._modalCancel = false;
    }
    Ok() {
        this._modalOk = true;
        this.OnOk(this).then(e => {
            if (e) {
                this.Show = false;
            }
        });
    }
    Cancel() {
        this._modalCancel = true;
        this.OnCancel(this).then(e => {
            if (e)
                this.Show = false;
        });
    }
    NavigateToComponent(url, ext) {
        if (url.length > 0) {
            if (url[0] != "/")
                url = "/" + url;
            this.Router.navigate([url], ext);
        }
    }
    ngOnDestroy() {
        for (var i in this._subs) {
            var c = this._subs[i];
            c.destroy();
        }
    }
}
BaseComponent.ɵfac = function BaseComponent_Factory(t) { return new (t || BaseComponent)(_angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdirectiveInject"](_angular_router__WEBPACK_IMPORTED_MODULE_2__["ActivatedRoute"]), _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injector"])); };
BaseComponent.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdefineComponent"]({ type: BaseComponent, selectors: [["ng-component"]], inputs: { IsEmbedded: "IsEmbedded" }, decls: 0, vars: 0, template: function BaseComponent_Template(rf, ctx) { }, encapsulation: 2 });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵsetClassMetadata"](BaseComponent, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"],
        args: [{ template: '' }]
    }], function () { return [{ type: _angular_router__WEBPACK_IMPORTED_MODULE_2__["ActivatedRoute"] }, { type: _angular_core__WEBPACK_IMPORTED_MODULE_1__["Injector"] }]; }, { IsEmbedded: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_1__["Input"],
            args: ["IsEmbedded"]
        }] }); })();


/***/ }),

/***/ "InfA":
/*!********************************************!*\
  !*** ./src/core/codeshell/localization.ts ***!
  \********************************************/
/*! exports provided: LocaleLoader, TranslationService, NgxTranslationService, Translator */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _localization_localeLoader__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./localization/localeLoader */ "6qUw");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "LocaleLoader", function() { return _localization_localeLoader__WEBPACK_IMPORTED_MODULE_0__["LocaleLoader"]; });

/* harmony import */ var _localization_translationService__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./localization/translationService */ "0ujZ");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "TranslationService", function() { return _localization_translationService__WEBPACK_IMPORTED_MODULE_1__["TranslationService"]; });

/* harmony import */ var _localization_ngxTransationService__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./localization/ngxTransationService */ "Sgtl");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "NgxTranslationService", function() { return _localization_ngxTransationService__WEBPACK_IMPORTED_MODULE_2__["NgxTranslationService"]; });

/* harmony import */ var _localization_translator__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./localization/translator */ "8v3h");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "Translator", function() { return _localization_translator__WEBPACK_IMPORTED_MODULE_3__["Translator"]; });







/***/ }),

/***/ "JLZs":
/*!************************************************!*\
  !*** ./src/core/codeshell/helpers/listItem.ts ***!
  \************************************************/
/*! exports provided: ListItem, BoundListItem */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ListItem", function() { return ListItem; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "BoundListItem", function() { return BoundListItem; });
class ListItem {
    constructor() {
        this.id = 0;
        this.state = "Detached";
        this.selected = false;
    }
    static IsEmpty(items) {
        if (!items || items.length == 0)
            return true;
        return !items.some(d => d.state != "Removed");
    }
    static GetLength(items) {
        if (!items)
            return 0;
        return items.filter(e => e.state != 'Removed' && e.state != 'Detached').length;
    }
    static GetChangedItems(items) {
        return items.filter(e => e.state == "Added" || e.state == "Modified" || e.state == "Removed");
    }
    static GetAdded(items) {
        return items.filter(e => e.state == "Added");
    }
    static GetModifiedOrDeleted(items) {
        return items.filter(e => e.state == "Modified" || e.state == "Removed");
    }
    static HasChanges(items) {
        return items.some(e => e.state == "Added" || e.state == "Modified" || e.state == "Removed");
    }
    static FromDB(obj) {
        let it = Object.assign(new ListItem, obj);
        it.selected = true;
        it.state = "Attached";
        return it;
    }
    static FromDB_GEN(con, obj) {
        let it = Object.assign(new con, obj);
        it.selected = true;
        it.state = "Attached";
        return it;
    }
    static Detached(obj) {
        let it = Object.assign(new ListItem, obj);
        it.selected = false;
        it.state = "Detached";
        return it;
    }
    static Detached_GEN(con, obj) {
        let it = Object.assign(new con, obj);
        it.selected = false;
        it.state = "Detached";
        return it;
    }
    AddToChangeList() {
        if (this.state == "Detached" || !this.state) {
            this.state = "Added";
        }
        else if (this.state == "Attached") {
            this.state = "Modified";
        }
    }
    SetModified() {
        if (this.state != "Added" && this.state != "Detached")
            this.state = "Modified";
    }
    SetRemoved() {
        if (this.state != "Added")
            this.state = "Removed";
    }
    SetAdded() {
        if (this.state == "Removed")
            this.state = "Attached";
        else if (this.state != "Modified" && this.state != "Attached")
            this.state = "Added";
    }
    SetAttached() {
        var r = ["Added", "Removed", "Modified"];
        if (r.indexOf(this.state) > -1)
            this.state = "Attached";
    }
    ApplyTo(items) {
        if (this.selected)
            this.AddTo(items);
        else
            this.RemoveFrom(items);
    }
    RemoveFrom(items) {
        if (this.state == "Added") {
            let ind = items.indexOf(this);
            if (ind > -1)
                items.splice(ind, 1);
            this.state = "Detached";
        }
        else {
            this.state = "Removed";
        }
        this.selected = false;
    }
    AddTo(items) {
        if (this.state == "Removed") {
            this.state = "Attached";
        }
        else if (this.state != "Added" && this.state != "Attached") {
            this.state = "Added";
            items.push(this);
        }
        this.selected = true;
    }
    SelectOnly(items) {
        let x = [];
        for (let d of items)
            x.push(d);
        for (let d of x)
            d.RemoveFrom(items);
        this.AddTo(items);
    }
    static Convert(lst) {
        let ret = [];
        for (var i in lst)
            ret[i] = ListItem.FromDB(lst[i]);
        return ret;
    }
    static Convert_GEN(con, lst) {
        let ret = [];
        for (var i in lst)
            ret[i] = ListItem.FromDB_GEN(con, lst[i]);
        return ret;
    }
}
class BoundListItem extends ListItem {
}


/***/ }),

/***/ "K+1B":
/*!********************************************************!*\
  !*** ./src/core/codeshell/helpers/componentRequest.ts ***!
  \********************************************************/
/*! exports provided: ComponentRequest */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ComponentRequest", function() { return ComponentRequest; });
class ComponentRequest {
    constructor() {
        this.Identifier = "";
        this.DefaultComponent = "";
    }
}


/***/ }),

/***/ "L7BY":
/*!*****************************************************************!*\
  !*** ./src/core/codeshell/components/search-group.component.ts ***!
  \*****************************************************************/
/*! exports provided: SearchGroup */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SearchGroup", function() { return SearchGroup; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "3Pt+");



const _c0 = function () { return { standalone: true }; };
class SearchGroup {
    constructor() {
        this.ChangeEvent = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
    }
    OnSearch() {
        this.ChangeEvent.emit(this.SearchTerm);
    }
}
SearchGroup.ɵfac = function SearchGroup_Factory(t) { return new (t || SearchGroup)(); };
SearchGroup.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineComponent"]({ type: SearchGroup, selectors: [["search-group"]], outputs: { ChangeEvent: "termChange" }, decls: 6, vars: 4, consts: [[1, "col-sm-12"], [1, "input-group"], ["type", "text", 1, "form-control", 3, "ngModel", "ngModelOptions", "placeholder", "ngModelChange", "keydown.enter"], [1, "input-group-btn"], [1, "btn", "btn-default", 3, "click"], [1, "fa", "fa-search"]], template: function SearchGroup_Template(rf, ctx) { if (rf & 1) {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "div", 0);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](1, "div", 1);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](2, "input", 2);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("ngModelChange", function SearchGroup_Template_input_ngModelChange_2_listener($event) { return ctx.SearchTerm = $event; })("keydown.enter", function SearchGroup_Template_input_keydown_enter_2_listener() { return ctx.OnSearch(); });
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](3, "span", 3);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](4, "button", 4);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function SearchGroup_Template_button_click_4_listener() { return ctx.OnSearch(); });
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](5, "i", 5);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
    } if (rf & 2) {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpropertyInterpolate"]("placeholder", "Words.Search");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngModel", ctx.SearchTerm)("ngModelOptions", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpureFunction0"](3, _c0));
    } }, directives: [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["DefaultValueAccessor"], _angular_forms__WEBPACK_IMPORTED_MODULE_1__["NgControlStatus"], _angular_forms__WEBPACK_IMPORTED_MODULE_1__["NgModel"]], encapsulation: 2 });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](SearchGroup, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"],
        args: [{
                templateUrl: "./search-group.component.html",
                selector: "search-group"
            }]
    }], null, { ChangeEvent: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"],
            args: ["termChange"]
        }] }); })();


/***/ }),

/***/ "LR6F":
/*!****************************************************!*\
  !*** ./src/core/codeshell/http/httpServiceBase.ts ***!
  \****************************************************/
/*! exports provided: HttpServiceBase */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "HttpServiceBase", function() { return HttpServiceBase; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "mrSG");
/* harmony import */ var _security_sessionManager__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../security/sessionManager */ "M6Pn");
/* harmony import */ var _angular_common_http___WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/common/http/ */ "tk/3");
/* harmony import */ var _shell__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../shell */ "UoIT");
/* harmony import */ var _httpRequest__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./httpRequest */ "vyk5");
/* harmony import */ var _utilities_utils__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../utilities/utils */ "VXkE");






class HttpServiceBase {
    constructor() {
        this.Silent = false;
        this.Client = _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Injector.get(_angular_common_http___WEBPACK_IMPORTED_MODULE_2__["HttpClient"]);
        this.Sessions = _security_sessionManager__WEBPACK_IMPORTED_MODULE_1__["SessionManager"].Current();
        this.Server = _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Main.Config;
    }
    get Headers() {
        let head = {
            "tenant-code": this.Server.Domain,
            "locale": this.Server.Locale,
            "ui-version": this.Server.Version
        };
        this.Sessions.CheckToken();
        let tok = this.Sessions.GetToken();
        if (tok != null)
            head["auth-token"] = tok.Token;
        if (this.SignalRConnctionId)
            head["connection-id"] = this.SignalRConnctionId;
        this.AddCustomHeaders(head);
        return head;
        return {};
    }
    AddCustomHeaders(data) { }
    Get(action, params) {
        let req = this.InitializeRequest(action, params);
        return this.process(_httpRequest__WEBPACK_IMPORTED_MODULE_4__["Methods"].Get, req);
    }
    GetAsHtml(action, params) {
        let req = this.InitializeRequest(action, params);
        if (req.Params.headers) {
            req.Params.responseType = "text/html";
        }
        return this.process(_httpRequest__WEBPACK_IMPORTED_MODULE_4__["Methods"].Get, req);
    }
    Post(action, body, params) {
        let req = this.InitializeRequest(action, params, body);
        return this.process(_httpRequest__WEBPACK_IMPORTED_MODULE_4__["Methods"].Post, req);
    }
    Put(action, body, params) {
        let req = this.InitializeRequest(action, params, body);
        return this.process(_httpRequest__WEBPACK_IMPORTED_MODULE_4__["Methods"].Put, req);
    }
    Delete(action, id) {
        let req = this.InitializeRequest(action, id);
        return this.process(_httpRequest__WEBPACK_IMPORTED_MODULE_4__["Methods"].Delete, req);
    }
    GetAs(action, params) {
        let req = this.InitializeRequest(action, params);
        return this.processAs(_httpRequest__WEBPACK_IMPORTED_MODULE_4__["Methods"].Get, req);
    }
    PostAs(action, body, params) {
        let req = this.InitializeRequest(action, params, body);
        return this.processAs(_httpRequest__WEBPACK_IMPORTED_MODULE_4__["Methods"].Post, req);
    }
    InitializeRequest(action, params, body) {
        let url = this.BaseUrl + "/" + action;
        let r = new _httpRequest__WEBPACK_IMPORTED_MODULE_4__["HttpRequest"](url, params, body);
        r.Params.headers = this.Headers;
        return r;
    }
    process(method, req) {
        return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, function* () {
            var p = new Promise(() => { });
            if (!this.Silent)
                _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Main.ShowLoading();
            switch (method) {
                case _httpRequest__WEBPACK_IMPORTED_MODULE_4__["Methods"].Get:
                    p = this.Client.get(req.Url, req.Params).toPromise();
                    break;
                case _httpRequest__WEBPACK_IMPORTED_MODULE_4__["Methods"].Post:
                    p = this.Client.post(req.Url, req.Body, req.Params).toPromise();
                    break;
                case _httpRequest__WEBPACK_IMPORTED_MODULE_4__["Methods"].Put:
                    p = this.Client.put(req.Url, req.Body, req.Params).toPromise();
                    break;
                case _httpRequest__WEBPACK_IMPORTED_MODULE_4__["Methods"].Delete:
                    p = this.Client.delete(req.Url, req.Params).toPromise();
                    break;
            }
            p.catch(e => this.OnError(e));
            p.then(e => this.OnRequestProcessed(e));
            return p;
        });
    }
    processAs(method, req) {
        var p = new Promise(() => { });
        if (!this.Silent)
            _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Main.ShowLoading();
        switch (method) {
            case _httpRequest__WEBPACK_IMPORTED_MODULE_4__["Methods"].Get:
                p = this.Client.get(req.Url, req.Params).toPromise();
                break;
            case _httpRequest__WEBPACK_IMPORTED_MODULE_4__["Methods"].Post:
                p = this.Client.post(req.Url, req.Body, req.Params).toPromise();
                break;
            case _httpRequest__WEBPACK_IMPORTED_MODULE_4__["Methods"].Put:
                p = this.Client.put(req.Url, req.Body, req.Params).toPromise();
                break;
            case _httpRequest__WEBPACK_IMPORTED_MODULE_4__["Methods"].Delete:
                p = this.Client.delete(req.Url, req.Params).toPromise();
                break;
        }
        p.catch(e => this.OnError(e));
        p.then(e => this.OnRequestProcessed(e));
        return p;
    }
    OnError(e) {
        _utilities_utils__WEBPACK_IMPORTED_MODULE_5__["Utils"].HandleError(e);
        _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Main.HideLoading();
    }
    OnRequestProcessed(e) {
        _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Main.HideLoading();
    }
}


/***/ }),

/***/ "M6Pn":
/*!*******************************************************!*\
  !*** ./src/core/codeshell/security/sessionManager.ts ***!
  \*******************************************************/
/*! exports provided: SessionManager */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SessionManager", function() { return SessionManager; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "mrSG");
/* harmony import */ var _security_models__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../security/models */ "gFuU");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var _helpers__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../helpers */ "GYDu");
/* harmony import */ var js_cookie__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! js-cookie */ "p46w");
/* harmony import */ var js_cookie__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(js_cookie__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _utilities_utils__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../utilities/utils */ "VXkE");
/* harmony import */ var _shell__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ../shell */ "UoIT");
/* harmony import */ var _accountServiceBase__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./accountServiceBase */ "wNr3");
/* harmony import */ var _tokenStorage__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./tokenStorage */ "yvMo");










var userData = null;
var loaded = false;
class SessionManager {
    constructor() {
        this._tokenData = null;
        this.IsLoggedIn = false;
        this.LogStatus = new _angular_core__WEBPACK_IMPORTED_MODULE_2__["EventEmitter"](false);
        this.OnLogin = new _angular_core__WEBPACK_IMPORTED_MODULE_2__["EventEmitter"]();
        this.OnUserDataFailed = new _angular_core__WEBPACK_IMPORTED_MODULE_2__["EventEmitter"]();
    }
    get TokenStorage() {
        if (!this._tokenStorage)
            this._tokenStorage = _shell__WEBPACK_IMPORTED_MODULE_6__["Shell"].Injector.get(_tokenStorage__WEBPACK_IMPORTED_MODULE_8__["TokenStorage"]);
        return this._tokenStorage;
    }
    get User() {
        if (!userData)
            throw "Not logged in";
        return userData;
    }
    TryRefreshAsync() {
        return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, function* () {
            var ref = this.TokenStorage.GetRefreshToken();
            if (ref) {
                var serv = _shell__WEBPACK_IMPORTED_MODULE_6__["Shell"].Injector.get(_accountServiceBase__WEBPACK_IMPORTED_MODULE_7__["AccountServiceBase"]);
                try {
                    var data = yield serv.RefreshToken(ref);
                    this.StartSession(data);
                }
                catch (e) {
                    Promise.reject("Failed to refresh using token");
                }
            }
            return Promise.reject("No token or refresh token found");
        });
    }
    ReloadUserDataAsync() {
        return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, function* () {
            var token = this.GetToken();
            if (!token)
                return Promise.reject("Cannot reload user data without token");
            yield this.GetUserDataFromServer();
            if (userData) {
                this.MapPermissions(userData);
                this.OnLogin.emit(userData);
            }
            return userData;
        });
    }
    GetUserAsync() {
        if (userData) {
            return Promise.resolve(userData);
        }
        if (!SessionManager._loadPromise) {
            var token = this.GetToken();
            if (!token) {
                SessionManager._loadPromise = this.TryRefreshAsync();
            }
            else {
                SessionManager._loadPromise = this.GetUserDataFromServer();
            }
        }
        return SessionManager._loadPromise;
    }
    GetUserDataFromServer() {
        return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, function* () {
            try {
                var serv = _shell__WEBPACK_IMPORTED_MODULE_6__["Shell"].Injector.get(_accountServiceBase__WEBPACK_IMPORTED_MODULE_7__["AccountServiceBase"]);
                userData = yield serv.GetUserData();
                this.MapPermissions(userData);
                return Promise.resolve(userData);
            }
            catch (e) {
                var res = new _helpers__WEBPACK_IMPORTED_MODULE_3__["SubmitResult"];
                res.code = 1;
                res.message = "unable_to_connect_to_server";
                if (e.error && e.error.code) {
                    res = Object.assign(new _helpers__WEBPACK_IMPORTED_MODULE_3__["SubmitResult"], e.error);
                }
                this.OnUserDataFailed.emit(res);
                return Promise.reject(e);
            }
        });
    }
    MapPermissions(data) {
        if (data) {
            for (var i in data.permissions) {
                var item = new _security_models__WEBPACK_IMPORTED_MODULE_1__["Permission"];
                Object.assign(item, data.permissions[i]);
                data.permissions[i] = item;
            }
        }
    }
    static Current() {
        if (this._instance == null) {
            this._instance = new SessionManager;
            this._instance.CheckToken();
        }
        return this._instance;
    }
    StartSession(data) {
        userData = data.userData;
        this.MapPermissions(userData);
        this._tokenData = Object.assign(new _security_models__WEBPACK_IMPORTED_MODULE_1__["TokenData"], {
            Token: data.token,
            Expiry: data.tokenExpiry
        });
        this.TokenStorage.SaveToken(this._tokenData);
        if (data.refreshToken)
            this.TokenStorage.SaveRefreshToken(data.refreshToken);
        this.OnLogin.emit(userData);
        this.LogStatus.emit(true);
        this.IsLoggedIn = true;
    }
    CheckToken() {
        this._tokenData = this.TokenStorage.LoadToken();
        if (this._tokenData == null) {
            this.IsLoggedIn = false;
        }
        else {
            let token = this._tokenData;
            this.IsLoggedIn = new Date() < new Date(token.Expiry);
        }
        this.LogStatus.emit(this.IsLoggedIn);
    }
    EndSession() {
        this.TokenStorage.Clear();
        userData = null;
        this.LogStatus.emit(false);
        this.IsLoggedIn = false;
        this._tokenData = null;
    }
    GetDeviceId() {
        var id = js_cookie__WEBPACK_IMPORTED_MODULE_4__["get"]("DID");
        if (!id) {
            let d = new Date();
            let no = Math.random().toString();
            id = new Date().getTime().toString() + '_' + Object(_utilities_utils__WEBPACK_IMPORTED_MODULE_5__["String_GetAfterLast"])(no, ".");
            d.setFullYear(d.getFullYear() + 3);
            js_cookie__WEBPACK_IMPORTED_MODULE_4__["set"]("DID", id, { path: "/", expires: d });
        }
        return id;
    }
    GetPermission(id) {
        if (this.IsLoggedIn && this.User.permissions[id]) {
            let u = this.User;
            return Object.assign(new _security_models__WEBPACK_IMPORTED_MODULE_1__["Permission"], u.permissions[id]);
        }
        else {
            let s = new _security_models__WEBPACK_IMPORTED_MODULE_1__["Permission"];
            s.view = false;
            return s;
        }
    }
    get Locale() {
        return js_cookie__WEBPACK_IMPORTED_MODULE_4__["get"]("Locale");
    }
    GetToken() {
        if (!this.IsLoggedIn) {
            return null;
        }
        if (this._tokenData && this._tokenData.IsExpired())
            return null;
        return this._tokenData;
    }
}
SessionManager.ɵfac = function SessionManager_Factory(t) { return new (t || SessionManager)(); };
SessionManager.ɵprov = _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵdefineInjectable"]({ token: SessionManager, factory: SessionManager.ɵfac });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵsetClassMetadata"](SessionManager, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_2__["Injectable"]
    }], null, null); })();


/***/ }),

/***/ "N0SU":
/*!***************************************************!*\
  !*** ./src/core/codeshell/services/listSource.ts ***!
  \***************************************************/
/*! exports provided: ListSource */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ListSource", function() { return ListSource; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "mrSG");
/* harmony import */ var _helpers__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../helpers */ "GYDu");
/* harmony import */ var _listSelectionService__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./listSelectionService */ "0Wec");



class ListSource {
    constructor(showing, predicate) {
        this._list = [];
        this._totalCount = 0;
        this._useSelect = false;
        // private _currentPage: number = 0;
        this.InitialSelection = null;
        this.Opts = { Showing: 0, Skip: 0 };
        this.pageIndex = 0;
        this.LoadedOnce = false;
        this.UseJoin = true;
        this.Selection = null;
        this.Loader = predicate;
        this.Opts.Showing = showing;
    }
    get List() { return this._list; }
    ;
    get TotalCount() { return this._totalCount; }
    ;
    get UseSelection() { return this.Selection != null; }
    set UseSelection(val) {
        this.Selection = new _listSelectionService__WEBPACK_IMPORTED_MODULE_2__["ListSelectionService"]();
    }
    static get Empty() {
        return new ListSource(10, e => Promise.resolve(new _helpers__WEBPACK_IMPORTED_MODULE_1__["LoadResult"]));
    }
    Retag() {
        if (this.TagArguments)
            this._list = _helpers__WEBPACK_IMPORTED_MODULE_1__["Tagged"].JoinLists(this.TagArguments);
    }
    SelectById(id) {
        if (this.TagArguments) {
            let s = this.List.find(d => d.id == id);
            if (s) {
                s.Tag.selected = true;
                s.Tag.AddTo(this.TagArguments.Source);
            }
        }
    }
    AfterLoad(e) {
        if (this.OnDataLoaded)
            this.OnDataLoaded(e.list);
        if (this.UseJoin) {
            if (this.TagArguments) {
                this.TagArguments.Data = e.list;
                this._list = _helpers__WEBPACK_IMPORTED_MODULE_1__["Tagged"].JoinLists(this.TagArguments);
            }
            else {
                this._list = [];
                for (var i in e.list)
                    this._list[i] = Object.assign(new _helpers__WEBPACK_IMPORTED_MODULE_1__["Tagged"], e.list[i]);
            }
        }
        else {
            this._list = e.list;
        }
        this._totalCount = e.totalCount;
        this.LoadedOnce = true;
        if (this.InitialSelection) {
            for (var id of this.InitialSelection)
                this.SelectById(id);
        }
    }
    LoadDataAsync() {
        return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, function* () {
            var data = yield this.Loader(this.Opts);
            this.AfterLoad(data);
            return data;
        });
    }
    LoadData() {
        this.Loader(this.Opts).then(e => {
            this.AfterLoad(e);
        });
    }
    PageChanged(id) {
        this.Opts.Skip = this.Opts.Showing * id;
        this.LoadData();
    }
    Search(term) {
        this.Opts.SearchTerm = term;
        this.Opts.Skip = 0;
        this.pageIndex = 0;
        this.LoadData();
    }
    SetFilters(filters) {
        this.Opts.Filters = JSON.stringify(filters);
    }
}


/***/ }),

/***/ "NXgI":
/*!**********************************************!*\
  !*** ./src/core/base/example-base.module.ts ***!
  \**********************************************/
/*! exports provided: ExampleBaseModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ExampleBaseModule", function() { return ExampleBaseModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var codeshell_main__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! codeshell/main */ "7OZ3");
/* harmony import */ var codeshell_security__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! codeshell/security */ "U6Sh");
/* harmony import */ var _http_account_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./http/account.service */ "2enV");
/* harmony import */ var _main_login_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./main/login.component */ "8sze");
/* harmony import */ var _main_top_bar_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./main/top-bar.component */ "WNdI");
/* harmony import */ var _main_navigation_side_bar_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./main/navigation-side-bar.component */ "a3Ap");








class ExampleBaseModule {
    static forRoot() {
        return {
            ngModule: ExampleBaseModule,
            providers: [
                { provide: codeshell_security__WEBPACK_IMPORTED_MODULE_2__["AccountServiceBase"], useClass: _http_account_service__WEBPACK_IMPORTED_MODULE_3__["AccountService"] },
                { provide: codeshell_security__WEBPACK_IMPORTED_MODULE_2__["TokenStorage"], useClass: codeshell_security__WEBPACK_IMPORTED_MODULE_2__["SessionTokenData"] }
            ]
        };
    }
}
ExampleBaseModule.ɵmod = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineNgModule"]({ type: ExampleBaseModule });
ExampleBaseModule.ɵinj = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineInjector"]({ factory: function ExampleBaseModule_Factory(t) { return new (t || ExampleBaseModule)(); }, imports: [[
            codeshell_main__WEBPACK_IMPORTED_MODULE_1__["CodeShellModule"],
        ], codeshell_main__WEBPACK_IMPORTED_MODULE_1__["CodeShellModule"]] });
(function () { (typeof ngJitMode === "undefined" || ngJitMode) && _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵsetNgModuleScope"](ExampleBaseModule, { declarations: [_main_login_component__WEBPACK_IMPORTED_MODULE_4__["Login"], _main_top_bar_component__WEBPACK_IMPORTED_MODULE_5__["TopBar"], _main_navigation_side_bar_component__WEBPACK_IMPORTED_MODULE_6__["NavigationSideBar"]], imports: [codeshell_main__WEBPACK_IMPORTED_MODULE_1__["CodeShellModule"]], exports: [codeshell_main__WEBPACK_IMPORTED_MODULE_1__["CodeShellModule"],
        _main_login_component__WEBPACK_IMPORTED_MODULE_4__["Login"], _main_top_bar_component__WEBPACK_IMPORTED_MODULE_5__["TopBar"], _main_navigation_side_bar_component__WEBPACK_IMPORTED_MODULE_6__["NavigationSideBar"]] }); })();
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](ExampleBaseModule, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"],
        args: [{
                declarations: [_main_login_component__WEBPACK_IMPORTED_MODULE_4__["Login"], _main_top_bar_component__WEBPACK_IMPORTED_MODULE_5__["TopBar"], _main_navigation_side_bar_component__WEBPACK_IMPORTED_MODULE_6__["NavigationSideBar"]],
                imports: [
                    codeshell_main__WEBPACK_IMPORTED_MODULE_1__["CodeShellModule"],
                ],
                exports: [
                    codeshell_main__WEBPACK_IMPORTED_MODULE_1__["CodeShellModule"],
                    _main_login_component__WEBPACK_IMPORTED_MODULE_4__["Login"], _main_top_bar_component__WEBPACK_IMPORTED_MODULE_5__["TopBar"], _main_navigation_side_bar_component__WEBPACK_IMPORTED_MODULE_6__["NavigationSideBar"]
                ]
            }]
    }], null, null); })();


/***/ }),

/***/ "NnUC":
/*!******************************************************!*\
  !*** ./src/core/codeshell/services/tableServices.ts ***!
  \******************************************************/
/*! exports provided: TableService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "TableService", function() { return TableService; });
/* harmony import */ var codeshell_helpers_listItem__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! codeshell/helpers/listItem */ "JLZs");
/* harmony import */ var codeshell_main__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! codeshell/main */ "7OZ3");


class TableService {
    constructor(listRef) {
        this.listRef = listRef;
        this.Adding = false;
    }
    get List() {
        return this.listRef();
    }
    _removeAddRow() {
        var mod = this.List.find(d => d.addingRow == true);
        if (mod)
            Object(codeshell_main__WEBPACK_IMPORTED_MODULE_1__["List_RemoveItem"])(this.List, mod);
    }
    StartAdd() {
        this.Adding = true;
        this.List.push(codeshell_helpers_listItem__WEBPACK_IMPORTED_MODULE_0__["ListItem"].Detached({ addingRow: true }));
    }
    CancelAdd() {
        this._removeAddRow();
        this.Adding = false;
    }
    SubmitAdd() {
        var mod = this.List.find(d => d.addingRow == true);
        if (mod) {
            mod.SetAdded();
            mod.addingRow = false;
            this.StartAdd();
        }
    }
}


/***/ }),

/***/ "OI30":
/*!**************************************************!*\
  !*** ./src/core/codeshell/helpers/interfaces.ts ***!
  \**************************************************/
/*! no exports provided */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);



/***/ }),

/***/ "PCNd":
/*!*****************************************!*\
  !*** ./src/app/shared/shared.module.ts ***!
  \*****************************************/
/*! exports provided: SharedModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SharedModule", function() { return SharedModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var _base_example_base_module__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @base/example-base.module */ "NXgI");
/* harmony import */ var _ngx_translate_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @ngx-translate/core */ "sYmb");
/* harmony import */ var codeshell_main__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! codeshell/main */ "7OZ3");





class SharedModule {
    constructor(trans, conf) {
        trans.setDefaultLang(conf.Locale);
        trans.use(conf.Locale);
    }
}
SharedModule.ɵmod = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineNgModule"]({ type: SharedModule });
SharedModule.ɵinj = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineInjector"]({ factory: function SharedModule_Factory(t) { return new (t || SharedModule)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵinject"](_ngx_translate_core__WEBPACK_IMPORTED_MODULE_2__["TranslateService"]), _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵinject"](codeshell_main__WEBPACK_IMPORTED_MODULE_3__["ServerConfigBase"])); }, imports: [[
            _base_example_base_module__WEBPACK_IMPORTED_MODULE_1__["ExampleBaseModule"]
        ], _base_example_base_module__WEBPACK_IMPORTED_MODULE_1__["ExampleBaseModule"]] });
(function () { (typeof ngJitMode === "undefined" || ngJitMode) && _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵsetNgModuleScope"](SharedModule, { imports: [_base_example_base_module__WEBPACK_IMPORTED_MODULE_1__["ExampleBaseModule"]], exports: [_base_example_base_module__WEBPACK_IMPORTED_MODULE_1__["ExampleBaseModule"]] }); })();
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](SharedModule, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"],
        args: [{
                declarations: [],
                exports: [
                    _base_example_base_module__WEBPACK_IMPORTED_MODULE_1__["ExampleBaseModule"]
                ],
                imports: [
                    _base_example_base_module__WEBPACK_IMPORTED_MODULE_1__["ExampleBaseModule"]
                ],
                entryComponents: []
            }]
    }], function () { return [{ type: _ngx_translate_core__WEBPACK_IMPORTED_MODULE_2__["TranslateService"] }, { type: codeshell_main__WEBPACK_IMPORTED_MODULE_3__["ServerConfigBase"] }]; }, null); })();


/***/ }),

/***/ "PwuP":
/*!***************************************************!*\
  !*** ./src/core/codeshell/validators/isUnique.ts ***!
  \***************************************************/
/*! exports provided: IsUnique */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "IsUnique", function() { return IsUnique; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "3Pt+");



class IsUnique {
    constructor(model) {
        this.model = model;
    }
    Change(arg) {
        if (this.service && this.column) {
            if (!this.model.value || this.model.value == "") {
                this.model.control.setErrors({ unique: null });
                this.model.control.updateValueAndValidity();
            }
            else {
                var sil = false;
                if (this.service) {
                    sil = this.service.Silent;
                    this.service.Silent = true;
                }
                this.model.control.setErrors({ unique: true });
                this.service.IsUnique(this.column, this.id, this.model.value).then(e => {
                    if (this.service)
                        this.service.Silent = sil;
                    if (this.model) {
                        if (e) {
                            this.model.control.setErrors({ unique: null });
                            this.model.control.updateValueAndValidity();
                        }
                        else
                            this.model.control.setErrors({ unique: true });
                    }
                });
            }
        }
    }
}
IsUnique.ɵfac = function IsUnique_Factory(t) { return new (t || IsUnique)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_forms__WEBPACK_IMPORTED_MODULE_1__["NgModel"])); };
IsUnique.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({ type: IsUnique, selectors: [["", "is-unique", "", "ngModel", ""]], hostBindings: function IsUnique_HostBindings(rf, ctx) { if (rf & 1) {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("blur", function IsUnique_blur_HostBindingHandler($event) { return ctx.Change($event); });
    } }, inputs: { service: ["data-service", "service"], column: ["column-id", "column"], id: ["item-id", "id"] }, exportAs: ["is-unique"] });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](IsUnique, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
        args: [{ selector: "[is-unique][ngModel]", exportAs: "is-unique" }]
    }], function () { return [{ type: _angular_forms__WEBPACK_IMPORTED_MODULE_1__["NgModel"] }]; }, { service: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["data-service"]
        }], column: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["column-id"]
        }], id: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["item-id"]
        }], Change: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"],
            args: ["blur", ['$event']]
        }] }); })();


/***/ }),

/***/ "Q6pc":
/*!****************************************************************!*\
  !*** ./src/core/codeshell/base-components/appComponentBase.ts ***!
  \****************************************************************/
/*! exports provided: IAppComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "IAppComponent", function() { return IAppComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "tyNb");
/* harmony import */ var _helpers__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../helpers */ "GYDu");
/* harmony import */ var _shell__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../shell */ "UoIT");
/* harmony import */ var _utilities_registry__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../utilities/registry */ "ppms");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/common/http */ "tk/3");
/* harmony import */ var _angular_platform_browser__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/platform-browser */ "jhN1");








const _c0 = ["topBar"];
class IAppComponent {
    constructor(inj, title) {
        this.title = title;
        this._loaderTimeout = null;
        this.ShowLoader = false;
        this.IsLoggedIn = false;
        this.deleteDialogShow = false;
        this.promptShow = false;
        this.promptMessage = "";
        this.ShowNav = true;
        this.TokenIsExpired = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.SideBarStatus = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.OnDeleteConfirm = e => { };
        this.OnDeleteCancel = e => { };
        //protected Toaster: ToastrService;
        this.RedirectToLogin = true;
        _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Injector = inj;
        _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Start(this);
        _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Session.LogStatus.subscribe((d) => {
            this.IsLoggedIn = d;
            this.OnLogStatusChanged(d);
        });
        _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Session.GetUserAsync().then(d => {
            this.IsLoggedIn = true;
            this.OnStartupSessionFound(d);
        }).catch(e => {
            this.OnStartupNoSession(e);
        });
        this.SideBarStatus.subscribe((state) => {
            setTimeout(() => {
                if (this.topBar)
                    this.topBar.setSideBarState(state);
            }, 500);
        });
        _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Session.OnUserDataFailed.subscribe((error) => {
            this.ShowPromptTranslate(error.message);
        });
        //this.Toaster = inj.get(ToastrService);
    }
    get FactoryResolver() { return _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Injector.get(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ComponentFactoryResolver"]); }
    get Config() { return null; }
    get Router() { return _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Injector.get(_angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"]); }
    ngOnInit() {
        // if (this.RestrictLang && this.Config.Locale != this.RestrictLang) {
        //     this.ChangeLangAsync(this.RestrictLang).then(e => location.reload());
        // }
    }
    GetMainUrl() {
        return '/';
    }
    OnStartupSessionFound(dto) {
    }
    OnStartupNoSession(response) {
        if (this.RedirectToLogin)
            this.Router.navigateByUrl("/Login");
    }
    OnLogStatusChanged(res) { }
    ChangeLangAsync(code) {
        var cl = _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Injector.get(_angular_common_http__WEBPACK_IMPORTED_MODULE_5__["HttpClient"]);
        return cl.get("/Home/SetLocale/?lang=" + code).toPromise();
    }
    ShowPrompt(data) {
        this.promptMessage = data;
        this.promptShow = true;
    }
    ShowPromptTranslate(data) {
        this.promptMessage = _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Message(data);
        this.promptShow = true;
    }
    ShowLoading() {
        this.ShowLoader = true;
        if (this._loaderTimeout)
            clearTimeout(this._loaderTimeout);
    }
    HideLoading() {
        if (this._loaderTimeout)
            clearTimeout(this._loaderTimeout);
        this._loaderTimeout = setTimeout(() => {
            this.ShowLoader = false;
        }, 800);
    }
    GetDialogAs(path) {
        if (path == null)
            return null;
        let e = _utilities_registry__WEBPACK_IMPORTED_MODULE_4__["Registry"].Get(path);
        if (e && this.ModalLoader) {
            var ref = this.ModalLoader.CreateComponent(e);
            this.ModalLoader.UseComponent(ref);
            return ref;
        }
        return null;
    }
    OnPromptOk() {
        this.promptShow = false;
    }
    LogOut() {
        _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Session.EndSession();
        _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Injector.get(_angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"]).navigate(["/"]);
    }
    ShowDeleteConfirmLocal(onConfirm) {
        this.deleteDialogShow = true;
        this.OnDeleteConfirm = e => {
            this.deleteDialogShow = false;
            onConfirm();
        };
    }
    ShowDeleteConfirm() {
        this.deleteDialogShow = true;
        this.confirmTitle = _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Word("Delete");
        this.confirmMessage = _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Message("delete_confirm_message");
        return new Promise((res, rej) => {
            this.OnDeleteConfirm = e => {
                this.deleteDialogShow = false;
                res(true);
            };
            this.OnDeleteCancel = e => {
                this.deleteDialogShow = false;
                res(false);
            };
        });
    }
    Confirm(message, translate = true, title) {
        this.deleteDialogShow = true;
        return new Promise((res, rej) => {
            if (title)
                this.confirmTitle = translate ? _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Word(title) : title;
            else
                this.confirmTitle = "";
            this.confirmMessage = translate ? _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Message(message) : message;
            this.OnDeleteConfirm = e => {
                this.deleteDialogShow = false;
                res();
            };
            this.OnDeleteCancel = e => {
                this.deleteDialogShow = false;
                rej();
            };
        });
    }
    Notify(message, type, title) {
        type = type == undefined ? _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"].Success : type;
        let typ = _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"][type].toString();
        title = title ? title : typ;
        this.ShowMessage(type, message, _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Word(title));
    }
    NotifyTranslate(messageId, type, title) {
        type = type == undefined ? _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"].Success : type;
        let typ = _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"][type].toString();
        title = title ? title : typ;
        this.ShowMessage(type, _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Message(messageId), _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Word(title));
    }
    SetTitle(pageIdentifier, translate = true) {
        if (translate)
            this.title.setTitle(_shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Page(pageIdentifier));
        else
            this.title.setTitle(pageIdentifier);
    }
    ClearModalLoader() {
        if (this.ModalLoader)
            this.ModalLoader.Container.clear();
    }
    ShowMessage(type, e, title) {
        // switch (type) {
        //     case NoteType.Error:
        //         this.Toaster.error(e, title, {
        //             positionClass: "toast-top-left"
        //         });
        //         break;
        //     case NoteType.Success:
        //         this.Toaster.success(e, title, {
        //             positionClass: "toast-top-left"
        //         });
        //         break;
        //     case NoteType.Warning:
        //         this.Toaster.warning(e, title, {
        //             positionClass: "toast-top-left"
        //         });
        //         break;
        // }
    }
}
IAppComponent.ɵfac = function IAppComponent_Factory(t) { return new (t || IAppComponent)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injector"]), _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_platform_browser__WEBPACK_IMPORTED_MODULE_6__["Title"])); };
IAppComponent.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineComponent"]({ type: IAppComponent, selectors: [["ng-component"]], viewQuery: function IAppComponent_Query(rf, ctx) { if (rf & 1) {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵviewQuery"](_c0, true);
    } if (rf & 2) {
        let _t;
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵqueryRefresh"](_t = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵloadQuery"]()) && (ctx.topBar = _t.first);
    } }, decls: 0, vars: 0, template: function IAppComponent_Template(rf, ctx) { }, encapsulation: 2 });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](IAppComponent, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"],
        args: [{ template: '' }]
    }], function () { return [{ type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Injector"] }, { type: _angular_platform_browser__WEBPACK_IMPORTED_MODULE_6__["Title"] }]; }, { topBar: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ViewChild"],
            args: ["topBar"]
        }] }); })();


/***/ }),

/***/ "RnJ/":
/*!**********************************************!*\
  !*** ./src/core/codeshell/services/index.ts ***!
  \**********************************************/
/*! exports provided: ListSelectionService, ListSource, SectionedFormService, Stored, TableService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _listSelectionService__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./listSelectionService */ "0Wec");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "ListSelectionService", function() { return _listSelectionService__WEBPACK_IMPORTED_MODULE_0__["ListSelectionService"]; });

/* harmony import */ var _listSource__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./listSource */ "N0SU");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "ListSource", function() { return _listSource__WEBPACK_IMPORTED_MODULE_1__["ListSource"]; });

/* harmony import */ var _sectionedFormService__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./sectionedFormService */ "niJb");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "SectionedFormService", function() { return _sectionedFormService__WEBPACK_IMPORTED_MODULE_2__["SectionedFormService"]; });

/* harmony import */ var _stored__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./stored */ "mHXC");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "Stored", function() { return _stored__WEBPACK_IMPORTED_MODULE_3__["Stored"]; });

/* harmony import */ var _tableServices__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./tableServices */ "NnUC");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "TableService", function() { return _tableServices__WEBPACK_IMPORTED_MODULE_4__["TableService"]; });








/***/ }),

/***/ "RnhZ":
/*!**************************************************!*\
  !*** ./node_modules/moment/locale sync ^\.\/.*$ ***!
  \**************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var map = {
	"./af": "K/tc",
	"./af.js": "K/tc",
	"./ar": "jnO4",
	"./ar-dz": "o1bE",
	"./ar-dz.js": "o1bE",
	"./ar-kw": "Qj4J",
	"./ar-kw.js": "Qj4J",
	"./ar-ly": "HP3h",
	"./ar-ly.js": "HP3h",
	"./ar-ma": "CoRJ",
	"./ar-ma.js": "CoRJ",
	"./ar-sa": "gjCT",
	"./ar-sa.js": "gjCT",
	"./ar-tn": "bYM6",
	"./ar-tn.js": "bYM6",
	"./ar.js": "jnO4",
	"./az": "SFxW",
	"./az.js": "SFxW",
	"./be": "H8ED",
	"./be.js": "H8ED",
	"./bg": "hKrs",
	"./bg.js": "hKrs",
	"./bm": "p/rL",
	"./bm.js": "p/rL",
	"./bn": "kEOa",
	"./bn-bd": "loYQ",
	"./bn-bd.js": "loYQ",
	"./bn.js": "kEOa",
	"./bo": "0mo+",
	"./bo.js": "0mo+",
	"./br": "aIdf",
	"./br.js": "aIdf",
	"./bs": "JVSJ",
	"./bs.js": "JVSJ",
	"./ca": "1xZ4",
	"./ca.js": "1xZ4",
	"./cs": "PA2r",
	"./cs.js": "PA2r",
	"./cv": "A+xa",
	"./cv.js": "A+xa",
	"./cy": "l5ep",
	"./cy.js": "l5ep",
	"./da": "DxQv",
	"./da.js": "DxQv",
	"./de": "tGlX",
	"./de-at": "s+uk",
	"./de-at.js": "s+uk",
	"./de-ch": "u3GI",
	"./de-ch.js": "u3GI",
	"./de.js": "tGlX",
	"./dv": "WYrj",
	"./dv.js": "WYrj",
	"./el": "jUeY",
	"./el.js": "jUeY",
	"./en-au": "Dmvi",
	"./en-au.js": "Dmvi",
	"./en-ca": "OIYi",
	"./en-ca.js": "OIYi",
	"./en-gb": "Oaa7",
	"./en-gb.js": "Oaa7",
	"./en-ie": "4dOw",
	"./en-ie.js": "4dOw",
	"./en-il": "czMo",
	"./en-il.js": "czMo",
	"./en-in": "7C5Q",
	"./en-in.js": "7C5Q",
	"./en-nz": "b1Dy",
	"./en-nz.js": "b1Dy",
	"./en-sg": "t+mt",
	"./en-sg.js": "t+mt",
	"./eo": "Zduo",
	"./eo.js": "Zduo",
	"./es": "iYuL",
	"./es-do": "CjzT",
	"./es-do.js": "CjzT",
	"./es-mx": "tbfe",
	"./es-mx.js": "tbfe",
	"./es-us": "Vclq",
	"./es-us.js": "Vclq",
	"./es.js": "iYuL",
	"./et": "7BjC",
	"./et.js": "7BjC",
	"./eu": "D/JM",
	"./eu.js": "D/JM",
	"./fa": "jfSC",
	"./fa.js": "jfSC",
	"./fi": "gekB",
	"./fi.js": "gekB",
	"./fil": "1ppg",
	"./fil.js": "1ppg",
	"./fo": "ByF4",
	"./fo.js": "ByF4",
	"./fr": "nyYc",
	"./fr-ca": "2fjn",
	"./fr-ca.js": "2fjn",
	"./fr-ch": "Dkky",
	"./fr-ch.js": "Dkky",
	"./fr.js": "nyYc",
	"./fy": "cRix",
	"./fy.js": "cRix",
	"./ga": "USCx",
	"./ga.js": "USCx",
	"./gd": "9rRi",
	"./gd.js": "9rRi",
	"./gl": "iEDd",
	"./gl.js": "iEDd",
	"./gom-deva": "qvJo",
	"./gom-deva.js": "qvJo",
	"./gom-latn": "DKr+",
	"./gom-latn.js": "DKr+",
	"./gu": "4MV3",
	"./gu.js": "4MV3",
	"./he": "x6pH",
	"./he.js": "x6pH",
	"./hi": "3E1r",
	"./hi.js": "3E1r",
	"./hr": "S6ln",
	"./hr.js": "S6ln",
	"./hu": "WxRl",
	"./hu.js": "WxRl",
	"./hy-am": "1rYy",
	"./hy-am.js": "1rYy",
	"./id": "UDhR",
	"./id.js": "UDhR",
	"./is": "BVg3",
	"./is.js": "BVg3",
	"./it": "bpih",
	"./it-ch": "bxKX",
	"./it-ch.js": "bxKX",
	"./it.js": "bpih",
	"./ja": "B55N",
	"./ja.js": "B55N",
	"./jv": "tUCv",
	"./jv.js": "tUCv",
	"./ka": "IBtZ",
	"./ka.js": "IBtZ",
	"./kk": "bXm7",
	"./kk.js": "bXm7",
	"./km": "6B0Y",
	"./km.js": "6B0Y",
	"./kn": "PpIw",
	"./kn.js": "PpIw",
	"./ko": "Ivi+",
	"./ko.js": "Ivi+",
	"./ku": "JCF/",
	"./ku.js": "JCF/",
	"./ky": "lgnt",
	"./ky.js": "lgnt",
	"./lb": "RAwQ",
	"./lb.js": "RAwQ",
	"./lo": "sp3z",
	"./lo.js": "sp3z",
	"./lt": "JvlW",
	"./lt.js": "JvlW",
	"./lv": "uXwI",
	"./lv.js": "uXwI",
	"./me": "KTz0",
	"./me.js": "KTz0",
	"./mi": "aIsn",
	"./mi.js": "aIsn",
	"./mk": "aQkU",
	"./mk.js": "aQkU",
	"./ml": "AvvY",
	"./ml.js": "AvvY",
	"./mn": "lYtQ",
	"./mn.js": "lYtQ",
	"./mr": "Ob0Z",
	"./mr.js": "Ob0Z",
	"./ms": "6+QB",
	"./ms-my": "ZAMP",
	"./ms-my.js": "ZAMP",
	"./ms.js": "6+QB",
	"./mt": "G0Uy",
	"./mt.js": "G0Uy",
	"./my": "honF",
	"./my.js": "honF",
	"./nb": "bOMt",
	"./nb.js": "bOMt",
	"./ne": "OjkT",
	"./ne.js": "OjkT",
	"./nl": "+s0g",
	"./nl-be": "2ykv",
	"./nl-be.js": "2ykv",
	"./nl.js": "+s0g",
	"./nn": "uEye",
	"./nn.js": "uEye",
	"./oc-lnc": "Fnuy",
	"./oc-lnc.js": "Fnuy",
	"./pa-in": "8/+R",
	"./pa-in.js": "8/+R",
	"./pl": "jVdC",
	"./pl.js": "jVdC",
	"./pt": "8mBD",
	"./pt-br": "0tRk",
	"./pt-br.js": "0tRk",
	"./pt.js": "8mBD",
	"./ro": "lyxo",
	"./ro.js": "lyxo",
	"./ru": "lXzo",
	"./ru.js": "lXzo",
	"./sd": "Z4QM",
	"./sd.js": "Z4QM",
	"./se": "//9w",
	"./se.js": "//9w",
	"./si": "7aV9",
	"./si.js": "7aV9",
	"./sk": "e+ae",
	"./sk.js": "e+ae",
	"./sl": "gVVK",
	"./sl.js": "gVVK",
	"./sq": "yPMs",
	"./sq.js": "yPMs",
	"./sr": "zx6S",
	"./sr-cyrl": "E+lV",
	"./sr-cyrl.js": "E+lV",
	"./sr.js": "zx6S",
	"./ss": "Ur1D",
	"./ss.js": "Ur1D",
	"./sv": "X709",
	"./sv.js": "X709",
	"./sw": "dNwA",
	"./sw.js": "dNwA",
	"./ta": "PeUW",
	"./ta.js": "PeUW",
	"./te": "XLvN",
	"./te.js": "XLvN",
	"./tet": "V2x9",
	"./tet.js": "V2x9",
	"./tg": "Oxv6",
	"./tg.js": "Oxv6",
	"./th": "EOgW",
	"./th.js": "EOgW",
	"./tk": "Wv91",
	"./tk.js": "Wv91",
	"./tl-ph": "Dzi0",
	"./tl-ph.js": "Dzi0",
	"./tlh": "z3Vd",
	"./tlh.js": "z3Vd",
	"./tr": "DoHr",
	"./tr.js": "DoHr",
	"./tzl": "z1FC",
	"./tzl.js": "z1FC",
	"./tzm": "wQk9",
	"./tzm-latn": "tT3J",
	"./tzm-latn.js": "tT3J",
	"./tzm.js": "wQk9",
	"./ug-cn": "YRex",
	"./ug-cn.js": "YRex",
	"./uk": "raLr",
	"./uk.js": "raLr",
	"./ur": "UpQW",
	"./ur.js": "UpQW",
	"./uz": "Loxo",
	"./uz-latn": "AQ68",
	"./uz-latn.js": "AQ68",
	"./uz.js": "Loxo",
	"./vi": "KSF8",
	"./vi.js": "KSF8",
	"./x-pseudo": "/X5v",
	"./x-pseudo.js": "/X5v",
	"./yo": "fzPg",
	"./yo.js": "fzPg",
	"./zh-cn": "XDpg",
	"./zh-cn.js": "XDpg",
	"./zh-hk": "SatO",
	"./zh-hk.js": "SatO",
	"./zh-mo": "OmwH",
	"./zh-mo.js": "OmwH",
	"./zh-tw": "kOpN",
	"./zh-tw.js": "kOpN"
};


function webpackContext(req) {
	var id = webpackContextResolve(req);
	return __webpack_require__(id);
}
function webpackContextResolve(req) {
	if(!__webpack_require__.o(map, req)) {
		var e = new Error("Cannot find module '" + req + "'");
		e.code = 'MODULE_NOT_FOUND';
		throw e;
	}
	return map[req];
}
webpackContext.keys = function webpackContextKeys() {
	return Object.keys(map);
};
webpackContext.resolve = webpackContextResolve;
module.exports = webpackContext;
webpackContext.id = "RnhZ";

/***/ }),

/***/ "Rux6":
/*!*************************************************************!*\
  !*** ./src/core/codeshell/components/paginate.component.ts ***!
  \*************************************************************/
/*! exports provided: Paginate */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Paginate", function() { return Paginate; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common */ "ofXK");



function Paginate_li_10_Template(rf, ctx) { if (rf & 1) {
    const _r3 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵgetCurrentView"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "li", 10);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](1, "a", 11);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function Paginate_li_10_Template_a_click_1_listener() { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r3); const p_r1 = ctx.$implicit; const ctx_r2 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"](); return ctx_r2.SelectPage(p_r1); });
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
} if (rf & 2) {
    const p_r1 = ctx.$implicit;
    const ctx_r0 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngClass", ctx_r0.Current == p_r1.id ? "active" : null);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngClass", ctx_r0.Current == p_r1.id ? "disabled" : null);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate"](p_r1.name);
} }
class Paginate {
    constructor() {
        this.totalPages = 0;
        this._current = 0;
        this._max = 0;
        this.Pages = [];
        this.Showing = 10;
        this.Total = 0;
        this.PageChange = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.currentPageChange = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
    }
    get Current() { return this._current; }
    set Current(val) {
        this._current = val;
        this.currentPageChange.emit(this._current);
    }
    ngOnInit() {
        if (this.Showing != 0) {
            this.SetPages();
        }
    }
    ngOnChanges() {
        this.SetPages();
    }
    SelectPage(p) {
        if (this.Current == p.id)
            return;
        this.Current = p.id;
        if (this.PageChange != undefined) {
            this.PageChange.emit(this.Current);
            //this.CurrentOut.emit(this.Current);
        }
    }
    Prev() {
        if (this.Current > 0) {
            this.Current -= 1;
            this.PageChange.emit(this.Current);
            //this.CurrentOut.emit(this.Current);
        }
    }
    Next() {
        if (this.Current < (this.totalPages - 1)) {
            this._current += 1;
            this.PageChange.emit(this.Current);
            //this.CurrentOut.emit(this.Current);
        }
    }
    SetPages() {
        ////debugger;
        this.Pages = [];
        if (this.Showing == 0 || this.Total == 0)
            return;
        let cnt = this.Total / this.Showing;
        let num = Math.floor(cnt);
        if (num < cnt)
            num += 1;
        var reset = this.Current > (num - 1);
        this.totalPages = num;
        if (reset)
            this.SelectPage({ id: 0, name: "1" });
        this.WritePages();
    }
    WritePages() {
        if (this.MaxPages) {
            ////debugger;
            let s = this.MaxPages / 2;
            let sInt = Math.floor(s);
            let start = 0, end = this.MaxPages;
            let noOfPages = this.MaxPages > this.totalPages ? this.totalPages : this.MaxPages;
            start = this.Current - sInt;
            start = start < 0 ? 0 : start;
            end = start + noOfPages;
            if ((this.totalPages - 1) < end) {
                end = this.totalPages - 1;
                start = end - noOfPages;
                start = start < 0 ? 0 : start;
            }
            for (let i = start; i <= end; i++)
                this.Pages.push({ id: i, name: (i + 1).toString() });
        }
        else {
            for (let i = 0; i < this.totalPages; i++)
                this.Pages.push({ id: i, name: (i + 1).toString() });
        }
    }
}
Paginate.ɵfac = function Paginate_Factory(t) { return new (t || Paginate)(); };
Paginate.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineComponent"]({ type: Paginate, selectors: [["paginate"]], inputs: { Showing: ["showing", "Showing"], Total: ["total-count", "Total"], Current: ["currentPage", "Current"], MaxPages: ["max-pages", "MaxPages"] }, outputs: { PageChange: "pageChange", currentPageChange: "currentPageChange" }, features: [_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵNgOnChangesFeature"]], decls: 17, vars: 2, consts: [[1, "row", 3, "hidden"], [1, "col-sm-12", 2, "text-align", "center"], ["aria-label", "Page navigation example"], [1, "pagination"], [1, "page-item"], ["aria-label", "Previous", 1, "page-link", 3, "click"], ["aria-hidden", "true"], [1, "sr-only"], ["class", "page-item", 3, "ngClass", 4, "ngFor", "ngForOf"], ["aria-label", "Next", 1, "page-link", 3, "click"], [1, "page-item", 3, "ngClass"], [1, "page-link", 3, "ngClass", "click"]], template: function Paginate_Template(rf, ctx) { if (rf & 1) {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "div", 0);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](1, "div", 1);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](2, "nav", 2);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](3, "ul", 3);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](4, "li", 4);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](5, "a", 5);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function Paginate_Template_a_click_5_listener() { return ctx.Prev(); });
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](6, "span", 6);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](7, "\u00AB");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](8, "span", 7);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](9, "Previous");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](10, Paginate_li_10_Template, 3, 3, "li", 8);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](11, "li", 4);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](12, "a", 9);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function Paginate_Template_a_click_12_listener() { return ctx.Next(); });
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](13, "span", 6);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](14, "\u00BB");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](15, "span", 7);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](16, "Next");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
    } if (rf & 2) {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("hidden", ctx.Pages.length < 2);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](10);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngForOf", ctx.Pages);
    } }, directives: [_angular_common__WEBPACK_IMPORTED_MODULE_1__["NgForOf"], _angular_common__WEBPACK_IMPORTED_MODULE_1__["NgClass"]], encapsulation: 2 });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](Paginate, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"],
        args: [{
                selector: "paginate",
                templateUrl: "./paginate.component.html"
            }]
    }], null, { Showing: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["showing"]
        }], Total: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["total-count"]
        }], Current: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["currentPage"]
        }], MaxPages: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["max-pages"]
        }], PageChange: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"],
            args: ["pageChange"]
        }], currentPageChange: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"],
            args: ["currentPageChange"]
        }] }); })();
class Page {
    constructor(data) {
        this.id = 0;
        this.name = "";
        Object.assign(this, data);
    }
}


/***/ }),

/***/ "S+5U":
/*!******************************************************************!*\
  !*** ./src/core/codeshell/directives/fix-date-time.directive.ts ***!
  \******************************************************************/
/*! exports provided: FixDateTime */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "FixDateTime", function() { return FixDateTime; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var _utilities_utils__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../utilities/utils */ "VXkE");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "3Pt+");
/* harmony import */ var angular2_datetimepicker__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! angular2-datetimepicker */ "1XXD");





class FixDateTime {
    constructor(model, el) {
        this.model = model;
        this.el = el;
        this.el.date = new Date;
        this.el.onDateSelect.subscribe((ev) => {
            this.model.viewToModelUpdate(Object(_utilities_utils__WEBPACK_IMPORTED_MODULE_1__["Date_Get"])(ev));
        });
    }
    ngOnInit() {
        this.el.settings.bigBanner = true;
        this.el.settings.timePicker = true;
        this.el.settings.format = 'dd-MM-yyyy hh:mm a';
        this.el.settings.defaultOpen = false;
    }
}
FixDateTime.ɵfac = function FixDateTime_Factory(t) { return new (t || FixDateTime)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_forms__WEBPACK_IMPORTED_MODULE_2__["NgModel"]), _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](angular2_datetimepicker__WEBPACK_IMPORTED_MODULE_3__["DatePicker"])); };
FixDateTime.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({ type: FixDateTime, selectors: [["angular2-date-picker", "ngModel", "", "fix-date-time", ""]], exportAs: ["fix-date-time"] });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](FixDateTime, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
        args: [{
                selector: "angular2-date-picker[ngModel][fix-date-time]",
                exportAs: "fix-date-time"
            }]
    }], function () { return [{ type: _angular_forms__WEBPACK_IMPORTED_MODULE_2__["NgModel"] }, { type: angular2_datetimepicker__WEBPACK_IMPORTED_MODULE_3__["DatePicker"] }]; }, null); })();


/***/ }),

/***/ "Se+h":
/*!********************************************************!*\
  !*** ./src/core/codeshell/helpers/editablePairsDTO.ts ***!
  \********************************************************/
/*! exports provided: EditablePairsDTO */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EditablePairsDTO", function() { return EditablePairsDTO; });
/* harmony import */ var _listItem__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./listItem */ "JLZs");

class EditablePairsDTO extends _listItem__WEBPACK_IMPORTED_MODULE_0__["ListItem"] {
    constructor() {
        super(...arguments);
        this.data = {};
    }
}


/***/ }),

/***/ "Sgtl":
/*!*****************************************************************!*\
  !*** ./src/core/codeshell/localization/ngxTransationService.ts ***!
  \*****************************************************************/
/*! exports provided: NgxTranslationService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "NgxTranslationService", function() { return NgxTranslationService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");


//import { TranslateService } from "@ngx-translate/core";
class NgxTranslationService {
    //constructor(private service: TranslateService) { }
    setDefaultLang(loc) {
        //this.service.setDefaultLang(loc);
    }
    use(loc) {
        //this.service.use(loc);
    }
}
NgxTranslationService.ɵfac = function NgxTranslationService_Factory(t) { return new (t || NgxTranslationService)(); };
NgxTranslationService.ɵprov = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineInjectable"]({ token: NgxTranslationService, factory: NgxTranslationService.ɵfac });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](NgxTranslationService, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"]
    }], null, null); })();


/***/ }),

/***/ "Sy1n":
/*!**********************************!*\
  !*** ./src/app/app.component.ts ***!
  \**********************************/
/*! exports provided: AppComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppComponent", function() { return AppComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var codeshell_main__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! codeshell/main */ "7OZ3");
/* harmony import */ var _base_app_component_base_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @base/app-component-base.component */ "DTUp");
/* harmony import */ var _angular_platform_browser__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/platform-browser */ "jhN1");
/* harmony import */ var _core_base_main_top_bar_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../core/base/main/top-bar.component */ "WNdI");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/common */ "ofXK");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/router */ "tyNb");
/* harmony import */ var _core_base_main_navigation_side_bar_component__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ../core/base/main/navigation-side-bar.component */ "a3Ap");









function AppComponent_div_6_Template(rf, ctx) { if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "div", 10);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](1, "navigation-side-bar");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
} }
function AppComponent_ng_template_10_Template(rf, ctx) { }
class AppComponent extends _base_app_component_base_component__WEBPACK_IMPORTED_MODULE_2__["AppComponentBase"] {
    constructor(inj, trans) {
        super(inj, trans);
        codeshell_main__WEBPACK_IMPORTED_MODULE_1__["Shell"].Main = this;
    }
}
AppComponent.ɵfac = function AppComponent_Factory(t) { return new (t || AppComponent)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injector"]), _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_platform_browser__WEBPACK_IMPORTED_MODULE_3__["Title"])); };
AppComponent.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineComponent"]({ type: AppComponent, selectors: [["app"]], features: [_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵInheritDefinitionFeature"]], decls: 15, vars: 5, consts: [[1, "loader-overlay"], [1, "loader"], [1, "wrapper"], ["topBar", ""], ["class", "wrapper-side", 4, "ngIf"], [1, "wrapper-content", 3, "ngClass"], [3, "ngClass", "dir"], ["values", "", 2, "display", "none"], ["lookupOptionsContainer", ""], ["viewParamsContainer", ""], [1, "wrapper-side"]], template: function AppComponent_Template(rf, ctx) { if (rf & 1) {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "div", 0);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](1, "div", 1);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](2, "div");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](3, "div", 2);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](4, "app-top-bar", null, 3);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](6, AppComponent_div_6_Template, 2, 0, "div", 4);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](7, "div", 5);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](8, "router-outlet");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](9, "div", 6);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](10, AppComponent_ng_template_10_Template, 0, 0, "ng-template");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](11, "div", 7, 8);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](13, "div", 7, 9);
    } if (rf & 2) {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("@loader", ctx.ShowLoader ? "shown" : "hidden");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](6);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", ctx.IsLoggedIn && ctx.ShowNav);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngClass", !ctx.IsLoggedIn || !ctx.ShowNav ? "expanded" : null);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngClass", ctx.Config.Locale == "ar" ? "ui-rtl" : null)("dir", ctx.Config.Locale == "ar" ? "rtl" : null);
    } }, directives: [_core_base_main_top_bar_component__WEBPACK_IMPORTED_MODULE_4__["TopBar"], _angular_common__WEBPACK_IMPORTED_MODULE_5__["NgIf"], _angular_common__WEBPACK_IMPORTED_MODULE_5__["NgClass"], _angular_router__WEBPACK_IMPORTED_MODULE_6__["RouterOutlet"], _core_base_main_navigation_side_bar_component__WEBPACK_IMPORTED_MODULE_7__["NavigationSideBar"]], encapsulation: 2 });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](AppComponent, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"],
        args: [{
                selector: 'app',
                templateUrl: './app.component.html'
            }]
    }], function () { return [{ type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Injector"] }, { type: _angular_platform_browser__WEBPACK_IMPORTED_MODULE_3__["Title"] }]; }, null); })();


/***/ }),

/***/ "U6Sh":
/*!**********************************************!*\
  !*** ./src/core/codeshell/security/index.ts ***!
  \**********************************************/
/*! exports provided: AccountServiceBase, AuthFilter, ResourceActions, DomainDataProvider, DomainData, RouteData, UserDTO, LoginResult, TokenData, Permission, AuthorizationError, AuthorizationServiceBase, SessionManager, ModuleItem, FunctionItem, SessionTokenData, TokenStorage */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _accountServiceBase__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./accountServiceBase */ "wNr3");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "AccountServiceBase", function() { return _accountServiceBase__WEBPACK_IMPORTED_MODULE_0__["AccountServiceBase"]; });

/* harmony import */ var _authFilter__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./authFilter */ "YTpK");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "AuthFilter", function() { return _authFilter__WEBPACK_IMPORTED_MODULE_1__["AuthFilter"]; });

/* harmony import */ var _models__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./models */ "gFuU");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "ResourceActions", function() { return _models__WEBPACK_IMPORTED_MODULE_2__["ResourceActions"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "DomainDataProvider", function() { return _models__WEBPACK_IMPORTED_MODULE_2__["DomainDataProvider"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "DomainData", function() { return _models__WEBPACK_IMPORTED_MODULE_2__["DomainData"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "RouteData", function() { return _models__WEBPACK_IMPORTED_MODULE_2__["RouteData"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "UserDTO", function() { return _models__WEBPACK_IMPORTED_MODULE_2__["UserDTO"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "LoginResult", function() { return _models__WEBPACK_IMPORTED_MODULE_2__["LoginResult"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "TokenData", function() { return _models__WEBPACK_IMPORTED_MODULE_2__["TokenData"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "Permission", function() { return _models__WEBPACK_IMPORTED_MODULE_2__["Permission"]; });

/* harmony import */ var _authorizationServiceBase__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./authorizationServiceBase */ "rp8W");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "AuthorizationError", function() { return _authorizationServiceBase__WEBPACK_IMPORTED_MODULE_3__["AuthorizationError"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "AuthorizationServiceBase", function() { return _authorizationServiceBase__WEBPACK_IMPORTED_MODULE_3__["AuthorizationServiceBase"]; });

/* harmony import */ var _sessionManager__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./sessionManager */ "M6Pn");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "SessionManager", function() { return _sessionManager__WEBPACK_IMPORTED_MODULE_4__["SessionManager"]; });

/* harmony import */ var _navs__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./navs */ "1Jgf");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "ModuleItem", function() { return _navs__WEBPACK_IMPORTED_MODULE_5__["ModuleItem"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "FunctionItem", function() { return _navs__WEBPACK_IMPORTED_MODULE_5__["FunctionItem"]; });

/* harmony import */ var _sessionTokenData__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./sessionTokenData */ "9uFN");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "SessionTokenData", function() { return _sessionTokenData__WEBPACK_IMPORTED_MODULE_6__["SessionTokenData"]; });

/* harmony import */ var _tokenStorage__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./tokenStorage */ "yvMo");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "TokenStorage", function() { return _tokenStorage__WEBPACK_IMPORTED_MODULE_7__["TokenStorage"]; });











/***/ }),

/***/ "UoIT":
/*!*************************************!*\
  !*** ./src/core/codeshell/shell.ts ***!
  \*************************************/
/*! exports provided: Shell */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Shell", function() { return Shell; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var _security_sessionManager__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./security/sessionManager */ "M6Pn");
/* harmony import */ var _ngx_translate_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @ngx-translate/core */ "sYmb");



class Shell {
    static get Translator() {
        if (Shell._translate == null)
            Shell._translate = Shell.Injector.get(_ngx_translate_core__WEBPACK_IMPORTED_MODULE_2__["TranslateService"]);
        return Shell._translate;
    }
    static get Session() {
        return this._session;
    }
    static Start(comp) {
        this.Main = comp;
        this._session = new _security_sessionManager__WEBPACK_IMPORTED_MODULE_1__["SessionManager"];
        this._session.GetDeviceId();
        this._session.CheckToken();
    }
    static MainAs() {
        return Shell.Main;
    }
    static Word(text, ...params) {
        return Shell.Translator.instant('Words.' + text, ...params);
    }
    static Message(text, ...params) {
        return Shell.Translator.instant(text, ...params);
    }
    static Column(text) {
        return Shell.Translator.instant(text);
    }
    static Page(text) {
        return Shell.Translator.instant(text);
    }
    static Translate(...params) {
        for (var i in params)
            params[i] = Shell.Translator.instant(params[i]);
        return params;
    }
    static TranslateIfNeeded(text) {
        if (!text)
            return "";
        return Shell.Translator.instant(text);
    }
}
Shell.ViewLoaded = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();


/***/ }),

/***/ "Ur/x":
/*!***********************************************************************!*\
  !*** ./src/core/codeshell/base-components/dto-edit-component-base.ts ***!
  \***********************************************************************/
/*! exports provided: DTOEditComponentBase */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DTOEditComponentBase", function() { return DTOEditComponentBase; });
/* harmony import */ var _editComponentBase__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./editComponentBase */ "G778");
/* harmony import */ var _helpers__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../helpers */ "GYDu");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/core */ "fXoL");




class DTOEditComponentBase extends _editComponentBase__WEBPACK_IMPORTED_MODULE_0__["EditComponentBase"] {
    constructor() {
        super(...arguments);
        this.model = {};
    }
    get ModelId() { return this.model.entity.id; }
    set ModelId(val) { this.model.entity.id = val; }
    DefaultModel() {
        return new _helpers__WEBPACK_IMPORTED_MODULE_1__["DTO"]();
    }
    SubmitNewAsync() {
        return this.Service.Save("post", this.model.entity);
    }
    SubmitUpdateAsync() {
        return this.Service.Update("put", this.model.entity);
    }
}
DTOEditComponentBase.ɵfac = function DTOEditComponentBase_Factory(t) { return ɵDTOEditComponentBase_BaseFactory(t || DTOEditComponentBase); };
DTOEditComponentBase.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵdefineComponent"]({ type: DTOEditComponentBase, selectors: [["ng-component"]], features: [_angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵInheritDefinitionFeature"]], decls: 0, vars: 0, template: function DTOEditComponentBase_Template(rf, ctx) { }, encapsulation: 2 });
const ɵDTOEditComponentBase_BaseFactory = /*@__PURE__*/ _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵgetInheritedFactory"](DTOEditComponentBase);
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵsetClassMetadata"](DTOEditComponentBase, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_2__["Component"],
        args: [{ template: '' }]
    }], null, null); })();


/***/ }),

/***/ "VXkE":
/*!***********************************************!*\
  !*** ./src/core/codeshell/utilities/utils.ts ***!
  \***********************************************/
/*! exports provided: absUrl, List_RemoveItem, List_RunRecursively_Nodes, List_OrderBy, List_OrderByDesc, List_FindIdRecusive, List_RunRecursively, List_RunRecursively_GEN, List_RunRecursivelyUp_GEN, List_ToggleItem, String_GetBeforeLast, String_GetAfterLast, Date_Get, Date_Elapsed, KeyValuePair, Utils */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "absUrl", function() { return absUrl; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "List_RemoveItem", function() { return List_RemoveItem; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "List_RunRecursively_Nodes", function() { return List_RunRecursively_Nodes; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "List_OrderBy", function() { return List_OrderBy; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "List_OrderByDesc", function() { return List_OrderByDesc; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "List_FindIdRecusive", function() { return List_FindIdRecusive; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "List_RunRecursively", function() { return List_RunRecursively; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "List_RunRecursively_GEN", function() { return List_RunRecursively_GEN; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "List_RunRecursivelyUp_GEN", function() { return List_RunRecursivelyUp_GEN; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "List_ToggleItem", function() { return List_ToggleItem; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "String_GetBeforeLast", function() { return String_GetBeforeLast; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "String_GetAfterLast", function() { return String_GetAfterLast; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Date_Get", function() { return Date_Get; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Date_Elapsed", function() { return Date_Elapsed; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "KeyValuePair", function() { return KeyValuePair; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Utils", function() { return Utils; });
/* harmony import */ var moment__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! moment */ "wd/R");
/* harmony import */ var moment__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(moment__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _shell__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../shell */ "UoIT");
/* harmony import */ var _helpers__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../helpers */ "GYDu");



function absUrl(url) {
    if (url) {
        if (url.length > 0) {
            if (url[0] != "/")
                url = "/" + url;
        }
        return url;
    }
    return "/";
}
function List_RemoveItem(lst, item) {
    let ind = lst.indexOf(item);
    if (ind > -1)
        lst.splice(ind, 1);
}
function List_RunRecursively_Nodes(lst, func) {
    for (let mod of lst) {
        func(mod);
        if (mod.children && mod.children.length > 0)
            List_RunRecursively_Nodes(mod.children, func);
    }
}
function List_OrderBy(arr, del) {
    arr.sort((a, b) => {
        var v1 = del(a);
        var v2 = del(b);
        if (v1 > v2)
            return 1;
        else if (v1 < v2)
            return -1;
        else
            return 0;
    });
    return arr;
}
function List_OrderByDesc(arr, del) {
    arr.sort((a, b) => {
        var v1 = del(a);
        var v2 = del(b);
        if (v1 > v2)
            return -1;
        else if (v1 < v2)
            return 1;
        else
            return 0;
    });
    return arr;
}
function List_FindIdRecusive(lst, id) {
    for (let mod of lst) {
        if (mod.id == id) {
            return mod;
        }
        else if (mod.children && mod.children.length > 0) {
            var m = List_FindIdRecusive(mod.children, id);
            if (m)
                return m;
        }
    }
    return null;
}
function List_RunRecursively(lst, func) {
    for (let mod of lst) {
        func(mod);
        if (mod.children && mod.children.length > 0)
            List_RunRecursively(mod.children, func);
    }
}
function List_RunRecursively_GEN(lst, func) {
    for (let mod of lst) {
        func(mod);
        if (mod.children && mod.children.length > 0)
            List_RunRecursively_GEN(mod.children, func);
    }
}
function List_RunRecursivelyUp_GEN(mod, func, first = true) {
    if (!first)
        func(mod);
    let Par = mod.parent;
    if (Par)
        List_RunRecursivelyUp_GEN(Par, func, false);
}
function List_ToggleItem(lst, item) {
    let ind = lst.indexOf(item);
    if (ind > -1)
        lst.splice(ind, 1);
    else
        lst.push(item);
}
function String_GetBeforeLast(data, del) {
    var x = data.lastIndexOf(del);
    return data.substr(0, x);
}
function String_GetAfterLast(data, del) {
    var x = data.lastIndexOf(del);
    if (x == -1)
        return data;
    return data.substr(x + del.length);
}
function Date_Get(ev) {
    let inp = ev;
    if (typeof ev == "string")
        inp = new Date(Date.parse(ev));
    return inp;
}
function Date_Elapsed(start, end) {
    if (end < start)
        return null;
    let endMoment = moment__WEBPACK_IMPORTED_MODULE_0__(end);
    let startMoment = moment__WEBPACK_IMPORTED_MODULE_0__(start);
    return moment__WEBPACK_IMPORTED_MODULE_0__["duration"](endMoment.diff(startMoment));
}
class KeyValuePair {
    constructor() {
        this.key = "";
        this.value = 0;
    }
}
class Utils {
    static GetQueryString(obj) {
        var str = [];
        for (var p in obj)
            if (obj.hasOwnProperty(p)) {
                str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
            }
        return str.join("&");
    }
    static GetIdString() {
        let sec = new Date().getTime();
        let gen = "";
        if (sec == this.lastSec) {
            this.i++;
        }
        else {
            this.i = 0;
        }
        gen = (sec.toString() + (this.i));
        this.lastSec = sec;
        return gen;
    }
    static HandleResult(res, note = false, deleting = false) {
        console.error("MESSAGE : " + res.message);
        console.error("EXCEPTION : " + res.exceptionMessage);
        if (res.stackTrace)
            console.error(res.stackTrace.join("\r\n"));
        if (res.data)
            console.error("INNER : ", res.data);
        if (note) {
            if (deleting) {
                var del = Object.assign(new _helpers__WEBPACK_IMPORTED_MODULE_2__["DeleteResult"], res);
                res = del;
                if (del.tableName) {
                    var message = _shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Message("delete_error_message", _shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Word(del.tableName));
                    _shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Main.Notify(message, _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"].Error);
                }
                else {
                    _shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Main.NotifyTranslate(res.message ? res.message : "error_message", _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"].Error);
                }
            }
            else {
                _shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Main.NotifyTranslate(res.message ? res.message : "error_message", _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"].Error);
            }
        }
    }
    static HandleError(e, note = false, deleting = false) {
        var res = new _helpers__WEBPACK_IMPORTED_MODULE_2__["SubmitResult"]();
        res.code = 1;
        if (e.error && e.error.code) {
            res = Object.assign(new _helpers__WEBPACK_IMPORTED_MODULE_2__["SubmitResult"], e.error);
            Utils.HandleResult(res, note, deleting);
        }
        else {
            switch (e.status) {
                case 404:
                    if (note)
                        _shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Main.NotifyTranslate("operation_unavailable", _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"].Error);
                    console.error("[" + e.url + "] : " + e.statusText + " >> " + e.message);
                    break;
                case 401:
                    if (note)
                        _shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Main.NotifyTranslate("unauthorized_operation", _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"].Error);
                    console.error("[" + e.url + "] : " + e.statusText + " >> " + e.message);
                    break;
                default:
                    if (note)
                        _shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Main.NotifyTranslate("unexpected_error", _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"].Error);
                    console.error("[" + e.url + "] : " + e.statusText + " >> " + e.message, _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"].Error);
                    break;
            }
        }
        return res;
    }
    static GetId() {
        var s = [];
        return Number.parseInt(this.GetIdString());
    }
    static Combine(...items) {
        let endsWithSlash = new RegExp("\/$");
        let startsWithSlash = new RegExp("^\/");
        let final = "";
        for (var i = 0; i < items.length; i++) {
            var st = items[i];
            if (startsWithSlash.test(st))
                st = st.substr(1, st.length);
            if (i == (items.length - 1)) {
                if (endsWithSlash.test(st))
                    st = st.substr(0, st.length - 1);
            }
            else {
                if (!endsWithSlash.test(st))
                    st = st + "/";
            }
            final += st;
        }
        return final;
    }
    static CalcAge(bDate) {
        var cDate = new Date();
        var mill = cDate.getTime() - bDate.getTime();
        var age = Math.floor(mill / (365 * 24 * 60 * 60 * 1000));
        return age;
    }
    static GetEnumString(E, value) {
        const keys = Object.keys(E).filter(k => typeof E[k] === "number"); // ["A", "B"]
        const values = keys.map(k => E[k]); // [0, 1]
    }
    static ConvertEnumToList(E) {
        const keys = Object.keys(E).filter(k => typeof E[k] === "number"); // ["A", "B"]
        const values = keys.map(k => E[k]); // [0, 1]
        var list = [];
        for (var i = 0; i < keys.length; i++) {
            list.push({ key: keys[i], value: values[i] });
        }
        return list;
    }
    static ConvertEnumToDictionary(E) {
        var dic = {};
        var lst = Utils.ConvertEnumToList(E);
        for (var i of lst)
            dic[i.value] = i.key;
        return dic;
    }
    static ConvertEnumToListLocalization(E, enumName) {
        const keys = Object.keys(E).filter(k => typeof E[k] === "number"); // ["A", "B"]
        const values = keys.map(k => E[k]); // [0, 1]
        var list = [];
        for (var i = 0; i < keys.length; i++) {
            keys[i] = _shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Word(enumName + "_" + keys[i]);
            list.push({ key: keys[i], value: values[i] });
        }
        return list;
    }
}
Utils.i = 0;


/***/ }),

/***/ "VZL9":
/*!**********************************************************!*\
  !*** ./src/core/codeshell/base-components/topBarBase.ts ***!
  \**********************************************************/
/*! exports provided: TopBarBase */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "TopBarBase", function() { return TopBarBase; });
/* harmony import */ var codeshell_serverConfigBase__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! codeshell/serverConfigBase */ "6m0k");
/* harmony import */ var codeshell_shell__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! codeshell/shell */ "UoIT");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/router */ "tyNb");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/common/http */ "tk/3");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/core */ "fXoL");






class TopBarBase {
    constructor() {
        this.isLoggedIn = false;
        this.navState = true;
        this.changePasswordItem = false;
        this.editProfileItem = false;
        this.notificationCount = 0;
    }
    get Router() {
        return codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Injector.get(_angular_router__WEBPACK_IMPORTED_MODULE_2__["Router"]);
    }
    _startListener() {
        if (this.Listener) {
            this.Listener.NotificationsChanged.subscribe(e => {
                console.log(e);
                this.notificationCount = e;
                this.OnNotificationChanged(e);
            });
            this.Listener.KeepAlive = true;
        }
    }
    ngOnInit() {
        this._startListener();
        codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Session.GetUserAsync().then(d => {
            this.isLoggedIn = true;
            this.user = d;
            this._onUser(this.user);
            this.OnReady();
        }).catch(d => {
            this.OnStartNoSession();
            this.OnReady();
        });
        codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Session.LogStatus.subscribe((v) => {
            this.isLoggedIn = v;
            this.OnLogStatusChange(v);
        });
        codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Session.OnLogin.subscribe((u) => {
            this.user = u;
            this._onUser(u);
        });
        codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].ViewLoaded.subscribe((d) => {
            //$(".wrapper-side").removeClass("expanded");
        });
        var conf = codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Injector.get(codeshell_serverConfigBase__WEBPACK_IMPORTED_MODULE_0__["ServerConfigBase"]);
        this.Lang = conf.Locale;
    }
    /**
     * overridable : called after user data is obtained from server
     */
    OnReady() { }
    /**
     * overridable called when user logs in or out
     * @param status
     */
    OnLogStatusChange(status) { }
    /**
     * called after login or when user is found
     */
    OnSession(userDto) {
    }
    _onUser(dto) {
        if (this.Listener) {
            this.Listener.StartWithUser(dto.userId);
        }
        if (this.NotificationService)
            this.NotificationService.GetCount().then(e => this.notificationCount = e);
        this.OnSession(dto);
    }
    OnNotificationChanged(c) {
    }
    /**
     * overridable : called when failing to obtain user data on startup
     */
    OnStartNoSession() { }
    Logout() {
        codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Session.EndSession();
        this.isLoggedIn = false;
        codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Injector.get(_angular_router__WEBPACK_IMPORTED_MODULE_2__["Router"]).navigateByUrl("/Login");
    }
    Slide() {
        if (!codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Main.ShowNav)
            return;
        //$(".wrapper-side").toggleClass("expanded");
    }
    ToggleNav() {
        if (!codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Main.ShowNav)
            return;
        this.setSideBarState(!this.navState);
    }
    setSideBarState(state) {
        this.navState = state;
        if (!state) {
            //$(".wrapper-side").addClass("compressed");
            // $(".wrapper-content").addClass("expanded");
        }
        else {
            //$(".wrapper-side").removeClass("compressed");
            //$(".wrapper-content").removeClass("expanded");
        }
    }
    ChangeLang() {
        var cl = codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Injector.get(_angular_common_http__WEBPACK_IMPORTED_MODULE_3__["HttpClient"]);
        var conf = codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Injector.get(codeshell_serverConfigBase__WEBPACK_IMPORTED_MODULE_0__["ServerConfigBase"]);
        cl.get("/Home/SetLocale/?lang=" + (conf.Locale == 'ar' ? 'en' : 'ar')).subscribe(d => {
            location.reload();
        });
    }
    SetLang(lng) {
        var cl = codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Injector.get(_angular_common_http__WEBPACK_IMPORTED_MODULE_3__["HttpClient"]);
        var conf = codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Injector.get(codeshell_serverConfigBase__WEBPACK_IMPORTED_MODULE_0__["ServerConfigBase"]);
        this.Lang = conf.Locale;
        cl.get("/Home/SetLocale/?lang=" + lng).subscribe(d => {
            this.Lang = lng;
            location.reload();
        });
    }
    OpenModal(path) {
        var comp = codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Main.GetDialogAs(path);
        if (comp != null) {
            return Promise.resolve(comp);
        }
        return Promise.reject("not found");
    }
    ChangePassword() {
        if (this.changePasswordComponent) {
            this.OpenModal(this.changePasswordComponent).then(comp => {
                comp.instance.Show = true;
                comp.instance.DataSubmitted = res => {
                    comp.instance.Show = false;
                };
            });
        }
    }
    EditProfile() {
        if (this.editProfileComponent)
            this.Router.navigateByUrl(this.editProfileComponent);
    }
}
TopBarBase.ɵfac = function TopBarBase_Factory(t) { return new (t || TopBarBase)(); };
TopBarBase.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵdefineComponent"]({ type: TopBarBase, selectors: [["ng-component"]], decls: 0, vars: 0, template: function TopBarBase_Template(rf, ctx) { }, encapsulation: 2 });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵsetClassMetadata"](TopBarBase, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_4__["Component"],
        args: [{ template: '' }]
    }], function () { return []; }, null); })();


/***/ }),

/***/ "WNdI":
/*!*************************************************!*\
  !*** ./src/core/base/main/top-bar.component.ts ***!
  \*************************************************/
/*! exports provided: TopBar */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "TopBar", function() { return TopBar; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var codeshell_base_components__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! codeshell/base-components */ "gyvI");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/common */ "ofXK");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/router */ "tyNb");
/* harmony import */ var _ngx_translate_core__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @ngx-translate/core */ "sYmb");






function TopBar_li_16_Template(rf, ctx) { if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "li");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](1, "a", 12);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](3, "translate");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](4, "i", 13);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
} if (rf & 2) {
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate1"](" ", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind1"](3, 1, "Words.Login"), " ");
} }
function TopBar_li_17_li_6_Template(rf, ctx) { if (rf & 1) {
    const _r5 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵgetCurrentView"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "li");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](1, "a", 9);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function TopBar_li_17_li_6_Template_a_click_1_listener() { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r5); const ctx_r4 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"](2); return ctx_r4.ChangePassword(); });
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](2, "i", 20);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](3);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](4, "translate");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
} if (rf & 2) {
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](3);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate1"](" \u00A0\u00A0", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind1"](4, 1, "Words.ChangePassword"), " ");
} }
function TopBar_li_17_li_7_Template(rf, ctx) { if (rf & 1) {
    const _r7 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵgetCurrentView"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "li");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](1, "a", 9);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function TopBar_li_17_li_7_Template_a_click_1_listener() { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r7); const ctx_r6 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"](2); return ctx_r6.EditProfile(); });
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](2, "i", 21);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](3);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](4, "translate");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
} if (rf & 2) {
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](3);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate1"](" \u00A0\u00A0", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind1"](4, 1, "Words.EditProfile"), " ");
} }
function TopBar_li_17_Template(rf, ctx) { if (rf & 1) {
    const _r9 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵgetCurrentView"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "li", 14);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](1, "a", 15);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](3, "img", 16);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](4, "span", 17);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](5, "ul", 18);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](6, TopBar_li_17_li_6_Template, 5, 3, "li", 10);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](7, TopBar_li_17_li_7_Template, 5, 3, "li", 10);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](8, "li");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](9, "a", 9);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function TopBar_li_17_Template_a_click_9_listener() { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r9); const ctx_r8 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"](); return ctx_r8.Logout(); });
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](10, "i", 19);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](11);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](12, "translate");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
} if (rf & 2) {
    const ctx_r1 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate1"](" ", ctx_r1.user.name, " ");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("src", ctx_r1.user.photo ? "/" + ctx_r1.user.photo : "/img/default_user.png", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵsanitizeUrl"]);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](3);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", ctx_r1.changePasswordComponent);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", ctx_r1.editProfileComponent);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](4);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate1"](" \u00A0\u00A0 ", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind1"](12, 5, "Words.Logout"), " ");
} }
class TopBar extends codeshell_base_components__WEBPACK_IMPORTED_MODULE_1__["TopBarBase"] {
}
TopBar.ɵfac = function TopBar_Factory(t) { return ɵTopBar_BaseFactory(t || TopBar); };
TopBar.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineComponent"]({ type: TopBar, selectors: [["app-top-bar"]], exportAs: ["app-top-bar"], features: [_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵInheritDefinitionFeature"]], decls: 18, vars: 5, consts: [[1, "top-bar"], [1, "container-fluid"], [1, "row"], [1, "pull-first"], [1, "nav", "nav-pills"], [1, "opt-btn", "mob", 3, "click"], [1, "fa", "fa-bars"], [1, "opt-btn", "not-mob", 3, "click"], [1, "pull-last"], [3, "click"], [4, "ngIf"], ["role", "presentation", "class", "dropdown", 4, "ngIf"], ["routerLink", "/Login", "routerLinkActive", "hidden"], [1, "fa", "fa-sign-in-alt"], ["role", "presentation", 1, "dropdown"], ["data-toggle", "dropdown", "href", "#", "role", "button", "aria-haspopup", "true", "aria-expanded", "false", 1, "dropdown-toggle"], [1, "img-circle", 3, "src"], [1, "caret"], [1, "dropdown-menu", "navbar-inverse"], [1, "fa", "fa-sign-out-alt"], [1, "fa", "fa-key"], [1, "fa", "fa-user"]], template: function TopBar_Template(rf, ctx) { if (rf & 1) {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "nav", 0);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](1, "div", 1);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](2, "div", 2);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](3, "div", 3);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](4, "ul", 4);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](5, "li");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](6, "a", 5);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function TopBar_Template_a_click_6_listener() { return ctx.Slide(); });
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](7, "i", 6);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](8, "a", 7);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function TopBar_Template_a_click_8_listener() { return ctx.ToggleNav(); });
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](9, "i", 6);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](10, "div", 8);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](11, "ul", 4);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](12, "li");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](13, "a", 9);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function TopBar_Template_a_click_13_listener() { return ctx.ChangeLang(); });
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](14);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](15, "translate");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](16, TopBar_li_16_Template, 5, 3, "li", 10);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](17, TopBar_li_17_Template, 13, 7, "li", 11);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
    } if (rf & 2) {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](14);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate1"](" ", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind1"](15, 3, "Words.Lang"), " ");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", !ctx.isLoggedIn);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", ctx.isLoggedIn);
    } }, directives: [_angular_common__WEBPACK_IMPORTED_MODULE_2__["NgIf"], _angular_router__WEBPACK_IMPORTED_MODULE_3__["RouterLinkWithHref"], _angular_router__WEBPACK_IMPORTED_MODULE_3__["RouterLinkActive"]], pipes: [_ngx_translate_core__WEBPACK_IMPORTED_MODULE_4__["TranslatePipe"]], encapsulation: 2 });
const ɵTopBar_BaseFactory = /*@__PURE__*/ _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵgetInheritedFactory"](TopBar);
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](TopBar, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"],
        args: [{ templateUrl: "./top-bar.component.html", selector: "app-top-bar", exportAs: "app-top-bar" }]
    }], null, null); })();


/***/ }),

/***/ "WyvS":
/*!*************************************************!*\
  !*** ./src/core/codeshell/pipes/absoluteUrl.ts ***!
  \*************************************************/
/*! exports provided: AbsoluteUrl */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AbsoluteUrl", function() { return AbsoluteUrl; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var codeshell_utilities_utils__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! codeshell/utilities/utils */ "VXkE");



class AbsoluteUrl {
    transform(value) {
        return Object(codeshell_utilities_utils__WEBPACK_IMPORTED_MODULE_1__["absUrl"])(value);
    }
}
AbsoluteUrl.ɵfac = function AbsoluteUrl_Factory(t) { return new (t || AbsoluteUrl)(); };
AbsoluteUrl.ɵpipe = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefinePipe"]({ name: "absUrl", type: AbsoluteUrl, pure: true });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](AbsoluteUrl, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Pipe"],
        args: [{ name: 'absUrl' }]
    }], null, null); })();


/***/ }),

/***/ "XGJ5":
/*!******************************************************************!*\
  !*** ./src/core/codeshell/directives/control-group.directive.ts ***!
  \******************************************************************/
/*! exports provided: BsFormGroup */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "BsFormGroup", function() { return BsFormGroup; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");


class BsFormGroup {
    constructor(el) {
        this.el = el;
        this.TextInput = null;
        this.SelectInput = null;
        this.TextareaInput = null;
        this.RadioInput = null;
        this.OtherInput = null;
        this._parent = null;
        this._enabled = true;
    }
    get InputControl() { return this._getInputControl(); }
    get Write() { return this._enabled; }
    ;
    get Group() { return this.el.nativeElement; }
    get Enabled() { return this._enabled; }
    set Enabled(val) {
        this._setEnabled(val);
    }
    get Value() { return this._getValue(); }
    ngOnInit() {
        let rad = this.Group.querySelector(".radio-group");
        if (rad) {
            this.RadioInput = rad;
        }
        else {
            this.TextInput = this.Group.querySelector("input");
            this.SelectInput = this.Group.querySelector("select");
            this.TextareaInput = this.Group.querySelector("textarea");
            this.OtherInput = this.Group.querySelector(".input-control");
        }
        if (this.InputControl)
            this._parent = this.InputControl.parentElement;
    }
    _setEnabled(val) {
        if (this._enabled == val)
            return;
        this._enabled = val;
        if (this.RadioInput) {
            let inps = this.RadioInput.getElementsByTagName("input");
            for (let i = 0; i < inps.length; i++) {
                var s = inps.item(i);
                if (s)
                    s.disabled = !this._enabled;
            }
            return;
        }
        if (this._enabled == false) {
            if (this._parent)
                this._parent.removeChild(this.InputControl);
        }
        else {
            if (this._parent)
                this._parent.appendChild(this.InputControl);
        }
    }
    _getValue() {
        if (this.TextInput)
            return this.TextInput.value;
        else if (this.SelectInput) {
            var ite = this.SelectInput.selectedOptions.item(0);
            if (ite) {
                return ite.innerHTML;
            }
        }
        else if (this.TextareaInput) {
            return this.TextareaInput.value;
        }
        return "";
    }
    _getInputControl() {
        if (this.TextInput)
            return this.TextInput;
        else if (this.SelectInput) {
            return this.SelectInput;
        }
        else if (this.TextareaInput) {
            return this.TextareaInput;
        }
        else if (this.OtherInput) {
            return this.OtherInput;
        }
        return null;
    }
}
BsFormGroup.ɵfac = function BsFormGroup_Factory(t) { return new (t || BsFormGroup)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"])); };
BsFormGroup.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({ type: BsFormGroup, selectors: [["", "bs-group", ""]], exportAs: ["bsFormGroup"] });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](BsFormGroup, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
        args: [{ selector: "[bs-group]", exportAs: "bsFormGroup" }]
    }], function () { return [{ type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"] }]; }, null); })();


/***/ }),

/***/ "XgJV":
/*!************************************************!*\
  !*** ./src/core/codeshell/directives/index.ts ***!
  \************************************************/
/*! exports provided: ImagePreLoad, OnEnter, ShowIf, Editable, SlimScroll, ComponentLoader, BsFormGroup, ListItemWatcher, Radio, DirctionFix, FixDate, FixDateTime, Selectable, FileUploader */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _image_preLoad_directive__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./image-preLoad.directive */ "5Qa7");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "ImagePreLoad", function() { return _image_preLoad_directive__WEBPACK_IMPORTED_MODULE_0__["ImagePreLoad"]; });

/* harmony import */ var _on_enter_directive__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./on-enter.directive */ "F2JI");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "OnEnter", function() { return _on_enter_directive__WEBPACK_IMPORTED_MODULE_1__["OnEnter"]; });

/* harmony import */ var _show_if_directive__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./show-if.directive */ "bWkv");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "ShowIf", function() { return _show_if_directive__WEBPACK_IMPORTED_MODULE_2__["ShowIf"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "Editable", function() { return _show_if_directive__WEBPACK_IMPORTED_MODULE_2__["Editable"]; });

/* harmony import */ var _slim_scroll_directive__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./slim-scroll.directive */ "6Ipe");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "SlimScroll", function() { return _slim_scroll_directive__WEBPACK_IMPORTED_MODULE_3__["SlimScroll"]; });

/* harmony import */ var _component_loader_directive__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./component-loader.directive */ "j3l2");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "ComponentLoader", function() { return _component_loader_directive__WEBPACK_IMPORTED_MODULE_4__["ComponentLoader"]; });

/* harmony import */ var _control_group_directive__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./control-group.directive */ "XGJ5");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "BsFormGroup", function() { return _control_group_directive__WEBPACK_IMPORTED_MODULE_5__["BsFormGroup"]; });

/* harmony import */ var _list_item_watcher_directive__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./list-item-watcher.directive */ "EuMQ");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "ListItemWatcher", function() { return _list_item_watcher_directive__WEBPACK_IMPORTED_MODULE_6__["ListItemWatcher"]; });

/* harmony import */ var _radio_directive__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./radio.directive */ "qJZ4");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "Radio", function() { return _radio_directive__WEBPACK_IMPORTED_MODULE_7__["Radio"]; });

/* harmony import */ var _direction_fix_directive__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./direction-fix.directive */ "n2jX");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "DirctionFix", function() { return _direction_fix_directive__WEBPACK_IMPORTED_MODULE_8__["DirctionFix"]; });

/* harmony import */ var _fix_date_directive__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ./fix-date.directive */ "x5Rz");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "FixDate", function() { return _fix_date_directive__WEBPACK_IMPORTED_MODULE_9__["FixDate"]; });

/* harmony import */ var _fix_date_time_directive__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ./fix-date-time.directive */ "S+5U");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "FixDateTime", function() { return _fix_date_time_directive__WEBPACK_IMPORTED_MODULE_10__["FixDateTime"]; });

/* harmony import */ var _selectable_directive__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ./selectable.directive */ "CJgx");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "Selectable", function() { return _selectable_directive__WEBPACK_IMPORTED_MODULE_11__["Selectable"]; });

/* harmony import */ var _file_uploader_directive__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! ./file-uploader.directive */ "ZCxi");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "FileUploader", function() { return _file_uploader_directive__WEBPACK_IMPORTED_MODULE_12__["FileUploader"]; });
















/***/ }),

/***/ "YNU7":
/*!*******************************************************************!*\
  !*** ./src/core/codeshell/components/duration-input.component.ts ***!
  \*******************************************************************/
/*! exports provided: DurationInput */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DurationInput", function() { return DurationInput; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common */ "ofXK");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "3Pt+");




function DurationInput_p_2_Template(rf, ctx) { if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "p");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
} if (rf & 2) {
    const ctx_r0 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate"](ctx_r0.hours);
} }
function DurationInput_input_3_Template(rf, ctx) { if (rf & 1) {
    const _r7 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵgetCurrentView"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "input", 6);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("change", function DurationInput_input_3_Template_input_change_0_listener() { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r7); const ctx_r6 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"](); return ctx_r6.onPartChanged(); })("ngModelChange", function DurationInput_input_3_Template_input_ngModelChange_0_listener($event) { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r7); const ctx_r8 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"](); return ctx_r8.hours = $event; });
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
} if (rf & 2) {
    const ctx_r1 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngModel", ctx_r1.hours);
} }
function DurationInput_p_7_Template(rf, ctx) { if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "p");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
} if (rf & 2) {
    const ctx_r2 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate"](ctx_r2.minutes);
} }
function DurationInput_input_8_Template(rf, ctx) { if (rf & 1) {
    const _r10 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵgetCurrentView"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "input", 7);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("change", function DurationInput_input_8_Template_input_change_0_listener() { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r10); const ctx_r9 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"](); return ctx_r9.onPartChanged(); })("ngModelChange", function DurationInput_input_8_Template_input_ngModelChange_0_listener($event) { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r10); const ctx_r11 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"](); return ctx_r11.minutes = $event; });
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
} if (rf & 2) {
    const ctx_r3 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngModel", ctx_r3.minutes);
} }
function DurationInput_p_12_Template(rf, ctx) { if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "p");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
} if (rf & 2) {
    const ctx_r4 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate"](ctx_r4.seconds);
} }
function DurationInput_input_13_Template(rf, ctx) { if (rf & 1) {
    const _r13 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵgetCurrentView"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "input", 8);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("change", function DurationInput_input_13_Template_input_change_0_listener() { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r13); const ctx_r12 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"](); return ctx_r12.onPartChanged(); })("ngModelChange", function DurationInput_input_13_Template_input_ngModelChange_0_listener($event) { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r13); const ctx_r14 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"](); return ctx_r14.seconds = $event; });
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
} if (rf & 2) {
    const ctx_r5 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngModel", ctx_r5.seconds);
} }
class DurationInput {
    constructor(el) {
        this.el = el;
        this.hours = 0;
        this.minutes = 0;
        this.seconds = 0;
        this._model = 0;
        this.readOnly = false;
        this.modelChange = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        let html = el.nativeElement;
    }
    get model() { return this._model; }
    ;
    set model(val) {
        this._model = val;
        this.applyToFields();
    }
    onPartChanged() {
        this._model = this.hours * 60 * 60 + this.minutes * 60 + this.seconds;
        this.modelChange.emit(this._model);
    }
    applyToFields() {
        this.hours = 0;
        this.minutes = 0;
        this.seconds = 0;
        if (this._model >= 3600) {
            this.hours = Math.floor(this._model / (3600));
        }
        if (this._model >= 60) {
            var hrsRem = this._model % 3600;
            this.minutes = Math.floor(hrsRem / 60);
        }
        var minRem = this._model % 60;
        this.seconds = Math.floor(minRem);
    }
}
DurationInput.ɵfac = function DurationInput_Factory(t) { return new (t || DurationInput)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"])); };
DurationInput.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineComponent"]({ type: DurationInput, selectors: [["duration-input"]], inputs: { readOnly: "readOnly", model: ["totalSeconds", "model"] }, outputs: { modelChange: "totalSecondsChange" }, exportAs: ["duration-input"], decls: 16, vars: 9, consts: [[1, "di-container"], [1, "di-part-container"], [4, "ngIf"], ["type", "number", "class", "form-control di-hours", "min", "0", "max", "24", 3, "ngModel", "change", "ngModelChange", 4, "ngIf"], ["type", "number", "class", "form-control di-minutes", "min", "0", "max", "59", 3, "ngModel", "change", "ngModelChange", 4, "ngIf"], ["type", "number", "class", "form-control di-seconds", "min", "0", "max", "59", 3, "ngModel", "change", "ngModelChange", 4, "ngIf"], ["type", "number", "min", "0", "max", "24", 1, "form-control", "di-hours", 3, "ngModel", "change", "ngModelChange"], ["type", "number", "min", "0", "max", "59", 1, "form-control", "di-minutes", 3, "ngModel", "change", "ngModelChange"], ["type", "number", "min", "0", "max", "59", 1, "form-control", "di-seconds", 3, "ngModel", "change", "ngModelChange"]], template: function DurationInput_Template(rf, ctx) { if (rf & 1) {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "div", 0);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](1, "div", 1);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](2, DurationInput_p_2_Template, 2, 1, "p", 2);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](3, DurationInput_input_3_Template, 1, 1, "input", 3);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](4, "label");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](5);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](6, "div", 1);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](7, DurationInput_p_7_Template, 2, 1, "p", 2);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](8, DurationInput_input_8_Template, 1, 1, "input", 4);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](9, "label");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](10);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](11, "div", 1);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](12, DurationInput_p_12_Template, 2, 1, "p", 2);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](13, DurationInput_input_13_Template, 1, 1, "input", 5);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](14, "label");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](15);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
    } if (rf & 2) {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", ctx.readOnly);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", !ctx.readOnly);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate"]("Words.Hours");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", ctx.readOnly);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", !ctx.readOnly);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate"]("Words.Minutes");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", ctx.readOnly);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", !ctx.readOnly);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate"]("Words.Seconds");
    } }, directives: [_angular_common__WEBPACK_IMPORTED_MODULE_1__["NgIf"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["NumberValueAccessor"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["DefaultValueAccessor"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["NgControlStatus"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["NgModel"]], encapsulation: 2 });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](DurationInput, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"],
        args: [{ templateUrl: "./duration-input.component.html", selector: "duration-input", exportAs: "duration-input" }]
    }], function () { return [{ type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"] }]; }, { readOnly: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["readOnly"]
        }], model: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["totalSeconds"]
        }], modelChange: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"],
            args: ["totalSecondsChange"]
        }] }); })();


/***/ }),

/***/ "YTpK":
/*!***************************************************!*\
  !*** ./src/core/codeshell/security/authFilter.ts ***!
  \***************************************************/
/*! exports provided: AuthFilter */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AuthFilter", function() { return AuthFilter; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var _models__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./models */ "gFuU");
/* harmony import */ var _authorizationServiceBase__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./authorizationServiceBase */ "rp8W");




class AuthFilter {
    constructor(authorizationService) {
        this.authorizationService = authorizationService;
    }
    canActivate(route, state) {
        let data = Object.assign(new _models__WEBPACK_IMPORTED_MODULE_1__["RouteData"], route.data);
        return this.authorizationService.IsAuthorizedAsync(data);
    }
}
AuthFilter.ɵfac = function AuthFilter_Factory(t) { return new (t || AuthFilter)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵinject"](_authorizationServiceBase__WEBPACK_IMPORTED_MODULE_2__["AuthorizationServiceBase"])); };
AuthFilter.ɵprov = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineInjectable"]({ token: AuthFilter, factory: AuthFilter.ɵfac });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](AuthFilter, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"]
    }], function () { return [{ type: _authorizationServiceBase__WEBPACK_IMPORTED_MODULE_2__["AuthorizationServiceBase"] }]; }, null); })();


/***/ }),

/***/ "ZCxi":
/*!******************************************************************!*\
  !*** ./src/core/codeshell/directives/file-uploader.directive.ts ***!
  \******************************************************************/
/*! exports provided: FileUploader */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "FileUploader", function() { return FileUploader; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");


class FileUploader {
    constructor(elem) {
        this.multiple = false;
        this.FileDataChange = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.fileDataManyChange = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this._fileData = null;
        this._fileDataMany = [];
        this._fileInput = false;
        var el = elem.nativeElement;
        if (el.type && el.type == "file") {
            this._fileInput = true;
        }
        this.Element = elem.nativeElement;
    }
    get FileData() { return this._fileData; }
    set FileData(data) { this._fileData = data; }
    get fileDataMany() { return this._fileDataMany; }
    set fileDataMany(data) { this._fileDataMany = data; }
    OnDragOver(event) {
        event.stopPropagation();
        event.preventDefault();
    }
    OnDrop(event) {
        if (event.dataTransfer) {
            this._uploadTmp(event.dataTransfer.files);
        }
        event.preventDefault();
    }
    OnChange(ev) {
        if (!this._fileInput)
            return;
        if (this.Element.files) {
            this._uploadTmp(this.Element.files);
        }
        if (this.Element)
            this.Element.value = "";
    }
    _uploadTmp(files) {
        if (this.Uploader) {
            this.Uploader(files).then(d => {
                if (this.multiple) {
                    this._fileDataMany = d;
                    this.fileDataManyChange.emit(this._fileDataMany);
                }
                else if (d.length > 0) {
                    this._fileData = d[0];
                    this.FileDataChange.emit(this._fileData);
                }
                else {
                    this._fileData = null;
                    this.FileDataChange.emit(null);
                }
            }).catch(e => {
                this._fileData = null;
                this.FileDataChange.emit(null);
            });
        }
        this.Element.value = "";
    }
    ngOnChanges(changes) {
    }
}
FileUploader.ɵfac = function FileUploader_Factory(t) { return new (t || FileUploader)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"])); };
FileUploader.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({ type: FileUploader, selectors: [["", "file-uploader", ""]], hostBindings: function FileUploader_HostBindings(rf, ctx) { if (rf & 1) {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("dragover", function FileUploader_dragover_HostBindingHandler($event) { return ctx.OnDragOver($event); })("drop", function FileUploader_drop_HostBindingHandler($event) { return ctx.OnDrop($event); })("change", function FileUploader_change_HostBindingHandler($event) { return ctx.OnChange($event); });
    } }, inputs: { multiple: "multiple", Uploader: ["file-uploader", "Uploader"], FileData: ["fileData", "FileData"], fileDataMany: "fileDataMany" }, outputs: { FileDataChange: "fileDataChange", fileDataManyChange: "fileDataManyChange" }, exportAs: ["[file-uploader]"], features: [_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵNgOnChangesFeature"]] });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](FileUploader, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
        args: [{ selector: "[file-uploader]", exportAs: "[file-uploader]" }]
    }], function () { return [{ type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"] }]; }, { multiple: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["multiple"]
        }], Uploader: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["file-uploader"]
        }], FileData: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["fileData"]
        }], FileDataChange: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"],
            args: ["fileDataChange"]
        }], fileDataMany: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["fileDataMany"]
        }], fileDataManyChange: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"],
            args: ["fileDataManyChange"]
        }], OnDragOver: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"],
            args: ["dragover", ["$event"]]
        }], OnDrop: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"],
            args: ["drop", ["$event"]]
        }], OnChange: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"],
            args: ["change", ["$event"]]
        }] }); })();


/***/ }),

/***/ "a3Ap":
/*!*************************************************************!*\
  !*** ./src/core/base/main/navigation-side-bar.component.ts ***!
  \*************************************************************/
/*! exports provided: NavigationSideBar */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "NavigationSideBar", function() { return NavigationSideBar; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var codeshell_base_components__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! codeshell/base-components */ "gyvI");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/router */ "tyNb");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/common */ "ofXK");
/* harmony import */ var _ngx_translate_core__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @ngx-translate/core */ "sYmb");






function NavigationSideBar_li_6_Template(rf, ctx) { if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "li");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](1, "a", 4);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](3, "translate");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
} if (rf & 2) {
    const it_r1 = ctx.$implicit;
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("routerLink", "/" + it_r1.url);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind1"](3, 2, "Pages." + it_r1.name));
} }
class NavigationSideBar extends codeshell_base_components__WEBPACK_IMPORTED_MODULE_1__["NavigationSideBarBase"] {
    constructor(inj) {
        super(inj);
    }
}
NavigationSideBar.ɵfac = function NavigationSideBar_Factory(t) { return new (t || NavigationSideBar)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injector"])); };
NavigationSideBar.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineComponent"]({ type: NavigationSideBar, selectors: [["navigation-side-bar"]], features: [_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵInheritDefinitionFeature"]], decls: 7, vars: 5, consts: [[1, "navbar-inverse", "nav-side"], [1, "nav"], [3, "routerLink"], [4, "ngFor", "ngForOf"], ["routerLinkActive", "active", 3, "routerLink"]], template: function NavigationSideBar_Template(rf, ctx) { if (rf & 1) {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "nav", 0);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](1, "ul", 1);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](2, "li");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](3, "a", 2);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](4);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](5, "translate");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](6, NavigationSideBar_li_6_Template, 4, 4, "li", 3);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
    } if (rf & 2) {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](3);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("routerLink", ctx.GetMainUrl());
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind1"](5, 3, "Words.Main"));
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngForOf", ctx.navs);
    } }, directives: [_angular_router__WEBPACK_IMPORTED_MODULE_2__["RouterLinkWithHref"], _angular_common__WEBPACK_IMPORTED_MODULE_3__["NgForOf"], _angular_router__WEBPACK_IMPORTED_MODULE_2__["RouterLinkActive"]], pipes: [_ngx_translate_core__WEBPACK_IMPORTED_MODULE_4__["TranslatePipe"]], encapsulation: 2 });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](NavigationSideBar, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"],
        args: [{ templateUrl: "./navigation-side-bar.component.html", selector: "navigation-side-bar" }]
    }], function () { return [{ type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Injector"] }]; }, null); })();


/***/ }),

/***/ "afo8":
/*!****************************************************!*\
  !*** ./src/core/codeshell/utilities/extensions.ts ***!
  \****************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

String.prototype.getBeforeLast = function (data) {
    var x = String(this).lastIndexOf(data);
    return String(this).substr(0, x);
};


/***/ }),

/***/ "bT1W":
/*!************************************************!*\
  !*** ./src/core/codeshell/codeshell.module.ts ***!
  \************************************************/
/*! exports provided: CodeShellModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "CodeShellModule", function() { return CodeShellModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common */ "ofXK");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "3Pt+");
/* harmony import */ var _angular_material_datepicker__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/material/datepicker */ "iadO");
/* harmony import */ var _angular_material_moment_adapter__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/material-moment-adapter */ "1yaQ");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/common/http */ "tk/3");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/router */ "tyNb");
/* harmony import */ var _ngx_translate_core__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @ngx-translate/core */ "sYmb");
/* harmony import */ var ngx_quill__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ngx-quill */ "CzEO");
/* harmony import */ var _ng_select_ng_select__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @ng-select/ng-select */ "ZOsW");
/* harmony import */ var angular_tree_component__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! angular-tree-component */ "rDsv");
/* harmony import */ var _directives__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ./directives */ "XgJV");
/* harmony import */ var _validators__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! ./validators */ "eXOA");
/* harmony import */ var _components__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! ./components */ "4N+8");
/* harmony import */ var _localization__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ./localization */ "InfA");
/* harmony import */ var _security__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ./security */ "U6Sh");
/* harmony import */ var _security_authorizationServiceBase__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ./security/authorizationServiceBase */ "rp8W");
/* harmony import */ var _security_tokenStorage__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! ./security/tokenStorage */ "yvMo");
/* harmony import */ var _pipes_absoluteUrl__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! ./pipes/absoluteUrl */ "WyvS");
/* harmony import */ var _angular_material_core__WEBPACK_IMPORTED_MODULE_19__ = __webpack_require__(/*! @angular/material/core */ "FKr1");









//import { AngularDateTimePickerModule } from "angular2-datetimepicker";















class CodeShellModule {
    static forRoot() {
        return {
            ngModule: CodeShellModule,
            providers: [
                _security__WEBPACK_IMPORTED_MODULE_15__["AuthFilter"],
                { provide: _localization__WEBPACK_IMPORTED_MODULE_14__["TranslationService"], useClass: _localization__WEBPACK_IMPORTED_MODULE_14__["NgxTranslationService"] },
                _security_authorizationServiceBase__WEBPACK_IMPORTED_MODULE_16__["AuthorizationServiceBase"],
                _security_tokenStorage__WEBPACK_IMPORTED_MODULE_17__["TokenStorage"]
            ]
        };
    }
}
CodeShellModule.ɵmod = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineNgModule"]({ type: CodeShellModule });
CodeShellModule.ɵinj = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineInjector"]({ factory: function CodeShellModule_Factory(t) { return new (t || CodeShellModule)(); }, imports: [[
            _angular_common__WEBPACK_IMPORTED_MODULE_1__["CommonModule"],
            _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormsModule"],
            _angular_forms__WEBPACK_IMPORTED_MODULE_2__["ReactiveFormsModule"],
            _angular_common_http__WEBPACK_IMPORTED_MODULE_5__["HttpClientModule"],
            _angular_router__WEBPACK_IMPORTED_MODULE_6__["RouterModule"],
            _ng_select_ng_select__WEBPACK_IMPORTED_MODULE_9__["NgSelectModule"],
            _angular_material_datepicker__WEBPACK_IMPORTED_MODULE_3__["MatDatepickerModule"],
            _angular_material_core__WEBPACK_IMPORTED_MODULE_19__["MatNativeDateModule"],
            _angular_material_moment_adapter__WEBPACK_IMPORTED_MODULE_4__["MatMomentDateModule"],
            ngx_quill__WEBPACK_IMPORTED_MODULE_8__["QuillModule"].forRoot(),
            //AngularDateTimePickerModule,
            angular_tree_component__WEBPACK_IMPORTED_MODULE_10__["TreeModule"].forRoot(),
            _ngx_translate_core__WEBPACK_IMPORTED_MODULE_7__["TranslateModule"].forRoot({ loader: { provide: _ngx_translate_core__WEBPACK_IMPORTED_MODULE_7__["TranslateLoader"], useClass: _localization__WEBPACK_IMPORTED_MODULE_14__["Translator"] } }),
        ], _angular_common__WEBPACK_IMPORTED_MODULE_1__["CommonModule"],
        _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormsModule"],
        _angular_common_http__WEBPACK_IMPORTED_MODULE_5__["HttpClientModule"],
        _angular_router__WEBPACK_IMPORTED_MODULE_6__["RouterModule"],
        _ng_select_ng_select__WEBPACK_IMPORTED_MODULE_9__["NgSelectModule"],
        _angular_material_datepicker__WEBPACK_IMPORTED_MODULE_3__["MatDatepickerModule"],
        _angular_material_core__WEBPACK_IMPORTED_MODULE_19__["MatNativeDateModule"],
        _angular_material_moment_adapter__WEBPACK_IMPORTED_MODULE_4__["MatMomentDateModule"],
        //AngularDateTimePickerModule,
        angular_tree_component__WEBPACK_IMPORTED_MODULE_10__["TreeModule"],
        _ngx_translate_core__WEBPACK_IMPORTED_MODULE_7__["TranslateModule"],
        ngx_quill__WEBPACK_IMPORTED_MODULE_8__["QuillModule"]] });
(function () { (typeof ngJitMode === "undefined" || ngJitMode) && _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵsetNgModuleScope"](CodeShellModule, { declarations: [_directives__WEBPACK_IMPORTED_MODULE_11__["BsFormGroup"], _directives__WEBPACK_IMPORTED_MODULE_11__["ShowIf"], _directives__WEBPACK_IMPORTED_MODULE_11__["OnEnter"], _directives__WEBPACK_IMPORTED_MODULE_11__["SlimScroll"],
        _directives__WEBPACK_IMPORTED_MODULE_11__["ComponentLoader"], _directives__WEBPACK_IMPORTED_MODULE_11__["ImagePreLoad"],
        _directives__WEBPACK_IMPORTED_MODULE_11__["ListItemWatcher"], _directives__WEBPACK_IMPORTED_MODULE_11__["Editable"], _directives__WEBPACK_IMPORTED_MODULE_11__["Radio"],
        _validators__WEBPACK_IMPORTED_MODULE_12__["NumberRangeValidator"], _validators__WEBPACK_IMPORTED_MODULE_12__["IsUnique"], _validators__WEBPACK_IMPORTED_MODULE_12__["ModalValidator"],
        _components__WEBPACK_IMPORTED_MODULE_13__["Paginate"], _components__WEBPACK_IMPORTED_MODULE_13__["SearchGroup"], _directives__WEBPACK_IMPORTED_MODULE_11__["DirctionFix"],
        _directives__WEBPACK_IMPORTED_MODULE_11__["FixDate"], _directives__WEBPACK_IMPORTED_MODULE_11__["FixDateTime"], _validators__WEBPACK_IMPORTED_MODULE_12__["DateValidator"],
        _directives__WEBPACK_IMPORTED_MODULE_11__["Selectable"], _components__WEBPACK_IMPORTED_MODULE_13__["DurationInput"], _directives__WEBPACK_IMPORTED_MODULE_11__["FileUploader"],
        _pipes_absoluteUrl__WEBPACK_IMPORTED_MODULE_18__["AbsoluteUrl"]], imports: [_angular_common__WEBPACK_IMPORTED_MODULE_1__["CommonModule"],
        _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormsModule"],
        _angular_forms__WEBPACK_IMPORTED_MODULE_2__["ReactiveFormsModule"],
        _angular_common_http__WEBPACK_IMPORTED_MODULE_5__["HttpClientModule"],
        _angular_router__WEBPACK_IMPORTED_MODULE_6__["RouterModule"],
        _ng_select_ng_select__WEBPACK_IMPORTED_MODULE_9__["NgSelectModule"],
        _angular_material_datepicker__WEBPACK_IMPORTED_MODULE_3__["MatDatepickerModule"],
        _angular_material_core__WEBPACK_IMPORTED_MODULE_19__["MatNativeDateModule"],
        _angular_material_moment_adapter__WEBPACK_IMPORTED_MODULE_4__["MatMomentDateModule"], ngx_quill__WEBPACK_IMPORTED_MODULE_8__["QuillModule"], angular_tree_component__WEBPACK_IMPORTED_MODULE_10__["TreeModule"], _ngx_translate_core__WEBPACK_IMPORTED_MODULE_7__["TranslateModule"]], exports: [_angular_common__WEBPACK_IMPORTED_MODULE_1__["CommonModule"],
        _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormsModule"],
        _angular_common_http__WEBPACK_IMPORTED_MODULE_5__["HttpClientModule"],
        _angular_router__WEBPACK_IMPORTED_MODULE_6__["RouterModule"],
        _ng_select_ng_select__WEBPACK_IMPORTED_MODULE_9__["NgSelectModule"],
        _angular_material_datepicker__WEBPACK_IMPORTED_MODULE_3__["MatDatepickerModule"],
        _angular_material_core__WEBPACK_IMPORTED_MODULE_19__["MatNativeDateModule"],
        _angular_material_moment_adapter__WEBPACK_IMPORTED_MODULE_4__["MatMomentDateModule"],
        //AngularDateTimePickerModule,
        angular_tree_component__WEBPACK_IMPORTED_MODULE_10__["TreeModule"],
        _ngx_translate_core__WEBPACK_IMPORTED_MODULE_7__["TranslateModule"],
        ngx_quill__WEBPACK_IMPORTED_MODULE_8__["QuillModule"],
        _directives__WEBPACK_IMPORTED_MODULE_11__["FixDate"], _directives__WEBPACK_IMPORTED_MODULE_11__["FixDateTime"], _validators__WEBPACK_IMPORTED_MODULE_12__["DateValidator"],
        _directives__WEBPACK_IMPORTED_MODULE_11__["BsFormGroup"], _directives__WEBPACK_IMPORTED_MODULE_11__["ShowIf"], _directives__WEBPACK_IMPORTED_MODULE_11__["OnEnter"], _directives__WEBPACK_IMPORTED_MODULE_11__["SlimScroll"],
        _directives__WEBPACK_IMPORTED_MODULE_11__["ComponentLoader"], _directives__WEBPACK_IMPORTED_MODULE_11__["ImagePreLoad"],
        _directives__WEBPACK_IMPORTED_MODULE_11__["ListItemWatcher"], _directives__WEBPACK_IMPORTED_MODULE_11__["Editable"], _directives__WEBPACK_IMPORTED_MODULE_11__["Radio"],
        _validators__WEBPACK_IMPORTED_MODULE_12__["NumberRangeValidator"], _validators__WEBPACK_IMPORTED_MODULE_12__["IsUnique"], _validators__WEBPACK_IMPORTED_MODULE_12__["ModalValidator"],
        _components__WEBPACK_IMPORTED_MODULE_13__["Paginate"], _components__WEBPACK_IMPORTED_MODULE_13__["SearchGroup"], _directives__WEBPACK_IMPORTED_MODULE_11__["DirctionFix"],
        _directives__WEBPACK_IMPORTED_MODULE_11__["Selectable"], _components__WEBPACK_IMPORTED_MODULE_13__["DurationInput"], _directives__WEBPACK_IMPORTED_MODULE_11__["FileUploader"],
        _pipes_absoluteUrl__WEBPACK_IMPORTED_MODULE_18__["AbsoluteUrl"]] }); })();
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](CodeShellModule, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"],
        args: [{
                imports: [
                    _angular_common__WEBPACK_IMPORTED_MODULE_1__["CommonModule"],
                    _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormsModule"],
                    _angular_forms__WEBPACK_IMPORTED_MODULE_2__["ReactiveFormsModule"],
                    _angular_common_http__WEBPACK_IMPORTED_MODULE_5__["HttpClientModule"],
                    _angular_router__WEBPACK_IMPORTED_MODULE_6__["RouterModule"],
                    _ng_select_ng_select__WEBPACK_IMPORTED_MODULE_9__["NgSelectModule"],
                    _angular_material_datepicker__WEBPACK_IMPORTED_MODULE_3__["MatDatepickerModule"],
                    _angular_material_core__WEBPACK_IMPORTED_MODULE_19__["MatNativeDateModule"],
                    _angular_material_moment_adapter__WEBPACK_IMPORTED_MODULE_4__["MatMomentDateModule"],
                    ngx_quill__WEBPACK_IMPORTED_MODULE_8__["QuillModule"].forRoot(),
                    //AngularDateTimePickerModule,
                    angular_tree_component__WEBPACK_IMPORTED_MODULE_10__["TreeModule"].forRoot(),
                    _ngx_translate_core__WEBPACK_IMPORTED_MODULE_7__["TranslateModule"].forRoot({ loader: { provide: _ngx_translate_core__WEBPACK_IMPORTED_MODULE_7__["TranslateLoader"], useClass: _localization__WEBPACK_IMPORTED_MODULE_14__["Translator"] } }),
                ],
                declarations: [
                    _directives__WEBPACK_IMPORTED_MODULE_11__["BsFormGroup"], _directives__WEBPACK_IMPORTED_MODULE_11__["ShowIf"], _directives__WEBPACK_IMPORTED_MODULE_11__["OnEnter"], _directives__WEBPACK_IMPORTED_MODULE_11__["SlimScroll"],
                    _directives__WEBPACK_IMPORTED_MODULE_11__["ComponentLoader"], _directives__WEBPACK_IMPORTED_MODULE_11__["ImagePreLoad"],
                    _directives__WEBPACK_IMPORTED_MODULE_11__["ListItemWatcher"], _directives__WEBPACK_IMPORTED_MODULE_11__["Editable"], _directives__WEBPACK_IMPORTED_MODULE_11__["Radio"],
                    _validators__WEBPACK_IMPORTED_MODULE_12__["NumberRangeValidator"], _validators__WEBPACK_IMPORTED_MODULE_12__["IsUnique"], _validators__WEBPACK_IMPORTED_MODULE_12__["ModalValidator"],
                    _components__WEBPACK_IMPORTED_MODULE_13__["Paginate"], _components__WEBPACK_IMPORTED_MODULE_13__["SearchGroup"], _directives__WEBPACK_IMPORTED_MODULE_11__["DirctionFix"],
                    _directives__WEBPACK_IMPORTED_MODULE_11__["FixDate"], _directives__WEBPACK_IMPORTED_MODULE_11__["FixDateTime"], _validators__WEBPACK_IMPORTED_MODULE_12__["DateValidator"],
                    _directives__WEBPACK_IMPORTED_MODULE_11__["Selectable"], _components__WEBPACK_IMPORTED_MODULE_13__["DurationInput"], _directives__WEBPACK_IMPORTED_MODULE_11__["FileUploader"],
                    _pipes_absoluteUrl__WEBPACK_IMPORTED_MODULE_18__["AbsoluteUrl"]
                ],
                exports: [
                    _angular_common__WEBPACK_IMPORTED_MODULE_1__["CommonModule"],
                    _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormsModule"],
                    _angular_common_http__WEBPACK_IMPORTED_MODULE_5__["HttpClientModule"],
                    _angular_router__WEBPACK_IMPORTED_MODULE_6__["RouterModule"],
                    _ng_select_ng_select__WEBPACK_IMPORTED_MODULE_9__["NgSelectModule"],
                    _angular_material_datepicker__WEBPACK_IMPORTED_MODULE_3__["MatDatepickerModule"],
                    _angular_material_core__WEBPACK_IMPORTED_MODULE_19__["MatNativeDateModule"],
                    _angular_material_moment_adapter__WEBPACK_IMPORTED_MODULE_4__["MatMomentDateModule"],
                    //AngularDateTimePickerModule,
                    angular_tree_component__WEBPACK_IMPORTED_MODULE_10__["TreeModule"],
                    _ngx_translate_core__WEBPACK_IMPORTED_MODULE_7__["TranslateModule"],
                    ngx_quill__WEBPACK_IMPORTED_MODULE_8__["QuillModule"],
                    _directives__WEBPACK_IMPORTED_MODULE_11__["FixDate"], _directives__WEBPACK_IMPORTED_MODULE_11__["FixDateTime"], _validators__WEBPACK_IMPORTED_MODULE_12__["DateValidator"],
                    _directives__WEBPACK_IMPORTED_MODULE_11__["BsFormGroup"], _directives__WEBPACK_IMPORTED_MODULE_11__["ShowIf"], _directives__WEBPACK_IMPORTED_MODULE_11__["OnEnter"], _directives__WEBPACK_IMPORTED_MODULE_11__["SlimScroll"],
                    _directives__WEBPACK_IMPORTED_MODULE_11__["ComponentLoader"], _directives__WEBPACK_IMPORTED_MODULE_11__["ImagePreLoad"],
                    _directives__WEBPACK_IMPORTED_MODULE_11__["ListItemWatcher"], _directives__WEBPACK_IMPORTED_MODULE_11__["Editable"], _directives__WEBPACK_IMPORTED_MODULE_11__["Radio"],
                    _validators__WEBPACK_IMPORTED_MODULE_12__["NumberRangeValidator"], _validators__WEBPACK_IMPORTED_MODULE_12__["IsUnique"], _validators__WEBPACK_IMPORTED_MODULE_12__["ModalValidator"],
                    _components__WEBPACK_IMPORTED_MODULE_13__["Paginate"], _components__WEBPACK_IMPORTED_MODULE_13__["SearchGroup"], _directives__WEBPACK_IMPORTED_MODULE_11__["DirctionFix"],
                    _directives__WEBPACK_IMPORTED_MODULE_11__["Selectable"], _components__WEBPACK_IMPORTED_MODULE_13__["DurationInput"], _directives__WEBPACK_IMPORTED_MODULE_11__["FileUploader"],
                    _pipes_absoluteUrl__WEBPACK_IMPORTED_MODULE_18__["AbsoluteUrl"]
                ],
            }]
    }], null, null); })();


/***/ }),

/***/ "bWkv":
/*!************************************************************!*\
  !*** ./src/core/codeshell/directives/show-if.directive.ts ***!
  \************************************************************/
/*! exports provided: ShowIf, Editable */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ShowIf", function() { return ShowIf; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Editable", function() { return Editable; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common */ "ofXK");



class ShowIf {
    constructor(el) {
        this.el = el;
        this.Condition = true;
    }
    get Style() { return this.el.nativeElement.style; }
    ngOnInit() {
        if (!this.Condition) {
            this.Style.display = "none";
        }
    }
    ngOnChanges() {
        if (!this.Condition) {
            this.Style.display = "none";
        }
        else {
            this.Style.removeProperty("display");
        }
    }
}
ShowIf.ɵfac = function ShowIf_Factory(t) { return new (t || ShowIf)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"])); };
ShowIf.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({ type: ShowIf, selectors: [["", "show-if", ""]], inputs: { Condition: ["show-if", "Condition"] }, features: [_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵNgOnChangesFeature"]] });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](ShowIf, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
        args: [{ selector: '[show-if]' }]
    }], function () { return [{ type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"] }]; }, { Condition: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["show-if"]
        }] }); })();
class Editable {
    constructor(ngFor) {
        this.ngFor = ngFor;
    }
    ngOnInit() {
    }
}
Editable.ɵfac = function Editable_Factory(t) { return new (t || Editable)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_common__WEBPACK_IMPORTED_MODULE_1__["NgForOf"])); };
Editable.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({ type: Editable, selectors: [["", "ngFor", "", "ngForEditable", ""]], inputs: { Ed: ["ngForEditable", "Ed"] }, exportAs: ["editable"] });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](Editable, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
        args: [{ selector: '[ngFor][ngForEditable]', exportAs: "editable" }]
    }], function () { return [{ type: _angular_common__WEBPACK_IMPORTED_MODULE_1__["NgForOf"] }]; }, { Ed: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["ngForEditable"]
        }] }); })();


/***/ }),

/***/ "cPZL":
/*!********************************************************!*\
  !*** ./src/core/codeshell/validators/validatorBase.ts ***!
  \********************************************************/
/*! exports provided: ValidatorBase */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ValidatorBase", function() { return ValidatorBase; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "3Pt+");



class ValidatorBase {
    constructor(el, model) {
        this.el = el;
        this.Model = model;
        this.Element = el.nativeElement;
    }
    valueChanged(ev) {
        this.runValidation();
    }
    ngOnChanges(ch) {
        if (this.RunIf(ch)) {
            this.runValidation();
        }
    }
    RunIf(ch) {
        return false;
    }
    runValidation() {
        var err = {};
        if (this.IsValid()) {
            err[this.Identifier] = null;
            this.Model.control.updateValueAndValidity();
        }
        else {
            err[this.Identifier] = true;
            this.Model.control.setErrors(err);
        }
    }
}
ValidatorBase.ɵfac = function ValidatorBase_Factory(t) { return new (t || ValidatorBase)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵinject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]), _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵinject"](_angular_forms__WEBPACK_IMPORTED_MODULE_1__["NgModel"])); };
ValidatorBase.ɵprov = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineInjectable"]({ token: ValidatorBase, factory: ValidatorBase.ɵfac });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](ValidatorBase, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"]
    }], function () { return [{ type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"] }, { type: _angular_forms__WEBPACK_IMPORTED_MODULE_1__["NgModel"] }]; }, { valueChanged: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"],
            args: ["blur", ["$event"]]
        }] }); })();


/***/ }),

/***/ "dpcW":
/*!**********************************************!*\
  !*** ./src/core/codeshell/helpers/models.ts ***!
  \**********************************************/
/*! exports provided: DTO, TmpFileData, Result, SubmitResult, DeleteResult, LoadResult, LoadResultGen, PropertyFilter, LoadOptions, NoteType */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DTO", function() { return DTO; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "TmpFileData", function() { return TmpFileData; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Result", function() { return Result; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SubmitResult", function() { return SubmitResult; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DeleteResult", function() { return DeleteResult; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "LoadResult", function() { return LoadResult; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "LoadResultGen", function() { return LoadResultGen; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "PropertyFilter", function() { return PropertyFilter; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "LoadOptions", function() { return LoadOptions; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "NoteType", function() { return NoteType; });
class DTO {
    constructor() {
        this.entity = {};
    }
}
class TmpFileData {
}
class Result {
    constructor() {
        this.success = true;
        this.type = NoteType.Success;
    }
}
class SubmitResult {
    constructor() {
        this.data = {};
        this.code = 0;
        this.message = "";
        this.exceptionMessage = "";
        this.stackTrace = [];
        this.affectedRows = 0;
    }
    static FromResponse(r) {
        try {
            return Object.assign(new SubmitResult, r.error);
        }
        catch (e) {
            return null;
        }
    }
}
class DeleteResult extends SubmitResult {
    constructor() {
        super(...arguments);
        this.canDelete = false;
        this.tableName = null;
    }
}
class LoadResult {
    constructor() {
        this.totalCount = 0;
        this.list = [];
    }
}
class LoadResultGen {
    constructor() {
        this.list = [];
    }
}
class PropertyFilter {
    constructor() {
        this.MemberName = "";
        this.FilterType = "reference";
        this.Ids = [];
    }
}
class LoadOptions {
    constructor() {
        this.Showing = 0;
        this.Skip = 0;
    }
    static AddFilter(opts, fil) {
        let arr;
        if (!opts.Filters) {
            opts.Filters = "";
            arr = [];
        }
        else {
            let f = JSON.parse(opts.Filters);
            arr = Object.assign(new Array(), f);
        }
        arr.push(fil);
        opts.Filters = JSON.stringify(arr);
    }
}
var NoteType;
(function (NoteType) {
    NoteType[NoteType["Success"] = 0] = "Success";
    NoteType[NoteType["Error"] = 1] = "Error";
    NoteType[NoteType["Warning"] = 2] = "Warning";
})(NoteType || (NoteType = {}));


/***/ }),

/***/ "eXOA":
/*!************************************************!*\
  !*** ./src/core/codeshell/validators/index.ts ***!
  \************************************************/
/*! exports provided: NumberRangeValidator, IsUnique, ModalValidator, DateValidator */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _rangeValidator__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./rangeValidator */ "9loO");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "NumberRangeValidator", function() { return _rangeValidator__WEBPACK_IMPORTED_MODULE_0__["NumberRangeValidator"]; });

/* harmony import */ var _isUnique__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./isUnique */ "PwuP");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "IsUnique", function() { return _isUnique__WEBPACK_IMPORTED_MODULE_1__["IsUnique"]; });

/* harmony import */ var _modalValidator__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./modalValidator */ "62p0");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "ModalValidator", function() { return _modalValidator__WEBPACK_IMPORTED_MODULE_2__["ModalValidator"]; });

/* harmony import */ var _dateValidator__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./dateValidator */ "zYQP");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "DateValidator", function() { return _dateValidator__WEBPACK_IMPORTED_MODULE_3__["DateValidator"]; });







/***/ }),

/***/ "fYjR":
/*!******************************************************!*\
  !*** ./src/core/codeshell/helpers/recursionModel.ts ***!
  \******************************************************/
/*! exports provided: RecursionModel */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "RecursionModel", function() { return RecursionModel; });
class RecursionModel {
    constructor() {
        this.id = 0;
        this.name = "";
        this.children = [];
        this.editing = false;
        this.parentId = null;
        this.chain = "";
        this.nameChain = "";
        this.isExpanded = false;
    }
    static FromDB(item, lst) {
        let ret = Object.assign(new RecursionModel, item);
        if (ret.children.length > 0) {
            for (var i in ret.children)
                ret.children[i] = this.FromDB(ret.children[i]);
        }
        return ret;
    }
}


/***/ }),

/***/ "gFuU":
/*!***********************************************!*\
  !*** ./src/core/codeshell/security/models.ts ***!
  \***********************************************/
/*! exports provided: ResourceActions, DomainDataProvider, DomainData, RouteData, UserDTO, LoginResult, TokenData, Permission */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ResourceActions", function() { return ResourceActions; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DomainDataProvider", function() { return DomainDataProvider; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DomainData", function() { return DomainData; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "RouteData", function() { return RouteData; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "UserDTO", function() { return UserDTO; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "LoginResult", function() { return LoginResult; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "TokenData", function() { return TokenData; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Permission", function() { return Permission; });
var ResourceActions;
(function (ResourceActions) {
    ResourceActions[ResourceActions["view"] = 0] = "view";
    ResourceActions[ResourceActions["details"] = 1] = "details";
    ResourceActions[ResourceActions["update"] = 2] = "update";
    ResourceActions[ResourceActions["insert"] = 3] = "insert";
    ResourceActions[ResourceActions["delete"] = 4] = "delete";
})(ResourceActions || (ResourceActions = {}));
class DomainDataProvider {
    constructor(domains) {
        this.Domains = [];
        this.Domains = domains;
    }
    GetByNavGroup(group, auth, user) {
        //var auth: AuthorizationServiceBase = Shell.Injector.get(AuthorizationServiceBase);
        var navs = [];
        var gr = this.Domains.find(e => e.name == group);
        if (gr) {
            for (var c of gr.children) {
                var r = Object.assign(new RouteData, c);
                var isAuthorized = auth ? auth.IsAuthorized(user, r) : true;
                if (isAuthorized && r.url) {
                    var item = {
                        name: r.name,
                        url: r.url
                    };
                    navs.push(item);
                }
            }
        }
        return navs;
    }
}
class DomainData {
    constructor() {
        this.name = "";
        this.children = [];
    }
}
class RouteData {
    constructor() {
        this.name = "";
        this.navigate = false;
        this.resource = "";
        this.action = ResourceActions.view;
    }
    get IsAnonymous() { return this.action == "anonymous"; }
    get AllowAll() { return this.action == "allowAll"; }
}
class UserDTO {
    constructor() {
        this.id = 0;
        this.userId = "";
        this.tenantCode = "";
        this.name = "";
        this.logonName = "";
        this.userTypeString = "";
        this.apps = [];
        this.permissions = {};
        this.entityLinks = {};
    }
}
class LoginResult {
    constructor() {
        this.success = false;
        this.message = "";
        this.userData = new UserDTO;
        this.token = "";
        this.tokenExpiry = new Date;
    }
}
class TokenData {
    constructor() {
        this.Token = "";
        this.Expiry = new Date;
    }
    IsExpired() {
        return new Date() > new Date(this.Expiry);
    }
}
class Permission {
    constructor() {
        this.insert = false;
        this.update = false;
        this.delete = false;
        this.details = false;
        this.view = true;
        this.actions = [];
    }
    static get Anonymous() {
        return Object.assign(new Permission, {
            insert: true,
            update: false,
            delete: false,
            details: false,
            view: true
        });
    }
    static get Denied() {
        return Object.assign(new Permission, {
            insert: false,
            update: false,
            delete: false,
            details: false,
            view: false
        });
    }
    static get FullAccess() {
        return Object.assign(new Permission, {
            insert: true,
            update: true,
            delete: true,
            details: true,
            view: true
        });
    }
    get canSubmit() { return this.insert || this.update || this.delete; }
    Can(ac) {
        if (ac == "anonymous" || ac == "allowAll")
            return true;
        switch (ac) {
            case 4:
                return this.delete;
            case 2:
                return this.update;
            case 3:
                return this.insert;
            case 1:
                return this.details;
            case 0:
                return this.view;
            default:
                if (this.actions == null)
                    return false;
                return this.actions.indexOf(ac) > -1;
        }
    }
}


/***/ }),

/***/ "gyvI":
/*!*****************************************************!*\
  !*** ./src/core/codeshell/base-components/index.ts ***!
  \*****************************************************/
/*! exports provided: IAppComponent, BaseComponent, DTOEditComponentBase, EditComponentBase, ListComponentBase, LoginBase, NavigationSideBarBase, SectionedEditComponentBase, SelectComponentBase, TopBarBase, ExpandedItems, TreeComponentBase */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _appComponentBase__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./appComponentBase */ "Q6pc");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "IAppComponent", function() { return _appComponentBase__WEBPACK_IMPORTED_MODULE_0__["IAppComponent"]; });

/* harmony import */ var _baseComponent__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./baseComponent */ "I5Ck");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "BaseComponent", function() { return _baseComponent__WEBPACK_IMPORTED_MODULE_1__["BaseComponent"]; });

/* harmony import */ var _dto_edit_component_base__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./dto-edit-component-base */ "Ur/x");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "DTOEditComponentBase", function() { return _dto_edit_component_base__WEBPACK_IMPORTED_MODULE_2__["DTOEditComponentBase"]; });

/* harmony import */ var _editComponentBase__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./editComponentBase */ "G778");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "EditComponentBase", function() { return _editComponentBase__WEBPACK_IMPORTED_MODULE_3__["EditComponentBase"]; });

/* harmony import */ var _listComponentBase__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./listComponentBase */ "sVHn");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "ListComponentBase", function() { return _listComponentBase__WEBPACK_IMPORTED_MODULE_4__["ListComponentBase"]; });

/* harmony import */ var _loginBase__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./loginBase */ "rCrt");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "LoginBase", function() { return _loginBase__WEBPACK_IMPORTED_MODULE_5__["LoginBase"]; });

/* harmony import */ var _navigation_side_bar_base__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./navigation-side-bar-base */ "+K43");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "NavigationSideBarBase", function() { return _navigation_side_bar_base__WEBPACK_IMPORTED_MODULE_6__["NavigationSideBarBase"]; });

/* harmony import */ var _sectionedEditComponentBase__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./sectionedEditComponentBase */ "u9rM");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "SectionedEditComponentBase", function() { return _sectionedEditComponentBase__WEBPACK_IMPORTED_MODULE_7__["SectionedEditComponentBase"]; });

/* harmony import */ var _selectComponentBase__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./selectComponentBase */ "6mC2");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "SelectComponentBase", function() { return _selectComponentBase__WEBPACK_IMPORTED_MODULE_8__["SelectComponentBase"]; });

/* harmony import */ var _topBarBase__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ./topBarBase */ "VZL9");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "TopBarBase", function() { return _topBarBase__WEBPACK_IMPORTED_MODULE_9__["TopBarBase"]; });

/* harmony import */ var _treeComponentBase__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ./treeComponentBase */ "n73x");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "ExpandedItems", function() { return _treeComponentBase__WEBPACK_IMPORTED_MODULE_10__["ExpandedItems"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "TreeComponentBase", function() { return _treeComponentBase__WEBPACK_IMPORTED_MODULE_10__["TreeComponentBase"]; });














/***/ }),

/***/ "iLnC":
/*!******************************************************!*\
  !*** ./src/core/codeshell/http/entityHttpService.ts ***!
  \******************************************************/
/*! exports provided: EntityHttpService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EntityHttpService", function() { return EntityHttpService; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "mrSG");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var _httpRequest__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./httpRequest */ "vyk5");
/* harmony import */ var _httpServiceBase__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./httpServiceBase */ "LR6F");
/* harmony import */ var _helpers__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../helpers */ "GYDu");






class EntityHttpService extends _httpServiceBase__WEBPACK_IMPORTED_MODULE_3__["HttpServiceBase"] {
    IsUnique(property, id, value) {
        let req = this.InitializeRequest("IsUnique", { Property: property, Id: (id ? id : 0), Value: value });
        return this.processAs(_httpRequest__WEBPACK_IMPORTED_MODULE_2__["Methods"].Get, req);
    }
    GetEditLookups(opt) {
        return this.Get("edit-lookups", opt);
    }
    GetListLookups(opt) {
        return this.Get("list-lookups", opt);
    }
    SetActive(id, state) {
        return this.Update("SetActive", { id: id, stateBool: state });
    }
    GetSingle(id) {
        return this.GetAs(id.toString());
    }
    GetPaged(action, opts) {
        let req = this.InitializeRequest(action, opts);
        return this.processAs(_httpRequest__WEBPACK_IMPORTED_MODULE_2__["Methods"].Get, req);
    }
    Save(action, body, params) {
        let req = this.InitializeRequest(action, params, body);
        return this.processAs(_httpRequest__WEBPACK_IMPORTED_MODULE_2__["Methods"].Post, req);
    }
    Update(action, body, params) {
        let req = this.InitializeRequest(action, params, body);
        return this.processAs(_httpRequest__WEBPACK_IMPORTED_MODULE_2__["Methods"].Put, req);
    }
    AttemptDelete(id) {
        let req = this.InitializeRequest("Delete", id);
        return this.processAs(_httpRequest__WEBPACK_IMPORTED_MODULE_2__["Methods"].Delete, req);
    }
    GetLocalizationData(id) {
        return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, function* () {
            var data = yield this.GetAs("GetLocalizationData/" + id);
            for (var i in data)
                data[i] = _helpers__WEBPACK_IMPORTED_MODULE_4__["ListItem"].FromDB_GEN(_helpers__WEBPACK_IMPORTED_MODULE_4__["LocalizablesDTO"], data[i]);
            return data;
        });
    }
    SetLocalizationData(id, data) {
        return this.PostAs("SetLocalizationData/" + id, data);
    }
}
EntityHttpService.ɵfac = function EntityHttpService_Factory(t) { return ɵEntityHttpService_BaseFactory(t || EntityHttpService); };
EntityHttpService.ɵprov = _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdefineInjectable"]({ token: EntityHttpService, factory: EntityHttpService.ɵfac });
const ɵEntityHttpService_BaseFactory = /*@__PURE__*/ _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵgetInheritedFactory"](EntityHttpService);
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵsetClassMetadata"](EntityHttpService, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"]
    }], null, null); })();


/***/ }),

/***/ "j3l2":
/*!*********************************************************************!*\
  !*** ./src/core/codeshell/directives/component-loader.directive.ts ***!
  \*********************************************************************/
/*! exports provided: ComponentLoader */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ComponentLoader", function() { return ComponentLoader; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");


class ComponentLoader {
    constructor(Container, inj, FactoryResolver) {
        this.Container = Container;
        this.inj = inj;
        this.FactoryResolver = FactoryResolver;
    }
    CreateComponent(e) {
        var fac = this.FactoryResolver.resolveComponentFactory(e);
        let ref = fac.create(this.inj);
        return ref;
    }
    UseComponent(ref) {
        if (this.Container.indexOf(ref.hostView) == -1) {
            this.Container.insert(ref.hostView);
        }
    }
    Clear() {
        this.Container.clear();
    }
}
ComponentLoader.ɵfac = function ComponentLoader_Factory(t) { return new (t || ComponentLoader)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ViewContainerRef"]), _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injector"]), _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ComponentFactoryResolver"])); };
ComponentLoader.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({ type: ComponentLoader, selectors: [["ng-template", "acs-component-loader", ""]], exportAs: ["componentLoader"] });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](ComponentLoader, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
        args: [{ selector: "ng-template[acs-component-loader]", exportAs: "componentLoader" }]
    }], function () { return [{ type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ViewContainerRef"] }, { type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Injector"] }, { type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ComponentFactoryResolver"] }]; }, null); })();


/***/ }),

/***/ "jBBj":
/*!**************************************************!*\
  !*** ./src/core/codeshell/helpers/viewParams.ts ***!
  \**************************************************/
/*! exports provided: ViewParams */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ViewParams", function() { return ViewParams; });
class ViewParams {
    constructor() {
        this.Other = {};
    }
}


/***/ }),

/***/ "mHXC":
/*!***********************************************!*\
  !*** ./src/core/codeshell/services/stored.ts ***!
  \***********************************************/
/*! exports provided: Stored */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Stored", function() { return Stored; });
class Stored {
    static Set(index, item) {
        let s = JSON.stringify(item);
        localStorage.setItem(index, s);
    }
    static Set_SS(index, item) {
        let s = JSON.stringify(item);
        sessionStorage.setItem(index, s);
    }
    static Clear(index) {
        localStorage.removeItem(index);
    }
    static Clear_SS(index) {
        sessionStorage.removeItem(index);
    }
    static Get(index, exp) {
        var item = localStorage.getItem(index);
        if (item == undefined || item == null)
            return null;
        try {
            let d = new exp;
            let ob = JSON.parse(item);
            Object.assign(d, ob);
            return d;
        }
        catch (e) {
            return null;
        }
    }
    static Get_SS(index, exp) {
        var item = sessionStorage.getItem(index);
        if (item == undefined || item == null)
            return null;
        try {
            let d = new exp;
            let ob = JSON.parse(item);
            Object.assign(d, ob);
            return d;
        }
        catch (e) {
            return null;
        }
    }
}


/***/ }),

/***/ "n2jX":
/*!******************************************************************!*\
  !*** ./src/core/codeshell/directives/direction-fix.directive.ts ***!
  \******************************************************************/
/*! exports provided: DirctionFix */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DirctionFix", function() { return DirctionFix; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");


class DirctionFix {
    constructor(el) {
        this.el = el;
    }
    get Element() { return this.el.nativeElement; }
    Change() {
    }
    ngOnInit() {
        setTimeout(() => {
            this.setDirection();
        }, 500);
    }
    setDirection() {
        var arabic = /[\u0600-\u06FF]/;
        this.Element.style.display = "inline-block";
        if (arabic.test(this.Element.innerText)) {
            this.Element.style.direction = "rtl";
        }
        else {
            this.Element.style.direction = "ltr";
        }
    }
}
DirctionFix.ɵfac = function DirctionFix_Factory(t) { return new (t || DirctionFix)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"])); };
DirctionFix.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({ type: DirctionFix, selectors: [["", "rtl-fix", ""]], hostBindings: function DirctionFix_HostBindings(rf, ctx) { if (rf & 1) {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("textContent", function DirctionFix_textContent_HostBindingHandler() { return ctx.Change(); });
    } }, exportAs: ["rtl-fix"] });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](DirctionFix, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
        args: [{ selector: "[rtl-fix]", exportAs: "rtl-fix" }]
    }], function () { return [{ type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"] }]; }, { Change: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"],
            args: ["textContent"]
        }] }); })();


/***/ }),

/***/ "n73x":
/*!*****************************************************************!*\
  !*** ./src/core/codeshell/base-components/treeComponentBase.ts ***!
  \*****************************************************************/
/*! exports provided: ExpandedItems, TreeComponentBase */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ExpandedItems", function() { return ExpandedItems; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "TreeComponentBase", function() { return TreeComponentBase; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "mrSG");
/* harmony import */ var _baseComponent__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./baseComponent */ "I5Ck");
/* harmony import */ var codeshell_helpers__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! codeshell/helpers */ "GYDu");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var angular_tree_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! angular-tree-component */ "rDsv");
/* harmony import */ var _shell__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../shell */ "UoIT");
/* harmony import */ var _utilities_utils__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ../utilities/utils */ "VXkE");
/* harmony import */ var _helpers_treeEventArgs__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ../helpers/treeEventArgs */ "6XeC");










class ExpandedItems {
    constructor() {
        this.Items = [];
    }
}
class TreeComponentBase extends _baseComponent__WEBPACK_IMPORTED_MODULE_1__["BaseComponent"] {
    constructor() {
        super(...arguments);
        this.LoadOnStart = true;
        this.RouteParams = {};
        this.ignore = false;
        this._loadedOnce = false;
        this.model = {};
        this.List = [];
        this.IsNew = true;
        this.AllowEdit = true;
        this.AllowMove = true;
        this.ShowOkButton = true;
        this.UseContentCounts = false;
        this.UseInTreeTextBoxes = true;
        this.selectedId = 0;
        this.OnTreeEvent = new _angular_core__WEBPACK_IMPORTED_MODULE_3__["EventEmitter"]();
        this.OnUnassigned = new _angular_core__WEBPACK_IMPORTED_MODULE_3__["EventEmitter"]();
        this.OnTreeLoadedEvet = new _angular_core__WEBPACK_IMPORTED_MODULE_3__["EventEmitter"]();
        this.treeOptions = {
            allowDrag: (node) => {
                return this.AllowEdit && this.AllowMove;
            },
            allowDrop: (node) => {
                return this.AllowEdit && this.AllowMove;
            },
        };
        this.OnTreeLoaded = () => { };
    }
    get UseCheckBoxes() { return this.treeOptions.useCheckbox == true; }
    set UseCheckBoxes(val) { this.treeOptions.useCheckbox = val; }
    get Expanded() {
        if (!this._expanded) {
            var x = codeshell_helpers__WEBPACK_IMPORTED_MODULE_2__["Stored"].Get(this.ComponentName + "_Expanded", ExpandedItems);
            if (x == null)
                this._expanded = [];
            else
                this._expanded = x.Items;
        }
        return this._expanded;
    }
    ngOnInit() {
        super.ngOnInit();
        let opts = this.GetLookupOptions();
        if (this.treeComponent)
            this.treeComponent.event.subscribe((e) => this.OnEvent(e));
        if (opts != null) {
            this.LoadLookupsAsync(opts).then(l => {
                this.Lookups = l;
                this.OnReady();
            });
        }
        else {
            this.OnReady();
        }
    }
    OnEvent(ev) {
        if (!ev.node)
            return;
        this.OnTreeEvent.emit(new _helpers_treeEventArgs__WEBPACK_IMPORTED_MODULE_7__["TreeEventArgs"](ev.eventName, ev.node));
        if (ev.eventName == "focus")
            this.setClickedRow(ev.node);
        if (this.UseCheckBoxes && ev.eventName == "select") {
            this.OnCheckBoxSelect(ev.node);
        }
        else if (this.UseCheckBoxes && ev.eventName == "deselect") {
            this.OnCheckBoxDeselect(ev.node);
        }
    }
    SetSelected(id) {
        if (this.treeComponent) {
            var n = this.treeComponent.treeModel.getNodeById(id);
            if (n) {
                n.focus();
            }
        }
    }
    ForceReload() {
        if (this._loadedOnce)
            this._loadedOnce = false;
    }
    LoadLookupsAsync(opts) {
        return this.Service.Get("GetListLookups", opts);
    }
    OnReady() {
        if (!this.IsEmbedded && this.LoadOnStart)
            this.LoadData();
    }
    StartAsync() {
        return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, function* () {
            if (!this._loadedOnce) {
                var lst = yield this.LoadDataPromise();
                this.AfterLoad(lst);
                this._loadedOnce = true;
            }
            yield this.ExpandSavedAsync();
            return this;
        });
    }
    OnCheckBoxSelect(node) { }
    OnCheckBoxDeselect(nods) { }
    OnSelected(item) { }
    LoadDataPromise() {
        this.List = [];
        if (this.Loader)
            return this.Loader();
        return this.Service.Get("tree");
    }
    ContentCountAsync() {
        if (this.CountLoader) {
            return this.CountLoader();
        }
        return Promise.resolve({});
    }
    ShowCheckBoxes() {
        this.treeOptions.useCheckbox = true;
    }
    ClearSelection() {
        this.selectedId = 0;
        this.selectedItem = undefined;
        if (this.selectedNode) {
            this.selectedNode.setIsSelected(false);
            this.selectedNode.setIsActive(false);
        }
        this.selectedNode = undefined;
        if (this.treeComponent) {
            this.treeComponent.treeModel.setActiveNode(null, null);
            this.treeComponent.treeModel.setFocusedNode(null);
            this.treeComponent.treeModel.update();
        }
        if (this.OnSelectedEvent)
            this.OnSelectedEvent(null);
    }
    AfterLoad(lst) {
        this.List = [];
        for (var i of lst)
            this.List.push(codeshell_helpers__WEBPACK_IMPORTED_MODULE_2__["RecursionModel"].FromDB(i));
        if (this.UseContentCounts)
            this.LoadCounts();
        if (this.OnLoadedEvent)
            this.OnLoadedEvent(this.List);
        if (this.treeComponent)
            this.treeComponent.treeModel.update();
        this.onTreeInit();
    }
    LoadData() {
        this.List = [];
        this.LoadDataPromise().then(e => {
            this.AfterLoad(e);
            this.ExpandSavedAsync();
        });
    }
    LoadCounts() {
        Object(_utilities_utils__WEBPACK_IMPORTED_MODULE_6__["List_RunRecursively"])(this.List, d => { d.contentCount = 0; d.hasContents = false; });
        this.ContentCountAsync().then(res => {
            for (var i in res) {
                var item = Object(_utilities_utils__WEBPACK_IMPORTED_MODULE_6__["List_FindIdRecusive"])(this.List, parseInt(i));
                if (item) {
                    item.hasContents = true;
                    item.contentCount = res[i];
                }
            }
        });
    }
    ExpandSavedAsync() {
        return new Promise((res, rej) => {
            setTimeout(() => {
                this.ignore = true;
                if (this.treeComponent) {
                    for (let id of this.Expanded) {
                        if (id == null)
                            continue;
                        let node = this.treeComponent.treeModel.getNodeById(id);
                        if (node)
                            node.expand();
                    }
                }
                this.ignore = false;
                res();
            }, 500);
        });
    }
    onTreeInit() {
        setTimeout(() => {
            this.OnTreeLoaded();
        }, 300);
    }
    onExpanded(event) {
        if (this.ignore)
            return;
        let item = event.node.data;
        if (event.isExpanded)
            this.Expanded.push(item.id);
        else {
            Object(_utilities_utils__WEBPACK_IMPORTED_MODULE_6__["List_RemoveItem"])(this.Expanded, item.id);
        }
        this.SaveExpanded();
    }
    setClickedRow(node) {
        let index = node.data;
        if (this.selectedId == index.id)
            return;
        this.selectedId = index.id;
        this.selectedNode = node;
        this.selectedItem = index;
        this.OnSelected(index);
        if (this.OnSelectedEvent)
            this.OnSelectedEvent(index);
    }
    OnDisplayNode() { }
    InitializeModel(parent) {
        return new codeshell_helpers__WEBPACK_IMPORTED_MODULE_2__["RecursionModel"]();
    }
    onMoveNode(ev) {
        let moved = ev.node;
        let loc = ev.to.parent;
        if (loc.virtual)
            moved.parentId = null;
        else
            moved.parentId = loc.id;
        var m = Object.assign({}, moved);
        m.children = [];
        this.Service.Put("Put", m).catch(e => this.LoadData());
    }
    AddWithModalAsync(parentNode) {
        return new Promise((resolve, reject) => {
            if (this.editingModalRequest) {
                var req = this.editingModalRequest;
                this.GetComponent(req).then(comp => {
                    var model = this.InitializeModel(parentNode);
                    if (parentNode)
                        model.parentId = parentNode.data.id;
                    comp.IsNew = true;
                    comp.BindAsync(model).then(d => {
                        comp.Show = true;
                    });
                    comp.DataSubmitted = (model, res) => {
                        if (comp.UseLocalization)
                            this.LoadData();
                        else
                            resolve({ model: model, result: res });
                        comp.Show = false;
                    };
                }).catch(e => reject(e));
            }
            else {
                reject("no Editing Component");
            }
        });
    }
    EditWithModalAsync(node) {
        return new Promise((resolve, reject) => {
            if (this.editingModalRequest) {
                var req = this.editingModalRequest;
                this.GetComponent(req).then(comp => {
                    var model = node.data;
                    if (node)
                        model.parentId = node.data.id;
                    comp.FillAsync(model.id).then(d => {
                        comp.Show = true;
                    });
                    comp.DataSubmitted = (model, res) => {
                        if (comp.UseLocalization)
                            this.LoadData();
                        else
                            resolve({ model: model, result: res });
                        comp.Show = false;
                    };
                }).catch(e => reject(e));
            }
            else {
                reject("no Editing Component");
            }
        });
    }
    EditWithTreeBoxAsync(node) {
        return new Promise((resolve, reject) => {
            if (this.treeComponent) {
                var model = node.data;
                model.editing = true;
                this.treeComponent.treeModel.virtualScroll.scrollIntoView(node, true);
                setTimeout(() => {
                    let el = document.getElementById('editor' + model.id);
                    if (el) {
                        el.addEventListener("keyup", ev => {
                            if (ev.key == "Enter") {
                                model.editing = false;
                                if (model.name && model.name.length > 1) {
                                    var ch = model.children;
                                    model.children = [];
                                    this.SubmitUpdateAsync(model).then(res => {
                                        model.children = ch;
                                        resolve({ model: model, result: res });
                                    }).catch(e => {
                                        model.editing = false;
                                    });
                                }
                            }
                        });
                        el.addEventListener("blur", ev => {
                            model.editing = false;
                            reject("blurred");
                        });
                        el.focus();
                    }
                }, 400);
            }
        });
    }
    AddWithTreeBoxAsync(addTo) {
        return new Promise((resolve, reject) => {
            if (this.treeComponent) {
                var model = this.InitializeModel(addTo);
                if (addTo)
                    model.parentId = addTo.data.id;
                model.editing = true;
                addTo = addTo ? addTo : this.treeComponent.treeModel.virtualRoot;
                var nNode = this.AppendToNode(addTo, model, true);
                addTo.expand();
                this.treeComponent.treeModel.virtualScroll.scrollIntoView(nNode, true);
                setTimeout(() => {
                    let el = document.getElementById('editor0');
                    if (el) {
                        el.addEventListener("keyup", ev => {
                            if (ev.key == "Enter") {
                                model.editing = false;
                                if (model.name && model.name.length > 1) {
                                    this.SubmitNewAsync(model).then(res => {
                                        resolve({ model: model, result: res });
                                    }).catch(e => {
                                        _utilities_utils__WEBPACK_IMPORTED_MODULE_6__["Utils"].HandleError(e, true);
                                        this.RemoveFromNode(addTo, model);
                                        reject(e);
                                    });
                                }
                                else {
                                    this.RemoveFromNode(addTo, model);
                                    reject("blurred");
                                }
                            }
                        });
                        el.addEventListener("blur", ev => {
                            if (model.editing)
                                this.RemoveFromNode(addTo, model);
                            reject("blurred");
                        });
                        el.focus();
                    }
                }, 400);
            }
        });
    }
    AfterAddSubmit(parentNode, model, res) {
        if (this.treeComponent) {
            parentNode = parentNode ? parentNode : this.treeComponent.treeModel.virtualRoot;
            if (res.data.Id) {
                model.id = res.data.Id;
            }
            this.AppendToNode(parentNode, model);
        }
    }
    AfterEditSubmit(node, changedModel) {
        var children = node.data.children;
        var lst = node.parent.data.children;
        var ind = lst.indexOf(node.data);
        changedModel.children = children;
        lst[ind] = changedModel;
        if (this.treeComponent)
            this.treeComponent.treeModel.update();
    }
    AddNode(parentNode) {
        if (!this.treeComponent)
            return;
        if (this.AddFunction) {
            this.AddFunction(parentNode).then(d => this.AfterAddSubmit(parentNode, d.model, d.result));
        }
        else if (this.editingModalRequest) {
            this.AddWithModalAsync(parentNode).then(d => this.AfterAddSubmit(parentNode, d.model, d.result));
        }
        else {
            this.AddWithTreeBoxAsync(parentNode).then(d => {
                if (d.result.data.Id)
                    d.model.id = d.result.data.Id;
            }).catch(e => { });
        }
    }
    EditNode(obj) {
        if (!this.treeComponent)
            return;
        if (this.EditFunction) {
            this.EditFunction(obj).then(d => this.AfterEditSubmit(obj, d.model));
        }
        else if (this.editingModalRequest) {
            this.EditWithModalAsync(obj).then(d => this.AfterEditSubmit(obj, d.model));
        }
        else {
            this.EditWithTreeBoxAsync(obj).then(d => { }).catch(e => { });
        }
    }
    DeleteAsync(item) {
        return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, function* () {
            var c = yield _shell__WEBPACK_IMPORTED_MODULE_5__["Shell"].Main.ShowDeleteConfirm();
            if (!c) {
                return Promise.reject("user rejected");
            }
            return yield this.Service.AttemptDelete(item);
        });
    }
    DeleteNode(node) {
        let item = node.data;
        this.DeleteAsync(item).then(d => {
            let parentModel = node.parent.data;
            let i = parentModel.children.indexOf(item);
            if (i > -1)
                parentModel.children.splice(i, 1);
            if (this.treeComponent)
                this.treeComponent.treeModel.update();
        }).catch(d => {
            _utilities_utils__WEBPACK_IMPORTED_MODULE_6__["Utils"].HandleError(d, true, true);
        });
    }
    SaveExpanded() {
        codeshell_helpers__WEBPACK_IMPORTED_MODULE_2__["Stored"].Set(this.ComponentName + "_Expanded", { Items: this.Expanded });
    }
    RemoveFromNode(from, model) {
        if (this.treeComponent) {
            from = from ? from : this.treeComponent.treeModel.virtualRoot;
            var lst = from.data.children;
            Object(_utilities_utils__WEBPACK_IMPORTED_MODULE_6__["List_RemoveItem"])(lst, model);
            if (this.treeComponent)
                this.treeComponent.treeModel.update();
        }
    }
    AppendToNode(addTo, model, top = false) {
        if (this.treeComponent) {
            var parentData = addTo.data.children;
            var newNode = new angular_tree_component__WEBPACK_IMPORTED_MODULE_4__["TreeNode"](model, addTo, this.treeComponent.treeModel, 0);
            if (top)
                addTo.children.unshift(newNode);
            else
                addTo.children.push(newNode);
            parentData.push(model);
            addTo.expand();
            this.treeComponent.treeModel.update();
            return newNode;
        }
        throw "no tree component";
    }
    SelectItemAsync() {
        return new Promise((res, rej) => {
            this.OnOk = d => {
                res(this.selectedItem);
                return Promise.resolve(true);
            };
            this.OnCancel = d => {
                rej("user canceled");
                return Promise.resolve(false);
            };
            this.StartAsync().then(comp => {
                this.Show = true;
            });
        });
    }
    SubmitNewAsync(model) {
        return this.Service.Post("Post", model);
    }
    SubmitUpdateAsync(model) {
        return this.Service.Put("Put", model);
    }
}
TreeComponentBase.ɵfac = function TreeComponentBase_Factory(t) { return ɵTreeComponentBase_BaseFactory(t || TreeComponentBase); };
TreeComponentBase.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵdefineComponent"]({ type: TreeComponentBase, selectors: [["ng-component"]], inputs: { LoadOnStart: "LoadOnStart" }, features: [_angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵInheritDefinitionFeature"]], decls: 0, vars: 0, template: function TreeComponentBase_Template(rf, ctx) { }, encapsulation: 2 });
const ɵTreeComponentBase_BaseFactory = /*@__PURE__*/ _angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵgetInheritedFactory"](TreeComponentBase);
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵsetClassMetadata"](TreeComponentBase, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_3__["Component"],
        args: [{ template: '' }]
    }], null, { LoadOnStart: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_3__["Input"]
        }] }); })();


/***/ }),

/***/ "niJb":
/*!*************************************************************!*\
  !*** ./src/core/codeshell/services/sectionedFormService.ts ***!
  \*************************************************************/
/*! exports provided: SectionedFormService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SectionedFormService", function() { return SectionedFormService; });
/* harmony import */ var codeshell_utilities_extensions__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! codeshell/utilities/extensions */ "afo8");
/* harmony import */ var codeshell_utilities_extensions__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(codeshell_utilities_extensions__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var codeshell_main__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! codeshell/main */ "7OZ3");



class SectionedFormService {
    constructor(tabs, active = [], defaultState = true, steps = false) {
        this.CurrentIndex = 1;
        this.CompleteIndex = 0;
        this.ActiveTabs = [];
        this.Steps = false;
        this.TabCount = 0;
        this._change = new _angular_core__WEBPACK_IMPORTED_MODULE_1__["EventEmitter"]();
        this._allComplete = new _angular_core__WEBPACK_IMPORTED_MODULE_1__["EventEmitter"]();
        this._validations = {};
        this._validStatus = new _angular_core__WEBPACK_IMPORTED_MODULE_1__["EventEmitter"]();
        this._isValid = true;
        this.TabCount = tabs;
        this.Steps = steps;
        if (defaultState) {
            for (let i = 1; i <= tabs; i++)
                this.ActiveTabs.push(i);
        }
        else {
            this.ActiveTabs = active;
        }
        setTimeout(() => {
            this._change.emit(this.ActiveTabs);
        }, 200);
    }
    get OnChange() { return this._change; }
    get OnAllComplete() { return this._allComplete; }
    get PecentComplete() { return ((this.CompleteIndex) / (this.TabCount - 1)) * 100; }
    get OnValidChanged() { return this._validStatus; }
    get IsValid() { return this._isValid; }
    Select(index, scroll) {
        this.ActiveTabs = [index];
        this.CurrentIndex = index;
        if (scroll)
            this.Scroll(scroll);
        this._change.emit(this.ActiveTabs);
    }
    Toggle(index, scroll) {
        var loc = this.ActiveTabs.indexOf(index);
        if (loc == -1) {
            if (this.Steps) {
                if (index <= (this.CompleteIndex + 1)) {
                    this.CurrentIndex = index;
                    this.ActiveTabs.push(index);
                }
            }
            else {
                this.ActiveTabs.push(index);
            }
        }
        else {
            this.ActiveTabs.splice(loc, 1);
        }
        if (scroll)
            this.Scroll(scroll);
        this._change.emit(this.ActiveTabs);
    }
    SetValidState(index, state) {
        this._validations[index] = state;
        var current = true;
        for (var v in this._validations) {
            if (this._validations[v] == false) {
                current = false;
                break;
            }
        }
        if (this._isValid != current) {
            this._validStatus.emit(current);
            this._isValid = current;
        }
    }
    SetComplete(index, force = false) {
        if (!force && this.CompleteIndex < index) {
            this.CompleteIndex = index;
        }
        Object(codeshell_main__WEBPACK_IMPORTED_MODULE_2__["List_RemoveItem"])(this.ActiveTabs, index);
        var nxt = index + 1;
        if (this.ActiveTabs.indexOf(nxt) == -1)
            this.ActiveTabs.push(nxt);
        this._change.emit(this.ActiveTabs);
    }
    Scroll(id) {
        let elem = document.getElementById(id);
        if (elem) {
            window.scrollTo({ top: elem.offsetTop });
        }
    }
    AllComplete() {
        this._allComplete.emit();
    }
    Next() {
        this.CompleteIndex = this.CurrentIndex;
        this.CurrentIndex += 1;
        this.ActiveTabs = [this.CurrentIndex];
    }
    Back() {
        if (this.CurrentIndex == 1)
            return;
        this.CompleteIndex -= 1;
        this.CurrentIndex -= 1;
        this.ActiveTabs = [this.CurrentIndex];
    }
    IsLast() {
        return this.CurrentIndex == this.TabCount;
    }
    CanGoBack() {
        return this.CurrentIndex > 1;
    }
    CanGoNext() {
        return this.CurrentIndex < this.TabCount;
    }
    IsComplete(index) {
        return index <= this.CompleteIndex;
    }
    IsActive(index) {
        return this.ActiveTabs.indexOf(index) > -1;
    }
    IsCurrent(index) {
        return this.CurrentIndex == index;
    }
}


/***/ }),

/***/ "nliR":
/*!*******************************!*\
  !*** ./src/app/App.module.ts ***!
  \*******************************/
/*! exports provided: AppModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppModule", function() { return AppModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var _angular_platform_browser__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/platform-browser */ "jhN1");
/* harmony import */ var _angular_platform_browser_animations__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/platform-browser/animations */ "R1ws");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ngx-toastr */ "5eHb");
/* harmony import */ var codeshell__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! codeshell */ "7OZ3");
/* harmony import */ var codeshell_security__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! codeshell/security */ "U6Sh");
/* harmony import */ var _base_example_base_module__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @base/example-base.module */ "NXgI");
/* harmony import */ var _app_component__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./app.component */ "Sy1n");
/* harmony import */ var _shared_shared_module__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./shared/shared.module */ "PCNd");
/* harmony import */ var _app_routing_module__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ./app-routing.module */ "vY5A");
/* harmony import */ var codeshell_base_module_module__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! codeshell/base-module.module */ "079c");
/* harmony import */ var _core_codeshell_codeshell_module__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ../core/codeshell/codeshell.module */ "bT1W");















class AppModule extends codeshell_base_module_module__WEBPACK_IMPORTED_MODULE_10__["BaseModule"] {
}
AppModule.ɵmod = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineNgModule"]({ type: AppModule, bootstrap: [_app_component__WEBPACK_IMPORTED_MODULE_7__["AppComponent"]] });
AppModule.ɵinj = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineInjector"]({ factory: function AppModule_Factory(t) { return ɵAppModule_BaseFactory(t || AppModule); }, providers: [
        { provide: codeshell_security__WEBPACK_IMPORTED_MODULE_5__["DomainDataProvider"], useValue: new codeshell_security__WEBPACK_IMPORTED_MODULE_5__["DomainDataProvider"](Object(_app_routing_module__WEBPACK_IMPORTED_MODULE_9__["GetDomainsData"])()) }
    ], imports: [[
            ngx_toastr__WEBPACK_IMPORTED_MODULE_3__["ToastrModule"].forRoot(),
            codeshell__WEBPACK_IMPORTED_MODULE_4__["CodeShellModule"].forRoot(),
            _base_example_base_module__WEBPACK_IMPORTED_MODULE_6__["ExampleBaseModule"].forRoot(),
            _shared_shared_module__WEBPACK_IMPORTED_MODULE_8__["SharedModule"],
            _angular_platform_browser__WEBPACK_IMPORTED_MODULE_1__["BrowserModule"],
            _angular_platform_browser_animations__WEBPACK_IMPORTED_MODULE_2__["BrowserAnimationsModule"],
            _app_routing_module__WEBPACK_IMPORTED_MODULE_9__["AppRoutingModule"]
        ]] });
(function () { (typeof ngJitMode === "undefined" || ngJitMode) && _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵsetNgModuleScope"](AppModule, { declarations: [_app_component__WEBPACK_IMPORTED_MODULE_7__["AppComponent"]], imports: [ngx_toastr__WEBPACK_IMPORTED_MODULE_3__["ToastrModule"], _core_codeshell_codeshell_module__WEBPACK_IMPORTED_MODULE_11__["CodeShellModule"], _base_example_base_module__WEBPACK_IMPORTED_MODULE_6__["ExampleBaseModule"], _shared_shared_module__WEBPACK_IMPORTED_MODULE_8__["SharedModule"],
        _angular_platform_browser__WEBPACK_IMPORTED_MODULE_1__["BrowserModule"],
        _angular_platform_browser_animations__WEBPACK_IMPORTED_MODULE_2__["BrowserAnimationsModule"],
        _app_routing_module__WEBPACK_IMPORTED_MODULE_9__["AppRoutingModule"]] }); })();
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](AppModule, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"],
        args: [{
                bootstrap: [_app_component__WEBPACK_IMPORTED_MODULE_7__["AppComponent"]],
                declarations: [_app_component__WEBPACK_IMPORTED_MODULE_7__["AppComponent"],],
                imports: [
                    ngx_toastr__WEBPACK_IMPORTED_MODULE_3__["ToastrModule"].forRoot(),
                    codeshell__WEBPACK_IMPORTED_MODULE_4__["CodeShellModule"].forRoot(),
                    _base_example_base_module__WEBPACK_IMPORTED_MODULE_6__["ExampleBaseModule"].forRoot(),
                    _shared_shared_module__WEBPACK_IMPORTED_MODULE_8__["SharedModule"],
                    _angular_platform_browser__WEBPACK_IMPORTED_MODULE_1__["BrowserModule"],
                    _angular_platform_browser_animations__WEBPACK_IMPORTED_MODULE_2__["BrowserAnimationsModule"],
                    _app_routing_module__WEBPACK_IMPORTED_MODULE_9__["AppRoutingModule"]
                ],
                providers: [
                    { provide: codeshell_security__WEBPACK_IMPORTED_MODULE_5__["DomainDataProvider"], useValue: new codeshell_security__WEBPACK_IMPORTED_MODULE_5__["DomainDataProvider"](Object(_app_routing_module__WEBPACK_IMPORTED_MODULE_9__["GetDomainsData"])()) }
                ]
            }]
    }], null, null); })();
const ɵAppModule_BaseFactory = /*@__PURE__*/ _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵgetInheritedFactory"](AppModule);


/***/ }),

/***/ "ppms":
/*!**************************************************!*\
  !*** ./src/core/codeshell/utilities/registry.ts ***!
  \**************************************************/
/*! exports provided: Registry */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Registry", function() { return Registry; });
/* harmony import */ var _security_models__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../security/models */ "gFuU");

class Registry {
    static get UserType() {
        if (this._userType)
            return this._userType;
        else
            return _security_models__WEBPACK_IMPORTED_MODULE_0__["UserDTO"];
    }
    ;
    static Register(name, s) {
        Registry.List[name] = s;
    }
    static Get(name) {
        if (Registry.List[name])
            return Registry.List[name];
        return undefined;
    }
    static RegisterUserType(con) {
        Registry._userType = con;
    }
}
Registry.List = {};


/***/ }),

/***/ "qJZ4":
/*!**********************************************************!*\
  !*** ./src/core/codeshell/directives/radio.directive.ts ***!
  \**********************************************************/
/*! exports provided: Radio */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Radio", function() { return Radio; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");


class Radio {
    constructor(el) {
        this.el = el;
        this.model = false;
        this.modelOut = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
    }
    get Element() { return this.el.nativeElement; }
    ngOnInit() {
        this.Element.checked = this.model;
    }
    OnClick() {
        var elems = document.getElementsByName(this.Element.name);
        for (let i = 0; i < elems.length; i++) {
            var el = elems.item(i);
            if (el != this.Element && el.type == "radio") {
                el.dispatchEvent(new Event('change'));
            }
        }
    }
    OnChange() {
        this.modelOut.emit(this.Element.checked);
    }
}
Radio.ɵfac = function Radio_Factory(t) { return new (t || Radio)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"])); };
Radio.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({ type: Radio, selectors: [["input", "type", "radio", "radioCheck", ""]], hostBindings: function Radio_HostBindings(rf, ctx) { if (rf & 1) {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function Radio_click_HostBindingHandler() { return ctx.OnClick(); })("change", function Radio_change_HostBindingHandler() { return ctx.OnChange(); });
    } }, inputs: { model: ["radioCheck", "model"] }, outputs: { modelOut: "radioCheckChange" }, exportAs: ["radioCheck"] });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](Radio, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
        args: [{ selector: "input[type='radio'][radioCheck]", exportAs: 'radioCheck' }]
    }], function () { return [{ type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"] }]; }, { model: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["radioCheck"]
        }], modelOut: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"],
            args: ["radioCheckChange"]
        }], OnClick: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"],
            args: ["click"]
        }], OnChange: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"],
            args: ["change"]
        }] }); })();


/***/ }),

/***/ "rCrt":
/*!*********************************************************!*\
  !*** ./src/core/codeshell/base-components/loginBase.ts ***!
  \*********************************************************/
/*! exports provided: LoginBase */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "LoginBase", function() { return LoginBase; });
/* harmony import */ var _baseComponent__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./baseComponent */ "I5Ck");
/* harmony import */ var codeshell_security__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! codeshell/security */ "U6Sh");
/* harmony import */ var codeshell_shell__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! codeshell/shell */ "UoIT");
/* harmony import */ var codeshell_utilities_utils__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! codeshell/utilities/utils */ "VXkE");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/core */ "fXoL");






class LoginBase extends _baseComponent__WEBPACK_IMPORTED_MODULE_0__["BaseComponent"] {
    constructor() {
        super(...arguments);
        this.AccountService = codeshell_shell__WEBPACK_IMPORTED_MODULE_2__["Shell"].Injector.get(codeshell_security__WEBPACK_IMPORTED_MODULE_1__["AccountServiceBase"]);
        this.model = {};
    }
    ShowPassword(passInput) {
        passInput.type = "text";
    }
    HidePassword(passInput) {
        passInput.type = "password";
    }
    TogglePassword(passInput) {
        if (passInput.type == "text")
            passInput.type = "password";
        else
            passInput.type = "text";
    }
    ngOnInit() {
        super.ngOnInit();
        if (codeshell_shell__WEBPACK_IMPORTED_MODULE_2__["Shell"].Session.IsLoggedIn)
            this.Router.navigateByUrl("/");
        this.Title = codeshell_shell__WEBPACK_IMPORTED_MODULE_2__["Shell"].Translator.instant("Words.Login");
    }
    Login() {
        this.AccountService.Login(this.model).then(data => {
            codeshell_shell__WEBPACK_IMPORTED_MODULE_2__["Shell"].Session.StartSession(data);
            this.Router.navigateByUrl("/");
        }).catch(error => {
            codeshell_utilities_utils__WEBPACK_IMPORTED_MODULE_3__["Utils"].HandleError(error, true);
        });
    }
}
LoginBase.ɵfac = function LoginBase_Factory(t) { return ɵLoginBase_BaseFactory(t || LoginBase); };
LoginBase.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵdefineComponent"]({ type: LoginBase, selectors: [["ng-component"]], features: [_angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵInheritDefinitionFeature"]], decls: 0, vars: 0, template: function LoginBase_Template(rf, ctx) { }, encapsulation: 2 });
const ɵLoginBase_BaseFactory = /*@__PURE__*/ _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵgetInheritedFactory"](LoginBase);
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵsetClassMetadata"](LoginBase, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_4__["Component"],
        args: [{ template: '' }]
    }], null, null); })();


/***/ }),

/***/ "rp8W":
/*!*****************************************************************!*\
  !*** ./src/core/codeshell/security/authorizationServiceBase.ts ***!
  \*****************************************************************/
/*! exports provided: AuthorizationError, AuthorizationServiceBase */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AuthorizationError", function() { return AuthorizationError; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AuthorizationServiceBase", function() { return AuthorizationServiceBase; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "mrSG");
/* harmony import */ var _models__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./models */ "gFuU");
/* harmony import */ var _shell__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../shell */ "UoIT");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/core */ "fXoL");




class AuthorizationError {
    constructor() {
        this.message = "";
    }
}
class AuthorizationServiceBase {
    constructor() {
    }
    IsAuthorized(user, data, events = false) {
        if (data.IsAnonymous)
            return true;
        this.User = user;
        if (this.User) {
            if (!this.CheckApp(data)) {
                if (events)
                    AuthorizationServiceBase.OnAuthorizationFailed.emit({ data: data, message: "Incompatible Apps" });
                return false;
            }
            if (data.AllowAll)
                return true;
            if (this.CheckUserType(data))
                return true;
            let p = this.GetPermission(data.resource);
            if (!p.Can(data.action)) {
                if (events)
                    AuthorizationServiceBase.OnAuthorizationFailed.emit({ data: data, message: "No permission" });
                return false;
            }
            return true;
        }
        return false;
    }
    IsAuthorizedAsync(data) {
        return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, function* () {
            if (data.IsAnonymous)
                return true;
            var user = yield _shell__WEBPACK_IMPORTED_MODULE_2__["Shell"].Session.GetUserAsync();
            return this.IsAuthorized(user, data, true);
        });
    }
    GetPermission(id) {
        if (this.User && this.User.permissions[id]) {
            let u = this.User;
            return u.permissions[id];
        }
        else {
            let s = new _models__WEBPACK_IMPORTED_MODULE_1__["Permission"];
            s.view = false;
            return s;
        }
    }
    CheckApp(data) {
        if (this.User && data.apps) {
            for (let d of data.apps) {
                if (this.User.app == d)
                    return true;
            }
            return false;
        }
        return true;
    }
    CheckUserType(data) {
        if (!this.User)
            return false;
        if (typeof data.action != "string")
            return false;
        let type = data.action;
        if (type.indexOf("UserType") != 0)
            return false;
        return ("UserType." + this.User.userTypeString) == type;
    }
}
AuthorizationServiceBase.OnAuthorizationFailed = new _angular_core__WEBPACK_IMPORTED_MODULE_3__["EventEmitter"]();


/***/ }),

/***/ "sVHn":
/*!*****************************************************************!*\
  !*** ./src/core/codeshell/base-components/listComponentBase.ts ***!
  \*****************************************************************/
/*! exports provided: ListComponentBase */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ListComponentBase", function() { return ListComponentBase; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "mrSG");
/* harmony import */ var _baseComponent__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./baseComponent */ "I5Ck");
/* harmony import */ var _shell__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../shell */ "UoIT");
/* harmony import */ var codeshell_main__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! codeshell/main */ "7OZ3");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/core */ "fXoL");






class ListComponentBase extends _baseComponent__WEBPACK_IMPORTED_MODULE_1__["BaseComponent"] {
    constructor() {
        super(...arguments);
        this.filter = {};
        this.list = [];
        this.totalCount = 0;
        this.pageIndex = 0;
        this.options = { Showing: 10, Skip: 0 };
        this._sortingClass = null;
        this.Selection = null;
        this._loadedOnce = false;
    }
    get CollectionId() { return null; }
    ngOnInit() {
        super.ngOnInit();
        let opts = this.GetLookupOptions();
        if (opts != null) {
            this.LoadLookupsAsync(opts).then(l => {
                this.Lookups = l;
                this.Start();
            });
        }
        else {
            this.Start();
        }
    }
    LoadLookupsAsync(opts) {
        return this.Service.Get("GetListLookups", opts);
    }
    Start() {
        if (!this.IsEmbedded)
            this.LoadData();
        this.OnReady();
    }
    StartAsync() {
        return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, function* () {
            if (!this._loadedOnce) {
                yield this.LoadData();
                this._loadedOnce = true;
            }
            return this;
        });
    }
    OnReady() {
        window.scrollTo(0, 0);
        if (this.options.OrderProperty)
            this._sortingClass = "sorting-head-" + this.options.Direction;
    }
    PageSelected(n) {
        this.options.Skip = this.options.Showing * n;
        this.pageIndex = n;
        this.LoadData();
    }
    Delete(item) {
        if (this.Debug)
            console.log("Deleting", item);
        this.DeleteAsync(item).then(d => {
            if (d.canDelete) {
                this.NotifyTranslate("delete_success");
                this.LoadData();
            }
            else {
                codeshell_main__WEBPACK_IMPORTED_MODULE_3__["Utils"].HandleResult(d, true, true);
            }
        }).catch(d => {
            if (d != "cancelled")
                codeshell_main__WEBPACK_IMPORTED_MODULE_3__["Utils"].HandleError(d, true, true);
        });
    }
    DeleteAsync(item) {
        return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, function* () {
            var c = yield _shell__WEBPACK_IMPORTED_MODULE_2__["Shell"].Main.ShowDeleteConfirm();
            if (!c) {
                return Promise.reject("cancelled");
            }
            return yield this.Service.AttemptDelete(item);
        });
    }
    AttemptDelete(item) {
        return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, function* () {
            let id = item.id;
            if (id == undefined) {
                id = item.entity.id;
            }
            var s = yield this.Service.AttemptDelete(id);
            if (s.canDelete) {
                this.LoadData();
                s.message = "delete_success";
            }
            return s;
        });
    }
    LoadDataPromise() {
        if (this.Loader)
            return this.Loader(this.options);
        if (this.CollectionId == null) {
            return this.Service.GetPaged("Get", this.options);
        }
        else {
            return this.Service.GetPaged("GetCollection/" + this.CollectionId, this.options);
        }
    }
    Clear(id) {
        this.totalCount = 0;
        this.list = [];
    }
    LoadData() {
        this.options.Filters = this.stringifyFilters();
        let prom = this.LoadDataPromise();
        prom.then(e => {
            this.ProcessResponse(e);
            this.list = e.list;
            this.totalCount = e.totalCount;
            if (this.Selection)
                this.Selection.List = this.list;
            this.AfterLoad();
        });
    }
    AfterLoad() { }
    ProcessResponse(e) { }
    GetFilters() {
        let filters = [];
        for (var i in this.filter) {
            if (this.filter[i].Value1 != undefined || this.filter[i].Value2 != undefined || this.filter[i].Ids.length > 0)
                filters.push(this.filter[i]);
        }
        return filters;
    }
    stringifyFilters() {
        var filters = this.GetFilters();
        return JSON.stringify(filters);
    }
    ClearFilter(item) {
        item.Value1 = undefined;
        item.Value2 = undefined;
        item.Ids = [];
        this.ResetPagination();
        this.LoadData();
    }
    SelectIdSingle(f, val) {
        if (val == 0) {
            f.Value1 = undefined;
            f.Value2 = undefined;
            f.Ids = [];
        }
        else {
            f.Ids = [val];
        }
        this.LoadData();
    }
    ToggleIdSingle(f, val) {
        let s = f.Ids.indexOf(val);
        if (s > -1)
            f.Ids.splice(s, 1);
        else {
            f.Ids = [val];
            f.Value1 = val;
        }
        this.LoadData();
    }
    ToggleId(f, id) {
        let s = f.Ids.indexOf(id);
        if (s > -1)
            f.Ids.splice(s, 1);
        else {
            f.Ids.push(id);
        }
        this.ResetPagination();
        this.LoadData();
    }
    IsSelected(f, id) {
        return f.Ids.indexOf(id) > -1;
    }
    ResetPagination() {
        this.options.Skip = 0;
        this.pageIndex = 0;
    }
    HeaderSearch(term) {
        this.options.SearchTerm = term;
        this.ResetPagination();
        this.LoadData();
    }
    SortBy(prop) {
        if (prop == this.options.OrderProperty) {
            this.options.Direction = this.options.Direction == "ASC" ? "DESC" : "ASC";
        }
        else {
            this.options.OrderProperty = prop;
            this.options.Direction = "ASC";
        }
        this._sortingClass = "sorting-head-" + this.options.Direction;
        this.PageSelected(0);
    }
    GetHeaderClass(prop) {
        if (this.options.OrderProperty == prop) {
            return this._sortingClass;
        }
        return null;
    }
}
ListComponentBase.ɵfac = function ListComponentBase_Factory(t) { return ɵListComponentBase_BaseFactory(t || ListComponentBase); };
ListComponentBase.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵdefineComponent"]({ type: ListComponentBase, selectors: [["ng-component"]], features: [_angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵInheritDefinitionFeature"]], decls: 0, vars: 0, template: function ListComponentBase_Template(rf, ctx) { }, encapsulation: 2 });
const ɵListComponentBase_BaseFactory = /*@__PURE__*/ _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵgetInheritedFactory"](ListComponentBase);
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵsetClassMetadata"](ListComponentBase, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_4__["Component"],
        args: [{ template: '' }]
    }], null, null); })();


/***/ }),

/***/ "u9rM":
/*!**************************************************************************!*\
  !*** ./src/core/codeshell/base-components/sectionedEditComponentBase.ts ***!
  \**************************************************************************/
/*! exports provided: SectionedEditComponentBase */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SectionedEditComponentBase", function() { return SectionedEditComponentBase; });
/* harmony import */ var _editComponentBase__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./editComponentBase */ "G778");
/* harmony import */ var codeshell_services_sectionedFormService__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! codeshell/services/sectionedFormService */ "niJb");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/core */ "fXoL");




class SectionedEditComponentBase extends _editComponentBase__WEBPACK_IMPORTED_MODULE_0__["EditComponentBase"] {
    constructor() {
        super(...arguments);
        this.SF = new codeshell_services_sectionedFormService__WEBPACK_IMPORTED_MODULE_1__["SectionedFormService"](1);
    }
}
SectionedEditComponentBase.ɵfac = function SectionedEditComponentBase_Factory(t) { return ɵSectionedEditComponentBase_BaseFactory(t || SectionedEditComponentBase); };
SectionedEditComponentBase.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵdefineComponent"]({ type: SectionedEditComponentBase, selectors: [["ng-component"]], inputs: { SF: ["manager", "SF"] }, features: [_angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵInheritDefinitionFeature"]], decls: 0, vars: 0, template: function SectionedEditComponentBase_Template(rf, ctx) { }, encapsulation: 2 });
const ɵSectionedEditComponentBase_BaseFactory = /*@__PURE__*/ _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵgetInheritedFactory"](SectionedEditComponentBase);
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵsetClassMetadata"](SectionedEditComponentBase, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_2__["Component"],
        args: [{ template: '' }]
    }], null, { SF: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_2__["Input"],
            args: ["manager"]
        }] }); })();


/***/ }),

/***/ "vY5A":
/*!***************************************!*\
  !*** ./src/app/app-routing.module.ts ***!
  \***************************************/
/*! exports provided: AppRoutingModule, GetDomainsData */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppRoutingModule", function() { return AppRoutingModule; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GetDomainsData", function() { return GetDomainsData; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "tyNb");
/* harmony import */ var codeshell_localization__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! codeshell/localization */ "InfA");
/* harmony import */ var _base_main_login_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @base/main/login.component */ "8sze");






codeshell_localization__WEBPACK_IMPORTED_MODULE_2__["Translator"].SetLoaders({});
const routes = [
    { path: 'login', component: _base_main_login_component__WEBPACK_IMPORTED_MODULE_3__["Login"], data: { action: 'anonymous', name: "login" } },
    { path: '**', redirectTo: '/' }
];
class AppRoutingModule {
}
AppRoutingModule.ɵmod = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineNgModule"]({ type: AppRoutingModule });
AppRoutingModule.ɵinj = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineInjector"]({ factory: function AppRoutingModule_Factory(t) { return new (t || AppRoutingModule)(); }, imports: [[_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"].forRoot(routes, { relativeLinkResolution: 'legacy' })], _angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]] });
(function () { (typeof ngJitMode === "undefined" || ngJitMode) && _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵsetNgModuleScope"](AppRoutingModule, { imports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]], exports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]] }); })();
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](AppRoutingModule, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"],
        args: [{
                imports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"].forRoot(routes, { relativeLinkResolution: 'legacy' })],
                exports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]],
            }]
    }], null, null); })();
let data = null;
function GetDomainsData() {
    if (!data) {
        data = [];
    }
    return data;
}


/***/ }),

/***/ "vyk5":
/*!************************************************!*\
  !*** ./src/core/codeshell/http/httpRequest.ts ***!
  \************************************************/
/*! exports provided: Methods, RequestParams, HttpRequest */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Methods", function() { return Methods; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "RequestParams", function() { return RequestParams; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "HttpRequest", function() { return HttpRequest; });
var Methods;
(function (Methods) {
    Methods[Methods["Get"] = 0] = "Get";
    Methods[Methods["Post"] = 1] = "Post";
    Methods[Methods["Put"] = 2] = "Put";
    Methods[Methods["Delete"] = 3] = "Delete";
})(Methods || (Methods = {}));
class RequestParams {
}
class HttpRequest {
    constructor(url, params, body) {
        this.Params = new RequestParams;
        this.Params = new RequestParams;
        this.Url = url;
        this.Body = body;
        if (typeof params == 'number') {
            this.Url += "/" + params;
        }
        else if (params) {
            this.Params.params = params;
        }
    }
}


/***/ }),

/***/ "wGhu":
/*!**********************************************!*\
  !*** ./src/core/codeshell/helpers/tagged.ts ***!
  \**********************************************/
/*! exports provided: TaggedArgs, Tagged */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "TaggedArgs", function() { return TaggedArgs; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Tagged", function() { return Tagged; });
/* harmony import */ var _listItem__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./listItem */ "JLZs");

class TaggedArgs {
    constructor() {
        this.Data = [];
        this.Source = [];
        /**  fromData: From Lookup, fromSrc: From selected items => Should return expression if arg1 exists in items */
        this.Comparer = (d, s) => true;
        this.CreateNew = d => { };
    }
}
class Tagged {
    constructor() {
        this.Tag = new _listItem__WEBPACK_IMPORTED_MODULE_0__["ListItem"]();
    }
    static FromDB(arg0) {
        var s = Object.assign(new Tagged, arg0);
        s.Tag = new _listItem__WEBPACK_IMPORTED_MODULE_0__["ListItem"];
        return s;
    }
    static CastList(lst) {
        for (var i in lst) {
            lst[i] = Tagged.FromDB(lst[i]);
        }
        return lst;
    }
    static JoinLists_GEN(con, args) {
        let lst = [];
        let comparer = (d, s) => false;
        let createNew = (item) => { };
        if (args.Comparer)
            comparer = args.Comparer;
        if (args.CreateNew)
            createNew = args.CreateNew;
        for (let dev of args.Data) {
            let d = Object.assign(new con, dev);
            let t = args.Source.find(e => comparer(dev, e));
            if (t) {
                t.selected = true;
            }
            else {
                t = _listItem__WEBPACK_IMPORTED_MODULE_0__["ListItem"].Detached(createNew(dev));
            }
            d.Tag = t;
            lst.push(d);
        }
        return lst;
    }
    static JoinLists(args) {
        let lst = [];
        let comparer = (d, s) => false;
        let createNew = (item) => { };
        if (args.Comparer)
            comparer = args.Comparer;
        if (args.CreateNew)
            createNew = args.CreateNew;
        for (let dev of args.Data) {
            let d = Object.assign(new Tagged, dev);
            let t = args.Source.find(e => comparer(dev, e));
            if (t) {
                t.selected = true;
            }
            else {
                t = _listItem__WEBPACK_IMPORTED_MODULE_0__["ListItem"].Detached(createNew(dev));
            }
            d.Tag = t;
            lst.push(d);
        }
        return lst;
    }
}


/***/ }),

/***/ "wNr3":
/*!***********************************************************!*\
  !*** ./src/core/codeshell/security/accountServiceBase.ts ***!
  \***********************************************************/
/*! exports provided: AccountServiceBase */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AccountServiceBase", function() { return AccountServiceBase; });
/* harmony import */ var _http_entityHttpService__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../http/entityHttpService */ "iLnC");

class AccountServiceBase extends _http_entityHttpService__WEBPACK_IMPORTED_MODULE_0__["EntityHttpService"] {
    Login(model) {
        return this.PostAs("Login", model);
    }
    GetUserData() {
        return this.GetAs("GetUserData");
    }
    RefreshToken(token) {
        return this.PostAs("RefreshToken", { token: token });
    }
    ChangePassword(dto) {
        return this.PostAs("ChangePassword", dto);
    }
    SendResetMail(mail) {
        return this.Save("SendResetMail", { email: mail });
    }
    RequestResetPassword(mail) {
        return this.Post("RequestResetPassword", { email: mail });
    }
}


/***/ }),

/***/ "x5Rz":
/*!*************************************************************!*\
  !*** ./src/core/codeshell/directives/fix-date.directive.ts ***!
  \*************************************************************/
/*! exports provided: FixDate */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "FixDate", function() { return FixDate; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");


class FixDate {
    Change(ev) { }
}
FixDate.ɵfac = function FixDate_Factory(t) { return new (t || FixDate)(); };
FixDate.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({ type: FixDate, selectors: [["", "fix-date", ""]], hostBindings: function FixDate_HostBindings(rf, ctx) { if (rf & 1) {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("dateChange", function FixDate_dateChange_HostBindingHandler($event) { return ctx.Change($event); });
    } }, exportAs: ["fix-date"] });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](FixDate, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
        args: [{
                selector: "[fix-date]",
                exportAs: "fix-date"
            }]
    }], null, { Change: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"],
            args: ["dateChange", ['$event']]
        }] }); })();


/***/ }),

/***/ "yvMo":
/*!*****************************************************!*\
  !*** ./src/core/codeshell/security/tokenStorage.ts ***!
  \*****************************************************/
/*! exports provided: TokenStorage */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "TokenStorage", function() { return TokenStorage; });
/* harmony import */ var _models__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./models */ "gFuU");
/* harmony import */ var codeshell_helpers__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! codeshell/helpers */ "GYDu");


class TokenStorage {
    SaveToken(data) {
        codeshell_helpers__WEBPACK_IMPORTED_MODULE_1__["Stored"].Set("TokenData", data);
    }
    LoadToken() {
        return codeshell_helpers__WEBPACK_IMPORTED_MODULE_1__["Stored"].Get("TokenData", _models__WEBPACK_IMPORTED_MODULE_0__["TokenData"]);
    }
    Clear() {
        codeshell_helpers__WEBPACK_IMPORTED_MODULE_1__["Stored"].Clear("TokenData");
        localStorage.removeItem("refresh");
    }
    GetRefreshToken() {
        return localStorage.getItem("refresh");
    }
    SaveRefreshToken(token) {
        localStorage.setItem("refresh", token);
    }
}


/***/ }),

/***/ "zUnb":
/*!*********************!*\
  !*** ./src/main.ts ***!
  \*********************/
/*! no exports provided */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var _environments_environment__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./environments/environment */ "AytR");
/* harmony import */ var _app_App_module__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./app/App.module */ "nliR");
/* harmony import */ var _angular_platform_browser__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/platform-browser */ "jhN1");




if (_environments_environment__WEBPACK_IMPORTED_MODULE_1__["environment"].production) {
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["enableProdMode"])();
}
_angular_platform_browser__WEBPACK_IMPORTED_MODULE_3__["platformBrowser"]().bootstrapModule(_app_App_module__WEBPACK_IMPORTED_MODULE_2__["AppModule"])
    .catch(err => console.error(err));


/***/ }),

/***/ "zYQP":
/*!********************************************************!*\
  !*** ./src/core/codeshell/validators/dateValidator.ts ***!
  \********************************************************/
/*! exports provided: DateValidator */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DateValidator", function() { return DateValidator; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var _utilities_utils__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../utilities/utils */ "VXkE");
/* harmony import */ var moment__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! moment */ "wd/R");
/* harmony import */ var moment__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(moment__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/forms */ "3Pt+");





class DateValidator {
    constructor(model, el) {
        this.model = model;
        this.IsRequired = false;
        let elem = el.nativeElement;
        this.IsRequired = elem.hasAttribute("required");
    }
    compareDates(date1, date2) {
        if (moment__WEBPACK_IMPORTED_MODULE_2__["isMoment"](date1)) {
            var e = date1;
            date1 = e.toDate();
        }
        if (moment__WEBPACK_IMPORTED_MODULE_2__["isMoment"](date2)) {
            var e = date2;
            date2 = e.toDate();
        }
        //debugger
        date1.setHours(0, 0, 0, 0);
        date2.setHours(0, 0, 0, 0);
        console.log(date1, date2);
        if (date1 >= date2)
            return 1;
        else
            return 2;
    }
    ngOnInit() {
        this.model.update.subscribe((v) => {
            if (!moment__WEBPACK_IMPORTED_MODULE_2__["isMoment"](v))
                v = new Date(v);
            setTimeout(() => {
                let min = this.minDate();
                let max = this.maxDate();
                let isValid = true;
                if (min != null) {
                    isValid = this.compareDates(v, min) == 1;
                }
                if (max != null && isValid) {
                    isValid = this.compareDates(v, max) == 2;
                }
                if (isValid) {
                    this.model.control.setErrors({ date_validation: null });
                    this.model.control.updateValueAndValidity();
                }
                else {
                    this.model.control.setErrors({ date_validation: true });
                }
            }, 200);
        });
    }
    minDate() {
        if (this.Type == "future")
            return new Date();
        if (this.Min) {
            return Object(_utilities_utils__WEBPACK_IMPORTED_MODULE_1__["Date_Get"])(this.Min);
        }
        return null;
    }
    ;
    maxDate() {
        if (this.Type == "past")
            return new Date();
        if (this.Max) {
            return Object(_utilities_utils__WEBPACK_IMPORTED_MODULE_1__["Date_Get"])(this.Max);
        }
        return null;
    }
    ;
}
DateValidator.ɵfac = function DateValidator_Factory(t) { return new (t || DateValidator)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_forms__WEBPACK_IMPORTED_MODULE_3__["NgModel"]), _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"])); };
DateValidator.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({ type: DateValidator, selectors: [["", "date-validate", "", "ngModel", ""]], inputs: { Min: ["min-date", "Min"], Max: ["max-date", "Max"], Type: ["date-validate", "Type"] }, exportAs: ["date-validate"] });
/*@__PURE__*/ (function () { _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](DateValidator, [{
        type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
        args: [{
                selector: '[date-validate][ngModel]',
                exportAs: 'date-validate'
            }]
    }], function () { return [{ type: _angular_forms__WEBPACK_IMPORTED_MODULE_3__["NgModel"] }, { type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"] }]; }, { Min: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["min-date"]
        }], Max: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["max-date"]
        }], Type: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["date-validate"]
        }] }); })();


/***/ }),

/***/ "zn8P":
/*!******************************************************!*\
  !*** ./$$_lazy_route_resource lazy namespace object ***!
  \******************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

function webpackEmptyAsyncContext(req) {
	// Here Promise.resolve().then() is used instead of new Promise() to prevent
	// uncaught exception popping up in devtools
	return Promise.resolve().then(function() {
		var e = new Error("Cannot find module '" + req + "'");
		e.code = 'MODULE_NOT_FOUND';
		throw e;
	});
}
webpackEmptyAsyncContext.keys = function() { return []; };
webpackEmptyAsyncContext.resolve = webpackEmptyAsyncContext;
module.exports = webpackEmptyAsyncContext;
webpackEmptyAsyncContext.id = "zn8P";

/***/ })

},[[0,"runtime","vendor"]]]);
//# sourceMappingURL=main-es2015.js.map