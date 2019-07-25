const path = require('path');
const merge = require('webpack-merge');
const AngularCompilerPlugin = require('@ngtools/webpack').AngularCompilerPlugin;
const WebpackSharedConfig = require("./WebpackSharedConfig");

module.exports = (env) => {

    var sharedConfig = WebpackSharedConfig({ prod: true, app: "MainApp" });
    var config = merge(sharedConfig, {
        entry: { "MainApp": "./MainApp/boot.ts" },
        plugins: [
            new AngularCompilerPlugin({
                tsConfigPath: './webpack.MainApp.js.tsc',
                entryModule: path.join(__dirname, "MainApp/app/MainAppModule#MainAppModule")
            })
        ]
    });

    return config;
};