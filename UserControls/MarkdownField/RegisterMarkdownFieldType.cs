using Telerik.Sitefinity.Web.UI.Fields;

namespace SitefinityWebApp.UserControls.MarkdownField
{
    /// <summary>
    /// Registers the Markdown field type with Sitefinity
    /// </summary>
    public static class RegisterMarkdownFieldType
    {
        /// <summary>
        /// Register the Markdown field control
        /// </summary>
        public static void Register()
        {
            FieldControls.Register(
                "MarkdownFieldControl",
                typeof(MarkdownFieldControl),
                "Markdown Editor (ToastUI)",
                "Allows editing text in Markdown format using Toast UI Editor."
            );
        }
    }
}
