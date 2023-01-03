const path = require("path");
const webpack = require("webpack");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const OptimizeCSSPlugin = require("optimize-css-assets-webpack-plugin");
const CopyWebpackPlugin = require("copy-webpack-plugin");
const CleanWebpackPlugin = require("clean-webpack-plugin");

module.exports = (env) => {
    const isDevBuild = !(env && env.prod);
    return [
        {
            mode: isDevBuild ? 'development' : 'production',
            stats: { modules: false },
            resolve: {
                extensions: [".js"]
            },
            module: {
                rules: [
                    { test: /\.(png|woff|woff2|eot|ttf|svg)(\?|$)/, use: "url-loader?limit=100000" },
                    { test: /\.css(\?|$)/, use: [MiniCssExtractPlugin.loader, "css-loader"] },
                    { test: /\.scss$/, use: [MiniCssExtractPlugin.loader, "css-loader?minimize!sass-loader"] }
                ]
            },
            entry: {
                vendor: [
                    "event-source-polyfill",
                    "ol/ol.css"
                ]
            },
            output: {
                path: path.join(__dirname, "wwwroot", "dist"),
                publicPath: "/dist/",
                filename: "[name].js",
                library: "[name]_[hash]"
            },
            plugins: [
                //  new CleanWebpackPlugin('wwwroot/dist'),
                new MiniCssExtractPlugin({
                    filename: "vendor.css"
                }),
                new OptimizeCSSPlugin({
                    cssProcessorOptions: {
                        safe: true
                    }
                }),
                new CopyWebpackPlugin({
                    patterns: [
                        { from: 'node_modules/jquery/dist', to: 'js/jquery' },
                        { from: 'node_modules/map/dist', to: 'js/map' },
                        { from: "node_modules/chart.js/dist", to: "js/chart" },
                        { from: "node_modules/@kartverket/geonorge-web-components/", to: "js/geonorge-web-components" }
                    ]
                }),

                new webpack.DllPlugin({
                    path: path.join(__dirname, "wwwroot", "dist", "[name]-manifest.json"),
                    name: "[name]_[hash]"
                }),
                new webpack.DefinePlugin({
                    "process.env.NODE_ENV": isDevBuild ? '"development"' : '"production"'
                })
            ].concat(isDevBuild ? [] : [new webpack.optimize.UglifyJsPlugin()])
        }
    ];
};
