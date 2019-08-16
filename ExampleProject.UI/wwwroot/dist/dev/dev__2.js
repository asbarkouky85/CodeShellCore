(window["webpackJsonp"] = window["webpackJsonp"] || []).push([[2],{

/***/ "./MainApp/app/Auth/AuthModule.ts":
/*!****************************************!*\
  !*** ./MainApp/app/Auth/AuthModule.ts ***!
  \****************************************/
/*! exports provided: AuthModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"AuthModule\", function() { return AuthModule; });\n/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ \"./node_modules/@angular/core/fesm5/core.js\");\n/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ \"./node_modules/@angular/router/fesm5/router.js\");\n/* harmony import */ var _Shared_SharedModule__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../Shared/SharedModule */ \"./MainApp/app/Shared/SharedModule.ts\");\n/* harmony import */ var codeshell_core__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! codeshell/core */ \"./node_modules/codeshell/core.js\");\n/* harmony import */ var _ngx_translate_core__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @ngx-translate/core */ \"./node_modules/@ngx-translate/core/fesm5/ngx-translate-core.js\");\nvar __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {\r\n    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;\r\n    if (typeof Reflect === \"object\" && typeof Reflect.decorate === \"function\") r = Reflect.decorate(decorators, target, key, desc);\r\n    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;\r\n    return c > 3 && r && Object.defineProperty(target, key, r), r;\r\n};\r\nvar __metadata = (undefined && undefined.__metadata) || function (k, v) {\r\n    if (typeof Reflect === \"object\" && typeof Reflect.metadata === \"function\") return Reflect.metadata(k, v);\r\n};\r\n\r\n\r\n\r\n\r\n\r\nvar AuthModule = /** @class */ (function () {\r\n    function AuthModule(trans, conf) {\r\n        trans.setDefaultLang(conf.Locale);\r\n        trans.use(conf.Locale);\r\n    }\r\n    AuthModule = __decorate([\r\n        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__[\"NgModule\"])({\r\n            declarations: [],\r\n            exports: [],\r\n            imports: [\r\n                _Shared_SharedModule__WEBPACK_IMPORTED_MODULE_2__[\"SharedModule\"],\r\n                _angular_router__WEBPACK_IMPORTED_MODULE_1__[\"RouterModule\"].forChild([\r\n                    { path: \"Users\", loadChildren: function () { return new Promise(function (resolve) { __webpack_require__.e(/*! require.ensure */ 0).then((function (require) { resolve(__webpack_require__(/*! ./Users/UsersModule */ \"./MainApp/app/Auth/Users/UsersModule.ts\")['UsersModule']); }).bind(null, __webpack_require__)).catch(__webpack_require__.oe); }); } },\r\n                    { path: \"Roles\", loadChildren: function () { return new Promise(function (resolve) { __webpack_require__.e(/*! require.ensure */ 1).then((function (require) { resolve(__webpack_require__(/*! ./Roles/RolesModule */ \"./MainApp/app/Auth/Roles/RolesModule.ts\")['RolesModule']); }).bind(null, __webpack_require__)).catch(__webpack_require__.oe); }); } },\r\n                ])\r\n            ]\r\n        }),\r\n        __metadata(\"design:paramtypes\", [_ngx_translate_core__WEBPACK_IMPORTED_MODULE_4__[\"TranslateService\"], codeshell_core__WEBPACK_IMPORTED_MODULE_3__[\"ServerConfigBase\"]])\r\n    ], AuthModule);\r\n    return AuthModule;\r\n}());\r\n\r\n\n\n//# sourceURL=webpack:///./MainApp/app/Auth/AuthModule.ts?");

/***/ })

}]);
//# sourceMappingURL=dev__2.js.map