/**
 * Sitefinity AdminApp Custom Field Extension Entry Point
 * 
 * This module registers a custom Markdown field type that can be selected
 * in Module Builder's "Interface widget for entering data" dropdown.
 */

import { enableProdMode, NgModuleRef } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { MarkdownFieldModule } from './markdown-field.module';
import { MarkdownFieldComponent } from './markdown-field.component';

// Enable production mode
enableProdMode();

// Declare global Sitefinity extensibility namespace
declare global {
  interface Window {
    sfExtensions: {
      registerField?: (fieldType: string, component: any) => void;
      fields?: Map<string, any>;
    };
  }
}

/**
 * Initialize Sitefinity extensions namespace
 */
function initializeExtensionsNamespace() {
  if (!window.sfExtensions) {
    window.sfExtensions = {};
  }
  if (!window.sfExtensions.fields) {
    window.sfExtensions.fields = new Map();
  }
}

/**
 * Register the Markdown field with Sitefinity AdminApp
 */
function registerMarkdownField() {
  initializeExtensionsNamespace();
  
  // Register the field component
  if (window.sfExtensions.registerField) {
    window.sfExtensions.registerField('MarkdownEditor', MarkdownFieldComponent);
  } else {
    // Fallback: store in fields map
    window.sfExtensions.fields!.set('MarkdownEditor', MarkdownFieldComponent);
  }
  
  console.log('[MarkdownField] Custom field registered: MarkdownEditor');
  console.log('[MarkdownField] Available in Module Builder as "Custom: MarkdownEditor"');
}

// Bootstrap the module
let moduleRef: NgModuleRef<MarkdownFieldModule> | null = null;

platformBrowserDynamic()
  .bootstrapModule(MarkdownFieldModule)
  .then((ref) => {
    moduleRef = ref;
    registerMarkdownField();
  })
  .catch(err => {
    console.error('[MarkdownField] Error loading extension:', err);
  });

// Export for external access if needed
export { MarkdownFieldComponent, MarkdownFieldModule };
