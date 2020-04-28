using Moq;
using Solaris.Web.SolarApi.Core.Repositories.Interfaces;

namespace Solaris.Web.SolarApi.Tests.ServicesTests
{
    public class SolarSystemServiceTests
    {
        private readonly Mock<ISolarSystemRepository> m_repositoryMock;

        public SolarSystemServiceTests()
        {
            m_repositoryMock = new Mock<ISolarSystemRepository>();
        }
    }
}