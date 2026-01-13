using System;
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

        /// <summary>
        /// Gets or sets the value of the field control.
        /// </summary>
        public override object Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value != null ? value.ToString() : string.Empty;

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
    }
}
