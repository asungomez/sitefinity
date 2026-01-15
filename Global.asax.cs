using System;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using SitefinityWebApp.UserControls.MarkdownField;

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
            // Register custom field control when Sitefinity bootstraps
            Bootstrapper.Bootstrapped += Bootstrapper_Bootstrapped;
        }

        private void Bootstrapper_Bootstrapped(object sender, EventArgs e)
        {
            // Log to confirm bootstrapping works and our type is loadable
            Log.Write("=== SitefinityWebApp Bootstrapped ===", ConfigurationPolicy.Trace);

            // Verify the MarkdownFieldControl type can be loaded
            var controlType = typeof(MarkdownFieldControl);
            Log.Write($"MarkdownFieldControl type: {controlType.AssemblyQualifiedName}", ConfigurationPolicy.Trace);
            Log.Write($"MarkdownFieldControl assembly: {controlType.Assembly.FullName}", ConfigurationPolicy.Trace);
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
