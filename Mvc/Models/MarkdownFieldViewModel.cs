using System;
using System.ComponentModel;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;

namespace SitefinityWebApp.Mvc.Models
{
    /// <summary>
    /// View model for the Markdown field control using TOAST UI Editor
    /// </summary>
    public class MarkdownFieldViewModel : IFormFieldViewModel<string>
    {
        /// <summary>
        /// Gets or sets the meta field.
        /// </summary>
        public IMetaField MetaField { get; set; }

        /// <summary>
        /// Gets or sets the value of the field.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this field has a value.
        /// </summary>
        public bool HasValue
        {
            get
            {
                return !string.IsNullOrEmpty(this.Value);
            }
        }

        /// <summary>
        /// Gets or sets the CSS class for styling.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the placeholder text.
        /// </summary>
        public string PlaceholderText { get; set; }

        /// <summary>
        /// Gets or sets the initial edit type (markdown or wysiwyg).
        /// </summary>
        [DefaultValue("markdown")]
        public string InitialEditType { get; set; } = "markdown";

        /// <summary>
        /// Gets or sets the preview style (tab or vertical).
        /// </summary>
        [DefaultValue("vertical")]
        public string PreviewStyle { get; set; } = "vertical";

        /// <summary>
        /// Gets or sets the editor height.
        /// </summary>
        [DefaultValue("400px")]
        public string EditorHeight { get; set; } = "400px";

        /// <summary>
        /// Gets or sets the validation definition.
        /// </summary>
        public FieldValidationDefinition ValidatorDefinition { get; set; }

        /// <summary>
        /// Gets the validation attributes as a string.
        /// </summary>
        public string ValidationAttributes
        {
            get
            {
                if (this.ValidatorDefinition != null && this.ValidatorDefinition.Required.HasValue && this.ValidatorDefinition.Required.Value)
                {
                    return "required='required'";
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the violation message for required field.
        /// </summary>
        public string RequiredViolationMessage
        {
            get
            {
                if (this.ValidatorDefinition != null && !string.IsNullOrEmpty(this.ValidatorDefinition.RequiredViolationMessage))
                {
                    return this.ValidatorDefinition.RequiredViolationMessage;
                }
                return "This field is required.";
            }
        }
    }

    /// <summary>
    /// Field validation definition for the Markdown field
    /// </summary>
    public class FieldValidationDefinition
    {
        /// <summary>
        /// Gets or sets a value indicating whether the field is required.
        /// </summary>
        public bool? Required { get; set; }

        /// <summary>
        /// Gets or sets the required violation message.
        /// </summary>
        public string RequiredViolationMessage { get; set; }
    }
}
