(function ($) {
    'use strict';

    // Initialize all markdown field editors on the page
    $(document).ready(function () {
        $('[data-sf-role="markdown-field-container"]').each(function () {
            initializeMarkdownEditor($(this));
        });
    });

    function initializeMarkdownEditor($container) {
        var $textarea = $container.find('[data-sf-role="markdown-field-input"]');
        var $hiddenInput = $container.find('[data-sf-role="markdown-field-value"]');
        var editorId = $textarea.attr('id');
        var config = $container.data('sf-config');

        if (!editorId || !window.toastui || !window.toastui.Editor) {
            console.error('TOAST UI Editor not loaded or textarea ID missing');
            return;
        }

        // Parse configuration
        var initialValue = $hiddenInput.val() || '';
        var initialEditType = config.initialEditType || 'wysiwyg';
        var previewStyle = config.previewStyle || 'vertical';
        var height = config.height || '400px';
        var placeholder = config.placeholder || '';

        // Hide the textarea since we're replacing it with the editor
        $textarea.hide();

        // Create editor options
        var editorOptions = {
            el: document.getElementById(editorId),
            height: height,
            initialEditType: initialEditType,
            previewStyle: previewStyle,
            initialValue: initialValue,
            placeholder: placeholder,
            usageStatistics: false,
            toolbarItems: [
                ['heading', 'bold', 'italic', 'strike'],
                ['hr', 'quote'],
                ['ul', 'ol', 'task', 'indent', 'outdent'],
                ['table', 'link'],
                ['code', 'codeblock']
            ]
        }

        // Initialize the TOAST UI Editor
        var editor = new toastui.Editor(editorOptions);

        // Store editor instance on the container for potential future access
        $container.data('editor-instance', editor);

        // Sync editor content with hidden input on change
        editor.on('change', function () {
            var markdown = editor.getMarkdown();
            $hiddenInput.val(markdown);

            // Trigger validation
            validateField($container, markdown);
        });

        // Initial validation check
        var initialMarkdown = editor.getMarkdown();
        if (initialMarkdown) {
            $hiddenInput.val(initialMarkdown);
        }

        // Validate on load if there are validation rules
        if ($container.data('sf-required')) {
            validateField($container, initialMarkdown);
        }
    }

    function validateField($container, value) {
        var isRequired = $container.data('sf-required');
        var $errorMessage = $container.find('[data-sf-role="error-message"]');
        var $violationMessages = $container.find('[data-sf-role="violation-messages"]');

        if (!$errorMessage.length || !$violationMessages.length) {
            return true;
        }

        var messages = JSON.parse($violationMessages.val() || '{}');
        var isValid = true;
        var errorText = '';

        // Check required validation
        if (isRequired && (!value || value.trim() === '')) {
            isValid = false;
            errorText = messages.required || 'This field is required.';
        }

        // Display or hide error message
        if (!isValid) {
            $errorMessage.text(errorText).show();
            $container.addClass('has-error');
        } else {
            $errorMessage.text('').hide();
            $container.removeClass('has-error');
        }

        return isValid;
    }

    // Form submission validation
    $(document).on('submit', 'form', function (e) {
        var isFormValid = true;

        $('[data-sf-role="markdown-field-container"]').each(function () {
            var $container = $(this);
            var editor = $container.data('editor-instance');

            if (editor) {
                var markdown = editor.getMarkdown();
                var $hiddenInput = $container.find('[data-sf-role="markdown-field-value"]');
                $hiddenInput.val(markdown);

                if (!validateField($container, markdown)) {
                    isFormValid = false;
                }
            }
        });

        if (!isFormValid) {
            e.preventDefault();
            return false;
        }
    });

})(jQuery);
