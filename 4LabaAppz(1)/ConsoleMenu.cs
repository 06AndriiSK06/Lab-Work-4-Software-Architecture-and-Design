using FinanceManager.ConsoleApp;
using FinanceManager.DB.Models;

class ConsoleMenu
{
    private readonly FinanceService _service;

    public ConsoleMenu(FinanceService service)
    {
        _service = service;
    }

    public void Show()
    {
        while (true)
        {
            Console.WriteLine("\n Фінансовий Менеджер");
            Console.WriteLine("1.Переглянути рахунки");
            Console.WriteLine("2.Додати рахунок");
            Console.WriteLine("3.Видалити рахунок");
            Console.WriteLine("4.Оновити рахунок");
            Console.WriteLine("5.Додати категорію");
            Console.WriteLine("6.Додати дохід");
            Console.WriteLine("7.Додати витрату");
            Console.WriteLine("8.Показати операції по рахунку");
            Console.WriteLine("9.Показати операції по категорії");
            Console.WriteLine("0.Вийти");
            Console.Write("Ваш вибір: ");

            Console.Write("Оберіть опцію: ");
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ShowAccounts();
                    break;
                case "2":
                    AddAccount();
                    break;
                case "3":
                    DeleteAccount();
                    break;
                case "4":
                    UpdateAccount();
                    break;
                case "5":
                    AddCategory();
                    break;
                case "6":
                    AddIncome();
                    break;
                case "7":
                    AddExpense();
                    break;
                case "8":
                    ShowTransactionsByAccount();
                    break;
                case "9":
                    ShowTransactionsByCategory();
                    break;
                case "0":
                    return; 
                default:
                    Console.WriteLine("Не правильний ввід");
                    break;
            }

        }
    }

    private void ShowAccounts()
    {
        foreach (var acc in _service.GetAllAccounts())
            Console.WriteLine($"ID: {acc.ID}, Назва: {acc.Name}, Баланс: {acc.Balance} грн");
    }

    private void AddAccount()
    {
        Console.Write("Назва рахунку: ");
        var name = Console.ReadLine();
        Console.Write("Початковий баланс: ");
        decimal balance = decimal.Parse(Console.ReadLine()!);  //convert string in number Tyoe decimal
        _service.AddAccount(name!, balance);
    }

    private void DeleteAccount()
    {
        Console.Write("ID для видалення: ");
        int id = int.Parse(Console.ReadLine()!);
        _service.DeleteAccount(id);
    }

    private void UpdateAccount()
    {
        Console.Write("ID для оновлення: ");
        int id = int.Parse(Console.ReadLine()!);
        Console.Write("Нова назва: ");
        var name = Console.ReadLine();
        Console.Write("Новий баланс: ");
        decimal balance = decimal.Parse(Console.ReadLine()!);
        _service.UpdateAccount(id, name!, balance);
    }

    private void AddCategory()
    {
        Console.Write("Назва категорії: ");
        string name = Console.ReadLine()!;
        Console.Write("Тип (Income/Expense): ");
        CategoryType type = Enum.Parse<CategoryType>(Console.ReadLine()!, true);
        _service.AddCategory(name, type);
    }

    private void AddIncome()
    {
        Console.Write("Назва: ");
        string name = Console.ReadLine()!;
        Console.Write("Опис: ");
        string desc = Console.ReadLine()!;
        Console.Write("Сума: ");
        decimal amount = decimal.Parse(Console.ReadLine()!);
        Console.Write("Дата (yyyy-mm-dd): ");
        DateTime date = DateTime.Parse(Console.ReadLine()!);

        ShowAccounts();
        Console.Write("ID рахунку: ");
        int accId = int.Parse(Console.ReadLine()!);

        ShowCategories(CategoryType.Income);
        Console.Write("ID категорії: ");
        int catId = int.Parse(Console.ReadLine()!);

        _service.AddIncome(name, desc, amount, date, accId, catId);
    }

    private void AddExpense()
    {
        Console.Write("Назва: ");
        string name = Console.ReadLine()!;
        Console.Write("Опис: ");
        string desc = Console.ReadLine()!;
        Console.Write("Сума: ");
        decimal amount = decimal.Parse(Console.ReadLine()!);
        Console.Write("Дата (yyyy-mm-dd): ");
        DateTime date = DateTime.Parse(Console.ReadLine()!);

        ShowAccounts();
        Console.Write("ID рахунку: ");
        int accId = int.Parse(Console.ReadLine()!);

        ShowCategories(CategoryType.Expense);
        Console.Write("ID категорії: ");
        int catId = int.Parse(Console.ReadLine()!);

        _service.AddExpense(name, desc, amount, date, accId, catId);
    }

    private void ShowTransactionsByAccount()
    {
        ShowAccounts();
        Console.Write("ID рахунку: ");
        int id = int.Parse(Console.ReadLine()!);
        var items = _service.GetTransactionsByAccount(id);
        foreach (var i in items)
            Console.WriteLine($"{i.Date.ToShortDateString()} | {i.Name} | {i.Amount} | {i.Category.Name} ({i.Category.Type})");
    }

    private void ShowTransactionsByCategory()
    {
        foreach (var cat in _service.GetCategories())
            Console.WriteLine($"ID: {cat.ID} | {cat.Name} ({cat.Type})");
        Console.Write("ID категорії: ");
        int id = int.Parse(Console.ReadLine()!);
        var items = _service.GetTransactionsByCategory(id);
        foreach (var i in items)
            Console.WriteLine($"{i.Date.ToShortDateString()} | {i.Name} | {i.Amount} | Рахунок: {i.Account.Name}");
    }

    private void ShowCategories(CategoryType type)
    {
        foreach (var cat in _service.GetCategories().Where(c => c.Type == type))
            Console.WriteLine($"ID: {cat.ID} | {cat.Name}");
    }
}
