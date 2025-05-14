using FinanceManager.DB;
using FinanceManager.ConsoleApp;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var context = new FinanceContext();
context.Database.EnsureCreated(); // створюю бд ( якщо немає ) 

var service = new FinanceService(context);
var menu = new ConsoleMenu(service);
menu.Show();
