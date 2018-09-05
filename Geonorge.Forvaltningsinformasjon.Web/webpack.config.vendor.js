const path = require('path');
const webpack = require('webpack');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const OptimizeCSSPlugin = require('optimize-css-assets-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const CleanWebpackPlugin = require('clean-webpack-plugin');

module.exports = (env) => {
    const extractCSS = new ExtractTextPlugin('vendor.css');
    const isDevBuild = !(env && env.prod);
    return [{
        stats: { modules: false },
        resolve: {
            extensions: ['.js']
        },
        module: {
            rules: [
                { test: /\.(png|woff|woff2|eot|ttf|svg)(\?|$)/, use: 'url-loader?limit=100000' },
                { test: /\.css(\?|$)/, use: extractCSS.extract(['css-loader']) },
                { test: /\.scss$/, use: ExtractTextPlugin.extract({ use: 'css-loader?minimize!sass-loader' }) },
            ]
        },
        entry: {
            vendor: [
                'bootstrap', 
                'bootstrap/dist/css/bootstrap.css', 
                'event-source-polyfill', 
                'jquery',
                'geonorge-base/src/sass/base.scss',
                'geonorge-shared-partials/src/css/pageTop.css'                
            ],
        },
        output: {
            path: path.join(__dirname, 'wwwroot', 'dist'),
            publicPath: '/dist/',
            filename: '[name].js',
            library: '[name]_[hash]',
        },
        plugins: [
          //  new CleanWebpackPlugin('wwwroot/dist'),
            extractCSS,
            new OptimizeCSSPlugin({
                cssProcessorOptions: {
                    safe: true
                }
            }),
            new CopyWebpackPlugin([
                { from: 'node_modules/geonorge-shared-partials/src/images', to: 'images/'},
                { from: 'node_modules/geonorge-shared-partials/src/partials', to: 'partials/'},
                { from: 'node_modules/geonorge-shared-partials/dist', to: 'js/geonorge-shared'},
                { from: 'node_modules/geonorge-base/src/js', to: 'js/geonorge-base'},
                { from: 'node_modules/geonorge-base/src/images', to: 'images/'},
                { from: 'node_modules/geonorge-base/src/fonts', to: 'fonts/'},
            ]),
            new webpack.ProvidePlugin({ $: 'jquery', jQuery: 'jquery' }), // Maps these identifiers to the jQuery package (because Bootstrap expects it to be a global variable)
            new webpack.DllPlugin({
                path: path.join(__dirname, 'wwwroot', 'dist', '[name]-manifest.json'),
                name: '[name]_[hash]'
            }),
            new webpack.DefinePlugin({
                'process.env.NODE_ENV': isDevBuild ? '"development"' : '"production"'
            })
        ].concat(isDevBuild ? [] : [
            new webpack.optimize.UglifyJsPlugin()
        ])
    }];
};
