using FinanceManager.BLL.Repositories;
using FinanceManager.DB.Models;
using FinanceManager.DB;
using PlacesDB;


namespace FinanceManager.BLL.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FinanceContext _context;

        public IRepository<Account> Accounts { get; }
        public IRepository<Category> Categories { get; }
        public IRepository<Income> Incomes { get; }
        public IRepository<Expense> Expenses { get; }

        public UnitOfWork(FinanceContext context)
        {
            _context = context;
            Accounts = new GenericRepository<Account>(context);
            Categories = new GenericRepository<Category>(context);
            Incomes = new GenericRepository<Income>(context);
            Expenses = new GenericRepository<Expense>(context);
        }

        public int Complete() => _context.SaveChanges();

        public void Dispose() => _context.Dispose();
    }
}
