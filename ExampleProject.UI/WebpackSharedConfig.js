const path = require('path');
const webpack = require('webpack');
const CheckerPlugin = require('awesome-typescript-loader').CheckerPlugin;

module.exports = function WebpackSharedConfig(env) {
    const isDevBuild = !(env && env.prod);
    const app = env.app ? env.app + "-" : "";
    const version = isDevBuild ? "dev" : "v" + (env.version ? env.version : "1.0.0.0")
    const clientBundleOutputDir = './wwwroot/dist';
    return {
        mode: isDevBuild ? "development" : "production",
        stats: { modules: false },
        context: __dirname,
        output: {
            filename: '[name]-' + (isDevBuild ? "dev" : version) + '.js',
            chunkFilename: version + '/' + app + version + '__[name].js',
            publicPath: '/dist/',
            path: path.join(__dirname, clientBundleOutputDir)
        },
        resolve: {
            extensions: ['.js', '.ts'],
            modules: [
                "node_modules",
                path.resolve(__dirname, "Core")
            ]
        },
        module: {
            rules: [
                { test: /\.ts$/, use: isDevBuild ? ['awesome-typescript-loader?silent=true', 'angular2-template-loader', 'angular2-router-loader'] : '@ngtools/webpack' },
                { test: /\.html$/, use: 'html-loader?minimize=false' },
                { test: /\.css$/, use: ['to-string-loader', isDevBuild ? 'css-loader' : 'css-loader?minimize'] },
                { test: /\.(png|jpg|jpeg|gif|svg)$/, use: 'url-loader?limit=25000' }
            ]
        },
        optimization: {
            splitChunks: isDevBuild ? {}:{
                chunks: 'all',
                name: false,
                maxSize: isDevBuild ? 0 : 999424,
                hidePathInfo :true
            }
        },
        plugins: [
            new CheckerPlugin(),
            new webpack.DllReferencePlugin({
                context: __dirname,
                manifest: require('./wwwroot/dist/vendor.' + (isDevBuild ? "dev" : "prod") + '-manifest.json')
            })
        ].concat(isDevBuild ?
            [
                new webpack.SourceMapDevToolPlugin({
                    filename: '[file].map',
                    moduleFilenameTemplate: path.relative(clientBundleOutputDir, '[resourcePath]')
                })
            ]
            : [])
    };
}