//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Models;
using Oxite.ModelsBinders;

namespace Oxite.Infrastructure
{
    public class OxiteRegisterModelBinders : IRegisterModelBinders
    {
        #region IRegisterModelBinders Members

        public void RegisterModelBinders(ModelBinderDictionary modelBinders)
        {
            modelBinders[typeof(ArchiveData)] = new ArchiveDataModelBinder();
            modelBinders[typeof(Area)] = new AreaModelBinder();
            modelBinders[typeof(Comment)] = new CommentModelBinder();
            modelBinders[typeof(PostBase)] = new PostBaseModelBinder();
            modelBinders[typeof(Post)] = new PostModelBinder();
            modelBinders[typeof(Page)] = new PageModelBinder();
            modelBinders[typeof(SearchCriteria)] = new SearchCriteriaModelBinder();
            modelBinders[typeof(Tag)] = new TagModelBinder();
            modelBinders[typeof(UserBase)] = new UserBaseModelBinder();
            modelBinders[typeof(Site)] = new SiteModelBinder();
            //modelBinders[typeof(Plugin)] = new PluginModelBinder();
            modelBinders[typeof(AreaSearchCriteria)] = new AreaSearchCriteriaModelBinder();
            modelBinders[typeof(User)] = new UserModelBinder();
            modelBinders[typeof(PostAddress)] = new PostAddressModelBinder();
            modelBinders[typeof(FileAddress)] = new FileAddressModelBinder();
            modelBinders[typeof(FileInput)] = new FileInputModelBinder();
            modelBinders[typeof(FileContentInput)] = new FileContentInputModelBinder();
        }

        #endregion
    }
}
