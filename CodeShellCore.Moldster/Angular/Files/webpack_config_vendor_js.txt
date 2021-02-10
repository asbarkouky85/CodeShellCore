const path = require('path');
const webpack = require('webpack');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const UglifyJsPlugin = require('uglifyjs-webpack-plugin');

const treeShakableModules = [

    '@angular/common',
    '@angular/forms',
    '@angular/animations',
    '@angular/platform-browser',
    "@angular/platform-browser/animations",
    '@angular/platform-browser-dynamic',
    '@angular/router',

    '@angular/compiler',

    '@angular/material',
    "@angular/material-moment-adapter",
    
    "./node_modules/@ngx-translate/core",
    "./node_modules/ngx-toastr",
    "./node_modules/angular-tree-component",
    "./node_modules/primeng/dialog"
];
const nonTreeShakableModules = [

    
    'zone.js',
    "reflect-metadata",

    'bootstrap',
    'es6-promise',
    'es6-shim',
    'event-source-polyfill',
    'jquery',
    "rxjs",

    "@angular/material/prebuilt-themes/indigo-pink.css",

    "./node_modules/ngx-toastr/toastr.css",
    
    './node_modules/primeng/resources/components/common/common.css',
    './node_modules/primeng/resources/components/dialog/dialog.css',
    './node_modules/primeng/resources/themes/bootstrap/theme.css',
    "./node_modules/@ng-select/ng-select/themes/default.theme.css"


];
const allModules = treeShakableModules.concat(nonTreeShakableModules);

module.exports = (env) => {
    const extractCSS = new ExtractTextPlugin('vendor.css');
    const isDevBuild = !(env && env.prod);
    const clientBundleConfig = {
        stats: { modules: false },
        resolve: { extensions: ['.js'] },
        mode: isDevBuild ? "development" : "production",
        entry: {
            vendor: isDevBuild ? allModules : nonTreeShakableModules
        },
        output: {
            publicPath: 'dist/',
            filename: '[name].' + (isDevBuild ? "dev" : "prod") + '.js',
            library: '[name]',
            path: path.join(__dirname, 'wwwroot', 'dist')
        },
        module: {
            rules: [
                { test: /\.(png|woff|woff2|eot|ttf|svg)(\?|$)/, use: 'url-loader?limit=100000' },
                { test: /\.css(\?|$)/, use: extractCSS.extract({ use: 'css-loader' }) }
            ]
        },
        optimization: {
            minimizer: isDevBuild ? [] : [new UglifyJsPlugin()]
        },
        plugins: [
            extractCSS,
            new webpack.ProvidePlugin({ $: 'jquery', jQuery: 'jquery' }), // Maps these identifiers to the jQuery package (because Bootstrap expects it to be a global variable)
            //new webpack.ContextReplacementPlugin(/\@angular\b.*\b(bundles|linker)/, path.join(__dirname, './ClientApp')), // Workaround for https://github.com/angular/angular/issues/11580
            //new webpack.ContextReplacementPlugin(/angular(\\|\/)core(\\|\/)@angular/, path.join(__dirname, './ClientApp')), // Workaround for https://github.com/angular/angular/issues/14898
            new webpack.IgnorePlugin(/^vertx$/),
            new webpack.DllPlugin({
                path: path.join(__dirname, 'wwwroot', 'dist', '[name].' + (isDevBuild ? "dev" : "prod") + '-manifest.json'),
                name: '[name]'
            })
        ]
    };

    return [clientBundleConfig];
};
