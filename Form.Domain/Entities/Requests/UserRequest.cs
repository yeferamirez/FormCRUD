namespace Form.Domain.Entities.Requests
{
    public class UserRequest
    {
        public string? FirstName { get; set; }
        public string? FirstLastname { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
