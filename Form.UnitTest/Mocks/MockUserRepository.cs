using Form.Domain.Entities;
using Form.Infrastructure.Interfaces.Repositories;
using Moq;

namespace Form.UnitTest.Mocks
{
    public class MockUserRepository : Mock<IUserRepository>
    {
        public MockUserRepository CreateUser()
        {
            Setup(x => x.CreateUser(It.IsAny<User>()));
            return this;
        }

        public MockUserRepository CreateUserException()
        {
            Setup(x => x.CreateUser(It.IsAny<User>())).ThrowsAsync(new Exception("Mock Exception"));
            return this;
        }
    }
}
