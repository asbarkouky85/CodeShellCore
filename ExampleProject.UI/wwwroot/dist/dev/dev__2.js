(window["webpackJsonp"] = window["webpackJsonp"] || []).push([[2],{

/***/ "./Core/ExampleProject/Auth/Users/Http.ts":
/*!************************************************!*\
  !*** ./Core/ExampleProject/Auth/Users/Http.ts ***!
  \************************************************/
/*! exports provided: UsersService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony import */ var _Http_UsersService__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./Http/UsersService */ \"./Core/ExampleProject/Auth/Users/Http/UsersService.ts\");\n/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, \"UsersService\", function() { return _Http_UsersService__WEBPACK_IMPORTED_MODULE_0__[\"UsersService\"]; });\n\n\r\n\n\n//# sourceURL=webpack:///./Core/ExampleProject/Auth/Users/Http.ts?");

/***/ }),

/***/ "./Core/ExampleProject/Auth/Users/Http/UsersService.ts":
/*!*************************************************************!*\
  !*** ./Core/ExampleProject/Auth/Users/Http/UsersService.ts ***!
  \*************************************************************/
/*! exports provided: UsersService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"UsersService\", function() { return UsersService; });\n/* harmony import */ var codeshell_http__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! codeshell/http */ \"./node_modules/codeshell/http.js\");\n/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ \"./node_modules/@angular/core/fesm5/core.js\");\nvar __extends = (undefined && undefined.__extends) || (function () {\r\n    var extendStatics = function (d, b) {\r\n        extendStatics = Object.setPrototypeOf ||\r\n            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||\r\n            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };\r\n        return extendStatics(d, b);\r\n    };\r\n    return function (d, b) {\r\n        extendStatics(d, b);\r\n        function __() { this.constructor = d; }\r\n        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());\r\n    };\r\n})();\r\nvar __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {\r\n    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;\r\n    if (typeof Reflect === \"object\" && typeof Reflect.decorate === \"function\") r = Reflect.decorate(decorators, target, key, desc);\r\n    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;\r\n    return c > 3 && r && Object.defineProperty(target, key, r), r;\r\n};\r\n\r\n\r\nvar UsersService = /** @class */ (function (_super) {\r\n    __extends(UsersService, _super);\r\n    function UsersService() {\r\n        return _super !== null && _super.apply(this, arguments) || this;\r\n    }\r\n    Object.defineProperty(UsersService.prototype, \"BaseUrl\", {\r\n        get: function () {\r\n            return \"/apiAction/Users\";\r\n        },\r\n        enumerable: true,\r\n        configurable: true\r\n    });\r\n    UsersService = __decorate([\r\n        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__[\"Injectable\"])()\r\n    ], UsersService);\r\n    return UsersService;\r\n}(codeshell_http__WEBPACK_IMPORTED_MODULE_0__[\"EntityHttpService\"]));\r\n\r\n\n\n//# sourceURL=webpack:///./Core/ExampleProject/Auth/Users/Http/UsersService.ts?");

/***/ }),

/***/ "./Core/ExampleProject/ExampleProjectBaseModule.ts":
/*!*********************************************************!*\
  !*** ./Core/ExampleProject/ExampleProjectBaseModule.ts ***!
  \*********************************************************/
