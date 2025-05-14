using FinanceManager.DB.Models;


namespace FinanceManager.Repository.AccountsRepository
{
    public interface IAccountsRepository //I для CRUD
    {
        IEnumerable<Account> GetAccounts();
        Account GetAccountByID(int id);
        void InsertAccount(Account account);
        void UpdateAccount(Account account);
        void DeleteAccount(int id);
        void Save();
    }
}
