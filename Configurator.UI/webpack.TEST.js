const path = require('path');
const merge = require('webpack-merge');
const AngularCompilerPlugin = require('@ngtools/webpack').AngularCompilerPlugin;
const WebpackSharedConfig = require("./WebpackSharedConfig");

module.exports = (env) => {
	env.app="TEST";
    var sharedConfig = WebpackSharedConfig(env);
    var config = merge(sharedConfig, {
        entry: { "TEST": "./TEST/boot.ts" },
        plugins: [
            new AngularCompilerPlugin({
                tsConfigPath: './webpack.TEST.js.tsc',
                entryModule: path.join(__dirname, "TEST/app/TESTModule#TESTModule")
            })
        ]
    });

    return config;
};