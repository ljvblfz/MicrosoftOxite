//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Principal;
using Oxite.Models;

namespace Oxite.Infrastructure
{
    [DataContract]
    public class User : IUser, ICacheEntity, IExtendedPropertyStore
    {
        private IEnumerable<Role> siteRoles;
        //private IEnumerable<KeyValuePair<Guid, Role>> blogRoles;
        //private IEnumerable<KeyValuePair<Guid, Role>> postRoles;
        //private IEnumerable<KeyValuePair<Guid, Role>> pageRoles;

        public User(Guid id, string userName, string displayName)
        {
            Identity = new UserIdentity(null, true, userName);
            ID = id;
            DisplayName = displayName;
            AuthenticationValues = new Dictionary<string, object>();
        }

        public User(Guid id, string userName, string displayName, string email, string emailHash, Language language, EntityState status)
            : this(id, userName, displayName)
        {
            Email = email;
            EmailHash = emailHash;
            LanguageDefault = language;
            Status = status;
        }

        public User(string userName, string displayName, string email, string emailHash, EntityState status)
        {
            Identity = new UserIdentity(null, true, userName);
            DisplayName = displayName;
            Email = email;
            EmailHash = emailHash;
            Status = status;
            AuthenticationValues = new Dictionary<string, object>();
        }

        public User(string userName, string displayName, string email, string emailHash, Language language, EntityState status)
            : this(userName, displayName, email, emailHash, status)
        {
            LanguageDefault = language;
        }

        public User(Guid id, string userName, string displayName, string email, string emailHash, EntityState status)
            : this(userName, displayName, email, emailHash, status)
        {
            ID = id;
        }

        public User(Guid id, string userName, string displayName, string email, string emailHash, EntityState status, IEnumerable<Role> siteRoles, IEnumerable<KeyValuePair<Guid, Role>> blogRoles, IEnumerable<KeyValuePair<Guid, Role>> postRoles, IEnumerable<KeyValuePair<Guid, Role>> pageRoles)
            : this(userName, displayName, email, emailHash, status)
        {
            ID = id;
            this.siteRoles = siteRoles;
            //this.blogRoles = blogRoles;
            //this.postRoles = postRoles;
            //this.pageRoles = pageRoles;
        }

        public Guid ID { get; private set; }
        [DataMember]
        public string DisplayName { get; private set; }
        [DataMember]
        public string Email { get; private set; }
        [DataMember]
        public string EmailHash { get; private set; }
        public EntityState Status { get; private set; }
        public Language LanguageDefault { get; private set; }
        public IEnumerable<Language> Languages
        {
            get { throw new NotImplementedException(); }
        }

        #region IUser Members

        public bool IsAuthenticated { get { return Identity.IsAuthenticated; } }
        public string Name { get { return Identity.Name; } }
        public IDictionary<string, object> AuthenticationValues { get; private set; }

        public T Cast<T>() where T : class, IUser
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IPrincipal Members

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return isInRole(role, siteRoles);
        }

        #endregion

        #region ICacheItem Members

        public string GetCacheItemKey()
        {
            return string.Format("User:{0:N}", ID);
        }

        public IEnumerable<ICacheEntity> GetCacheDependencyItems()
        {
            return Enumerable.Empty<ICacheEntity>();
        }

        #endregion

        #region IExtendedPropertyStore Members

        public IEnumerable<ExtendedProperty> ExtendedProperties { get; private set; }

        public string ScopeType
        {
            get { return this.GetType().FullName; }
        }

        public string ScopeKey
        {
            get { return ID.ToString("N"); }
        }

        #endregion

        #region Static Methods

        private static bool isInRole(string roleName, IEnumerable<Role> roles)
        {
            foreach (Role role in roles)
                if (string.Compare(role.Name, roleName, true) == 0)
                    return true;
                else if (role.Roles != null)
                    return isInRole(roleName, role.Roles);

            return false;
        }

        private static bool isInRole(string roleName, Guid id, IEnumerable<KeyValuePair<Guid, Role>> roles)
        {
            foreach (KeyValuePair<Guid, Role> role in roles)
                if (role.Key == id)
                    if (string.Compare(role.Value.Name, roleName, true) == 0)
                        return true;
                    else if (role.Value.Roles != null)
                        return isInRole(roleName, role.Value.Roles);

            return false;
        }

        #endregion
    }
}