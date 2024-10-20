using Form.Domain.Entities;
using Form.UnitTest.Utils;
using Newtonsoft.Json;

namespace Form.UnitTest.Mocks.Entities
{
    public class LoaderUser
    {
        public User GetUser()
        {
            return JsonConvert.DeserializeObject<User>(JsonLoaderUtil.LoadJsonFile("./JsonFiles/User/User.json"));
        }

        public List<User> GetListUsers()
        {
            return new List<User>
            {
                GetUser()
            };
        }
    }
}
