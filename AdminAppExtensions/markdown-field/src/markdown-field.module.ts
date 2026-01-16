import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MarkdownFieldComponent } from './markdown-field.component';

@NgModule({
  declarations: [
    MarkdownFieldComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    MarkdownFieldComponent
  ]
})
export class MarkdownFieldModule { }
