const path = require('path');
const merge = require('webpack-merge');
const AngularCompilerPlugin = require('@ngtools/webpack').AngularCompilerPlugin;
const WebpackSharedConfig = require("./WebpackSharedConfig");

module.exports = (env) => {
	env.app="tenant_2";
    var sharedConfig = WebpackSharedConfig(env);
    var config = merge(sharedConfig, {
        entry: { "tenant_2": "./tenant_2/boot.ts" },
        plugins: [
            new AngularCompilerPlugin({
                tsConfigPath: './webpack.tenant_2.js.tsc',
                entryModule: path.join(__dirname, "tenant_2/app/tenant_2Module#tenant_2Module")
            })
        ]
    });

    return config;
};