using Form.Domain.Entities;
using Form.Domain.Entities.Requests;

namespace Form.Infrastructure.Utils
{
    public static class BuildObjectsUtil
    {
        public static User BuildUser(User userRequest)
        {
            userRequest.CreationDate = DateTime.Now;
            userRequest.ModificationDate = DateTime.Now;
            return userRequest;
        }

        public static void BuildUserPagination(UserRequest userRequest, IQueryable<User> query)
        {
            if (!string.IsNullOrWhiteSpace(userRequest.FirstName))
            {
                query = query.Where(u => u.FirstName.Equals(userRequest.FirstName));
            }

            if (!string.IsNullOrWhiteSpace(userRequest.FirstLastname))
            {
                query = query.Where(u => u.FirstLastname.Equals(userRequest.FirstLastname));
            }
        }
    }
}
