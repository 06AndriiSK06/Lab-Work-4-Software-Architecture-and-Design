using AutoMapper;
using FinanceManager.BLL.DTOs;
using FinanceManager.BLL.Repositories;
using FinanceManager.BLL.UoW;
using FinanceManager.DB.Models;

using System.Linq;

namespace FinanceManager.BLL
{
    public class FinanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FinanceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        //AccountService

        public IEnumerable<AccountDto> GetAllAccounts()
        {
            var accounts = _unitOfWork.Accounts.GetAll();
            return _mapper.Map<IEnumerable<AccountDto>>(accounts);
        }



        // + 
        public void AddAccount(string name, decimal balance)
        {
            var account = new Account { Name = name, Balance = balance };
            _unitOfWork.Accounts.Add(account);
            _unitOfWork.Complete();
        }
         
        // - 
        public void DeleteAccount(int id)
        {
            _unitOfWork.Accounts.Delete(id);
            _unitOfWork.Complete();
        }

        //Update
        public void UpdateAccount(int id, string name, decimal balance)
        {
            var account = _unitOfWork.Accounts.GetById(id);
            if (account == null) return;
            account.Name = name;
            account.Balance = balance;
            _unitOfWork.Accounts.Update(account);
            _unitOfWork.Complete();
        }



        //
        //
        //
        //
        //

        //CategoryService
        public IEnumerable<CategoryDto> GetCategories()
        {
            var categories = _unitOfWork.Categories.GetAll();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public void AddCategory(string name, CategoryType type)
        {
            _unitOfWork.Categories.Add(new Category { Name = name, Type = type });
            _unitOfWork.Complete();
        }



        //

        //
        //
        //
        //
        //TransactionService
        public void AddIncome(string name, string desc, decimal amount, DateTime date, int accId, int catId)
        {
            var account = _unitOfWork.Accounts.GetById(accId)!;
            var category = _unitOfWork.Categories.GetById(catId)!;
            var income = new Income
            {
                Name = name,
                Description = desc,
                Amount = amount,
                Date = date,
                Account = account,
                Category = category
            };
            account.Balance += amount;
            _unitOfWork.Incomes.Add(income);
            _unitOfWork.Complete();
        }

        public void AddExpense(string name, string desc, decimal amount, DateTime date, int accId, int catId)
        {
            var account = _unitOfWork.Accounts.GetById(accId)!;
            var category = _unitOfWork.Categories.GetById(catId)!;
            var expense = new Expense
            {
                Name = name,
                Description = desc,
                Amount = amount,
                Date = date,
                Account = account,
                Category = category
            };
            account.Balance -= amount;
            _unitOfWork.Expenses.Add(expense);
            _unitOfWork.Complete();
        }

        // Всі транзакції по акаунту: доходи + витрати 

        public IEnumerable<TransactionDto> GetTransactionsByAccount(int accountId)
        {
            var transactions = _unitOfWork.Incomes.GetAll(x => x.Account )
                .Where(t => t.Account.ID == accountId)
                .Cast<BaseTransactionItem>()
                .Concat(_unitOfWork.Expenses.GetAll( x => x.Account).Where(t => t.Account.ID == accountId)); //поєдную всі витрати та доходи

            return _mapper.Map<IEnumerable<TransactionDto>>(transactions);
        }

        public IEnumerable<TransactionDto> GetTransactionsByCategory(int categoryId)
        {
            var transactions = _unitOfWork.Incomes.GetAll(x => x.Category)
                .Where(t => t.Category.ID == categoryId)
                .Cast<BaseTransactionItem>()
             .Concat(_unitOfWork.Expenses.GetAll(x => x.Category).Where(t => t.Category.ID == categoryId)); //поєдную всі витрати та доходи

            return _mapper.Map<IEnumerable<TransactionDto>>(transactions);
        }
    }
}
