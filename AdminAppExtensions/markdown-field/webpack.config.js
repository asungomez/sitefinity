const path = require('path');

module.exports = {
  entry: './src/index.ts',
  output: {
    filename: 'markdown-field.bundle.js',
    path: path.resolve(__dirname, '../../AdminApp'),
    library: {
      name: 'SitefinityMarkdownField',
      type: 'umd',
      umdNamedDefine: true
    },
    globalObject: 'this'
  },
  resolve: {
    extensions: ['.ts', '.js']
  },
  module: {
    rules: [
      {
        test: /\.ts$/,
        use: 'ts-loader',
        exclude: /node_modules/
      },
      {
        test: /\.css$/,
        use: ['style-loader', 'css-loader']
      }
    ]
  },
  devtool: 'source-map'
};
