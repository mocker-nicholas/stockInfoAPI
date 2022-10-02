namespace stockInfoApi.Helpers
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
            Buy,
            Sell,
            Limit,
        }

        public static bool AccountTypeIsValid(int num)
        {
            return Enum.TryParse(Enum.GetName(typeof(AccountType), num), true, out AccountType accountType);
        }
    }
}
