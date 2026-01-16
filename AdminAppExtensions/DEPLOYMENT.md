# Deploying the Markdown Field Extension

## Build Process

The GitHub Actions workflow automatically builds both:
1. The .NET solution (`SitefinityWebApp.dll`)
2. The AdminApp extension (JavaScript bundle)

## What Gets Built

After a successful build, you'll have:

```
bin/
  └── SitefinityWebApp.dll
  └── SitefinityWebApp.pdb

AdminApp/
  └── main.[hash].js
  └── main.[hash].js.map
  └── styles.[hash].css
  └── polyfills.[hash].js
  └── runtime.[hash].js
```

## Deployment Steps

### 1. Deploy to Server

Upload all files to your Sitefinity server:
- `bin/SitefinityWebApp.dll` → `C:\home\site\wwwroot\bin\`
- `AdminApp/*.js` files → `C:\home\site\wwwroot\AdminApp\`

### 2. Register the Extension

Edit `C:\home\site\wwwroot\AdminApp\index.html`:

Find the closing `</body>` tag and add the script reference **before** it:

```html
  <script src="/adminapp/main.[actual-hash].js" defer></script>
</body>
```

**Important**: Replace `[actual-hash]` with the real hash from your build output.

### 3. Restart the Application

Restart your Sitefinity application to load the new DLL:
- In Azure: Restart the App Service
- In IIS: Recycle the application pool

### 4. Verify Extension Loaded

1. Open your browser's Developer Tools (F12)
2. Navigate to your Sitefinity AdminApp: `https://your-site.com/Sitefinity/adminapp`
3. Check the Console tab for:
   ```
   [MarkdownField] Custom field registered: MarkdownEditor
   ```

If you see this message, the extension is loaded successfully.

### 5. Configure in Module Builder

1. Go to **Administration → Module Builder**
2. Select your module → **Edit content type** → **Add a field**
3. Fill in:
   - **Name**: `Markdown` (or any name)
   - **Type**: `Long text`
   - **Interface widget**: `Custom...`
   - **Type or virtual path**: `MarkdownEditor`
4. Click **Continue** → **Save changes**

### 6. Test the Field

1. Navigate to your module's content items
2. Edit or create an item
3. You should see the Toast UI Markdown editor for your field

## Troubleshooting

### Extension not loading

**Check**: Browser console for JavaScript errors
**Fix**: Ensure the script path in `index.html` is correct

### "MarkdownEditor" not available in dropdown

**Check**: Console log for registration message
**Fix**: The extension may not have loaded - verify step 2

### Editor not rendering

**Check**: Network tab for failed CSS/JS requests
**Fix**: Ensure all bundle files are deployed correctly

### Field shows textarea instead of editor

**Possible causes**:
1. Extension didn't load
2. Field type name is misspelled (must be exactly `MarkdownEditor`)
3. Browser cache - try hard refresh (Ctrl+Shift+R)

## Finding the Build Hash

After GitHub Actions completes:

1. Go to Actions tab → Latest workflow run
2. Download the `build-output` artifact
3. Look at the filenames: `main.abc123def.js`
4. The hash is `abc123def`
