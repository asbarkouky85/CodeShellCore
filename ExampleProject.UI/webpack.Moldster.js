const path = require('path');
const merge = require('webpack-merge');
const AngularCompilerPlugin = require('@ngtools/webpack').AngularCompilerPlugin;
const WebpackSharedConfig = require("./WebpackSharedConfig");

module.exports = (env) => {

    var sharedConfig = WebpackSharedConfig({ prod: true, app: "Moldster" });
    var config = merge(sharedConfig, {
        entry: { "Moldster": "./Moldster/boot.ts" },
        plugins: [
            new AngularCompilerPlugin({
                tsConfigPath: './webpack.Moldster.js.tsc',
                entryModule: path.join(__dirname, "Moldster/app/MoldsterModule#MoldsterModule")
            })
        ]
    });

    return config;
};