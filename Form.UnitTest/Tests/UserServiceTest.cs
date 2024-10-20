using Form.Business.Services;
using Form.UnitTest.Mocks;
using Form.UnitTest.Mocks.Entities;

namespace Form.UnitTest.Tests
{
    public class UserServiceTest
    {
        [Fact]
        public async Task CreateUser()
        {
            var mockUserRepository = new MockUserRepository().CreateUser().Object;
            var userService = new UserService(mockUserRepository);
            var user = new LoaderUser().GetUser();
            await userService.CreateUser(user);
        }

        [Fact]
        public async Task CreateUserException()
        {
            var mockUserRepository = new MockUserRepository().CreateUserException().Object;
            var userService = new UserService(mockUserRepository);
            var user = new LoaderUser().GetUser();
            await Assert.ThrowsAsync<Exception>(() => userService.CreateUser(user));
        }
    }
}
