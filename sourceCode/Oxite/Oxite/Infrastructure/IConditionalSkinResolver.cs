using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oxite.Infrastructure
{
    public interface IConditionalSkinResolver : ISkinResolver
    {
        bool CanResolve(SkinResolverContext context);
    }
}