/*! exports provided: ExampleProjectBaseModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"ExampleProjectBaseModule\", function() { return ExampleProjectBaseModule; });\n/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ \"./node_modules/@angular/core/fesm5/core.js\");\n/* harmony import */ var codeshell__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! codeshell */ \"./node_modules/codeshell/index.js\");\n/* harmony import */ var _angular_platform_browser__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/platform-browser */ \"./node_modules/@angular/platform-browser/fesm5/platform-browser.js\");\n/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ngx-toastr */ \"./node_modules/ngx-toastr/fesm5/ngx-toastr.js\");\n/* harmony import */ var _angular_platform_browser_animations__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/platform-browser/animations */ \"./node_modules/@angular/platform-browser/fesm5/animations.js\");\n/* harmony import */ var _Main_Login__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./Main/Login */ \"./Core/ExampleProject/Main/Login.ts\");\n/* harmony import */ var _Main_topBar__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./Main/topBar */ \"./Core/ExampleProject/Main/topBar.ts\");\n/* harmony import */ var _Main_navigationSideBar__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./Main/navigationSideBar */ \"./Core/ExampleProject/Main/navigationSideBar.ts\");\n/* harmony import */ var codeshell_core__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! codeshell/core */ \"./node_modules/codeshell/core.js\");\n/* harmony import */ var _ServerConfig__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ./ServerConfig */ \"./Core/ExampleProject/ServerConfig.ts\");\n/* harmony import */ var codeshell_security__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! codeshell/security */ \"./node_modules/codeshell/security.js\");\n/* harmony import */ var _Http_AccountService__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ./Http/AccountService */ \"./Core/ExampleProject/Http/AccountService.ts\");\n/* harmony import */ var _Auth_Users_Http__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! ./Auth/Users/Http */ \"./Core/ExampleProject/Auth/Users/Http.ts\");\nvar __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {\r\n    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;\r\n    if (typeof Reflect === \"object\" && typeof Reflect.decorate === \"function\") r = Reflect.decorate(decorators, target, key, desc);\r\n    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;\r\n    return c > 3 && r && Object.defineProperty(target, key, r), r;\r\n};\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\nvar ExampleProjectBaseModule = /** @class */ (function () {\r\n    function ExampleProjectBaseModule() {\r\n    }\r\n    ExampleProjectBaseModule_1 = ExampleProjectBaseModule;\r\n    ExampleProjectBaseModule.forRoot = function () {\r\n        return {\r\n            ngModule: ExampleProjectBaseModule_1,\r\n            providers: [\r\n                _Auth_Users_Http__WEBPACK_IMPORTED_MODULE_12__[\"UsersService\"],\r\n                { provide: codeshell_core__WEBPACK_IMPORTED_MODULE_8__[\"ServerConfigBase\"], useClass: _ServerConfig__WEBPACK_IMPORTED_MODULE_9__[\"ServerConfig\"] },\r\n                { provide: codeshell_security__WEBPACK_IMPORTED_MODULE_10__[\"AccountServiceBase\"], useClass: _Http_AccountService__WEBPACK_IMPORTED_MODULE_11__[\"AccountService\"] }\r\n            ]\r\n        };\r\n    };\r\n    var ExampleProjectBaseModule_1;\r\n    ExampleProjectBaseModule = ExampleProjectBaseModule_1 = __decorate([\r\n        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__[\"NgModule\"])({\r\n            declarations: [_Main_Login__WEBPACK_IMPORTED_MODULE_5__[\"Login\"], _Main_topBar__WEBPACK_IMPORTED_MODULE_6__[\"topBar\"], _Main_navigationSideBar__WEBPACK_IMPORTED_MODULE_7__[\"navigationSideBar\"]],\r\n            imports: [\r\n                codeshell__WEBPACK_IMPORTED_MODULE_1__[\"CodeShellModule\"].forRoot(),\r\n                _angular_platform_browser__WEBPACK_IMPORTED_MODULE_2__[\"BrowserModule\"],\r\n                _angular_platform_browser_animations__WEBPACK_IMPORTED_MODULE_4__[\"BrowserAnimationsModule\"],\r\n                ngx_toastr__WEBPACK_IMPORTED_MODULE_3__[\"ToastrModule\"].forRoot()\r\n            ],\r\n            exports: [\r\n                codeshell__WEBPACK_IMPORTED_MODULE_1__[\"CodeShellModule\"],\r\n                _angular_platform_browser__WEBPACK_IMPORTED_MODULE_2__[\"BrowserModule\"],\r\n                _angular_platform_browser_animations__WEBPACK_IMPORTED_MODULE_4__[\"BrowserAnimationsModule\"],\r\n                ngx_toastr__WEBPACK_IMPORTED_MODULE_3__[\"ToastrModule\"],\r\n                _Main_Login__WEBPACK_IMPORTED_MODULE_5__[\"Login\"],\r\n                _Main_topBar__WEBPACK_IMPORTED_MODULE_6__[\"topBar\"],\r\n                _Main_navigationSideBar__WEBPACK_IMPORTED_MODULE_7__[\"navigationSideBar\"]\r\n            ]\r\n        })\r\n    ], ExampleProjectBaseModule);\r\n    return ExampleProjectBaseModule;\r\n}());\r\n\r\n\n\n//# sourceURL=webpack:///./Core/ExampleProject/ExampleProjectBaseModule.ts?");

/***/ }),

/***/ "./Core/ExampleProject/Http/AccountService.ts":
/*!****************************************************!*\
  !*** ./Core/ExampleProject/Http/AccountService.ts ***!
  \****************************************************/
