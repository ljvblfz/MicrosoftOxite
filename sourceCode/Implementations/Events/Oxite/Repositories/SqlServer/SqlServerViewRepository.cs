using System;

namespace Oxite.Repositories.SqlServer
{
    public class SqlServerViewRepository : IViewRepository
    {

        private readonly OxiteDataContext context;

        public SqlServerViewRepository(OxiteDataContext context)
        {
            this.context = context;
        }

        public void AddView(string type, string entityType, Guid id, string requestIP)
        {
            context.oxite_ViewTrackings.InsertOnSubmit(new oxite_ViewTracking
                                                           {
                                                               EntityID = id,
                                                               EntityType = entityType,
                                                               IPAddress = requestIP,
                                                               Timestamp = DateTime.UtcNow,
                                                               ViewID = Guid.NewGuid(),
                                                               ViewType = type
                                                           });

            context.SubmitChanges();
        }
    }
}