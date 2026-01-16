# Markdown Field Extension for Sitefinity AdminApp

Custom field type that adds a WYSIWYG Markdown editor to Sitefinity AdminApp using Toast UI Editor.

## Prerequisites

- Node.js 18+
- Sitefinity 13.3+ with AdminApp

## Building

The extension is automatically built by GitHub Actions, but you can also build manually:

```bash
cd AdminAppExtensions/markdown-field
npm install
npm run build
```

This will output the bundle files to the `AdminApp` folder.

## Deployment

After GitHub Actions builds the project, the following files will be in `AdminApp/`:
- `main.*.js` - The extension bundle
- `main.*.js.map` - Source map for debugging

These files need to be deployed to your Sitefinity server's `AdminApp/` folder.

## Loading the Extension in AdminApp

The extension needs to be loaded by AdminApp. There are two approaches:

### Option 1: Load via index.html (Recommended)

Edit `AdminApp/index.html` and add before the closing `</body>` tag:

```html
<script src="/adminapp/main.[hash].js" defer></script>
</body>
```

Replace `[hash]` with the actual hash from your build output.

### Option 2: Load via config.json

Edit `AdminApp/config.json` and add an `extensions` section:

```json
{
  "editorSettings": {
    ...
  },
  "extensions": [
    {
      "name": "MarkdownField",
      "path": "/adminapp/main.[hash].js"
    }
  ]
}
```

## Using in Module Builder

After the extension is loaded:

1. Go to **Administration → Module Builder → [Your Module] → Add a field**
2. Name: `Markdown` (or any name you want)
3. Type: **Long text**
4. Interface widget for entering data: **Custom...**
5. In the "Type or virtual path of the custom widget" field, enter: `MarkdownEditor`
6. Click **Continue** and **Save**

The field will now use the Toast UI Markdown editor when editing content.

## Troubleshooting

1. **Extension not loading**: Check browser console for errors
2. **Field not appearing**: Ensure the extension script is loaded before navigating to Module Builder
3. **Editor not initializing**: Check that Toast UI Editor CSS is loading correctly

Check browser console logs for:
```
[MarkdownField] Custom field registered: MarkdownEditor
```

This confirms the extension loaded successfully.
