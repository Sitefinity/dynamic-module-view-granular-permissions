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

        protected override void InitializeControls(Telerik.Sitefinity.Web.UI.GenericContainer container)
        {
            base.InitializeControls(container);

            List<string> taxonNames = new List<string>();
            GetAllTaxonNamesForDataItem(taxonNames);

            //gets the current user
            var user = ClaimsManager.GetCurrentIdentity();

            if (!user.IsUnrestricted)
            {
                //gets current user role names
                var currentUserRoleNames = user.Roles.Select(r => r.Name.ToLower());

                bool isDynamicItemAllowed = false;

                //always allowed for everyone
                if (taxonNames.Contains("pm-everyone"))
                {
                    isDynamicItemAllowed = true;
                    return;
                }

                foreach (var roleName in currentUserRoleNames)
                {
                    //filter dynamic items for Enterprise customers
                    if (taxonNames.Contains(roleName))
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

        private void GetAllTaxonNamesForDataItem(List<string> taxonNames)
        {
            //get all tags for a data item
            var taxaIds = base.DataItem.GetValue<TrackedList<Guid>>("permissions");
            TaxonomyManager taxaManager = new TaxonomyManager();
            foreach (var taxaId in taxaIds)
            {
                var taxa = taxaManager.GetTaxa<Taxon>().Where(t => t.Id == taxaId).Single();
                taxonNames.Add(taxa.Name.ToLower());
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