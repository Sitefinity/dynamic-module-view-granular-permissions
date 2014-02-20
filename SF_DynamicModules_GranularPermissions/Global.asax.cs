using System;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.DynamicModules.Events;
using Telerik.Sitefinity.Services;

namespace SitefinityWebApp
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Bootstrapper.Initialized += Bootstrapper_Initialized;
        }

        void Bootstrapper_Initialized(object sender, Telerik.Sitefinity.Data.ExecutedEventArgs e)
        {
            if (e.CommandName == "Bootstrapped")
            {
                EventHub.Subscribe<IDynamicContentCreatedEvent>(ReportCreated);
                EventHub.Subscribe<IDynamicContentUpdatedEvent>(ReportleUpdated);
                EventHub.Subscribe<IDynamicContentDeletedEvent>(ReportDeleted);
            }
        }

        private void ReportDeleted(IDynamicContentDeletedEvent @event)
        {
            DynamicContentViewMasterCustom.permissionsTaxa.Clear();
        }

        private void ReportleUpdated(IDynamicContentUpdatedEvent @event)
        {
            DynamicContentViewMasterCustom.permissionsTaxa.Clear();
        }

        private void ReportCreated(IDynamicContentCreatedEvent @event)
        {
            DynamicContentViewMasterCustom.permissionsTaxa.Clear();
        }
    }
}