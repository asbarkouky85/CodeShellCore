(window["webpackJsonp"] = window["webpackJsonp"] || []).push([[5],{

/***/ "./ClientApp/app/NavigationGroups/NavGroupPages.html":
/*!***********************************************************!*\
  !*** ./ClientApp/app/NavigationGroups/NavGroupPages.html ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

eval("module.exports = \"<div class=\\\"page-header\\\" [ngClass]=\\\"IsEmbedded?'section-header':'container-fluid'\\\" *ngIf=\\\"!HideHeader\\\">\\r\\n    <div class=\\\"row\\\">\\r\\n        <div class=\\\"col-sm-5 col-xs-12\\\" *ngIf=\\\"!IsEmbedded\\\">\\r\\n            <h2>{{'Pages.NavigationGroups__NavGroupPages' | translate }}<span *ngIf=\\\"HeaderExtra\\\"> - {{HeaderExtra}}</span></h2>\\r\\n        </div>\\r\\n\\r\\n        <div class=\\\"col-sm-7 col-xs-12\\\" *ngIf=\\\"!IsEmbedded\\\">\\r\\n            <div class=\\\"row\\\">\\r\\n\\r\\n                    <div class=\\\"col-md-8 col-xs-12 text-last padTop\\\">\\r\\n                        \\r\\n<div class=\\\"row form-group \\\"\\r\\n     id=\\\"FG_tenantCode\\\"\\r\\n     #FG_tenantCode=\\\"bsFormGroup\\\" bs-group\\r\\n     >\\r\\n    <label class=\\\"col-sm-3 control-label\\\">\\r\\n        {{'Columns.Page__TenantCode' | translate }}\\r\\n    </label>\\r\\n    <div class=\\\"col-sm-9\\\">\\r\\n        \\r\\n<select class=\\\"form-control\\\"\\r\\n        name=\\\"Form__tenantCode\\\" \\r\\n        [li-watch]=\\\"model\\\"\\r\\n        [(ngModel)]=\\\"model.tenantCode\\\"\\r\\n         (change)=\\\"tenantChanged()\\\" >\\r\\n    <option *ngFor=\\\"let item of tenants\\\" [ngValue]=\\\"item.code\\\">{{item.name}}</option>\\r\\n</select>\\r\\n    <p *ngIf=\\\"!FG_tenantCode.Write\\\">{{FG_tenantCode.Value}}</p>\\r\\n\\r\\n        \\r\\n    </div>\\r\\n</div>\\r\\n\\r\\n                    </div>\\r\\n                <div class=\\\"col-md-4 col-xs-12 padTop padBottom\\\">\\r\\n                    <div class=\\\"pull-last btn-group\\\">\\r\\n\\r\\n                        \\r\\n    <button class=\\\"btn btn-primary\\\"\\r\\n            (click)=\\\"save()\\\"\\r\\n            \\r\\n             title=\\\"{{'Words.Save' | translate }}\\\">\\r\\n        <i class=\\\"fa fa-save fa-lg\\\"></i> \\r\\n    </button>\\r\\n\\r\\n\\r\\n    <button class=\\\"btn btn-success\\\"\\r\\n            (click)=\\\"AddPage()\\\"\\r\\n            \\r\\n             title=\\\"{{'Words.Add Page To Nave' | translate }}\\\">\\r\\n        <i class=\\\"fa fa-plus fa-lg\\\"></i> \\r\\n    </button>\\r\\n\\r\\n                        \\r\\n    <button class=\\\"btn btn-info margin-sides\\\"\\r\\n            (click)=\\\"Refresh()\\\"\\r\\n            \\r\\n            >\\r\\n        <i class=\\\"fa fa-redo\\\"></i> \\r\\n    </button>\\r\\n\\r\\n                    </div>\\r\\n                </div>\\r\\n            </div>\\r\\n        </div>\\r\\n\\r\\n        <div *ngIf=\\\"IsEmbedded\\\" class=\\\"container-fluid\\\">\\r\\n            <div class=\\\"row\\\">\\r\\n                <div class=\\\"col-md-8\\\">\\r\\n                    {{'Pages.NavigationGroups__NavGroupPages' | translate }}<span *ngIf=\\\"HeaderExtra\\\"> - {{HeaderExtra}}</span>\\r\\n                </div>\\r\\n                <div class=\\\"col-md-4\\\">\\r\\n                </div>\\r\\n            </div>\\r\\n        </div>\\r\\n    </div>\\r\\n    <div class=\\\"row\\\" *ngIf=\\\"!IsEmbedded\\\">\\r\\n\\r\\n        <ol class=\\\"breadcrumb\\\">\\r\\n            <li>\\r\\n                <a routerLink=\\\"'/'\\\">{{'Words.Main' | translate }}</a>\\r\\n            </li>\\r\\n            <li>\\r\\n                {{'Pages.NavigationGroups__NavGroupPages' | translate }}\\r\\n            </li>\\r\\n        </ol>\\r\\n    </div>\\r\\n</div><div [ngClass]=\\\"IsEmbedded?'panel panel-default embedded':'animated fadeInRight'\\\">\\r\\n    <div [ngClass]=\\\"IsEmbedded?'panel-body':'container-fluid content-block'\\\">\\r\\n        \\r\\n<div class=\\\"row\\\">\\r\\n    <div class=\\\"col-md-4\\\">\\r\\n        <naveList #NaveList [IsEmbedded]='true' (valueChange)=\\\"getPages($event)\\\"></naveList>\\r\\n    </div>\\r\\n    <div class=\\\"col-md-8\\\">\\r\\n        <navigationPageList #NavigationPageList [IsEmbedded]='true'></navigationPageList>\\r\\n    </div>\\r\\n</div>\\r\\n        \\r\\n        \\r\\n    </div>\\r\\n</div>\\r\\n\\n<div style='display:none' #lookupOptionsContainer values='{\\\"tenants\\\":\\\"C0\\\"}'></div>\\n<div style='display:none' #viewParamsContainer values='{\\\"ModelType\\\":null,\\\"AddUrl\\\":null,\\\"EditUrl\\\":null,\\\"DetailsUrl\\\":null,\\\"ListUrl\\\":null,\\\"Fields\\\":[],\\\"Other\\\":{\\\"NaveList\\\":\\\"NavigationGroups/NaveList\\\",\\\"NavigationPageList\\\":\\\"NavigationGroups/NavigationPageList\\\"}}'></div>\";\n\n//# sourceURL=webpack:///./ClientApp/app/NavigationGroups/NavGroupPages.html?");

/***/ }),

/***/ "./ClientApp/app/NavigationGroups/NavGroupPages.ts":
/*!*********************************************************!*\
  !*** ./ClientApp/app/NavigationGroups/NavGroupPages.ts ***!
  \*********************************************************/
/*! exports provided: NavGroupPages */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"NavGroupPages\", function() { return NavGroupPages; });\n/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ \"./node_modules/@angular/core/fesm5/core.js\");\n/* harmony import */ var Example_NavigationGroups_NavGroupPagesBase__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! Example/NavigationGroups/NavGroupPagesBase */ \"./Core/Example/NavigationGroups/NavGroupPagesBase.ts\");\nvar __extends = (undefined && undefined.__extends) || (function () {\r\n    var extendStatics = Object.setPrototypeOf ||\r\n        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||\r\n        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };\r\n    return function (d, b) {\r\n        extendStatics(d, b);\r\n        function __() { this.constructor = d; }\r\n        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());\r\n    };\r\n})();\r\nvar __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {\r\n    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;\r\n    if (typeof Reflect === \"object\" && typeof Reflect.decorate === \"function\") r = Reflect.decorate(decorators, target, key, desc);\r\n    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;\r\n    return c > 3 && r && Object.defineProperty(target, key, r), r;\r\n};\r\n\r\n\r\nvar NavGroupPages = /** @class */ (function (_super) {\r\n    __extends(NavGroupPages, _super);\r\n    function NavGroupPages() {\r\n        return _super !== null && _super.apply(this, arguments) || this;\r\n    }\r\n    NavGroupPages.prototype.GetPageId = function () { return 2000136858000; };\r\n    Object.defineProperty(NavGroupPages.prototype, \"CollectionId\", {\r\n        get: function () { return null; },\r\n        enumerable: true,\r\n        configurable: true\r\n    });\r\n    NavGroupPages = __decorate([\r\n        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__[\"Component\"])({ template: __webpack_require__(/*! ./NavGroupPages.html */ \"./ClientApp/app/NavigationGroups/NavGroupPages.html\"), selector: \"navGroupPages\" })\r\n    ], NavGroupPages);\r\n    return NavGroupPages;\r\n}(Example_NavigationGroups_NavGroupPagesBase__WEBPACK_IMPORTED_MODULE_1__[\"NavGroupPagesBase\"]));\r\n\r\n\n\n//# sourceURL=webpack:///./ClientApp/app/NavigationGroups/NavGroupPages.ts?");

