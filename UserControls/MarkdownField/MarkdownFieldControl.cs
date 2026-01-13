using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace SitefinityWebApp.UserControls.MarkdownField
{
    /// <summary>
    /// Markdown field control using TOAST UI Editor for Dynamic Modules
    /// </summary>
    public class MarkdownFieldControl : FieldControl
    {
        /// <summary>
        /// Gets the layout template path
        /// </summary>
        public override string LayoutTemplatePath
        {
            get { return "~/UserControls/MarkdownField/MarkdownFieldControl.ascx"; }
        }

        private string _value = string.Empty;
        private HiddenField _hiddenField;

        /// <summary>
        /// Gets or sets the value of the field control.
        /// </summary>
        public override object Value
        {
            get
            {
                // Try to get value from hidden field first
                if (_hiddenField != null && !string.IsNullOrEmpty(_hiddenField.Value))
                {
                    _value = _hiddenField.Value;
                }
                return _value;
            }
            set
            {
                _value = value != null ? value.ToString() : string.Empty;

                // Set hidden field value if available
                if (_hiddenField != null)
                {
                    _hiddenField.Value = _value;
                }

                // Set editor value via JavaScript
                if (this.Page != null)
                {
                    string safeValue = _value.Replace("'", "\\'").Replace("\n", "\\n").Replace("\r", "");
                    string script = string.Format(
                        "if(window['{0}_setValue']) window['{0}_setValue']('{1}');",
                        this.ClientID,
                        safeValue);
                    this.Page.ClientScript.RegisterStartupScript(
                        this.GetType(),
                        "SetValue_" + this.ClientID,
                        script,
                        true);
                }
            }
        }

        /// <summary>
        /// Initialize controls - required by SimpleView base class
        /// </summary>
        /// <param name="container">The container</param>
        protected override void InitializeControls(GenericContainer container)
        {
            // Find the hidden field from the template
            var hiddenFieldId = this.ClientID + "_hiddenValue";
            _hiddenField = container.FindControl(hiddenFieldId) as HiddenField;
        }
    }
}
