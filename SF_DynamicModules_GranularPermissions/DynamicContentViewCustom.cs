using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.DynamicModules.Web.UI.Frontend;

namespace SitefinityWebApp
{
    public class DynamicContentViewCustom : DynamicContentView
    {
        protected override void CreateChildControls()
        {
            base.DetailViewControl = new DynamicContentViewDetailCustom(this.DynamicManager);
            base.DetailViewControl.Host = this;
            base.MasterViewControl = new DynamicContentViewMasterCustom(this.DynamicManager);
            base.MasterViewControl.Host = this;
            base.CreateChildControls();
        }
    }
}