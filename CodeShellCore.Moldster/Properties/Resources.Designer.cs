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
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
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
        ///   Looks up a localized string similar to &lt;div [@loader]=&quot;ShowLoader ? &apos;shown&apos; : &apos;hidden&apos;&quot; class=&quot;loader-overlay&quot;&gt;
        ///    &lt;div class=&quot;loader&quot;&gt;&lt;/div&gt;
        ///&lt;/div&gt;
        ///
        ///&lt;div&gt;
        ///    &lt;div class=&quot;wrapper&quot;&gt;
        ///        &lt;div class=&quot;wrapper-content&quot;&gt;
        ///            &lt;router-outlet&gt;&lt;/router-outlet&gt;
        ///        &lt;/div&gt;
        ///    &lt;/div&gt;
        ///&lt;/div&gt;
        ///
        ///&lt;div [ngClass]=&quot;Config.Locale==&apos;ar&apos;? &apos;ui-rtl&apos;:null&quot; [dir]=&quot;Config.Locale==&apos;ar&apos;? &apos;rtl&apos;:null&quot;&gt;
        ///    &lt;ng-template test-loader&gt;&lt;/ng-template&gt;
        ///
        ///    &lt;p-dialog #deleteDialog [(visible)]=&quot;deleteDialogShow&quot; [modal]=&quot;true&quot; width=&quot;600&quot;&gt;
        ///        &lt;p- [rest of string was truncated]&quot;;.
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
        ///   Looks up a localized string similar to import { Injector } from &quot;@angular/core&quot;;
        ///import { Title } from &quot;@angular/platform-browser&quot;;
        ///import { ServerConfigBase } from &quot;codeshell/core&quot;;
        ///import { IAppComponent } from &quot;codeshell/baseComponents&quot;;
        ///import { TestLoader } from &quot;codeshell/core&quot;;
        ///
        ///export class AppComponentBase extends IAppComponent {
        ///    ModalLoader?: TestLoader | undefined;
        ///    constructor(inj: Injector, trans: Title, conf: ServerConfigBase) {
        ///        super(inj, trans, conf);
        ///    }
        ///}.
        /// </summary>
        public static string AppComponentBase_ts {
            get {
                return ResourceManager.GetString("AppComponentBase_ts", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///  &quot;UseHotUpdate&quot;: true,
        ///  &quot;env&quot;: &quot;dev&quot;,
        ///  &quot;Services&quot;: {
        ///	
        ///  }
        ///}.
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
        public static byte[] Component_ts {
            get {
                object obj = ResourceManager.GetObject("Component_ts", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to create function [dbo].[CompareVersion](
        ///	@v1 varchar(100),
        ///	@v2 varchar(100)
        ///)
        ///returns int
        ///AS
        ///begin
        ///declare @result int;
        ///
        ///    select @result=case 
        ///    when CONVERT(int, LEFT(@v1, CHARINDEX(&apos;.&apos;, @v1)-1)) &lt; CONVERT(int, LEFT(@v2, CHARINDEX(&apos;.&apos;, @v2)-1)) then 2
        ///    when CONVERT(int, LEFT(@v1, CHARINDEX(&apos;.&apos;, @v1)-1)) &gt; CONVERT(int, LEFT(@v2, CHARINDEX(&apos;.&apos;, @v2)-1)) then 1
        ///    when CONVERT(int, substring(@v1, CHARINDEX(&apos;.&apos;, @v1)+1, LEN(@v1))) &lt; CONVERT(int, substring(@v2, CHARINDEX(&apos;.&apos;, @v2)+1, LEN(@ [rest of string was truncated]&quot;;.
        /// </summary>
        public static string Creation {
            get {
                return ResourceManager.GetString("Creation", resourceCulture);
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
        public static byte[] DomainModule_ts {
            get {
                object obj = ResourceManager.GetObject("DomainModule_ts", resourceCulture);
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
        ///    &lt;link href=&quot;https://fonts.googleapis.com/css?family=Cairo:700|Changa:400,700|Tajawal&quot; rel=&quot;stylesheet&quot;&gt;
        ///
        ///    &lt;link href=&quot;~/css/plugins/bootstrap@(Model.Config.Locale==&quot;ar&quot;?&quot;.rtl&quot;:&quot;&quot;).css&quot; rel=&quot;stylesheet&quot; /&gt;
        ///    &lt;link href=&quot;~/css/plugins/animate.css [rest of string was truncated]&quot;;.
        /// </summary>
        public static string Index_cshtml {
            get {
                return ResourceManager.GetString("Index_cshtml", resourceCulture);
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
        ///   Looks up a localized string similar to &lt;section class=&quot;row&quot;&gt;
        ///
        ///    &lt;div class=&quot;col-md-6 col-md-offset-3 col-sm-12 col-sm-offset-0&quot;&gt;
        ///        &lt;div class=&quot;title-header text-center&quot;&gt;
        ///            &lt;h1&gt;{{&apos;Words.Log__&apos; | translate}} &lt;span&gt;{{&apos;Words.In__&apos; | translate}}&lt;/span&gt;&lt;/h1&gt;
        ///        &lt;/div&gt;
        ///        &lt;div class=&quot;border&quot;&gt;&lt;/div&gt;
        ///        &lt;br /&gt;
        ///        &lt;br /&gt;
        ///
        ///        &lt;form class=&quot;order-price &quot; #Form=&quot;ngForm&quot;&gt;
        ///            &lt;div class=&quot;row&quot;&gt;
        ///                &lt;div class=&quot;col-md-10 col-md-offset-1 col-sm-10 col-sm-offset-1 &quot;&gt;
        ///
        ///                     [rest of string was truncated]&quot;;.
        /// </summary>
        public static string Login_html {
            get {
                return ResourceManager.GetString("Login_html", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to import { Component } from &apos;@angular/core&apos;;
        ///import { BaseComponent } from &apos;codeshell/baseComponents&apos;;
        ///import { Shell } from &apos;codeshell/core&apos;;
        ///import { AccountServiceBase } from &apos;codeshell/security&apos;;
        ///import { NoteType } from &apos;codeshell/helpers&apos;;
        ///
        ///@Component({ templateUrl: &quot;./Login.html&quot; })
        ///export class Login extends BaseComponent {
        ///
        ///    AccountService: AccountServiceBase = Shell.Injector.get(AccountServiceBase);
        ///    model: any = {};
        ///    GetPageId(): number { return 0; }
        ///
        ///    Login() {
        ///        th [rest of string was truncated]&quot;;.
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
        ///   Looks up a localized string similar to {
        ///  &quot;dependencies&quot;: {
        ///    &quot;angular-tree-component&quot;: &quot;^8.0.0&quot;,
        ///    &quot;angular2-datetimepicker&quot;: &quot;^1.1.1&quot;,
        ///    &quot;primeicons&quot;: &quot;^1.0.0-beta.10&quot;,
        ///    &quot;primeng&quot;: &quot;6.1.3&quot;
        ///  },
        ///  &quot;devDependencies&quot;: {
        ///    &quot;@agm/core&quot;: &quot;1.0.0-beta.2&quot;,
        ///    &quot;@angular/animations&quot;: &quot;5.2.5&quot;,
        ///    &quot;@angular/cdk&quot;: &quot;5.2.5&quot;,
        ///    &quot;@angular/cli&quot;: &quot;6.1.1&quot;,
        ///    &quot;@angular/common&quot;: &quot;5.2.5&quot;,
        ///    &quot;@angular/compiler&quot;: &quot;5.2.5&quot;,
        ///    &quot;@angular/compiler-cli&quot;: &quot;5.2.5&quot;,
        ///    &quot;@angular/core&quot;: &quot;5.2.5&quot;,
        ///    &quot;@angular/forms&quot;: &quot;5.2.5&quot;,
        ///    &quot;@angular [rest of string was truncated]&quot;;.
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
        ///   Looks up a localized string similar to import { ServerConfigBase } from &quot;CodeShell/core&quot;;
        ///
        ///export class ServerConfig implements ServerConfigBase {
        ///    GoogleKey: string = &quot;&quot;;
        ///    BaseURL: string = &quot;&quot;;
        ///    Domain: string = &quot;&quot;;
        ///    Locale: string = &quot;&quot;;
        ///
        ///    constructor() {
        ///        let item: HTMLElement = document.getElementById(&quot;view-data&quot;) as HTMLElement;
        ///        if (item)
        ///            Object.assign(this, JSON.parse(item.innerHTML));
        ///    }
        ///
        ///    private static _config: ServerConfig;
        ///    public static Instance: ServerConfig = new Serv [rest of string was truncated]&quot;;.
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
        ///    &apos;@angular/core&apos;,
        ///    &apos;@angular/common&apos;,
        ///    &apos;@angular/forms&apos;,
        ///    &apos;@angular/http&apos;,
        ///    &apos;@angular/animations&apos;,
        ///    &apos;@angular/platform-browser&apos;,
        ///    &quot;@angular/platform-browser/animations&quot;,
        ///    &apos;@angular/platform-browser-dynamic&apos;,
        ///    &apos;@angular/router&apos;,
        ///
        ///    &apos;@angular/co [rest of string was truncated]&quot;;.
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
        ///
        ///module.exports = function WebpackSharedConfig(env) {
        ///    const isDevBuild = !(env &amp;&amp; env.prod);
        ///    const app = env.app ? env.app + &quot;-&quot; : &quot;&quot;;
        ///    const version = isDevBuild ? &quot;dev&quot; : &quot;v&quot; + (env.version ? env.version : &quot;1.0.0.0&quot;)
        ///    const clientBundleOutputDir = &apos;./wwwroot/dist&apos;;
        ///    return {
        ///        mode: isDevBuild ? &quot;development&quot; : &quot;production&quot;,
        ///        s [rest of string was truncated]&quot;;.
        /// </summary>
        public static string WebPackSharedConfig_js {
            get {
                return ResourceManager.GetString("WebPackSharedConfig_js", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] wwwroot {
            get {
                object obj = ResourceManager.GetObject("wwwroot", resourceCulture);
                return ((byte[])(obj));
            }
        }
    }
}
