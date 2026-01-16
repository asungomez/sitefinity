/**
 * Markdown Field Enhancer for Sitefinity AdminApp
 *
 * Automatically replaces Long Text fields with "Markdown" in their name
 * with Toast UI Editor (WYSIWYG Markdown editor).
 *
 * Usage:
 * 1. Create a Long text field in Module Builder
 * 2. Name it "Markdown" (or include "markdown" in the name, case-insensitive)
 * 3. The field will automatically use Toast UI Editor
 */

(function() {
    'use strict';

    // Configuration - adjust field name matching as needed
    const FIELD_NAME_PATTERNS = [
        /^markdown$/i,        // Matches field named exactly "markdown" (case-insensitive)
    ];

    const TOAST_UI_CDN = {
        css: 'https://uicdn.toast.com/editor/latest/toastui-editor.min.css',
        js: 'https://uicdn.toast.com/editor/latest/toastui-editor-all.min.js'
    };

    // Track initialized editors
    const initializedTextareas = new WeakSet();
    const editorInstances = new Map();

    // Load Toast UI Editor CSS
    function loadToastUIStyles() {
        if (!document.querySelector(`link[href="${TOAST_UI_CDN.css}"]`)) {
            const link = document.createElement('link');
            link.rel = 'stylesheet';
            link.href = TOAST_UI_CDN.css;
            document.head.appendChild(link);
        }
    }

    // Load Toast UI Editor JS
    function loadToastUIScript() {
        return new Promise((resolve, reject) => {
            if (window.toastui && window.toastui.Editor) {
                resolve();
                return;
            }

            if (document.querySelector(`script[src="${TOAST_UI_CDN.js}"]`)) {
                // Script is loading, wait for it
                const checkInterval = setInterval(() => {
                    if (window.toastui && window.toastui.Editor) {
                        clearInterval(checkInterval);
                        resolve();
                    }
                }, 100);
                return;
            }

            const script = document.createElement('script');
            script.src = TOAST_UI_CDN.js;
            script.onload = resolve;
            script.onerror = reject;
            document.head.appendChild(script);
        });
    }

    // Check if a textarea should be enhanced
    function shouldEnhanceTextarea(textarea) {
        // Check name attribute
        if (textarea.name) {
            for (const pattern of FIELD_NAME_PATTERNS) {
                if (pattern.test(textarea.name)) {
                    return true;
                }
            }
        }

        // Check id attribute
        if (textarea.id) {
            for (const pattern of FIELD_NAME_PATTERNS) {
                if (pattern.test(textarea.id)) {
                    return true;
                }
            }
        }

        // Check for label text
        const formGroup = textarea.closest('.form-group, [class*="field"], .sf-field-wrp');
        if (formGroup) {
            const label = formGroup.querySelector('label');
            if (label) {
                const labelText = label.textContent.trim();
                for (const pattern of FIELD_NAME_PATTERNS) {
                    if (pattern.test(labelText)) {
                        return true;
                    }
                }
            }
        }

        // Check ng-reflect-name attribute (Angular)
        const ngReflectName = textarea.getAttribute('ng-reflect-name');
        if (ngReflectName) {
            for (const pattern of FIELD_NAME_PATTERNS) {
                if (pattern.test(ngReflectName)) {
                    return true;
                }
            }
        }

        return false;
    }

    // Initialize Toast UI Editor on a textarea
    function initializeEditor(textarea) {
        // Create wrapper
        const wrapper = document.createElement('div');
        wrapper.className = 'markdown-editor-wrapper';
        wrapper.style.cssText = 'margin: 8px 0; border: 1px solid #d0d0d0; border-radius: 4px; overflow: hidden;';

        // Create editor container
        const editorContainer = document.createElement('div');
        editorContainer.className = 'markdown-editor-container';
        wrapper.appendChild(editorContainer);

        // Hide textarea but keep it for form submission
        textarea.style.display = 'none';
        textarea.parentNode.insertBefore(wrapper, textarea);

        // Create editor
        const editor = new window.toastui.Editor({
            el: editorContainer,
            height: '400px',
            initialEditType: 'wysiwyg',
            previewStyle: 'vertical',
            initialValue: textarea.value || '',
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
                    // Sync value back to textarea
                    const markdown = editor.getMarkdown();
                    textarea.value = markdown;

                    // Trigger change events for Angular to detect
                    textarea.dispatchEvent(new Event('input', { bubbles: true }));
                    textarea.dispatchEvent(new Event('change', { bubbles: true }));
                }
            }
        });

        // Store editor instance
        editorInstances.set(textarea, editor);

        // Handle readonly state
        if (textarea.readOnly || textarea.disabled) {
            editorContainer.style.pointerEvents = 'none';
            editorContainer.style.opacity = '0.7';
            const toolbar = editorContainer.querySelector('.toastui-editor-toolbar');
            if (toolbar) {
                toolbar.style.display = 'none';
            }
        }

        console.log('[MarkdownFieldEnhancer] Editor initialized for field:', textarea.name || textarea.id);
    }

    // Scan for textareas and enhance matching ones
    function scanAndEnhance() {
        const textareas = document.querySelectorAll('textarea');

        textareas.forEach(textarea => {
            // Skip if already initialized
            if (initializedTextareas.has(textarea)) {
                return;
            }

            // Check if this textarea should be enhanced
            if (shouldEnhanceTextarea(textarea)) {
                initializedTextareas.add(textarea);

                // Ensure Toast UI Editor is loaded before initializing
                loadToastUIScript().then(() => {
                    initializeEditor(textarea);
                }).catch(error => {
                    console.error('[MarkdownFieldEnhancer] Failed to load Toast UI Editor:', error);
                });
            }
        });
    }

    // Set up MutationObserver to watch for dynamically added textareas
    function setupObserver() {
        const observer = new MutationObserver((mutations) => {
            let shouldScan = false;

            for (const mutation of mutations) {
                if (mutation.type === 'childList' && mutation.addedNodes.length > 0) {
                    for (const node of Array.from(mutation.addedNodes)) {
                        if (node instanceof Element) {
                            if (node.tagName === 'TEXTAREA' || node.querySelector('textarea')) {
                                shouldScan = true;
                                break;
                            }
                        }
                    }
                }
                if (shouldScan) break;
            }

            if (shouldScan) {
                // Debounce scanning
                setTimeout(scanAndEnhance, 200);
            }
        });

        observer.observe(document.body, {
            childList: true,
            subtree: true
        });
    }

    // Initialize
    function initialize() {
        console.log('[MarkdownFieldEnhancer] Initializing...');

        // Load Toast UI styles
        loadToastUIStyles();

        // Initial scan
        setTimeout(scanAndEnhance, 500);

        // Set up observer for dynamic content
        setupObserver();

        // Re-scan on Angular route changes (AdminApp uses client-side routing)
        window.addEventListener('popstate', () => {
            setTimeout(scanAndEnhance, 500);
        });

        // Also scan on hash changes
        window.addEventListener('hashchange', () => {
            setTimeout(scanAndEnhance, 500);
        });

        console.log('[MarkdownFieldEnhancer] Ready. Fields matching patterns will be enhanced:', FIELD_NAME_PATTERNS);
    }

    // Run when DOM is ready
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initialize);
    } else {
        initialize();
    }
})();
