using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Pages;

namespace SitefinityWebApp.UserControls.MarkdownField
{
    /// <summary>
    /// Markdown field control using TOAST UI Editor for Dynamic Modules
    /// </summary>
    public partial class MarkdownFieldControl : FieldControl
    {
        #region Properties

        /// <summary>
        /// Gets the reference to the label control that represents the title of the field control.
        /// </summary>
        protected override WebControl TitleControl
        {
            get { return this.TitleLabel; }
        }

        /// <summary>
        /// Gets the reference to the label control that displays the description of the field control.
        /// </summary>
        protected override WebControl DescriptionControl
        {
            get { return this.DescriptionLabel; }
        }

        /// <summary>
        /// Gets the reference to the control that displays the example for this field control.
        /// </summary>
        protected override WebControl ExampleControl
        {
            get { return null; }
        }

        /// <summary>
        /// Gets a unique ID for the editor container
        /// </summary>
        protected string EditorContainerId
        {
            get { return "markdown-editor-" + this.ClientID; }
        }

        #endregion

        #region Overridden Methods

        /// <summary>
        /// Initialize the controls.
        /// </summary>
        /// <param name="container"></param>
        protected override void InitializeControls(GenericContainer container)
        {
            // Base initialization
            base.InitializeControls(container);
        }

        /// <summary>
        /// Called when the page is loaded
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Include TOAST UI Editor scripts and styles
            if (!Page.ClientScript.IsClientScriptIncludeRegistered("ToastUIEditorCSS"))
            {
                Page.ClientScript.RegisterClientScriptInclude(
                    "ToastUIEditorCSS",
                    this.ResolveUrl("~/ResourcePackages/Bootstrap/assets/vendor/toast-ui/toastui-editor.min.css"));
            }

            if (!Page.ClientScript.IsClientScriptIncludeRegistered("ToastUIEditorJS"))
            {
                Page.ClientScript.RegisterClientScriptInclude(
                    "ToastUIEditorJS",
                    this.ResolveUrl("~/ResourcePackages/Bootstrap/assets/vendor/toast-ui/toastui-editor-all.min.js"));
            }
        }

        /// <summary>
        /// Gets the value of the field control.
        /// </summary>
        public override object Value
        {
            get
            {
                return this.MarkdownTextBox.Text;
            }
            set
            {
                if (value != null)
                {
                    this.MarkdownTextBox.Text = value.ToString();
                }
                else
                {
                    this.MarkdownTextBox.Text = string.Empty;
                }
            }
        }

        #endregion

        #region Controls

        /// <summary>
        /// The title label
        /// </summary>
        protected Literal TitleLabel;

        /// <summary>
        /// The description label
        /// </summary>
        protected Literal DescriptionLabel;

        /// <summary>
        /// The hidden textbox that stores the markdown value
        /// </summary>
        protected TextBox MarkdownTextBox;

        #endregion
    }
}
