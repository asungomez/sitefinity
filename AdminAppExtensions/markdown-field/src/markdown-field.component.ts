import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FieldBase } from '@progress/sitefinity-adminapp-sdk/app/api/v1';
import Editor from '@toast-ui/editor';

/**
 * Custom Markdown Field Component for Sitefinity AdminApp
 * Uses Toast UI Editor for WYSIWYG markdown editing
 */
@Component({
  selector: 'sf-markdown-field',
  template: `
    <div class="sf-markdown-field-wrapper">
      <div #editorContainer class="sf-markdown-editor"></div>
    </div>
  `,
  styles: [`
    .sf-markdown-field-wrapper {
      margin: 8px 0;
      border: 1px solid #ccc;
      border-radius: 4px;
      overflow: hidden;
    }
    
    .sf-markdown-editor {
      min-height: 400px;
    }
    
    :host ::ng-deep .toastui-editor-defaultUI {
      border: none;
    }
  `]
})
export class MarkdownFieldComponent extends FieldBase<string> implements OnInit, OnDestroy {
  @ViewChild('editorContainer', { static: true })
  editorContainer!: ElementRef<HTMLDivElement>;

  private editor?: Editor;

  ngOnInit(): void {
    // Initialize Toast UI Editor
    this.editor = new Editor({
      el: this.editorContainer.nativeElement,
      height: '400px',
      initialEditType: 'wysiwyg',
      previewStyle: 'vertical',
      initialValue: this.value || '',
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
          // Update the field value when editor content changes
          if (this.editor) {
            const markdown = this.editor.getMarkdown();
            this.valueChange(markdown);
          }
        }
      }
    });

    // Handle readonly state
    if (this.readonly) {
      this.editor.changeMode('viewer');
    }
  }

  ngOnDestroy(): void {
    // Clean up the editor instance
    if (this.editor) {
      this.editor.destroy();
    }
  }

  /**
   * Called when the field value is updated externally
   */
  protected override onValueChange(value: string): void {
    if (this.editor && value !== this.editor.getMarkdown()) {
      this.editor.setMarkdown(value || '');
    }
  }

  /**
   * Called to validate the field value
   */
  protected override validateValue(): boolean {
    // Markdown fields are always valid (can be empty or contain any markdown)
    return true;
  }
}
