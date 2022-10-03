using System.Text.RegularExpressions;

namespace stockInfoApi.Helpers
{
    public class Validations
    {
        public static bool ValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidNickname(string nickname)
        {
            string pattern = "^[a-zA-Z][a-zA-Z0-9]*$";
            var result = Regex.IsMatch(nickname, pattern);
            if(result)
                return true;
            return false;
        }
    }
}
