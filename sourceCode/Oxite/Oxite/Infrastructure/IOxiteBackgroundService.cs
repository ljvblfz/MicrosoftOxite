namespace Oxite.Infrastructure
{
    public interface IOxiteBackgroundService
    {
        void RegisterBackgroundServices(IBackgroundServiceRegistry registry);
    }
}