/*! exports provided: AccountService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"AccountService\", function() { return AccountService; });\n/* harmony import */ var codeshell_security__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! codeshell/security */ \"./node_modules/codeshell/security.js\");\nvar __extends = (undefined && undefined.__extends) || (function () {\r\n    var extendStatics = function (d, b) {\r\n        extendStatics = Object.setPrototypeOf ||\r\n            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||\r\n            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };\r\n        return extendStatics(d, b);\r\n    };\r\n    return function (d, b) {\r\n        extendStatics(d, b);\r\n        function __() { this.constructor = d; }\r\n        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());\r\n    };\r\n})();\r\n\r\nvar AccountService = /** @class */ (function (_super) {\r\n    __extends(AccountService, _super);\r\n    function AccountService() {\r\n        var _this = _super !== null && _super.apply(this, arguments) || this;\r\n        _this.BaseUrl = \"/apiAction/Account\";\r\n        return _this;\r\n    }\r\n    return AccountService;\r\n}(codeshell_security__WEBPACK_IMPORTED_MODULE_0__[\"AccountServiceBase\"]));\r\n\r\n\n\n//# sourceURL=webpack:///./Core/ExampleProject/Http/AccountService.ts?");

/***/ }),

/***/ "./Core/ExampleProject/Main/navigationSideBar.html":
/*!*********************************************************!*\
  !*** ./Core/ExampleProject/Main/navigationSideBar.html ***!
  \*********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

eval("module.exports = \"\\r\\n<nav class=\\\"navbar-default navbar-static-side\\\" role=\\\"navigation\\\">\\r\\n    <div class=\\\"sidebar-collapse\\\">\\r\\n        <ul class=\\\"nav \\\" id=\\\"side-menu\\\">\\r\\n            <li class=\\\"nav-header  \\\">\\r\\n                <div class=\\\"dropdown profile-element\\\">\\r\\n                    <span>\\r\\n                        <img alt=\\\"image\\\" class=\\\"img-circle profile-picture-sm\\\" src=\\\"/img/default_user.png\\\" />\\r\\n                    </span>\\r\\n                    <a data-toggle=\\\"dropdown\\\" class=\\\"dropdown-toggle\\\" href=\\\"#\\\">\\r\\n                        <span class=\\\"clear\\\">\\r\\n                            <span class=\\\"block m-t-xs\\\">\\r\\n                                <strong class=\\\"font-bold\\\">{{'Words.Welcome' | translate}}</strong>\\r\\n                            </span>\\r\\n                            <span class=\\\"text-muted text-xs block\\\">\\r\\n                                <b class=\\\"caret\\\"></b>\\r\\n                            </span>\\r\\n                        </span>\\r\\n                    </a>\\r\\n                    <ul class=\\\"dropdown-menu animated fadeInRight m-t-xs\\\">\\r\\n                        <li>\\r\\n\\r\\n                        </li>\\r\\n                        <li>\\r\\n                            <a (click)=\\\"Logout()\\\">{{'Words.LogOut' | translate}}</a>\\r\\n                        </li>\\r\\n                    </ul>\\r\\n                </div>\\r\\n\\r\\n            </li>\\r\\n            <li>\\r\\n                <a [routerLink]=\\\"['/']\\\">{{'Words.Main' | translate}}</a>\\r\\n            </li>\\r\\n        </ul>\\r\\n    </div>\\r\\n</nav>\";\n\n//# sourceURL=webpack:///./Core/ExampleProject/Main/navigationSideBar.html?");

/***/ }),

/***/ "./Core/ExampleProject/Main/navigationSideBar.ts":
/*!*******************************************************!*\
  !*** ./Core/ExampleProject/Main/navigationSideBar.ts ***!
  \*******************************************************/
