//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Linq;
using Oxite.Models;
using Oxite.Modules.Membership.Models;

namespace Oxite.Modules.Membership.Repositories.SqlServer
{
    public class SqlServerRoleRepository : IRoleRepository
    {
        private OxiteMembershipDataContext context;

        public SqlServerRoleRepository(OxiteMembershipDataContext context)
        {
            this.context = context;
        }

        #region IRoleRepository Members

        public Role GetRole(Guid roleID)
        {
            return (
                from r in context.oxite_Roles
                where r.RoleID == roleID
                select projectRole(r)
                ).FirstOrDefault();
        }

        public Role GetRole(string roleName)
        {
            return (
                from r in context.oxite_Roles
                where r.RoleName == roleName
                select projectRole(r)
                ).FirstOrDefault();
        }

        public IQueryable<Role> GetSiteRoles()
        {
            return
                from r in context.oxite_Roles
                where (r.RoleType & (byte)RoleType.Site) == (byte)RoleType.Site && r.GroupRoleID == r.RoleID
                orderby r.RoleName
                select projectRoleRecursive(r);
        }

        public IQueryable<Role> FindRoles(RoleSearchCriteria criteria)
        {
            var query =
                from r in context.oxite_Roles
                where
                    (string.IsNullOrEmpty(criteria.RoleName) || r.RoleName.Contains(criteria.RoleName)) &&
                    ((criteria.RoleType & RoleType.Site) != RoleType.Site || (r.RoleType & (byte)RoleType.Site) == (byte)RoleType.Site) &&
                    ((criteria.RoleType & RoleType.Blog) != RoleType.Blog || (r.RoleType & (byte)RoleType.Blog) == (byte)RoleType.Blog) &&
                    ((criteria.RoleType & RoleType.Post) != RoleType.Post || (r.RoleType & (byte)RoleType.Post) == (byte)RoleType.Post) &&
                    ((criteria.RoleType & RoleType.Page) != RoleType.Page || (r.RoleType & (byte)RoleType.Page) == (byte)RoleType.Page)
                orderby r.RoleName
                select r;

            return query.Select(r => projectRole(r));
        }

        public Role Save(Role role)
        {
            oxite_Role roleToSave = null;

            if (role.ID != Guid.Empty)
                roleToSave = context.oxite_Roles.FirstOrDefault(r => r.RoleID == role.ID);

            if (roleToSave == null)
            {
                roleToSave = new oxite_Role();

                roleToSave.RoleID = role.ID != Guid.Empty ? role.ID : Guid.NewGuid();

                context.oxite_Roles.InsertOnSubmit(roleToSave);
            }

            roleToSave.GroupRoleID = role.Group != null && role.Group.ID != Guid.Empty ? role.Group.ID : roleToSave.RoleID;
            roleToSave.RoleName = role.Name;
            roleToSave.RoleType = (byte)role.Type;

            context.SubmitChanges();

            return GetRole(roleToSave.RoleID);
        }

        public bool Remove(string roleName)
        {
            bool roleRemoved = false;
            oxite_Role role = context.oxite_Roles.FirstOrDefault(r => r.RoleName == roleName);

            if (role != null)
            {
                context.oxite_SiteRoleUserRelationships.DeleteAllOnSubmit(from sru in context.oxite_SiteRoleUserRelationships where sru.RoleID == role.RoleID select sru);
                //context.oxite_BlogRoleUserRelationships.DeleteAllOnSubmit(from aru in context.oxite_BlogRoleUserRelationships where aru.RoleID == role.RoleID select aru);
                context.oxite_PostRoleUserRelationships.DeleteAllOnSubmit(from pru in context.oxite_PostRoleUserRelationships where pru.RoleID == role.RoleID select pru);
                context.oxite_PageRoleUserRelationships.DeleteAllOnSubmit(from pru in context.oxite_PageRoleUserRelationships where pru.RoleID == role.RoleID select pru);
                context.oxite_Roles.DeleteOnSubmit(role);

                context.SubmitChanges();

                roleRemoved = true;
            }

            return roleRemoved;
        }

        #endregion

        #region Private Methods

        private static Role projectRole(oxite_Role role)
        {
            return new Role(role.GroupRoleID != role.RoleID ? new Role(role.GroupRoleID) : null, (RoleType)role.RoleType, role.RoleID, role.RoleName);
        }

        private Role projectRoleRecursive(oxite_Role role)
        {
            Role newRole = new Role(null, (RoleType)role.RoleType, role.RoleID, role.RoleName);

            foreach (oxite_Role childRole in context.oxite_Roles.Where(r => r.GroupRoleID == role.RoleID))
                 projectRoleRecursive(newRole, childRole);

            return newRole;
        }

        private void projectRoleRecursive(Role group, oxite_Role role)
        {
            Role newRole = group.AddRole((RoleType)role.RoleType, role.RoleID, role.RoleName);

            foreach (oxite_Role childRole in context.oxite_Roles.Where(r => r.GroupRoleID == role.RoleID))
                projectRoleRecursive(newRole, childRole);
        }

        #endregion
    }
}
