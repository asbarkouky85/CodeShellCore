(function () {
  function _get(target, property, receiver) { if (typeof Reflect !== "undefined" && Reflect.get) { _get = Reflect.get; } else { _get = function _get(target, property, receiver) { var base = _superPropBase(target, property); if (!base) return; var desc = Object.getOwnPropertyDescriptor(base, property); if (desc.get) { return desc.get.call(receiver); } return desc.value; }; } return _get(target, property, receiver || target); }

  function _superPropBase(object, property) { while (!Object.prototype.hasOwnProperty.call(object, property)) { object = _getPrototypeOf(object); if (object === null) break; } return object; }

  function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function"); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, writable: true, configurable: true } }); if (superClass) _setPrototypeOf(subClass, superClass); }

  function _setPrototypeOf(o, p) { _setPrototypeOf = Object.setPrototypeOf || function _setPrototypeOf(o, p) { o.__proto__ = p; return o; }; return _setPrototypeOf(o, p); }

  function _createSuper(Derived) { var hasNativeReflectConstruct = _isNativeReflectConstruct(); return function _createSuperInternal() { var Super = _getPrototypeOf(Derived), result; if (hasNativeReflectConstruct) { var NewTarget = _getPrototypeOf(this).constructor; result = Reflect.construct(Super, arguments, NewTarget); } else { result = Super.apply(this, arguments); } return _possibleConstructorReturn(this, result); }; }

  function _possibleConstructorReturn(self, call) { if (call && (typeof call === "object" || typeof call === "function")) { return call; } return _assertThisInitialized(self); }

  function _assertThisInitialized(self) { if (self === void 0) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return self; }

  function _isNativeReflectConstruct() { if (typeof Reflect === "undefined" || !Reflect.construct) return false; if (Reflect.construct.sham) return false; if (typeof Proxy === "function") return true; try { Date.prototype.toString.call(Reflect.construct(Date, [], function () {})); return true; } catch (e) { return false; } }

  function _getPrototypeOf(o) { _getPrototypeOf = Object.setPrototypeOf ? Object.getPrototypeOf : function _getPrototypeOf(o) { return o.__proto__ || Object.getPrototypeOf(o); }; return _getPrototypeOf(o); }

  function _createForOfIteratorHelper(o, allowArrayLike) { var it; if (typeof Symbol === "undefined" || o[Symbol.iterator] == null) { if (Array.isArray(o) || (it = _unsupportedIterableToArray(o)) || allowArrayLike && o && typeof o.length === "number") { if (it) o = it; var i = 0; var F = function F() {}; return { s: F, n: function n() { if (i >= o.length) return { done: true }; return { done: false, value: o[i++] }; }, e: function e(_e) { throw _e; }, f: F }; } throw new TypeError("Invalid attempt to iterate non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method."); } var normalCompletion = true, didErr = false, err; return { s: function s() { it = o[Symbol.iterator](); }, n: function n() { var step = it.next(); normalCompletion = step.done; return step; }, e: function e(_e2) { didErr = true; err = _e2; }, f: function f() { try { if (!normalCompletion && it["return"] != null) it["return"](); } finally { if (didErr) throw err; } } }; }

  function _unsupportedIterableToArray(o, minLen) { if (!o) return; if (typeof o === "string") return _arrayLikeToArray(o, minLen); var n = Object.prototype.toString.call(o).slice(8, -1); if (n === "Object" && o.constructor) n = o.constructor.name; if (n === "Map" || n === "Set") return Array.from(o); if (n === "Arguments" || /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(n)) return _arrayLikeToArray(o, minLen); }

  function _arrayLikeToArray(arr, len) { if (len == null || len > arr.length) len = arr.length; for (var i = 0, arr2 = new Array(len); i < len; i++) { arr2[i] = arr[i]; } return arr2; }

  function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

  function _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }

  function _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }

  (window["webpackJsonp"] = window["webpackJsonp"] || []).push([["main"], {
    /***/
    "+K43":
    /*!************************************************************************!*\
      !*** ./src/core/codeshell/base-components/navigation-side-bar-base.ts ***!
      \************************************************************************/

    /*! exports provided: NavigationSideBarBase */

    /***/
    function K43(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "NavigationSideBarBase", function () {
        return NavigationSideBarBase;
      });
      /* harmony import */


      var codeshell_security__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! codeshell/security */
      "U6Sh");
      /* harmony import */


      var codeshell_shell__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! codeshell/shell */
      "UoIT");
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var _angular_router__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! @angular/router */
      "tyNb");
      /* harmony import */


      var codeshell_utilities_utils__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
      /*! codeshell/utilities/utils */
      "VXkE");

      var NavigationSideBarBase = /*#__PURE__*/function () {
        function NavigationSideBarBase(Injector) {
          _classCallCheck(this, NavigationSideBarBase);

          this.Injector = Injector;
          this.isLoggedIn = false;
          this.navs = [];
        }

        _createClass(NavigationSideBarBase, [{
          key: "Router",
          get: function get() {
            return this.Injector.get(_angular_router__WEBPACK_IMPORTED_MODULE_3__["Router"]);
          }
        }, {
          key: "ngOnInit",
          value: function ngOnInit() {
            var _this = this;

            codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Session.LogStatus.subscribe(function (v) {
              _this.isLoggedIn = v;

              _this.LoadNavigation();
            });
            codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Session.GetUserAsync().then(function (user) {
              _this.user = user;
              _this.isLoggedIn = true;

              _this.LoadNavigation();
            })["catch"](function (d) {
              _this.LoadNavigation();
            });
            this.OnReady();
          }
        }, {
          key: "OnReady",
          value: function OnReady() {}
        }, {
          key: "GetMainUrl",
          value: function GetMainUrl() {
            return "/";
          }
        }, {
          key: "GotoMain",
          value: function GotoMain() {
            var main = Object(codeshell_utilities_utils__WEBPACK_IMPORTED_MODULE_4__["absUrl"])(this.GetMainUrl());
            console.log(main);
            this.Router.navigateByUrl(main);
          }
        }, {
          key: "LoadNavigation",
          value: function LoadNavigation() {
            var auth = codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Injector.get(codeshell_security__WEBPACK_IMPORTED_MODULE_0__["AuthorizationServiceBase"]);
            var doms = codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Injector.get(codeshell_security__WEBPACK_IMPORTED_MODULE_0__["DomainDataProvider"]);

            var _iterator = _createForOfIteratorHelper(doms.Domains),
                _step;

            try {
              for (_iterator.s(); !(_step = _iterator.n()).done;) {
                var dom = _step.value;

                if (dom.name == "Main") {
                  var _iterator2 = _createForOfIteratorHelper(dom.children),
                      _step2;

                  try {
                    for (_iterator2.s(); !(_step2 = _iterator2.n()).done;) {
                      var c = _step2.value;
                      var r = Object.assign(new codeshell_security__WEBPACK_IMPORTED_MODULE_0__["RouteData"](), c);

                      if (auth.IsAuthorized(this.user, r) && r.url) {
                        var item = {
                          name: r.name,
                          url: r.url
                        };
                        this.navs.push(item);
                      }
                    }
                  } catch (err) {
                    _iterator2.e(err);
                  } finally {
                    _iterator2.f();
                  }
                }
              }
            } catch (err) {
              _iterator.e(err);
            } finally {
              _iterator.f();
            }
          }
        }, {
          key: "Logout",
          value: function Logout() {
            codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Session.EndSession();
            location.pathname = "/";
          }
        }]);

        return NavigationSideBarBase;
      }();

      NavigationSideBarBase.ɵfac = function NavigationSideBarBase_Factory(t) {
        return new (t || NavigationSideBarBase)(_angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_2__["Injector"]));
      };

      NavigationSideBarBase.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵdefineComponent"]({
        type: NavigationSideBarBase,
        selectors: [["ng-component"]],
        decls: 0,
        vars: 0,
        template: function NavigationSideBarBase_Template(rf, ctx) {},
        encapsulation: 2
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵsetClassMetadata"](NavigationSideBarBase, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_2__["Component"],
          args: [{
            template: ''
          }]
        }], function () {
          return [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_2__["Injector"]
          }];
        }, null);
      })();
      /***/

    },

    /***/
    0:
    /*!***************************!*\
      !*** multi ./src/main.ts ***!
      \***************************/

    /*! no static exports found */

    /***/
    function _(module, exports, __webpack_require__) {
      module.exports = __webpack_require__(
      /*! C:\_abdelrahman\Personal\_gitHub\CodeShellCore\Example.UI\src\main.ts */
      "zUnb");
      /***/
    },

    /***/
    "079c":
    /*!**************************************************!*\
      !*** ./src/core/codeshell/base-module.module.ts ***!
      \**************************************************/

    /*! exports provided: BaseModule */

    /***/
    function c(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "BaseModule", function () {
        return BaseModule;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! @angular/router */
      "tyNb");
      /* harmony import */


      var _shell__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! ./shell */
      "UoIT");
      /* harmony import */


      var _localization_translationService__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! ./localization/translationService */
      "0ujZ");
      /* harmony import */


      var _serverConfigBase__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
      /*! ./serverConfigBase */
      "6m0k");

      var BaseModule = function BaseModule(trans, router, conf) {
        _classCallCheck(this, BaseModule);

        trans.setDefaultLang(conf.Locale);
        trans.use(conf.Locale);
        router.events.subscribe(function (event) {
          if (event instanceof _angular_router__WEBPACK_IMPORTED_MODULE_1__["RouteConfigLoadStart"]) {
            _shell__WEBPACK_IMPORTED_MODULE_2__["Shell"].Main.ShowLoader = true;
          }

          if (event instanceof _angular_router__WEBPACK_IMPORTED_MODULE_1__["RouteConfigLoadEnd"]) {
            _shell__WEBPACK_IMPORTED_MODULE_2__["Shell"].Main.ShowLoader = false;
          }
        });
      };

      BaseModule.ɵfac = function BaseModule_Factory(t) {
        return new (t || BaseModule)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵinject"](_localization_translationService__WEBPACK_IMPORTED_MODULE_3__["TranslationService"]), _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵinject"](_angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"]), _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵinject"](_serverConfigBase__WEBPACK_IMPORTED_MODULE_4__["ServerConfigBase"]));
      };

      BaseModule.ɵprov = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineInjectable"]({
        token: BaseModule,
        factory: BaseModule.ɵfac
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](BaseModule, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"]
        }], function () {
          return [{
            type: _localization_translationService__WEBPACK_IMPORTED_MODULE_3__["TranslationService"]
          }, {
            type: _angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"]
          }, {
            type: _serverConfigBase__WEBPACK_IMPORTED_MODULE_4__["ServerConfigBase"]
          }];
        }, null);
      })();
      /***/

    },

    /***/
    "0Wec":
    /*!*************************************************************!*\
      !*** ./src/core/codeshell/services/listSelectionService.ts ***!
      \*************************************************************/

    /*! exports provided: ListSelectionService */

    /***/
    function Wec(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "ListSelectionService", function () {
        return ListSelectionService;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var codeshell_main__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! codeshell/main */
      "7OZ3");

      var ListSelectionService = /*#__PURE__*/function () {
        function ListSelectionService() {
          _classCallCheck(this, ListSelectionService);

          this.List = [];
          this.Ids = [];
          this.itemsSelected = false;
          this._last = [];
          this.selectStart = -1;
          this._itemsSelectedChange = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
          this._selectionChange = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        }

        _createClass(ListSelectionService, [{
          key: "ItemsSelectedChange",
          get: function get() {
            return this._itemsSelectedChange;
          }
        }, {
          key: "SelectionChanged",
          get: function get() {
            return this._selectionChange;
          }
        }, {
          key: "SelectedItems",
          get: function get() {
            return this.List.filter(function (d) {
              return d.rowSelected == true;
            });
          }
        }, {
          key: "_updateSelectionState",
          value: function _updateSelectionState() {
            var anySelected = this.Ids.length > 0;
            if (anySelected != this.itemsSelected) this._itemsSelectedChange.emit(anySelected);
            this.itemsSelected = anySelected;
            if (JSON.stringify(this._last) != JSON.stringify(this.Ids)) this._selectionChange.emit();
            this._last = this.Ids;
          }
        }, {
          key: "SetItemSelectionStatus",
          value: function SetItemSelectionStatus(item, status) {
            var only = arguments.length > 2 && arguments[2] !== undefined ? arguments[2] : false;

            if (only) {
              var _iterator3 = _createForOfIteratorHelper(this.List),
                  _step3;

              try {
                for (_iterator3.s(); !(_step3 = _iterator3.n()).done;) {
                  var i = _step3.value;
                  i.rowSelected = false;
                }
              } catch (err) {
                _iterator3.e(err);
              } finally {
                _iterator3.f();
              }

              status = true;
            }

            if (!status) {
              Object(codeshell_main__WEBPACK_IMPORTED_MODULE_1__["List_RemoveItem"])(this.Ids, item.id);
              item.rowSelected = false;
            } else {
              if (only) {
                this.Ids = [item.id];
              } else {
                this.Ids.push(item.id);
              }

              item.rowSelected = true;
            }

            this._updateSelectionState();
          }
        }, {
          key: "ItemClicked",
          value: function ItemClicked(item, event) {
            if (!event.shiftKey) {
              this.selectStart = this.List.indexOf(item);
              this.SetItemSelectionStatus(item, !item.rowSelected, !event.ctrlKey);
            } else {
              var current = this.List.indexOf(item);
              var selStart = current > this.selectStart ? this.selectStart : current;
              var selEnd = current > this.selectStart ? current : this.selectStart;

              for (var i = selStart; i <= selEnd; i++) {
                var it = this.List[i];
                this.SetItemSelectionStatus(it, true);
              }
            }
          }
        }, {
          key: "ClearSelection",
          value: function ClearSelection() {
            var _iterator4 = _createForOfIteratorHelper(this.List),
                _step4;

            try {
              for (_iterator4.s(); !(_step4 = _iterator4.n()).done;) {
                var i = _step4.value;
                i.rowSelected = false;
              }
            } catch (err) {
              _iterator4.e(err);
            } finally {
              _iterator4.f();
            }

            this.Ids = [];

            this._updateSelectionState();
          }
        }]);

        return ListSelectionService;
      }();
      /***/

    },

    /***/
    "0ujZ":
    /*!***************************************************************!*\
      !*** ./src/core/codeshell/localization/translationService.ts ***!
      \***************************************************************/

    /*! exports provided: TranslationService */

    /***/
    function ujZ(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "TranslationService", function () {
        return TranslationService;
      });

      var TranslationService = function TranslationService() {
        _classCallCheck(this, TranslationService);
      };
      /***/

    },

    /***/
    "1Jgf":
    /*!*********************************************!*\
      !*** ./src/core/codeshell/security/navs.ts ***!
      \*********************************************/

    /*! exports provided: ModuleItem, FunctionItem */

    /***/
    function Jgf(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "ModuleItem", function () {
        return ModuleItem;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "FunctionItem", function () {
        return FunctionItem;
      });

      var ModuleItem = function ModuleItem() {
        _classCallCheck(this, ModuleItem);

        this.name = "";
        this.identifier = "";
        this.children = [];
        this.active = false;
      };

      var FunctionItem = function FunctionItem() {
        _classCallCheck(this, FunctionItem);

        this.name = "";
        this.url = "";
      };
      /***/

    },

    /***/
    "2enV":
    /*!***********************************************!*\
      !*** ./src/core/base/http/account.service.ts ***!
      \***********************************************/

    /*! exports provided: AccountService */

    /***/
    function enV(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "AccountService", function () {
        return AccountService;
      });
      /* harmony import */


      var codeshell_security__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! codeshell/security */
      "U6Sh");

      var AccountService = /*#__PURE__*/function (_codeshell_security__) {
        _inherits(AccountService, _codeshell_security__);

        var _super = _createSuper(AccountService);

        function AccountService() {
          var _this2;

          _classCallCheck(this, AccountService);

          _this2 = _super.apply(this, arguments);
          _this2.BaseUrl = "/apiAction/Account";
          return _this2;
        }

        return AccountService;
      }(codeshell_security__WEBPACK_IMPORTED_MODULE_0__["AccountServiceBase"]);
      /***/

    },

    /***/
    "4N+8":
    /*!************************************************!*\
      !*** ./src/core/codeshell/components/index.ts ***!
      \************************************************/

    /*! exports provided: Paginate, SearchGroup, DurationInput */

    /***/
    function N8(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony import */


      var _paginate_component__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! ./paginate.component */
      "Rux6");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "Paginate", function () {
        return _paginate_component__WEBPACK_IMPORTED_MODULE_0__["Paginate"];
      });
      /* harmony import */


      var _search_group_component__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ./search-group.component */
      "L7BY");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "SearchGroup", function () {
        return _search_group_component__WEBPACK_IMPORTED_MODULE_1__["SearchGroup"];
      });
      /* harmony import */


      var _duration_input_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! ./duration-input.component */
      "YNU7");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "DurationInput", function () {
        return _duration_input_component__WEBPACK_IMPORTED_MODULE_2__["DurationInput"];
      });
      /***/

    },

    /***/
    "5Qa7":
    /*!******************************************************************!*\
      !*** ./src/core/codeshell/directives/image-preLoad.directive.ts ***!
      \******************************************************************/

    /*! exports provided: ImagePreLoad */

    /***/
    function Qa7(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "ImagePreLoad", function () {
        return ImagePreLoad;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");

      var ImagePreLoad = /*#__PURE__*/function () {
        function ImagePreLoad(el) {
          var _this3 = this;

          _classCallCheck(this, ImagePreLoad);

          this.el = el;
          this.isLoaded = false;
          var ht = el.nativeElement;
          this.awaiterDiv = document.createElement("div");
          this.awaiterDiv.classList.add("awaiter");
          this.awaiterDiv.style.width = "100%";
          this.awaiterDiv.style.display = "inline-block";
          this.awaiterDiv.style.height = "256px";
          this.awaiterDiv.style.position = "relative";

          for (var i = 0; i < ht.classList.length; i++) {
            var x = ht.classList.item(i);
            if (x) this.awaiterDiv.classList.add(x);
          } //$(ht).parent().append(this.awaiterDiv);
          //$(ht).hide();


          ht.addEventListener("load", function (e) {
            if (ht.complete && ht.naturalWidth !== 0) {
              //$(ht).fadeIn(500);
              //$(this.awaiterDiv).remove();
              _this3.isLoaded = true;
              return {};
            }
          });
          setTimeout(function () {
            if (!_this3.isLoaded) {
              //$(ht).fadeIn(500);
              ht.style.height = _this3.getClosestWidth(ht) + "px"; // $(this.awaiterDiv).remove();

              _this3.isLoaded = true;
            }
          }, 2000);
        }

        _createClass(ImagePreLoad, [{
          key: "getClosestWidth",
          value: function getClosestWidth(el) {
            var w = el.getBoundingClientRect();
            if (w.width > 0) return w.width;else if (el.parentElement) return this.getClosestWidth(el.parentElement);else return 0;
          }
        }, {
          key: "ngOnInit",
          value: function ngOnInit() {
            if (this.awaiterDiv) {
              var _ht = this.el.nativeElement;
              var w = this.getClosestWidth(_ht);
              this.awaiterDiv.style.height = w == 0 ? "256px" : w + "px";
            }
          }
        }]);

        return ImagePreLoad;
      }();

      ImagePreLoad.ɵfac = function ImagePreLoad_Factory(t) {
        return new (t || ImagePreLoad)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]));
      };

      ImagePreLoad.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({
        type: ImagePreLoad,
        selectors: [["", "awaiter", ""]],
        exportAs: ["awaiter"]
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](ImagePreLoad, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
          args: [{
            selector: "[awaiter]",
            exportAs: "awaiter"
          }]
        }], function () {
          return [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]
          }];
        }, null);
      })();
      /***/

    },

    /***/
    "5hkl":
    /*!*******************************************************!*\
      !*** ./src/core/codeshell/helpers/localizablesDTO.ts ***!
      \*******************************************************/

    /*! exports provided: LocalizablesDTO */

    /***/
    function hkl(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "LocalizablesDTO", function () {
        return LocalizablesDTO;
      });
      /* harmony import */


      var _listItem__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! ./listItem */
      "JLZs");

      var LocalizablesDTO = /*#__PURE__*/function (_listItem__WEBPACK_IM) {
        _inherits(LocalizablesDTO, _listItem__WEBPACK_IM);

        var _super2 = _createSuper(LocalizablesDTO);

        function LocalizablesDTO() {
          var _this4;

          _classCallCheck(this, LocalizablesDTO);

          _this4 = _super2.apply(this, arguments);
          _this4.langId = 0;
          _this4.data = {};
          return _this4;
        }

        return LocalizablesDTO;
      }(_listItem__WEBPACK_IMPORTED_MODULE_0__["ListItem"]);
      /***/

    },

    /***/
    "62p0":
    /*!*********************************************************!*\
      !*** ./src/core/codeshell/validators/modalValidator.ts ***!
      \*********************************************************/

    /*! exports provided: ModalValidator */

    /***/
    function p0(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "ModalValidator", function () {
        return ModalValidator;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! @angular/forms */
      "3Pt+");

      var ModalValidator = /*#__PURE__*/function () {
        function ModalValidator(frm) {
          _classCallCheck(this, ModalValidator);

          this.frm = frm;
          this._isValid = true;
          this.fieldName = "";
          this.control = new _angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormControl"]();
        }

        _createClass(ModalValidator, [{
          key: "IsValid",
          get: function get() {
            return this._isValid;
          },
          set: function set(val) {
            if (val) {
              this.control.setErrors({
                required_modal: null
              });
              this.control.setValue("v");
              this.control.updateValueAndValidity();
            } else {
              this.control.setValue(null);
              this.control.setErrors({
                required_modal: true
              }); //this.control.updateValueAndValidity();
            }

            this._isValid = val;
          }
        }, {
          key: "ngOnInit",
          value: function ngOnInit() {
            this.frm.control.addControl(this.fieldName, this.control);
            this.frm.controls[this.fieldName] = this.control;
            this.control.markAsTouched();
            this.IsValid = false;
          }
        }, {
          key: "ngOnDestroy",
          value: function ngOnDestroy() {
            this.frm.control.removeControl(this.fieldName);
          }
        }]);

        return ModalValidator;
      }();

      ModalValidator.ɵfac = function ModalValidator_Factory(t) {
        return new (t || ModalValidator)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_forms__WEBPACK_IMPORTED_MODULE_1__["NgForm"]));
      };

      ModalValidator.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({
        type: ModalValidator,
        selectors: [["", "modal-required", ""]],
        inputs: {
          fieldName: "fieldName",
          IsValid: ["modal-required", "IsValid"]
        },
        exportAs: ["modal-required"]
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](ModalValidator, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
          args: [{
            selector: "[modal-required]",
            exportAs: "modal-required"
          }]
        }], function () {
          return [{
            type: _angular_forms__WEBPACK_IMPORTED_MODULE_1__["NgForm"]
          }];
        }, {
          fieldName: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["fieldName"]
          }],
          IsValid: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["modal-required"]
          }]
        });
      })();
      /***/

    },

    /***/
    "6Ipe":
    /*!****************************************************************!*\
      !*** ./src/core/codeshell/directives/slim-scroll.directive.ts ***!
      \****************************************************************/

    /*! exports provided: SlimScroll */

    /***/
    function Ipe(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "SlimScroll", function () {
        return SlimScroll;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");

      var SlimScroll = /*#__PURE__*/function () {
        function SlimScroll(el) {
          _classCallCheck(this, SlimScroll);

          this.el = el;
          this.height = '250px';
        }

        _createClass(SlimScroll, [{
          key: "ngOnInit",
          value: function ngOnInit() {// $(this.el.nativeElement).slimScroll({
            //     position: 'left',
            //     height: this.height,
            //     railColor: "white",
            //     railOpacity: 0.5
            // });
          }
        }, {
          key: "ngOnChanges",
          value: function ngOnChanges() {}
        }]);

        return SlimScroll;
      }();

      SlimScroll.ɵfac = function SlimScroll_Factory(t) {
        return new (t || SlimScroll)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]));
      };

      SlimScroll.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({
        type: SlimScroll,
        selectors: [["", "slimScroll", ""]],
        inputs: {
          height: ["slimScroll", "height"]
        },
        exportAs: ["slimScroll"],
        features: [_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵNgOnChangesFeature"]]
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](SlimScroll, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
          args: [{
            selector: "[slimScroll]",
            exportAs: "slimScroll"
          }]
        }], function () {
          return [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]
          }];
        }, {
          height: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["slimScroll"]
          }]
        });
      })();
      /***/

    },

    /***/
    "6XeC":
    /*!*****************************************************!*\
      !*** ./src/core/codeshell/helpers/treeEventArgs.ts ***!
      \*****************************************************/

    /*! exports provided: TreeEventArgs */

    /***/
    function XeC(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "TreeEventArgs", function () {
        return TreeEventArgs;
      });

      var TreeEventArgs = function TreeEventArgs(EventName, Node) {
        _classCallCheck(this, TreeEventArgs);

        this.EventName = EventName;
        this.Node = Node;
      };
      /***/

    },

    /***/
    "6m0k":
    /*!************************************************!*\
      !*** ./src/core/codeshell/serverConfigBase.ts ***!
      \************************************************/

    /*! exports provided: ServerConfigBase */

    /***/
    function m0k(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "ServerConfigBase", function () {
        return ServerConfigBase;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");

      var ServerConfigBase = function ServerConfigBase() {
        _classCallCheck(this, ServerConfigBase);

        this.DefaultLocale = "en";
        this.Locale = "en";
        this.Domain = "Auth";
        this.Version = null;
      };

      ServerConfigBase.ɵfac = function ServerConfigBase_Factory(t) {
        return new (t || ServerConfigBase)();
      };

      ServerConfigBase.ɵprov = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineInjectable"]({
        token: ServerConfigBase,
        factory: ServerConfigBase.ɵfac
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](ServerConfigBase, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"]
        }], null, null);
      })();
      /***/

    },

    /***/
    "6mC2":
    /*!*******************************************************************!*\
      !*** ./src/core/codeshell/base-components/selectComponentBase.ts ***!
      \*******************************************************************/

    /*! exports provided: SelectComponentBase */

    /***/
    function mC2(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "SelectComponentBase", function () {
        return SelectComponentBase;
      });
      /* harmony import */


      var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! tslib */
      "mrSG");
      /* harmony import */


      var _baseComponent__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ./baseComponent */
      "I5Ck");
      /* harmony import */


      var _helpers__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! ../helpers */
      "GYDu");
      /* harmony import */


      var _services__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! ../services */
      "RnJ/");
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");

      var SelectComponentBase = /*#__PURE__*/function (_baseComponent__WEBPA) {
        _inherits(SelectComponentBase, _baseComponent__WEBPA);

        var _super3 = _createSuper(SelectComponentBase);

        function SelectComponentBase() {
          var _this5;

          _classCallCheck(this, SelectComponentBase);

          _this5 = _super3.apply(this, arguments);

          _this5.defaultLoader = function (e) {
            return Promise.resolve(new _helpers__WEBPACK_IMPORTED_MODULE_2__["LoadResult"]());
          };

          _this5.Multi = true;
          _this5.Source = new _services__WEBPACK_IMPORTED_MODULE_3__["ListSource"](10, function (e) {
            return _this5.Loader(e);
          });
          _this5._items = [];
          _this5.SelectHeight = "400px";
          _this5.SelectionChangedEvent = new _angular_core__WEBPACK_IMPORTED_MODULE_4__["EventEmitter"]();
          return _this5;
        }

        _createClass(SelectComponentBase, [{
          key: "Loader",
          get: function get() {
            return this.defaultLoader;
          },
          set: function set(val) {
            this.defaultLoader = val;
          }
        }, {
          key: "SelectionChanged",
          value: function SelectionChanged() {
            this.SelectionChangedEvent.emit(this.Items);
          }
        }, {
          key: "Items",
          get: function get() {
            return this._items;
          },
          set: function set(value) {
            this._items = value;
            this.Args.Source = this._items;
          }
        }, {
          key: "Args",
          get: function get() {
            if (!this.Source.TagArguments) {
              this.Source.TagArguments = {
                Data: this.Source.List,
                Source: this.Items,
                Comparer: function Comparer(d, s) {
                  return s.id == d.id;
                },
                CreateNew: function CreateNew(d) {
                  return {
                    id: d.id
                  };
                }
              };
            }

            return this.Source.TagArguments;
          }
        }, {
          key: "Select",
          value: function Select(model) {
            model.Tag.SelectOnly(this.Items);
            this.Ok();
          }
        }, {
          key: "ClearSelection",
          value: function ClearSelection() {
            this.Items.length = 0;
            this.Ok();
          }
        }, {
          key: "LoadData",
          value: function LoadData() {
            this.Source.LoadData();
          }
        }, {
          key: "StartAsync",
          value: function StartAsync() {
            return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, /*#__PURE__*/regeneratorRuntime.mark(function _callee() {
              return regeneratorRuntime.wrap(function _callee$(_context) {
                while (1) {
                  switch (_context.prev = _context.next) {
                    case 0:
                      if (this.Source.LoadedOnce) {
                        _context.next = 5;
                        break;
                      }

                      _context.next = 3;
                      return this.Source.LoadDataAsync();

                    case 3:
                      _context.next = 6;
                      break;

                    case 5:
                      this.Source.Retag();

                    case 6:
                      return _context.abrupt("return", this);

                    case 7:
                    case "end":
                      return _context.stop();
                  }
                }
              }, _callee, this);
            }));
          }
        }]);

        return SelectComponentBase;
      }(_baseComponent__WEBPACK_IMPORTED_MODULE_1__["BaseComponent"]);

      SelectComponentBase.ɵfac = function SelectComponentBase_Factory(t) {
        return ɵSelectComponentBase_BaseFactory(t || SelectComponentBase);
      };

      SelectComponentBase.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵdefineComponent"]({
        type: SelectComponentBase,
        selectors: [["ng-component"]],
        features: [_angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵInheritDefinitionFeature"]],
        decls: 0,
        vars: 0,
        template: function SelectComponentBase_Template(rf, ctx) {},
        encapsulation: 2
      });

      var ɵSelectComponentBase_BaseFactory = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵgetInheritedFactory"](SelectComponentBase);
      /*@__PURE__*/


      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵsetClassMetadata"](SelectComponentBase, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_4__["Component"],
          args: [{
            template: ''
          }]
        }], null, null);
      })();
      /***/

    },

    /***/
    "6qUw":
    /*!*********************************************************!*\
      !*** ./src/core/codeshell/localization/localeLoader.ts ***!
      \*********************************************************/

    /*! exports provided: LocaleLoader */

    /***/
    function qUw(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "LocaleLoader", function () {
        return LocaleLoader;
      });

      var LocaleLoader = function LocaleLoader() {
        _classCallCheck(this, LocaleLoader);
      };
      /***/

    },

    /***/
    "7OZ3":
    /*!************************************!*\
      !*** ./src/core/codeshell/main.ts ***!
      \************************************/

    /*! exports provided: ServerConfigBase, Registry, absUrl, List_RemoveItem, List_RunRecursively_Nodes, List_OrderBy, List_OrderByDesc, List_FindIdRecusive, List_RunRecursively, List_RunRecursively_GEN, List_RunRecursivelyUp_GEN, List_ToggleItem, String_GetBeforeLast, String_GetAfterLast, Date_Get, Date_Elapsed, KeyValuePair, Utils, Shell, CodeShellModule */

    /***/
    function OZ3(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony import */


      var _serverConfigBase__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! ./serverConfigBase */
      "6m0k");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "ServerConfigBase", function () {
        return _serverConfigBase__WEBPACK_IMPORTED_MODULE_0__["ServerConfigBase"];
      });
      /* harmony import */


      var _utilities_registry__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ./utilities/registry */
      "ppms");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "Registry", function () {
        return _utilities_registry__WEBPACK_IMPORTED_MODULE_1__["Registry"];
      });
      /* harmony import */


      var _utilities_utils__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! ./utilities/utils */
      "VXkE");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "absUrl", function () {
        return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["absUrl"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "List_RemoveItem", function () {
        return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["List_RemoveItem"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "List_RunRecursively_Nodes", function () {
        return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["List_RunRecursively_Nodes"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "List_OrderBy", function () {
        return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["List_OrderBy"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "List_OrderByDesc", function () {
        return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["List_OrderByDesc"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "List_FindIdRecusive", function () {
        return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["List_FindIdRecusive"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "List_RunRecursively", function () {
        return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["List_RunRecursively"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "List_RunRecursively_GEN", function () {
        return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["List_RunRecursively_GEN"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "List_RunRecursivelyUp_GEN", function () {
        return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["List_RunRecursivelyUp_GEN"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "List_ToggleItem", function () {
        return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["List_ToggleItem"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "String_GetBeforeLast", function () {
        return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["String_GetBeforeLast"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "String_GetAfterLast", function () {
        return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["String_GetAfterLast"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "Date_Get", function () {
        return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["Date_Get"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "Date_Elapsed", function () {
        return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["Date_Elapsed"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "KeyValuePair", function () {
        return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["KeyValuePair"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "Utils", function () {
        return _utilities_utils__WEBPACK_IMPORTED_MODULE_2__["Utils"];
      });
      /* harmony import */


      var _shell__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! ./shell */
      "UoIT");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "Shell", function () {
        return _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"];
      });
      /* empty/unused harmony star reexport */

      /* harmony import */


      var _codeshell_module__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
      /*! ./codeshell.module */
      "bT1W");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "CodeShellModule", function () {
        return _codeshell_module__WEBPACK_IMPORTED_MODULE_4__["CodeShellModule"];
      });
      /***/

    },

    /***/
    "8sze":
    /*!***********************************************!*\
      !*** ./src/core/base/main/login.component.ts ***!
      \***********************************************/

    /*! exports provided: Login */

    /***/
    function sze(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "Login", function () {
        return Login;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var codeshell_base_components__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! codeshell/base-components */
      "gyvI");
      /* harmony import */


      var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! @angular/forms */
      "3Pt+");
      /* harmony import */


      var _angular_common__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! @angular/common */
      "ofXK");
      /* harmony import */


      var _angular_router__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
      /*! @angular/router */
      "tyNb");
      /* harmony import */


      var _ngx_translate_core__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(
      /*! @ngx-translate/core */
      "sYmb");
      /* harmony import */


      var _codeshell_pipes_absoluteUrl__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(
      /*! ../../codeshell/pipes/absoluteUrl */
      "WyvS");

      var _c0 = function _c0(a0) {
        return {
          p0: a0
        };
      };

      function Login_span_21_small_1_Template(rf, ctx) {
        if (rf & 1) {
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "small", 21);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](1);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](2, "translate");

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](3, "translate");

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        }

        if (rf & 2) {
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind2"](2, 1, "Messages.field_required", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpureFunction1"](6, _c0, _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind1"](3, 4, "Words.UserName"))));
        }
      }

      function Login_span_21_Template(rf, ctx) {
        if (rf & 1) {
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "span");

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](1, Login_span_21_small_1_Template, 4, 8, "small", 20);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        }

        if (rf & 2) {
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();

          var _r1 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵreference"](19);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", _r1.errors.required);
        }
      }

      function Login_div_35_Template(rf, ctx) {
        if (rf & 1) {
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "div", 22);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](1, "a", 23);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](2, "absUrl");

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](3);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](4, "translate");

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        }

        if (rf & 2) {
          var ctx_r4 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("routerLink", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind1"](2, 2, ctx_r4.ForgotPasswordUrl));

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind1"](4, 4, "Words.ForgotPassword"));
        }
      }

      var Login = /*#__PURE__*/function (_codeshell_base_compo) {
        _inherits(Login, _codeshell_base_compo);

        var _super4 = _createSuper(Login);

        function Login() {
          var _this6;

          _classCallCheck(this, Login);

          _this6 = _super4.apply(this, arguments);
          _this6.ForgotPasswordUrl = "/Auth/ForgotPassword";
          return _this6;
        }

        return Login;
      }(codeshell_base_components__WEBPACK_IMPORTED_MODULE_1__["LoginBase"]);

      Login.ɵfac = function Login_Factory(t) {
        return ɵLogin_BaseFactory(t || Login);
      };

      Login.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineComponent"]({
        type: Login,
        selectors: [["ng-component"]],
        features: [_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵInheritDefinitionFeature"]],
        decls: 41,
        vars: 24,
        consts: [[1, "container"], [1, "row"], [1, "col-md-4", "col-md-offset-4", "col-sm-12", "col-sm-offset-0", "content-block", "animated", "fadeInDownBig"], [1, "not-mob"], [1, "title-header", "text-center", "page-header"], [1, "order-price"], ["Form", "ngForm"], [1, "col-md-12"], [1, "form-group"], ["type", "text", "name", "UserName", "required", "", 1, "form-control", 2, "direction", "ltr", 3, "ngModel", "placeholder", "ngModelChange"], ["UserName", "ngModel"], [4, "ngIf"], ["name", "Password", "required", "", "type", "password", 1, "form-control", 2, "direction", "ltr", 3, "ngModel", "placeholder", "ngModelChange"], ["Password", "ngModel"], [1, "col-md-7"], [1, "chiller_cb"], ["type", "checkbox", "name", "Remember", 3, "ngModel", "ngModelChange"], ["class", "col-md-5", 4, "ngIf"], ["type", "submit", 1, "btn", "btn-primary", "btn-block", "btn-lg", 3, "disabled", "click"], ["aria-hidden", "true", 1, "fa", "fa-sign-in-alt"], ["class", "form-text text-danger", 4, "ngIf"], [1, "form-text", "text-danger"], [1, "col-md-5"], [3, "routerLink"]],
        template: function Login_Template(rf, ctx) {
          if (rf & 1) {
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

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("ngModelChange", function Login_Template_input_ngModelChange_18_listener($event) {
              return ctx.model.UserName = $event;
            });

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](20, "translate");

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](21, Login_span_21_Template, 2, 1, "span", 11);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](22, "div", 8);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](23, "input", 12, 13);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("ngModelChange", function Login_Template_input_ngModelChange_23_listener($event) {
              return ctx.model.Password = $event;
            });

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](25, "translate");

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](26, "div", 8);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](27, "div", 1);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](28, "div", 14);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](29, "label", 15);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](30, "input", 16);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("ngModelChange", function Login_Template_input_ngModelChange_30_listener($event) {
              return ctx.model.RememberMe = $event;
            });

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

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function Login_Template_button_click_37_listener() {
              return ctx.Login();
            });

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
          }

          if (rf & 2) {
            var _r0 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵreference"](14);

            var _r1 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵreference"](19);

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
          }
        },
        directives: [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["ɵangular_packages_forms_forms_y"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["NgControlStatusGroup"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["NgForm"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["DefaultValueAccessor"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["RequiredValidator"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["NgControlStatus"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["NgModel"], _angular_common__WEBPACK_IMPORTED_MODULE_3__["NgIf"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["CheckboxControlValueAccessor"], _angular_router__WEBPACK_IMPORTED_MODULE_4__["RouterLinkWithHref"]],
        pipes: [_ngx_translate_core__WEBPACK_IMPORTED_MODULE_5__["TranslatePipe"], _codeshell_pipes_absoluteUrl__WEBPACK_IMPORTED_MODULE_6__["AbsoluteUrl"]],
        encapsulation: 2
      });

      var ɵLogin_BaseFactory = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵgetInheritedFactory"](Login);
      /*@__PURE__*/


      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](Login, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"],
          args: [{
            templateUrl: "./login.component.html"
          }]
        }], null, null);
      })();
      /***/

    },

    /***/
    "8v3h":
    /*!*******************************************************!*\
      !*** ./src/core/codeshell/localization/translator.ts ***!
      \*******************************************************/

    /*! exports provided: Translator */

    /***/
    function v3h(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "Translator", function () {
        return Translator;
      });
      /* harmony import */


      var rxjs__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! rxjs */
      "qCKp");

      var Translator
      /* extends TranslateLoader*/
      = /*#__PURE__*/function () {
        function Translator() {
          _classCallCheck(this, Translator);
        }

        _createClass(Translator, [{
          key: "getTranslation",
          value: function getTranslation(lang) {
            var res = {};

            if (Translator.Loaders[lang] != undefined) {
              res = Translator.Loaders[lang].Load();
            } else {
              res = {
                Columns: {},
                Words: {},
                Messages: {},
                Pages: {}
              };
            }

            return Object(rxjs__WEBPACK_IMPORTED_MODULE_0__["of"])(res);
          }
        }], [{
          key: "SetLoaders",
          value: function SetLoaders(loaders) {
            Translator.Loaders = loaders;
          }
        }]);

        return Translator;
      }();

      Translator.Loaders = {};
      /***/
    },

    /***/
    "9loO":
    /*!*********************************************************!*\
      !*** ./src/core/codeshell/validators/rangeValidator.ts ***!
      \*********************************************************/

    /*! exports provided: NumberRangeValidator */

    /***/
    function loO(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "NumberRangeValidator", function () {
        return NumberRangeValidator;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var _validatorBase__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ./validatorBase */
      "cPZL");
      /* harmony import */


      var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! @angular/forms */
      "3Pt+");

      var NumberRangeValidator = /*#__PURE__*/function (_validatorBase__WEBPA) {
        _inherits(NumberRangeValidator, _validatorBase__WEBPA);

        var _super5 = _createSuper(NumberRangeValidator);

        function NumberRangeValidator(el, mod) {
          var _this7;

          _classCallCheck(this, NumberRangeValidator);

          _this7 = _super5.call(this, el, mod);
          _this7.Identifier = "number_range";
          return _this7;
        }

        _createClass(NumberRangeValidator, [{
          key: "Max",
          get: function get() {
            return this._max;
          },
          set: function set(val) {
            this._max = val;
          }
        }, {
          key: "ngOnInit",
          value: function ngOnInit() {
            if (this.Max != undefined) this.Element.max = this.Max.toString();
          }
        }, {
          key: "RunIf",
          value: function RunIf(ch) {
            return ch.Min != undefined || ch.Max != undefined;
          }
        }, {
          key: "IsValid",
          value: function IsValid() {
            var v = true;

            if (this.Max) {
              v = v && this.Model.value < this.Max;
            }

            if (this.Min) {
              v = v && this.Model.value > this.Min;
            }

            return v;
          }
        }]);

        return NumberRangeValidator;
      }(_validatorBase__WEBPACK_IMPORTED_MODULE_1__["ValidatorBase"]);

      NumberRangeValidator.ɵfac = function NumberRangeValidator_Factory(t) {
        return new (t || NumberRangeValidator)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]), _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_forms__WEBPACK_IMPORTED_MODULE_2__["NgModel"]));
      };

      NumberRangeValidator.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({
        type: NumberRangeValidator,
        selectors: [["", "numberRange", "", "ngModel", ""]],
        inputs: {
          Min: ["min", "Min"],
          Max: ["max", "Max"]
        },
        exportAs: ["numberRange"],
        features: [_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵInheritDefinitionFeature"]]
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](NumberRangeValidator, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
          args: [{
            selector: '[numberRange][ngModel]',
            exportAs: 'numberRange'
          }]
        }], function () {
          return [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]
          }, {
            type: _angular_forms__WEBPACK_IMPORTED_MODULE_2__["NgModel"]
          }];
        }, {
          Min: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["min"]
          }],
          Max: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["max"]
          }]
        });
      })();
      /***/

    },

    /***/
    "9uFN":
    /*!*********************************************************!*\
      !*** ./src/core/codeshell/security/sessionTokenData.ts ***!
      \*********************************************************/

    /*! exports provided: SessionTokenData */

    /***/
    function uFN(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "SessionTokenData", function () {
        return SessionTokenData;
      });
      /* harmony import */


      var _tokenStorage__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! ./tokenStorage */
      "yvMo");
      /* harmony import */


      var _models__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ./models */
      "gFuU");
      /* harmony import */


      var codeshell_services_stored__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! codeshell/services/stored */
      "mHXC");

      var SessionTokenData = /*#__PURE__*/function (_tokenStorage__WEBPAC) {
        _inherits(SessionTokenData, _tokenStorage__WEBPAC);

        var _super6 = _createSuper(SessionTokenData);

        function SessionTokenData() {
          _classCallCheck(this, SessionTokenData);

          return _super6.apply(this, arguments);
        }

        _createClass(SessionTokenData, [{
          key: "LoadToken",
          value: function LoadToken() {
            return codeshell_services_stored__WEBPACK_IMPORTED_MODULE_2__["Stored"].Get_SS("TokenData", _models__WEBPACK_IMPORTED_MODULE_1__["TokenData"]);
          }
        }, {
          key: "SaveToken",
          value: function SaveToken(data) {
            codeshell_services_stored__WEBPACK_IMPORTED_MODULE_2__["Stored"].Set_SS("TokenData", data);
          }
        }, {
          key: "Clear",
          value: function Clear() {
            codeshell_services_stored__WEBPACK_IMPORTED_MODULE_2__["Stored"].Clear_SS("TokenData");
            localStorage.removeItem("refresh");
          }
        }]);

        return SessionTokenData;
      }(_tokenStorage__WEBPACK_IMPORTED_MODULE_0__["TokenStorage"]);
      /***/

    },

    /***/
    "AytR":
    /*!*****************************************!*\
      !*** ./src/environments/environment.ts ***!
      \*****************************************/

    /*! exports provided: environment */

    /***/
    function AytR(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "environment", function () {
        return environment;
      });

      var environment = {
        production: false
      };
      /***/
    },

    /***/
    "CJgx":
    /*!***************************************************************!*\
      !*** ./src/core/codeshell/directives/selectable.directive.ts ***!
      \***************************************************************/

    /*! exports provided: Selectable */

    /***/
    function CJgx(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "Selectable", function () {
        return Selectable;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");

      var Selectable = /*#__PURE__*/function () {
        function Selectable(ref) {
          _classCallCheck(this, Selectable);

          this.model = {};
          this.Element = ref.nativeElement;
        }

        _createClass(Selectable, [{
          key: "ngClass",
          get: function get() {
            return this.model.rowSelected ? 'list-item selected' : 'list-item';
          }
        }, {
          key: "OnClick",
          value: function OnClick(event) {
            if (this.Service) this.Service.ItemClicked(this.model, event);
          }
        }]);

        return Selectable;
      }();

      Selectable.ɵfac = function Selectable_Factory(t) {
        return new (t || Selectable)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]));
      };

      Selectable.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({
        type: Selectable,
        selectors: [["", "selectable", ""]],
        hostVars: 2,
        hostBindings: function Selectable_HostBindings(rf, ctx) {
          if (rf & 1) {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function Selectable_click_HostBindingHandler($event) {
              return ctx.OnClick($event);
            });
          }

          if (rf & 2) {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵclassMap"](ctx.ngClass);
          }
        },
        inputs: {
          model: ["selectable", "model"],
          Service: ["select-service", "Service"]
        },
        exportAs: ["selectable"]
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](Selectable, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
          args: [{
            selector: "[selectable]",
            exportAs: "selectable"
          }]
        }], function () {
          return [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]
          }];
        }, {
          model: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["selectable"]
          }],
          Service: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["select-service"]
          }],
          ngClass: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["HostBinding"],
            args: ["class"]
          }],
          OnClick: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"],
            args: ["click", ["$event"]]
          }]
        });
      })();
      /***/

    },

    /***/
    "DTUp":
    /*!*******************************************************!*\
      !*** ./src/core/base/app-component-base.component.ts ***!
      \*******************************************************/

    /*! exports provided: AppComponentBase */

    /***/
    function DTUp(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "AppComponentBase", function () {
        return AppComponentBase;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var codeshell_base_components__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! codeshell/base-components */
      "gyvI");
      /* harmony import */


      var codeshell_directives__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! codeshell/directives */
      "XgJV");

      var AppComponentBase = /*#__PURE__*/function (_codeshell_base_compo2) {
        _inherits(AppComponentBase, _codeshell_base_compo2);

        var _super7 = _createSuper(AppComponentBase);

        function AppComponentBase() {
          _classCallCheck(this, AppComponentBase);

          return _super7.apply(this, arguments);
        }

        return AppComponentBase;
      }(codeshell_base_components__WEBPACK_IMPORTED_MODULE_1__["IAppComponent"]);

      AppComponentBase.ɵfac = function AppComponentBase_Factory(t) {
        return ɵAppComponentBase_BaseFactory(t || AppComponentBase);
      };

      AppComponentBase.ɵprov = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineInjectable"]({
        token: AppComponentBase,
        factory: AppComponentBase.ɵfac
      });

      var ɵAppComponentBase_BaseFactory = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵgetInheritedFactory"](AppComponentBase);
      /*@__PURE__*/


      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](AppComponentBase, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"]
        }], null, {
          ModalLoader: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ViewChild"],
            args: [codeshell_directives__WEBPACK_IMPORTED_MODULE_2__["ComponentLoader"]]
          }]
        });
      })();
      /***/

    },

    /***/
    "EuMQ":
    /*!**********************************************************************!*\
      !*** ./src/core/codeshell/directives/list-item-watcher.directive.ts ***!
      \**********************************************************************/

    /*! exports provided: ListItemWatcher */

    /***/
    function EuMQ(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "ListItemWatcher", function () {
        return ListItemWatcher;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var _helpers__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ../helpers */
      "GYDu");

      var ListItemWatcher = /*#__PURE__*/function () {
        function ListItemWatcher(cont) {
          _classCallCheck(this, ListItemWatcher);

          this.item = null;
        }

        _createClass(ListItemWatcher, [{
          key: "Change",
          value: function Change(ev) {
            if (this.item) {
              this.item.SetModified();
            }
          }
        }, {
          key: "ngOnInit",
          value: function ngOnInit() {
            if (this.mod instanceof _helpers__WEBPACK_IMPORTED_MODULE_1__["ListItem"]) {
              this.item = this.mod;
            }
          }
        }]);

        return ListItemWatcher;
      }();

      ListItemWatcher.ɵfac = function ListItemWatcher_Factory(t) {
        return new (t || ListItemWatcher)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]));
      };

      ListItemWatcher.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({
        type: ListItemWatcher,
        selectors: [["", "acs-li-watch", ""]],
        hostBindings: function ListItemWatcher_HostBindings(rf, ctx) {
          if (rf & 1) {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("change", function ListItemWatcher_change_HostBindingHandler($event) {
              return ctx.Change($event);
            });
          }
        },
        inputs: {
          mod: ["acs-li-watch", "mod"]
        },
        exportAs: ["liWatch"]
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](ListItemWatcher, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
          args: [{
            selector: "[acs-li-watch]",
            exportAs: "liWatch"
          }]
        }], function () {
          return [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]
          }];
        }, {
          mod: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["acs-li-watch"]
          }],
          Change: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"],
            args: ["change", ['$event']]
          }]
        });
      })();
      /***/

    },

    /***/
    "F2JI":
    /*!*************************************************************!*\
      !*** ./src/core/codeshell/directives/on-enter.directive.ts ***!
      \*************************************************************/

    /*! exports provided: OnEnter */

    /***/
    function F2JI(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "OnEnter", function () {
        return OnEnter;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");

      var OnEnter = /*#__PURE__*/function () {
        function OnEnter() {
          _classCallCheck(this, OnEnter);

          this.out = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        }

        _createClass(OnEnter, [{
          key: "OnKeyPress",
          value: function OnKeyPress(e) {
            if (e.key == "Enter") this.out.emit();
          }
        }]);

        return OnEnter;
      }();

      OnEnter.ɵfac = function OnEnter_Factory(t) {
        return new (t || OnEnter)();
      };

      OnEnter.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({
        type: OnEnter,
        selectors: [["input", "onEnter", ""]],
        hostBindings: function OnEnter_HostBindings(rf, ctx) {
          if (rf & 1) {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("keypress", function OnEnter_keypress_HostBindingHandler($event) {
              return ctx.OnKeyPress($event);
            });
          }
        },
        outputs: {
          out: "onEnter"
        }
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](OnEnter, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
          args: [{
            selector: "input[onEnter]"
          }]
        }], null, {
          out: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"],
            args: ["onEnter"]
          }],
          OnKeyPress: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"],
            args: ["keypress", ['$event']]
          }]
        });
      })();
      /***/

    },

    /***/
    "G778":
    /*!*****************************************************************!*\
      !*** ./src/core/codeshell/base-components/editComponentBase.ts ***!
      \*****************************************************************/

    /*! exports provided: EditComponentBase */

    /***/
    function G778(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "EditComponentBase", function () {
        return EditComponentBase;
      });
      /* harmony import */


      var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! tslib */
      "mrSG");
      /* harmony import */


      var _baseComponent__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ./baseComponent */
      "I5Ck");
      /* harmony import */


      var _helpers__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! ../helpers */
      "GYDu");
      /* harmony import */


      var _shell__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! ../shell */
      "UoIT");
      /* harmony import */


      var _angular_router__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
      /*! @angular/router */
      "tyNb");
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var _helpers_localizablesDTO__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(
      /*! ../helpers/localizablesDTO */
      "5hkl");
      /* harmony import */


      var codeshell_utilities_utils__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(
      /*! codeshell/utilities/utils */
      "VXkE");

      var EditComponentBase = /*#__PURE__*/function (_baseComponent__WEBPA2) {
        _inherits(EditComponentBase, _baseComponent__WEBPA2);

        var _super8 = _createSuper(EditComponentBase);

        function EditComponentBase() {
          var _this8;

          _classCallCheck(this, EditComponentBase);

          _this8 = _super8.apply(this, arguments);
          _this8.model = {};
          _this8.CurrentLang = "ar";
          _this8.UI_Lang = "ar";
          _this8._formState = "VALID";
          _this8._formValidity = new _angular_core__WEBPACK_IMPORTED_MODULE_5__["EventEmitter"]();
          _this8.UseLocalization = false;
          _this8.Localizables = {};
          _this8.HideButtons = false;
          _this8._lookupsLoaded = false;
          _this8.isBound = false;
          _this8.LoadLookups = false;
          _this8.IsNew = true;
          _this8.FormIsValid = true;
          _this8.SubmitClicked = false;
          _this8.DataSubmittedEvent = new _angular_core__WEBPACK_IMPORTED_MODULE_5__["EventEmitter"]();
          _this8.RouteParams = {};
          return _this8;
        }

        _createClass(EditComponentBase, [{
          key: "ValidState",
          get: function get() {
            return this._formValidity;
          }
        }, {
          key: "CanSubmit",
          get: function get() {
            return this.FormIsValid;
          },
          set: function set(st) {}
        }, {
          key: "Loc",
          get: function get() {
            if (!this.Localizables[this.CurrentLang]) this.Localizables[this.CurrentLang] = new _helpers_localizablesDTO__WEBPACK_IMPORTED_MODULE_6__["LocalizablesDTO"]();
            return this.Localizables[this.CurrentLang];
          }
        }, {
          key: "ngOnInit",
          value: function ngOnInit() {
            var _this9 = this;

            _get(_getPrototypeOf(EditComponentBase.prototype), "ngOnInit", this).call(this);

            this.model = this._bound ? this._bound : this.DefaultModel();
            var lookupOpts = this.GetLookupOptions();

            if (!this.IsEmbedded) {
              if (lookupOpts == null && !this.LoadLookups) {
                this.StartComponent();
              } else {
                this.LoadLookupsAsync(lookupOpts).then(function (lookups) {
                  _this9.Lookups = lookups;

                  _this9.StartComponent();
                });
              }
            }

            this.UI_Lang = "en";
            this.CurrentLang = _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Main.Config.DefaultLocale == "ar" ? "en" : "ar";

            if (this.Form && this.Form.statusChanges) {
              this.Form.statusChanges.subscribe(function (d) {
                if (d != _this9._formState) {
                  _this9.FormIsValid = d != "INVALID";

                  _this9.OnFormValidityChange(_this9.FormIsValid);

                  _this9.EmitValidity();
                }

                _this9._formState = d;
              });
              setTimeout(function () {
                return _this9.EmitValidity();
              }, 200);
            }
          }
        }, {
          key: "IsValid",
          value: function IsValid() {
            var valid = true;
            debugger;

            if (this.Form) {
              valid = valid && !this.Form.invalid;
            }

            if (this.FormGroup) {
              valid = valid && this.FormGroup.invalid;
            }

            return valid;
          }
        }, {
          key: "ModelId",
          get: function get() {
            return this.model.id;
          },
          set: function set(val) {
            this.model.id = val;
          }
        }, {
          key: "DefaultModel",
          value: function DefaultModel() {
            return {};
          }
        }, {
          key: "OnFormValidityChange",
          value: function OnFormValidityChange(state) {}
        }, {
          key: "EmitValidity",
          value: function EmitValidity() {
            this._formValidity.emit(this.CanSubmit);
          }
        }, {
          key: "LoadLookupsAsync",
          value: function LoadLookupsAsync(opts) {
            return this.Service.GetEditLookups(opts);
          }
        }, {
          key: "StartEditOrCreate",
          value: function StartEditOrCreate() {
            if (!this.IsNew) {
              this.modelId = Number.parseInt(this.RouteParams['id']);
              this.Fill(this.modelId);
            } else {
              this.StartNew();
            }
          }
        }, {
          key: "StartComponent",
          value: function StartComponent() {
            var _this10 = this;

            if (!this.IsEmbedded) {
              this.Route.params.subscribe(function (params) {
                _this10.RouteParams = params;
                _this10.IsNew = _this10.RouteParams['id'] == undefined;

                _this10.StartEditOrCreate();
              });
            } else {
              this.IsInitialized = true;
              this.OnReady();
            }
          }
        }, {
          key: "_loadLookupsOnce",
          value: function _loadLookupsOnce() {
            return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, /*#__PURE__*/regeneratorRuntime.mark(function _callee2() {
              var lookupOpts;
              return regeneratorRuntime.wrap(function _callee2$(_context2) {
                while (1) {
                  switch (_context2.prev = _context2.next) {
                    case 0:
                      if (!this._lookupsLoaded) {
                        _context2.next = 2;
                        break;
                      }

                      return _context2.abrupt("return");

                    case 2:
                      lookupOpts = this.GetLookupOptions();

                      if (!(lookupOpts != null || this.LoadLookups)) {
                        _context2.next = 7;
                        break;
                      }

                      _context2.next = 6;
                      return this.LoadLookupsAsync(lookupOpts);

                    case 6:
                      this.Lookups = _context2.sent;

                    case 7:
                      this._lookupsLoaded = true;

                    case 8:
                    case "end":
                      return _context2.stop();
                  }
                }
              }, _callee2, this);
            }));
          }
        }, {
          key: "_componentStarted",
          value: function _componentStarted() {
            var _this11 = this;

            this.IsInitialized = true;
            setTimeout(function () {
              return _this11.SetAccessibility();
            }, 700);

            if (this.UseLocalization && this.model && this.ModelId != 0) {
              this.Service.GetLocalizationData(this.ModelId).then(function (s) {
                _this11.Localizables = s;
              });
            }

            this.OnReady();
          }
        }, {
          key: "FillAsync",
          value: function FillAsync(id) {
            return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, /*#__PURE__*/regeneratorRuntime.mark(function _callee3() {
              return regeneratorRuntime.wrap(function _callee3$(_context3) {
                while (1) {
                  switch (_context3.prev = _context3.next) {
                    case 0:
                      _context3.next = 2;
                      return this._loadLookupsOnce();

                    case 2:
                      _context3.next = 4;
                      return this.GetModelFromServerAsync(id);

                    case 4:
                      this.model = _context3.sent;
                      this.IsNew = false;

                      this._componentStarted();

                    case 7:
                    case "end":
                      return _context3.stop();
                  }
                }
              }, _callee3, this);
            }));
          }
        }, {
          key: "Clear",
          value: function Clear() {
            this.model = this.DefaultModel();
            this.Localizables = {};
            this.IsNew = true;
          }
        }, {
          key: "BindAsync",
          value: function BindAsync(mod) {
            return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, /*#__PURE__*/regeneratorRuntime.mark(function _callee4() {
              return regeneratorRuntime.wrap(function _callee4$(_context4) {
                while (1) {
                  switch (_context4.prev = _context4.next) {
                    case 0:
                      _context4.next = 2;
                      return this._loadLookupsOnce();

                    case 2:
                      this._bound = mod;
                      this.model = mod;

                      this._componentStarted();

                    case 5:
                    case "end":
                      return _context4.stop();
                  }
                }
              }, _callee4, this);
            }));
          }
        }, {
          key: "Fill",
          value: function Fill(id) {
            var _this12 = this;

            this.GetModelFromServerAsync(id).then(function (v) {
              _this12.model = v;
              _this12.IsNew = false;

              _this12._componentStarted();
            });
          }
        }, {
          key: "Bind",
          value: function Bind(item) {
            var _this13 = this;

            this._bound = item;

            if (this.IsInitialized) {
              this.model = item;
              setTimeout(function () {
                return _this13.SetAccessibility();
              }, 500);
              this.OnReady();
            }

            this.Show = true;

            if (this.UseLocalization && this._bound && this._bound.id != 0) {
              this.Service.GetLocalizationData(this._bound.id).then(function (s) {
                _this13.Localizables = s;
              });
            }
          }
        }, {
          key: "StartNew",
          value: function StartNew() {
            var _this14 = this;

            this.InitializeNewModelAsync().then(function () {
              _this14.IsInitialized = true;
              setTimeout(function () {
                return _this14.SetAccessibility();
              }, 500);

              _this14.OnReady();
            });
          }
        }, {
          key: "SetAccessibility",
          value: function SetAccessibility() {}
          /**
           * Is called before submit to check if the form is valid
           * */

        }, {
          key: "Validate",
          value: function Validate() {
            if (this.Form && this.Form.invalid) {
              return {
                success: false,
                message: "invalid_form",
                type: _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"].Error
              };
            }

            return {
              success: true,
              type: _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"].Success
            };
          }
          /**
           * Can be overridden to enable initializing the model on the server side
           * */

        }, {
          key: "InitializeNewModelAsync",
          value: function InitializeNewModelAsync() {
            return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, /*#__PURE__*/regeneratorRuntime.mark(function _callee5() {
              return regeneratorRuntime.wrap(function _callee5$(_context5) {
                while (1) {
                  switch (_context5.prev = _context5.next) {
                    case 0:
                    case "end":
                      return _context5.stop();
                  }
                }
              }, _callee5);
            }));
          }
          /**
           * When an id is found in the route params this method is called to obtain model from server using that id
           * Can be overridden
           * @param id as obtaind from url
           */

        }, {
          key: "GetModelFromServerAsync",
          value: function GetModelFromServerAsync(id) {
            return this.Service.GetSingle(id);
          }
          /**
           * Called after lookups and model is both loaded
           * */

        }, {
          key: "OnReady",
          value: function OnReady() {}
        }, {
          key: "SubmitLocalizablesAsync",
          value: function SubmitLocalizablesAsync() {
            return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, /*#__PURE__*/regeneratorRuntime.mark(function _callee6() {
              var s, submit, i, e, res;
              return regeneratorRuntime.wrap(function _callee6$(_context6) {
                while (1) {
                  switch (_context6.prev = _context6.next) {
                    case 0:
                      s = {};
                      submit = false;

                      for (i in this.Localizables) {
                        if (this.Localizables[i].state == "Modified" || this.Localizables[i].state == "Added") {
                          s[i] = this.Localizables[i];
                          submit = true;
                        }
                      }

                      if (!submit) {
                        _context6.next = 8;
                        break;
                      }

                      e = this.model;
                      _context6.next = 7;
                      return this.Service.SetLocalizationData(this.ModelId, s);

                    case 7:
                      res = _context6.sent;

                    case 8:
                    case "end":
                      return _context6.stop();
                  }
                }
              }, _callee6, this);
            }));
          }
        }, {
          key: "SubmitNewAsync",
          value: function SubmitNewAsync() {
            return this.Service.Post("Post", this.model);
          }
        }, {
          key: "SubmitUpdateAsync",
          value: function SubmitUpdateAsync() {
            return this.Service.Put("Put", this.model);
          }
        }, {
          key: "SubmitAsync",
          value: function SubmitAsync() {
            return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, /*#__PURE__*/regeneratorRuntime.mark(function _callee7() {
              var prom;
              return regeneratorRuntime.wrap(function _callee7$(_context7) {
                while (1) {
                  switch (_context7.prev = _context7.next) {
                    case 0:
                      if (!this.IsNew) {
                        _context7.next = 10;
                        break;
                      }

                      _context7.next = 3;
                      return this.SubmitNewAsync();

                    case 3:
                      prom = _context7.sent;

                      if (!prom.data.Id) {
                        _context7.next = 8;
                        break;
                      }

                      this.ModelId = prom.data.Id;
                      _context7.next = 8;
                      return this.SubmitLocalizablesAsync();

                    case 8:
                      _context7.next = 15;
                      break;

                    case 10:
                      _context7.next = 12;
                      return this.SubmitUpdateAsync();

                    case 12:
                      prom = _context7.sent;
                      _context7.next = 15;
                      return this.SubmitLocalizablesAsync();

                    case 15:
                      return _context7.abrupt("return", prom);

                    case 16:
                    case "end":
                      return _context7.stop();
                  }
                }
              }, _callee7, this);
            }));
          }
        }, {
          key: "Submit",
          value: function Submit() {
            var _this15 = this;

            if (!this.IsValid()) {
              this.SubmitClicked = true;
              return;
            }

            this.SubmitAsync().then(function (e) {
              _this15.OnSubmitSuccess(e);

              if (_this15.DataSubmitted) _this15.DataSubmitted(_this15.model, e);

              _this15.DataSubmittedEvent.emit(e);
            })["catch"](function (x) {
              _this15.OnSubmitFailed(x);
            });
          }
        }, {
          key: "OnSubmitSuccess",
          value: function OnSubmitSuccess(res) {
            this.NotifyTranslate(res.message, _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"].Success);

            if (!this.IsEmbedded) {
              if (this.ViewParams.ListUrl) {
                var s = _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Injector.get(_angular_router__WEBPACK_IMPORTED_MODULE_4__["Router"]);

                s.navigateByUrl(this.ViewParams.ListUrl);
              } else {
                this.Navigation.back();
              }
            } else if (this.model.id) {//this.StartEditOrCreate();
            }
          }
        }, {
          key: "OnSubmitFailed",
          value: function OnSubmitFailed(res) {
            codeshell_utilities_utils__WEBPACK_IMPORTED_MODULE_7__["Utils"].HandleError(res, true);
          }
        }, {
          key: "Delete",
          value: function Delete(id) {
            var _this16 = this;

            _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Main.ShowDeleteConfirm().then(function (e) {
              if (e) {
                _this16.Service.Delete("Delete", id).then(function (e) {
                  if (!_this16.IsEmbedded) {
                    _this16.NotifyTranslate("delete_success");

                    if (_this16.ViewParams.ListUrl) {
                      _this16.NavigateToComponent(_this16.ViewParams.ListUrl);
                    } else {
                      _this16.Navigation.back();
                    }
                  }
                })["catch"](function (e) {
                  codeshell_utilities_utils__WEBPACK_IMPORTED_MODULE_7__["Utils"].HandleError(e, true, true);
                });
              }
            });
          }
        }]);

        return EditComponentBase;
      }(_baseComponent__WEBPACK_IMPORTED_MODULE_1__["BaseComponent"]);

      EditComponentBase.ɵfac = function EditComponentBase_Factory(t) {
        return ɵEditComponentBase_BaseFactory(t || EditComponentBase);
      };

      EditComponentBase.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵdefineComponent"]({
        type: EditComponentBase,
        selectors: [["ng-component"]],
        outputs: {
          ValidState: "is-valid"
        },
        features: [_angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵInheritDefinitionFeature"]],
        decls: 0,
        vars: 0,
        template: function EditComponentBase_Template(rf, ctx) {},
        encapsulation: 2
      });

      var ɵEditComponentBase_BaseFactory = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵgetInheritedFactory"](EditComponentBase);
      /*@__PURE__*/


      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵsetClassMetadata"](EditComponentBase, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_5__["Component"],
          args: [{
            template: ''
          }]
        }], null, {
          ValidState: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_5__["Output"],
            args: ["is-valid"]
          }]
        });
      })();
      /***/

    },

    /***/
    "GYDu":
    /*!*********************************************!*\
      !*** ./src/core/codeshell/helpers/index.ts ***!
      \*********************************************/

    /*! exports provided: DTO, TmpFileData, Result, SubmitResult, DeleteResult, LoadResult, LoadResultGen, PropertyFilter, LoadOptions, NoteType, ListItem, BoundListItem, ViewParams, TaggedArgs, Tagged, Stored, RecursionModel, ComponentRequest, LocalizablesDTO, TreeEventArgs, EditablePairsDTO */

    /***/
    function GYDu(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony import */


      var _interfaces__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! ./interfaces */
      "OI30");
      /* empty/unused harmony star reexport */

      /* harmony import */


      var _models__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ./models */
      "dpcW");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "DTO", function () {
        return _models__WEBPACK_IMPORTED_MODULE_1__["DTO"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "TmpFileData", function () {
        return _models__WEBPACK_IMPORTED_MODULE_1__["TmpFileData"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "Result", function () {
        return _models__WEBPACK_IMPORTED_MODULE_1__["Result"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "SubmitResult", function () {
        return _models__WEBPACK_IMPORTED_MODULE_1__["SubmitResult"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "DeleteResult", function () {
        return _models__WEBPACK_IMPORTED_MODULE_1__["DeleteResult"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "LoadResult", function () {
        return _models__WEBPACK_IMPORTED_MODULE_1__["LoadResult"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "LoadResultGen", function () {
        return _models__WEBPACK_IMPORTED_MODULE_1__["LoadResultGen"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "PropertyFilter", function () {
        return _models__WEBPACK_IMPORTED_MODULE_1__["PropertyFilter"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "LoadOptions", function () {
        return _models__WEBPACK_IMPORTED_MODULE_1__["LoadOptions"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "NoteType", function () {
        return _models__WEBPACK_IMPORTED_MODULE_1__["NoteType"];
      });
      /* harmony import */


      var _listItem__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! ./listItem */
      "JLZs");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "ListItem", function () {
        return _listItem__WEBPACK_IMPORTED_MODULE_2__["ListItem"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "BoundListItem", function () {
        return _listItem__WEBPACK_IMPORTED_MODULE_2__["BoundListItem"];
      });
      /* harmony import */


      var _viewParams__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! ./viewParams */
      "jBBj");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "ViewParams", function () {
        return _viewParams__WEBPACK_IMPORTED_MODULE_3__["ViewParams"];
      });
      /* harmony import */


      var _tagged__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
      /*! ./tagged */
      "wGhu");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "TaggedArgs", function () {
        return _tagged__WEBPACK_IMPORTED_MODULE_4__["TaggedArgs"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "Tagged", function () {
        return _tagged__WEBPACK_IMPORTED_MODULE_4__["Tagged"];
      });
      /* harmony import */


      var _services_stored__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(
      /*! ../services/stored */
      "mHXC");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "Stored", function () {
        return _services_stored__WEBPACK_IMPORTED_MODULE_5__["Stored"];
      });
      /* harmony import */


      var _recursionModel__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(
      /*! ./recursionModel */
      "fYjR");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "RecursionModel", function () {
        return _recursionModel__WEBPACK_IMPORTED_MODULE_6__["RecursionModel"];
      });
      /* harmony import */


      var _componentRequest__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(
      /*! ./componentRequest */
      "K+1B");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "ComponentRequest", function () {
        return _componentRequest__WEBPACK_IMPORTED_MODULE_7__["ComponentRequest"];
      });
      /* harmony import */


      var _localizablesDTO__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(
      /*! ./localizablesDTO */
      "5hkl");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "LocalizablesDTO", function () {
        return _localizablesDTO__WEBPACK_IMPORTED_MODULE_8__["LocalizablesDTO"];
      });
      /* harmony import */


      var _treeEventArgs__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(
      /*! ./treeEventArgs */
      "6XeC");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "TreeEventArgs", function () {
        return _treeEventArgs__WEBPACK_IMPORTED_MODULE_9__["TreeEventArgs"];
      });
      /* harmony import */


      var _editablePairsDTO__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(
      /*! ./editablePairsDTO */
      "Se+h");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "EditablePairsDTO", function () {
        return _editablePairsDTO__WEBPACK_IMPORTED_MODULE_10__["EditablePairsDTO"];
      });
      /***/

    },

    /***/
    "I5Ck":
    /*!*************************************************************!*\
      !*** ./src/core/codeshell/base-components/baseComponent.ts ***!
      \*************************************************************/

    /*! exports provided: BaseComponent */

    /***/
    function I5Ck(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "BaseComponent", function () {
        return BaseComponent;
      });
      /* harmony import */


      var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! tslib */
      "mrSG");
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var _angular_router__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! @angular/router */
      "tyNb");
      /* harmony import */


      var _angular_common__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! @angular/common */
      "ofXK");
      /* harmony import */


      var _shell__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
      /*! ../shell */
      "UoIT");
      /* harmony import */


      var _security_models__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(
      /*! ../security/models */
      "gFuU");
      /* harmony import */


      var _helpers__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(
      /*! ../helpers */
      "GYDu");
      /* harmony import */


      var codeshell_main__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(
      /*! codeshell/main */
      "7OZ3");

      var BaseComponent = /*#__PURE__*/function () {
        function BaseComponent(route, inj) {
          _classCallCheck(this, BaseComponent);

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

          this.OnOk = function (e) {
            return Promise.resolve(true);
          };

          this.OnCancel = function (e) {
            return Promise.resolve(true);
          };

          this.Route = route;
          this.Injector = inj;
        }

        _createClass(BaseComponent, [{
          key: "Navigation",
          get: function get() {
            return _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Injector.get(_angular_common__WEBPACK_IMPORTED_MODULE_3__["Location"]);
          }
        }, {
          key: "Loc",
          get: function get() {
            return new _helpers__WEBPACK_IMPORTED_MODULE_6__["LocalizablesDTO"]();
          }
        }, {
          key: "ComponentName",
          get: function get() {
            return this.constructor.name;
          }
        }, {
          key: "Router",
          get: function get() {
            return _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Injector.get(_angular_router__WEBPACK_IMPORTED_MODULE_2__["Router"]);
          }
        }, {
          key: "Resolver",
          get: function get() {
            return this.Injector.get(_angular_core__WEBPACK_IMPORTED_MODULE_1__["ComponentFactoryResolver"]);
          }
        }, {
          key: "ngOnInit",
          value: function ngOnInit() {
            this.construct();
          }
        }, {
          key: "construct",
          value: function construct() {
            var conf = this.Route.routeConfig;

            if (conf) {
              if (conf.data) {
                this.RouteData = Object.assign(new _security_models__WEBPACK_IMPORTED_MODULE_5__["RouteData"](), conf.data);
              } else if (this.ComponentRouteData) {
                this.RouteData = Object.assign(new _security_models__WEBPACK_IMPORTED_MODULE_5__["RouteData"](), this.ComponentRouteData);
              } else {
                this.RouteData = Object.assign(new _security_models__WEBPACK_IMPORTED_MODULE_5__["RouteData"](), {
                  action: "anonymous"
                });
              }

              _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Main.SetTitle(this.RouteData.name);

              if (this.RouteData.IsAnonymous) {
                this.Permission = _security_models__WEBPACK_IMPORTED_MODULE_5__["Permission"].FullAccess;
              } else if (!_shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Session.IsLoggedIn) {
                _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Injector.get(_angular_router__WEBPACK_IMPORTED_MODULE_2__["Router"]).navigate(["/Login"]);
              } else if (this.RouteData.AllowAll) {
                this.Permission = _security_models__WEBPACK_IMPORTED_MODULE_5__["Permission"].FullAccess;
              } else {
                this.Permission = _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Session.GetPermission(this.RouteData.resource);
              }
            } else {
              this.IsEmbedded = true;
            }
          }
        }, {
          key: "GetMainUrl",
          value: function GetMainUrl() {
            return _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Main.GetMainUrl();
          }
        }, {
          key: "ngAfterViewInit",
          value: function ngAfterViewInit() {
            _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].ViewLoaded.emit({});
          }
        }, {
          key: "Log",
          value: function Log() {
            if (this.Debug) {
              var _console;

              for (var _len = arguments.length, params = new Array(_len), _key = 0; _key < _len; _key++) {
                params[_key] = arguments[_key];
              }

              (_console = console).log.apply(_console, [this.ComponentName].concat(params));
            }
          }
        }, {
          key: "SortBy",
          value: function SortBy(prop) {}
        }, {
          key: "GetHeaderClass",
          value: function GetHeaderClass(prop) {
            return null;
          }
        }, {
          key: "GetObjectFromHtml",
          value: function GetObjectFromHtml(ref) {
            var el = ref.nativeElement;
            var attr = el.attributes.getNamedItem("values");

            if (attr != null && attr.value.length > 0) {
              var s = eval('(' + attr.value + ')');

              if (s) {
                return s;
              }
            }

            return null;
          }
        }, {
          key: "GetObjectFromHtmlAs",
          value: function GetObjectFromHtmlAs(ref, type) {
            var s = this.GetObjectFromHtml(ref);
            if (s != null) return Object.assign(new type(), s);
            return null;
          }
        }, {
          key: "OnConstructed",
          value: function OnConstructed() {}
        }, {
          key: "GetComponent",
          value: function GetComponent(opener) {
            var createNew = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : false;
            return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, /*#__PURE__*/regeneratorRuntime.mark(function _callee8() {
              var comp;
              return regeneratorRuntime.wrap(function _callee8$(_context8) {
                while (1) {
                  switch (_context8.prev = _context8.next) {
                    case 0:
                      if (createNew) _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Main.ClearModalLoader();

                      if (!(this._subs[opener.Identifier] == undefined)) {
                        _context8.next = 9;
                        break;
                      }

                      _context8.next = 4;
                      return this.LoadComponentFromParams(opener.Identifier, opener.DefaultComponent);

                    case 4:
                      comp = _context8.sent;

                      if (!comp) {
                        this._subs[opener.Identifier] = null;
                      } else if (opener.Init) {
                        opener.Init(comp.instance);
                      }

                      if (!createNew) this._subs[opener.Identifier] = comp;
                      _context8.next = 10;
                      break;

                    case 9:
                      comp = this._subs[opener.Identifier];

                    case 10:
                      if (!comp) {
                        _context8.next = 15;
                        break;
                      }

                      this.LoadComponentRef(comp);
                      return _context8.abrupt("return", Promise.resolve(comp.instance));

                    case 15:
                      return _context8.abrupt("return", Promise.reject("failed to obtain"));

                    case 16:
                    case "end":
                      return _context8.stop();
                  }
                }
              }, _callee8, this);
            }));
          }
        }, {
          key: "OpenModal",
          value: function OpenModal(path) {
            //var comp = Shell.Main.GetDialogAs<T>(path);
            var e = codeshell_main__WEBPACK_IMPORTED_MODULE_7__["Registry"].Get(path);

            if (e) {
              console.log(this.Resolver);
              var fac = this.Resolver.resolveComponentFactory(e);
              var comp = fac.create(_shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Injector);

              if (comp != null && _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Main.ModalLoader) {
                _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Main.ModalLoader.UseComponent(comp);

                return Promise.resolve(comp);
              }
            }

            return Promise.reject("not found");
          }
        }, {
          key: "LoadComponentRef",
          value: function LoadComponentRef(ref) {
            if (_shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Main.ModalLoader) {
              _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Main.ModalLoader.UseComponent(ref);
            }
          }
        }, {
          key: "LoadComponentFromParams",
          value: function LoadComponentFromParams(fromOther, def) {
            var path = this.ViewParams.Other[fromOther];

            if (path) {
              return this.OpenModal(path);
            } else if (def) {
              return this.OpenModal(def);
            }

            return Promise.reject("No key '" + fromOther + "' in ViewParams.Other");
          }
        }, {
          key: "ModalSearch",
          value: function ModalSearch(modelId, term) {}
        }, {
          key: "GetLookupOptions",
          value: function GetLookupOptions() {
            return this.LookupOptions;
          }
        }, {
          key: "Refresh",
          value: function Refresh() {
            this.ngOnInit();
          }
        }, {
          key: "GetParameter",
          value: function GetParameter(key) {
            var def = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : null;
            var val = this.ViewParams.Other[key];
            if (val && val.length > 0) return this.ViewParams.Other[key];else return def;
          }
        }, {
          key: "GetParameterAsBoolean",
          value: function GetParameterAsBoolean(key) {
            var def = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : false;
            var val = this.GetParameter(key);

            if (val != null) {
              if (val.toLowerCase() == "true") return true;else if (val.toLowerCase() == "false") return false;
            }

            return def;
          }
        }, {
          key: "GetPermission",
          value: function GetPermission(resource) {
            return _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Session.GetPermission(resource);
          }
        }, {
          key: "Notify",
          value: function Notify(message) {
            var type = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : _helpers__WEBPACK_IMPORTED_MODULE_6__["NoteType"].Success;
            var title = arguments.length > 2 && arguments[2] !== undefined ? arguments[2] : undefined;

            _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Main.Notify(message, type, title);
          }
        }, {
          key: "NotifyCanNotDeleteRow",
          value: function NotifyCanNotDeleteRow(res) {
            //debugger
            var tableName = "";
            if (res.tableName) tableName = _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Word(res.tableName);

            var mess = _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Message("entity_has_children", tableName || "Unknown");

            this.Notify(mess, _helpers__WEBPACK_IMPORTED_MODULE_6__["NoteType"].Error, undefined);
          }
        }, {
          key: "NotifyTranslate",
          value: function NotifyTranslate(messageId) {
            var type = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : _helpers__WEBPACK_IMPORTED_MODULE_6__["NoteType"].Success;
            var title = arguments.length > 2 && arguments[2] !== undefined ? arguments[2] : undefined;

            _shell__WEBPACK_IMPORTED_MODULE_4__["Shell"].Main.NotifyTranslate(messageId, type, title);
          }
        }, {
          key: "OnModalHide",
          value: function OnModalHide() {
            var ev = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : null;
            if (!this._modalOk && !this._modalCancel) this.Cancel();
            this._modalOk = false;
            this._modalCancel = false;
          }
        }, {
          key: "Ok",
          value: function Ok() {
            var _this17 = this;

            this._modalOk = true;
            this.OnOk(this).then(function (e) {
              if (e) {
                _this17.Show = false;
              }
            });
          }
        }, {
          key: "Cancel",
          value: function Cancel() {
            var _this18 = this;

            this._modalCancel = true;
            this.OnCancel(this).then(function (e) {
              if (e) _this18.Show = false;
            });
          }
        }, {
          key: "NavigateToComponent",
          value: function NavigateToComponent(url, ext) {
            if (url.length > 0) {
              if (url[0] != "/") url = "/" + url;
              this.Router.navigate([url], ext);
            }
          }
        }, {
          key: "ngOnDestroy",
          value: function ngOnDestroy() {
            for (var i in this._subs) {
              var c = this._subs[i];
              c.destroy();
            }
          }
        }]);

        return BaseComponent;
      }();

      BaseComponent.ɵfac = function BaseComponent_Factory(t) {
        return new (t || BaseComponent)(_angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdirectiveInject"](_angular_router__WEBPACK_IMPORTED_MODULE_2__["ActivatedRoute"]), _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injector"]));
      };

      BaseComponent.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdefineComponent"]({
        type: BaseComponent,
        selectors: [["ng-component"]],
        inputs: {
          IsEmbedded: "IsEmbedded"
        },
        decls: 0,
        vars: 0,
        template: function BaseComponent_Template(rf, ctx) {},
        encapsulation: 2
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵsetClassMetadata"](BaseComponent, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"],
          args: [{
            template: ''
          }]
        }], function () {
          return [{
            type: _angular_router__WEBPACK_IMPORTED_MODULE_2__["ActivatedRoute"]
          }, {
            type: _angular_core__WEBPACK_IMPORTED_MODULE_1__["Injector"]
          }];
        }, {
          IsEmbedded: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_1__["Input"],
            args: ["IsEmbedded"]
          }]
        });
      })();
      /***/

    },

    /***/
    "InfA":
    /*!********************************************!*\
      !*** ./src/core/codeshell/localization.ts ***!
      \********************************************/

    /*! exports provided: LocaleLoader, TranslationService, NgxTranslationService, Translator */

    /***/
    function InfA(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony import */


      var _localization_localeLoader__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! ./localization/localeLoader */
      "6qUw");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "LocaleLoader", function () {
        return _localization_localeLoader__WEBPACK_IMPORTED_MODULE_0__["LocaleLoader"];
      });
      /* harmony import */


      var _localization_translationService__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ./localization/translationService */
      "0ujZ");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "TranslationService", function () {
        return _localization_translationService__WEBPACK_IMPORTED_MODULE_1__["TranslationService"];
      });
      /* harmony import */


      var _localization_ngxTransationService__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! ./localization/ngxTransationService */
      "Sgtl");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "NgxTranslationService", function () {
        return _localization_ngxTransationService__WEBPACK_IMPORTED_MODULE_2__["NgxTranslationService"];
      });
      /* harmony import */


      var _localization_translator__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! ./localization/translator */
      "8v3h");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "Translator", function () {
        return _localization_translator__WEBPACK_IMPORTED_MODULE_3__["Translator"];
      });
      /***/

    },

    /***/
    "JLZs":
    /*!************************************************!*\
      !*** ./src/core/codeshell/helpers/listItem.ts ***!
      \************************************************/

    /*! exports provided: ListItem, BoundListItem */

    /***/
    function JLZs(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "ListItem", function () {
        return ListItem;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "BoundListItem", function () {
        return BoundListItem;
      });

      var ListItem = /*#__PURE__*/function () {
        function ListItem() {
          _classCallCheck(this, ListItem);

          this.id = 0;
          this.state = "Detached";
          this.selected = false;
        }

        _createClass(ListItem, [{
          key: "AddToChangeList",
          value: function AddToChangeList() {
            if (this.state == "Detached" || !this.state) {
              this.state = "Added";
            } else if (this.state == "Attached") {
              this.state = "Modified";
            }
          }
        }, {
          key: "SetModified",
          value: function SetModified() {
            if (this.state != "Added" && this.state != "Detached") this.state = "Modified";
          }
        }, {
          key: "SetRemoved",
          value: function SetRemoved() {
            if (this.state != "Added") this.state = "Removed";
          }
        }, {
          key: "SetAdded",
          value: function SetAdded() {
            if (this.state == "Removed") this.state = "Attached";else if (this.state != "Modified" && this.state != "Attached") this.state = "Added";
          }
        }, {
          key: "SetAttached",
          value: function SetAttached() {
            var r = ["Added", "Removed", "Modified"];
            if (r.indexOf(this.state) > -1) this.state = "Attached";
          }
        }, {
          key: "ApplyTo",
          value: function ApplyTo(items) {
            if (this.selected) this.AddTo(items);else this.RemoveFrom(items);
          }
        }, {
          key: "RemoveFrom",
          value: function RemoveFrom(items) {
            if (this.state == "Added") {
              var ind = items.indexOf(this);
              if (ind > -1) items.splice(ind, 1);
              this.state = "Detached";
            } else {
              this.state = "Removed";
            }

            this.selected = false;
          }
        }, {
          key: "AddTo",
          value: function AddTo(items) {
            if (this.state == "Removed") {
              this.state = "Attached";
            } else if (this.state != "Added" && this.state != "Attached") {
              this.state = "Added";
              items.push(this);
            }

            this.selected = true;
          }
        }, {
          key: "SelectOnly",
          value: function SelectOnly(items) {
            var x = [];

            var _iterator5 = _createForOfIteratorHelper(items),
                _step5;

            try {
              for (_iterator5.s(); !(_step5 = _iterator5.n()).done;) {
                var _d = _step5.value;
                x.push(_d);
              }
            } catch (err) {
              _iterator5.e(err);
            } finally {
              _iterator5.f();
            }

            for (var _i = 0, _x = x; _i < _x.length; _i++) {
              var d = _x[_i];
              d.RemoveFrom(items);
            }

            this.AddTo(items);
          }
        }], [{
          key: "IsEmpty",
          value: function IsEmpty(items) {
            if (!items || items.length == 0) return true;
            return !items.some(function (d) {
              return d.state != "Removed";
            });
          }
        }, {
          key: "GetLength",
          value: function GetLength(items) {
            if (!items) return 0;
            return items.filter(function (e) {
              return e.state != 'Removed' && e.state != 'Detached';
            }).length;
          }
        }, {
          key: "GetChangedItems",
          value: function GetChangedItems(items) {
            return items.filter(function (e) {
              return e.state == "Added" || e.state == "Modified" || e.state == "Removed";
            });
          }
        }, {
          key: "GetAdded",
          value: function GetAdded(items) {
            return items.filter(function (e) {
              return e.state == "Added";
            });
          }
        }, {
          key: "GetModifiedOrDeleted",
          value: function GetModifiedOrDeleted(items) {
            return items.filter(function (e) {
              return e.state == "Modified" || e.state == "Removed";
            });
          }
        }, {
          key: "HasChanges",
          value: function HasChanges(items) {
            return items.some(function (e) {
              return e.state == "Added" || e.state == "Modified" || e.state == "Removed";
            });
          }
        }, {
          key: "FromDB",
          value: function FromDB(obj) {
            var it = Object.assign(new ListItem(), obj);
            it.selected = true;
            it.state = "Attached";
            return it;
          }
        }, {
          key: "FromDB_GEN",
          value: function FromDB_GEN(con, obj) {
            var it = Object.assign(new con(), obj);
            it.selected = true;
            it.state = "Attached";
            return it;
          }
        }, {
          key: "Detached",
          value: function Detached(obj) {
            var it = Object.assign(new ListItem(), obj);
            it.selected = false;
            it.state = "Detached";
            return it;
          }
        }, {
          key: "Detached_GEN",
          value: function Detached_GEN(con, obj) {
            var it = Object.assign(new con(), obj);
            it.selected = false;
            it.state = "Detached";
            return it;
          }
        }, {
          key: "Convert",
          value: function Convert(lst) {
            var ret = [];

            for (var i in lst) {
              ret[i] = ListItem.FromDB(lst[i]);
            }

            return ret;
          }
        }, {
          key: "Convert_GEN",
          value: function Convert_GEN(con, lst) {
            var ret = [];

            for (var i in lst) {
              ret[i] = ListItem.FromDB_GEN(con, lst[i]);
            }

            return ret;
          }
        }]);

        return ListItem;
      }();

      var BoundListItem = /*#__PURE__*/function (_ListItem) {
        _inherits(BoundListItem, _ListItem);

        var _super9 = _createSuper(BoundListItem);

        function BoundListItem() {
          _classCallCheck(this, BoundListItem);

          return _super9.apply(this, arguments);
        }

        return BoundListItem;
      }(ListItem);
      /***/

    },

    /***/
    "K+1B":
    /*!********************************************************!*\
      !*** ./src/core/codeshell/helpers/componentRequest.ts ***!
      \********************************************************/

    /*! exports provided: ComponentRequest */

    /***/
    function K1B(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "ComponentRequest", function () {
        return ComponentRequest;
      });

      var ComponentRequest = function ComponentRequest() {
        _classCallCheck(this, ComponentRequest);

        this.Identifier = "";
        this.DefaultComponent = "";
      };
      /***/

    },

    /***/
    "L7BY":
    /*!*****************************************************************!*\
      !*** ./src/core/codeshell/components/search-group.component.ts ***!
      \*****************************************************************/

    /*! exports provided: SearchGroup */

    /***/
    function L7BY(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "SearchGroup", function () {
        return SearchGroup;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! @angular/forms */
      "3Pt+");

      var _c0 = function _c0() {
        return {
          standalone: true
        };
      };

      var SearchGroup = /*#__PURE__*/function () {
        function SearchGroup() {
          _classCallCheck(this, SearchGroup);

          this.ChangeEvent = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        }

        _createClass(SearchGroup, [{
          key: "OnSearch",
          value: function OnSearch() {
            this.ChangeEvent.emit(this.SearchTerm);
          }
        }]);

        return SearchGroup;
      }();

      SearchGroup.ɵfac = function SearchGroup_Factory(t) {
        return new (t || SearchGroup)();
      };

      SearchGroup.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineComponent"]({
        type: SearchGroup,
        selectors: [["search-group"]],
        outputs: {
          ChangeEvent: "termChange"
        },
        decls: 6,
        vars: 4,
        consts: [[1, "col-sm-12"], [1, "input-group"], ["type", "text", 1, "form-control", 3, "ngModel", "ngModelOptions", "placeholder", "ngModelChange", "keydown.enter"], [1, "input-group-btn"], [1, "btn", "btn-default", 3, "click"], [1, "fa", "fa-search"]],
        template: function SearchGroup_Template(rf, ctx) {
          if (rf & 1) {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "div", 0);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](1, "div", 1);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](2, "input", 2);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("ngModelChange", function SearchGroup_Template_input_ngModelChange_2_listener($event) {
              return ctx.SearchTerm = $event;
            })("keydown.enter", function SearchGroup_Template_input_keydown_enter_2_listener() {
              return ctx.OnSearch();
            });

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](3, "span", 3);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](4, "button", 4);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function SearchGroup_Template_button_click_4_listener() {
              return ctx.OnSearch();
            });

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](5, "i", 5);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
          }

          if (rf & 2) {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpropertyInterpolate"]("placeholder", "Words.Search");

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngModel", ctx.SearchTerm)("ngModelOptions", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpureFunction0"](3, _c0));
          }
        },
        directives: [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["DefaultValueAccessor"], _angular_forms__WEBPACK_IMPORTED_MODULE_1__["NgControlStatus"], _angular_forms__WEBPACK_IMPORTED_MODULE_1__["NgModel"]],
        encapsulation: 2
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](SearchGroup, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"],
          args: [{
            templateUrl: "./search-group.component.html",
            selector: "search-group"
          }]
        }], null, {
          ChangeEvent: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"],
            args: ["termChange"]
          }]
        });
      })();
      /***/

    },

    /***/
    "LR6F":
    /*!****************************************************!*\
      !*** ./src/core/codeshell/http/httpServiceBase.ts ***!
      \****************************************************/

    /*! exports provided: HttpServiceBase */

    /***/
    function LR6F(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "HttpServiceBase", function () {
        return HttpServiceBase;
      });
      /* harmony import */


      var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! tslib */
      "mrSG");
      /* harmony import */


      var _security_sessionManager__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ../security/sessionManager */
      "M6Pn");
      /* harmony import */


      var _angular_common_http___WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! @angular/common/http/ */
      "tk/3");
      /* harmony import */


      var _shell__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! ../shell */
      "UoIT");
      /* harmony import */


      var _httpRequest__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
      /*! ./httpRequest */
      "vyk5");
      /* harmony import */


      var _utilities_utils__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(
      /*! ../utilities/utils */
      "VXkE");

      var HttpServiceBase = /*#__PURE__*/function () {
        function HttpServiceBase() {
          _classCallCheck(this, HttpServiceBase);

          this.Silent = false;
          this.Client = _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Injector.get(_angular_common_http___WEBPACK_IMPORTED_MODULE_2__["HttpClient"]);
          this.Sessions = _security_sessionManager__WEBPACK_IMPORTED_MODULE_1__["SessionManager"].Current();
          this.Server = _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Main.Config;
        }

        _createClass(HttpServiceBase, [{
          key: "Headers",
          get: function get() {
            var head = {
              "tenant-code": this.Server.Domain,
              "locale": this.Server.Locale,
              "ui-version": this.Server.Version
            };
            this.Sessions.CheckToken();
            var tok = this.Sessions.GetToken();
            if (tok != null) head["auth-token"] = tok.Token;
            if (this.SignalRConnctionId) head["connection-id"] = this.SignalRConnctionId;
            this.AddCustomHeaders(head);
            return head;
            return {};
          }
        }, {
          key: "AddCustomHeaders",
          value: function AddCustomHeaders(data) {}
        }, {
          key: "Get",
          value: function Get(action, params) {
            var req = this.InitializeRequest(action, params);
            return this.process(_httpRequest__WEBPACK_IMPORTED_MODULE_4__["Methods"].Get, req);
          }
        }, {
          key: "GetAsHtml",
          value: function GetAsHtml(action, params) {
            var req = this.InitializeRequest(action, params);

            if (req.Params.headers) {
              req.Params.responseType = "text/html";
            }

            return this.process(_httpRequest__WEBPACK_IMPORTED_MODULE_4__["Methods"].Get, req);
          }
        }, {
          key: "Post",
          value: function Post(action, body, params) {
            var req = this.InitializeRequest(action, params, body);
            return this.process(_httpRequest__WEBPACK_IMPORTED_MODULE_4__["Methods"].Post, req);
          }
        }, {
          key: "Put",
          value: function Put(action, body, params) {
            var req = this.InitializeRequest(action, params, body);
            return this.process(_httpRequest__WEBPACK_IMPORTED_MODULE_4__["Methods"].Put, req);
          }
        }, {
          key: "Delete",
          value: function Delete(action, id) {
            var req = this.InitializeRequest(action, id);
            return this.process(_httpRequest__WEBPACK_IMPORTED_MODULE_4__["Methods"].Delete, req);
          }
        }, {
          key: "GetAs",
          value: function GetAs(action, params) {
            var req = this.InitializeRequest(action, params);
            return this.processAs(_httpRequest__WEBPACK_IMPORTED_MODULE_4__["Methods"].Get, req);
          }
        }, {
          key: "PostAs",
          value: function PostAs(action, body, params) {
            var req = this.InitializeRequest(action, params, body);
            return this.processAs(_httpRequest__WEBPACK_IMPORTED_MODULE_4__["Methods"].Post, req);
          }
        }, {
          key: "InitializeRequest",
          value: function InitializeRequest(action, params, body) {
            var url = this.BaseUrl + "/" + action;
            var r = new _httpRequest__WEBPACK_IMPORTED_MODULE_4__["HttpRequest"](url, params, body);
            r.Params.headers = this.Headers;
            return r;
          }
        }, {
          key: "process",
          value: function process(method, req) {
            return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, /*#__PURE__*/regeneratorRuntime.mark(function _callee9() {
              var _this19 = this;

              var p;
              return regeneratorRuntime.wrap(function _callee9$(_context9) {
                while (1) {
                  switch (_context9.prev = _context9.next) {
                    case 0:
                      p = new Promise(function () {});
                      if (!this.Silent) _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Main.ShowLoading();
                      _context9.t0 = method;
                      _context9.next = _context9.t0 === _httpRequest__WEBPACK_IMPORTED_MODULE_4__["Methods"].Get ? 5 : _context9.t0 === _httpRequest__WEBPACK_IMPORTED_MODULE_4__["Methods"].Post ? 7 : _context9.t0 === _httpRequest__WEBPACK_IMPORTED_MODULE_4__["Methods"].Put ? 9 : _context9.t0 === _httpRequest__WEBPACK_IMPORTED_MODULE_4__["Methods"].Delete ? 11 : 13;
                      break;

                    case 5:
                      p = this.Client.get(req.Url, req.Params).toPromise();
                      return _context9.abrupt("break", 13);

                    case 7:
                      p = this.Client.post(req.Url, req.Body, req.Params).toPromise();
                      return _context9.abrupt("break", 13);

                    case 9:
                      p = this.Client.put(req.Url, req.Body, req.Params).toPromise();
                      return _context9.abrupt("break", 13);

                    case 11:
                      p = this.Client["delete"](req.Url, req.Params).toPromise();
                      return _context9.abrupt("break", 13);

                    case 13:
                      p["catch"](function (e) {
                        return _this19.OnError(e);
                      });
                      p.then(function (e) {
                        return _this19.OnRequestProcessed(e);
                      });
                      return _context9.abrupt("return", p);

                    case 16:
                    case "end":
                      return _context9.stop();
                  }
                }
              }, _callee9, this);
            }));
          }
        }, {
          key: "processAs",
          value: function processAs(method, req) {
            var _this20 = this;

            var p = new Promise(function () {});
            if (!this.Silent) _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Main.ShowLoading();

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
                p = this.Client["delete"](req.Url, req.Params).toPromise();
                break;
            }

            p["catch"](function (e) {
              return _this20.OnError(e);
            });
            p.then(function (e) {
              return _this20.OnRequestProcessed(e);
            });
            return p;
          }
        }, {
          key: "OnError",
          value: function OnError(e) {
            _utilities_utils__WEBPACK_IMPORTED_MODULE_5__["Utils"].HandleError(e);

            _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Main.HideLoading();
          }
        }, {
          key: "OnRequestProcessed",
          value: function OnRequestProcessed(e) {
            _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Main.HideLoading();
          }
        }]);

        return HttpServiceBase;
      }();
      /***/

    },

    /***/
    "M6Pn":
    /*!*******************************************************!*\
      !*** ./src/core/codeshell/security/sessionManager.ts ***!
      \*******************************************************/

    /*! exports provided: SessionManager */

    /***/
    function M6Pn(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "SessionManager", function () {
        return SessionManager;
      });
      /* harmony import */


      var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! tslib */
      "mrSG");
      /* harmony import */


      var _security_models__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ../security/models */
      "gFuU");
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var _helpers__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! ../helpers */
      "GYDu");
      /* harmony import */


      var js_cookie__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
      /*! js-cookie */
      "p46w");
      /* harmony import */


      var js_cookie__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(js_cookie__WEBPACK_IMPORTED_MODULE_4__);
      /* harmony import */


      var _utilities_utils__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(
      /*! ../utilities/utils */
      "VXkE");
      /* harmony import */


      var _shell__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(
      /*! ../shell */
      "UoIT");
      /* harmony import */


      var _accountServiceBase__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(
      /*! ./accountServiceBase */
      "wNr3");
      /* harmony import */


      var _tokenStorage__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(
      /*! ./tokenStorage */
      "yvMo");

      var userData = null;
      var loaded = false;

      var SessionManager = /*#__PURE__*/function () {
        function SessionManager() {
          _classCallCheck(this, SessionManager);

          this._tokenData = null;
          this.IsLoggedIn = false;
          this.LogStatus = new _angular_core__WEBPACK_IMPORTED_MODULE_2__["EventEmitter"](false);
          this.OnLogin = new _angular_core__WEBPACK_IMPORTED_MODULE_2__["EventEmitter"]();
          this.OnUserDataFailed = new _angular_core__WEBPACK_IMPORTED_MODULE_2__["EventEmitter"]();
        }

        _createClass(SessionManager, [{
          key: "TokenStorage",
          get: function get() {
            if (!this._tokenStorage) this._tokenStorage = _shell__WEBPACK_IMPORTED_MODULE_6__["Shell"].Injector.get(_tokenStorage__WEBPACK_IMPORTED_MODULE_8__["TokenStorage"]);
            return this._tokenStorage;
          }
        }, {
          key: "User",
          get: function get() {
            if (!userData) throw "Not logged in";
            return userData;
          }
        }, {
          key: "TryRefreshAsync",
          value: function TryRefreshAsync() {
            return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, /*#__PURE__*/regeneratorRuntime.mark(function _callee10() {
              var ref, serv, data;
              return regeneratorRuntime.wrap(function _callee10$(_context10) {
                while (1) {
                  switch (_context10.prev = _context10.next) {
                    case 0:
                      ref = this.TokenStorage.GetRefreshToken();

                      if (!ref) {
                        _context10.next = 13;
                        break;
                      }

                      serv = _shell__WEBPACK_IMPORTED_MODULE_6__["Shell"].Injector.get(_accountServiceBase__WEBPACK_IMPORTED_MODULE_7__["AccountServiceBase"]);
                      _context10.prev = 3;
                      _context10.next = 6;
                      return serv.RefreshToken(ref);

                    case 6:
                      data = _context10.sent;
                      this.StartSession(data);
                      _context10.next = 13;
                      break;

                    case 10:
                      _context10.prev = 10;
                      _context10.t0 = _context10["catch"](3);
                      Promise.reject("Failed to refresh using token");

                    case 13:
                      return _context10.abrupt("return", Promise.reject("No token or refresh token found"));

                    case 14:
                    case "end":
                      return _context10.stop();
                  }
                }
              }, _callee10, this, [[3, 10]]);
            }));
          }
        }, {
          key: "ReloadUserDataAsync",
          value: function ReloadUserDataAsync() {
            return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, /*#__PURE__*/regeneratorRuntime.mark(function _callee11() {
              var token;
              return regeneratorRuntime.wrap(function _callee11$(_context11) {
                while (1) {
                  switch (_context11.prev = _context11.next) {
                    case 0:
                      token = this.GetToken();

                      if (token) {
                        _context11.next = 3;
                        break;
                      }

                      return _context11.abrupt("return", Promise.reject("Cannot reload user data without token"));

                    case 3:
                      _context11.next = 5;
                      return this.GetUserDataFromServer();

                    case 5:
                      if (userData) {
                        this.MapPermissions(userData);
                        this.OnLogin.emit(userData);
                      }

                      return _context11.abrupt("return", userData);

                    case 7:
                    case "end":
                      return _context11.stop();
                  }
                }
              }, _callee11, this);
            }));
          }
        }, {
          key: "GetUserAsync",
          value: function GetUserAsync() {
            if (userData) {
              return Promise.resolve(userData);
            }

            if (!SessionManager._loadPromise) {
              var token = this.GetToken();

              if (!token) {
                SessionManager._loadPromise = this.TryRefreshAsync();
              } else {
                SessionManager._loadPromise = this.GetUserDataFromServer();
              }
            }

            return SessionManager._loadPromise;
          }
        }, {
          key: "GetUserDataFromServer",
          value: function GetUserDataFromServer() {
            return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, /*#__PURE__*/regeneratorRuntime.mark(function _callee12() {
              var serv, res;
              return regeneratorRuntime.wrap(function _callee12$(_context12) {
                while (1) {
                  switch (_context12.prev = _context12.next) {
                    case 0:
                      _context12.prev = 0;
                      serv = _shell__WEBPACK_IMPORTED_MODULE_6__["Shell"].Injector.get(_accountServiceBase__WEBPACK_IMPORTED_MODULE_7__["AccountServiceBase"]);
                      _context12.next = 4;
                      return serv.GetUserData();

                    case 4:
                      userData = _context12.sent;
                      this.MapPermissions(userData);
                      return _context12.abrupt("return", Promise.resolve(userData));

                    case 9:
                      _context12.prev = 9;
                      _context12.t0 = _context12["catch"](0);
                      res = new _helpers__WEBPACK_IMPORTED_MODULE_3__["SubmitResult"]();
                      res.code = 1;
                      res.message = "unable_to_connect_to_server";

                      if (_context12.t0.error && _context12.t0.error.code) {
                        res = Object.assign(new _helpers__WEBPACK_IMPORTED_MODULE_3__["SubmitResult"](), _context12.t0.error);
                      }

                      this.OnUserDataFailed.emit(res);
                      return _context12.abrupt("return", Promise.reject(_context12.t0));

                    case 17:
                    case "end":
                      return _context12.stop();
                  }
                }
              }, _callee12, this, [[0, 9]]);
            }));
          }
        }, {
          key: "MapPermissions",
          value: function MapPermissions(data) {
            if (data) {
              for (var i in data.permissions) {
                var item = new _security_models__WEBPACK_IMPORTED_MODULE_1__["Permission"]();
                Object.assign(item, data.permissions[i]);
                data.permissions[i] = item;
              }
            }
          }
        }, {
          key: "StartSession",
          value: function StartSession(data) {
            userData = data.userData;
            this.MapPermissions(userData);
            this._tokenData = Object.assign(new _security_models__WEBPACK_IMPORTED_MODULE_1__["TokenData"](), {
              Token: data.token,
              Expiry: data.tokenExpiry
            });
            this.TokenStorage.SaveToken(this._tokenData);
            if (data.refreshToken) this.TokenStorage.SaveRefreshToken(data.refreshToken);
            this.OnLogin.emit(userData);
            this.LogStatus.emit(true);
            this.IsLoggedIn = true;
          }
        }, {
          key: "CheckToken",
          value: function CheckToken() {
            this._tokenData = this.TokenStorage.LoadToken();

            if (this._tokenData == null) {
              this.IsLoggedIn = false;
            } else {
              var token = this._tokenData;
              this.IsLoggedIn = new Date() < new Date(token.Expiry);
            }

            this.LogStatus.emit(this.IsLoggedIn);
          }
        }, {
          key: "EndSession",
          value: function EndSession() {
            this.TokenStorage.Clear();
            userData = null;
            this.LogStatus.emit(false);
            this.IsLoggedIn = false;
            this._tokenData = null;
          }
        }, {
          key: "GetDeviceId",
          value: function GetDeviceId() {
            var id = js_cookie__WEBPACK_IMPORTED_MODULE_4__["get"]("DID");

            if (!id) {
              var d = new Date();
              var no = Math.random().toString();
              id = new Date().getTime().toString() + '_' + Object(_utilities_utils__WEBPACK_IMPORTED_MODULE_5__["String_GetAfterLast"])(no, ".");
              d.setFullYear(d.getFullYear() + 3);
              js_cookie__WEBPACK_IMPORTED_MODULE_4__["set"]("DID", id, {
                path: "/",
                expires: d
              });
            }

            return id;
          }
        }, {
          key: "GetPermission",
          value: function GetPermission(id) {
            if (this.IsLoggedIn && this.User.permissions[id]) {
              var u = this.User;
              return Object.assign(new _security_models__WEBPACK_IMPORTED_MODULE_1__["Permission"](), u.permissions[id]);
            } else {
              var s = new _security_models__WEBPACK_IMPORTED_MODULE_1__["Permission"]();
              s.view = false;
              return s;
            }
          }
        }, {
          key: "Locale",
          get: function get() {
            return js_cookie__WEBPACK_IMPORTED_MODULE_4__["get"]("Locale");
          }
        }, {
          key: "GetToken",
          value: function GetToken() {
            if (!this.IsLoggedIn) {
              return null;
            }

            if (this._tokenData && this._tokenData.IsExpired()) return null;
            return this._tokenData;
          }
        }], [{
          key: "Current",
          value: function Current() {
            if (this._instance == null) {
              this._instance = new SessionManager();

              this._instance.CheckToken();
            }

            return this._instance;
          }
        }]);

        return SessionManager;
      }();

      SessionManager.ɵfac = function SessionManager_Factory(t) {
        return new (t || SessionManager)();
      };

      SessionManager.ɵprov = _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵdefineInjectable"]({
        token: SessionManager,
        factory: SessionManager.ɵfac
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵsetClassMetadata"](SessionManager, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_2__["Injectable"]
        }], null, null);
      })();
      /***/

    },

    /***/
    "N0SU":
    /*!***************************************************!*\
      !*** ./src/core/codeshell/services/listSource.ts ***!
      \***************************************************/

    /*! exports provided: ListSource */

    /***/
    function N0SU(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "ListSource", function () {
        return ListSource;
      });
      /* harmony import */


      var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! tslib */
      "mrSG");
      /* harmony import */


      var _helpers__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ../helpers */
      "GYDu");
      /* harmony import */


      var _listSelectionService__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! ./listSelectionService */
      "0Wec");

      var ListSource = /*#__PURE__*/function () {
        function ListSource(showing, predicate) {
          _classCallCheck(this, ListSource);

          this._list = [];
          this._totalCount = 0;
          this._useSelect = false; // private _currentPage: number = 0;

          this.InitialSelection = null;
          this.Opts = {
            Showing: 0,
            Skip: 0
          };
          this.pageIndex = 0;
          this.LoadedOnce = false;
          this.UseJoin = true;
          this.Selection = null;
          this.Loader = predicate;
          this.Opts.Showing = showing;
        }

        _createClass(ListSource, [{
          key: "List",
          get: function get() {
            return this._list;
          }
        }, {
          key: "TotalCount",
          get: function get() {
            return this._totalCount;
          }
        }, {
          key: "UseSelection",
          get: function get() {
            return this.Selection != null;
          },
          set: function set(val) {
            this.Selection = new _listSelectionService__WEBPACK_IMPORTED_MODULE_2__["ListSelectionService"]();
          }
        }, {
          key: "Retag",
          value: function Retag() {
            if (this.TagArguments) this._list = _helpers__WEBPACK_IMPORTED_MODULE_1__["Tagged"].JoinLists(this.TagArguments);
          }
        }, {
          key: "SelectById",
          value: function SelectById(id) {
            if (this.TagArguments) {
              var s = this.List.find(function (d) {
                return d.id == id;
              });

              if (s) {
                s.Tag.selected = true;
                s.Tag.AddTo(this.TagArguments.Source);
              }
            }
          }
        }, {
          key: "AfterLoad",
          value: function AfterLoad(e) {
            if (this.OnDataLoaded) this.OnDataLoaded(e.list);

            if (this.UseJoin) {
              if (this.TagArguments) {
                this.TagArguments.Data = e.list;
                this._list = _helpers__WEBPACK_IMPORTED_MODULE_1__["Tagged"].JoinLists(this.TagArguments);
              } else {
                this._list = [];

                for (var i in e.list) {
                  this._list[i] = Object.assign(new _helpers__WEBPACK_IMPORTED_MODULE_1__["Tagged"](), e.list[i]);
                }
              }
            } else {
              this._list = e.list;
            }

            this._totalCount = e.totalCount;
            this.LoadedOnce = true;

            if (this.InitialSelection) {
              var _iterator6 = _createForOfIteratorHelper(this.InitialSelection),
                  _step6;

              try {
                for (_iterator6.s(); !(_step6 = _iterator6.n()).done;) {
                  var id = _step6.value;
                  this.SelectById(id);
                }
              } catch (err) {
                _iterator6.e(err);
              } finally {
                _iterator6.f();
              }
            }
          }
        }, {
          key: "LoadDataAsync",
          value: function LoadDataAsync() {
            return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, /*#__PURE__*/regeneratorRuntime.mark(function _callee13() {
              var data;
              return regeneratorRuntime.wrap(function _callee13$(_context13) {
                while (1) {
                  switch (_context13.prev = _context13.next) {
                    case 0:
                      _context13.next = 2;
                      return this.Loader(this.Opts);

                    case 2:
                      data = _context13.sent;
                      this.AfterLoad(data);
                      return _context13.abrupt("return", data);

                    case 5:
                    case "end":
                      return _context13.stop();
                  }
                }
              }, _callee13, this);
            }));
          }
        }, {
          key: "LoadData",
          value: function LoadData() {
            var _this21 = this;

            this.Loader(this.Opts).then(function (e) {
              _this21.AfterLoad(e);
            });
          }
        }, {
          key: "PageChanged",
          value: function PageChanged(id) {
            this.Opts.Skip = this.Opts.Showing * id;
            this.LoadData();
          }
        }, {
          key: "Search",
          value: function Search(term) {
            this.Opts.SearchTerm = term;
            this.Opts.Skip = 0;
            this.pageIndex = 0;
            this.LoadData();
          }
        }, {
          key: "SetFilters",
          value: function SetFilters(filters) {
            this.Opts.Filters = JSON.stringify(filters);
          }
        }], [{
          key: "Empty",
          get: function get() {
            return new ListSource(10, function (e) {
              return Promise.resolve(new _helpers__WEBPACK_IMPORTED_MODULE_1__["LoadResult"]());
            });
          }
        }]);

        return ListSource;
      }();
      /***/

    },

    /***/
    "NXgI":
    /*!**********************************************!*\
      !*** ./src/core/base/example-base.module.ts ***!
      \**********************************************/

    /*! exports provided: ExampleBaseModule */

    /***/
    function NXgI(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "ExampleBaseModule", function () {
        return ExampleBaseModule;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var codeshell_main__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! codeshell/main */
      "7OZ3");
      /* harmony import */


      var codeshell_security__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! codeshell/security */
      "U6Sh");
      /* harmony import */


      var _http_account_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! ./http/account.service */
      "2enV");
      /* harmony import */


      var _main_login_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
      /*! ./main/login.component */
      "8sze");
      /* harmony import */


      var _main_top_bar_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(
      /*! ./main/top-bar.component */
      "WNdI");
      /* harmony import */


      var _main_navigation_side_bar_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(
      /*! ./main/navigation-side-bar.component */
      "a3Ap");

      var ExampleBaseModule = /*#__PURE__*/function () {
        function ExampleBaseModule() {
          _classCallCheck(this, ExampleBaseModule);
        }

        _createClass(ExampleBaseModule, null, [{
          key: "forRoot",
          value: function forRoot() {
            return {
              ngModule: ExampleBaseModule,
              providers: [{
                provide: codeshell_security__WEBPACK_IMPORTED_MODULE_2__["AccountServiceBase"],
                useClass: _http_account_service__WEBPACK_IMPORTED_MODULE_3__["AccountService"]
              }, {
                provide: codeshell_security__WEBPACK_IMPORTED_MODULE_2__["TokenStorage"],
                useClass: codeshell_security__WEBPACK_IMPORTED_MODULE_2__["SessionTokenData"]
              }]
            };
          }
        }]);

        return ExampleBaseModule;
      }();

      ExampleBaseModule.ɵmod = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineNgModule"]({
        type: ExampleBaseModule
      });
      ExampleBaseModule.ɵinj = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineInjector"]({
        factory: function ExampleBaseModule_Factory(t) {
          return new (t || ExampleBaseModule)();
        },
        imports: [[codeshell_main__WEBPACK_IMPORTED_MODULE_1__["CodeShellModule"]], codeshell_main__WEBPACK_IMPORTED_MODULE_1__["CodeShellModule"]]
      });

      (function () {
        (typeof ngJitMode === "undefined" || ngJitMode) && _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵsetNgModuleScope"](ExampleBaseModule, {
          declarations: [_main_login_component__WEBPACK_IMPORTED_MODULE_4__["Login"], _main_top_bar_component__WEBPACK_IMPORTED_MODULE_5__["TopBar"], _main_navigation_side_bar_component__WEBPACK_IMPORTED_MODULE_6__["NavigationSideBar"]],
          imports: [codeshell_main__WEBPACK_IMPORTED_MODULE_1__["CodeShellModule"]],
          exports: [codeshell_main__WEBPACK_IMPORTED_MODULE_1__["CodeShellModule"], _main_login_component__WEBPACK_IMPORTED_MODULE_4__["Login"], _main_top_bar_component__WEBPACK_IMPORTED_MODULE_5__["TopBar"], _main_navigation_side_bar_component__WEBPACK_IMPORTED_MODULE_6__["NavigationSideBar"]]
        });
      })();
      /*@__PURE__*/


      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](ExampleBaseModule, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"],
          args: [{
            declarations: [_main_login_component__WEBPACK_IMPORTED_MODULE_4__["Login"], _main_top_bar_component__WEBPACK_IMPORTED_MODULE_5__["TopBar"], _main_navigation_side_bar_component__WEBPACK_IMPORTED_MODULE_6__["NavigationSideBar"]],
            imports: [codeshell_main__WEBPACK_IMPORTED_MODULE_1__["CodeShellModule"]],
            exports: [codeshell_main__WEBPACK_IMPORTED_MODULE_1__["CodeShellModule"], _main_login_component__WEBPACK_IMPORTED_MODULE_4__["Login"], _main_top_bar_component__WEBPACK_IMPORTED_MODULE_5__["TopBar"], _main_navigation_side_bar_component__WEBPACK_IMPORTED_MODULE_6__["NavigationSideBar"]]
          }]
        }], null, null);
      })();
      /***/

    },

    /***/
    "NnUC":
    /*!******************************************************!*\
      !*** ./src/core/codeshell/services/tableServices.ts ***!
      \******************************************************/

    /*! exports provided: TableService */

    /***/
    function NnUC(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "TableService", function () {
        return TableService;
      });
      /* harmony import */


      var codeshell_helpers_listItem__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! codeshell/helpers/listItem */
      "JLZs");
      /* harmony import */


      var codeshell_main__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! codeshell/main */
      "7OZ3");

      var TableService = /*#__PURE__*/function () {
        function TableService(listRef) {
          _classCallCheck(this, TableService);

          this.listRef = listRef;
          this.Adding = false;
        }

        _createClass(TableService, [{
          key: "List",
          get: function get() {
            return this.listRef();
          }
        }, {
          key: "_removeAddRow",
          value: function _removeAddRow() {
            var mod = this.List.find(function (d) {
              return d.addingRow == true;
            });
            if (mod) Object(codeshell_main__WEBPACK_IMPORTED_MODULE_1__["List_RemoveItem"])(this.List, mod);
          }
        }, {
          key: "StartAdd",
          value: function StartAdd() {
            this.Adding = true;
            this.List.push(codeshell_helpers_listItem__WEBPACK_IMPORTED_MODULE_0__["ListItem"].Detached({
              addingRow: true
            }));
          }
        }, {
          key: "CancelAdd",
          value: function CancelAdd() {
            this._removeAddRow();

            this.Adding = false;
          }
        }, {
          key: "SubmitAdd",
          value: function SubmitAdd() {
            var mod = this.List.find(function (d) {
              return d.addingRow == true;
            });

            if (mod) {
              mod.SetAdded();
              mod.addingRow = false;
              this.StartAdd();
            }
          }
        }]);

        return TableService;
      }();
      /***/

    },

    /***/
    "OI30":
    /*!**************************************************!*\
      !*** ./src/core/codeshell/helpers/interfaces.ts ***!
      \**************************************************/

    /*! no exports provided */

    /***/
    function OI30(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /***/

    },

    /***/
    "PCNd":
    /*!*****************************************!*\
      !*** ./src/app/shared/shared.module.ts ***!
      \*****************************************/

    /*! exports provided: SharedModule */

    /***/
    function PCNd(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "SharedModule", function () {
        return SharedModule;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var _base_example_base_module__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! @base/example-base.module */
      "NXgI");
      /* harmony import */


      var _ngx_translate_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! @ngx-translate/core */
      "sYmb");
      /* harmony import */


      var codeshell_main__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! codeshell/main */
      "7OZ3");

      var SharedModule = function SharedModule(trans, conf) {
        _classCallCheck(this, SharedModule);

        trans.setDefaultLang(conf.Locale);
        trans.use(conf.Locale);
      };

      SharedModule.ɵmod = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineNgModule"]({
        type: SharedModule
      });
      SharedModule.ɵinj = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineInjector"]({
        factory: function SharedModule_Factory(t) {
          return new (t || SharedModule)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵinject"](_ngx_translate_core__WEBPACK_IMPORTED_MODULE_2__["TranslateService"]), _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵinject"](codeshell_main__WEBPACK_IMPORTED_MODULE_3__["ServerConfigBase"]));
        },
        imports: [[_base_example_base_module__WEBPACK_IMPORTED_MODULE_1__["ExampleBaseModule"]], _base_example_base_module__WEBPACK_IMPORTED_MODULE_1__["ExampleBaseModule"]]
      });

      (function () {
        (typeof ngJitMode === "undefined" || ngJitMode) && _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵsetNgModuleScope"](SharedModule, {
          imports: [_base_example_base_module__WEBPACK_IMPORTED_MODULE_1__["ExampleBaseModule"]],
          exports: [_base_example_base_module__WEBPACK_IMPORTED_MODULE_1__["ExampleBaseModule"]]
        });
      })();
      /*@__PURE__*/


      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](SharedModule, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"],
          args: [{
            declarations: [],
            exports: [_base_example_base_module__WEBPACK_IMPORTED_MODULE_1__["ExampleBaseModule"]],
            imports: [_base_example_base_module__WEBPACK_IMPORTED_MODULE_1__["ExampleBaseModule"]],
            entryComponents: []
          }]
        }], function () {
          return [{
            type: _ngx_translate_core__WEBPACK_IMPORTED_MODULE_2__["TranslateService"]
          }, {
            type: codeshell_main__WEBPACK_IMPORTED_MODULE_3__["ServerConfigBase"]
          }];
        }, null);
      })();
      /***/

    },

    /***/
    "PwuP":
    /*!***************************************************!*\
      !*** ./src/core/codeshell/validators/isUnique.ts ***!
      \***************************************************/

    /*! exports provided: IsUnique */

    /***/
    function PwuP(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "IsUnique", function () {
        return IsUnique;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! @angular/forms */
      "3Pt+");

      var IsUnique = /*#__PURE__*/function () {
        function IsUnique(model) {
          _classCallCheck(this, IsUnique);

          this.model = model;
        }

        _createClass(IsUnique, [{
          key: "Change",
          value: function Change(arg) {
            var _this22 = this;

            if (this.service && this.column) {
              if (!this.model.value || this.model.value == "") {
                this.model.control.setErrors({
                  unique: null
                });
                this.model.control.updateValueAndValidity();
              } else {
                var sil = false;

                if (this.service) {
                  sil = this.service.Silent;
                  this.service.Silent = true;
                }

                this.model.control.setErrors({
                  unique: true
                });
                this.service.IsUnique(this.column, this.id, this.model.value).then(function (e) {
                  if (_this22.service) _this22.service.Silent = sil;

                  if (_this22.model) {
                    if (e) {
                      _this22.model.control.setErrors({
                        unique: null
                      });

                      _this22.model.control.updateValueAndValidity();
                    } else _this22.model.control.setErrors({
                      unique: true
                    });
                  }
                });
              }
            }
          }
        }]);

        return IsUnique;
      }();

      IsUnique.ɵfac = function IsUnique_Factory(t) {
        return new (t || IsUnique)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_forms__WEBPACK_IMPORTED_MODULE_1__["NgModel"]));
      };

      IsUnique.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({
        type: IsUnique,
        selectors: [["", "is-unique", "", "ngModel", ""]],
        hostBindings: function IsUnique_HostBindings(rf, ctx) {
          if (rf & 1) {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("blur", function IsUnique_blur_HostBindingHandler($event) {
              return ctx.Change($event);
            });
          }
        },
        inputs: {
          service: ["data-service", "service"],
          column: ["column-id", "column"],
          id: ["item-id", "id"]
        },
        exportAs: ["is-unique"]
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](IsUnique, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
          args: [{
            selector: "[is-unique][ngModel]",
            exportAs: "is-unique"
          }]
        }], function () {
          return [{
            type: _angular_forms__WEBPACK_IMPORTED_MODULE_1__["NgModel"]
          }];
        }, {
          service: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["data-service"]
          }],
          column: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["column-id"]
          }],
          id: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["item-id"]
          }],
          Change: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"],
            args: ["blur", ['$event']]
          }]
        });
      })();
      /***/

    },

    /***/
    "Q6pc":
    /*!****************************************************************!*\
      !*** ./src/core/codeshell/base-components/appComponentBase.ts ***!
      \****************************************************************/

    /*! exports provided: IAppComponent */

    /***/
    function Q6pc(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "IAppComponent", function () {
        return IAppComponent;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! @angular/router */
      "tyNb");
      /* harmony import */


      var _helpers__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! ../helpers */
      "GYDu");
      /* harmony import */


      var _shell__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! ../shell */
      "UoIT");
      /* harmony import */


      var _utilities_registry__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
      /*! ../utilities/registry */
      "ppms");
      /* harmony import */


      var _angular_common_http__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(
      /*! @angular/common/http */
      "tk/3");
      /* harmony import */


      var _angular_platform_browser__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(
      /*! @angular/platform-browser */
      "jhN1");

      var _c0 = ["topBar"];

      var IAppComponent = /*#__PURE__*/function () {
        function IAppComponent(inj, title) {
          var _this23 = this;

          _classCallCheck(this, IAppComponent);

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

          this.OnDeleteConfirm = function (e) {};

          this.OnDeleteCancel = function (e) {}; //protected Toaster: ToastrService;


          this.RedirectToLogin = true;
          _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Injector = inj;

          _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Start(this);

          _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Session.LogStatus.subscribe(function (d) {
            _this23.IsLoggedIn = d;

            _this23.OnLogStatusChanged(d);
          });

          _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Session.GetUserAsync().then(function (d) {
            _this23.IsLoggedIn = true;

            _this23.OnStartupSessionFound(d);
          })["catch"](function (e) {
            _this23.OnStartupNoSession(e);
          });

          this.SideBarStatus.subscribe(function (state) {
            setTimeout(function () {
              if (_this23.topBar) _this23.topBar.setSideBarState(state);
            }, 500);
          });

          _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Session.OnUserDataFailed.subscribe(function (error) {
            _this23.ShowPromptTranslate(error.message);
          }); //this.Toaster = inj.get(ToastrService);

        }

        _createClass(IAppComponent, [{
          key: "FactoryResolver",
          get: function get() {
            return _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Injector.get(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ComponentFactoryResolver"]);
          }
        }, {
          key: "Config",
          get: function get() {
            return null;
          }
        }, {
          key: "Router",
          get: function get() {
            return _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Injector.get(_angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"]);
          }
        }, {
          key: "ngOnInit",
          value: function ngOnInit() {// if (this.RestrictLang && this.Config.Locale != this.RestrictLang) {
            //     this.ChangeLangAsync(this.RestrictLang).then(e => location.reload());
            // }
          }
        }, {
          key: "GetMainUrl",
          value: function GetMainUrl() {
            return '/';
          }
        }, {
          key: "OnStartupSessionFound",
          value: function OnStartupSessionFound(dto) {}
        }, {
          key: "OnStartupNoSession",
          value: function OnStartupNoSession(response) {
            if (this.RedirectToLogin) this.Router.navigateByUrl("/Login");
          }
        }, {
          key: "OnLogStatusChanged",
          value: function OnLogStatusChanged(res) {}
        }, {
          key: "ChangeLangAsync",
          value: function ChangeLangAsync(code) {
            var cl = _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Injector.get(_angular_common_http__WEBPACK_IMPORTED_MODULE_5__["HttpClient"]);

            return cl.get("/Home/SetLocale/?lang=" + code).toPromise();
          }
        }, {
          key: "ShowPrompt",
          value: function ShowPrompt(data) {
            this.promptMessage = data;
            this.promptShow = true;
          }
        }, {
          key: "ShowPromptTranslate",
          value: function ShowPromptTranslate(data) {
            this.promptMessage = _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Message(data);
            this.promptShow = true;
          }
        }, {
          key: "ShowLoading",
          value: function ShowLoading() {
            this.ShowLoader = true;
            if (this._loaderTimeout) clearTimeout(this._loaderTimeout);
          }
        }, {
          key: "HideLoading",
          value: function HideLoading() {
            var _this24 = this;

            if (this._loaderTimeout) clearTimeout(this._loaderTimeout);
            this._loaderTimeout = setTimeout(function () {
              _this24.ShowLoader = false;
            }, 800);
          }
        }, {
          key: "GetDialogAs",
          value: function GetDialogAs(path) {
            if (path == null) return null;

            var e = _utilities_registry__WEBPACK_IMPORTED_MODULE_4__["Registry"].Get(path);

            if (e && this.ModalLoader) {
              var ref = this.ModalLoader.CreateComponent(e);
              this.ModalLoader.UseComponent(ref);
              return ref;
            }

            return null;
          }
        }, {
          key: "OnPromptOk",
          value: function OnPromptOk() {
            this.promptShow = false;
          }
        }, {
          key: "LogOut",
          value: function LogOut() {
            _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Session.EndSession();

            _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Injector.get(_angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"]).navigate(["/"]);
          }
        }, {
          key: "ShowDeleteConfirmLocal",
          value: function ShowDeleteConfirmLocal(onConfirm) {
            var _this25 = this;

            this.deleteDialogShow = true;

            this.OnDeleteConfirm = function (e) {
              _this25.deleteDialogShow = false;
              onConfirm();
            };
          }
        }, {
          key: "ShowDeleteConfirm",
          value: function ShowDeleteConfirm() {
            var _this26 = this;

            this.deleteDialogShow = true;
            this.confirmTitle = _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Word("Delete");
            this.confirmMessage = _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Message("delete_confirm_message");
            return new Promise(function (res, rej) {
              _this26.OnDeleteConfirm = function (e) {
                _this26.deleteDialogShow = false;
                res(true);
              };

              _this26.OnDeleteCancel = function (e) {
                _this26.deleteDialogShow = false;
                res(false);
              };
            });
          }
        }, {
          key: "Confirm",
          value: function Confirm(message) {
            var _this27 = this;

            var translate = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : true;
            var title = arguments.length > 2 ? arguments[2] : undefined;
            this.deleteDialogShow = true;
            return new Promise(function (res, rej) {
              if (title) _this27.confirmTitle = translate ? _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Word(title) : title;else _this27.confirmTitle = "";
              _this27.confirmMessage = translate ? _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Message(message) : message;

              _this27.OnDeleteConfirm = function (e) {
                _this27.deleteDialogShow = false;
                res();
              };

              _this27.OnDeleteCancel = function (e) {
                _this27.deleteDialogShow = false;
                rej();
              };
            });
          }
        }, {
          key: "Notify",
          value: function Notify(message, type, title) {
            type = type == undefined ? _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"].Success : type;

            var typ = _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"][type].toString();

            title = title ? title : typ;
            this.ShowMessage(type, message, _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Word(title));
          }
        }, {
          key: "NotifyTranslate",
          value: function NotifyTranslate(messageId, type, title) {
            type = type == undefined ? _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"].Success : type;

            var typ = _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"][type].toString();

            title = title ? title : typ;
            this.ShowMessage(type, _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Message(messageId), _shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Word(title));
          }
        }, {
          key: "SetTitle",
          value: function SetTitle(pageIdentifier) {
            var translate = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : true;
            if (translate) this.title.setTitle(_shell__WEBPACK_IMPORTED_MODULE_3__["Shell"].Page(pageIdentifier));else this.title.setTitle(pageIdentifier);
          }
        }, {
          key: "ClearModalLoader",
          value: function ClearModalLoader() {
            if (this.ModalLoader) this.ModalLoader.Container.clear();
          }
        }, {
          key: "ShowMessage",
          value: function ShowMessage(type, e, title) {// switch (type) {
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
        }]);

        return IAppComponent;
      }();

      IAppComponent.ɵfac = function IAppComponent_Factory(t) {
        return new (t || IAppComponent)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injector"]), _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_platform_browser__WEBPACK_IMPORTED_MODULE_6__["Title"]));
      };

      IAppComponent.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineComponent"]({
        type: IAppComponent,
        selectors: [["ng-component"]],
        viewQuery: function IAppComponent_Query(rf, ctx) {
          if (rf & 1) {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵviewQuery"](_c0, true);
          }

          if (rf & 2) {
            var _t;

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵqueryRefresh"](_t = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵloadQuery"]()) && (ctx.topBar = _t.first);
          }
        },
        decls: 0,
        vars: 0,
        template: function IAppComponent_Template(rf, ctx) {},
        encapsulation: 2
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](IAppComponent, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"],
          args: [{
            template: ''
          }]
        }], function () {
          return [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Injector"]
          }, {
            type: _angular_platform_browser__WEBPACK_IMPORTED_MODULE_6__["Title"]
          }];
        }, {
          topBar: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ViewChild"],
            args: ["topBar"]
          }]
        });
      })();
      /***/

    },

    /***/
    "RnJ/":
    /*!**********************************************!*\
      !*** ./src/core/codeshell/services/index.ts ***!
      \**********************************************/

    /*! exports provided: ListSelectionService, ListSource, SectionedFormService, Stored, TableService */

    /***/
    function RnJ(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony import */


      var _listSelectionService__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! ./listSelectionService */
      "0Wec");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "ListSelectionService", function () {
        return _listSelectionService__WEBPACK_IMPORTED_MODULE_0__["ListSelectionService"];
      });
      /* harmony import */


      var _listSource__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ./listSource */
      "N0SU");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "ListSource", function () {
        return _listSource__WEBPACK_IMPORTED_MODULE_1__["ListSource"];
      });
      /* harmony import */


      var _sectionedFormService__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! ./sectionedFormService */
      "niJb");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "SectionedFormService", function () {
        return _sectionedFormService__WEBPACK_IMPORTED_MODULE_2__["SectionedFormService"];
      });
      /* harmony import */


      var _stored__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! ./stored */
      "mHXC");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "Stored", function () {
        return _stored__WEBPACK_IMPORTED_MODULE_3__["Stored"];
      });
      /* harmony import */


      var _tableServices__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
      /*! ./tableServices */
      "NnUC");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "TableService", function () {
        return _tableServices__WEBPACK_IMPORTED_MODULE_4__["TableService"];
      });
      /***/

    },

    /***/
    "RnhZ":
    /*!**************************************************!*\
      !*** ./node_modules/moment/locale sync ^\.\/.*$ ***!
      \**************************************************/

    /*! no static exports found */

    /***/
    function RnhZ(module, exports, __webpack_require__) {
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
        if (!__webpack_require__.o(map, req)) {
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
      /***/
    },

    /***/
    "Rux6":
    /*!*************************************************************!*\
      !*** ./src/core/codeshell/components/paginate.component.ts ***!
      \*************************************************************/

    /*! exports provided: Paginate */

    /***/
    function Rux6(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "Paginate", function () {
        return Paginate;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var _angular_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! @angular/common */
      "ofXK");

      function Paginate_li_10_Template(rf, ctx) {
        if (rf & 1) {
          var _r3 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵgetCurrentView"]();

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "li", 10);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](1, "a", 11);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function Paginate_li_10_Template_a_click_1_listener() {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r3);

            var p_r1 = ctx.$implicit;

            var ctx_r2 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();

            return ctx_r2.SelectPage(p_r1);
          });

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](2);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        }

        if (rf & 2) {
          var p_r1 = ctx.$implicit;

          var ctx_r0 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngClass", ctx_r0.Current == p_r1.id ? "active" : null);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngClass", ctx_r0.Current == p_r1.id ? "disabled" : null);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate"](p_r1.name);
        }
      }

      var Paginate = /*#__PURE__*/function () {
        function Paginate() {
          _classCallCheck(this, Paginate);

          this.totalPages = 0;
          this._current = 0;
          this._max = 0;
          this.Pages = [];
          this.Showing = 10;
          this.Total = 0;
          this.PageChange = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
          this.currentPageChange = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        }

        _createClass(Paginate, [{
          key: "Current",
          get: function get() {
            return this._current;
          },
          set: function set(val) {
            this._current = val;
            this.currentPageChange.emit(this._current);
          }
        }, {
          key: "ngOnInit",
          value: function ngOnInit() {
            if (this.Showing != 0) {
              this.SetPages();
            }
          }
        }, {
          key: "ngOnChanges",
          value: function ngOnChanges() {
            this.SetPages();
          }
        }, {
          key: "SelectPage",
          value: function SelectPage(p) {
            if (this.Current == p.id) return;
            this.Current = p.id;

            if (this.PageChange != undefined) {
              this.PageChange.emit(this.Current); //this.CurrentOut.emit(this.Current);
            }
          }
        }, {
          key: "Prev",
          value: function Prev() {
            if (this.Current > 0) {
              this.Current -= 1;
              this.PageChange.emit(this.Current); //this.CurrentOut.emit(this.Current);
            }
          }
        }, {
          key: "Next",
          value: function Next() {
            if (this.Current < this.totalPages - 1) {
              this._current += 1;
              this.PageChange.emit(this.Current); //this.CurrentOut.emit(this.Current);
            }
          }
        }, {
          key: "SetPages",
          value: function SetPages() {
            ////debugger;
            this.Pages = [];
            if (this.Showing == 0 || this.Total == 0) return;
            var cnt = this.Total / this.Showing;
            var num = Math.floor(cnt);
            if (num < cnt) num += 1;
            var reset = this.Current > num - 1;
            this.totalPages = num;
            if (reset) this.SelectPage({
              id: 0,
              name: "1"
            });
            this.WritePages();
          }
        }, {
          key: "WritePages",
          value: function WritePages() {
            if (this.MaxPages) {
              ////debugger;
              var s = this.MaxPages / 2;
              var sInt = Math.floor(s);
              var start = 0,
                  end = this.MaxPages;
              var noOfPages = this.MaxPages > this.totalPages ? this.totalPages : this.MaxPages;
              start = this.Current - sInt;
              start = start < 0 ? 0 : start;
              end = start + noOfPages;

              if (this.totalPages - 1 < end) {
                end = this.totalPages - 1;
                start = end - noOfPages;
                start = start < 0 ? 0 : start;
              }

              for (var i = start; i <= end; i++) {
                this.Pages.push({
                  id: i,
                  name: (i + 1).toString()
                });
              }
            } else {
              for (var _i2 = 0; _i2 < this.totalPages; _i2++) {
                this.Pages.push({
                  id: _i2,
                  name: (_i2 + 1).toString()
                });
              }
            }
          }
        }]);

        return Paginate;
      }();

      Paginate.ɵfac = function Paginate_Factory(t) {
        return new (t || Paginate)();
      };

      Paginate.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineComponent"]({
        type: Paginate,
        selectors: [["paginate"]],
        inputs: {
          Showing: ["showing", "Showing"],
          Total: ["total-count", "Total"],
          Current: ["currentPage", "Current"],
          MaxPages: ["max-pages", "MaxPages"]
        },
        outputs: {
          PageChange: "pageChange",
          currentPageChange: "currentPageChange"
        },
        features: [_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵNgOnChangesFeature"]],
        decls: 17,
        vars: 2,
        consts: [[1, "row", 3, "hidden"], [1, "col-sm-12", 2, "text-align", "center"], ["aria-label", "Page navigation example"], [1, "pagination"], [1, "page-item"], ["aria-label", "Previous", 1, "page-link", 3, "click"], ["aria-hidden", "true"], [1, "sr-only"], ["class", "page-item", 3, "ngClass", 4, "ngFor", "ngForOf"], ["aria-label", "Next", 1, "page-link", 3, "click"], [1, "page-item", 3, "ngClass"], [1, "page-link", 3, "ngClass", "click"]],
        template: function Paginate_Template(rf, ctx) {
          if (rf & 1) {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "div", 0);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](1, "div", 1);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](2, "nav", 2);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](3, "ul", 3);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](4, "li", 4);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](5, "a", 5);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function Paginate_Template_a_click_5_listener() {
              return ctx.Prev();
            });

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](6, "span", 6);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](7, "\xAB");

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](8, "span", 7);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](9, "Previous");

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](10, Paginate_li_10_Template, 3, 3, "li", 8);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](11, "li", 4);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](12, "a", 9);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function Paginate_Template_a_click_12_listener() {
              return ctx.Next();
            });

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](13, "span", 6);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](14, "\xBB");

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
          }

          if (rf & 2) {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("hidden", ctx.Pages.length < 2);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](10);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngForOf", ctx.Pages);
          }
        },
        directives: [_angular_common__WEBPACK_IMPORTED_MODULE_1__["NgForOf"], _angular_common__WEBPACK_IMPORTED_MODULE_1__["NgClass"]],
        encapsulation: 2
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](Paginate, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"],
          args: [{
            selector: "paginate",
            templateUrl: "./paginate.component.html"
          }]
        }], null, {
          Showing: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["showing"]
          }],
          Total: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["total-count"]
          }],
          Current: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["currentPage"]
          }],
          MaxPages: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["max-pages"]
          }],
          PageChange: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"],
            args: ["pageChange"]
          }],
          currentPageChange: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"],
            args: ["currentPageChange"]
          }]
        });
      })();

      var Page = function Page(data) {
        _classCallCheck(this, Page);

        this.id = 0;
        this.name = "";
        Object.assign(this, data);
      };
      /***/

    },

    /***/
    "S+5U":
    /*!******************************************************************!*\
      !*** ./src/core/codeshell/directives/fix-date-time.directive.ts ***!
      \******************************************************************/

    /*! exports provided: FixDateTime */

    /***/
    function S5U(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "FixDateTime", function () {
        return FixDateTime;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var _utilities_utils__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ../utilities/utils */
      "VXkE");
      /* harmony import */


      var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! @angular/forms */
      "3Pt+");
      /* harmony import */


      var angular2_datetimepicker__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! angular2-datetimepicker */
      "1XXD");

      var FixDateTime = /*#__PURE__*/function () {
        function FixDateTime(model, el) {
          var _this28 = this;

          _classCallCheck(this, FixDateTime);

          this.model = model;
          this.el = el;
          this.el.date = new Date();
          this.el.onDateSelect.subscribe(function (ev) {
            _this28.model.viewToModelUpdate(Object(_utilities_utils__WEBPACK_IMPORTED_MODULE_1__["Date_Get"])(ev));
          });
        }

        _createClass(FixDateTime, [{
          key: "ngOnInit",
          value: function ngOnInit() {
            this.el.settings.bigBanner = true;
            this.el.settings.timePicker = true;
            this.el.settings.format = 'dd-MM-yyyy hh:mm a';
            this.el.settings.defaultOpen = false;
          }
        }]);

        return FixDateTime;
      }();

      FixDateTime.ɵfac = function FixDateTime_Factory(t) {
        return new (t || FixDateTime)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_forms__WEBPACK_IMPORTED_MODULE_2__["NgModel"]), _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](angular2_datetimepicker__WEBPACK_IMPORTED_MODULE_3__["DatePicker"]));
      };

      FixDateTime.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({
        type: FixDateTime,
        selectors: [["angular2-date-picker", "ngModel", "", "fix-date-time", ""]],
        exportAs: ["fix-date-time"]
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](FixDateTime, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
          args: [{
            selector: "angular2-date-picker[ngModel][fix-date-time]",
            exportAs: "fix-date-time"
          }]
        }], function () {
          return [{
            type: _angular_forms__WEBPACK_IMPORTED_MODULE_2__["NgModel"]
          }, {
            type: angular2_datetimepicker__WEBPACK_IMPORTED_MODULE_3__["DatePicker"]
          }];
        }, null);
      })();
      /***/

    },

    /***/
    "Se+h":
    /*!********************************************************!*\
      !*** ./src/core/codeshell/helpers/editablePairsDTO.ts ***!
      \********************************************************/

    /*! exports provided: EditablePairsDTO */

    /***/
    function SeH(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "EditablePairsDTO", function () {
        return EditablePairsDTO;
      });
      /* harmony import */


      var _listItem__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! ./listItem */
      "JLZs");

      var EditablePairsDTO = /*#__PURE__*/function (_listItem__WEBPACK_IM2) {
        _inherits(EditablePairsDTO, _listItem__WEBPACK_IM2);

        var _super10 = _createSuper(EditablePairsDTO);

        function EditablePairsDTO() {
          var _this29;

          _classCallCheck(this, EditablePairsDTO);

          _this29 = _super10.apply(this, arguments);
          _this29.data = {};
          return _this29;
        }

        return EditablePairsDTO;
      }(_listItem__WEBPACK_IMPORTED_MODULE_0__["ListItem"]);
      /***/

    },

    /***/
    "Sgtl":
    /*!*****************************************************************!*\
      !*** ./src/core/codeshell/localization/ngxTransationService.ts ***!
      \*****************************************************************/

    /*! exports provided: NgxTranslationService */

    /***/
    function Sgtl(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "NgxTranslationService", function () {
        return NgxTranslationService;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL"); //import { TranslateService } from "@ngx-translate/core";


      var NgxTranslationService = /*#__PURE__*/function () {
        function NgxTranslationService() {
          _classCallCheck(this, NgxTranslationService);
        }

        _createClass(NgxTranslationService, [{
          key: "setDefaultLang",
          value: //constructor(private service: TranslateService) { }
          function setDefaultLang(loc) {//this.service.setDefaultLang(loc);
          }
        }, {
          key: "use",
          value: function use(loc) {//this.service.use(loc);
          }
        }]);

        return NgxTranslationService;
      }();

      NgxTranslationService.ɵfac = function NgxTranslationService_Factory(t) {
        return new (t || NgxTranslationService)();
      };

      NgxTranslationService.ɵprov = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineInjectable"]({
        token: NgxTranslationService,
        factory: NgxTranslationService.ɵfac
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](NgxTranslationService, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"]
        }], null, null);
      })();
      /***/

    },

    /***/
    "Sy1n":
    /*!**********************************!*\
      !*** ./src/app/app.component.ts ***!
      \**********************************/

    /*! exports provided: AppComponent */

    /***/
    function Sy1n(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "AppComponent", function () {
        return AppComponent;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var codeshell_main__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! codeshell/main */
      "7OZ3");
      /* harmony import */


      var _base_app_component_base_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! @base/app-component-base.component */
      "DTUp");
      /* harmony import */


      var _angular_platform_browser__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! @angular/platform-browser */
      "jhN1");
      /* harmony import */


      var _core_base_main_top_bar_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
      /*! ../core/base/main/top-bar.component */
      "WNdI");
      /* harmony import */


      var _angular_common__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(
      /*! @angular/common */
      "ofXK");
      /* harmony import */


      var _angular_router__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(
      /*! @angular/router */
      "tyNb");
      /* harmony import */


      var _core_base_main_navigation_side_bar_component__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(
      /*! ../core/base/main/navigation-side-bar.component */
      "a3Ap");

      function AppComponent_div_6_Template(rf, ctx) {
        if (rf & 1) {
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "div", 10);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](1, "navigation-side-bar");

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        }
      }

      function AppComponent_ng_template_10_Template(rf, ctx) {}

      var AppComponent = /*#__PURE__*/function (_base_app_component_b) {
        _inherits(AppComponent, _base_app_component_b);

        var _super11 = _createSuper(AppComponent);

        function AppComponent(inj, trans) {
          var _this30;

          _classCallCheck(this, AppComponent);

          _this30 = _super11.call(this, inj, trans);
          codeshell_main__WEBPACK_IMPORTED_MODULE_1__["Shell"].Main = _assertThisInitialized(_this30);
          return _this30;
        }

        return AppComponent;
      }(_base_app_component_base_component__WEBPACK_IMPORTED_MODULE_2__["AppComponentBase"]);

      AppComponent.ɵfac = function AppComponent_Factory(t) {
        return new (t || AppComponent)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injector"]), _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_platform_browser__WEBPACK_IMPORTED_MODULE_3__["Title"]));
      };

      AppComponent.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineComponent"]({
        type: AppComponent,
        selectors: [["app"]],
        features: [_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵInheritDefinitionFeature"]],
        decls: 15,
        vars: 5,
        consts: [[1, "loader-overlay"], [1, "loader"], [1, "wrapper"], ["topBar", ""], ["class", "wrapper-side", 4, "ngIf"], [1, "wrapper-content", 3, "ngClass"], [3, "ngClass", "dir"], ["values", "", 2, "display", "none"], ["lookupOptionsContainer", ""], ["viewParamsContainer", ""], [1, "wrapper-side"]],
        template: function AppComponent_Template(rf, ctx) {
          if (rf & 1) {
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
          }

          if (rf & 2) {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("@loader", ctx.ShowLoader ? "shown" : "hidden");

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](6);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", ctx.IsLoggedIn && ctx.ShowNav);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngClass", !ctx.IsLoggedIn || !ctx.ShowNav ? "expanded" : null);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngClass", ctx.Config.Locale == "ar" ? "ui-rtl" : null)("dir", ctx.Config.Locale == "ar" ? "rtl" : null);
          }
        },
        directives: [_core_base_main_top_bar_component__WEBPACK_IMPORTED_MODULE_4__["TopBar"], _angular_common__WEBPACK_IMPORTED_MODULE_5__["NgIf"], _angular_common__WEBPACK_IMPORTED_MODULE_5__["NgClass"], _angular_router__WEBPACK_IMPORTED_MODULE_6__["RouterOutlet"], _core_base_main_navigation_side_bar_component__WEBPACK_IMPORTED_MODULE_7__["NavigationSideBar"]],
        encapsulation: 2
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](AppComponent, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"],
          args: [{
            selector: 'app',
            templateUrl: './app.component.html'
          }]
        }], function () {
          return [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Injector"]
          }, {
            type: _angular_platform_browser__WEBPACK_IMPORTED_MODULE_3__["Title"]
          }];
        }, null);
      })();
      /***/

    },

    /***/
    "U6Sh":
    /*!**********************************************!*\
      !*** ./src/core/codeshell/security/index.ts ***!
      \**********************************************/

    /*! exports provided: AccountServiceBase, AuthFilter, ResourceActions, DomainDataProvider, DomainData, RouteData, UserDTO, LoginResult, TokenData, Permission, AuthorizationError, AuthorizationServiceBase, SessionManager, ModuleItem, FunctionItem, SessionTokenData, TokenStorage */

    /***/
    function U6Sh(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony import */


      var _accountServiceBase__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! ./accountServiceBase */
      "wNr3");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "AccountServiceBase", function () {
        return _accountServiceBase__WEBPACK_IMPORTED_MODULE_0__["AccountServiceBase"];
      });
      /* harmony import */


      var _authFilter__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ./authFilter */
      "YTpK");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "AuthFilter", function () {
        return _authFilter__WEBPACK_IMPORTED_MODULE_1__["AuthFilter"];
      });
      /* harmony import */


      var _models__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! ./models */
      "gFuU");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "ResourceActions", function () {
        return _models__WEBPACK_IMPORTED_MODULE_2__["ResourceActions"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "DomainDataProvider", function () {
        return _models__WEBPACK_IMPORTED_MODULE_2__["DomainDataProvider"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "DomainData", function () {
        return _models__WEBPACK_IMPORTED_MODULE_2__["DomainData"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "RouteData", function () {
        return _models__WEBPACK_IMPORTED_MODULE_2__["RouteData"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "UserDTO", function () {
        return _models__WEBPACK_IMPORTED_MODULE_2__["UserDTO"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "LoginResult", function () {
        return _models__WEBPACK_IMPORTED_MODULE_2__["LoginResult"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "TokenData", function () {
        return _models__WEBPACK_IMPORTED_MODULE_2__["TokenData"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "Permission", function () {
        return _models__WEBPACK_IMPORTED_MODULE_2__["Permission"];
      });
      /* harmony import */


      var _authorizationServiceBase__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! ./authorizationServiceBase */
      "rp8W");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "AuthorizationError", function () {
        return _authorizationServiceBase__WEBPACK_IMPORTED_MODULE_3__["AuthorizationError"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "AuthorizationServiceBase", function () {
        return _authorizationServiceBase__WEBPACK_IMPORTED_MODULE_3__["AuthorizationServiceBase"];
      });
      /* harmony import */


      var _sessionManager__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
      /*! ./sessionManager */
      "M6Pn");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "SessionManager", function () {
        return _sessionManager__WEBPACK_IMPORTED_MODULE_4__["SessionManager"];
      });
      /* harmony import */


      var _navs__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(
      /*! ./navs */
      "1Jgf");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "ModuleItem", function () {
        return _navs__WEBPACK_IMPORTED_MODULE_5__["ModuleItem"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "FunctionItem", function () {
        return _navs__WEBPACK_IMPORTED_MODULE_5__["FunctionItem"];
      });
      /* harmony import */


      var _sessionTokenData__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(
      /*! ./sessionTokenData */
      "9uFN");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "SessionTokenData", function () {
        return _sessionTokenData__WEBPACK_IMPORTED_MODULE_6__["SessionTokenData"];
      });
      /* harmony import */


      var _tokenStorage__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(
      /*! ./tokenStorage */
      "yvMo");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "TokenStorage", function () {
        return _tokenStorage__WEBPACK_IMPORTED_MODULE_7__["TokenStorage"];
      });
      /***/

    },

    /***/
    "UoIT":
    /*!*************************************!*\
      !*** ./src/core/codeshell/shell.ts ***!
      \*************************************/

    /*! exports provided: Shell */

    /***/
    function UoIT(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "Shell", function () {
        return Shell;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var _security_sessionManager__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ./security/sessionManager */
      "M6Pn");
      /* harmony import */


      var _ngx_translate_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! @ngx-translate/core */
      "sYmb");

      var Shell = /*#__PURE__*/function () {
        function Shell() {
          _classCallCheck(this, Shell);
        }

        _createClass(Shell, null, [{
          key: "Translator",
          get: function get() {
            if (Shell._translate == null) Shell._translate = Shell.Injector.get(_ngx_translate_core__WEBPACK_IMPORTED_MODULE_2__["TranslateService"]);
            return Shell._translate;
          }
        }, {
          key: "Session",
          get: function get() {
            return this._session;
          }
        }, {
          key: "Start",
          value: function Start(comp) {
            this.Main = comp;
            this._session = new _security_sessionManager__WEBPACK_IMPORTED_MODULE_1__["SessionManager"]();

            this._session.GetDeviceId();

            this._session.CheckToken();
          }
        }, {
          key: "MainAs",
          value: function MainAs() {
            return Shell.Main;
          }
        }, {
          key: "Word",
          value: function Word(text) {
            var _Shell$Translator;

            for (var _len2 = arguments.length, params = new Array(_len2 > 1 ? _len2 - 1 : 0), _key2 = 1; _key2 < _len2; _key2++) {
              params[_key2 - 1] = arguments[_key2];
            }

            return (_Shell$Translator = Shell.Translator).instant.apply(_Shell$Translator, ['Words.' + text].concat(params));
          }
        }, {
          key: "Message",
          value: function Message(text) {
            var _Shell$Translator2;

            for (var _len3 = arguments.length, params = new Array(_len3 > 1 ? _len3 - 1 : 0), _key3 = 1; _key3 < _len3; _key3++) {
              params[_key3 - 1] = arguments[_key3];
            }

            return (_Shell$Translator2 = Shell.Translator).instant.apply(_Shell$Translator2, [text].concat(params));
          }
        }, {
          key: "Column",
          value: function Column(text) {
            return Shell.Translator.instant(text);
          }
        }, {
          key: "Page",
          value: function Page(text) {
            return Shell.Translator.instant(text);
          }
        }, {
          key: "Translate",
          value: function Translate() {
            for (var _len4 = arguments.length, params = new Array(_len4), _key4 = 0; _key4 < _len4; _key4++) {
              params[_key4] = arguments[_key4];
            }

            for (var i in params) {
              params[i] = Shell.Translator.instant(params[i]);
            }

            return params;
          }
        }, {
          key: "TranslateIfNeeded",
          value: function TranslateIfNeeded(text) {
            if (!text) return "";
            return Shell.Translator.instant(text);
          }
        }]);

        return Shell;
      }();

      Shell.ViewLoaded = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
      /***/
    },

    /***/
    "Ur/x":
    /*!***********************************************************************!*\
      !*** ./src/core/codeshell/base-components/dto-edit-component-base.ts ***!
      \***********************************************************************/

    /*! exports provided: DTOEditComponentBase */

    /***/
    function UrX(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "DTOEditComponentBase", function () {
        return DTOEditComponentBase;
      });
      /* harmony import */


      var _editComponentBase__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! ./editComponentBase */
      "G778");
      /* harmony import */


      var _helpers__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ../helpers */
      "GYDu");
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");

      var DTOEditComponentBase = /*#__PURE__*/function (_editComponentBase__W) {
        _inherits(DTOEditComponentBase, _editComponentBase__W);

        var _super12 = _createSuper(DTOEditComponentBase);

        function DTOEditComponentBase() {
          var _this31;

          _classCallCheck(this, DTOEditComponentBase);

          _this31 = _super12.apply(this, arguments);
          _this31.model = {};
          return _this31;
        }

        _createClass(DTOEditComponentBase, [{
          key: "ModelId",
          get: function get() {
            return this.model.entity.id;
          },
          set: function set(val) {
            this.model.entity.id = val;
          }
        }, {
          key: "DefaultModel",
          value: function DefaultModel() {
            return new _helpers__WEBPACK_IMPORTED_MODULE_1__["DTO"]();
          }
        }, {
          key: "SubmitNewAsync",
          value: function SubmitNewAsync() {
            return this.Service.Save("post", this.model.entity);
          }
        }, {
          key: "SubmitUpdateAsync",
          value: function SubmitUpdateAsync() {
            return this.Service.Update("put", this.model.entity);
          }
        }]);

        return DTOEditComponentBase;
      }(_editComponentBase__WEBPACK_IMPORTED_MODULE_0__["EditComponentBase"]);

      DTOEditComponentBase.ɵfac = function DTOEditComponentBase_Factory(t) {
        return ɵDTOEditComponentBase_BaseFactory(t || DTOEditComponentBase);
      };

      DTOEditComponentBase.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵdefineComponent"]({
        type: DTOEditComponentBase,
        selectors: [["ng-component"]],
        features: [_angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵInheritDefinitionFeature"]],
        decls: 0,
        vars: 0,
        template: function DTOEditComponentBase_Template(rf, ctx) {},
        encapsulation: 2
      });

      var ɵDTOEditComponentBase_BaseFactory = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵgetInheritedFactory"](DTOEditComponentBase);
      /*@__PURE__*/


      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵsetClassMetadata"](DTOEditComponentBase, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_2__["Component"],
          args: [{
            template: ''
          }]
        }], null, null);
      })();
      /***/

    },

    /***/
    "VXkE":
    /*!***********************************************!*\
      !*** ./src/core/codeshell/utilities/utils.ts ***!
      \***********************************************/

    /*! exports provided: absUrl, List_RemoveItem, List_RunRecursively_Nodes, List_OrderBy, List_OrderByDesc, List_FindIdRecusive, List_RunRecursively, List_RunRecursively_GEN, List_RunRecursivelyUp_GEN, List_ToggleItem, String_GetBeforeLast, String_GetAfterLast, Date_Get, Date_Elapsed, KeyValuePair, Utils */

    /***/
    function VXkE(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "absUrl", function () {
        return absUrl;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "List_RemoveItem", function () {
        return List_RemoveItem;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "List_RunRecursively_Nodes", function () {
        return List_RunRecursively_Nodes;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "List_OrderBy", function () {
        return List_OrderBy;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "List_OrderByDesc", function () {
        return List_OrderByDesc;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "List_FindIdRecusive", function () {
        return List_FindIdRecusive;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "List_RunRecursively", function () {
        return List_RunRecursively;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "List_RunRecursively_GEN", function () {
        return List_RunRecursively_GEN;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "List_RunRecursivelyUp_GEN", function () {
        return List_RunRecursivelyUp_GEN;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "List_ToggleItem", function () {
        return List_ToggleItem;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "String_GetBeforeLast", function () {
        return String_GetBeforeLast;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "String_GetAfterLast", function () {
        return String_GetAfterLast;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "Date_Get", function () {
        return Date_Get;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "Date_Elapsed", function () {
        return Date_Elapsed;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "KeyValuePair", function () {
        return KeyValuePair;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "Utils", function () {
        return Utils;
      });
      /* harmony import */


      var moment__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! moment */
      "wd/R");
      /* harmony import */


      var moment__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(moment__WEBPACK_IMPORTED_MODULE_0__);
      /* harmony import */


      var _shell__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ../shell */
      "UoIT");
      /* harmony import */


      var _helpers__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! ../helpers */
      "GYDu");

      function absUrl(url) {
        if (url) {
          if (url.length > 0) {
            if (url[0] != "/") url = "/" + url;
          }

          return url;
        }

        return "/";
      }

      function List_RemoveItem(lst, item) {
        var ind = lst.indexOf(item);
        if (ind > -1) lst.splice(ind, 1);
      }

      function List_RunRecursively_Nodes(lst, func) {
        var _iterator7 = _createForOfIteratorHelper(lst),
            _step7;

        try {
          for (_iterator7.s(); !(_step7 = _iterator7.n()).done;) {
            var mod = _step7.value;
            func(mod);
            if (mod.children && mod.children.length > 0) List_RunRecursively_Nodes(mod.children, func);
          }
        } catch (err) {
          _iterator7.e(err);
        } finally {
          _iterator7.f();
        }
      }

      function List_OrderBy(arr, del) {
        arr.sort(function (a, b) {
          var v1 = del(a);
          var v2 = del(b);
          if (v1 > v2) return 1;else if (v1 < v2) return -1;else return 0;
        });
        return arr;
      }

      function List_OrderByDesc(arr, del) {
        arr.sort(function (a, b) {
          var v1 = del(a);
          var v2 = del(b);
          if (v1 > v2) return -1;else if (v1 < v2) return 1;else return 0;
        });
        return arr;
      }

      function List_FindIdRecusive(lst, id) {
        var _iterator8 = _createForOfIteratorHelper(lst),
            _step8;

        try {
          for (_iterator8.s(); !(_step8 = _iterator8.n()).done;) {
            var mod = _step8.value;

            if (mod.id == id) {
              return mod;
            } else if (mod.children && mod.children.length > 0) {
              var m = List_FindIdRecusive(mod.children, id);
              if (m) return m;
            }
          }
        } catch (err) {
          _iterator8.e(err);
        } finally {
          _iterator8.f();
        }

        return null;
      }

      function List_RunRecursively(lst, func) {
        var _iterator9 = _createForOfIteratorHelper(lst),
            _step9;

        try {
          for (_iterator9.s(); !(_step9 = _iterator9.n()).done;) {
            var mod = _step9.value;
            func(mod);
            if (mod.children && mod.children.length > 0) List_RunRecursively(mod.children, func);
          }
        } catch (err) {
          _iterator9.e(err);
        } finally {
          _iterator9.f();
        }
      }

      function List_RunRecursively_GEN(lst, func) {
        var _iterator10 = _createForOfIteratorHelper(lst),
            _step10;

        try {
          for (_iterator10.s(); !(_step10 = _iterator10.n()).done;) {
            var mod = _step10.value;
            func(mod);
            if (mod.children && mod.children.length > 0) List_RunRecursively_GEN(mod.children, func);
          }
        } catch (err) {
          _iterator10.e(err);
        } finally {
          _iterator10.f();
        }
      }

      function List_RunRecursivelyUp_GEN(mod, func) {
        var first = arguments.length > 2 && arguments[2] !== undefined ? arguments[2] : true;
        if (!first) func(mod);
        var Par = mod.parent;
        if (Par) List_RunRecursivelyUp_GEN(Par, func, false);
      }

      function List_ToggleItem(lst, item) {
        var ind = lst.indexOf(item);
        if (ind > -1) lst.splice(ind, 1);else lst.push(item);
      }

      function String_GetBeforeLast(data, del) {
        var x = data.lastIndexOf(del);
        return data.substr(0, x);
      }

      function String_GetAfterLast(data, del) {
        var x = data.lastIndexOf(del);
        if (x == -1) return data;
        return data.substr(x + del.length);
      }

      function Date_Get(ev) {
        var inp = ev;
        if (typeof ev == "string") inp = new Date(Date.parse(ev));
        return inp;
      }

      function Date_Elapsed(start, end) {
        if (end < start) return null;
        var endMoment = moment__WEBPACK_IMPORTED_MODULE_0__(end);
        var startMoment = moment__WEBPACK_IMPORTED_MODULE_0__(start);
        return moment__WEBPACK_IMPORTED_MODULE_0__["duration"](endMoment.diff(startMoment));
      }

      var KeyValuePair = function KeyValuePair() {
        _classCallCheck(this, KeyValuePair);

        this.key = "";
        this.value = 0;
      };

      var Utils = /*#__PURE__*/function () {
        function Utils() {
          _classCallCheck(this, Utils);
        }

        _createClass(Utils, null, [{
          key: "GetQueryString",
          value: function GetQueryString(obj) {
            var str = [];

            for (var p in obj) {
              if (obj.hasOwnProperty(p)) {
                str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
              }
            }

            return str.join("&");
          }
        }, {
          key: "GetIdString",
          value: function GetIdString() {
            var sec = new Date().getTime();
            var gen = "";

            if (sec == this.lastSec) {
              this.i++;
            } else {
              this.i = 0;
            }

            gen = sec.toString() + this.i;
            this.lastSec = sec;
            return gen;
          }
        }, {
          key: "HandleResult",
          value: function HandleResult(res) {
            var note = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : false;
            var deleting = arguments.length > 2 && arguments[2] !== undefined ? arguments[2] : false;
            console.error("MESSAGE : " + res.message);
            console.error("EXCEPTION : " + res.exceptionMessage);
            if (res.stackTrace) console.error(res.stackTrace.join("\r\n"));
            if (res.data) console.error("INNER : ", res.data);

            if (note) {
              if (deleting) {
                var del = Object.assign(new _helpers__WEBPACK_IMPORTED_MODULE_2__["DeleteResult"](), res);
                res = del;

                if (del.tableName) {
                  var message = _shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Message("delete_error_message", _shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Word(del.tableName));

                  _shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Main.Notify(message, _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"].Error);
                } else {
                  _shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Main.NotifyTranslate(res.message ? res.message : "error_message", _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"].Error);
                }
              } else {
                _shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Main.NotifyTranslate(res.message ? res.message : "error_message", _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"].Error);
              }
            }
          }
        }, {
          key: "HandleError",
          value: function HandleError(e) {
            var note = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : false;
            var deleting = arguments.length > 2 && arguments[2] !== undefined ? arguments[2] : false;
            var res = new _helpers__WEBPACK_IMPORTED_MODULE_2__["SubmitResult"]();
            res.code = 1;

            if (e.error && e.error.code) {
              res = Object.assign(new _helpers__WEBPACK_IMPORTED_MODULE_2__["SubmitResult"](), e.error);
              Utils.HandleResult(res, note, deleting);
            } else {
              switch (e.status) {
                case 404:
                  if (note) _shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Main.NotifyTranslate("operation_unavailable", _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"].Error);
                  console.error("[" + e.url + "] : " + e.statusText + " >> " + e.message);
                  break;

                case 401:
                  if (note) _shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Main.NotifyTranslate("unauthorized_operation", _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"].Error);
                  console.error("[" + e.url + "] : " + e.statusText + " >> " + e.message);
                  break;

                default:
                  if (note) _shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Main.NotifyTranslate("unexpected_error", _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"].Error);
                  console.error("[" + e.url + "] : " + e.statusText + " >> " + e.message, _helpers__WEBPACK_IMPORTED_MODULE_2__["NoteType"].Error);
                  break;
              }
            }

            return res;
          }
        }, {
          key: "GetId",
          value: function GetId() {
            var s = [];
            return Number.parseInt(this.GetIdString());
          }
        }, {
          key: "Combine",
          value: function Combine() {
            var endsWithSlash = new RegExp("\/$");
            var startsWithSlash = new RegExp("^\/");
            var _final = "";

            for (var i = 0; i < arguments.length; i++) {
              var st = i < 0 || arguments.length <= i ? undefined : arguments[i];
              if (startsWithSlash.test(st)) st = st.substr(1, st.length);

              if (i == arguments.length - 1) {
                if (endsWithSlash.test(st)) st = st.substr(0, st.length - 1);
              } else {
                if (!endsWithSlash.test(st)) st = st + "/";
              }

              _final += st;
            }

            return _final;
          }
        }, {
          key: "CalcAge",
          value: function CalcAge(bDate) {
            var cDate = new Date();
            var mill = cDate.getTime() - bDate.getTime();
            var age = Math.floor(mill / (365 * 24 * 60 * 60 * 1000));
            return age;
          }
        }, {
          key: "GetEnumString",
          value: function GetEnumString(E, value) {
            var keys = Object.keys(E).filter(function (k) {
              return typeof E[k] === "number";
            }); // ["A", "B"]

            var values = keys.map(function (k) {
              return E[k];
            }); // [0, 1]
          }
        }, {
          key: "ConvertEnumToList",
          value: function ConvertEnumToList(E) {
            var keys = Object.keys(E).filter(function (k) {
              return typeof E[k] === "number";
            }); // ["A", "B"]

            var values = keys.map(function (k) {
              return E[k];
            }); // [0, 1]

            var list = [];

            for (var i = 0; i < keys.length; i++) {
              list.push({
                key: keys[i],
                value: values[i]
              });
            }

            return list;
          }
        }, {
          key: "ConvertEnumToDictionary",
          value: function ConvertEnumToDictionary(E) {
            var dic = {};
            var lst = Utils.ConvertEnumToList(E);

            var _iterator11 = _createForOfIteratorHelper(lst),
                _step11;

            try {
              for (_iterator11.s(); !(_step11 = _iterator11.n()).done;) {
                var i = _step11.value;
                dic[i.value] = i.key;
              }
            } catch (err) {
              _iterator11.e(err);
            } finally {
              _iterator11.f();
            }

            return dic;
          }
        }, {
          key: "ConvertEnumToListLocalization",
          value: function ConvertEnumToListLocalization(E, enumName) {
            var keys = Object.keys(E).filter(function (k) {
              return typeof E[k] === "number";
            }); // ["A", "B"]

            var values = keys.map(function (k) {
              return E[k];
            }); // [0, 1]

            var list = [];

            for (var i = 0; i < keys.length; i++) {
              keys[i] = _shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Word(enumName + "_" + keys[i]);
              list.push({
                key: keys[i],
                value: values[i]
              });
            }

            return list;
          }
        }]);

        return Utils;
      }();

      Utils.i = 0;
      /***/
    },

    /***/
    "VZL9":
    /*!**********************************************************!*\
      !*** ./src/core/codeshell/base-components/topBarBase.ts ***!
      \**********************************************************/

    /*! exports provided: TopBarBase */

    /***/
    function VZL9(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "TopBarBase", function () {
        return TopBarBase;
      });
      /* harmony import */


      var codeshell_serverConfigBase__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! codeshell/serverConfigBase */
      "6m0k");
      /* harmony import */


      var codeshell_shell__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! codeshell/shell */
      "UoIT");
      /* harmony import */


      var _angular_router__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! @angular/router */
      "tyNb");
      /* harmony import */


      var _angular_common_http__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! @angular/common/http */
      "tk/3");
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");

      var TopBarBase = /*#__PURE__*/function () {
        function TopBarBase() {
          _classCallCheck(this, TopBarBase);

          this.isLoggedIn = false;
          this.navState = true;
          this.changePasswordItem = false;
          this.editProfileItem = false;
          this.notificationCount = 0;
        }

        _createClass(TopBarBase, [{
          key: "Router",
          get: function get() {
            return codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Injector.get(_angular_router__WEBPACK_IMPORTED_MODULE_2__["Router"]);
          }
        }, {
          key: "_startListener",
          value: function _startListener() {
            var _this32 = this;

            if (this.Listener) {
              this.Listener.NotificationsChanged.subscribe(function (e) {
                console.log(e);
                _this32.notificationCount = e;

                _this32.OnNotificationChanged(e);
              });
              this.Listener.KeepAlive = true;
            }
          }
        }, {
          key: "ngOnInit",
          value: function ngOnInit() {
            var _this33 = this;

            this._startListener();

            codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Session.GetUserAsync().then(function (d) {
              _this33.isLoggedIn = true;
              _this33.user = d;

              _this33._onUser(_this33.user);

              _this33.OnReady();
            })["catch"](function (d) {
              _this33.OnStartNoSession();

              _this33.OnReady();
            });
            codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Session.LogStatus.subscribe(function (v) {
              _this33.isLoggedIn = v;

              _this33.OnLogStatusChange(v);
            });
            codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Session.OnLogin.subscribe(function (u) {
              _this33.user = u;

              _this33._onUser(u);
            });
            codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].ViewLoaded.subscribe(function (d) {//$(".wrapper-side").removeClass("expanded");
            });
            var conf = codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Injector.get(codeshell_serverConfigBase__WEBPACK_IMPORTED_MODULE_0__["ServerConfigBase"]);
            this.Lang = conf.Locale;
          }
          /**
           * overridable : called after user data is obtained from server
           */

        }, {
          key: "OnReady",
          value: function OnReady() {}
          /**
           * overridable called when user logs in or out
           * @param status
           */

        }, {
          key: "OnLogStatusChange",
          value: function OnLogStatusChange(status) {}
          /**
           * called after login or when user is found
           */

        }, {
          key: "OnSession",
          value: function OnSession(userDto) {}
        }, {
          key: "_onUser",
          value: function _onUser(dto) {
            var _this34 = this;

            if (this.Listener) {
              this.Listener.StartWithUser(dto.userId);
            }

            if (this.NotificationService) this.NotificationService.GetCount().then(function (e) {
              return _this34.notificationCount = e;
            });
            this.OnSession(dto);
          }
        }, {
          key: "OnNotificationChanged",
          value: function OnNotificationChanged(c) {}
          /**
           * overridable : called when failing to obtain user data on startup
           */

        }, {
          key: "OnStartNoSession",
          value: function OnStartNoSession() {}
        }, {
          key: "Logout",
          value: function Logout() {
            codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Session.EndSession();
            this.isLoggedIn = false;
            codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Injector.get(_angular_router__WEBPACK_IMPORTED_MODULE_2__["Router"]).navigateByUrl("/Login");
          }
        }, {
          key: "Slide",
          value: function Slide() {
            if (!codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Main.ShowNav) return; //$(".wrapper-side").toggleClass("expanded");
          }
        }, {
          key: "ToggleNav",
          value: function ToggleNav() {
            if (!codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Main.ShowNav) return;
            this.setSideBarState(!this.navState);
          }
        }, {
          key: "setSideBarState",
          value: function setSideBarState(state) {
            this.navState = state;

            if (!state) {//$(".wrapper-side").addClass("compressed");
              // $(".wrapper-content").addClass("expanded");
            } else {//$(".wrapper-side").removeClass("compressed");
                //$(".wrapper-content").removeClass("expanded");
              }
          }
        }, {
          key: "ChangeLang",
          value: function ChangeLang() {
            var cl = codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Injector.get(_angular_common_http__WEBPACK_IMPORTED_MODULE_3__["HttpClient"]);
            var conf = codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Injector.get(codeshell_serverConfigBase__WEBPACK_IMPORTED_MODULE_0__["ServerConfigBase"]);
            cl.get("/Home/SetLocale/?lang=" + (conf.Locale == 'ar' ? 'en' : 'ar')).subscribe(function (d) {
              location.reload();
            });
          }
        }, {
          key: "SetLang",
          value: function SetLang(lng) {
            var _this35 = this;

            var cl = codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Injector.get(_angular_common_http__WEBPACK_IMPORTED_MODULE_3__["HttpClient"]);
            var conf = codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Injector.get(codeshell_serverConfigBase__WEBPACK_IMPORTED_MODULE_0__["ServerConfigBase"]);
            this.Lang = conf.Locale;
            cl.get("/Home/SetLocale/?lang=" + lng).subscribe(function (d) {
              _this35.Lang = lng;
              location.reload();
            });
          }
        }, {
          key: "OpenModal",
          value: function OpenModal(path) {
            var comp = codeshell_shell__WEBPACK_IMPORTED_MODULE_1__["Shell"].Main.GetDialogAs(path);

            if (comp != null) {
              return Promise.resolve(comp);
            }

            return Promise.reject("not found");
          }
        }, {
          key: "ChangePassword",
          value: function ChangePassword() {
            if (this.changePasswordComponent) {
              this.OpenModal(this.changePasswordComponent).then(function (comp) {
                comp.instance.Show = true;

                comp.instance.DataSubmitted = function (res) {
                  comp.instance.Show = false;
                };
              });
            }
          }
        }, {
          key: "EditProfile",
          value: function EditProfile() {
            if (this.editProfileComponent) this.Router.navigateByUrl(this.editProfileComponent);
          }
        }]);

        return TopBarBase;
      }();

      TopBarBase.ɵfac = function TopBarBase_Factory(t) {
        return new (t || TopBarBase)();
      };

      TopBarBase.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵdefineComponent"]({
        type: TopBarBase,
        selectors: [["ng-component"]],
        decls: 0,
        vars: 0,
        template: function TopBarBase_Template(rf, ctx) {},
        encapsulation: 2
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵsetClassMetadata"](TopBarBase, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_4__["Component"],
          args: [{
            template: ''
          }]
        }], function () {
          return [];
        }, null);
      })();
      /***/

    },

    /***/
    "WNdI":
    /*!*************************************************!*\
      !*** ./src/core/base/main/top-bar.component.ts ***!
      \*************************************************/

    /*! exports provided: TopBar */

    /***/
    function WNdI(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "TopBar", function () {
        return TopBar;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var codeshell_base_components__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! codeshell/base-components */
      "gyvI");
      /* harmony import */


      var _angular_common__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! @angular/common */
      "ofXK");
      /* harmony import */


      var _angular_router__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! @angular/router */
      "tyNb");
      /* harmony import */


      var _ngx_translate_core__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
      /*! @ngx-translate/core */
      "sYmb");

      function TopBar_li_16_Template(rf, ctx) {
        if (rf & 1) {
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "li");

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](1, "a", 12);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](2);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](3, "translate");

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](4, "i", 13);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        }

        if (rf & 2) {
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate1"](" ", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind1"](3, 1, "Words.Login"), " ");
        }
      }

      function TopBar_li_17_li_6_Template(rf, ctx) {
        if (rf & 1) {
          var _r5 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵgetCurrentView"]();

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "li");

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](1, "a", 9);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function TopBar_li_17_li_6_Template_a_click_1_listener() {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r5);

            var ctx_r4 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"](2);

            return ctx_r4.ChangePassword();
          });

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](2, "i", 20);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](3);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](4, "translate");

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        }

        if (rf & 2) {
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](3);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate1"](" \xA0\xA0", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind1"](4, 1, "Words.ChangePassword"), " ");
        }
      }

      function TopBar_li_17_li_7_Template(rf, ctx) {
        if (rf & 1) {
          var _r7 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵgetCurrentView"]();

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "li");

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](1, "a", 9);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function TopBar_li_17_li_7_Template_a_click_1_listener() {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r7);

            var ctx_r6 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"](2);

            return ctx_r6.EditProfile();
          });

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](2, "i", 21);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](3);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](4, "translate");

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        }

        if (rf & 2) {
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](3);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate1"](" \xA0\xA0", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind1"](4, 1, "Words.EditProfile"), " ");
        }
      }

      function TopBar_li_17_Template(rf, ctx) {
        if (rf & 1) {
          var _r9 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵgetCurrentView"]();

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

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function TopBar_li_17_Template_a_click_9_listener() {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r9);

            var ctx_r8 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();

            return ctx_r8.Logout();
          });

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](10, "i", 19);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](11);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](12, "translate");

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        }

        if (rf & 2) {
          var ctx_r1 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate1"](" ", ctx_r1.user.name, " ");

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("src", ctx_r1.user.photo ? "/" + ctx_r1.user.photo : "/img/default_user.png", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵsanitizeUrl"]);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](3);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", ctx_r1.changePasswordComponent);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", ctx_r1.editProfileComponent);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](4);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate1"](" \xA0\xA0 ", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind1"](12, 5, "Words.Logout"), " ");
        }
      }

      var TopBar = /*#__PURE__*/function (_codeshell_base_compo3) {
        _inherits(TopBar, _codeshell_base_compo3);

        var _super13 = _createSuper(TopBar);

        function TopBar() {
          _classCallCheck(this, TopBar);

          return _super13.apply(this, arguments);
        }

        return TopBar;
      }(codeshell_base_components__WEBPACK_IMPORTED_MODULE_1__["TopBarBase"]);

      TopBar.ɵfac = function TopBar_Factory(t) {
        return ɵTopBar_BaseFactory(t || TopBar);
      };

      TopBar.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineComponent"]({
        type: TopBar,
        selectors: [["app-top-bar"]],
        exportAs: ["app-top-bar"],
        features: [_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵInheritDefinitionFeature"]],
        decls: 18,
        vars: 5,
        consts: [[1, "top-bar"], [1, "container-fluid"], [1, "row"], [1, "pull-first"], [1, "nav", "nav-pills"], [1, "opt-btn", "mob", 3, "click"], [1, "fa", "fa-bars"], [1, "opt-btn", "not-mob", 3, "click"], [1, "pull-last"], [3, "click"], [4, "ngIf"], ["role", "presentation", "class", "dropdown", 4, "ngIf"], ["routerLink", "/Login", "routerLinkActive", "hidden"], [1, "fa", "fa-sign-in-alt"], ["role", "presentation", 1, "dropdown"], ["data-toggle", "dropdown", "href", "#", "role", "button", "aria-haspopup", "true", "aria-expanded", "false", 1, "dropdown-toggle"], [1, "img-circle", 3, "src"], [1, "caret"], [1, "dropdown-menu", "navbar-inverse"], [1, "fa", "fa-sign-out-alt"], [1, "fa", "fa-key"], [1, "fa", "fa-user"]],
        template: function TopBar_Template(rf, ctx) {
          if (rf & 1) {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "nav", 0);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](1, "div", 1);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](2, "div", 2);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](3, "div", 3);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](4, "ul", 4);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](5, "li");

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](6, "a", 5);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function TopBar_Template_a_click_6_listener() {
              return ctx.Slide();
            });

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](7, "i", 6);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](8, "a", 7);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function TopBar_Template_a_click_8_listener() {
              return ctx.ToggleNav();
            });

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](9, "i", 6);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](10, "div", 8);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](11, "ul", 4);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](12, "li");

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](13, "a", 9);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function TopBar_Template_a_click_13_listener() {
              return ctx.ChangeLang();
            });

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
          }

          if (rf & 2) {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](14);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate1"](" ", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind1"](15, 3, "Words.Lang"), " ");

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", !ctx.isLoggedIn);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", ctx.isLoggedIn);
          }
        },
        directives: [_angular_common__WEBPACK_IMPORTED_MODULE_2__["NgIf"], _angular_router__WEBPACK_IMPORTED_MODULE_3__["RouterLinkWithHref"], _angular_router__WEBPACK_IMPORTED_MODULE_3__["RouterLinkActive"]],
        pipes: [_ngx_translate_core__WEBPACK_IMPORTED_MODULE_4__["TranslatePipe"]],
        encapsulation: 2
      });

      var ɵTopBar_BaseFactory = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵgetInheritedFactory"](TopBar);
      /*@__PURE__*/


      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](TopBar, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"],
          args: [{
            templateUrl: "./top-bar.component.html",
            selector: "app-top-bar",
            exportAs: "app-top-bar"
          }]
        }], null, null);
      })();
      /***/

    },

    /***/
    "WyvS":
    /*!*************************************************!*\
      !*** ./src/core/codeshell/pipes/absoluteUrl.ts ***!
      \*************************************************/

    /*! exports provided: AbsoluteUrl */

    /***/
    function WyvS(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "AbsoluteUrl", function () {
        return AbsoluteUrl;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var codeshell_utilities_utils__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! codeshell/utilities/utils */
      "VXkE");

      var AbsoluteUrl = /*#__PURE__*/function () {
        function AbsoluteUrl() {
          _classCallCheck(this, AbsoluteUrl);
        }

        _createClass(AbsoluteUrl, [{
          key: "transform",
          value: function transform(value) {
            return Object(codeshell_utilities_utils__WEBPACK_IMPORTED_MODULE_1__["absUrl"])(value);
          }
        }]);

        return AbsoluteUrl;
      }();

      AbsoluteUrl.ɵfac = function AbsoluteUrl_Factory(t) {
        return new (t || AbsoluteUrl)();
      };

      AbsoluteUrl.ɵpipe = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefinePipe"]({
        name: "absUrl",
        type: AbsoluteUrl,
        pure: true
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](AbsoluteUrl, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Pipe"],
          args: [{
            name: 'absUrl'
          }]
        }], null, null);
      })();
      /***/

    },

    /***/
    "XGJ5":
    /*!******************************************************************!*\
      !*** ./src/core/codeshell/directives/control-group.directive.ts ***!
      \******************************************************************/

    /*! exports provided: BsFormGroup */

    /***/
    function XGJ5(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "BsFormGroup", function () {
        return BsFormGroup;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");

      var BsFormGroup = /*#__PURE__*/function () {
        function BsFormGroup(el) {
          _classCallCheck(this, BsFormGroup);

          this.el = el;
          this.TextInput = null;
          this.SelectInput = null;
          this.TextareaInput = null;
          this.RadioInput = null;
          this.OtherInput = null;
          this._parent = null;
          this._enabled = true;
        }

        _createClass(BsFormGroup, [{
          key: "InputControl",
          get: function get() {
            return this._getInputControl();
          }
        }, {
          key: "Write",
          get: function get() {
            return this._enabled;
          }
        }, {
          key: "Group",
          get: function get() {
            return this.el.nativeElement;
          }
        }, {
          key: "Enabled",
          get: function get() {
            return this._enabled;
          },
          set: function set(val) {
            this._setEnabled(val);
          }
        }, {
          key: "Value",
          get: function get() {
            return this._getValue();
          }
        }, {
          key: "ngOnInit",
          value: function ngOnInit() {
            var rad = this.Group.querySelector(".radio-group");

            if (rad) {
              this.RadioInput = rad;
            } else {
              this.TextInput = this.Group.querySelector("input");
              this.SelectInput = this.Group.querySelector("select");
              this.TextareaInput = this.Group.querySelector("textarea");
              this.OtherInput = this.Group.querySelector(".input-control");
            }

            if (this.InputControl) this._parent = this.InputControl.parentElement;
          }
        }, {
          key: "_setEnabled",
          value: function _setEnabled(val) {
            if (this._enabled == val) return;
            this._enabled = val;

            if (this.RadioInput) {
              var inps = this.RadioInput.getElementsByTagName("input");

              for (var i = 0; i < inps.length; i++) {
                var s = inps.item(i);
                if (s) s.disabled = !this._enabled;
              }

              return;
            }

            if (this._enabled == false) {
              if (this._parent) this._parent.removeChild(this.InputControl);
            } else {
              if (this._parent) this._parent.appendChild(this.InputControl);
            }
          }
        }, {
          key: "_getValue",
          value: function _getValue() {
            if (this.TextInput) return this.TextInput.value;else if (this.SelectInput) {
              var ite = this.SelectInput.selectedOptions.item(0);

              if (ite) {
                return ite.innerHTML;
              }
            } else if (this.TextareaInput) {
              return this.TextareaInput.value;
            }
            return "";
          }
        }, {
          key: "_getInputControl",
          value: function _getInputControl() {
            if (this.TextInput) return this.TextInput;else if (this.SelectInput) {
              return this.SelectInput;
            } else if (this.TextareaInput) {
              return this.TextareaInput;
            } else if (this.OtherInput) {
              return this.OtherInput;
            }
            return null;
          }
        }]);

        return BsFormGroup;
      }();

      BsFormGroup.ɵfac = function BsFormGroup_Factory(t) {
        return new (t || BsFormGroup)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]));
      };

      BsFormGroup.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({
        type: BsFormGroup,
        selectors: [["", "bs-group", ""]],
        exportAs: ["bsFormGroup"]
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](BsFormGroup, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
          args: [{
            selector: "[bs-group]",
            exportAs: "bsFormGroup"
          }]
        }], function () {
          return [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]
          }];
        }, null);
      })();
      /***/

    },

    /***/
    "XgJV":
    /*!************************************************!*\
      !*** ./src/core/codeshell/directives/index.ts ***!
      \************************************************/

    /*! exports provided: ImagePreLoad, OnEnter, ShowIf, Editable, SlimScroll, ComponentLoader, BsFormGroup, ListItemWatcher, Radio, DirctionFix, FixDate, FixDateTime, Selectable, FileUploader */

    /***/
    function XgJV(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony import */


      var _image_preLoad_directive__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! ./image-preLoad.directive */
      "5Qa7");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "ImagePreLoad", function () {
        return _image_preLoad_directive__WEBPACK_IMPORTED_MODULE_0__["ImagePreLoad"];
      });
      /* harmony import */


      var _on_enter_directive__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ./on-enter.directive */
      "F2JI");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "OnEnter", function () {
        return _on_enter_directive__WEBPACK_IMPORTED_MODULE_1__["OnEnter"];
      });
      /* harmony import */


      var _show_if_directive__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! ./show-if.directive */
      "bWkv");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "ShowIf", function () {
        return _show_if_directive__WEBPACK_IMPORTED_MODULE_2__["ShowIf"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "Editable", function () {
        return _show_if_directive__WEBPACK_IMPORTED_MODULE_2__["Editable"];
      });
      /* harmony import */


      var _slim_scroll_directive__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! ./slim-scroll.directive */
      "6Ipe");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "SlimScroll", function () {
        return _slim_scroll_directive__WEBPACK_IMPORTED_MODULE_3__["SlimScroll"];
      });
      /* harmony import */


      var _component_loader_directive__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
      /*! ./component-loader.directive */
      "j3l2");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "ComponentLoader", function () {
        return _component_loader_directive__WEBPACK_IMPORTED_MODULE_4__["ComponentLoader"];
      });
      /* harmony import */


      var _control_group_directive__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(
      /*! ./control-group.directive */
      "XGJ5");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "BsFormGroup", function () {
        return _control_group_directive__WEBPACK_IMPORTED_MODULE_5__["BsFormGroup"];
      });
      /* harmony import */


      var _list_item_watcher_directive__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(
      /*! ./list-item-watcher.directive */
      "EuMQ");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "ListItemWatcher", function () {
        return _list_item_watcher_directive__WEBPACK_IMPORTED_MODULE_6__["ListItemWatcher"];
      });
      /* harmony import */


      var _radio_directive__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(
      /*! ./radio.directive */
      "qJZ4");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "Radio", function () {
        return _radio_directive__WEBPACK_IMPORTED_MODULE_7__["Radio"];
      });
      /* harmony import */


      var _direction_fix_directive__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(
      /*! ./direction-fix.directive */
      "n2jX");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "DirctionFix", function () {
        return _direction_fix_directive__WEBPACK_IMPORTED_MODULE_8__["DirctionFix"];
      });
      /* harmony import */


      var _fix_date_directive__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(
      /*! ./fix-date.directive */
      "x5Rz");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "FixDate", function () {
        return _fix_date_directive__WEBPACK_IMPORTED_MODULE_9__["FixDate"];
      });
      /* harmony import */


      var _fix_date_time_directive__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(
      /*! ./fix-date-time.directive */
      "S+5U");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "FixDateTime", function () {
        return _fix_date_time_directive__WEBPACK_IMPORTED_MODULE_10__["FixDateTime"];
      });
      /* harmony import */


      var _selectable_directive__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(
      /*! ./selectable.directive */
      "CJgx");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "Selectable", function () {
        return _selectable_directive__WEBPACK_IMPORTED_MODULE_11__["Selectable"];
      });
      /* harmony import */


      var _file_uploader_directive__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(
      /*! ./file-uploader.directive */
      "ZCxi");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "FileUploader", function () {
        return _file_uploader_directive__WEBPACK_IMPORTED_MODULE_12__["FileUploader"];
      });
      /***/

    },

    /***/
    "YNU7":
    /*!*******************************************************************!*\
      !*** ./src/core/codeshell/components/duration-input.component.ts ***!
      \*******************************************************************/

    /*! exports provided: DurationInput */

    /***/
    function YNU7(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "DurationInput", function () {
        return DurationInput;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var _angular_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! @angular/common */
      "ofXK");
      /* harmony import */


      var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! @angular/forms */
      "3Pt+");

      function DurationInput_p_2_Template(rf, ctx) {
        if (rf & 1) {
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "p");

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](1);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        }

        if (rf & 2) {
          var ctx_r0 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate"](ctx_r0.hours);
        }
      }

      function DurationInput_input_3_Template(rf, ctx) {
        if (rf & 1) {
          var _r7 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵgetCurrentView"]();

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "input", 6);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("change", function DurationInput_input_3_Template_input_change_0_listener() {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r7);

            var ctx_r6 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();

            return ctx_r6.onPartChanged();
          })("ngModelChange", function DurationInput_input_3_Template_input_ngModelChange_0_listener($event) {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r7);

            var ctx_r8 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();

            return ctx_r8.hours = $event;
          });

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        }

        if (rf & 2) {
          var ctx_r1 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngModel", ctx_r1.hours);
        }
      }

      function DurationInput_p_7_Template(rf, ctx) {
        if (rf & 1) {
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "p");

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](1);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        }

        if (rf & 2) {
          var ctx_r2 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate"](ctx_r2.minutes);
        }
      }

      function DurationInput_input_8_Template(rf, ctx) {
        if (rf & 1) {
          var _r10 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵgetCurrentView"]();

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "input", 7);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("change", function DurationInput_input_8_Template_input_change_0_listener() {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r10);

            var ctx_r9 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();

            return ctx_r9.onPartChanged();
          })("ngModelChange", function DurationInput_input_8_Template_input_ngModelChange_0_listener($event) {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r10);

            var ctx_r11 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();

            return ctx_r11.minutes = $event;
          });

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        }

        if (rf & 2) {
          var ctx_r3 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngModel", ctx_r3.minutes);
        }
      }

      function DurationInput_p_12_Template(rf, ctx) {
        if (rf & 1) {
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "p");

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](1);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        }

        if (rf & 2) {
          var ctx_r4 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate"](ctx_r4.seconds);
        }
      }

      function DurationInput_input_13_Template(rf, ctx) {
        if (rf & 1) {
          var _r13 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵgetCurrentView"]();

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "input", 8);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("change", function DurationInput_input_13_Template_input_change_0_listener() {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r13);

            var ctx_r12 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();

            return ctx_r12.onPartChanged();
          })("ngModelChange", function DurationInput_input_13_Template_input_ngModelChange_0_listener($event) {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r13);

            var ctx_r14 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();

            return ctx_r14.seconds = $event;
          });

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        }

        if (rf & 2) {
          var ctx_r5 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngModel", ctx_r5.seconds);
        }
      }

      var DurationInput = /*#__PURE__*/function () {
        function DurationInput(el) {
          _classCallCheck(this, DurationInput);

          this.el = el;
          this.hours = 0;
          this.minutes = 0;
          this.seconds = 0;
          this._model = 0;
          this.readOnly = false;
          this.modelChange = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
          var html = el.nativeElement;
        }

        _createClass(DurationInput, [{
          key: "model",
          get: function get() {
            return this._model;
          },
          set: function set(val) {
            this._model = val;
            this.applyToFields();
          }
        }, {
          key: "onPartChanged",
          value: function onPartChanged() {
            this._model = this.hours * 60 * 60 + this.minutes * 60 + this.seconds;
            this.modelChange.emit(this._model);
          }
        }, {
          key: "applyToFields",
          value: function applyToFields() {
            this.hours = 0;
            this.minutes = 0;
            this.seconds = 0;

            if (this._model >= 3600) {
              this.hours = Math.floor(this._model / 3600);
            }

            if (this._model >= 60) {
              var hrsRem = this._model % 3600;
              this.minutes = Math.floor(hrsRem / 60);
            }

            var minRem = this._model % 60;
            this.seconds = Math.floor(minRem);
          }
        }]);

        return DurationInput;
      }();

      DurationInput.ɵfac = function DurationInput_Factory(t) {
        return new (t || DurationInput)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]));
      };

      DurationInput.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineComponent"]({
        type: DurationInput,
        selectors: [["duration-input"]],
        inputs: {
          readOnly: "readOnly",
          model: ["totalSeconds", "model"]
        },
        outputs: {
          modelChange: "totalSecondsChange"
        },
        exportAs: ["duration-input"],
        decls: 16,
        vars: 9,
        consts: [[1, "di-container"], [1, "di-part-container"], [4, "ngIf"], ["type", "number", "class", "form-control di-hours", "min", "0", "max", "24", 3, "ngModel", "change", "ngModelChange", 4, "ngIf"], ["type", "number", "class", "form-control di-minutes", "min", "0", "max", "59", 3, "ngModel", "change", "ngModelChange", 4, "ngIf"], ["type", "number", "class", "form-control di-seconds", "min", "0", "max", "59", 3, "ngModel", "change", "ngModelChange", 4, "ngIf"], ["type", "number", "min", "0", "max", "24", 1, "form-control", "di-hours", 3, "ngModel", "change", "ngModelChange"], ["type", "number", "min", "0", "max", "59", 1, "form-control", "di-minutes", 3, "ngModel", "change", "ngModelChange"], ["type", "number", "min", "0", "max", "59", 1, "form-control", "di-seconds", 3, "ngModel", "change", "ngModelChange"]],
        template: function DurationInput_Template(rf, ctx) {
          if (rf & 1) {
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
          }

          if (rf & 2) {
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
          }
        },
        directives: [_angular_common__WEBPACK_IMPORTED_MODULE_1__["NgIf"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["NumberValueAccessor"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["DefaultValueAccessor"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["NgControlStatus"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["NgModel"]],
        encapsulation: 2
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](DurationInput, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"],
          args: [{
            templateUrl: "./duration-input.component.html",
            selector: "duration-input",
            exportAs: "duration-input"
          }]
        }], function () {
          return [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]
          }];
        }, {
          readOnly: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["readOnly"]
          }],
          model: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["totalSeconds"]
          }],
          modelChange: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"],
            args: ["totalSecondsChange"]
          }]
        });
      })();
      /***/

    },

    /***/
    "YTpK":
    /*!***************************************************!*\
      !*** ./src/core/codeshell/security/authFilter.ts ***!
      \***************************************************/

    /*! exports provided: AuthFilter */

    /***/
    function YTpK(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "AuthFilter", function () {
        return AuthFilter;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var _models__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ./models */
      "gFuU");
      /* harmony import */


      var _authorizationServiceBase__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! ./authorizationServiceBase */
      "rp8W");

      var AuthFilter = /*#__PURE__*/function () {
        function AuthFilter(authorizationService) {
          _classCallCheck(this, AuthFilter);

          this.authorizationService = authorizationService;
        }

        _createClass(AuthFilter, [{
          key: "canActivate",
          value: function canActivate(route, state) {
            var data = Object.assign(new _models__WEBPACK_IMPORTED_MODULE_1__["RouteData"](), route.data);
            return this.authorizationService.IsAuthorizedAsync(data);
          }
        }]);

        return AuthFilter;
      }();

      AuthFilter.ɵfac = function AuthFilter_Factory(t) {
        return new (t || AuthFilter)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵinject"](_authorizationServiceBase__WEBPACK_IMPORTED_MODULE_2__["AuthorizationServiceBase"]));
      };

      AuthFilter.ɵprov = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineInjectable"]({
        token: AuthFilter,
        factory: AuthFilter.ɵfac
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](AuthFilter, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"]
        }], function () {
          return [{
            type: _authorizationServiceBase__WEBPACK_IMPORTED_MODULE_2__["AuthorizationServiceBase"]
          }];
        }, null);
      })();
      /***/

    },

    /***/
    "ZCxi":
    /*!******************************************************************!*\
      !*** ./src/core/codeshell/directives/file-uploader.directive.ts ***!
      \******************************************************************/

    /*! exports provided: FileUploader */

    /***/
    function ZCxi(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "FileUploader", function () {
        return FileUploader;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");

      var FileUploader = /*#__PURE__*/function () {
        function FileUploader(elem) {
          _classCallCheck(this, FileUploader);

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

        _createClass(FileUploader, [{
          key: "FileData",
          get: function get() {
            return this._fileData;
          },
          set: function set(data) {
            this._fileData = data;
          }
        }, {
          key: "fileDataMany",
          get: function get() {
            return this._fileDataMany;
          },
          set: function set(data) {
            this._fileDataMany = data;
          }
        }, {
          key: "OnDragOver",
          value: function OnDragOver(event) {
            event.stopPropagation();
            event.preventDefault();
          }
        }, {
          key: "OnDrop",
          value: function OnDrop(event) {
            if (event.dataTransfer) {
              this._uploadTmp(event.dataTransfer.files);
            }

            event.preventDefault();
          }
        }, {
          key: "OnChange",
          value: function OnChange(ev) {
            if (!this._fileInput) return;

            if (this.Element.files) {
              this._uploadTmp(this.Element.files);
            }

            if (this.Element) this.Element.value = "";
          }
        }, {
          key: "_uploadTmp",
          value: function _uploadTmp(files) {
            var _this36 = this;

            if (this.Uploader) {
              this.Uploader(files).then(function (d) {
                if (_this36.multiple) {
                  _this36._fileDataMany = d;

                  _this36.fileDataManyChange.emit(_this36._fileDataMany);
                } else if (d.length > 0) {
                  _this36._fileData = d[0];

                  _this36.FileDataChange.emit(_this36._fileData);
                } else {
                  _this36._fileData = null;

                  _this36.FileDataChange.emit(null);
                }
              })["catch"](function (e) {
                _this36._fileData = null;

                _this36.FileDataChange.emit(null);
              });
            }

            this.Element.value = "";
          }
        }, {
          key: "ngOnChanges",
          value: function ngOnChanges(changes) {}
        }]);

        return FileUploader;
      }();

      FileUploader.ɵfac = function FileUploader_Factory(t) {
        return new (t || FileUploader)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]));
      };

      FileUploader.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({
        type: FileUploader,
        selectors: [["", "file-uploader", ""]],
        hostBindings: function FileUploader_HostBindings(rf, ctx) {
          if (rf & 1) {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("dragover", function FileUploader_dragover_HostBindingHandler($event) {
              return ctx.OnDragOver($event);
            })("drop", function FileUploader_drop_HostBindingHandler($event) {
              return ctx.OnDrop($event);
            })("change", function FileUploader_change_HostBindingHandler($event) {
              return ctx.OnChange($event);
            });
          }
        },
        inputs: {
          multiple: "multiple",
          Uploader: ["file-uploader", "Uploader"],
          FileData: ["fileData", "FileData"],
          fileDataMany: "fileDataMany"
        },
        outputs: {
          FileDataChange: "fileDataChange",
          fileDataManyChange: "fileDataManyChange"
        },
        exportAs: ["[file-uploader]"],
        features: [_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵNgOnChangesFeature"]]
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](FileUploader, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
          args: [{
            selector: "[file-uploader]",
            exportAs: "[file-uploader]"
          }]
        }], function () {
          return [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]
          }];
        }, {
          multiple: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["multiple"]
          }],
          Uploader: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["file-uploader"]
          }],
          FileData: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["fileData"]
          }],
          FileDataChange: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"],
            args: ["fileDataChange"]
          }],
          fileDataMany: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["fileDataMany"]
          }],
          fileDataManyChange: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"],
            args: ["fileDataManyChange"]
          }],
          OnDragOver: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"],
            args: ["dragover", ["$event"]]
          }],
          OnDrop: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"],
            args: ["drop", ["$event"]]
          }],
          OnChange: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"],
            args: ["change", ["$event"]]
          }]
        });
      })();
      /***/

    },

    /***/
    "a3Ap":
    /*!*************************************************************!*\
      !*** ./src/core/base/main/navigation-side-bar.component.ts ***!
      \*************************************************************/

    /*! exports provided: NavigationSideBar */

    /***/
    function a3Ap(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "NavigationSideBar", function () {
        return NavigationSideBar;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var codeshell_base_components__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! codeshell/base-components */
      "gyvI");
      /* harmony import */


      var _angular_router__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! @angular/router */
      "tyNb");
      /* harmony import */


      var _angular_common__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! @angular/common */
      "ofXK");
      /* harmony import */


      var _ngx_translate_core__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
      /*! @ngx-translate/core */
      "sYmb");

      function NavigationSideBar_li_6_Template(rf, ctx) {
        if (rf & 1) {
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "li");

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](1, "a", 4);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](2);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipe"](3, "translate");

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        }

        if (rf & 2) {
          var it_r1 = ctx.$implicit;

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("routerLink", "/" + it_r1.url);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);

          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind1"](3, 2, "Pages." + it_r1.name));
        }
      }

      var NavigationSideBar = /*#__PURE__*/function (_codeshell_base_compo4) {
        _inherits(NavigationSideBar, _codeshell_base_compo4);

        var _super14 = _createSuper(NavigationSideBar);

        function NavigationSideBar(inj) {
          _classCallCheck(this, NavigationSideBar);

          return _super14.call(this, inj);
        }

        return NavigationSideBar;
      }(codeshell_base_components__WEBPACK_IMPORTED_MODULE_1__["NavigationSideBarBase"]);

      NavigationSideBar.ɵfac = function NavigationSideBar_Factory(t) {
        return new (t || NavigationSideBar)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injector"]));
      };

      NavigationSideBar.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineComponent"]({
        type: NavigationSideBar,
        selectors: [["navigation-side-bar"]],
        features: [_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵInheritDefinitionFeature"]],
        decls: 7,
        vars: 5,
        consts: [[1, "navbar-inverse", "nav-side"], [1, "nav"], [3, "routerLink"], [4, "ngFor", "ngForOf"], ["routerLinkActive", "active", 3, "routerLink"]],
        template: function NavigationSideBar_Template(rf, ctx) {
          if (rf & 1) {
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
          }

          if (rf & 2) {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](3);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("routerLink", ctx.GetMainUrl());

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpipeBind1"](5, 3, "Words.Main"));

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);

            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngForOf", ctx.navs);
          }
        },
        directives: [_angular_router__WEBPACK_IMPORTED_MODULE_2__["RouterLinkWithHref"], _angular_common__WEBPACK_IMPORTED_MODULE_3__["NgForOf"], _angular_router__WEBPACK_IMPORTED_MODULE_2__["RouterLinkActive"]],
        pipes: [_ngx_translate_core__WEBPACK_IMPORTED_MODULE_4__["TranslatePipe"]],
        encapsulation: 2
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](NavigationSideBar, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"],
          args: [{
            templateUrl: "./navigation-side-bar.component.html",
            selector: "navigation-side-bar"
          }]
        }], function () {
          return [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Injector"]
          }];
        }, null);
      })();
      /***/

    },

    /***/
    "afo8":
    /*!****************************************************!*\
      !*** ./src/core/codeshell/utilities/extensions.ts ***!
      \****************************************************/

    /*! no static exports found */

    /***/
    function afo8(module, exports) {
      String.prototype.getBeforeLast = function (data) {
        var x = String(this).lastIndexOf(data);
        return String(this).substr(0, x);
      };
      /***/

    },

    /***/
    "bT1W":
    /*!************************************************!*\
      !*** ./src/core/codeshell/codeshell.module.ts ***!
      \************************************************/

    /*! exports provided: CodeShellModule */

    /***/
    function bT1W(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "CodeShellModule", function () {
        return CodeShellModule;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var _angular_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! @angular/common */
      "ofXK");
      /* harmony import */


      var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! @angular/forms */
      "3Pt+");
      /* harmony import */


      var _angular_material_datepicker__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! @angular/material/datepicker */
      "iadO");
      /* harmony import */


      var _angular_material_moment_adapter__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
      /*! @angular/material-moment-adapter */
      "1yaQ");
      /* harmony import */


      var _angular_common_http__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(
      /*! @angular/common/http */
      "tk/3");
      /* harmony import */


      var _angular_router__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(
      /*! @angular/router */
      "tyNb");
      /* harmony import */


      var _ngx_translate_core__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(
      /*! @ngx-translate/core */
      "sYmb");
      /* harmony import */


      var ngx_quill__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(
      /*! ngx-quill */
      "CzEO");
      /* harmony import */


      var _ng_select_ng_select__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(
      /*! @ng-select/ng-select */
      "ZOsW");
      /* harmony import */


      var angular_tree_component__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(
      /*! angular-tree-component */
      "rDsv");
      /* harmony import */


      var _directives__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(
      /*! ./directives */
      "XgJV");
      /* harmony import */


      var _validators__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(
      /*! ./validators */
      "eXOA");
      /* harmony import */


      var _components__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(
      /*! ./components */
      "4N+8");
      /* harmony import */


      var _localization__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(
      /*! ./localization */
      "InfA");
      /* harmony import */


      var _security__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(
      /*! ./security */
      "U6Sh");
      /* harmony import */


      var _security_authorizationServiceBase__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(
      /*! ./security/authorizationServiceBase */
      "rp8W");
      /* harmony import */


      var _security_tokenStorage__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(
      /*! ./security/tokenStorage */
      "yvMo");
      /* harmony import */


      var _pipes_absoluteUrl__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(
      /*! ./pipes/absoluteUrl */
      "WyvS");
      /* harmony import */


      var _angular_material_core__WEBPACK_IMPORTED_MODULE_19__ = __webpack_require__(
      /*! @angular/material/core */
      "FKr1"); //import { AngularDateTimePickerModule } from "angular2-datetimepicker";


      var CodeShellModule = /*#__PURE__*/function () {
        function CodeShellModule() {
          _classCallCheck(this, CodeShellModule);
        }

        _createClass(CodeShellModule, null, [{
          key: "forRoot",
          value: function forRoot() {
            return {
              ngModule: CodeShellModule,
              providers: [_security__WEBPACK_IMPORTED_MODULE_15__["AuthFilter"], {
                provide: _localization__WEBPACK_IMPORTED_MODULE_14__["TranslationService"],
                useClass: _localization__WEBPACK_IMPORTED_MODULE_14__["NgxTranslationService"]
              }, _security_authorizationServiceBase__WEBPACK_IMPORTED_MODULE_16__["AuthorizationServiceBase"], _security_tokenStorage__WEBPACK_IMPORTED_MODULE_17__["TokenStorage"]]
            };
          }
        }]);

        return CodeShellModule;
      }();

      CodeShellModule.ɵmod = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineNgModule"]({
        type: CodeShellModule
      });
      CodeShellModule.ɵinj = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineInjector"]({
        factory: function CodeShellModule_Factory(t) {
          return new (t || CodeShellModule)();
        },
        imports: [[_angular_common__WEBPACK_IMPORTED_MODULE_1__["CommonModule"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormsModule"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["ReactiveFormsModule"], _angular_common_http__WEBPACK_IMPORTED_MODULE_5__["HttpClientModule"], _angular_router__WEBPACK_IMPORTED_MODULE_6__["RouterModule"], _ng_select_ng_select__WEBPACK_IMPORTED_MODULE_9__["NgSelectModule"], _angular_material_datepicker__WEBPACK_IMPORTED_MODULE_3__["MatDatepickerModule"], _angular_material_core__WEBPACK_IMPORTED_MODULE_19__["MatNativeDateModule"], _angular_material_moment_adapter__WEBPACK_IMPORTED_MODULE_4__["MatMomentDateModule"], ngx_quill__WEBPACK_IMPORTED_MODULE_8__["QuillModule"].forRoot(), //AngularDateTimePickerModule,
        angular_tree_component__WEBPACK_IMPORTED_MODULE_10__["TreeModule"].forRoot(), _ngx_translate_core__WEBPACK_IMPORTED_MODULE_7__["TranslateModule"].forRoot({
          loader: {
            provide: _ngx_translate_core__WEBPACK_IMPORTED_MODULE_7__["TranslateLoader"],
            useClass: _localization__WEBPACK_IMPORTED_MODULE_14__["Translator"]
          }
        })], _angular_common__WEBPACK_IMPORTED_MODULE_1__["CommonModule"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormsModule"], _angular_common_http__WEBPACK_IMPORTED_MODULE_5__["HttpClientModule"], _angular_router__WEBPACK_IMPORTED_MODULE_6__["RouterModule"], _ng_select_ng_select__WEBPACK_IMPORTED_MODULE_9__["NgSelectModule"], _angular_material_datepicker__WEBPACK_IMPORTED_MODULE_3__["MatDatepickerModule"], _angular_material_core__WEBPACK_IMPORTED_MODULE_19__["MatNativeDateModule"], _angular_material_moment_adapter__WEBPACK_IMPORTED_MODULE_4__["MatMomentDateModule"], //AngularDateTimePickerModule,
        angular_tree_component__WEBPACK_IMPORTED_MODULE_10__["TreeModule"], _ngx_translate_core__WEBPACK_IMPORTED_MODULE_7__["TranslateModule"], ngx_quill__WEBPACK_IMPORTED_MODULE_8__["QuillModule"]]
      });

      (function () {
        (typeof ngJitMode === "undefined" || ngJitMode) && _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵsetNgModuleScope"](CodeShellModule, {
          declarations: [_directives__WEBPACK_IMPORTED_MODULE_11__["BsFormGroup"], _directives__WEBPACK_IMPORTED_MODULE_11__["ShowIf"], _directives__WEBPACK_IMPORTED_MODULE_11__["OnEnter"], _directives__WEBPACK_IMPORTED_MODULE_11__["SlimScroll"], _directives__WEBPACK_IMPORTED_MODULE_11__["ComponentLoader"], _directives__WEBPACK_IMPORTED_MODULE_11__["ImagePreLoad"], _directives__WEBPACK_IMPORTED_MODULE_11__["ListItemWatcher"], _directives__WEBPACK_IMPORTED_MODULE_11__["Editable"], _directives__WEBPACK_IMPORTED_MODULE_11__["Radio"], _validators__WEBPACK_IMPORTED_MODULE_12__["NumberRangeValidator"], _validators__WEBPACK_IMPORTED_MODULE_12__["IsUnique"], _validators__WEBPACK_IMPORTED_MODULE_12__["ModalValidator"], _components__WEBPACK_IMPORTED_MODULE_13__["Paginate"], _components__WEBPACK_IMPORTED_MODULE_13__["SearchGroup"], _directives__WEBPACK_IMPORTED_MODULE_11__["DirctionFix"], _directives__WEBPACK_IMPORTED_MODULE_11__["FixDate"], _directives__WEBPACK_IMPORTED_MODULE_11__["FixDateTime"], _validators__WEBPACK_IMPORTED_MODULE_12__["DateValidator"], _directives__WEBPACK_IMPORTED_MODULE_11__["Selectable"], _components__WEBPACK_IMPORTED_MODULE_13__["DurationInput"], _directives__WEBPACK_IMPORTED_MODULE_11__["FileUploader"], _pipes_absoluteUrl__WEBPACK_IMPORTED_MODULE_18__["AbsoluteUrl"]],
          imports: [_angular_common__WEBPACK_IMPORTED_MODULE_1__["CommonModule"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormsModule"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["ReactiveFormsModule"], _angular_common_http__WEBPACK_IMPORTED_MODULE_5__["HttpClientModule"], _angular_router__WEBPACK_IMPORTED_MODULE_6__["RouterModule"], _ng_select_ng_select__WEBPACK_IMPORTED_MODULE_9__["NgSelectModule"], _angular_material_datepicker__WEBPACK_IMPORTED_MODULE_3__["MatDatepickerModule"], _angular_material_core__WEBPACK_IMPORTED_MODULE_19__["MatNativeDateModule"], _angular_material_moment_adapter__WEBPACK_IMPORTED_MODULE_4__["MatMomentDateModule"], ngx_quill__WEBPACK_IMPORTED_MODULE_8__["QuillModule"], angular_tree_component__WEBPACK_IMPORTED_MODULE_10__["TreeModule"], _ngx_translate_core__WEBPACK_IMPORTED_MODULE_7__["TranslateModule"]],
          exports: [_angular_common__WEBPACK_IMPORTED_MODULE_1__["CommonModule"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormsModule"], _angular_common_http__WEBPACK_IMPORTED_MODULE_5__["HttpClientModule"], _angular_router__WEBPACK_IMPORTED_MODULE_6__["RouterModule"], _ng_select_ng_select__WEBPACK_IMPORTED_MODULE_9__["NgSelectModule"], _angular_material_datepicker__WEBPACK_IMPORTED_MODULE_3__["MatDatepickerModule"], _angular_material_core__WEBPACK_IMPORTED_MODULE_19__["MatNativeDateModule"], _angular_material_moment_adapter__WEBPACK_IMPORTED_MODULE_4__["MatMomentDateModule"], //AngularDateTimePickerModule,
          angular_tree_component__WEBPACK_IMPORTED_MODULE_10__["TreeModule"], _ngx_translate_core__WEBPACK_IMPORTED_MODULE_7__["TranslateModule"], ngx_quill__WEBPACK_IMPORTED_MODULE_8__["QuillModule"], _directives__WEBPACK_IMPORTED_MODULE_11__["FixDate"], _directives__WEBPACK_IMPORTED_MODULE_11__["FixDateTime"], _validators__WEBPACK_IMPORTED_MODULE_12__["DateValidator"], _directives__WEBPACK_IMPORTED_MODULE_11__["BsFormGroup"], _directives__WEBPACK_IMPORTED_MODULE_11__["ShowIf"], _directives__WEBPACK_IMPORTED_MODULE_11__["OnEnter"], _directives__WEBPACK_IMPORTED_MODULE_11__["SlimScroll"], _directives__WEBPACK_IMPORTED_MODULE_11__["ComponentLoader"], _directives__WEBPACK_IMPORTED_MODULE_11__["ImagePreLoad"], _directives__WEBPACK_IMPORTED_MODULE_11__["ListItemWatcher"], _directives__WEBPACK_IMPORTED_MODULE_11__["Editable"], _directives__WEBPACK_IMPORTED_MODULE_11__["Radio"], _validators__WEBPACK_IMPORTED_MODULE_12__["NumberRangeValidator"], _validators__WEBPACK_IMPORTED_MODULE_12__["IsUnique"], _validators__WEBPACK_IMPORTED_MODULE_12__["ModalValidator"], _components__WEBPACK_IMPORTED_MODULE_13__["Paginate"], _components__WEBPACK_IMPORTED_MODULE_13__["SearchGroup"], _directives__WEBPACK_IMPORTED_MODULE_11__["DirctionFix"], _directives__WEBPACK_IMPORTED_MODULE_11__["Selectable"], _components__WEBPACK_IMPORTED_MODULE_13__["DurationInput"], _directives__WEBPACK_IMPORTED_MODULE_11__["FileUploader"], _pipes_absoluteUrl__WEBPACK_IMPORTED_MODULE_18__["AbsoluteUrl"]]
        });
      })();
      /*@__PURE__*/


      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](CodeShellModule, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"],
          args: [{
            imports: [_angular_common__WEBPACK_IMPORTED_MODULE_1__["CommonModule"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormsModule"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["ReactiveFormsModule"], _angular_common_http__WEBPACK_IMPORTED_MODULE_5__["HttpClientModule"], _angular_router__WEBPACK_IMPORTED_MODULE_6__["RouterModule"], _ng_select_ng_select__WEBPACK_IMPORTED_MODULE_9__["NgSelectModule"], _angular_material_datepicker__WEBPACK_IMPORTED_MODULE_3__["MatDatepickerModule"], _angular_material_core__WEBPACK_IMPORTED_MODULE_19__["MatNativeDateModule"], _angular_material_moment_adapter__WEBPACK_IMPORTED_MODULE_4__["MatMomentDateModule"], ngx_quill__WEBPACK_IMPORTED_MODULE_8__["QuillModule"].forRoot(), //AngularDateTimePickerModule,
            angular_tree_component__WEBPACK_IMPORTED_MODULE_10__["TreeModule"].forRoot(), _ngx_translate_core__WEBPACK_IMPORTED_MODULE_7__["TranslateModule"].forRoot({
              loader: {
                provide: _ngx_translate_core__WEBPACK_IMPORTED_MODULE_7__["TranslateLoader"],
                useClass: _localization__WEBPACK_IMPORTED_MODULE_14__["Translator"]
              }
            })],
            declarations: [_directives__WEBPACK_IMPORTED_MODULE_11__["BsFormGroup"], _directives__WEBPACK_IMPORTED_MODULE_11__["ShowIf"], _directives__WEBPACK_IMPORTED_MODULE_11__["OnEnter"], _directives__WEBPACK_IMPORTED_MODULE_11__["SlimScroll"], _directives__WEBPACK_IMPORTED_MODULE_11__["ComponentLoader"], _directives__WEBPACK_IMPORTED_MODULE_11__["ImagePreLoad"], _directives__WEBPACK_IMPORTED_MODULE_11__["ListItemWatcher"], _directives__WEBPACK_IMPORTED_MODULE_11__["Editable"], _directives__WEBPACK_IMPORTED_MODULE_11__["Radio"], _validators__WEBPACK_IMPORTED_MODULE_12__["NumberRangeValidator"], _validators__WEBPACK_IMPORTED_MODULE_12__["IsUnique"], _validators__WEBPACK_IMPORTED_MODULE_12__["ModalValidator"], _components__WEBPACK_IMPORTED_MODULE_13__["Paginate"], _components__WEBPACK_IMPORTED_MODULE_13__["SearchGroup"], _directives__WEBPACK_IMPORTED_MODULE_11__["DirctionFix"], _directives__WEBPACK_IMPORTED_MODULE_11__["FixDate"], _directives__WEBPACK_IMPORTED_MODULE_11__["FixDateTime"], _validators__WEBPACK_IMPORTED_MODULE_12__["DateValidator"], _directives__WEBPACK_IMPORTED_MODULE_11__["Selectable"], _components__WEBPACK_IMPORTED_MODULE_13__["DurationInput"], _directives__WEBPACK_IMPORTED_MODULE_11__["FileUploader"], _pipes_absoluteUrl__WEBPACK_IMPORTED_MODULE_18__["AbsoluteUrl"]],
            exports: [_angular_common__WEBPACK_IMPORTED_MODULE_1__["CommonModule"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormsModule"], _angular_common_http__WEBPACK_IMPORTED_MODULE_5__["HttpClientModule"], _angular_router__WEBPACK_IMPORTED_MODULE_6__["RouterModule"], _ng_select_ng_select__WEBPACK_IMPORTED_MODULE_9__["NgSelectModule"], _angular_material_datepicker__WEBPACK_IMPORTED_MODULE_3__["MatDatepickerModule"], _angular_material_core__WEBPACK_IMPORTED_MODULE_19__["MatNativeDateModule"], _angular_material_moment_adapter__WEBPACK_IMPORTED_MODULE_4__["MatMomentDateModule"], //AngularDateTimePickerModule,
            angular_tree_component__WEBPACK_IMPORTED_MODULE_10__["TreeModule"], _ngx_translate_core__WEBPACK_IMPORTED_MODULE_7__["TranslateModule"], ngx_quill__WEBPACK_IMPORTED_MODULE_8__["QuillModule"], _directives__WEBPACK_IMPORTED_MODULE_11__["FixDate"], _directives__WEBPACK_IMPORTED_MODULE_11__["FixDateTime"], _validators__WEBPACK_IMPORTED_MODULE_12__["DateValidator"], _directives__WEBPACK_IMPORTED_MODULE_11__["BsFormGroup"], _directives__WEBPACK_IMPORTED_MODULE_11__["ShowIf"], _directives__WEBPACK_IMPORTED_MODULE_11__["OnEnter"], _directives__WEBPACK_IMPORTED_MODULE_11__["SlimScroll"], _directives__WEBPACK_IMPORTED_MODULE_11__["ComponentLoader"], _directives__WEBPACK_IMPORTED_MODULE_11__["ImagePreLoad"], _directives__WEBPACK_IMPORTED_MODULE_11__["ListItemWatcher"], _directives__WEBPACK_IMPORTED_MODULE_11__["Editable"], _directives__WEBPACK_IMPORTED_MODULE_11__["Radio"], _validators__WEBPACK_IMPORTED_MODULE_12__["NumberRangeValidator"], _validators__WEBPACK_IMPORTED_MODULE_12__["IsUnique"], _validators__WEBPACK_IMPORTED_MODULE_12__["ModalValidator"], _components__WEBPACK_IMPORTED_MODULE_13__["Paginate"], _components__WEBPACK_IMPORTED_MODULE_13__["SearchGroup"], _directives__WEBPACK_IMPORTED_MODULE_11__["DirctionFix"], _directives__WEBPACK_IMPORTED_MODULE_11__["Selectable"], _components__WEBPACK_IMPORTED_MODULE_13__["DurationInput"], _directives__WEBPACK_IMPORTED_MODULE_11__["FileUploader"], _pipes_absoluteUrl__WEBPACK_IMPORTED_MODULE_18__["AbsoluteUrl"]]
          }]
        }], null, null);
      })();
      /***/

    },

    /***/
    "bWkv":
    /*!************************************************************!*\
      !*** ./src/core/codeshell/directives/show-if.directive.ts ***!
      \************************************************************/

    /*! exports provided: ShowIf, Editable */

    /***/
    function bWkv(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "ShowIf", function () {
        return ShowIf;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "Editable", function () {
        return Editable;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var _angular_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! @angular/common */
      "ofXK");

      var ShowIf = /*#__PURE__*/function () {
        function ShowIf(el) {
          _classCallCheck(this, ShowIf);

          this.el = el;
          this.Condition = true;
        }

        _createClass(ShowIf, [{
          key: "Style",
          get: function get() {
            return this.el.nativeElement.style;
          }
        }, {
          key: "ngOnInit",
          value: function ngOnInit() {
            if (!this.Condition) {
              this.Style.display = "none";
            }
          }
        }, {
          key: "ngOnChanges",
          value: function ngOnChanges() {
            if (!this.Condition) {
              this.Style.display = "none";
            } else {
              this.Style.removeProperty("display");
            }
          }
        }]);

        return ShowIf;
      }();

      ShowIf.ɵfac = function ShowIf_Factory(t) {
        return new (t || ShowIf)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]));
      };

      ShowIf.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({
        type: ShowIf,
        selectors: [["", "show-if", ""]],
        inputs: {
          Condition: ["show-if", "Condition"]
        },
        features: [_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵNgOnChangesFeature"]]
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](ShowIf, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
          args: [{
            selector: '[show-if]'
          }]
        }], function () {
          return [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]
          }];
        }, {
          Condition: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["show-if"]
          }]
        });
      })();

      var Editable = /*#__PURE__*/function () {
        function Editable(ngFor) {
          _classCallCheck(this, Editable);

          this.ngFor = ngFor;
        }

        _createClass(Editable, [{
          key: "ngOnInit",
          value: function ngOnInit() {}
        }]);

        return Editable;
      }();

      Editable.ɵfac = function Editable_Factory(t) {
        return new (t || Editable)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_common__WEBPACK_IMPORTED_MODULE_1__["NgForOf"]));
      };

      Editable.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({
        type: Editable,
        selectors: [["", "ngFor", "", "ngForEditable", ""]],
        inputs: {
          Ed: ["ngForEditable", "Ed"]
        },
        exportAs: ["editable"]
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](Editable, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
          args: [{
            selector: '[ngFor][ngForEditable]',
            exportAs: "editable"
          }]
        }], function () {
          return [{
            type: _angular_common__WEBPACK_IMPORTED_MODULE_1__["NgForOf"]
          }];
        }, {
          Ed: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["ngForEditable"]
          }]
        });
      })();
      /***/

    },

    /***/
    "cPZL":
    /*!********************************************************!*\
      !*** ./src/core/codeshell/validators/validatorBase.ts ***!
      \********************************************************/

    /*! exports provided: ValidatorBase */

    /***/
    function cPZL(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "ValidatorBase", function () {
        return ValidatorBase;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! @angular/forms */
      "3Pt+");

      var ValidatorBase = /*#__PURE__*/function () {
        function ValidatorBase(el, model) {
          _classCallCheck(this, ValidatorBase);

          this.el = el;
          this.Model = model;
          this.Element = el.nativeElement;
        }

        _createClass(ValidatorBase, [{
          key: "valueChanged",
          value: function valueChanged(ev) {
            this.runValidation();
          }
        }, {
          key: "ngOnChanges",
          value: function ngOnChanges(ch) {
            if (this.RunIf(ch)) {
              this.runValidation();
            }
          }
        }, {
          key: "RunIf",
          value: function RunIf(ch) {
            return false;
          }
        }, {
          key: "runValidation",
          value: function runValidation() {
            var err = {};

            if (this.IsValid()) {
              err[this.Identifier] = null;
              this.Model.control.updateValueAndValidity();
            } else {
              err[this.Identifier] = true;
              this.Model.control.setErrors(err);
            }
          }
        }]);

        return ValidatorBase;
      }();

      ValidatorBase.ɵfac = function ValidatorBase_Factory(t) {
        return new (t || ValidatorBase)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵinject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]), _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵinject"](_angular_forms__WEBPACK_IMPORTED_MODULE_1__["NgModel"]));
      };

      ValidatorBase.ɵprov = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineInjectable"]({
        token: ValidatorBase,
        factory: ValidatorBase.ɵfac
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](ValidatorBase, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"]
        }], function () {
          return [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]
          }, {
            type: _angular_forms__WEBPACK_IMPORTED_MODULE_1__["NgModel"]
          }];
        }, {
          valueChanged: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"],
            args: ["blur", ["$event"]]
          }]
        });
      })();
      /***/

    },

    /***/
    "dpcW":
    /*!**********************************************!*\
      !*** ./src/core/codeshell/helpers/models.ts ***!
      \**********************************************/

    /*! exports provided: DTO, TmpFileData, Result, SubmitResult, DeleteResult, LoadResult, LoadResultGen, PropertyFilter, LoadOptions, NoteType */

    /***/
    function dpcW(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "DTO", function () {
        return DTO;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "TmpFileData", function () {
        return TmpFileData;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "Result", function () {
        return Result;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "SubmitResult", function () {
        return SubmitResult;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "DeleteResult", function () {
        return DeleteResult;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "LoadResult", function () {
        return LoadResult;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "LoadResultGen", function () {
        return LoadResultGen;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "PropertyFilter", function () {
        return PropertyFilter;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "LoadOptions", function () {
        return LoadOptions;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "NoteType", function () {
        return NoteType;
      });

      var DTO = function DTO() {
        _classCallCheck(this, DTO);

        this.entity = {};
      };

      var TmpFileData = function TmpFileData() {
        _classCallCheck(this, TmpFileData);
      };

      var Result = function Result() {
        _classCallCheck(this, Result);

        this.success = true;
        this.type = NoteType.Success;
      };

      var SubmitResult = /*#__PURE__*/function () {
        function SubmitResult() {
          _classCallCheck(this, SubmitResult);

          this.data = {};
          this.code = 0;
          this.message = "";
          this.exceptionMessage = "";
          this.stackTrace = [];
          this.affectedRows = 0;
        }

        _createClass(SubmitResult, null, [{
          key: "FromResponse",
          value: function FromResponse(r) {
            try {
              return Object.assign(new SubmitResult(), r.error);
            } catch (e) {
              return null;
            }
          }
        }]);

        return SubmitResult;
      }();

      var DeleteResult = /*#__PURE__*/function (_SubmitResult) {
        _inherits(DeleteResult, _SubmitResult);

        var _super15 = _createSuper(DeleteResult);

        function DeleteResult() {
          var _this37;

          _classCallCheck(this, DeleteResult);

          _this37 = _super15.apply(this, arguments);
          _this37.canDelete = false;
          _this37.tableName = null;
          return _this37;
        }

        return DeleteResult;
      }(SubmitResult);

      var LoadResult = function LoadResult() {
        _classCallCheck(this, LoadResult);

        this.totalCount = 0;
        this.list = [];
      };

      var LoadResultGen = function LoadResultGen() {
        _classCallCheck(this, LoadResultGen);

        this.list = [];
      };

      var PropertyFilter = function PropertyFilter() {
        _classCallCheck(this, PropertyFilter);

        this.MemberName = "";
        this.FilterType = "reference";
        this.Ids = [];
      };

      var LoadOptions = /*#__PURE__*/function () {
        function LoadOptions() {
          _classCallCheck(this, LoadOptions);

          this.Showing = 0;
          this.Skip = 0;
        }

        _createClass(LoadOptions, null, [{
          key: "AddFilter",
          value: function AddFilter(opts, fil) {
            var arr;

            if (!opts.Filters) {
              opts.Filters = "";
              arr = [];
            } else {
              var f = JSON.parse(opts.Filters);
              arr = Object.assign(new Array(), f);
            }

            arr.push(fil);
            opts.Filters = JSON.stringify(arr);
          }
        }]);

        return LoadOptions;
      }();

      var NoteType;

      (function (NoteType) {
        NoteType[NoteType["Success"] = 0] = "Success";
        NoteType[NoteType["Error"] = 1] = "Error";
        NoteType[NoteType["Warning"] = 2] = "Warning";
      })(NoteType || (NoteType = {}));
      /***/

    },

    /***/
    "eXOA":
    /*!************************************************!*\
      !*** ./src/core/codeshell/validators/index.ts ***!
      \************************************************/

    /*! exports provided: NumberRangeValidator, IsUnique, ModalValidator, DateValidator */

    /***/
    function eXOA(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony import */


      var _rangeValidator__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! ./rangeValidator */
      "9loO");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "NumberRangeValidator", function () {
        return _rangeValidator__WEBPACK_IMPORTED_MODULE_0__["NumberRangeValidator"];
      });
      /* harmony import */


      var _isUnique__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ./isUnique */
      "PwuP");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "IsUnique", function () {
        return _isUnique__WEBPACK_IMPORTED_MODULE_1__["IsUnique"];
      });
      /* harmony import */


      var _modalValidator__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! ./modalValidator */
      "62p0");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "ModalValidator", function () {
        return _modalValidator__WEBPACK_IMPORTED_MODULE_2__["ModalValidator"];
      });
      /* harmony import */


      var _dateValidator__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! ./dateValidator */
      "zYQP");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "DateValidator", function () {
        return _dateValidator__WEBPACK_IMPORTED_MODULE_3__["DateValidator"];
      });
      /***/

    },

    /***/
    "fYjR":
    /*!******************************************************!*\
      !*** ./src/core/codeshell/helpers/recursionModel.ts ***!
      \******************************************************/

    /*! exports provided: RecursionModel */

    /***/
    function fYjR(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "RecursionModel", function () {
        return RecursionModel;
      });

      var RecursionModel = /*#__PURE__*/function () {
        function RecursionModel() {
          _classCallCheck(this, RecursionModel);

          this.id = 0;
          this.name = "";
          this.children = [];
          this.editing = false;
          this.parentId = null;
          this.chain = "";
          this.nameChain = "";
          this.isExpanded = false;
        }

        _createClass(RecursionModel, null, [{
          key: "FromDB",
          value: function FromDB(item, lst) {
            var ret = Object.assign(new RecursionModel(), item);

            if (ret.children.length > 0) {
              for (var i in ret.children) {
                ret.children[i] = this.FromDB(ret.children[i]);
              }
            }

            return ret;
          }
        }]);

        return RecursionModel;
      }();
      /***/

    },

    /***/
    "gFuU":
    /*!***********************************************!*\
      !*** ./src/core/codeshell/security/models.ts ***!
      \***********************************************/

    /*! exports provided: ResourceActions, DomainDataProvider, DomainData, RouteData, UserDTO, LoginResult, TokenData, Permission */

    /***/
    function gFuU(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "ResourceActions", function () {
        return ResourceActions;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "DomainDataProvider", function () {
        return DomainDataProvider;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "DomainData", function () {
        return DomainData;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "RouteData", function () {
        return RouteData;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "UserDTO", function () {
        return UserDTO;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "LoginResult", function () {
        return LoginResult;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "TokenData", function () {
        return TokenData;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "Permission", function () {
        return Permission;
      });

      var ResourceActions;

      (function (ResourceActions) {
        ResourceActions[ResourceActions["view"] = 0] = "view";
        ResourceActions[ResourceActions["details"] = 1] = "details";
        ResourceActions[ResourceActions["update"] = 2] = "update";
        ResourceActions[ResourceActions["insert"] = 3] = "insert";
        ResourceActions[ResourceActions["delete"] = 4] = "delete";
      })(ResourceActions || (ResourceActions = {}));

      var DomainDataProvider = /*#__PURE__*/function () {
        function DomainDataProvider(domains) {
          _classCallCheck(this, DomainDataProvider);

          this.Domains = [];
          this.Domains = domains;
        }

        _createClass(DomainDataProvider, [{
          key: "GetByNavGroup",
          value: function GetByNavGroup(group, auth, user) {
            //var auth: AuthorizationServiceBase = Shell.Injector.get(AuthorizationServiceBase);
            var navs = [];
            var gr = this.Domains.find(function (e) {
              return e.name == group;
            });

            if (gr) {
              var _iterator12 = _createForOfIteratorHelper(gr.children),
                  _step12;

              try {
                for (_iterator12.s(); !(_step12 = _iterator12.n()).done;) {
                  var c = _step12.value;
                  var r = Object.assign(new RouteData(), c);
                  var isAuthorized = auth ? auth.IsAuthorized(user, r) : true;

                  if (isAuthorized && r.url) {
                    var item = {
                      name: r.name,
                      url: r.url
                    };
                    navs.push(item);
                  }
                }
              } catch (err) {
                _iterator12.e(err);
              } finally {
                _iterator12.f();
              }
            }

            return navs;
          }
        }]);

        return DomainDataProvider;
      }();

      var DomainData = function DomainData() {
        _classCallCheck(this, DomainData);

        this.name = "";
        this.children = [];
      };

      var RouteData = /*#__PURE__*/function () {
        function RouteData() {
          _classCallCheck(this, RouteData);

          this.name = "";
          this.navigate = false;
          this.resource = "";
          this.action = ResourceActions.view;
        }

        _createClass(RouteData, [{
          key: "IsAnonymous",
          get: function get() {
            return this.action == "anonymous";
          }
        }, {
          key: "AllowAll",
          get: function get() {
            return this.action == "allowAll";
          }
        }]);

        return RouteData;
      }();

      var UserDTO = function UserDTO() {
        _classCallCheck(this, UserDTO);

        this.id = 0;
        this.userId = "";
        this.tenantCode = "";
        this.name = "";
        this.logonName = "";
        this.userTypeString = "";
        this.apps = [];
        this.permissions = {};
        this.entityLinks = {};
      };

      var LoginResult = function LoginResult() {
        _classCallCheck(this, LoginResult);

        this.success = false;
        this.message = "";
        this.userData = new UserDTO();
        this.token = "";
        this.tokenExpiry = new Date();
      };

      var TokenData = /*#__PURE__*/function () {
        function TokenData() {
          _classCallCheck(this, TokenData);

          this.Token = "";
          this.Expiry = new Date();
        }

        _createClass(TokenData, [{
          key: "IsExpired",
          value: function IsExpired() {
            return new Date() > new Date(this.Expiry);
          }
        }]);

        return TokenData;
      }();

      var Permission = /*#__PURE__*/function () {
        function Permission() {
          _classCallCheck(this, Permission);

          this.insert = false;
          this.update = false;
          this["delete"] = false;
          this.details = false;
          this.view = true;
          this.actions = [];
        }

        _createClass(Permission, [{
          key: "canSubmit",
          get: function get() {
            return this.insert || this.update || this["delete"];
          }
        }, {
          key: "Can",
          value: function Can(ac) {
            if (ac == "anonymous" || ac == "allowAll") return true;

            switch (ac) {
              case 4:
                return this["delete"];

              case 2:
                return this.update;

              case 3:
                return this.insert;

              case 1:
                return this.details;

              case 0:
                return this.view;

              default:
                if (this.actions == null) return false;
                return this.actions.indexOf(ac) > -1;
            }
          }
        }], [{
          key: "Anonymous",
          get: function get() {
            return Object.assign(new Permission(), {
              insert: true,
              update: false,
              "delete": false,
              details: false,
              view: true
            });
          }
        }, {
          key: "Denied",
          get: function get() {
            return Object.assign(new Permission(), {
              insert: false,
              update: false,
              "delete": false,
              details: false,
              view: false
            });
          }
        }, {
          key: "FullAccess",
          get: function get() {
            return Object.assign(new Permission(), {
              insert: true,
              update: true,
              "delete": true,
              details: true,
              view: true
            });
          }
        }]);

        return Permission;
      }();
      /***/

    },

    /***/
    "gyvI":
    /*!*****************************************************!*\
      !*** ./src/core/codeshell/base-components/index.ts ***!
      \*****************************************************/

    /*! exports provided: IAppComponent, BaseComponent, DTOEditComponentBase, EditComponentBase, ListComponentBase, LoginBase, NavigationSideBarBase, SectionedEditComponentBase, SelectComponentBase, TopBarBase, ExpandedItems, TreeComponentBase */

    /***/
    function gyvI(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony import */


      var _appComponentBase__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! ./appComponentBase */
      "Q6pc");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "IAppComponent", function () {
        return _appComponentBase__WEBPACK_IMPORTED_MODULE_0__["IAppComponent"];
      });
      /* harmony import */


      var _baseComponent__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ./baseComponent */
      "I5Ck");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "BaseComponent", function () {
        return _baseComponent__WEBPACK_IMPORTED_MODULE_1__["BaseComponent"];
      });
      /* harmony import */


      var _dto_edit_component_base__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! ./dto-edit-component-base */
      "Ur/x");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "DTOEditComponentBase", function () {
        return _dto_edit_component_base__WEBPACK_IMPORTED_MODULE_2__["DTOEditComponentBase"];
      });
      /* harmony import */


      var _editComponentBase__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! ./editComponentBase */
      "G778");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "EditComponentBase", function () {
        return _editComponentBase__WEBPACK_IMPORTED_MODULE_3__["EditComponentBase"];
      });
      /* harmony import */


      var _listComponentBase__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
      /*! ./listComponentBase */
      "sVHn");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "ListComponentBase", function () {
        return _listComponentBase__WEBPACK_IMPORTED_MODULE_4__["ListComponentBase"];
      });
      /* harmony import */


      var _loginBase__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(
      /*! ./loginBase */
      "rCrt");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "LoginBase", function () {
        return _loginBase__WEBPACK_IMPORTED_MODULE_5__["LoginBase"];
      });
      /* harmony import */


      var _navigation_side_bar_base__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(
      /*! ./navigation-side-bar-base */
      "+K43");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "NavigationSideBarBase", function () {
        return _navigation_side_bar_base__WEBPACK_IMPORTED_MODULE_6__["NavigationSideBarBase"];
      });
      /* harmony import */


      var _sectionedEditComponentBase__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(
      /*! ./sectionedEditComponentBase */
      "u9rM");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "SectionedEditComponentBase", function () {
        return _sectionedEditComponentBase__WEBPACK_IMPORTED_MODULE_7__["SectionedEditComponentBase"];
      });
      /* harmony import */


      var _selectComponentBase__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(
      /*! ./selectComponentBase */
      "6mC2");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "SelectComponentBase", function () {
        return _selectComponentBase__WEBPACK_IMPORTED_MODULE_8__["SelectComponentBase"];
      });
      /* harmony import */


      var _topBarBase__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(
      /*! ./topBarBase */
      "VZL9");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "TopBarBase", function () {
        return _topBarBase__WEBPACK_IMPORTED_MODULE_9__["TopBarBase"];
      });
      /* harmony import */


      var _treeComponentBase__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(
      /*! ./treeComponentBase */
      "n73x");
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "ExpandedItems", function () {
        return _treeComponentBase__WEBPACK_IMPORTED_MODULE_10__["ExpandedItems"];
      });
      /* harmony reexport (safe) */


      __webpack_require__.d(__webpack_exports__, "TreeComponentBase", function () {
        return _treeComponentBase__WEBPACK_IMPORTED_MODULE_10__["TreeComponentBase"];
      });
      /***/

    },

    /***/
    "iLnC":
    /*!******************************************************!*\
      !*** ./src/core/codeshell/http/entityHttpService.ts ***!
      \******************************************************/

    /*! exports provided: EntityHttpService */

    /***/
    function iLnC(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "EntityHttpService", function () {
        return EntityHttpService;
      });
      /* harmony import */


      var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! tslib */
      "mrSG");
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var _httpRequest__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! ./httpRequest */
      "vyk5");
      /* harmony import */


      var _httpServiceBase__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! ./httpServiceBase */
      "LR6F");
      /* harmony import */


      var _helpers__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
      /*! ../helpers */
      "GYDu");

      var EntityHttpService = /*#__PURE__*/function (_httpServiceBase__WEB) {
        _inherits(EntityHttpService, _httpServiceBase__WEB);

        var _super16 = _createSuper(EntityHttpService);

        function EntityHttpService() {
          _classCallCheck(this, EntityHttpService);

          return _super16.apply(this, arguments);
        }

        _createClass(EntityHttpService, [{
          key: "IsUnique",
          value: function IsUnique(property, id, value) {
            var req = this.InitializeRequest("IsUnique", {
              Property: property,
              Id: id ? id : 0,
              Value: value
            });
            return this.processAs(_httpRequest__WEBPACK_IMPORTED_MODULE_2__["Methods"].Get, req);
          }
        }, {
          key: "GetEditLookups",
          value: function GetEditLookups(opt) {
            return this.Get("edit-lookups", opt);
          }
        }, {
          key: "GetListLookups",
          value: function GetListLookups(opt) {
            return this.Get("list-lookups", opt);
          }
        }, {
          key: "SetActive",
          value: function SetActive(id, state) {
            return this.Update("SetActive", {
              id: id,
              stateBool: state
            });
          }
        }, {
          key: "GetSingle",
          value: function GetSingle(id) {
            return this.GetAs(id.toString());
          }
        }, {
          key: "GetPaged",
          value: function GetPaged(action, opts) {
            var req = this.InitializeRequest(action, opts);
            return this.processAs(_httpRequest__WEBPACK_IMPORTED_MODULE_2__["Methods"].Get, req);
          }
        }, {
          key: "Save",
          value: function Save(action, body, params) {
            var req = this.InitializeRequest(action, params, body);
            return this.processAs(_httpRequest__WEBPACK_IMPORTED_MODULE_2__["Methods"].Post, req);
          }
        }, {
          key: "Update",
          value: function Update(action, body, params) {
            var req = this.InitializeRequest(action, params, body);
            return this.processAs(_httpRequest__WEBPACK_IMPORTED_MODULE_2__["Methods"].Put, req);
          }
        }, {
          key: "AttemptDelete",
          value: function AttemptDelete(id) {
            var req = this.InitializeRequest("Delete", id);
            return this.processAs(_httpRequest__WEBPACK_IMPORTED_MODULE_2__["Methods"].Delete, req);
          }
        }, {
          key: "GetLocalizationData",
          value: function GetLocalizationData(id) {
            return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, /*#__PURE__*/regeneratorRuntime.mark(function _callee14() {
              var data, i;
              return regeneratorRuntime.wrap(function _callee14$(_context14) {
                while (1) {
                  switch (_context14.prev = _context14.next) {
                    case 0:
                      _context14.next = 2;
                      return this.GetAs("GetLocalizationData/" + id);

                    case 2:
                      data = _context14.sent;

                      for (i in data) {
                        data[i] = _helpers__WEBPACK_IMPORTED_MODULE_4__["ListItem"].FromDB_GEN(_helpers__WEBPACK_IMPORTED_MODULE_4__["LocalizablesDTO"], data[i]);
                      }

                      return _context14.abrupt("return", data);

                    case 5:
                    case "end":
                      return _context14.stop();
                  }
                }
              }, _callee14, this);
            }));
          }
        }, {
          key: "SetLocalizationData",
          value: function SetLocalizationData(id, data) {
            return this.PostAs("SetLocalizationData/" + id, data);
          }
        }]);

        return EntityHttpService;
      }(_httpServiceBase__WEBPACK_IMPORTED_MODULE_3__["HttpServiceBase"]);

      EntityHttpService.ɵfac = function EntityHttpService_Factory(t) {
        return ɵEntityHttpService_BaseFactory(t || EntityHttpService);
      };

      EntityHttpService.ɵprov = _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdefineInjectable"]({
        token: EntityHttpService,
        factory: EntityHttpService.ɵfac
      });

      var ɵEntityHttpService_BaseFactory = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵgetInheritedFactory"](EntityHttpService);
      /*@__PURE__*/


      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵsetClassMetadata"](EntityHttpService, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"]
        }], null, null);
      })();
      /***/

    },

    /***/
    "j3l2":
    /*!*********************************************************************!*\
      !*** ./src/core/codeshell/directives/component-loader.directive.ts ***!
      \*********************************************************************/

    /*! exports provided: ComponentLoader */

    /***/
    function j3l2(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "ComponentLoader", function () {
        return ComponentLoader;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");

      var ComponentLoader = /*#__PURE__*/function () {
        function ComponentLoader(Container, inj, FactoryResolver) {
          _classCallCheck(this, ComponentLoader);

          this.Container = Container;
          this.inj = inj;
          this.FactoryResolver = FactoryResolver;
        }

        _createClass(ComponentLoader, [{
          key: "CreateComponent",
          value: function CreateComponent(e) {
            var fac = this.FactoryResolver.resolveComponentFactory(e);
            var ref = fac.create(this.inj);
            return ref;
          }
        }, {
          key: "UseComponent",
          value: function UseComponent(ref) {
            if (this.Container.indexOf(ref.hostView) == -1) {
              this.Container.insert(ref.hostView);
            }
          }
        }, {
          key: "Clear",
          value: function Clear() {
            this.Container.clear();
          }
        }]);

        return ComponentLoader;
      }();

      ComponentLoader.ɵfac = function ComponentLoader_Factory(t) {
        return new (t || ComponentLoader)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ViewContainerRef"]), _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injector"]), _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ComponentFactoryResolver"]));
      };

      ComponentLoader.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({
        type: ComponentLoader,
        selectors: [["ng-template", "acs-component-loader", ""]],
        exportAs: ["componentLoader"]
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](ComponentLoader, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
          args: [{
            selector: "ng-template[acs-component-loader]",
            exportAs: "componentLoader"
          }]
        }], function () {
          return [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ViewContainerRef"]
          }, {
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Injector"]
          }, {
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ComponentFactoryResolver"]
          }];
        }, null);
      })();
      /***/

    },

    /***/
    "jBBj":
    /*!**************************************************!*\
      !*** ./src/core/codeshell/helpers/viewParams.ts ***!
      \**************************************************/

    /*! exports provided: ViewParams */

    /***/
    function jBBj(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "ViewParams", function () {
        return ViewParams;
      });

      var ViewParams = function ViewParams() {
        _classCallCheck(this, ViewParams);

        this.Other = {};
      };
      /***/

    },

    /***/
    "mHXC":
    /*!***********************************************!*\
      !*** ./src/core/codeshell/services/stored.ts ***!
      \***********************************************/

    /*! exports provided: Stored */

    /***/
    function mHXC(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "Stored", function () {
        return Stored;
      });

      var Stored = /*#__PURE__*/function () {
        function Stored() {
          _classCallCheck(this, Stored);
        }

        _createClass(Stored, null, [{
          key: "Set",
          value: function Set(index, item) {
            var s = JSON.stringify(item);
            localStorage.setItem(index, s);
          }
        }, {
          key: "Set_SS",
          value: function Set_SS(index, item) {
            var s = JSON.stringify(item);
            sessionStorage.setItem(index, s);
          }
        }, {
          key: "Clear",
          value: function Clear(index) {
            localStorage.removeItem(index);
          }
        }, {
          key: "Clear_SS",
          value: function Clear_SS(index) {
            sessionStorage.removeItem(index);
          }
        }, {
          key: "Get",
          value: function Get(index, exp) {
            var item = localStorage.getItem(index);
            if (item == undefined || item == null) return null;

            try {
              var d = new exp();
              var ob = JSON.parse(item);
              Object.assign(d, ob);
              return d;
            } catch (e) {
              return null;
            }
          }
        }, {
          key: "Get_SS",
          value: function Get_SS(index, exp) {
            var item = sessionStorage.getItem(index);
            if (item == undefined || item == null) return null;

            try {
              var d = new exp();
              var ob = JSON.parse(item);
              Object.assign(d, ob);
              return d;
            } catch (e) {
              return null;
            }
          }
        }]);

        return Stored;
      }();
      /***/

    },

    /***/
    "n2jX":
    /*!******************************************************************!*\
      !*** ./src/core/codeshell/directives/direction-fix.directive.ts ***!
      \******************************************************************/

    /*! exports provided: DirctionFix */

    /***/
    function n2jX(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "DirctionFix", function () {
        return DirctionFix;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");

      var DirctionFix = /*#__PURE__*/function () {
        function DirctionFix(el) {
          _classCallCheck(this, DirctionFix);

          this.el = el;
        }

        _createClass(DirctionFix, [{
          key: "Element",
          get: function get() {
            return this.el.nativeElement;
          }
        }, {
          key: "Change",
          value: function Change() {}
        }, {
          key: "ngOnInit",
          value: function ngOnInit() {
            var _this38 = this;

            setTimeout(function () {
              _this38.setDirection();
            }, 500);
          }
        }, {
          key: "setDirection",
          value: function setDirection() {
            var arabic = /[\u0600-\u06FF]/;
            this.Element.style.display = "inline-block";

            if (arabic.test(this.Element.innerText)) {
              this.Element.style.direction = "rtl";
            } else {
              this.Element.style.direction = "ltr";
            }
          }
        }]);

        return DirctionFix;
      }();

      DirctionFix.ɵfac = function DirctionFix_Factory(t) {
        return new (t || DirctionFix)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]));
      };

      DirctionFix.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({
        type: DirctionFix,
        selectors: [["", "rtl-fix", ""]],
        hostBindings: function DirctionFix_HostBindings(rf, ctx) {
          if (rf & 1) {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("textContent", function DirctionFix_textContent_HostBindingHandler() {
              return ctx.Change();
            });
          }
        },
        exportAs: ["rtl-fix"]
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](DirctionFix, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
          args: [{
            selector: "[rtl-fix]",
            exportAs: "rtl-fix"
          }]
        }], function () {
          return [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]
          }];
        }, {
          Change: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"],
            args: ["textContent"]
          }]
        });
      })();
      /***/

    },

    /***/
    "n73x":
    /*!*****************************************************************!*\
      !*** ./src/core/codeshell/base-components/treeComponentBase.ts ***!
      \*****************************************************************/

    /*! exports provided: ExpandedItems, TreeComponentBase */

    /***/
    function n73x(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "ExpandedItems", function () {
        return ExpandedItems;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "TreeComponentBase", function () {
        return TreeComponentBase;
      });
      /* harmony import */


      var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! tslib */
      "mrSG");
      /* harmony import */


      var _baseComponent__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ./baseComponent */
      "I5Ck");
      /* harmony import */


      var codeshell_helpers__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! codeshell/helpers */
      "GYDu");
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var angular_tree_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
      /*! angular-tree-component */
      "rDsv");
      /* harmony import */


      var _shell__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(
      /*! ../shell */
      "UoIT");
      /* harmony import */


      var _utilities_utils__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(
      /*! ../utilities/utils */
      "VXkE");
      /* harmony import */


      var _helpers_treeEventArgs__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(
      /*! ../helpers/treeEventArgs */
      "6XeC");

      var ExpandedItems = function ExpandedItems() {
        _classCallCheck(this, ExpandedItems);

        this.Items = [];
      };

      var TreeComponentBase = /*#__PURE__*/function (_baseComponent__WEBPA3) {
        _inherits(TreeComponentBase, _baseComponent__WEBPA3);

        var _super17 = _createSuper(TreeComponentBase);

        function TreeComponentBase() {
          var _this39;

          _classCallCheck(this, TreeComponentBase);

          _this39 = _super17.apply(this, arguments);
          _this39.LoadOnStart = true;
          _this39.RouteParams = {};
          _this39.ignore = false;
          _this39._loadedOnce = false;
          _this39.model = {};
          _this39.List = [];
          _this39.IsNew = true;
          _this39.AllowEdit = true;
          _this39.AllowMove = true;
          _this39.ShowOkButton = true;
          _this39.UseContentCounts = false;
          _this39.UseInTreeTextBoxes = true;
          _this39.selectedId = 0;
          _this39.OnTreeEvent = new _angular_core__WEBPACK_IMPORTED_MODULE_3__["EventEmitter"]();
          _this39.OnUnassigned = new _angular_core__WEBPACK_IMPORTED_MODULE_3__["EventEmitter"]();
          _this39.OnTreeLoadedEvet = new _angular_core__WEBPACK_IMPORTED_MODULE_3__["EventEmitter"]();
          _this39.treeOptions = {
            allowDrag: function allowDrag(node) {
              return _this39.AllowEdit && _this39.AllowMove;
            },
            allowDrop: function allowDrop(node) {
              return _this39.AllowEdit && _this39.AllowMove;
            }
          };

          _this39.OnTreeLoaded = function () {};

          return _this39;
        }

        _createClass(TreeComponentBase, [{
          key: "UseCheckBoxes",
          get: function get() {
            return this.treeOptions.useCheckbox == true;
          },
          set: function set(val) {
            this.treeOptions.useCheckbox = val;
          }
        }, {
          key: "Expanded",
          get: function get() {
            if (!this._expanded) {
              var x = codeshell_helpers__WEBPACK_IMPORTED_MODULE_2__["Stored"].Get(this.ComponentName + "_Expanded", ExpandedItems);
              if (x == null) this._expanded = [];else this._expanded = x.Items;
            }

            return this._expanded;
          }
        }, {
          key: "ngOnInit",
          value: function ngOnInit() {
            var _this40 = this;

            _get(_getPrototypeOf(TreeComponentBase.prototype), "ngOnInit", this).call(this);

            var opts = this.GetLookupOptions();
            if (this.treeComponent) this.treeComponent.event.subscribe(function (e) {
              return _this40.OnEvent(e);
            });

            if (opts != null) {
              this.LoadLookupsAsync(opts).then(function (l) {
                _this40.Lookups = l;

                _this40.OnReady();
              });
            } else {
              this.OnReady();
            }
          }
        }, {
          key: "OnEvent",
          value: function OnEvent(ev) {
            if (!ev.node) return;
            this.OnTreeEvent.emit(new _helpers_treeEventArgs__WEBPACK_IMPORTED_MODULE_7__["TreeEventArgs"](ev.eventName, ev.node));
            if (ev.eventName == "focus") this.setClickedRow(ev.node);

            if (this.UseCheckBoxes && ev.eventName == "select") {
              this.OnCheckBoxSelect(ev.node);
            } else if (this.UseCheckBoxes && ev.eventName == "deselect") {
              this.OnCheckBoxDeselect(ev.node);
            }
          }
        }, {
          key: "SetSelected",
          value: function SetSelected(id) {
            if (this.treeComponent) {
              var n = this.treeComponent.treeModel.getNodeById(id);

              if (n) {
                n.focus();
              }
            }
          }
        }, {
          key: "ForceReload",
          value: function ForceReload() {
            if (this._loadedOnce) this._loadedOnce = false;
          }
        }, {
          key: "LoadLookupsAsync",
          value: function LoadLookupsAsync(opts) {
            return this.Service.Get("GetListLookups", opts);
          }
        }, {
          key: "OnReady",
          value: function OnReady() {
            if (!this.IsEmbedded && this.LoadOnStart) this.LoadData();
          }
        }, {
          key: "StartAsync",
          value: function StartAsync() {
            return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, /*#__PURE__*/regeneratorRuntime.mark(function _callee15() {
              var lst;
              return regeneratorRuntime.wrap(function _callee15$(_context15) {
                while (1) {
                  switch (_context15.prev = _context15.next) {
                    case 0:
                      if (this._loadedOnce) {
                        _context15.next = 6;
                        break;
                      }

                      _context15.next = 3;
                      return this.LoadDataPromise();

                    case 3:
                      lst = _context15.sent;
                      this.AfterLoad(lst);
                      this._loadedOnce = true;

                    case 6:
                      _context15.next = 8;
                      return this.ExpandSavedAsync();

                    case 8:
                      return _context15.abrupt("return", this);

                    case 9:
                    case "end":
                      return _context15.stop();
                  }
                }
              }, _callee15, this);
            }));
          }
        }, {
          key: "OnCheckBoxSelect",
          value: function OnCheckBoxSelect(node) {}
        }, {
          key: "OnCheckBoxDeselect",
          value: function OnCheckBoxDeselect(nods) {}
        }, {
          key: "OnSelected",
          value: function OnSelected(item) {}
        }, {
          key: "LoadDataPromise",
          value: function LoadDataPromise() {
            this.List = [];
            if (this.Loader) return this.Loader();
            return this.Service.Get("tree");
          }
        }, {
          key: "ContentCountAsync",
          value: function ContentCountAsync() {
            if (this.CountLoader) {
              return this.CountLoader();
            }

            return Promise.resolve({});
          }
        }, {
          key: "ShowCheckBoxes",
          value: function ShowCheckBoxes() {
            this.treeOptions.useCheckbox = true;
          }
        }, {
          key: "ClearSelection",
          value: function ClearSelection() {
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

            if (this.OnSelectedEvent) this.OnSelectedEvent(null);
          }
        }, {
          key: "AfterLoad",
          value: function AfterLoad(lst) {
            this.List = [];

            var _iterator13 = _createForOfIteratorHelper(lst),
                _step13;

            try {
              for (_iterator13.s(); !(_step13 = _iterator13.n()).done;) {
                var i = _step13.value;
                this.List.push(codeshell_helpers__WEBPACK_IMPORTED_MODULE_2__["RecursionModel"].FromDB(i));
              }
            } catch (err) {
              _iterator13.e(err);
            } finally {
              _iterator13.f();
            }

            if (this.UseContentCounts) this.LoadCounts();
            if (this.OnLoadedEvent) this.OnLoadedEvent(this.List);
            if (this.treeComponent) this.treeComponent.treeModel.update();
            this.onTreeInit();
          }
        }, {
          key: "LoadData",
          value: function LoadData() {
            var _this41 = this;

            this.List = [];
            this.LoadDataPromise().then(function (e) {
              _this41.AfterLoad(e);

              _this41.ExpandSavedAsync();
            });
          }
        }, {
          key: "LoadCounts",
          value: function LoadCounts() {
            var _this42 = this;

            Object(_utilities_utils__WEBPACK_IMPORTED_MODULE_6__["List_RunRecursively"])(this.List, function (d) {
              d.contentCount = 0;
              d.hasContents = false;
            });
            this.ContentCountAsync().then(function (res) {
              for (var i in res) {
                var item = Object(_utilities_utils__WEBPACK_IMPORTED_MODULE_6__["List_FindIdRecusive"])(_this42.List, parseInt(i));

                if (item) {
                  item.hasContents = true;
                  item.contentCount = res[i];
                }
              }
            });
          }
        }, {
          key: "ExpandSavedAsync",
          value: function ExpandSavedAsync() {
            var _this43 = this;

            return new Promise(function (res, rej) {
              setTimeout(function () {
                _this43.ignore = true;

                if (_this43.treeComponent) {
                  var _iterator14 = _createForOfIteratorHelper(_this43.Expanded),
                      _step14;

                  try {
                    for (_iterator14.s(); !(_step14 = _iterator14.n()).done;) {
                      var id = _step14.value;
                      if (id == null) continue;

                      var node = _this43.treeComponent.treeModel.getNodeById(id);

                      if (node) node.expand();
                    }
                  } catch (err) {
                    _iterator14.e(err);
                  } finally {
                    _iterator14.f();
                  }
                }

                _this43.ignore = false;
                res();
              }, 500);
            });
          }
        }, {
          key: "onTreeInit",
          value: function onTreeInit() {
            var _this44 = this;

            setTimeout(function () {
              _this44.OnTreeLoaded();
            }, 300);
          }
        }, {
          key: "onExpanded",
          value: function onExpanded(event) {
            if (this.ignore) return;
            var item = event.node.data;
            if (event.isExpanded) this.Expanded.push(item.id);else {
              Object(_utilities_utils__WEBPACK_IMPORTED_MODULE_6__["List_RemoveItem"])(this.Expanded, item.id);
            }
            this.SaveExpanded();
          }
        }, {
          key: "setClickedRow",
          value: function setClickedRow(node) {
            var index = node.data;
            if (this.selectedId == index.id) return;
            this.selectedId = index.id;
            this.selectedNode = node;
            this.selectedItem = index;
            this.OnSelected(index);
            if (this.OnSelectedEvent) this.OnSelectedEvent(index);
          }
        }, {
          key: "OnDisplayNode",
          value: function OnDisplayNode() {}
        }, {
          key: "InitializeModel",
          value: function InitializeModel(parent) {
            return new codeshell_helpers__WEBPACK_IMPORTED_MODULE_2__["RecursionModel"]();
          }
        }, {
          key: "onMoveNode",
          value: function onMoveNode(ev) {
            var _this45 = this;

            var moved = ev.node;
            var loc = ev.to.parent;
            if (loc.virtual) moved.parentId = null;else moved.parentId = loc.id;
            var m = Object.assign({}, moved);
            m.children = [];
            this.Service.Put("Put", m)["catch"](function (e) {
              return _this45.LoadData();
            });
          }
        }, {
          key: "AddWithModalAsync",
          value: function AddWithModalAsync(parentNode) {
            var _this46 = this;

            return new Promise(function (resolve, reject) {
              if (_this46.editingModalRequest) {
                var req = _this46.editingModalRequest;

                _this46.GetComponent(req).then(function (comp) {
                  var model = _this46.InitializeModel(parentNode);

                  if (parentNode) model.parentId = parentNode.data.id;
                  comp.IsNew = true;
                  comp.BindAsync(model).then(function (d) {
                    comp.Show = true;
                  });

                  comp.DataSubmitted = function (model, res) {
                    if (comp.UseLocalization) _this46.LoadData();else resolve({
                      model: model,
                      result: res
                    });
                    comp.Show = false;
                  };
                })["catch"](function (e) {
                  return reject(e);
                });
              } else {
                reject("no Editing Component");
              }
            });
          }
        }, {
          key: "EditWithModalAsync",
          value: function EditWithModalAsync(node) {
            var _this47 = this;

            return new Promise(function (resolve, reject) {
              if (_this47.editingModalRequest) {
                var req = _this47.editingModalRequest;

                _this47.GetComponent(req).then(function (comp) {
                  var model = node.data;
                  if (node) model.parentId = node.data.id;
                  comp.FillAsync(model.id).then(function (d) {
                    comp.Show = true;
                  });

                  comp.DataSubmitted = function (model, res) {
                    if (comp.UseLocalization) _this47.LoadData();else resolve({
                      model: model,
                      result: res
                    });
                    comp.Show = false;
                  };
                })["catch"](function (e) {
                  return reject(e);
                });
              } else {
                reject("no Editing Component");
              }
            });
          }
        }, {
          key: "EditWithTreeBoxAsync",
          value: function EditWithTreeBoxAsync(node) {
            var _this48 = this;

            return new Promise(function (resolve, reject) {
              if (_this48.treeComponent) {
                var model = node.data;
                model.editing = true;

                _this48.treeComponent.treeModel.virtualScroll.scrollIntoView(node, true);

                setTimeout(function () {
                  var el = document.getElementById('editor' + model.id);

                  if (el) {
                    el.addEventListener("keyup", function (ev) {
                      if (ev.key == "Enter") {
                        model.editing = false;

                        if (model.name && model.name.length > 1) {
                          var ch = model.children;
                          model.children = [];

                          _this48.SubmitUpdateAsync(model).then(function (res) {
                            model.children = ch;
                            resolve({
                              model: model,
                              result: res
                            });
                          })["catch"](function (e) {
                            model.editing = false;
                          });
                        }
                      }
                    });
                    el.addEventListener("blur", function (ev) {
                      model.editing = false;
                      reject("blurred");
                    });
                    el.focus();
                  }
                }, 400);
              }
            });
          }
        }, {
          key: "AddWithTreeBoxAsync",
          value: function AddWithTreeBoxAsync(addTo) {
            var _this49 = this;

            return new Promise(function (resolve, reject) {
              if (_this49.treeComponent) {
                var model = _this49.InitializeModel(addTo);

                if (addTo) model.parentId = addTo.data.id;
                model.editing = true;
                addTo = addTo ? addTo : _this49.treeComponent.treeModel.virtualRoot;

                var nNode = _this49.AppendToNode(addTo, model, true);

                addTo.expand();

                _this49.treeComponent.treeModel.virtualScroll.scrollIntoView(nNode, true);

                setTimeout(function () {
                  var el = document.getElementById('editor0');

                  if (el) {
                    el.addEventListener("keyup", function (ev) {
                      if (ev.key == "Enter") {
                        model.editing = false;

                        if (model.name && model.name.length > 1) {
                          _this49.SubmitNewAsync(model).then(function (res) {
                            resolve({
                              model: model,
                              result: res
                            });
                          })["catch"](function (e) {
                            _utilities_utils__WEBPACK_IMPORTED_MODULE_6__["Utils"].HandleError(e, true);

                            _this49.RemoveFromNode(addTo, model);

                            reject(e);
                          });
                        } else {
                          _this49.RemoveFromNode(addTo, model);

                          reject("blurred");
                        }
                      }
                    });
                    el.addEventListener("blur", function (ev) {
                      if (model.editing) _this49.RemoveFromNode(addTo, model);
                      reject("blurred");
                    });
                    el.focus();
                  }
                }, 400);
              }
            });
          }
        }, {
          key: "AfterAddSubmit",
          value: function AfterAddSubmit(parentNode, model, res) {
            if (this.treeComponent) {
              parentNode = parentNode ? parentNode : this.treeComponent.treeModel.virtualRoot;

              if (res.data.Id) {
                model.id = res.data.Id;
              }

              this.AppendToNode(parentNode, model);
            }
          }
        }, {
          key: "AfterEditSubmit",
          value: function AfterEditSubmit(node, changedModel) {
            var children = node.data.children;
            var lst = node.parent.data.children;
            var ind = lst.indexOf(node.data);
            changedModel.children = children;
            lst[ind] = changedModel;
            if (this.treeComponent) this.treeComponent.treeModel.update();
          }
        }, {
          key: "AddNode",
          value: function AddNode(parentNode) {
            var _this50 = this;

            if (!this.treeComponent) return;

            if (this.AddFunction) {
              this.AddFunction(parentNode).then(function (d) {
                return _this50.AfterAddSubmit(parentNode, d.model, d.result);
              });
            } else if (this.editingModalRequest) {
              this.AddWithModalAsync(parentNode).then(function (d) {
                return _this50.AfterAddSubmit(parentNode, d.model, d.result);
              });
            } else {
              this.AddWithTreeBoxAsync(parentNode).then(function (d) {
                if (d.result.data.Id) d.model.id = d.result.data.Id;
              })["catch"](function (e) {});
            }
          }
        }, {
          key: "EditNode",
          value: function EditNode(obj) {
            var _this51 = this;

            if (!this.treeComponent) return;

            if (this.EditFunction) {
              this.EditFunction(obj).then(function (d) {
                return _this51.AfterEditSubmit(obj, d.model);
              });
            } else if (this.editingModalRequest) {
              this.EditWithModalAsync(obj).then(function (d) {
                return _this51.AfterEditSubmit(obj, d.model);
              });
            } else {
              this.EditWithTreeBoxAsync(obj).then(function (d) {})["catch"](function (e) {});
            }
          }
        }, {
          key: "DeleteAsync",
          value: function DeleteAsync(item) {
            return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, /*#__PURE__*/regeneratorRuntime.mark(function _callee16() {
              var c;
              return regeneratorRuntime.wrap(function _callee16$(_context16) {
                while (1) {
                  switch (_context16.prev = _context16.next) {
                    case 0:
                      _context16.next = 2;
                      return _shell__WEBPACK_IMPORTED_MODULE_5__["Shell"].Main.ShowDeleteConfirm();

                    case 2:
                      c = _context16.sent;

                      if (c) {
                        _context16.next = 5;
                        break;
                      }

                      return _context16.abrupt("return", Promise.reject("user rejected"));

                    case 5:
                      _context16.next = 7;
                      return this.Service.AttemptDelete(item);

                    case 7:
                      return _context16.abrupt("return", _context16.sent);

                    case 8:
                    case "end":
                      return _context16.stop();
                  }
                }
              }, _callee16, this);
            }));
          }
        }, {
          key: "DeleteNode",
          value: function DeleteNode(node) {
            var _this52 = this;

            var item = node.data;
            this.DeleteAsync(item).then(function (d) {
              var parentModel = node.parent.data;
              var i = parentModel.children.indexOf(item);
              if (i > -1) parentModel.children.splice(i, 1);
              if (_this52.treeComponent) _this52.treeComponent.treeModel.update();
            })["catch"](function (d) {
              _utilities_utils__WEBPACK_IMPORTED_MODULE_6__["Utils"].HandleError(d, true, true);
            });
          }
        }, {
          key: "SaveExpanded",
          value: function SaveExpanded() {
            codeshell_helpers__WEBPACK_IMPORTED_MODULE_2__["Stored"].Set(this.ComponentName + "_Expanded", {
              Items: this.Expanded
            });
          }
        }, {
          key: "RemoveFromNode",
          value: function RemoveFromNode(from, model) {
            if (this.treeComponent) {
              from = from ? from : this.treeComponent.treeModel.virtualRoot;
              var lst = from.data.children;
              Object(_utilities_utils__WEBPACK_IMPORTED_MODULE_6__["List_RemoveItem"])(lst, model);
              if (this.treeComponent) this.treeComponent.treeModel.update();
            }
          }
        }, {
          key: "AppendToNode",
          value: function AppendToNode(addTo, model) {
            var top = arguments.length > 2 && arguments[2] !== undefined ? arguments[2] : false;

            if (this.treeComponent) {
              var parentData = addTo.data.children;
              var newNode = new angular_tree_component__WEBPACK_IMPORTED_MODULE_4__["TreeNode"](model, addTo, this.treeComponent.treeModel, 0);
              if (top) addTo.children.unshift(newNode);else addTo.children.push(newNode);
              parentData.push(model);
              addTo.expand();
              this.treeComponent.treeModel.update();
              return newNode;
            }

            throw "no tree component";
          }
        }, {
          key: "SelectItemAsync",
          value: function SelectItemAsync() {
            var _this53 = this;

            return new Promise(function (res, rej) {
              _this53.OnOk = function (d) {
                res(_this53.selectedItem);
                return Promise.resolve(true);
              };

              _this53.OnCancel = function (d) {
                rej("user canceled");
                return Promise.resolve(false);
              };

              _this53.StartAsync().then(function (comp) {
                _this53.Show = true;
              });
            });
          }
        }, {
          key: "SubmitNewAsync",
          value: function SubmitNewAsync(model) {
            return this.Service.Post("Post", model);
          }
        }, {
          key: "SubmitUpdateAsync",
          value: function SubmitUpdateAsync(model) {
            return this.Service.Put("Put", model);
          }
        }]);

        return TreeComponentBase;
      }(_baseComponent__WEBPACK_IMPORTED_MODULE_1__["BaseComponent"]);

      TreeComponentBase.ɵfac = function TreeComponentBase_Factory(t) {
        return ɵTreeComponentBase_BaseFactory(t || TreeComponentBase);
      };

      TreeComponentBase.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵdefineComponent"]({
        type: TreeComponentBase,
        selectors: [["ng-component"]],
        inputs: {
          LoadOnStart: "LoadOnStart"
        },
        features: [_angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵInheritDefinitionFeature"]],
        decls: 0,
        vars: 0,
        template: function TreeComponentBase_Template(rf, ctx) {},
        encapsulation: 2
      });

      var ɵTreeComponentBase_BaseFactory = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵgetInheritedFactory"](TreeComponentBase);
      /*@__PURE__*/


      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵsetClassMetadata"](TreeComponentBase, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_3__["Component"],
          args: [{
            template: ''
          }]
        }], null, {
          LoadOnStart: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_3__["Input"]
          }]
        });
      })();
      /***/

    },

    /***/
    "niJb":
    /*!*************************************************************!*\
      !*** ./src/core/codeshell/services/sectionedFormService.ts ***!
      \*************************************************************/

    /*! exports provided: SectionedFormService */

    /***/
    function niJb(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "SectionedFormService", function () {
        return SectionedFormService;
      });
      /* harmony import */


      var codeshell_utilities_extensions__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! codeshell/utilities/extensions */
      "afo8");
      /* harmony import */


      var codeshell_utilities_extensions__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(codeshell_utilities_extensions__WEBPACK_IMPORTED_MODULE_0__);
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var codeshell_main__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! codeshell/main */
      "7OZ3");

      var SectionedFormService = /*#__PURE__*/function () {
        function SectionedFormService(tabs) {
          var _this54 = this;

          var active = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : [];
          var defaultState = arguments.length > 2 && arguments[2] !== undefined ? arguments[2] : true;
          var steps = arguments.length > 3 && arguments[3] !== undefined ? arguments[3] : false;

          _classCallCheck(this, SectionedFormService);

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
            for (var i = 1; i <= tabs; i++) {
              this.ActiveTabs.push(i);
            }
          } else {
            this.ActiveTabs = active;
          }

          setTimeout(function () {
            _this54._change.emit(_this54.ActiveTabs);
          }, 200);
        }

        _createClass(SectionedFormService, [{
          key: "OnChange",
          get: function get() {
            return this._change;
          }
        }, {
          key: "OnAllComplete",
          get: function get() {
            return this._allComplete;
          }
        }, {
          key: "PecentComplete",
          get: function get() {
            return this.CompleteIndex / (this.TabCount - 1) * 100;
          }
        }, {
          key: "OnValidChanged",
          get: function get() {
            return this._validStatus;
          }
        }, {
          key: "IsValid",
          get: function get() {
            return this._isValid;
          }
        }, {
          key: "Select",
          value: function Select(index, scroll) {
            this.ActiveTabs = [index];
            this.CurrentIndex = index;
            if (scroll) this.Scroll(scroll);

            this._change.emit(this.ActiveTabs);
          }
        }, {
          key: "Toggle",
          value: function Toggle(index, scroll) {
            var loc = this.ActiveTabs.indexOf(index);

            if (loc == -1) {
              if (this.Steps) {
                if (index <= this.CompleteIndex + 1) {
                  this.CurrentIndex = index;
                  this.ActiveTabs.push(index);
                }
              } else {
                this.ActiveTabs.push(index);
              }
            } else {
              this.ActiveTabs.splice(loc, 1);
            }

            if (scroll) this.Scroll(scroll);

            this._change.emit(this.ActiveTabs);
          }
        }, {
          key: "SetValidState",
          value: function SetValidState(index, state) {
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
        }, {
          key: "SetComplete",
          value: function SetComplete(index) {
            var force = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : false;

            if (!force && this.CompleteIndex < index) {
              this.CompleteIndex = index;
            }

            Object(codeshell_main__WEBPACK_IMPORTED_MODULE_2__["List_RemoveItem"])(this.ActiveTabs, index);
            var nxt = index + 1;
            if (this.ActiveTabs.indexOf(nxt) == -1) this.ActiveTabs.push(nxt);

            this._change.emit(this.ActiveTabs);
          }
        }, {
          key: "Scroll",
          value: function Scroll(id) {
            var elem = document.getElementById(id);

            if (elem) {
              window.scrollTo({
                top: elem.offsetTop
              });
            }
          }
        }, {
          key: "AllComplete",
          value: function AllComplete() {
            this._allComplete.emit();
          }
        }, {
          key: "Next",
          value: function Next() {
            this.CompleteIndex = this.CurrentIndex;
            this.CurrentIndex += 1;
            this.ActiveTabs = [this.CurrentIndex];
          }
        }, {
          key: "Back",
          value: function Back() {
            if (this.CurrentIndex == 1) return;
            this.CompleteIndex -= 1;
            this.CurrentIndex -= 1;
            this.ActiveTabs = [this.CurrentIndex];
          }
        }, {
          key: "IsLast",
          value: function IsLast() {
            return this.CurrentIndex == this.TabCount;
          }
        }, {
          key: "CanGoBack",
          value: function CanGoBack() {
            return this.CurrentIndex > 1;
          }
        }, {
          key: "CanGoNext",
          value: function CanGoNext() {
            return this.CurrentIndex < this.TabCount;
          }
        }, {
          key: "IsComplete",
          value: function IsComplete(index) {
            return index <= this.CompleteIndex;
          }
        }, {
          key: "IsActive",
          value: function IsActive(index) {
            return this.ActiveTabs.indexOf(index) > -1;
          }
        }, {
          key: "IsCurrent",
          value: function IsCurrent(index) {
            return this.CurrentIndex == index;
          }
        }]);

        return SectionedFormService;
      }();
      /***/

    },

    /***/
    "nliR":
    /*!*******************************!*\
      !*** ./src/app/App.module.ts ***!
      \*******************************/

    /*! exports provided: AppModule */

    /***/
    function nliR(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "AppModule", function () {
        return AppModule;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var _angular_platform_browser__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! @angular/platform-browser */
      "jhN1");
      /* harmony import */


      var _angular_platform_browser_animations__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! @angular/platform-browser/animations */
      "R1ws");
      /* harmony import */


      var ngx_toastr__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! ngx-toastr */
      "5eHb");
      /* harmony import */


      var codeshell__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
      /*! codeshell */
      "7OZ3");
      /* harmony import */


      var codeshell_security__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(
      /*! codeshell/security */
      "U6Sh");
      /* harmony import */


      var _base_example_base_module__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(
      /*! @base/example-base.module */
      "NXgI");
      /* harmony import */


      var _app_component__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(
      /*! ./app.component */
      "Sy1n");
      /* harmony import */


      var _shared_shared_module__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(
      /*! ./shared/shared.module */
      "PCNd");
      /* harmony import */


      var _app_routing_module__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(
      /*! ./app-routing.module */
      "vY5A");
      /* harmony import */


      var codeshell_base_module_module__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(
      /*! codeshell/base-module.module */
      "079c");
      /* harmony import */


      var _core_codeshell_codeshell_module__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(
      /*! ../core/codeshell/codeshell.module */
      "bT1W");

      var AppModule = /*#__PURE__*/function (_codeshell_base_modul) {
        _inherits(AppModule, _codeshell_base_modul);

        var _super18 = _createSuper(AppModule);

        function AppModule() {
          _classCallCheck(this, AppModule);

          return _super18.apply(this, arguments);
        }

        return AppModule;
      }(codeshell_base_module_module__WEBPACK_IMPORTED_MODULE_10__["BaseModule"]);

      AppModule.ɵmod = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineNgModule"]({
        type: AppModule,
        bootstrap: [_app_component__WEBPACK_IMPORTED_MODULE_7__["AppComponent"]]
      });
      AppModule.ɵinj = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineInjector"]({
        factory: function AppModule_Factory(t) {
          return ɵAppModule_BaseFactory(t || AppModule);
        },
        providers: [{
          provide: codeshell_security__WEBPACK_IMPORTED_MODULE_5__["DomainDataProvider"],
          useValue: new codeshell_security__WEBPACK_IMPORTED_MODULE_5__["DomainDataProvider"](Object(_app_routing_module__WEBPACK_IMPORTED_MODULE_9__["GetDomainsData"])())
        }],
        imports: [[ngx_toastr__WEBPACK_IMPORTED_MODULE_3__["ToastrModule"].forRoot(), codeshell__WEBPACK_IMPORTED_MODULE_4__["CodeShellModule"].forRoot(), _base_example_base_module__WEBPACK_IMPORTED_MODULE_6__["ExampleBaseModule"].forRoot(), _shared_shared_module__WEBPACK_IMPORTED_MODULE_8__["SharedModule"], _angular_platform_browser__WEBPACK_IMPORTED_MODULE_1__["BrowserModule"], _angular_platform_browser_animations__WEBPACK_IMPORTED_MODULE_2__["BrowserAnimationsModule"], _app_routing_module__WEBPACK_IMPORTED_MODULE_9__["AppRoutingModule"]]]
      });

      (function () {
        (typeof ngJitMode === "undefined" || ngJitMode) && _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵsetNgModuleScope"](AppModule, {
          declarations: [_app_component__WEBPACK_IMPORTED_MODULE_7__["AppComponent"]],
          imports: [ngx_toastr__WEBPACK_IMPORTED_MODULE_3__["ToastrModule"], _core_codeshell_codeshell_module__WEBPACK_IMPORTED_MODULE_11__["CodeShellModule"], _base_example_base_module__WEBPACK_IMPORTED_MODULE_6__["ExampleBaseModule"], _shared_shared_module__WEBPACK_IMPORTED_MODULE_8__["SharedModule"], _angular_platform_browser__WEBPACK_IMPORTED_MODULE_1__["BrowserModule"], _angular_platform_browser_animations__WEBPACK_IMPORTED_MODULE_2__["BrowserAnimationsModule"], _app_routing_module__WEBPACK_IMPORTED_MODULE_9__["AppRoutingModule"]]
        });
      })();
      /*@__PURE__*/


      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](AppModule, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"],
          args: [{
            bootstrap: [_app_component__WEBPACK_IMPORTED_MODULE_7__["AppComponent"]],
            declarations: [_app_component__WEBPACK_IMPORTED_MODULE_7__["AppComponent"]],
            imports: [ngx_toastr__WEBPACK_IMPORTED_MODULE_3__["ToastrModule"].forRoot(), codeshell__WEBPACK_IMPORTED_MODULE_4__["CodeShellModule"].forRoot(), _base_example_base_module__WEBPACK_IMPORTED_MODULE_6__["ExampleBaseModule"].forRoot(), _shared_shared_module__WEBPACK_IMPORTED_MODULE_8__["SharedModule"], _angular_platform_browser__WEBPACK_IMPORTED_MODULE_1__["BrowserModule"], _angular_platform_browser_animations__WEBPACK_IMPORTED_MODULE_2__["BrowserAnimationsModule"], _app_routing_module__WEBPACK_IMPORTED_MODULE_9__["AppRoutingModule"]],
            providers: [{
              provide: codeshell_security__WEBPACK_IMPORTED_MODULE_5__["DomainDataProvider"],
              useValue: new codeshell_security__WEBPACK_IMPORTED_MODULE_5__["DomainDataProvider"](Object(_app_routing_module__WEBPACK_IMPORTED_MODULE_9__["GetDomainsData"])())
            }]
          }]
        }], null, null);
      })();

      var ɵAppModule_BaseFactory = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵgetInheritedFactory"](AppModule);
      /***/

    },

    /***/
    "ppms":
    /*!**************************************************!*\
      !*** ./src/core/codeshell/utilities/registry.ts ***!
      \**************************************************/

    /*! exports provided: Registry */

    /***/
    function ppms(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "Registry", function () {
        return Registry;
      });
      /* harmony import */


      var _security_models__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! ../security/models */
      "gFuU");

      var Registry = /*#__PURE__*/function () {
        function Registry() {
          _classCallCheck(this, Registry);
        }

        _createClass(Registry, null, [{
          key: "UserType",
          get: function get() {
            if (this._userType) return this._userType;else return _security_models__WEBPACK_IMPORTED_MODULE_0__["UserDTO"];
          }
        }, {
          key: "Register",
          value: function Register(name, s) {
            Registry.List[name] = s;
          }
        }, {
          key: "Get",
          value: function Get(name) {
            if (Registry.List[name]) return Registry.List[name];
            return undefined;
          }
        }, {
          key: "RegisterUserType",
          value: function RegisterUserType(con) {
            Registry._userType = con;
          }
        }]);

        return Registry;
      }();

      Registry.List = {};
      /***/
    },

    /***/
    "qJZ4":
    /*!**********************************************************!*\
      !*** ./src/core/codeshell/directives/radio.directive.ts ***!
      \**********************************************************/

    /*! exports provided: Radio */

    /***/
    function qJZ4(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "Radio", function () {
        return Radio;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");

      var Radio = /*#__PURE__*/function () {
        function Radio(el) {
          _classCallCheck(this, Radio);

          this.el = el;
          this.model = false;
          this.modelOut = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        }

        _createClass(Radio, [{
          key: "Element",
          get: function get() {
            return this.el.nativeElement;
          }
        }, {
          key: "ngOnInit",
          value: function ngOnInit() {
            this.Element.checked = this.model;
          }
        }, {
          key: "OnClick",
          value: function OnClick() {
            var elems = document.getElementsByName(this.Element.name);

            for (var i = 0; i < elems.length; i++) {
              var el = elems.item(i);

              if (el != this.Element && el.type == "radio") {
                el.dispatchEvent(new Event('change'));
              }
            }
          }
        }, {
          key: "OnChange",
          value: function OnChange() {
            this.modelOut.emit(this.Element.checked);
          }
        }]);

        return Radio;
      }();

      Radio.ɵfac = function Radio_Factory(t) {
        return new (t || Radio)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]));
      };

      Radio.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({
        type: Radio,
        selectors: [["input", "type", "radio", "radioCheck", ""]],
        hostBindings: function Radio_HostBindings(rf, ctx) {
          if (rf & 1) {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function Radio_click_HostBindingHandler() {
              return ctx.OnClick();
            })("change", function Radio_change_HostBindingHandler() {
              return ctx.OnChange();
            });
          }
        },
        inputs: {
          model: ["radioCheck", "model"]
        },
        outputs: {
          modelOut: "radioCheckChange"
        },
        exportAs: ["radioCheck"]
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](Radio, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
          args: [{
            selector: "input[type='radio'][radioCheck]",
            exportAs: 'radioCheck'
          }]
        }], function () {
          return [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]
          }];
        }, {
          model: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["radioCheck"]
          }],
          modelOut: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"],
            args: ["radioCheckChange"]
          }],
          OnClick: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"],
            args: ["click"]
          }],
          OnChange: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"],
            args: ["change"]
          }]
        });
      })();
      /***/

    },

    /***/
    "rCrt":
    /*!*********************************************************!*\
      !*** ./src/core/codeshell/base-components/loginBase.ts ***!
      \*********************************************************/

    /*! exports provided: LoginBase */

    /***/
    function rCrt(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "LoginBase", function () {
        return LoginBase;
      });
      /* harmony import */


      var _baseComponent__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! ./baseComponent */
      "I5Ck");
      /* harmony import */


      var codeshell_security__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! codeshell/security */
      "U6Sh");
      /* harmony import */


      var codeshell_shell__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! codeshell/shell */
      "UoIT");
      /* harmony import */


      var codeshell_utilities_utils__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! codeshell/utilities/utils */
      "VXkE");
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");

      var LoginBase = /*#__PURE__*/function (_baseComponent__WEBPA4) {
        _inherits(LoginBase, _baseComponent__WEBPA4);

        var _super19 = _createSuper(LoginBase);

        function LoginBase() {
          var _this55;

          _classCallCheck(this, LoginBase);

          _this55 = _super19.apply(this, arguments);
          _this55.AccountService = codeshell_shell__WEBPACK_IMPORTED_MODULE_2__["Shell"].Injector.get(codeshell_security__WEBPACK_IMPORTED_MODULE_1__["AccountServiceBase"]);
          _this55.model = {};
          return _this55;
        }

        _createClass(LoginBase, [{
          key: "ShowPassword",
          value: function ShowPassword(passInput) {
            passInput.type = "text";
          }
        }, {
          key: "HidePassword",
          value: function HidePassword(passInput) {
            passInput.type = "password";
          }
        }, {
          key: "TogglePassword",
          value: function TogglePassword(passInput) {
            if (passInput.type == "text") passInput.type = "password";else passInput.type = "text";
          }
        }, {
          key: "ngOnInit",
          value: function ngOnInit() {
            _get(_getPrototypeOf(LoginBase.prototype), "ngOnInit", this).call(this);

            if (codeshell_shell__WEBPACK_IMPORTED_MODULE_2__["Shell"].Session.IsLoggedIn) this.Router.navigateByUrl("/");
            this.Title = codeshell_shell__WEBPACK_IMPORTED_MODULE_2__["Shell"].Translator.instant("Words.Login");
          }
        }, {
          key: "Login",
          value: function Login() {
            var _this56 = this;

            this.AccountService.Login(this.model).then(function (data) {
              codeshell_shell__WEBPACK_IMPORTED_MODULE_2__["Shell"].Session.StartSession(data);

              _this56.Router.navigateByUrl("/");
            })["catch"](function (error) {
              codeshell_utilities_utils__WEBPACK_IMPORTED_MODULE_3__["Utils"].HandleError(error, true);
            });
          }
        }]);

        return LoginBase;
      }(_baseComponent__WEBPACK_IMPORTED_MODULE_0__["BaseComponent"]);

      LoginBase.ɵfac = function LoginBase_Factory(t) {
        return ɵLoginBase_BaseFactory(t || LoginBase);
      };

      LoginBase.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵdefineComponent"]({
        type: LoginBase,
        selectors: [["ng-component"]],
        features: [_angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵInheritDefinitionFeature"]],
        decls: 0,
        vars: 0,
        template: function LoginBase_Template(rf, ctx) {},
        encapsulation: 2
      });

      var ɵLoginBase_BaseFactory = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵgetInheritedFactory"](LoginBase);
      /*@__PURE__*/


      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵsetClassMetadata"](LoginBase, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_4__["Component"],
          args: [{
            template: ''
          }]
        }], null, null);
      })();
      /***/

    },

    /***/
    "rp8W":
    /*!*****************************************************************!*\
      !*** ./src/core/codeshell/security/authorizationServiceBase.ts ***!
      \*****************************************************************/

    /*! exports provided: AuthorizationError, AuthorizationServiceBase */

    /***/
    function rp8W(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "AuthorizationError", function () {
        return AuthorizationError;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "AuthorizationServiceBase", function () {
        return AuthorizationServiceBase;
      });
      /* harmony import */


      var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! tslib */
      "mrSG");
      /* harmony import */


      var _models__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ./models */
      "gFuU");
      /* harmony import */


      var _shell__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! ../shell */
      "UoIT");
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");

      var AuthorizationError = function AuthorizationError() {
        _classCallCheck(this, AuthorizationError);

        this.message = "";
      };

      var AuthorizationServiceBase = /*#__PURE__*/function () {
        function AuthorizationServiceBase() {
          _classCallCheck(this, AuthorizationServiceBase);
        }

        _createClass(AuthorizationServiceBase, [{
          key: "IsAuthorized",
          value: function IsAuthorized(user, data) {
            var events = arguments.length > 2 && arguments[2] !== undefined ? arguments[2] : false;
            if (data.IsAnonymous) return true;
            this.User = user;

            if (this.User) {
              if (!this.CheckApp(data)) {
                if (events) AuthorizationServiceBase.OnAuthorizationFailed.emit({
                  data: data,
                  message: "Incompatible Apps"
                });
                return false;
              }

              if (data.AllowAll) return true;
              if (this.CheckUserType(data)) return true;
              var p = this.GetPermission(data.resource);

              if (!p.Can(data.action)) {
                if (events) AuthorizationServiceBase.OnAuthorizationFailed.emit({
                  data: data,
                  message: "No permission"
                });
                return false;
              }

              return true;
            }

            return false;
          }
        }, {
          key: "IsAuthorizedAsync",
          value: function IsAuthorizedAsync(data) {
            return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, /*#__PURE__*/regeneratorRuntime.mark(function _callee17() {
              var user;
              return regeneratorRuntime.wrap(function _callee17$(_context17) {
                while (1) {
                  switch (_context17.prev = _context17.next) {
                    case 0:
                      if (!data.IsAnonymous) {
                        _context17.next = 2;
                        break;
                      }

                      return _context17.abrupt("return", true);

                    case 2:
                      _context17.next = 4;
                      return _shell__WEBPACK_IMPORTED_MODULE_2__["Shell"].Session.GetUserAsync();

                    case 4:
                      user = _context17.sent;
                      return _context17.abrupt("return", this.IsAuthorized(user, data, true));

                    case 6:
                    case "end":
                      return _context17.stop();
                  }
                }
              }, _callee17, this);
            }));
          }
        }, {
          key: "GetPermission",
          value: function GetPermission(id) {
            if (this.User && this.User.permissions[id]) {
              var u = this.User;
              return u.permissions[id];
            } else {
              var s = new _models__WEBPACK_IMPORTED_MODULE_1__["Permission"]();
              s.view = false;
              return s;
            }
          }
        }, {
          key: "CheckApp",
          value: function CheckApp(data) {
            if (this.User && data.apps) {
              var _iterator15 = _createForOfIteratorHelper(data.apps),
                  _step15;

              try {
                for (_iterator15.s(); !(_step15 = _iterator15.n()).done;) {
                  var d = _step15.value;
                  if (this.User.app == d) return true;
                }
              } catch (err) {
                _iterator15.e(err);
              } finally {
                _iterator15.f();
              }

              return false;
            }

            return true;
          }
        }, {
          key: "CheckUserType",
          value: function CheckUserType(data) {
            if (!this.User) return false;
            if (typeof data.action != "string") return false;
            var type = data.action;
            if (type.indexOf("UserType") != 0) return false;
            return "UserType." + this.User.userTypeString == type;
          }
        }]);

        return AuthorizationServiceBase;
      }();

      AuthorizationServiceBase.OnAuthorizationFailed = new _angular_core__WEBPACK_IMPORTED_MODULE_3__["EventEmitter"]();
      /***/
    },

    /***/
    "sVHn":
    /*!*****************************************************************!*\
      !*** ./src/core/codeshell/base-components/listComponentBase.ts ***!
      \*****************************************************************/

    /*! exports provided: ListComponentBase */

    /***/
    function sVHn(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "ListComponentBase", function () {
        return ListComponentBase;
      });
      /* harmony import */


      var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! tslib */
      "mrSG");
      /* harmony import */


      var _baseComponent__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ./baseComponent */
      "I5Ck");
      /* harmony import */


      var _shell__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! ../shell */
      "UoIT");
      /* harmony import */


      var codeshell_main__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! codeshell/main */
      "7OZ3");
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");

      var ListComponentBase = /*#__PURE__*/function (_baseComponent__WEBPA5) {
        _inherits(ListComponentBase, _baseComponent__WEBPA5);

        var _super20 = _createSuper(ListComponentBase);

        function ListComponentBase() {
          var _this57;

          _classCallCheck(this, ListComponentBase);

          _this57 = _super20.apply(this, arguments);
          _this57.filter = {};
          _this57.list = [];
          _this57.totalCount = 0;
          _this57.pageIndex = 0;
          _this57.options = {
            Showing: 10,
            Skip: 0
          };
          _this57._sortingClass = null;
          _this57.Selection = null;
          _this57._loadedOnce = false;
          return _this57;
        }

        _createClass(ListComponentBase, [{
          key: "CollectionId",
          get: function get() {
            return null;
          }
        }, {
          key: "ngOnInit",
          value: function ngOnInit() {
            var _this58 = this;

            _get(_getPrototypeOf(ListComponentBase.prototype), "ngOnInit", this).call(this);

            var opts = this.GetLookupOptions();

            if (opts != null) {
              this.LoadLookupsAsync(opts).then(function (l) {
                _this58.Lookups = l;

                _this58.Start();
              });
            } else {
              this.Start();
            }
          }
        }, {
          key: "LoadLookupsAsync",
          value: function LoadLookupsAsync(opts) {
            return this.Service.Get("GetListLookups", opts);
          }
        }, {
          key: "Start",
          value: function Start() {
            if (!this.IsEmbedded) this.LoadData();
            this.OnReady();
          }
        }, {
          key: "StartAsync",
          value: function StartAsync() {
            return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, /*#__PURE__*/regeneratorRuntime.mark(function _callee18() {
              return regeneratorRuntime.wrap(function _callee18$(_context18) {
                while (1) {
                  switch (_context18.prev = _context18.next) {
                    case 0:
                      if (this._loadedOnce) {
                        _context18.next = 4;
                        break;
                      }

                      _context18.next = 3;
                      return this.LoadData();

                    case 3:
                      this._loadedOnce = true;

                    case 4:
                      return _context18.abrupt("return", this);

                    case 5:
                    case "end":
                      return _context18.stop();
                  }
                }
              }, _callee18, this);
            }));
          }
        }, {
          key: "OnReady",
          value: function OnReady() {
            window.scrollTo(0, 0);
            if (this.options.OrderProperty) this._sortingClass = "sorting-head-" + this.options.Direction;
          }
        }, {
          key: "PageSelected",
          value: function PageSelected(n) {
            this.options.Skip = this.options.Showing * n;
            this.pageIndex = n;
            this.LoadData();
          }
        }, {
          key: "Delete",
          value: function Delete(item) {
            var _this59 = this;

            if (this.Debug) console.log("Deleting", item);
            this.DeleteAsync(item).then(function (d) {
              if (d.canDelete) {
                _this59.NotifyTranslate("delete_success");

                _this59.LoadData();
              } else {
                codeshell_main__WEBPACK_IMPORTED_MODULE_3__["Utils"].HandleResult(d, true, true);
              }
            })["catch"](function (d) {
              if (d != "cancelled") codeshell_main__WEBPACK_IMPORTED_MODULE_3__["Utils"].HandleError(d, true, true);
            });
          }
        }, {
          key: "DeleteAsync",
          value: function DeleteAsync(item) {
            return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, /*#__PURE__*/regeneratorRuntime.mark(function _callee19() {
              var c;
              return regeneratorRuntime.wrap(function _callee19$(_context19) {
                while (1) {
                  switch (_context19.prev = _context19.next) {
                    case 0:
                      _context19.next = 2;
                      return _shell__WEBPACK_IMPORTED_MODULE_2__["Shell"].Main.ShowDeleteConfirm();

                    case 2:
                      c = _context19.sent;

                      if (c) {
                        _context19.next = 5;
                        break;
                      }

                      return _context19.abrupt("return", Promise.reject("cancelled"));

                    case 5:
                      _context19.next = 7;
                      return this.Service.AttemptDelete(item);

                    case 7:
                      return _context19.abrupt("return", _context19.sent);

                    case 8:
                    case "end":
                      return _context19.stop();
                  }
                }
              }, _callee19, this);
            }));
          }
        }, {
          key: "AttemptDelete",
          value: function AttemptDelete(item) {
            return Object(tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"])(this, void 0, void 0, /*#__PURE__*/regeneratorRuntime.mark(function _callee20() {
              var id, s;
              return regeneratorRuntime.wrap(function _callee20$(_context20) {
                while (1) {
                  switch (_context20.prev = _context20.next) {
                    case 0:
                      id = item.id;

                      if (id == undefined) {
                        id = item.entity.id;
                      }

                      _context20.next = 4;
                      return this.Service.AttemptDelete(id);

                    case 4:
                      s = _context20.sent;

                      if (s.canDelete) {
                        this.LoadData();
                        s.message = "delete_success";
                      }

                      return _context20.abrupt("return", s);

                    case 7:
                    case "end":
                      return _context20.stop();
                  }
                }
              }, _callee20, this);
            }));
          }
        }, {
          key: "LoadDataPromise",
          value: function LoadDataPromise() {
            if (this.Loader) return this.Loader(this.options);

            if (this.CollectionId == null) {
              return this.Service.GetPaged("Get", this.options);
            } else {
              return this.Service.GetPaged("GetCollection/" + this.CollectionId, this.options);
            }
          }
        }, {
          key: "Clear",
          value: function Clear(id) {
            this.totalCount = 0;
            this.list = [];
          }
        }, {
          key: "LoadData",
          value: function LoadData() {
            var _this60 = this;

            this.options.Filters = this.stringifyFilters();
            var prom = this.LoadDataPromise();
            prom.then(function (e) {
              _this60.ProcessResponse(e);

              _this60.list = e.list;
              _this60.totalCount = e.totalCount;
              if (_this60.Selection) _this60.Selection.List = _this60.list;

              _this60.AfterLoad();
            });
          }
        }, {
          key: "AfterLoad",
          value: function AfterLoad() {}
        }, {
          key: "ProcessResponse",
          value: function ProcessResponse(e) {}
        }, {
          key: "GetFilters",
          value: function GetFilters() {
            var filters = [];

            for (var i in this.filter) {
              if (this.filter[i].Value1 != undefined || this.filter[i].Value2 != undefined || this.filter[i].Ids.length > 0) filters.push(this.filter[i]);
            }

            return filters;
          }
        }, {
          key: "stringifyFilters",
          value: function stringifyFilters() {
            var filters = this.GetFilters();
            return JSON.stringify(filters);
          }
        }, {
          key: "ClearFilter",
          value: function ClearFilter(item) {
            item.Value1 = undefined;
            item.Value2 = undefined;
            item.Ids = [];
            this.ResetPagination();
            this.LoadData();
          }
        }, {
          key: "SelectIdSingle",
          value: function SelectIdSingle(f, val) {
            if (val == 0) {
              f.Value1 = undefined;
              f.Value2 = undefined;
              f.Ids = [];
            } else {
              f.Ids = [val];
            }

            this.LoadData();
          }
        }, {
          key: "ToggleIdSingle",
          value: function ToggleIdSingle(f, val) {
            var s = f.Ids.indexOf(val);
            if (s > -1) f.Ids.splice(s, 1);else {
              f.Ids = [val];
              f.Value1 = val;
            }
            this.LoadData();
          }
        }, {
          key: "ToggleId",
          value: function ToggleId(f, id) {
            var s = f.Ids.indexOf(id);
            if (s > -1) f.Ids.splice(s, 1);else {
              f.Ids.push(id);
            }
            this.ResetPagination();
            this.LoadData();
          }
        }, {
          key: "IsSelected",
          value: function IsSelected(f, id) {
            return f.Ids.indexOf(id) > -1;
          }
        }, {
          key: "ResetPagination",
          value: function ResetPagination() {
            this.options.Skip = 0;
            this.pageIndex = 0;
          }
        }, {
          key: "HeaderSearch",
          value: function HeaderSearch(term) {
            this.options.SearchTerm = term;
            this.ResetPagination();
            this.LoadData();
          }
        }, {
          key: "SortBy",
          value: function SortBy(prop) {
            if (prop == this.options.OrderProperty) {
              this.options.Direction = this.options.Direction == "ASC" ? "DESC" : "ASC";
            } else {
              this.options.OrderProperty = prop;
              this.options.Direction = "ASC";
            }

            this._sortingClass = "sorting-head-" + this.options.Direction;
            this.PageSelected(0);
          }
        }, {
          key: "GetHeaderClass",
          value: function GetHeaderClass(prop) {
            if (this.options.OrderProperty == prop) {
              return this._sortingClass;
            }

            return null;
          }
        }]);

        return ListComponentBase;
      }(_baseComponent__WEBPACK_IMPORTED_MODULE_1__["BaseComponent"]);

      ListComponentBase.ɵfac = function ListComponentBase_Factory(t) {
        return ɵListComponentBase_BaseFactory(t || ListComponentBase);
      };

      ListComponentBase.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵdefineComponent"]({
        type: ListComponentBase,
        selectors: [["ng-component"]],
        features: [_angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵInheritDefinitionFeature"]],
        decls: 0,
        vars: 0,
        template: function ListComponentBase_Template(rf, ctx) {},
        encapsulation: 2
      });

      var ɵListComponentBase_BaseFactory = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵgetInheritedFactory"](ListComponentBase);
      /*@__PURE__*/


      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵsetClassMetadata"](ListComponentBase, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_4__["Component"],
          args: [{
            template: ''
          }]
        }], null, null);
      })();
      /***/

    },

    /***/
    "u9rM":
    /*!**************************************************************************!*\
      !*** ./src/core/codeshell/base-components/sectionedEditComponentBase.ts ***!
      \**************************************************************************/

    /*! exports provided: SectionedEditComponentBase */

    /***/
    function u9rM(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "SectionedEditComponentBase", function () {
        return SectionedEditComponentBase;
      });
      /* harmony import */


      var _editComponentBase__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! ./editComponentBase */
      "G778");
      /* harmony import */


      var codeshell_services_sectionedFormService__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! codeshell/services/sectionedFormService */
      "niJb");
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");

      var SectionedEditComponentBase = /*#__PURE__*/function (_editComponentBase__W2) {
        _inherits(SectionedEditComponentBase, _editComponentBase__W2);

        var _super21 = _createSuper(SectionedEditComponentBase);

        function SectionedEditComponentBase() {
          var _this61;

          _classCallCheck(this, SectionedEditComponentBase);

          _this61 = _super21.apply(this, arguments);
          _this61.SF = new codeshell_services_sectionedFormService__WEBPACK_IMPORTED_MODULE_1__["SectionedFormService"](1);
          return _this61;
        }

        return SectionedEditComponentBase;
      }(_editComponentBase__WEBPACK_IMPORTED_MODULE_0__["EditComponentBase"]);

      SectionedEditComponentBase.ɵfac = function SectionedEditComponentBase_Factory(t) {
        return ɵSectionedEditComponentBase_BaseFactory(t || SectionedEditComponentBase);
      };

      SectionedEditComponentBase.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵdefineComponent"]({
        type: SectionedEditComponentBase,
        selectors: [["ng-component"]],
        inputs: {
          SF: ["manager", "SF"]
        },
        features: [_angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵInheritDefinitionFeature"]],
        decls: 0,
        vars: 0,
        template: function SectionedEditComponentBase_Template(rf, ctx) {},
        encapsulation: 2
      });

      var ɵSectionedEditComponentBase_BaseFactory = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵgetInheritedFactory"](SectionedEditComponentBase);
      /*@__PURE__*/


      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵsetClassMetadata"](SectionedEditComponentBase, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_2__["Component"],
          args: [{
            template: ''
          }]
        }], null, {
          SF: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_2__["Input"],
            args: ["manager"]
          }]
        });
      })();
      /***/

    },

    /***/
    "vY5A":
    /*!***************************************!*\
      !*** ./src/app/app-routing.module.ts ***!
      \***************************************/

    /*! exports provided: AppRoutingModule, GetDomainsData */

    /***/
    function vY5A(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "AppRoutingModule", function () {
        return AppRoutingModule;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "GetDomainsData", function () {
        return GetDomainsData;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! @angular/router */
      "tyNb");
      /* harmony import */


      var codeshell_localization__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! codeshell/localization */
      "InfA");
      /* harmony import */


      var _base_main_login_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! @base/main/login.component */
      "8sze");

      codeshell_localization__WEBPACK_IMPORTED_MODULE_2__["Translator"].SetLoaders({});
      var routes = [{
        path: 'login',
        component: _base_main_login_component__WEBPACK_IMPORTED_MODULE_3__["Login"],
        data: {
          action: 'anonymous',
          name: "login"
        }
      }, {
        path: '**',
        redirectTo: '/'
      }];

      var AppRoutingModule = function AppRoutingModule() {
        _classCallCheck(this, AppRoutingModule);
      };

      AppRoutingModule.ɵmod = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineNgModule"]({
        type: AppRoutingModule
      });
      AppRoutingModule.ɵinj = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineInjector"]({
        factory: function AppRoutingModule_Factory(t) {
          return new (t || AppRoutingModule)();
        },
        imports: [[_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"].forRoot(routes, {
          relativeLinkResolution: 'legacy'
        })], _angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]]
      });

      (function () {
        (typeof ngJitMode === "undefined" || ngJitMode) && _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵsetNgModuleScope"](AppRoutingModule, {
          imports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]],
          exports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]]
        });
      })();
      /*@__PURE__*/


      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](AppRoutingModule, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"],
          args: [{
            imports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"].forRoot(routes, {
              relativeLinkResolution: 'legacy'
            })],
            exports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]]
          }]
        }], null, null);
      })();

      var data = null;

      function GetDomainsData() {
        if (!data) {
          data = [];
        }

        return data;
      }
      /***/

    },

    /***/
    "vyk5":
    /*!************************************************!*\
      !*** ./src/core/codeshell/http/httpRequest.ts ***!
      \************************************************/

    /*! exports provided: Methods, RequestParams, HttpRequest */

    /***/
    function vyk5(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "Methods", function () {
        return Methods;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "RequestParams", function () {
        return RequestParams;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "HttpRequest", function () {
        return HttpRequest;
      });

      var Methods;

      (function (Methods) {
        Methods[Methods["Get"] = 0] = "Get";
        Methods[Methods["Post"] = 1] = "Post";
        Methods[Methods["Put"] = 2] = "Put";
        Methods[Methods["Delete"] = 3] = "Delete";
      })(Methods || (Methods = {}));

      var RequestParams = function RequestParams() {
        _classCallCheck(this, RequestParams);
      };

      var HttpRequest = function HttpRequest(url, params, body) {
        _classCallCheck(this, HttpRequest);

        this.Params = new RequestParams();
        this.Params = new RequestParams();
        this.Url = url;
        this.Body = body;

        if (typeof params == 'number') {
          this.Url += "/" + params;
        } else if (params) {
          this.Params.params = params;
        }
      };
      /***/

    },

    /***/
    "wGhu":
    /*!**********************************************!*\
      !*** ./src/core/codeshell/helpers/tagged.ts ***!
      \**********************************************/

    /*! exports provided: TaggedArgs, Tagged */

    /***/
    function wGhu(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "TaggedArgs", function () {
        return TaggedArgs;
      });
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "Tagged", function () {
        return Tagged;
      });
      /* harmony import */


      var _listItem__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! ./listItem */
      "JLZs");

      var TaggedArgs = function TaggedArgs() {
        _classCallCheck(this, TaggedArgs);

        this.Data = [];
        this.Source = [];
        /**  fromData: From Lookup, fromSrc: From selected items => Should return expression if arg1 exists in items */

        this.Comparer = function (d, s) {
          return true;
        };

        this.CreateNew = function (d) {};
      };

      var Tagged = /*#__PURE__*/function () {
        function Tagged() {
          _classCallCheck(this, Tagged);

          this.Tag = new _listItem__WEBPACK_IMPORTED_MODULE_0__["ListItem"]();
        }

        _createClass(Tagged, null, [{
          key: "FromDB",
          value: function FromDB(arg0) {
            var s = Object.assign(new Tagged(), arg0);
            s.Tag = new _listItem__WEBPACK_IMPORTED_MODULE_0__["ListItem"]();
            return s;
          }
        }, {
          key: "CastList",
          value: function CastList(lst) {
            for (var i in lst) {
              lst[i] = Tagged.FromDB(lst[i]);
            }

            return lst;
          }
        }, {
          key: "JoinLists_GEN",
          value: function JoinLists_GEN(con, args) {
            var lst = [];

            var comparer = function comparer(d, s) {
              return false;
            };

            var createNew = function createNew(item) {};

            if (args.Comparer) comparer = args.Comparer;
            if (args.CreateNew) createNew = args.CreateNew;

            var _iterator16 = _createForOfIteratorHelper(args.Data),
                _step16;

            try {
              var _loop = function _loop() {
                var dev = _step16.value;
                var d = Object.assign(new con(), dev);
                var t = args.Source.find(function (e) {
                  return comparer(dev, e);
                });

                if (t) {
                  t.selected = true;
                } else {
                  t = _listItem__WEBPACK_IMPORTED_MODULE_0__["ListItem"].Detached(createNew(dev));
                }

                d.Tag = t;
                lst.push(d);
              };

              for (_iterator16.s(); !(_step16 = _iterator16.n()).done;) {
                _loop();
              }
            } catch (err) {
              _iterator16.e(err);
            } finally {
              _iterator16.f();
            }

            return lst;
          }
        }, {
          key: "JoinLists",
          value: function JoinLists(args) {
            var lst = [];

            var comparer = function comparer(d, s) {
              return false;
            };

            var createNew = function createNew(item) {};

            if (args.Comparer) comparer = args.Comparer;
            if (args.CreateNew) createNew = args.CreateNew;

            var _iterator17 = _createForOfIteratorHelper(args.Data),
                _step17;

            try {
              var _loop2 = function _loop2() {
                var dev = _step17.value;
                var d = Object.assign(new Tagged(), dev);
                var t = args.Source.find(function (e) {
                  return comparer(dev, e);
                });

                if (t) {
                  t.selected = true;
                } else {
                  t = _listItem__WEBPACK_IMPORTED_MODULE_0__["ListItem"].Detached(createNew(dev));
                }

                d.Tag = t;
                lst.push(d);
              };

              for (_iterator17.s(); !(_step17 = _iterator17.n()).done;) {
                _loop2();
              }
            } catch (err) {
              _iterator17.e(err);
            } finally {
              _iterator17.f();
            }

            return lst;
          }
        }]);

        return Tagged;
      }();
      /***/

    },

    /***/
    "wNr3":
    /*!***********************************************************!*\
      !*** ./src/core/codeshell/security/accountServiceBase.ts ***!
      \***********************************************************/

    /*! exports provided: AccountServiceBase */

    /***/
    function wNr3(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "AccountServiceBase", function () {
        return AccountServiceBase;
      });
      /* harmony import */


      var _http_entityHttpService__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! ../http/entityHttpService */
      "iLnC");

      var AccountServiceBase = /*#__PURE__*/function (_http_entityHttpServi) {
        _inherits(AccountServiceBase, _http_entityHttpServi);

        var _super22 = _createSuper(AccountServiceBase);

        function AccountServiceBase() {
          _classCallCheck(this, AccountServiceBase);

          return _super22.apply(this, arguments);
        }

        _createClass(AccountServiceBase, [{
          key: "Login",
          value: function Login(model) {
            return this.PostAs("Login", model);
          }
        }, {
          key: "GetUserData",
          value: function GetUserData() {
            return this.GetAs("GetUserData");
          }
        }, {
          key: "RefreshToken",
          value: function RefreshToken(token) {
            return this.PostAs("RefreshToken", {
              token: token
            });
          }
        }, {
          key: "ChangePassword",
          value: function ChangePassword(dto) {
            return this.PostAs("ChangePassword", dto);
          }
        }, {
          key: "SendResetMail",
          value: function SendResetMail(mail) {
            return this.Save("SendResetMail", {
              email: mail
            });
          }
        }, {
          key: "RequestResetPassword",
          value: function RequestResetPassword(mail) {
            return this.Post("RequestResetPassword", {
              email: mail
            });
          }
        }]);

        return AccountServiceBase;
      }(_http_entityHttpService__WEBPACK_IMPORTED_MODULE_0__["EntityHttpService"]);
      /***/

    },

    /***/
    "x5Rz":
    /*!*************************************************************!*\
      !*** ./src/core/codeshell/directives/fix-date.directive.ts ***!
      \*************************************************************/

    /*! exports provided: FixDate */

    /***/
    function x5Rz(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "FixDate", function () {
        return FixDate;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");

      var FixDate = /*#__PURE__*/function () {
        function FixDate() {
          _classCallCheck(this, FixDate);
        }

        _createClass(FixDate, [{
          key: "Change",
          value: function Change(ev) {}
        }]);

        return FixDate;
      }();

      FixDate.ɵfac = function FixDate_Factory(t) {
        return new (t || FixDate)();
      };

      FixDate.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({
        type: FixDate,
        selectors: [["", "fix-date", ""]],
        hostBindings: function FixDate_HostBindings(rf, ctx) {
          if (rf & 1) {
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("dateChange", function FixDate_dateChange_HostBindingHandler($event) {
              return ctx.Change($event);
            });
          }
        },
        exportAs: ["fix-date"]
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](FixDate, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
          args: [{
            selector: "[fix-date]",
            exportAs: "fix-date"
          }]
        }], null, {
          Change: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"],
            args: ["dateChange", ['$event']]
          }]
        });
      })();
      /***/

    },

    /***/
    "yvMo":
    /*!*****************************************************!*\
      !*** ./src/core/codeshell/security/tokenStorage.ts ***!
      \*****************************************************/

    /*! exports provided: TokenStorage */

    /***/
    function yvMo(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "TokenStorage", function () {
        return TokenStorage;
      });
      /* harmony import */


      var _models__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! ./models */
      "gFuU");
      /* harmony import */


      var codeshell_helpers__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! codeshell/helpers */
      "GYDu");

      var TokenStorage = /*#__PURE__*/function () {
        function TokenStorage() {
          _classCallCheck(this, TokenStorage);
        }

        _createClass(TokenStorage, [{
          key: "SaveToken",
          value: function SaveToken(data) {
            codeshell_helpers__WEBPACK_IMPORTED_MODULE_1__["Stored"].Set("TokenData", data);
          }
        }, {
          key: "LoadToken",
          value: function LoadToken() {
            return codeshell_helpers__WEBPACK_IMPORTED_MODULE_1__["Stored"].Get("TokenData", _models__WEBPACK_IMPORTED_MODULE_0__["TokenData"]);
          }
        }, {
          key: "Clear",
          value: function Clear() {
            codeshell_helpers__WEBPACK_IMPORTED_MODULE_1__["Stored"].Clear("TokenData");
            localStorage.removeItem("refresh");
          }
        }, {
          key: "GetRefreshToken",
          value: function GetRefreshToken() {
            return localStorage.getItem("refresh");
          }
        }, {
          key: "SaveRefreshToken",
          value: function SaveRefreshToken(token) {
            localStorage.setItem("refresh", token);
          }
        }]);

        return TokenStorage;
      }();
      /***/

    },

    /***/
    "zUnb":
    /*!*********************!*\
      !*** ./src/main.ts ***!
      \*********************/

    /*! no exports provided */

    /***/
    function zUnb(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var _environments_environment__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ./environments/environment */
      "AytR");
      /* harmony import */


      var _app_App_module__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! ./app/App.module */
      "nliR");
      /* harmony import */


      var _angular_platform_browser__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! @angular/platform-browser */
      "jhN1");

      if (_environments_environment__WEBPACK_IMPORTED_MODULE_1__["environment"].production) {
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["enableProdMode"])();
      }

      _angular_platform_browser__WEBPACK_IMPORTED_MODULE_3__["platformBrowser"]().bootstrapModule(_app_App_module__WEBPACK_IMPORTED_MODULE_2__["AppModule"])["catch"](function (err) {
        return console.error(err);
      });
      /***/

    },

    /***/
    "zYQP":
    /*!********************************************************!*\
      !*** ./src/core/codeshell/validators/dateValidator.ts ***!
      \********************************************************/

    /*! exports provided: DateValidator */

    /***/
    function zYQP(module, __webpack_exports__, __webpack_require__) {
      "use strict";

      __webpack_require__.r(__webpack_exports__);
      /* harmony export (binding) */


      __webpack_require__.d(__webpack_exports__, "DateValidator", function () {
        return DateValidator;
      });
      /* harmony import */


      var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
      /*! @angular/core */
      "fXoL");
      /* harmony import */


      var _utilities_utils__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
      /*! ../utilities/utils */
      "VXkE");
      /* harmony import */


      var moment__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
      /*! moment */
      "wd/R");
      /* harmony import */


      var moment__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(moment__WEBPACK_IMPORTED_MODULE_2__);
      /* harmony import */


      var _angular_forms__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
      /*! @angular/forms */
      "3Pt+");

      var DateValidator = /*#__PURE__*/function () {
        function DateValidator(model, el) {
          _classCallCheck(this, DateValidator);

          this.model = model;
          this.IsRequired = false;
          var elem = el.nativeElement;
          this.IsRequired = elem.hasAttribute("required");
        }

        _createClass(DateValidator, [{
          key: "compareDates",
          value: function compareDates(date1, date2) {
            if (moment__WEBPACK_IMPORTED_MODULE_2__["isMoment"](date1)) {
              var e = date1;
              date1 = e.toDate();
            }

            if (moment__WEBPACK_IMPORTED_MODULE_2__["isMoment"](date2)) {
              var e = date2;
              date2 = e.toDate();
            } //debugger


            date1.setHours(0, 0, 0, 0);
            date2.setHours(0, 0, 0, 0);
            console.log(date1, date2);
            if (date1 >= date2) return 1;else return 2;
          }
        }, {
          key: "ngOnInit",
          value: function ngOnInit() {
            var _this62 = this;

            this.model.update.subscribe(function (v) {
              if (!moment__WEBPACK_IMPORTED_MODULE_2__["isMoment"](v)) v = new Date(v);
              setTimeout(function () {
                var min = _this62.minDate();

                var max = _this62.maxDate();

                var isValid = true;

                if (min != null) {
                  isValid = _this62.compareDates(v, min) == 1;
                }

                if (max != null && isValid) {
                  isValid = _this62.compareDates(v, max) == 2;
                }

                if (isValid) {
                  _this62.model.control.setErrors({
                    date_validation: null
                  });

                  _this62.model.control.updateValueAndValidity();
                } else {
                  _this62.model.control.setErrors({
                    date_validation: true
                  });
                }
              }, 200);
            });
          }
        }, {
          key: "minDate",
          value: function minDate() {
            if (this.Type == "future") return new Date();

            if (this.Min) {
              return Object(_utilities_utils__WEBPACK_IMPORTED_MODULE_1__["Date_Get"])(this.Min);
            }

            return null;
          }
        }, {
          key: "maxDate",
          value: function maxDate() {
            if (this.Type == "past") return new Date();

            if (this.Max) {
              return Object(_utilities_utils__WEBPACK_IMPORTED_MODULE_1__["Date_Get"])(this.Max);
            }

            return null;
          }
        }]);

        return DateValidator;
      }();

      DateValidator.ɵfac = function DateValidator_Factory(t) {
        return new (t || DateValidator)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_forms__WEBPACK_IMPORTED_MODULE_3__["NgModel"]), _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]));
      };

      DateValidator.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({
        type: DateValidator,
        selectors: [["", "date-validate", "", "ngModel", ""]],
        inputs: {
          Min: ["min-date", "Min"],
          Max: ["max-date", "Max"],
          Type: ["date-validate", "Type"]
        },
        exportAs: ["date-validate"]
      });
      /*@__PURE__*/

      (function () {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵsetClassMetadata"](DateValidator, [{
          type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"],
          args: [{
            selector: '[date-validate][ngModel]',
            exportAs: 'date-validate'
          }]
        }], function () {
          return [{
            type: _angular_forms__WEBPACK_IMPORTED_MODULE_3__["NgModel"]
          }, {
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"]
          }];
        }, {
          Min: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["min-date"]
          }],
          Max: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["max-date"]
          }],
          Type: [{
            type: _angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"],
            args: ["date-validate"]
          }]
        });
      })();
      /***/

    },

    /***/
    "zn8P":
    /*!******************************************************!*\
      !*** ./$$_lazy_route_resource lazy namespace object ***!
      \******************************************************/

    /*! no static exports found */

    /***/
    function zn8P(module, exports) {
      function webpackEmptyAsyncContext(req) {
        // Here Promise.resolve().then() is used instead of new Promise() to prevent
        // uncaught exception popping up in devtools
        return Promise.resolve().then(function () {
          var e = new Error("Cannot find module '" + req + "'");
          e.code = 'MODULE_NOT_FOUND';
          throw e;
        });
      }

      webpackEmptyAsyncContext.keys = function () {
        return [];
      };

      webpackEmptyAsyncContext.resolve = webpackEmptyAsyncContext;
      module.exports = webpackEmptyAsyncContext;
      webpackEmptyAsyncContext.id = "zn8P";
      /***/
    }
  }, [[0, "runtime", "vendor"]]]);
})();
//# sourceMappingURL=main-es5.js.map