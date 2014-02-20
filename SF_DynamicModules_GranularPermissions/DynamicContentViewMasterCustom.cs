using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.DynamicModules.Web.UI.Frontend;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace SitefinityWebApp
{
    public class DynamicContentViewMasterCustom : DynamicContentViewMaster
    {
        public DynamicContentViewMasterCustom(DynamicModuleManager dynamicModuleManager = null)
            : base(dynamicModuleManager)
        {
            CachePermissionsTaxa();
        }

        public static Dictionary<Guid, string> permissionsTaxa = new Dictionary<Guid, string>();

        protected override IQueryable<DynamicContent> SetExpressions(IQueryable<DynamicContent> query, string filterExpression, string sortExpression, int? skip, int? take, ref int? totalCount)
        {
            filterExpression = SetCustomFilterExpression(filterExpression);
            return base.SetExpressions(query, filterExpression, sortExpression, skip, take, ref totalCount);
        }

        private void CachePermissionsTaxa()
        {
            if (permissionsTaxa.Count == 0)
            {
                TaxonomyManager taxaManager = TaxonomyManager.GetManager();
                var permissions = taxaManager.GetTaxa<FlatTaxon>().Where(t => t.Taxonomy.Name == "permissions");
                foreach (var permission in permissions)
                {
                    if (!permissionsTaxa.Contains(new KeyValuePair<Guid, string>(permission.Id, permission.Name)))
                    {
                        permissionsTaxa.Add(permission.Id, permission.Name);
                    }
                }
            }
        }

        public string SetCustomFilterExpression(string filterExpression)
        {
            //gets the current user
            var user = ClaimsManager.GetCurrentIdentity();
            if (user.IsUnrestricted)
            {
                //Administrators are always unrestricted
                filterExpression += "";
                return filterExpression;
            }

            //gets current user roles
            var currentUserRoleNames = user.Roles.Select(a => a.Name).Where(b => b.StartsWith("pm-"));
            List<Guid> taxonIds = new List<Guid>();

            foreach (var roleName in currentUserRoleNames)
            {
                var taxonId = GetTaxaIdByCurrentRole(roleName);
                if (taxonId != Guid.Empty)
                    taxonIds.Add(taxonId);
            }

            if (taxonIds.Count > 0)
            {
                //filter dynamic content items according to the current user roles 
                foreach (var taxonId in taxonIds)
                {
                    filterExpression += string.Format(" AND Permissions.Contains(({0}))", taxonId.ToString());
                }
            }

            //show the dynamic items that are with tag for everyone
            var everyoneTaxa = permissionsTaxa.FirstOrDefault(p => p.Value.ToLower().Equals("pm-everyone"));
            if (everyoneTaxa.Key != Guid.Empty)
            {
                if (filterExpression.Contains("Permissions"))
                {
                    filterExpression += string.Format(" OR Visible = true AND Status = Live AND Permissions.Contains(({0}))", everyoneTaxa.Key);
                }
                else
                {
                    filterExpression += string.Format(" AND Permissions.Contains(({0}))", everyoneTaxa.Key);
                }
            }

            return filterExpression;
        }

        private Guid GetTaxaIdByCurrentRole(string role)
        {
            var taxonomy = permissionsTaxa.FirstOrDefault(p => p.Value.ToLower().Equals(role));
            return taxonomy.Key;
        }
    }
}