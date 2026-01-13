<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MarkdownFieldControl.ascx.cs" Inherits="SitefinityWebApp.UserControls.MarkdownField.MarkdownFieldControl" %>

<div class="sfFieldWrp">
    <label class="sfTxtLbl"><asp:Literal ID="TitleLabel" runat="server" /></label>
    <asp:TextBox ID="MarkdownTextBox" runat="server" TextMode="MultiLine" CssClass="sfTxt" style="display:none;" />
    <div id="<%= EditorContainerId %>" class="markdown-editor-container"></div>
    <asp:Literal ID="DescriptionLabel" runat="server" />
</div>

<script type="text/javascript">
(function() {
    var containerId = '<%= EditorContainerId %>';
    var textboxId = '<%= MarkdownTextBox.ClientID %>';
    var initialValue = document.getElementById(textboxId).value;

    // Wait for TOAST UI Editor to load
    function initEditor() {
        if (typeof toastui === 'undefined' || !toastui.Editor) {
            setTimeout(initEditor, 100);
            return;
        }

        var editor = new toastui.Editor({
            el: document.getElementById(containerId),
            height: '400px',
            initialEditType: 'wysiwyg',
            previewStyle: 'vertical',
            initialValue: initialValue,
            usageStatistics: false,
            toolbarItems: [
                ['heading', 'bold', 'italic', 'strike'],
                ['hr', 'quote'],
                ['ul', 'ol', 'task', 'indent', 'outdent'],
                ['table', 'link'],
                ['code', 'codeblock']
            ]
        });

        // Sync editor content back to hidden textbox
        editor.on('change', function() {
            var markdown = editor.getMarkdown();
            document.getElementById(textboxId).value = markdown;
        });

        // Save on form submit
        var form = document.getElementById(textboxId).form;
        if (form) {
            form.addEventListener('submit', function() {
                document.getElementById(textboxId).value = editor.getMarkdown();
            });
        }
    }

    // Start initialization
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initEditor);
    } else {
        initEditor();
    }
})();
</script>