/***/ }),

/***/ "./ClientApp/app/NavigationGroups/NavigationGroupsModule.ts":
/*!******************************************************************!*\
  !*** ./ClientApp/app/NavigationGroups/NavigationGroupsModule.ts ***!
  \******************************************************************/
/*! exports provided: NavigationGroupsModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"NavigationGroupsModule\", function() { return NavigationGroupsModule; });\n/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ \"./node_modules/@angular/core/fesm5/core.js\");\n/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ \"./node_modules/@angular/router/fesm5/router.js\");\n/* harmony import */ var _Shared_SharedModule__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../Shared/SharedModule */ \"./ClientApp/app/Shared/SharedModule.ts\");\n/* harmony import */ var codeshell_core__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! codeshell/core */ \"./Core/codeshell/core.ts\");\n/* harmony import */ var codeshell_security__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! codeshell/security */ \"./Core/codeshell/security.ts\");\n/* harmony import */ var _ngx_translate_core__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @ngx-translate/core */ \"./node_modules/@ngx-translate/core/fesm5/ngx-translate-core.js\");\n/* harmony import */ var _NavGroupPages__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./NavGroupPages */ \"./ClientApp/app/NavigationGroups/NavGroupPages.ts\");\nvar __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {\r\n    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;\r\n    if (typeof Reflect === \"object\" && typeof Reflect.decorate === \"function\") r = Reflect.decorate(decorators, target, key, desc);\r\n    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;\r\n    return c > 3 && r && Object.defineProperty(target, key, r), r;\r\n};\r\nvar __metadata = (undefined && undefined.__metadata) || function (k, v) {\r\n    if (typeof Reflect === \"object\" && typeof Reflect.metadata === \"function\") return Reflect.metadata(k, v);\r\n};\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\nvar NavigationGroupsModule = /** @class */ (function () {\r\n    function NavigationGroupsModule(trans, conf) {\r\n        trans.setDefaultLang(conf.Locale);\r\n        trans.use(conf.Locale);\r\n    }\r\n    NavigationGroupsModule = __decorate([\r\n        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__[\"NgModule\"])({\r\n            declarations: [_NavGroupPages__WEBPACK_IMPORTED_MODULE_6__[\"NavGroupPages\"],],\r\n            exports: [_NavGroupPages__WEBPACK_IMPORTED_MODULE_6__[\"NavGroupPages\"],],\r\n            imports: [\r\n                _Shared_SharedModule__WEBPACK_IMPORTED_MODULE_2__[\"SharedModule\"],\r\n                _angular_router__WEBPACK_IMPORTED_MODULE_1__[\"RouterModule\"].forChild([\r\n                    { path: \"NavGroupPages\", component: _NavGroupPages__WEBPACK_IMPORTED_MODULE_6__[\"NavGroupPages\"], canActivate: [codeshell_security__WEBPACK_IMPORTED_MODULE_4__[\"AuthFilter\"]], data: { name: \"NavigationGroups__NavGroupPages\", navigate: false, resource: \"NavigationGroups\", action: \"anonymous\", apps: null } },\r\n                ])\r\n            ]\r\n        }),\r\n        __metadata(\"design:paramtypes\", [_ngx_translate_core__WEBPACK_IMPORTED_MODULE_5__[\"TranslateService\"], codeshell_core__WEBPACK_IMPORTED_MODULE_3__[\"ServerConfigBase\"]])\r\n    ], NavigationGroupsModule);\r\n    return NavigationGroupsModule;\r\n}());\r\n\r\ncodeshell_core__WEBPACK_IMPORTED_MODULE_3__[\"Registry\"].Register(\"NavigationGroups/NavGroupPages\", _NavGroupPages__WEBPACK_IMPORTED_MODULE_6__[\"NavGroupPages\"]);\r\n\n\n//# sourceURL=webpack:///./ClientApp/app/NavigationGroups/NavigationGroupsModule.ts?");