/*! exports provided: navigationSideBar */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"navigationSideBar\", function() { return navigationSideBar; });\n/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ \"./node_modules/@angular/core/fesm5/core.js\");\n/* harmony import */ var codeshell_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! codeshell/core */ \"./node_modules/codeshell/core.js\");\n/* harmony import */ var codeshell_baseComponents__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! codeshell/baseComponents */ \"./node_modules/codeshell/baseComponents.js\");\nvar __extends = (undefined && undefined.__extends) || (function () {\r\n    var extendStatics = function (d, b) {\r\n        extendStatics = Object.setPrototypeOf ||\r\n            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||\r\n            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };\r\n        return extendStatics(d, b);\r\n    };\r\n    return function (d, b) {\r\n        extendStatics(d, b);\r\n        function __() { this.constructor = d; }\r\n        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());\r\n    };\r\n})();\r\nvar __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {\r\n    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;\r\n    if (typeof Reflect === \"object\" && typeof Reflect.decorate === \"function\") r = Reflect.decorate(decorators, target, key, desc);\r\n    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;\r\n    return c > 3 && r && Object.defineProperty(target, key, r), r;\r\n};\r\n\r\n\r\n\r\nvar navigationSideBar = /** @class */ (function (_super) {\r\n    __extends(navigationSideBar, _super);\r\n    function navigationSideBar() {\r\n        var _this = _super !== null && _super.apply(this, arguments) || this;\r\n        _this.isLoggedIn = false;\r\n        return _this;\r\n    }\r\n    navigationSideBar.prototype.GetPageId = function () {\r\n        return 0;\r\n    };\r\n    navigationSideBar.prototype.ngOnInit = function () {\r\n        var _this = this;\r\n        _super.prototype.ngOnInit.call(this);\r\n        this.isLoggedIn = codeshell_core__WEBPACK_IMPORTED_MODULE_1__[\"Shell\"].Session.IsLoggedIn;\r\n        codeshell_core__WEBPACK_IMPORTED_MODULE_1__[\"Shell\"].Session.LogStatus.subscribe(function (v) {\r\n            _this.isLoggedIn = v;\r\n        });\r\n    };\r\n    navigationSideBar.prototype.Logout = function () {\r\n        codeshell_core__WEBPACK_IMPORTED_MODULE_1__[\"Shell\"].Session.EndSession();\r\n        this.Router.navigateByUrl(\"/Login\");\r\n    };\r\n    navigationSideBar = __decorate([\r\n        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__[\"Component\"])({ template: __webpack_require__(/*! ./navigationSideBar.html */ \"./Core/ExampleProject/Main/navigationSideBar.html\"), selector: \"navigationSideBar\" })\r\n    ], navigationSideBar);\r\n    return navigationSideBar;\r\n}(codeshell_baseComponents__WEBPACK_IMPORTED_MODULE_2__[\"BaseComponent\"]));\r\n\r\n\n\n//# sourceURL=webpack:///./Core/ExampleProject/Main/navigationSideBar.ts?");

/***/ }),

/***/ "./Core/ExampleProject/Main/topBar.html":
/*!**********************************************!*\
  !*** ./Core/ExampleProject/Main/topBar.html ***!
  \**********************************************/
/*! no static exports found */
/***/ (function(module, exports) {

eval("module.exports = \"\\r\\n<nav class=\\\"navbar navbar-default navbar-static-top\\\">\\r\\n    <div class=\\\"container-fluid\\\">\\r\\n        <div class=\\\"navbar-header\\\">\\r\\n            \\r\\n                <div class=\\\"navbar-brand\\\">\\r\\n                    <a class=\\\"btn btn-primary \\\" onclick=\\\"slide()\\\" href=\\\"#\\\">\\r\\n                        <i class=\\\"fa fa-bars\\\"></i>\\r\\n                    </a>\\r\\n                </div>\\r\\n           \\r\\n        </div>\\r\\n\\r\\n        <div class=\\\"collapse navbar-collapse pull-last\\\">\\r\\n            <ul class=\\\"nav navbar-nav\\\">\\r\\n                <li>\\r\\n                    <a (click)=\\\"ChangeLang()\\\">\\r\\n                        {{'Words.Lang' | translate}}\\r\\n                    </a>\\r\\n                </li>\\r\\n                <li *ngIf=\\\"!isLoggedIn\\\">\\r\\n                    <a routerLink=\\\"/Login\\\">\\r\\n                        {{'Words.Login' | translate}} <i class=\\\"fa fa-sign-in-alt\\\"></i>\\r\\n                    </a>\\r\\n                </li>\\r\\n                <li *ngIf=\\\"isLoggedIn\\\">\\r\\n                    <a (click)=\\\"Logout()\\\">\\r\\n                        {{'Words.Logout' | translate}} <i class=\\\"fa fa-sign-out-alt\\\"></i>\\r\\n                    </a>\\r\\n                </li>\\r\\n            </ul>\\r\\n        </div>\\r\\n    </div>\\r\\n\\r\\n</nav>\\r\\n\";\n\n//# sourceURL=webpack:///./Core/ExampleProject/Main/topBar.html?");

/***/ }),

