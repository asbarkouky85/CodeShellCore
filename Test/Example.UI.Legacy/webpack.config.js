const merge = require('webpack-merge');
const WebpackSharedConfig = require("./WebpackSharedConfig");

module.exports = (env) => {

    var sharedConfig = WebpackSharedConfig({ prod: false });

    var config = merge(sharedConfig, {
        entry: {
            "tenant_1" : "./tenant_1/boot.ts",
        }
    })
    return config;
};
