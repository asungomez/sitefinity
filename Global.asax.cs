using System;
using System.Web;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace SitefinityWebApp
{
    /// <summary>
    /// Global application class
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// Handles the Initialized event of the Sitefinity Bootstrapper.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedEventArgs"/> instance containing the event data.</param>
        private static void Bootstrapper_Initialized(object sender, ExecutedEventArgs e)
        {
            if (e.CommandName == "Bootstrapper")
            {
                // Register the Markdown field control for Dynamic Modules
                RegisterMarkdownFieldControl();
            }
        }

        /// <summary>
        /// Registers the Markdown field control as a custom field type for Dynamic Modules
        /// </summary>
        private static void RegisterMarkdownFieldControl()
        {
            try
            {
                // Register the field control as LongText type with custom control path
                var fieldType = typeof(string);
                var controlVirtualPath = "~/UserControls/MarkdownField/MarkdownFieldControl.ascx";

                // Register the type converter for the Markdown field
                TypeResolutionService.RegisterTypeConverter(
                    "Markdown",
                    new CustomFieldTypeConverter
                    {
                        ControlVirtualPath = controlVirtualPath,
                        DataFieldType = fieldType,
                        DesignerFieldControlVirtualPath = controlVirtualPath,
                        DisplayName = "Markdown Editor",
                        DbType = Telerik.Sitefinity.Data.Metadata.DbType.LongText
                    });

                Telerik.Sitefinity.Abstractions.Log.Write(
                    "Markdown field control registered successfully.",
                    Telerik.Sitefinity.Abstractions.ConfigurationPolicy.Trace);
            }
            catch (Exception ex)
            {
                Telerik.Sitefinity.Abstractions.Log.Write(
                    string.Format("Error registering Markdown field control: {0}", ex.Message),
                    Telerik.Sitefinity.Abstractions.ConfigurationPolicy.ErrorLog);
            }
        }

        /// <summary>
        /// Handles the Application_Start event
        /// </summary>
        protected void Application_Start(object sender, EventArgs e)
        {
            Bootstrapper.Initialized += Bootstrapper_Initialized;
        }

        /// <summary>
        /// Handles the Application_End event
        /// </summary>
        protected void Application_End(object sender, EventArgs e)
        {
            // Application end logic
        }

        /// <summary>
        /// Handles the Application_Error event
        /// </summary>
        protected void Application_Error(object sender, EventArgs e)
        {
            // Application error handling
        }

        /// <summary>
        /// Handles the Session_Start event
        /// </summary>
        protected void Session_Start(object sender, EventArgs e)
        {
            // Session start logic
        }

        /// <summary>
        /// Handles the Session_End event
        /// </summary>
        protected void Session_End(object sender, EventArgs e)
        {
            // Session end logic
        }
    }
}