/***/ "./Core/ExampleProject/Main/topBar.ts":
/*!********************************************!*\
  !*** ./Core/ExampleProject/Main/topBar.ts ***!
  \********************************************/
/*! exports provided: topBar */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"topBar\", function() { return topBar; });\n/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ \"./node_modules/@angular/core/fesm5/core.js\");\n/* harmony import */ var codeshell_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! codeshell/core */ \"./node_modules/codeshell/core.js\");\n/* harmony import */ var codeshell_baseComponents__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! codeshell/baseComponents */ \"./node_modules/codeshell/baseComponents.js\");\n/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/common/http */ \"./node_modules/@angular/common/fesm5/http.js\");\nvar __extends = (undefined && undefined.__extends) || (function () {\r\n    var extendStatics = function (d, b) {\r\n        extendStatics = Object.setPrototypeOf ||\r\n            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||\r\n            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };\r\n        return extendStatics(d, b);\r\n    };\r\n    return function (d, b) {\r\n        extendStatics(d, b);\r\n        function __() { this.constructor = d; }\r\n        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());\r\n    };\r\n})();\r\nvar __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {\r\n    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;\r\n    if (typeof Reflect === \"object\" && typeof Reflect.decorate === \"function\") r = Reflect.decorate(decorators, target, key, desc);\r\n    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;\r\n    return c > 3 && r && Object.defineProperty(target, key, r), r;\r\n};\r\n\r\n\r\n\r\n\r\nvar topBar = /** @class */ (function (_super) {\r\n    __extends(topBar, _super);\r\n    function topBar() {\r\n        var _this = _super !== null && _super.apply(this, arguments) || this;\r\n        _this.isLoggedIn = false;\r\n        return _this;\r\n    }\r\n    topBar.prototype.GetPageId = function () {\r\n        throw new Error(\"Method not implemented.\");\r\n    };\r\n    topBar.prototype.ngOnInit = function () {\r\n        var _this = this;\r\n        this.isLoggedIn = codeshell_core__WEBPACK_IMPORTED_MODULE_1__[\"Shell\"].Session.IsLoggedIn;\r\n        codeshell_core__WEBPACK_IMPORTED_MODULE_1__[\"Shell\"].Session.LogStatus.subscribe(function (v) { return _this.isLoggedIn = v; });\r\n    };\r\n    topBar.prototype.Logout = function () {\r\n        codeshell_core__WEBPACK_IMPORTED_MODULE_1__[\"Shell\"].Session.EndSession();\r\n        this.isLoggedIn = false;\r\n        this.Router.navigateByUrl(\"/Login\");\r\n    };\r\n    topBar.prototype.Slide = function () {\r\n        $(\".content-wrapper\").toggleClass(\"expanded\");\r\n        $(\".nav-wrapper\").toggleClass(\"compressed\");\r\n    };\r\n    topBar.prototype.ChangeLang = function () {\r\n        var cl = codeshell_core__WEBPACK_IMPORTED_MODULE_1__[\"Shell\"].Injector.get(_angular_common_http__WEBPACK_IMPORTED_MODULE_3__[\"HttpClient\"]);\r\n        var conf = codeshell_core__WEBPACK_IMPORTED_MODULE_1__[\"Shell\"].Injector.get(codeshell_core__WEBPACK_IMPORTED_MODULE_1__[\"ServerConfigBase\"]);\r\n        cl.get(\"/Home/SetLocale/?lang=\" + (conf.Locale == 'ar' ? 'en' : 'ar')).subscribe(function (d) {\r\n            location.reload();\r\n        });\r\n    };\r\n    topBar = __decorate([\r\n        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__[\"Component\"])({ template: __webpack_require__(/*! ./topBar.html */ \"./Core/ExampleProject/Main/topBar.html\"), selector: \"top-bar\" })\r\n    ], topBar);\r\n    return topBar;\r\n}(codeshell_baseComponents__WEBPACK_IMPORTED_MODULE_2__[\"BaseComponent\"]));\r\n\r\n\n\n//# sourceURL=webpack:///./Core/ExampleProject/Main/topBar.ts?");

/***/ }),

/***/ "./Core/ExampleProject/ServerConfig.ts":
/*!*********************************************!*\
  !*** ./Core/ExampleProject/ServerConfig.ts ***!
  \*********************************************/
