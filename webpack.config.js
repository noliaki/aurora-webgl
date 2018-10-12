const webpack = require('webpack')
const path = require('path')
const UglifyJSPlugin = require('uglifyjs-webpack-plugin')
const plugins = [
  new webpack.DefinePlugin({
    'process.env.NODE_ENV': JSON.stringify(process.env.NODE_ENV)
  })
]

const config = {
  mode: process.env.NODE_ENV,
  context: path.resolve('./src'),
  entry: './entry.ts',
  output: {
    path: path.resolve('./docs/js'),
    filename: 'bundle.js'
  },
  resolve: {
    extensions: ['.ts', 'fs', 'vs']
  },
  module: {
    rules: [
      {
        test: /\.ts$/,
        loader: 'ts-loader',
        exclude: /node_modules/
      },
      {
        test: /\.(vs|fs)$/,
        loader: 'raw-loader',
        exclude: /node_modules/
      }
    ]
  },
  optimization: {
    minimizer: [
      new UglifyJSPlugin({
        uglifyOptions: {
          compress: {
            drop_console: process.env.NODE_ENV === 'production'
          }
        }
      })
    ]
  },
  plugins
}

if (process.env.NODE_ENV === 'development') {
  config.watch = true
  config.cache = true

  config.plugins = config.plugins.concat([
    new webpack.LoaderOptionsPlugin({
      debug: true
    })
  ])
}

module.exports = config
