//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Models;
using Oxite.Modules.Membership.Models;

namespace Oxite.Modules.Membership.Repositories.SqlServer
{
    public class SqlServerUserRepository : IUserRepository
    {
        private readonly OxiteMembershipDataContext context;

        public SqlServerUserRepository(OxiteMembershipDataContext context)
        {
            this.context = context;
        }

        #region IUserRepository Members

        public UserAuthenticated GetUser(Guid siteID, Guid id)
        {
            return (
                from u in context.oxite_Users
                where u.UserID == id && u.Status == (byte)EntityState.Normal
                select projectUser(siteID, u)
                ).FirstOrDefault();
        }

        public UserAuthenticated GetUser(Guid siteID, string name)
        {
            return (
                from u in context.oxite_Users
                where u.Username == name && u.Status == (byte)EntityState.Normal
                select projectUser(siteID, u)
                ).FirstOrDefault();
        }

        public UserAuthenticated GetUserByModuleData(Guid siteID, string moduleName, string data)
        {
            return (
                from u in context.oxite_Users
                join umd in context.oxite_UserModuleDatas on u.UserID equals umd.UserID
                where u.Status == (byte)EntityState.Normal && string.Compare(umd.ModuleName, moduleName, true) == 0 && umd.Data == data
                select projectUser(siteID, u)
                ).FirstOrDefault();
        }

        public IQueryable<UserAuthenticated> FindUsers(UserSearchCriteria criteria)
        {
            return
                from u in context.oxite_Users
                where u.Status == (byte)EntityState.Normal && (u.Username.Contains(criteria.Name) || u.DisplayName.Contains(criteria.Name))
                select new UserAuthenticated(u.UserID, u.Username, u.DisplayName);
        }

        public UserAuthenticated Save(UserAuthenticated user, Guid siteID)
        {
            oxite_User userToSave = null;

            if (user.ID != Guid.Empty)
                userToSave = context.oxite_Users.FirstOrDefault(u => u.UserID == user.ID);

            if (userToSave == null)
            {
                userToSave = new oxite_User();

                userToSave.UserID = user.ID != Guid.Empty ? user.ID : Guid.NewGuid();

                context.oxite_Users.InsertOnSubmit(userToSave);
            }

            //TODO: (erikpo) Move these fields into a profile table (except maybe username?)
            if (user.LanguageDefault != null && user.LanguageDefault.ID != Guid.Empty)
                userToSave.DefaultLanguageID = user.LanguageDefault.ID;
            else
                userToSave.DefaultLanguageID = context.oxite_Languages.Where(l => l.LanguageName == "en").First().LanguageID;
            userToSave.Username = user.Name;
            userToSave.DisplayName = user.DisplayName;
            userToSave.Email = user.Email;
            userToSave.HashedEmail = user.EmailHash;
            userToSave.Status = (byte)user.Status;
            //TODO: (erikpo) Add url fields

            context.SubmitChanges();

            return GetUser(siteID, userToSave.UserID);
        }

        public bool Remove(Guid userID)
        {
            oxite_User foundUser = context.oxite_Users.FirstOrDefault(u => u.UserID == userID && u.Status != (byte)EntityState.Removed);
            bool removedUser = false;

            if (foundUser != null)
            {
                foundUser.Status = (byte)EntityState.Removed;

                context.SubmitChanges();

                removedUser = true;
            }

            return removedUser;
        }

        public string GetModuleData(Guid siteID, string userName, string moduleName)
        {
            UserAuthenticated user = GetUser(siteID, userName);

            return GetModuleData(siteID, user != null ? user.ID : Guid.Empty, moduleName);
        }

        public string GetModuleData(Guid siteID, Guid userID, string moduleName)
        {
            oxite_UserModuleData umData = context.oxite_UserModuleDatas.FirstOrDefault(umd => umd.UserID == userID && string.Compare(umd.ModuleName, moduleName, true) == 0);

            if (umData != null)
                return umData.Data;
            else
                return null;
        }

        public void SetModuleData(Guid siteID, string userName, string moduleName, string data)
        {
            UserAuthenticated user = GetUser(siteID, userName);

            SetModuleData(siteID, user != null ? user.ID : Guid.Empty, moduleName, data);
        }

        public void SetModuleData(Guid siteID, Guid userID, string moduleName, string data)
        {
            if (userID != null)
            {
                oxite_UserModuleData umData = context.oxite_UserModuleDatas.FirstOrDefault(umd => umd.UserID == userID && string.Compare(umd.ModuleName, moduleName, true) == 0);

                if (umData != null)
                    umData.Data = data;
                else
                    context.oxite_UserModuleDatas.InsertOnSubmit(
                        new oxite_UserModuleData
                        {
                            UserID = userID,
                            ModuleName = moduleName,
                            Data = data
                        }
                        );

                context.SubmitChanges();
            }
        }

        #endregion

        #region Private Methods

        private UserAuthenticated projectUser(Guid siteID, oxite_User user)
        {
            var siteRoles =
                from sru in context.oxite_SiteRoleUserRelationships
                join r in context.oxite_Roles on sru.RoleID equals r.RoleID
                where sru.SiteID == siteID && sru.UserID == user.UserID
                select projectRoleRecursive(r, RoleType.Site);
            //var blogRoles =
            //    from aru in context.oxite_BlogRoleUserRelationships
            //    join r in context.oxite_Roles on aru.RoleID equals r.RoleID
            //    where aru.UserID == user.UserID
            //    select new KeyValuePair<Guid, Role>(aru.BlogID, projectRoleRecursive(r, RoleType.Blog));
            //var postRoles =
            //    from pru in context.oxite_PostRoleUserRelationships
            //    join r in context.oxite_Roles on pru.RoleID equals r.RoleID
            //    where pru.UserID == user.UserID
            //    select new KeyValuePair<Guid, Role>(pru.PostID, projectRoleRecursive(r, RoleType.Post));
            //var pageRoles =
            //    from pru in context.oxite_PageRoleUserRelationships
            //    join r in context.oxite_Roles on pru.RoleID equals r.RoleID
            //    where pru.UserID == user.UserID
            //    select new KeyValuePair<Guid, Role>(pru.PageID, projectRoleRecursive(r, RoleType.Page));

            return new UserAuthenticated(user.UserID, user.Username, user.DisplayName, user.Email, user.HashedEmail, (EntityState)user.Status, siteRoles.ToList(), Enumerable.Empty<KeyValuePair<Guid, Role>>(), Enumerable.Empty<KeyValuePair<Guid, Role>>(), Enumerable.Empty<KeyValuePair<Guid, Role>>());
        }

        private Role projectRoleRecursive(oxite_Role role, RoleType type)
        {
            Role newRole = new Role(null, (RoleType)role.RoleType, role.RoleID, role.RoleName);

            foreach (oxite_Role childRole in context.oxite_Roles.Where(r => r.GroupRoleID == role.RoleID && r.RoleID != role.RoleID && (r.RoleType & (byte)type) == (byte)type))
                projectRoleRecursive(newRole, childRole, type);

            return newRole;
        }

        private void projectRoleRecursive(Role group, oxite_Role role, RoleType type)
        {
            Role newRole = group.AddRole((RoleType)role.RoleType, role.RoleID, role.RoleName);

            foreach (oxite_Role childRole in context.oxite_Roles.Where(r => r.GroupRoleID == role.RoleID && r.RoleID != role.RoleID && (r.RoleType & (byte)type) == (byte)type))
                projectRoleRecursive(newRole, childRole, type);
        }

        #endregion
    }
}