/*! exports provided: ServerConfig */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"ServerConfig\", function() { return ServerConfig; });\nvar ServerConfig = /** @class */ (function () {\r\n    function ServerConfig() {\r\n        this.GoogleKey = \"\";\r\n        this.BaseURL = \"\";\r\n        this.Domain = \"\";\r\n        this.Locale = \"\";\r\n        this.Urls = {};\r\n        var item = document.getElementById(\"view-data\");\r\n        if (item)\r\n            Object.assign(this, JSON.parse(item.innerHTML));\r\n    }\r\n    return ServerConfig;\r\n}());\r\n\r\n\n\n//# sourceURL=webpack:///./Core/ExampleProject/ServerConfig.ts?");

/***/ }),

/***/ "./MainApp/app/Auth/AuthModule.ts":
/*!****************************************!*\
  !*** ./MainApp/app/Auth/AuthModule.ts ***!
  \****************************************/
/*! exports provided: AuthModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"AuthModule\", function() { return AuthModule; });\n/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ \"./node_modules/@angular/core/fesm5/core.js\");\n/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ \"./node_modules/@angular/router/fesm5/router.js\");\n/* harmony import */ var _Shared_SharedModule__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../Shared/SharedModule */ \"./MainApp/app/Shared/SharedModule.ts\");\n/* harmony import */ var codeshell_core__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! codeshell/core */ \"./node_modules/codeshell/core.js\");\n/* harmony import */ var _ngx_translate_core__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @ngx-translate/core */ \"./node_modules/@ngx-translate/core/fesm5/ngx-translate-core.js\");\nvar __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {\r\n    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;\r\n    if (typeof Reflect === \"object\" && typeof Reflect.decorate === \"function\") r = Reflect.decorate(decorators, target, key, desc);\r\n    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;\r\n    return c > 3 && r && Object.defineProperty(target, key, r), r;\r\n};\r\nvar __metadata = (undefined && undefined.__metadata) || function (k, v) {\r\n    if (typeof Reflect === \"object\" && typeof Reflect.metadata === \"function\") return Reflect.metadata(k, v);\r\n};\r\n\r\n\r\n\r\n\r\n\r\nvar AuthModule = /** @class */ (function () {\r\n    function AuthModule(trans, conf) {\r\n        trans.setDefaultLang(conf.Locale);\r\n        trans.use(conf.Locale);\r\n    }\r\n    AuthModule = __decorate([\r\n        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__[\"NgModule\"])({\r\n            declarations: [],\r\n            exports: [],\r\n            imports: [\r\n                _Shared_SharedModule__WEBPACK_IMPORTED_MODULE_2__[\"SharedModule\"],\r\n                _angular_router__WEBPACK_IMPORTED_MODULE_1__[\"RouterModule\"].forChild([\r\n                    { path: \"Users\", loadChildren: function () { return new Promise(function (resolve) { __webpack_require__.e(/*! require.ensure */ 0).then((function (require) { resolve(__webpack_require__(/*! ./Users/UsersModule */ \"./MainApp/app/Auth/Users/UsersModule.ts\")['UsersModule']); }).bind(null, __webpack_require__)).catch(__webpack_require__.oe); }); } },\r\n                ])\r\n            ]\r\n        }),\r\n        __metadata(\"design:paramtypes\", [_ngx_translate_core__WEBPACK_IMPORTED_MODULE_4__[\"TranslateService\"], codeshell_core__WEBPACK_IMPORTED_MODULE_3__[\"ServerConfigBase\"]])\r\n    ], AuthModule);\r\n    return AuthModule;\r\n}());\r\n\r\n\n\n//# sourceURL=webpack:///./MainApp/app/Auth/AuthModule.ts?");

/***/ }),

/***/ "./MainApp/app/Shared/SharedModule.ts":
/*!********************************************!*\
  !*** ./MainApp/app/Shared/SharedModule.ts ***!
  \********************************************/
