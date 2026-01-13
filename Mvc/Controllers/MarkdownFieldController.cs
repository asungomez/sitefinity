using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Pages.Web.Services;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using SitefinityWebApp.Mvc.Models;

namespace SitefinityWebApp.Mvc.Controllers
{
    /// <summary>
    /// Controller for the Markdown field control using TOAST UI Editor
    /// </summary>
    [DatabaseMapping(UserFriendlyDataType.LongText)]
    [Localization(typeof(FieldResources))]
    [IndexRenderMode(IndexRenderModes.NoOutput)]
    public class MarkdownFieldController : FormFieldControllerBase<MarkdownFieldViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownFieldController"/> class.
        /// </summary>
        public MarkdownFieldController()
        {
            this.TemplateName = "Write";
        }

        /// <summary>
        /// Gets or sets the placeholder text.
        /// </summary>
        [Category("Labels and Messages")]
        [DisplayName("Placeholder")]
        [Description("Text that will be displayed when the field is empty.")]
        public string PlaceholderText { get; set; }

        /// <summary>
        /// Gets or sets the initial edit type (markdown or wysiwyg).
        /// </summary>
        [Category("Settings")]
        [DisplayName("Initial Edit Type")]
        [Description("The initial editing mode: markdown or wysiwyg")]
        [DefaultValue("wysiwyg")]
        public string InitialEditType { get; set; } = "wysiwyg";

        /// <summary>
        /// Gets or sets the preview style (tab or vertical).
        /// </summary>
        [Category("Settings")]
        [DisplayName("Preview Style")]
        [Description("Preview style in markdown mode: tab or vertical")]
        [DefaultValue("vertical")]
        public string PreviewStyle { get; set; } = "vertical";

        /// <summary>
        /// Gets or sets the editor height.
        /// </summary>
        [Category("Settings")]
        [DisplayName("Editor Height")]
        [Description("Height of the editor (e.g., 400px)")]
        [DefaultValue("400px")]
        public string EditorHeight { get; set; } = "400px";

        /// <summary>
        /// Gets the form field for the Markdown control.
        /// </summary>
        protected override IFormFieldControl FormFieldControl
        {
            get
            {
                if (this.formFieldControl == null)
                {
                    var metaField = this.GetMetaField();
                    this.formFieldControl = new MarkdownFormFieldControl(metaField);
                }

                return this.formFieldControl;
            }
        }

        /// <summary>
        /// Builds the view model for rendering.
        /// </summary>
        /// <param name="value">The current value of the field.</param>
        /// <returns>The view model.</returns>
        protected override MarkdownFieldViewModel BuildViewModel(object value)
        {
            var viewModel = new MarkdownFieldViewModel
            {
                Value = value as string ?? string.Empty,
                PlaceholderText = this.PlaceholderText,
                InitialEditType = this.InitialEditType,
                PreviewStyle = this.PreviewStyle,
                EditorHeight = this.EditorHeight,
                CssClass = this.CssClass,
                MetaField = this.MetaField,
                ValidatorDefinition = new FieldValidationDefinition
                {
                    Required = this.ValidatorDefinition.Required,
                    RequiredViolationMessage = this.ValidatorDefinition.RequiredViolationMessage
                }
            };

            return viewModel;
        }

        /// <summary>
        /// Gets the meta field for this control.
        /// </summary>
        /// <returns>The meta field.</returns>
        private IMetaField GetMetaField()
        {
            var manager = FormsManager.GetManager();
            var metaFieldName = this.MetaField.FieldName;

            if (this.MetaField is IMetaField)
            {
                return this.MetaField;
            }

            return null;
        }

        private IFormFieldControl formFieldControl;
    }

    /// <summary>
    /// Form field control implementation for the Markdown field
    /// </summary>
    internal class MarkdownFormFieldControl : IFormFieldControl
    {
        private IMetaField metaField;
        private string value;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownFormFieldControl"/> class.
        /// </summary>
        /// <param name="metaField">The meta field.</param>
        public MarkdownFormFieldControl(IMetaField metaField)
        {
            this.metaField = metaField;
        }

        /// <summary>
        /// Gets or sets the value of the field.
        /// </summary>
        public object Value
        {
            get { return this.value; }
            set { this.value = value as string; }
        }

        /// <summary>
        /// Gets the meta field.
        /// </summary>
        public IMetaField MetaField
        {
            get { return this.metaField; }
        }
    }
}