/***/ }),

/***/ "./Core/Example/NavigationGroups/NavGroupPagesBase.ts":
/*!************************************************************!*\
  !*** ./Core/Example/NavigationGroups/NavGroupPagesBase.ts ***!
  \************************************************************/
/*! exports provided: NavGroupPagesBase */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"NavGroupPagesBase\", function() { return NavGroupPagesBase; });\n/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ \"./node_modules/@angular/core/fesm5/core.js\");\n/* harmony import */ var Example_Http__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! Example/Http */ \"./Core/Example/Http.ts\");\n/* harmony import */ var codeshell_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! codeshell/core */ \"./Core/codeshell/core.ts\");\n/* harmony import */ var _codeshell_helpers__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../codeshell/helpers */ \"./Core/codeshell/helpers.ts\");\n/* harmony import */ var _NavigationPageListBase__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./NavigationPageListBase */ \"./Core/Example/NavigationGroups/NavigationPageListBase.ts\");\n/* harmony import */ var _NaveListBase__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./NaveListBase */ \"./Core/Example/NavigationGroups/NaveListBase.ts\");\n/* harmony import */ var Example_TenantComponentBase__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! Example/TenantComponentBase */ \"./Core/Example/TenantComponentBase.ts\");\nvar __extends = (undefined && undefined.__extends) || (function () {\r\n    var extendStatics = Object.setPrototypeOf ||\r\n        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||\r\n        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };\r\n    return function (d, b) {\r\n        extendStatics(d, b);\r\n        function __() { this.constructor = d; }\r\n        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());\r\n    };\r\n})();\r\nvar __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {\r\n    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;\r\n    if (typeof Reflect === \"object\" && typeof Reflect.decorate === \"function\") r = Reflect.decorate(decorators, target, key, desc);\r\n    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;\r\n    return c > 3 && r && Object.defineProperty(target, key, r), r;\r\n};\r\nvar __metadata = (undefined && undefined.__metadata) || function (k, v) {\r\n    if (typeof Reflect === \"object\" && typeof Reflect.metadata === \"function\") return Reflect.metadata(k, v);\r\n};\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\nvar NavGroupPagesBase = /** @class */ (function (_super) {\r\n    __extends(NavGroupPagesBase, _super);\r\n    function NavGroupPagesBase() {\r\n        return _super !== null && _super.apply(this, arguments) || this;\r\n    }\r\n    Object.defineProperty(NavGroupPagesBase.prototype, \"Service\", {\r\n        get: function () { return codeshell_core__WEBPACK_IMPORTED_MODULE_2__[\"Shell\"].Injector.get(Example_Http__WEBPACK_IMPORTED_MODULE_1__[\"NavigationGroupsService\"]); },\r\n        enumerable: true,\r\n        configurable: true\r\n    });\r\n    NavGroupPagesBase.prototype.OnReady = function () {\r\n        codeshell_core__WEBPACK_IMPORTED_MODULE_2__[\"Shell\"].Main.SideBarStatus.emit(false);\r\n        if (this.model.tenantCode) {\r\n            this.loadNaveGroupPages();\r\n        }\r\n    };\r\n    NavGroupPagesBase.prototype.getPages = function (item) {\r\n        if (this.NavigationPageList) {\r\n            this.Service.naveId = item;\r\n            this.NavigationPageList.tenantId = this.getTenantId();\r\n            this.NavigationPageList.navigationGroupId = item;\r\n            this.NavigationPageList.LoadData();\r\n        }\r\n    };\r\n    NavGroupPagesBase.prototype.AddPage = function () {\r\n        if (this.NavigationPageList) {\r\n            this.NavigationPageList.AddPages();\r\n        }\r\n    };\r\n    NavGroupPagesBase.prototype.tenantChanged = function () {\r\n        _super.prototype.tenantChanged.call(this);\r\n        if (this.NavigationPageList) {\r\n            var id = this.getTenantId();\r\n            if (id)\r\n                this.NavigationPageList.TenantChanged(id);\r\n        }\r\n        this.loadNaveGroupPages();\r\n    };\r\n    NavGroupPagesBase.prototype.loadNaveGroupPages = function () {\r\n        if (this.NaveList) {\r\n            this.NaveList.LoadData();\r\n        }\r\n    };\r\n    NavGroupPagesBase.prototype.save = function () {\r\n        var _this = this;\r\n        if (this.NavigationPageList) {\r\n            var changed = _codeshell_helpers__WEBPACK_IMPORTED_MODULE_3__[\"ListItem\"].GetChangedItems(this.NavigationPageList.list);\r\n            this.Service.Post(\"Create\", changed).then(function (res) {\r\n                _this.Notify(\"Changed Successfully\", _codeshell_helpers__WEBPACK_IMPORTED_MODULE_3__[\"NoteType\"].Success);\r\n                if (_this.NavigationPageList) {\r\n                    _this.NavigationPageList.LoadData();\r\n                }\r\n            });\r\n        }\r\n    };\r\n    __decorate([\r\n        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__[\"ViewChild\"])(\"NaveList\"),\r\n        __metadata(\"design:type\", _NaveListBase__WEBPACK_IMPORTED_MODULE_5__[\"NaveListBase\"])\r\n    ], NavGroupPagesBase.prototype, \"NaveList\", void 0);\r\n    __decorate([\r\n        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__[\"ViewChild\"])(\"NavigationPageList\"),\r\n        __metadata(\"design:type\", _NavigationPageListBase__WEBPACK_IMPORTED_MODULE_4__[\"NavigationPageListBase\"])\r\n    ], NavGroupPagesBase.prototype, \"NavigationPageList\", void 0);\r\n    NavGroupPagesBase = __decorate([\r\n        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__[\"Injectable\"])()\r\n    ], NavGroupPagesBase);\r\n    return NavGroupPagesBase;\r\n}(Example_TenantComponentBase__WEBPACK_IMPORTED_MODULE_6__[\"TenantComponentBase\"]));\r\n\r\n\n\n//# sourceURL=webpack:///./Core/Example/NavigationGroups/NavGroupPagesBase.ts?");

