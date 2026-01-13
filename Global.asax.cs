using System;
using System.Linq;
using System.Web;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using SitefinityWebApp.Mvc.Controllers;

namespace SitefinityWebApp
{
    /// <summary>
    /// Global application class
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// Handles the Initializing event of the Sitefinity Bootstrapper.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutingEventArgs"/> instance containing the event data.</param>
        private static void Bootstrapper_Initializing(object sender, ExecutingEventArgs e)
        {
            // This event is raised during the initialization of Sitefinity
            // Use it to register custom types, routes, etc.
        }

        /// <summary>
        /// Handles the Initialized event of the Sitefinity Bootstrapper.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private static void Bootstrapper_Initialized(object sender, ExecutedEventArgs e)
        {
            if (e.CommandName == "Bootstrapper")
            {
                // Register the Markdown field control
                RegisterMarkdownFieldControl();
            }
        }

        /// <summary>
        /// Registers the Markdown field control with the Forms module
        /// </summary>
        private static void RegisterMarkdownFieldControl()
        {
            try
            {
                var configManager = Telerik.Sitefinity.Configuration.Config.GetManager();
                var formsConfig = configManager.GetSection<Telerik.Sitefinity.Modules.Forms.Configuration.FormsConfig>();

                // Check if already registered
                var existingControl = formsConfig.ToolboxItems.Elements
                    .FirstOrDefault(e => e.Name == "MarkdownField");

                if (existingControl == null)
                {
                    // Create toolbox item for the Markdown field
                    var markdownFieldItem = new Telerik.Sitefinity.Modules.Forms.Configuration.FormToolboxItem(formsConfig.ToolboxItems)
                    {
                        Name = "MarkdownField",
                        Title = "Markdown Editor",
                        Description = "A rich text markdown editor field using TOAST UI Editor",
                        ControllerType = typeof(MarkdownFieldController).AssemblyQualifiedName,
                        CssClass = "sfMarkdownFieldIcn sfMvcIcn",
                        SectionName = "Common",
                        Ordinal = 0.5f, // Position in the toolbox
                        ModuleName = "Forms"
                    };

                    // Add to the toolbox
                    formsConfig.ToolboxItems.Add(markdownFieldItem);

                    // Save the configuration
                    configManager.SaveSection(formsConfig);

                    SystemManager.RestartApplication(OperationReason.Configuration);
                }
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
            Bootstrapper.Initializing += Bootstrapper_Initializing;
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
