using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Web.UI.Frontend;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Model;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using System.Net;
using Telerik.Sitefinity.Services;

namespace SitefinityWebApp
{
    public class DynamicContentViewDetailCustom : DynamicContentViewDetail
    {
        public DynamicContentViewDetailCustom(DynamicModuleManager dynamicModuleManager = null)
            : base(dynamicModuleManager)
        {
        }

        public Dictionary<Guid, string> permissionsTaxa = new Dictionary<Guid, string>();

        protected override void InitializeControls(Telerik.Sitefinity.Web.UI.GenericContainer container)
        {
            base.InitializeControls(container);

            List<string> tagNames = new List<string>();
            GetAllTagNamesForDataItem(tagNames);

            //gets the current user
            var user = ClaimsManager.GetCurrentIdentity();

            if (!user.IsUnrestricted)
            {
                //gets current user role names
                var currentUserRoleNames = user.Roles.Select(r => r.Name.ToLower());

                bool isDynamicItemAllowed = false;

                //always allowed for everyone
                if (tagNames.Contains("pm-everyone"))
                {
                    isDynamicItemAllowed = true;
                    return;
                }

                foreach (var roleName in currentUserRoleNames)
                {
                    //filter dynamic items for Enterprise customers
                    if (tagNames.Contains(roleName))
                    {
                        isDynamicItemAllowed = true;
                        break;
                    }
                }

                if (!isDynamicItemAllowed)
                {
                    Redirect403();
                }
            }
        }

        private void GetAllTagNamesForDataItem(List<string> tagNames)
        {
            //get all tags for a data item
            var tagIds = base.DataItem.GetValue<TrackedList<Guid>>("permissions");
            TaxonomyManager taxaManager = new TaxonomyManager();
            foreach (var tagId in tagIds)
            {
                var taxa = taxaManager.GetTaxa<Taxon>().Where(t => t.Id == tagId).Single();
                tagNames.Add(taxa.Name.ToLower());
            }
        }

        private void Redirect403()
        {
            int statusCode = (int)HttpStatusCode.Forbidden;

            SystemManager.CurrentHttpContext.Response.StatusCode = statusCode;
            SystemManager.CurrentHttpContext.Response.End();
        }
    }
}