/***/ }),

/***/ "./Core/Example/TenantComponentBase.ts":
/*!*********************************************!*\
  !*** ./Core/Example/TenantComponentBase.ts ***!
  \*********************************************/
/*! exports provided: TenantComponentBase */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"TenantComponentBase\", function() { return TenantComponentBase; });\n/* harmony import */ var codeshell_baseComponents__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! codeshell/baseComponents */ \"./Core/codeshell/baseComponents.ts\");\n/* harmony import */ var codeshell_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! codeshell/core */ \"./Core/codeshell/core.ts\");\n/* harmony import */ var _Http__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./Http */ \"./Core/Example/Http.ts\");\nvar __extends = (undefined && undefined.__extends) || (function () {\r\n    var extendStatics = Object.setPrototypeOf ||\r\n        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||\r\n        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };\r\n    return function (d, b) {\r\n        extendStatics(d, b);\r\n        function __() { this.constructor = d; }\r\n        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());\r\n    };\r\n})();\r\n\r\n\r\n\r\nvar TenantComponentBase = /** @class */ (function (_super) {\r\n    __extends(TenantComponentBase, _super);\r\n    function TenantComponentBase() {\r\n        var _this = _super !== null && _super.apply(this, arguments) || this;\r\n        _this.model = {};\r\n        _this.tenants = [];\r\n        return _this;\r\n    }\r\n    Object.defineProperty(TenantComponentBase.prototype, \"App\", {\r\n        get: function () { return codeshell_core__WEBPACK_IMPORTED_MODULE_1__[\"Shell\"].Main; },\r\n        enumerable: true,\r\n        configurable: true\r\n    });\r\n    Object.defineProperty(TenantComponentBase.prototype, \"TenantService\", {\r\n        get: function () { return codeshell_core__WEBPACK_IMPORTED_MODULE_1__[\"Shell\"].Injector.get(_Http__WEBPACK_IMPORTED_MODULE_2__[\"TenantsService\"]); },\r\n        enumerable: true,\r\n        configurable: true\r\n    });\r\n    TenantComponentBase.prototype.tenantChanged = function () {\r\n        this.App.UseState.tenantCode = this.model.tenantCode;\r\n        this.App.SaveState();\r\n    };\r\n    TenantComponentBase.prototype.ngOnInit = function () {\r\n        var _this = this;\r\n        this.TenantService.Get(\"Get\").then(function (res) {\r\n            _this.tenants = res.list;\r\n            if (_this.App.UseState.tenantCode)\r\n                _this.model.tenantCode = _this.App.UseState.tenantCode;\r\n            _this.OnReady();\r\n        });\r\n    };\r\n    TenantComponentBase.prototype.OnReady = function () {\r\n    };\r\n    TenantComponentBase.prototype.getTenantId = function () {\r\n        var _this = this;\r\n        if (this.model.tenantCode) {\r\n            var ten = this.tenants.find(function (d) { return d.code == _this.model.tenantCode; });\r\n            if (ten)\r\n                return ten.id;\r\n        }\r\n        return undefined;\r\n    };\r\n    return TenantComponentBase;\r\n}(codeshell_baseComponents__WEBPACK_IMPORTED_MODULE_0__[\"BaseComponent\"]));\r\n\r\n\n\n//# sourceURL=webpack:///./Core/Example/TenantComponentBase.ts?");

/***/ })

}]);
//# sourceMappingURL=dev__5.js.map