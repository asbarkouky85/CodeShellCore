const path = require('path');
const merge = require('webpack-merge');
const AngularCompilerPlugin = require('@ngtools/webpack').AngularCompilerPlugin;
const WebpackSharedConfig = require("./WebpackSharedConfig");

module.exports = (env) => {
	env.app="ClientApp";
    var sharedConfig = WebpackSharedConfig(env);
    var config = merge(sharedConfig, {
        entry: { "ClientApp": "./ClientApp/boot.ts" },
        plugins: [
            new AngularCompilerPlugin({
                tsConfigPath: './webpack.ClientApp.js.tsc',
                entryModule: path.join(__dirname, "ClientApp/app/ClientAppModule#ClientAppModule")
            })
        ]
    });

    return config;
};