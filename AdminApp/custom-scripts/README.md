# Markdown Field Enhancer

Automatically converts Long Text fields named exactly "Markdown" to use Toast UI Editor (WYSIWYG Markdown editor).

## How It Works

The script automatically detects textareas and replaces them with Toast UI Editor if:
- Field name is exactly "Markdown" (case-insensitive: "markdown", "MARKDOWN", "Markdown" all work)

## Usage

### 1. Create a Field in Module Builder

1. Go to **Administration → Module Builder → [Your Module]**
2. Click **Add a field**
3. Configure the field:
   - **Name**: `Markdown` (must be exactly "Markdown", case doesn't matter)
   - **Type**: `Long text`
   - **Interface widget**: Leave as default (Rich text editor or Text area)
4. Save the field

### 2. Edit Content

When you edit content with this field, it will automatically appear as a Toast UI Markdown editor instead of a plain textarea.

## Customization

To customize which fields are enhanced, edit the `FIELD_NAME_PATTERNS` array in `markdown-field-enhancer.js`:

```javascript
const FIELD_NAME_PATTERNS = [
    /^markdown$/i,    // Matches field named exactly "markdown" (case-insensitive)
    /^content$/i,     // Example: also match fields named "content"
    /^description$/i, // Example: also match fields named "description"
];
```

## Deployment

1. **Upload the script**: Copy `markdown-field-enhancer.js` to your server:
   ```
   AdminApp/custom-scripts/markdown-field-enhancer.js
   ```

2. **Update index.html**: Ensure this line is present before the closing `</body>` tag:
   ```html
   <script src="/adminapp/custom-scripts/markdown-field-enhancer.js" defer></script>
   ```

3. **Clear browser cache** and reload AdminApp

## Verification

Open your browser's developer console when editing content. You should see:
```
[MarkdownFieldEnhancer] Initializing...
[MarkdownFieldEnhancer] Ready. Fields matching patterns will be enhanced: ...
[MarkdownFieldEnhancer] Editor initialized for field: Markdown
```

## Toast UI Editor

This script loads Toast UI Editor from the official CDN:
- CSS: https://uicdn.toast.com/editor/latest/toastui-editor.min.css
- JS: https://uicdn.toast.com/editor/latest/toastui-editor-all.min.js

No additional dependencies needed!
