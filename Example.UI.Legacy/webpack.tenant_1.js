const path = require('path');
const merge = require('webpack-merge');
const AngularCompilerPlugin = require('@ngtools/webpack').AngularCompilerPlugin;
const WebpackSharedConfig = require("./WebpackSharedConfig");

module.exports = (env) => {
	env.app="tenant_1";
    var sharedConfig = WebpackSharedConfig(env);
    var config = merge(sharedConfig, {
        entry: { "tenant_1": "./tenant_1/boot.ts" },
        plugins: [
            new AngularCompilerPlugin({
                tsConfigPath: './webpack.tenant_1.js.tsc',
                entryModule: path.join(__dirname, "tenant_1/app/tenant_1Module#tenant_1Module")
            })
        ]
    });

    return config;
};