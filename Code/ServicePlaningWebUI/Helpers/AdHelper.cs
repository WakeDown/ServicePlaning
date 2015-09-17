using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using ServicePlaningWebUI.Models;
using ServicePlaningWebUI.Objects;

namespace ServicePlaningWebUI.Helpers
{
    public class AdHelper
    {
        public static List<ListClass> GetGroupListFromAdUnit(string adQuery)
        {
            List<ListClass> list = new List<ListClass>();

            var domain = new PrincipalContext(ContextType.Domain, "UN1T.GROUP", String.Format("{0}, DC=UN1T,DC=GROUP", adQuery));
            GroupPrincipal groupList = new GroupPrincipal(domain, "*");
            PrincipalSearcher ps = new PrincipalSearcher(groupList);

            foreach (var grp in ps.FindAll())
            {
                list.Add(new ListClass() {Name =grp.Name,Id= grp.Sid.ToString()});
            }

            return list;
        }

        public static List<AdGroup> GetUserAdGroups(User user)
        {
            List<AdGroup> result = new List<AdGroup>();

            // establish domain context
            PrincipalContext yourDomain = new PrincipalContext(ContextType.Domain);
            
            // find your user
            UserPrincipal up = UserPrincipal.FindByIdentity(yourDomain, user.Login);

            // if found - grab its groups
            if (up != null)
            {
                //PrincipalSearchResult<Principal> groups = up.GetAuthorizationGroups();

                //// iterate over all groups
                //foreach (Principal p in groups)
                //{
                //    // make sure to add only group principals
                //        if (p is GroupPrincipal)
                //        {
                //            result.Add(new AdGroup() { SID = p.Sid.Value, Name = p.DisplayName });
                //        }
                //}

                PrincipalSearchResult<Principal> groups = up.GetAuthorizationGroups();

                var iterGroup = groups.GetEnumerator();
                using (iterGroup)
                {
                    while (iterGroup.MoveNext())
                    {
                        try
                        {
                            Principal p = iterGroup.Current;
                            //result.Add((GroupPrincipal)p);
                            result.Add(new AdGroup() { SID = p.Sid.Value, Name = p.DisplayName });
                        }
                        catch (PrincipalOperationException)
                        {
                            continue;
                        }
                    }
                }
            }

            return result;
        }
    }
}