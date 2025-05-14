using FinanceManager.DB;
using FinanceManager.Repository.AccountsRepository;

using Microsoft.EntityFrameworkCore;
using FinanceManager.DB.Models;

Console.OutputEncoding = System.Text.Encoding.UTF8;

using var context = new FinanceContext();
var accountsRepo = new AccountsRepository(context);

// додаю тестові
if (!context.Accounts.Any())
{
    context.Accounts.Add(new Account
    {
        Name = "Основний рахунок",
        Balance = 5000m
    });

    context.Accounts.Add(new Account
    {
        Name = "Рахунок для збережень",
        Balance = 15000m
    });

    context.SaveChanges(); // сейвлю зміни В БД
}

// вивід рахунків
Console.WriteLine("Список рахунків:");
foreach (var acc in accountsRepo.GetAccounts())
{
    Console.WriteLine($"ID: {acc.ID} | Назва: {acc.Name} | Баланс: {acc.Balance} грн");
}
