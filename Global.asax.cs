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
        /// Handles the Initialized event of the Sitefinity Bootstrapper.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private static void Bootstrapper_Initialized(object sender, EventArgs e)
        {
            // Log that the application has initialized
            Telerik.Sitefinity.Abstractions.Log.Write(
                "Sitefinity application initialized. Markdown field control available at ~/UserControls/MarkdownField/MarkdownFieldControl.ascx",
                Telerik.Sitefinity.Abstractions.ConfigurationPolicy.Trace);
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
