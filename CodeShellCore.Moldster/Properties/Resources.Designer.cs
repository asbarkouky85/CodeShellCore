﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CodeShellCore.Moldster.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CodeShellCore.Moldster.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to import { AccountServiceBase } from &quot;codeshell/security&quot;;
        ///
        ///export class AccountService extends AccountServiceBase {
        ///    protected BaseUrl = &quot;/apiAction/Account&quot;;
        ///}.
        /// </summary>
        public static string AccountService_ts {
            get {
                return ResourceManager.GetString("AccountService_ts", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;div&gt;
        ///    &lt;div class=&quot;wrapper&quot;&gt;
        ///        &lt;app-top-bar #topBar&gt;&lt;/app-top-bar&gt;
        ///        &lt;div class=&quot;wrapper-side&quot; *ngIf=&quot;IsLoggedIn &amp;&amp; ShowNav&quot;&gt;
        ///            &lt;app-navigation-side-bar&gt;&lt;/app-navigation-side-bar&gt;
        ///        &lt;/div&gt;
        ///        &lt;div class=&quot;wrapper-content&quot; [ngClass]=&quot;!IsLoggedIn || !ShowNav?&apos;expanded&apos;:null&quot;&gt;
        ///            &lt;router-outlet&gt;&lt;/router-outlet&gt;
        ///        &lt;/div&gt;
        ///    &lt;/div&gt;
        ///&lt;/div&gt;
        ///
        ///&lt;ng-template test-loader&gt;&lt;/ng-template&gt;
        ///.
        /// </summary>
        public static string AppComponent_cshtml {
            get {
                return ResourceManager.GetString("AppComponent_cshtml", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] AppComponent_ts {
            get {
                object obj = ResourceManager.GetObject("AppComponent_ts", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to import { ViewChild } from &quot;@angular/core&quot;;
        ///import { IAppComponent } from &quot;codeshell/base-components&quot;;
        ///import { TestLoader } from &quot;codeshell/directives&quot;;
        ///
        ///export class AppComponentBase extends IAppComponent {
        ///
        ///    @ViewChild(TestLoader)
        ///    ModalLoader?: TestLoader | undefined;
        ///    
        ///}.
        /// </summary>
        public static string AppComponentBase_ts {
            get {
                return ResourceManager.GetString("AppComponentBase_ts", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///  
        ///}
        ///.
        /// </summary>
        public static string appsettings_json {
            get {
                return ResourceManager.GetString("appsettings_json", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] BaseComponent_ts {
            get {
                object obj = ResourceManager.GetObject("BaseComponent_ts", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] BaseModule_ts {
            get {
                object obj = ResourceManager.GetObject("BaseModule_ts", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] BasicComponent_ts {
            get {
                object obj = ResourceManager.GetObject("BasicComponent_ts", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] Boot_ts {
            get {
                object obj = ResourceManager.GetObject("Boot_ts", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] codeshell {
            get {
                object obj = ResourceManager.GetObject("codeshell", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] Component_ts {
            get {
                object obj = ResourceManager.GetObject("Component_ts", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 牣慥整映湵瑣潩⁮摛潢⹝䍛浯慰敲敖獲潩嵮നऊ癀‱慶捲慨⡲〱⤰ബऊ癀′慶捲慨⡲〱⤰਍ഩ爊瑥牵獮椠瑮਍十਍敢楧൮搊捥慬敲䀠敲畳瑬椠瑮഻ഊ †猠汥捥⁴牀獥汵㵴慣敳ഠ †眠敨⁮佃噎剅⡔湩ⱴ䰠䙅⡔癀ⰱ䌠䅈䥒䑎塅✨✮‬癀⤱ㄭ⤩㰠䌠乏䕖呒椨瑮‬䕌呆䀨㉶‬䡃剁义䕄⡘⸧Ⱗ䀠㉶⴩⤱ ///桴湥㈠਍††桷湥䌠乏䕖呒椨瑮‬䕌呆䀨ㅶ‬䡃剁义䕄⡘⸧Ⱗ䀠ㅶ⴩⤱ ///‾佃噎剅⡔湩ⱴ䰠䙅⡔癀ⰲ䌠䅈䥒䑎塅✨✮‬癀⤲ㄭ⤩琠敨⁮റ †眠敨⁮佃噎剅⡔湩ⱴ猠扵瑳楲杮䀨ㅶ‬䡃剁义䕄⡘⸧Ⱗ䀠ㅶ⬩ⰱ䰠久䀨ㅶ⤩ ///‼佃噎剅⡔湩ⱴ猠扵瑳楲杮䀨㉶‬䡃剁义䕄⡘⸧Ⱗ䀠㉶⬩ⰱ䰠久䀨ㅶ⤩ ///桴湥㈠਍††桷湥䌠乏䕖呒椨瑮‬畳獢牴湩⡧癀ⰱ䌠䅈䥒䑎塅✨✮‬癀⤱ㄫ‬䕌⡎癀⤱⤩㸠䌠乏䕖呒椨瑮‬畳獢牴湩⡧癀ⰲ䌠䅈䥒䑎塅✨✮‬癀⤲ㄫ‬䕌⡎癀⤱⤩琠敨⁮റ †攠獬⁥‰湥㭤਍敲畴湲䀠敲畳瑬਍湥㭤਍਍਍਍਍佇਍⨯⨪⨪‪扏敪瑣›唠敳䑲晥湩摥畆据楴湯嬠扤嵯嬮敇敮慲整摉⁝†匠牣灩⁴慄整›〱㠯㈯㈰‰㨹㌰ㄺ‸䵐⨠⨪⨪⼪਍䕓⁔乁䥓也䱕卌传ൎ䜊൏匊呅儠何䕔彄䑉久䥔䥆剅传ൎ䜊൏ഊ䌊䕒呁⁅商䍎䥔乏嬠扤嵯嬮敇敮慲整摉൝⠊਍䀉楴敭䐠瑡瑥浩ⱥ਍䀉潲乷浵椠瑮਍ഩ刊呅剕华戠杩湩൴䄊൓䈊䝅义਍ⴉ‭敄汣牡⁥桴⁥敲畴湲瘠牡慩汢⁥敨敲਍䐉䍅䅌䕒 [rest of string was truncated]&quot;;.
        /// </summary>
        public static string Creation {
            get {
                return ResourceManager.GetString("Creation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] css {
            get {
                object obj = ResourceManager.GetObject("css", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to declare module &quot;*.json&quot; {
        ///    const value: any;
        ///    export default value;
        ///}
        ///declare var jQuery: any;
        ///declare var $: any;
        ///.
        /// </summary>
        public static string declarations_d {
            get {
                return ResourceManager.GetString("declarations_d", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] DomainModule_Lazy_ts {
            get {
                object obj = ResourceManager.GetObject("DomainModule_Lazy_ts", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] img {
            get {
                object obj = ResourceManager.GetObject("img", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!DOCTYPE&gt;
        ///@model CodeShellCore.Web.Moldster.IndexModel
        ///&lt;html&gt;
        ///&lt;head&gt;
        ///    &lt;title&gt;@Model.Title&lt;/title&gt;
        ///    &lt;base href=&quot;@Model.Config.BaseURL&quot; /&gt;
        ///    &lt;meta charset=&quot;utf-8&quot;&gt;
        ///    &lt;meta name=&quot;viewport&quot; content=&quot;width=device-width, initial-scale=1.0&quot;&gt;
        ///
        ///    &lt;link href=&quot;https://fonts.googleapis.com/css?family=Armata&amp;display=swap&quot; rel=&quot;stylesheet&quot;&gt;
        ///    &lt;link href=&quot;https://fonts.googleapis.com/css?family=Tajawal&amp;display=swap&quot; rel=&quot;stylesheet&quot;&gt;
        ///    &lt;link href=&quot;https://fonts.googleapis.com/css?family=Titilli [rest of string was truncated]&quot;;.
        /// </summary>
        public static string Index_cshtml {
            get {
                return ResourceManager.GetString("Index_cshtml", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] js {
            get {
                object obj = ResourceManager.GetObject("js", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] LocaleLoader_ts {
            get {
                object obj = ResourceManager.GetObject("LocaleLoader_ts", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] Localization {
            get {
                object obj = ResourceManager.GetObject("Localization", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;div class=&quot;container&quot;&gt;
        ///    &lt;section class=&quot;row&quot;&gt;
        ///        &lt;br /&gt;
        ///        &lt;br /&gt;
        ///        &lt;div class=&quot;col-md-4 col-md-offset-4 col-sm-12 col-sm-offset-0 content-block animated fadeInDownBig&quot;&gt;
        ///            &lt;br class=&quot;not-mob&quot; /&gt;
        ///            &lt;div class=&quot;title-header text-center page-header&quot;&gt;
        ///                &lt;h2&gt;{{&apos;Words.Log__&apos; | translate}} &lt;span&gt;{{&apos;Words.In__&apos; | translate}}&lt;/span&gt;&lt;/h2&gt;
        ///            &lt;/div&gt;
        ///
        ///            &lt;form class=&quot;order-price &quot; #Form=&quot;ngForm&quot;&gt;
        ///                &lt;div class=&quot;row&quot;&gt;
        ///       [rest of string was truncated]&quot;;.
        /// </summary>
        public static string Login_html {
            get {
                return ResourceManager.GetString("Login_html", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to import { Component } from &apos;@angular/core&apos;;
        ///import { LoginBase } from &apos;codeshell/base-components&apos;;
        ///
        ///@Component({ templateUrl: &quot;./login.component.html&quot; })
        ///export class Login extends LoginBase {
        ///    ForgotPasswordUrl = &quot;/Auth/ForgotPassword&quot;;
        ///}.
        /// </summary>
        public static string Login_ts {
            get {
                return ResourceManager.GetString("Login_ts", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] ModalComponent_ts {
            get {
                object obj = ResourceManager.GetObject("ModalComponent_ts", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] Module_ts {
            get {
                object obj = ResourceManager.GetObject("Module_ts", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///&lt;nav class=&quot;navbar-inverse nav-side&quot;&gt;
        ///
        ///    &lt;ul class=&quot;nav&quot;&gt;
        ///        &lt;li&gt;
        ///            &lt;a [routerLink]=&quot;GetMainUrl()&quot;&gt;{{&apos;Words.Main&apos; | translate}}&lt;/a&gt;
        ///        &lt;/li&gt;
        ///        &lt;li *ngFor=&quot;let it of navs&quot;&gt;
        ///            &lt;a [routerLink]=&quot;&apos;/&apos;+it.url&quot; routerLinkActive=&quot;active&quot;&gt;{{&apos;Pages.&apos;+it.name | translate}}&lt;/a&gt;
        ///        &lt;/li&gt;
        ///    &lt;/ul&gt;
        ///&lt;/nav&gt;.
        /// </summary>
        public static string navigationSideBar_html {
            get {
                return ResourceManager.GetString("navigationSideBar_html", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to import { Component, Injector } from &quot;@angular/core&quot;;
        ///import { NavigationSideBarBase } from &quot;codeshell/base-components&quot;;
        ///
        ///@Component({ templateUrl: &quot;./navigation-side-bar.component.html&quot;, selector: &quot;app-navigation-side-bar&quot; })
        ///export class NavigationSideBar extends NavigationSideBarBase {
        ///
        ///    constructor(inj:Injector){
        ///        super(inj);
        ///    }
        ///}.
        /// </summary>
        public static string navigationSideBar_ts {
            get {
                return ResourceManager.GetString("navigationSideBar_ts", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///  &quot;dependencies&quot;: {
        ///    &quot;@agm/core&quot;: &quot;1.0.0-beta.5&quot;,
        ///    &quot;@angular/animations&quot;: &quot;6.1.10&quot;,
        ///    &quot;@angular/cdk&quot;: &quot;6.4.7&quot;,
        ///    &quot;@angular/common&quot;: &quot;6.1.10&quot;,
        ///    &quot;@angular/compiler&quot;: &quot;6.1.10&quot;,
        ///    &quot;@angular/core&quot;: &quot;6.1.10&quot;,
        ///    &quot;@angular/forms&quot;: &quot;6.1.10&quot;,
        ///    &quot;@angular/material&quot;: &quot;6.4.7&quot;,
        ///    &quot;@angular/material-moment-adapter&quot;: &quot;6.4.7&quot;,
        ///    &quot;@angular/platform-browser&quot;: &quot;6.1.10&quot;,
        ///    &quot;@angular/platform-browser-dynamic&quot;: &quot;6.1.10&quot;,
        ///    &quot;@angular/router&quot;: &quot;6.1.10&quot;,
        ///    &quot;@aspnet/signalr&quot;: &quot;^1.1.4&quot;,
        ///   [rest of string was truncated]&quot;;.
        /// </summary>
        public static string package_json {
            get {
                return ResourceManager.GetString("package_json", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] ParentRoute_ts {
            get {
                object obj = ResourceManager.GetObject("ParentRoute_ts", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /***************************************************************************************************
        /// * Load `$localize` onto the global scope - used if i18n tags appear in Angular templates.
        /// */
        ///import &apos;@angular/localize/init&apos;;
        ////**
        /// * This file includes polyfills needed by Angular and is loaded before the app.
        /// * You can add your own extra polyfills to this file.
        /// *
        /// * This file is divided into 2 sections:
        /// *   1. Browser polyfills. These are applied before loading ZoneJS and are sorted by browser [rest of string was truncated]&quot;;.
        /// </summary>
        public static string pollyfills_ts {
            get {
                return ResourceManager.GetString("pollyfills_ts", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] Route_ts {
            get {
                object obj = ResourceManager.GetObject("Route_ts", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] Routes_ts {
            get {
                object obj = ResourceManager.GetObject("Routes_ts", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to import { ServerConfigBase } from &quot;codeshell/core&quot;;
        ///
        ///export class ServerConfig implements ServerConfigBase {
        ///    GoogleKey: string = &quot;&quot;;
        ///	DefaultLocale: string = &quot;&quot;;
        ///    Version?: string | null | undefined;
        ///    BaseURL: string = &quot;&quot;;
        ///    Domain: string = &quot;&quot;;
        ///    Locale: string = &quot;&quot;;
        ///	Urls : any = {};
        ///
        ///    constructor() {
        ///        let item: HTMLElement = document.getElementById(&quot;view-data&quot;) as HTMLElement;
        ///        if (item)
        ///            Object.assign(this, JSON.parse(item.innerHTML));
        ///    }
        ///}.
        /// </summary>
        public static string ServerConfig_ts {
            get {
                return ResourceManager.GetString("ServerConfig_ts", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] Service_ts {
            get {
                object obj = ResourceManager.GetObject("Service_ts", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] ServicedBaseComponent_ts {
            get {
                object obj = ResourceManager.GetObject("ServicedBaseComponent_ts", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] SharedModule_ts {
            get {
                object obj = ResourceManager.GetObject("SharedModule_ts", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] ShellComponents {
            get {
                object obj = ResourceManager.GetObject("ShellComponents", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;nav class=&quot;top-bar&quot;&gt;
        ///    &lt;div class=&quot;container-fluid&quot;&gt;
        ///        &lt;div class=&quot;row&quot;&gt;
        ///            &lt;div class=&quot;pull-first&quot;&gt;
        ///                &lt;ul class=&quot;nav nav-pills&quot;&gt;
        ///                    &lt;li&gt;
        ///                        &lt;a (click)=&quot;Slide()&quot; class=&quot;opt-btn mob&quot;&gt;
        ///                            &lt;i class=&quot;fa fa-bars&quot;&gt;&lt;/i&gt;
        ///                        &lt;/a&gt;
        ///                        &lt;a (click)=&quot;ToggleNav()&quot; class=&quot;opt-btn not-mob&quot;&gt;
        ///                            &lt;i class=&quot;fa fa-bars&quot;&gt;&lt;/i&gt;
        ///                        &lt;/a&gt;
        ///      [rest of string was truncated]&quot;;.
        /// </summary>
        public static string topBar_html {
            get {
                return ResourceManager.GetString("topBar_html", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to import { Component } from &quot;@angular/core&quot;;
        ///import { TopBarBase } from &quot;codeshell/base-components&quot;;
        ///
        ///@Component({ templateUrl: &quot;./top-bar.component.html&quot;, selector: &quot;app-top-bar&quot;, exportAs: &quot;app-top-bar&quot; })
        ///export class TopBar extends TopBarBase {
        ///
        ///}.
        /// </summary>
        public static string topBar_ts {
            get {
                return ResourceManager.GetString("topBar_ts", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///  &quot;compilerOptions&quot;: {
        ///    &quot;module&quot;: &quot;es2015&quot;,
        ///    &quot;moduleResolution&quot;: &quot;node&quot;,
        ///    &quot;target&quot;: &quot;es5&quot;,
        ///    &quot;sourceMap&quot;: true,    
        ///    &quot;experimentalDecorators&quot;: true,
        ///    &quot;strictNullChecks&quot;: true,
        ///    &quot;emitDecoratorMetadata&quot;: true,
        ///    &quot;skipDefaultLibCheck&quot;: true,
        ///    &quot;skipLibCheck&quot;: true, // Workaround for https://github.com/angular/angular/issues/17863. Remove this if you upgrade to a fixed version of Angular.
        ///    &quot;strict&quot;: true,
        ///    &quot;lib&quot;: [ &quot;es6&quot;, &quot;dom&quot; ],
        ///    &quot;types&quot;: [ &quot;webpack-env&quot; ],
        ///    [rest of string was truncated]&quot;;.
        /// </summary>
        public static string tsconfig_json {
            get {
                return ResourceManager.GetString("tsconfig_json", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [SID.Config]
        ///GO
        ////****** Object:  StoredProcedure [dbo].[SyncAuthDb]    Script Date: 5/4/2020 12:45:22 AM ******/
        ///SET ANSI_NULLS ON
        ///GO
        ///SET QUOTED_IDENTIFIER ON
        ///GO
        ///ALTER PROCEDURE [dbo].[SyncAuthDb](
        /// @tenantId bigint,
        /// @dbName nvarchar(100)
        ///)
        ///AS
        ///BEGIN
        ///	SET NOCOUNT ON;
        ///
        ///
        ///	declare @sql nvarchar(max)=&apos;&apos;;
        ///
        ///	set @sql =&apos;insert into [&apos;+@dbName+&apos;].Auth.Domains (Id,Name,CreatedOn,UpdatedOn)
        ///	select Id,Name,CreatedOn,UpdatedOn
        ///	from Domains
        ///	where 
        ///		ParentId is null AND 
        ///		Id not in (select [rest of string was truncated]&quot;;.
        /// </summary>
        public static string update_v2_10_3 {
            get {
                return ResourceManager.GetString("update_v2_10_3", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to exec sp_rename &apos;dbo.DomainEntityCollections&apos;, &apos;ResourceCollections&apos;;
        ///GO
        ///exec sp_rename &apos;dbo.TenantApps&apos;,&apos;Apps&apos;;
        ///GO
        ///exec sp_rename &apos;dbo.EntityCollectionConditions&apos;,&apos;ResourceCollectionConditions&apos;;
        ///GO
        ///
        ///alter table ResourceCollections drop constraint FK_DomainEntityCollections_DomainEntities;
        ///alter table ResourceCollections drop column DomainEntityId;
        ///
        ///alter table PageCategories drop constraint FK_PageCategories_DomainEntities1;
        ///alter table PageCategories drop column DomainEntityId;
        ///alter table Page [rest of string was truncated]&quot;;.
        /// </summary>
        public static string update_v2_15_0 {
            get {
                return ResourceManager.GetString("update_v2_15_0", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///CREATE TABLE [dbo].[PageCategoryParameters](
        ///	[Id] [bigint] NOT NULL,
        ///	[Name] [varchar](50) NULL,
        ///	[Type] [int] NOT NULL,
        ///	[DefaultValue] [varchar](max) NULL,
        ///	[PageCategoryId] [bigint] NOT NULL,
        ///	[CreatedOn] [datetime] NULL,
        ///	[CreatedBy] [bigint] NULL,
        ///	[UpdatedOn] [datetime] NULL,
        ///	[UpdatedBy] [bigint] NULL,
        /// CONSTRAINT [PK_PageCategoryParameters] PRIMARY KEY CLUSTERED 
        ///(
        ///	[Id] ASC
        ///)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_L [rest of string was truncated]&quot;;.
        /// </summary>
        public static string update_v2_6_0 {
            get {
                return ResourceManager.GetString("update_v2_6_0", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TABLE [dbo].[CustomFields](
        ///	[Id] [bigint] NOT NULL,
        ///	[Name] [varchar](150) NULL,
        ///	[Type] [varchar](50) NULL,
        ///	[PageId] [bigint] NOT NULL,
        ///	[CreatedOn] [datetime] NULL,
        ///	[CreatedBy] [bigint] NULL,
        ///	[UpdatedOn] [datetime] NULL,
        ///	[UpdatedBy] [bigint] NULL,
        /// CONSTRAINT [PK_CustomFields] PRIMARY KEY CLUSTERED 
        ///(
        ///	[Id] ASC
        ///)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ///) ON [PRIMARY]
        ///
        ///GO
        ///
        ///ALTER TABLE [d [rest of string was truncated]&quot;;.
        /// </summary>
        public static string update_v2_8_0 {
            get {
                return ResourceManager.GetString("update_v2_8_0", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] webpack_config_js {
            get {
                object obj = ResourceManager.GetObject("webpack_config_js", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to const path = require(&apos;path&apos;);
        ///const webpack = require(&apos;webpack&apos;);
        ///const ExtractTextPlugin = require(&apos;extract-text-webpack-plugin&apos;);
        ///const UglifyJsPlugin = require(&apos;uglifyjs-webpack-plugin&apos;);
        ///
        ///const treeShakableModules = [
        ///
        ///    &apos;@angular/common&apos;,
        ///    &apos;@angular/forms&apos;,
        ///    &apos;@angular/animations&apos;,
        ///    &apos;@angular/platform-browser&apos;,
        ///    &quot;@angular/platform-browser/animations&quot;,
        ///    &apos;@angular/platform-browser-dynamic&apos;,
        ///    &apos;@angular/router&apos;,
        ///
        ///    &apos;@angular/compiler&apos;,
        ///
        ///    &apos;@angular/material&apos;,
        ///    &quot;@ [rest of string was truncated]&quot;;.
        /// </summary>
        public static string webpack_config_vendor_js {
            get {
                return ResourceManager.GetString("webpack_config_vendor_js", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] webpack_tenant_js {
            get {
                object obj = ResourceManager.GetObject("webpack_tenant_js", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] webpack_tenant_js_bat {
            get {
                object obj = ResourceManager.GetObject("webpack_tenant_js_bat", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] webpack_tenant_js_json {
            get {
                object obj = ResourceManager.GetObject("webpack_tenant_js_json", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to const path = require(&apos;path&apos;);
        ///const webpack = require(&apos;webpack&apos;);
        ///const CheckerPlugin = require(&apos;awesome-typescript-loader&apos;).CheckerPlugin;
        ///const ExtractTextPlugin = require(&apos;extract-text-webpack-plugin&apos;);
        ///
        ///module.exports = function WebpackSharedConfig(env) {
        ///    const isDevBuild = !(env &amp;&amp; env.prod);
        ///    const app = env.app ? env.app + &quot;-&quot; : &quot;&quot;;
        ///    const version = isDevBuild ? &quot;dev&quot; : &quot;v&quot; + (env.version ? env.version : &quot;1.0.0.0&quot;)
        ///    const clientBundleOutputDir = &apos;./wwwroot/dist&apos;;
        ///    return {
        /// [rest of string was truncated]&quot;;.
        /// </summary>
        public static string WebPackSharedConfig_js {
            get {
                return ResourceManager.GetString("WebPackSharedConfig_js", resourceCulture);
            }
        }
    }
}
