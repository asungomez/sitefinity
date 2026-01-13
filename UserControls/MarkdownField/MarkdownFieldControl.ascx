<%@ Control Language="C#" AutoEventWireup="true" Inherits="SitefinityWebApp.UserControls.MarkdownField.MarkdownFieldControl" %>

<link rel="stylesheet" href="~/ResourcePackages/Bootstrap/assets/vendor/toast-ui/toastui-editor.min.css" />
<script src="~/ResourcePackages/Bootstrap/assets/vendor/toast-ui/toastui-editor-all.min.js"></script>

<input type="hidden" id="<%= ClientID %>_hiddenValue" />
<div id="<%= ClientID %>_editor" style="margin: 10px 0;"></div>

<script type="text/javascript">
(function() {
    var editorId = '<%= ClientID %>_editor';
    var editor = null;

    // Initialize when DOM is ready
    function initEditor() {
        if (typeof toastui === 'undefined' || !toastui.Editor) {
            setTimeout(initEditor, 100);
            return;
        }

        editor = new toastui.Editor({
            el: document.getElementById(editorId),
            height: '400px',
            initialEditType: 'wysiwyg',
            previewStyle: 'vertical',
            usageStatistics: false,
            toolbarItems: [
                ['heading', 'bold', 'italic', 'strike'],
                ['hr', 'quote'],
                ['ul', 'ol', 'task', 'indent', 'outdent'],
                ['table', 'link'],
                ['code', 'codeblock']
            ],
            events: {
                change: function() {
                    // Update hidden field on every change
                    var hiddenField = document.getElementById('<%= ClientID %>_hiddenValue');
                    if (hiddenField) {
                        hiddenField.value = editor.getMarkdown();
                    }
                }
            }
        });

        // Set initial value if any
        var initialValue = window['<%= ClientID %>_initialValue'];
        if (initialValue) {
            editor.setMarkdown(initialValue);
        }
    }

    // Expose getValue/setValue for Sitefinity
    window['<%= ClientID %>_getValue'] = function() {
        if (editor) {
            var markdown = editor.getMarkdown();
            var hiddenField = document.getElementById('<%= ClientID %>_hiddenValue');
            if (hiddenField) {
                hiddenField.value = markdown;
            }
            return markdown;
        }
        return '';
    };

    window['<%= ClientID %>_setValue'] = function(value) {
        if (editor) {
            editor.setMarkdown(value || '');
        } else {
            window['<%= ClientID %>_initialValue'] = value;
        }
    };

    // Start initialization
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initEditor);
    } else {
        initEditor();
    }
})();
</script>
