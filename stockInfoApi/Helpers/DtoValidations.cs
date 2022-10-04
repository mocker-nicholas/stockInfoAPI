using stockInfoApi.Models.AccountDtos;
using stockInfoApi.Models.HelperModels;
using System.Text.RegularExpressions;

namespace stockInfoApi.Helpers
{
    public class DtoValidations
    {
        public static ValidationCheck ValidPutAccountDto (PutAccountDto req)
        {
            var email = ValidEmail(req.EmailAddress);
            var nickname = ValidNickname(req.Nickname);
            var accountType = Enums.AccountTypeIsValid((int)req.AccountType);
            if (!email)
                return new ValidationCheck(true, "Invalid email address");
            else if (!nickname)
                return new ValidationCheck(true, "Nickname can only contain letters and numbers");
            else if (!accountType)
                return new ValidationCheck(true, "Invalid account type");
            else
                return new ValidationCheck(false, ""); 
        }

        public static ValidationCheck ValidPostAccountDto (PostAccountDto req)
        {
            var email = ValidEmail(req.EmailAddress);
            var nickname = ValidNickname(req.Nickname);
            var accountType = Enums.AccountTypeIsValid((int)req.AccountType);
            if (!email)
                return new ValidationCheck(true, "Invalid email address");
            else if (!nickname)
                return new ValidationCheck(true, "Nickname can only contain letters and numbers");
            else if (!accountType)
                return new ValidationCheck(true, "Invalid account type");
            else
                return new ValidationCheck(false, "");
        }

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
