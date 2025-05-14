using FinanceManager.DB;
using FinanceManager.DB.Models;
using FinanceManager.Repository.AccountsRepository;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.ConsoleApp
{
    public class FinanceService
    {
        private readonly FinanceContext _context;
        private readonly AccountsRepository _accountRepo;

        public FinanceService(FinanceContext context)
        {
            _context = context;
            _accountRepo = new AccountsRepository(context);
        }

        // Accounts 
        public IEnumerable<Account> GetAllAccounts() => _accountRepo.GetAccounts();
        //create new account and  +  throw   Repository
        public void AddAccount(string name, decimal balance)
        {
            var account = new Account { Name = name, Balance = balance };
            _accountRepo.InsertAccount(account);
        }

        public void DeleteAccount(int id) => _accountRepo.DeleteAccount(id);

        public void UpdateAccount(int id, string name, decimal balance)
        {
            var account = _accountRepo.GetAccountByID(id);
            if (account != null)
            {
                account.Name = name;
                account.Balance = balance;
                _accountRepo.UpdateAccount(account);
            }
        }

        // Categories
        public IEnumerable<Category> GetCategories() => _context.Categories.ToList();

        public void AddCategory(string name, CategoryType type)
        {
            _context.Categories.Add(new Category { Name = name, Type = type });
            _context.SaveChanges();
        }

        // Transactions
        public void AddIncome(string name, string desc, decimal amount, DateTime date, int accountId, int categoryId)
        {
            var income = new Income
            {
                Name = name,
                Description = desc,
                Amount = amount,
                Date = date,
                Account = _context.Accounts.Find(accountId)!,
                Category = _context.Categories.Find(categoryId)!
            };
            _context.Incomes.Add(income);

            income.Account.Balance += amount;
            _context.SaveChanges();
        }

        public void AddExpense(string name, string desc, decimal amount, DateTime date, int accountId, int categoryId)
        {
            var expense = new Expense
            {
                Name = name,
                Description = desc,
                Amount = amount,
                Date = date,
                Account = _context.Accounts.Find(accountId)!,
                Category = _context.Categories.Find(categoryId)!
            };
            _context.Expenses.Add(expense);

            expense.Account.Balance -= amount;
            _context.SaveChanges();
        }

        public IEnumerable<BaseTransactionItem> GetTransactionsByAccount(int accountId)
        {
            return _context.Set<BaseTransactionItem>()
                .Include(t => t.Category)
                .Include(t => t.Account)
                .Where(t => t.Account.ID == accountId)
                .ToList();
        }

        public IEnumerable<BaseTransactionItem> GetTransactionsByCategory(int categoryId)
        {
            return _context.Set<BaseTransactionItem>()
                .Include(t => t.Category)
                .Include(t => t.Account)
                .Where(t => t.Category.ID == categoryId)
                .ToList();
        }
    }
}
