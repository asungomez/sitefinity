using System;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Services;

namespace SitefinityWebApp
{
    /// <summary>
    /// Global application class
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// Handles the Application_Start event
        /// </summary>
        protected void Application_Start(object sender, EventArgs e)
        {
            Bootstrapper.Initialized += Bootstrapper_Initialized;
        }

        /// <summary>
        /// Handles the Initialized event of the Sitefinity Bootstrapper.
        /// </summary>
        private static void Bootstrapper_Initialized(object sender, EventArgs e)
        {
            // Register the Markdown field control
            UserControls.MarkdownField.RegisterMarkdownFieldType.Register();

            Log.Write("Markdown field control registered successfully.", ConfigurationPolicy.Trace);
        }

        /// <summary>
        /// Handles the Application_End event
        /// </summary>
        protected void Application_End(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Application_Error event
        /// </summary>
        protected void Application_Error(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Session_Start event
        /// </summary>
        protected void Session_Start(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Session_End event
        /// </summary>
        protected void Session_End(object sender, EventArgs e)
        {
        }
    }
}