/*! exports provided: SharedModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"SharedModule\", function() { return SharedModule; });\n/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ \"./node_modules/@angular/core/fesm5/core.js\");\n/* harmony import */ var _ngx_translate_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @ngx-translate/core */ \"./node_modules/@ngx-translate/core/fesm5/ngx-translate-core.js\");\n/* harmony import */ var ExampleProject_ExampleProjectBaseModule__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ExampleProject/ExampleProjectBaseModule */ \"./Core/ExampleProject/ExampleProjectBaseModule.ts\");\n/* harmony import */ var codeshell_core__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! codeshell/core */ \"./node_modules/codeshell/core.js\");\nvar __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {\r\n    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;\r\n    if (typeof Reflect === \"object\" && typeof Reflect.decorate === \"function\") r = Reflect.decorate(decorators, target, key, desc);\r\n    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;\r\n    return c > 3 && r && Object.defineProperty(target, key, r), r;\r\n};\r\nvar __metadata = (undefined && undefined.__metadata) || function (k, v) {\r\n    if (typeof Reflect === \"object\" && typeof Reflect.metadata === \"function\") return Reflect.metadata(k, v);\r\n};\r\n\r\n\r\n\r\n\r\nvar SharedModule = /** @class */ (function () {\r\n    function SharedModule(trans, conf) {\r\n        trans.setDefaultLang(conf.Locale);\r\n        trans.use(conf.Locale);\r\n    }\r\n    SharedModule = __decorate([\r\n        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__[\"NgModule\"])({\r\n            declarations: [],\r\n            exports: [\r\n                ExampleProject_ExampleProjectBaseModule__WEBPACK_IMPORTED_MODULE_2__[\"ExampleProjectBaseModule\"]\r\n            ],\r\n            imports: [\r\n                ExampleProject_ExampleProjectBaseModule__WEBPACK_IMPORTED_MODULE_2__[\"ExampleProjectBaseModule\"]\r\n            ],\r\n            entryComponents: []\r\n        }),\r\n        __metadata(\"design:paramtypes\", [_ngx_translate_core__WEBPACK_IMPORTED_MODULE_1__[\"TranslateService\"], codeshell_core__WEBPACK_IMPORTED_MODULE_3__[\"ServerConfigBase\"]])\r\n    ], SharedModule);\r\n    return SharedModule;\r\n}());\r\n\r\n\n\n//# sourceURL=webpack:///./MainApp/app/Shared/SharedModule.ts?");

/***/ }),

/***/ "./node_modules/@angular/http/fesm5/http.js":
/*!**************************************************************************************!*\
  !*** delegated ./node_modules/@angular/http/fesm5/http.js from dll-reference vendor ***!
  \**************************************************************************************/
/*! exports provided: ɵangular_packages_http_http_e, ɵangular_packages_http_http_f, ɵangular_packages_http_http_a, ɵangular_packages_http_http_b, ɵangular_packages_http_http_c, BrowserXhr, JSONPBackend, JSONPConnection, CookieXSRFStrategy, XHRBackend, XHRConnection, BaseRequestOptions, RequestOptions, BaseResponseOptions, ResponseOptions, ReadyState, RequestMethod, ResponseContentType, ResponseType, Headers, Http, Jsonp, HttpModule, JsonpModule, Connection, ConnectionBackend, XSRFStrategy, Request, Response, QueryEncoder, URLSearchParams, VERSION */
/***/ (function(module, exports, __webpack_require__) {

eval("module.exports = (__webpack_require__(/*! dll-reference vendor */ \"dll-reference vendor\"))(\"./node_modules/@angular/http/fesm5/http.js\");\n\n//# sourceURL=webpack:///delegated_./node_modules/@angular/http/fesm5/http.js_from_dll-reference_vendor?");

/***/ }),

/***/ "./node_modules/@angular/material-moment-adapter/esm5/material-moment-adapter.es5.js":
/*!*******************************************************************************************************************************!*\
  !*** delegated ./node_modules/@angular/material-moment-adapter/esm5/material-moment-adapter.es5.js from dll-reference vendor ***!
  \*******************************************************************************************************************************/
/*! exports provided: MomentDateModule, MatMomentDateModule, MAT_MOMENT_DATE_ADAPTER_OPTIONS_FACTORY, MAT_MOMENT_DATE_ADAPTER_OPTIONS, MomentDateAdapter, MAT_MOMENT_DATE_FORMATS */
/***/ (function(module, exports, __webpack_require__) {

eval("module.exports = (__webpack_require__(/*! dll-reference vendor */ \"dll-reference vendor\"))(\"./node_modules/@angular/material-moment-adapter/esm5/material-moment-adapter.es5.js\");\n\n//# sourceURL=webpack:///delegated_./node_modules/@angular/material-moment-adapter/esm5/material-moment-adapter.es5.js_from_dll-reference_vendor?");

/***/ }),

