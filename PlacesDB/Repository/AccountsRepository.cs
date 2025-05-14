using FinanceManager.DB;
using FinanceManager.DB.Models;
using Microsoft.EntityFrameworkCore;


namespace FinanceManager.Repository.AccountsRepository
{
    public class AccountsRepository : IAccountsRepository, IDisposable
    {
        private FinanceContext context;

        public AccountsRepository(FinanceContext context)
        {
            this.context = context;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return context.Accounts.Include(a => a.Transactions).ToList();
        }

        public Account GetAccountByID(int id)
        {
            return context.Accounts.Include(a => a.Transactions).FirstOrDefault(a => a.ID == id);
        }

        public void InsertAccount(Account account)
        {
            context.Accounts.Add(account);
            Save();
        }

        public void UpdateAccount(Account account)
        {
            context.Entry(account).State = EntityState.Modified;
            Save();
        }

        public void DeleteAccount(int id) //видалення по айді
        {
            var account = context.Accounts.Find(id);
            if (account != null)
            {
                context.Accounts.Remove(account);
                Save();
            }
        }

        public void Save() => context.SaveChanges(); 

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing) context.Dispose(); // звільняємо контекст
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
