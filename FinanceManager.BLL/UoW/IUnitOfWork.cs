using FinanceManager.BLL.Repositories;
using FinanceManager.DB.Models;
using PlacesDB;


namespace FinanceManager.BLL.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Account> Accounts { get; }
        IRepository<Category> Categories { get; }
        IRepository<Income> Incomes { get; }
        IRepository<Expense> Expenses { get; }
        int Complete();
    }
}
