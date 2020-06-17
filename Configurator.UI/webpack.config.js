const merge = require('webpack-merge');
const WebpackSharedConfig = require("./WebpackSharedConfig");

module.exports = (env) => {

    var sharedConfig = WebpackSharedConfig({ prod: false });

    var config = merge(sharedConfig, {
        entry: {
            "ClientApp" : "./ClientApp/boot.ts",
        }
    })
    return config;
};
