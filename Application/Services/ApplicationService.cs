using Interfaces;

namespace Demo.Services
{
    public class ApplicationService : IApplicationService
    {
        public string Test()
        {
            return "Application Service Tested!";
        }
    }
}
