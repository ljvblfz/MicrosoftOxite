//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;

namespace Oxite.Modules.Blogs.Models
{
    public class PostTagComparer : IEqualityComparer<PostTag>
    {
        #region IEqualityComparer<PostTag> Members

        public bool Equals(PostTag x, PostTag y)
        {
            return string.Compare(x.Name, y.Name, true) == 0;
        }

        public int GetHashCode(PostTag obj)
        {
            return obj.GetHashCode();
        }

        #endregion
    }
}
