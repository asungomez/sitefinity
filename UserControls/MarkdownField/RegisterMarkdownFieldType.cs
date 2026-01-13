namespace SitefinityWebApp.UserControls.MarkdownField
{
    /// <summary>
    /// Registers the Markdown field type with Sitefinity
    /// Note: For Dynamic Module fields, registration is done through the Sitefinity admin UI.
    /// Use this fully qualified type name when adding the custom field:
    /// SitefinityWebApp.UserControls.MarkdownField.MarkdownFieldControl, SitefinityWebApp
    /// </summary>
    public static class RegisterMarkdownFieldType
    {
        /// <summary>
        /// Register method - kept for compatibility but not needed for Dynamic Module fields.
        /// Field controls are automatically discovered by Sitefinity when referenced by their full type name.
        /// </summary>
        public static void Register()
        {
            // No programmatic registration needed for Dynamic Module field controls
            // Simply reference the control by its fully qualified type name in the Sitefinity admin UI:
            // SitefinityWebApp.UserControls.MarkdownField.MarkdownFieldControl, SitefinityWebApp
        }
    }
}
