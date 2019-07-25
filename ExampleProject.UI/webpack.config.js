const merge = require('webpack-merge');
const WebpackSharedConfig = require("./WebpackSharedConfig");

module.exports = (env) => {

    var sharedConfig = WebpackSharedConfig({ prod: false });

    var config = merge(sharedConfig, {
        entry: {
            "MainApp" : "./MainApp/boot.ts",
        }
    })
    return config;
};
