using System.Text.RegularExpressions;

namespace ISPH.Infrastructure.Services.Utils.Validation
{
    public static class DataValidator
    {
        public static bool IsValidPassword(string password)
        {
            string expr = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$";
            Regex re = new Regex(expr, RegexOptions.IgnoreCase);
            return re.IsMatch(password);
        }

        public static bool IsValidEmail(string email)
        {
            string strRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            Regex re = new Regex(strRegex, RegexOptions.IgnoreCase);
            return re.IsMatch(email);
        }
    }
}
