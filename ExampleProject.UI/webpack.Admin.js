const path = require('path');
const merge = require('webpack-merge');
const AngularCompilerPlugin = require('@ngtools/webpack').AngularCompilerPlugin;
const WebpackSharedConfig = require("./WebpackSharedConfig");

module.exports = (env) => {

    var sharedConfig = WebpackSharedConfig({ prod: true, app: "Admin" });
    var config = merge(sharedConfig, {
        entry: { "Admin": "./Admin/boot.ts" },
        plugins: [
            new AngularCompilerPlugin({
                tsConfigPath: './webpack.Admin.js.tsc',
                entryModule: path.join(__dirname, "Admin/app/AdminModule#AdminModule")
            })
        ]
    });

    return config;
};