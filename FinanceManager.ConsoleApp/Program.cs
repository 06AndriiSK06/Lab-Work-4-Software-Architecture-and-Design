using FinanceManager.ConsoleApp;
using FinanceManager.BLL;
using FinanceManager.BLL.UoW;
using FinanceManager.BLL.Repositories;
using FinanceManager.DB.Models;
using PlacesDB;
using AutoMapper;
using FinanceManager.BLL.Mapping;
using FinanceManager.DB;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var context = new FinanceContext();
var unitOfWork = new UnitOfWork(context);

//  AutoMapper
var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
var mapper = config.CreateMapper();

var service = new FinanceService(unitOfWork, mapper);
var menu = new ConsoleMenu(service);


menu.Show(); // запускаю інтерфейс
