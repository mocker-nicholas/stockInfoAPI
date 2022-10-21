namespace stockInfoApi.DAL.Enums
{
    public class Enums
    {
       public enum AccountType
        {
            Admin = 0,
            Standard = 1,
        }

        public enum TransactionType
        {
            Buy = 0,
            Sell = 1,
            Limit = 2,
        }

        public static bool AccountTypeIsValid(int num)
        {
            return Enum.TryParse(Enum.GetName(typeof(AccountType), num), true, out AccountType accountType);
        }
        public static bool TransactionTypeIsValid(int num)
        {
            return Enum.TryParse(Enum.GetName(typeof(AccountType), num), true, out AccountType accountType);
        }
    }
}