/***/ "./node_modules/primeng/components/common/shared.js":
/*!**********************************************************************************************!*\
  !*** delegated ./node_modules/primeng/components/common/shared.js from dll-reference vendor ***!
  \**********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

eval("module.exports = (__webpack_require__(/*! dll-reference vendor */ \"dll-reference vendor\"))(\"./node_modules/primeng/components/common/shared.js\");\n\n//# sourceURL=webpack:///delegated_./node_modules/primeng/components/common/shared.js_from_dll-reference_vendor?");

/***/ }),

/***/ "./node_modules/primeng/components/dom/domhandler.js":
/*!***********************************************************************************************!*\
  !*** delegated ./node_modules/primeng/components/dom/domhandler.js from dll-reference vendor ***!
  \***********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

eval("module.exports = (__webpack_require__(/*! dll-reference vendor */ \"dll-reference vendor\"))(\"./node_modules/primeng/components/dom/domhandler.js\");\n\n//# sourceURL=webpack:///delegated_./node_modules/primeng/components/dom/domhandler.js_from_dll-reference_vendor?");

/***/ }),

/***/ "./node_modules/primeng/dialog.js":
/*!****************************************************************************!*\
  !*** delegated ./node_modules/primeng/dialog.js from dll-reference vendor ***!
  \****************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

eval("module.exports = (__webpack_require__(/*! dll-reference vendor */ \"dll-reference vendor\"))(\"./node_modules/primeng/dialog.js\");\n\n//# sourceURL=webpack:///delegated_./node_modules/primeng/dialog.js_from_dll-reference_vendor?");

/***/ }),

/***/ "./node_modules/rxjs/_esm5/operators/index.js":
/*!****************************************************************************************!*\
  !*** delegated ./node_modules/rxjs/_esm5/operators/index.js from dll-reference vendor ***!
  \****************************************************************************************/
/*! exports provided: audit, auditTime, buffer, bufferCount, bufferTime, bufferToggle, bufferWhen, catchError, combineAll, combineLatest, concat, concatAll, concatMap, concatMapTo, count, debounce, debounceTime, defaultIfEmpty, delay, delayWhen, dematerialize, distinct, distinctUntilChanged, distinctUntilKeyChanged, elementAt, endWith, every, exhaust, exhaustMap, expand, filter, finalize, find, findIndex, first, groupBy, ignoreElements, isEmpty, last, map, mapTo, materialize, max, merge, mergeAll, mergeMap, flatMap, mergeMapTo, mergeScan, min, multicast, observeOn, onErrorResumeNext, pairwise, partition, pluck, publish, publishBehavior, publishLast, publishReplay, race, reduce, repeat, repeatWhen, retry, retryWhen, refCount, sample, sampleTime, scan, sequenceEqual, share, shareReplay, single, skip, skipLast, skipUntil, skipWhile, startWith, subscribeOn, switchAll, switchMap, switchMapTo, take, takeLast, takeUntil, takeWhile, tap, throttle, throttleTime, throwIfEmpty, timeInterval, timeout, timeoutWith, timestamp, toArray, window, windowCount, windowTime, windowToggle, windowWhen, withLatestFrom, zip, zipAll */
/***/ (function(module, exports, __webpack_require__) {

eval("module.exports = (__webpack_require__(/*! dll-reference vendor */ \"dll-reference vendor\"))(\"./node_modules/rxjs/_esm5/operators/index.js\");\n\n//# sourceURL=webpack:///delegated_./node_modules/rxjs/_esm5/operators/index.js_from_dll-reference_vendor?");

/***/ }),

/***/ "./node_modules/tslib/tslib.es6.js":
/*!*****************************************************************************!*\
  !*** delegated ./node_modules/tslib/tslib.es6.js from dll-reference vendor ***!
  \*****************************************************************************/
/*! exports provided: __extends, __assign, __rest, __decorate, __param, __metadata, __awaiter, __generator, __exportStar, __values, __read, __spread, __spreadArrays, __await, __asyncGenerator, __asyncDelegator, __asyncValues, __makeTemplateObject, __importStar, __importDefault */
/***/ (function(module, exports, __webpack_require__) {

eval("module.exports = (__webpack_require__(/*! dll-reference vendor */ \"dll-reference vendor\"))(\"./node_modules/tslib/tslib.es6.js\");\n\n//# sourceURL=webpack:///delegated_./node_modules/tslib/tslib.es6.js_from_dll-reference_vendor?");

/***/ })

}]);
//# sourceMappingURL=dev__2.js.map