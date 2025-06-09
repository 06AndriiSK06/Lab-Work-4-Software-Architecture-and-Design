using FinanceManager.BLL;
using FinanceManager.DB.Models;
using FinanceManager.BLL.UoW;
using FinanceManager.BLL.Repositories;

using PlacesDB; 

namespace FinanceManager.ConsoleApp
{
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
                Console.WriteLine("1. Переглянути рахунки");
                Console.WriteLine("2. Додати рахунок");
                Console.WriteLine("3. Видалити рахунок");
                Console.WriteLine("4. Оновити рахунок");
                Console.WriteLine("5. Додати категорію");
                Console.WriteLine("6. Додати дохід");
                Console.WriteLine("7. Додати витрату");
                Console.WriteLine("0. Вийти");
                Console.Write("Ваш вибір: ");
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
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неправильний ввід");
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
            decimal balance = decimal.Parse(Console.ReadLine()!);
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

            var categories = _service.GetCategories().Where(c => c.Type == CategoryType.Income).ToList();
            foreach (var cat in categories)
                Console.WriteLine($"ID: {cat.ID} | {cat.Name}");
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

            var categories = _service.GetCategories().Where(c => c.Type == CategoryType.Expense).ToList();
            foreach (var cat in categories)
                Console.WriteLine($"ID: {cat.ID} | {cat.Name}");
            Console.Write("ID категорії: ");
            int catId = int.Parse(Console.ReadLine()!);

            _service.AddExpense(name, desc, amount, date, accId, catId);
        }
    }
}
