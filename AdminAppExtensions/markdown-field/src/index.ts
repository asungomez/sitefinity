/**
 * Sitefinity AdminApp Custom Markdown Field
 *
 * This extension provides a custom Markdown editor field using Toast UI Editor.
 * It registers with Sitefinity's custom field system to be available in Module Builder.
 */

import Editor from '@toast-ui/editor';
import '@toast-ui/editor/dist/toastui-editor.css';

// Sitefinity custom field interface
interface SitefinityFieldContext {
  value: any;
  onChange: (value: any) => void;
  readonly?: boolean;
  fieldDefinition?: any;
}

interface SitefinityFieldInstance {
  getValue: () => any;
  setValue: (value: any) => void;
  destroy?: () => void;
}

// Global Sitefinity namespace
declare global {
  interface Window {
    $$?: any;
    Telerik?: any;
    Sys?: any;
    sfCustomFields?: {
      [key: string]: (context: SitefinityFieldContext) => SitefinityFieldInstance;
    };
  }
}

/**
 * Creates a Markdown editor instance
 */
function createMarkdownEditor(container: HTMLElement, context: SitefinityFieldContext): SitefinityFieldInstance {
  // Create wrapper
  const wrapper = document.createElement('div');
  wrapper.className = 'sf-markdown-field-wrapper';
  wrapper.style.cssText = 'margin: 8px 0; border: 1px solid #d0d0d0; border-radius: 4px; overflow: hidden;';

  // Create editor container
  const editorContainer = document.createElement('div');
  editorContainer.className = 'sf-markdown-editor';
  wrapper.appendChild(editorContainer);

  container.appendChild(wrapper);

  // Initialize Toast UI Editor
  const editor = new Editor({
    el: editorContainer,
    height: '400px',
    initialEditType: 'wysiwyg',
    previewStyle: 'vertical',
    initialValue: context.value || '',
    usageStatistics: false,
    toolbarItems: [
      ['heading', 'bold', 'italic', 'strike'],
      ['hr', 'quote'],
      ['ul', 'ol', 'task', 'indent', 'outdent'],
      ['table', 'link'],
      ['code', 'codeblock']
    ],
    events: {
      change: () => {
        const markdown = editor.getMarkdown();
        context.onChange(markdown);
      }
    }
  });

  // Handle readonly - disable editing by making the editor non-editable
  if (context.readonly) {
    // Toast UI Editor doesn't have a built-in readonly mode, so we disable it via CSS
    editorContainer.style.pointerEvents = 'none';
    editorContainer.style.opacity = '0.7';
    const toolbar = editorContainer.querySelector('.toastui-editor-toolbar') as HTMLElement;
    if (toolbar) {
      toolbar.style.display = 'none';
    }
  }

  console.log('[MarkdownField] Editor initialized');

  return {
    getValue: () => editor.getMarkdown(),
    setValue: (value: string) => {
      if (editor && value !== editor.getMarkdown()) {
        editor.setMarkdown(value || '');
      }
    },
    destroy: () => {
      editor.destroy();
      wrapper.remove();
    }
  };
}

/**
 * Register the Markdown field with Sitefinity
 */
function registerMarkdownField() {
  // Initialize custom fields namespace
  if (!window.sfCustomFields) {
    window.sfCustomFields = {};
  }

  // Register the field
  window.sfCustomFields['MarkdownEditor'] = (context: SitefinityFieldContext) => {
    const container = document.createElement('div');
    container.className = 'sf-markdown-field-container';
    return createMarkdownEditor(container, context);
  };

  console.log('[MarkdownField] MarkdownEditor field type registered');
  console.log('[MarkdownField] Use "MarkdownEditor" as the custom widget type in Module Builder');
}

// Auto-register when script loads
if (document.readyState === 'loading') {
  document.addEventListener('DOMContentLoaded', registerMarkdownField);
} else {
  registerMarkdownField();
}

// Also register on window load as fallback
window.addEventListener('load', () => {
  if (!window.sfCustomFields || !window.sfCustomFields['MarkdownEditor']) {
    registerMarkdownField();
  }
});

export { registerMarkdownField, createMarkdownEditor };
