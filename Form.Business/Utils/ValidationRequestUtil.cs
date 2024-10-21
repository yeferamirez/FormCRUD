
namespace Form.Business.Utils
{
    public static class ValidationRequestUtil
    {
        public static bool IsValidDate(string dateRequest)
        {
            try
            {
                DateTime.Parse(dateRequest);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
