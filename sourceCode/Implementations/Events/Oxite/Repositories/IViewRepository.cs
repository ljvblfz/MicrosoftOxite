using System;

namespace Oxite.Repositories
{
    public interface IViewRepository
    {
        void AddView(string type, string entityType, Guid id, string requestIP);
    }
}