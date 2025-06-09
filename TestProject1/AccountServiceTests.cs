using AutoMapper;
using FinanceManager.BLL.DTOs;
using FinanceManager.BLL;
using FinanceManager.BLL.UoW;
using FinanceManager.BLL.Repositories;
using FinanceManager.DB.Models;
using Moq;
using System.Collections.Generic;
using Xunit;
using FinanceManager.BLL.Mapping;

namespace FinanceManager.Tests
{
    public class AccountServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IRepository<Account>> _mockAccountRepo;
        private readonly IMapper _mapper;
        private readonly FinanceService _accountService;

        public AccountServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>(); //≥м≥тац≥€
            _mockAccountRepo = new Mock<IRepository<Account>>();

            _mockUnitOfWork.Setup(u => u.Accounts).Returns(_mockAccountRepo.Object);

            // конф≥г. AutoMapper 
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(MappingProfile).Assembly);
            });
            _mapper = config.CreateMapper();
            _accountService = new FinanceService(_mockUnitOfWork.Object, _mapper); //екземпл€р серв≥са, що тестуЇм
        }

        [Fact]
        public void GetAllAccounts_Test()
        {
            var accounts = new List<Account> {
                new Account { ID = 1, Name = "TestAcc", Balance = 200 }
            };
            _mockAccountRepo.Setup(r => r.GetAll()).Returns(accounts);
            var result = _accountService.GetAllAccounts();

       
            Assert.Single(result); //перев≥р€ю ,що в result т≥льки 1 об'Їкт
            Assert.Equal("TestAcc", result.First().Name);
        }

        [Fact]
        public void AddAccount_Test()
        {
            _accountService.AddAccount("NewAcc", 500);

            _mockAccountRepo.Verify(r => r.Add(It.Is<Account>(a => a.Name == "NewAcc" && a.Balance == 500)), Times.Once);
            _mockUnitOfWork.Verify(u => u.Complete(), Times.Once);
        }

        [Fact]
        public void DeleteAccount_Test()
        {
            _accountService.DeleteAccount(1); //id 1 

            _mockAccountRepo.Verify(r => r.Delete(1), Times.Once);  
            _mockUnitOfWork.Verify(u => u.Complete(), Times.Once);
        }

        [Fact]
        public void UpdateAccount_Test()
        {

            var account = new Account { ID = 3, Name = "Old", Balance = 100 };
            _mockAccountRepo.Setup(r => r.GetById(3)).Returns(account);


            _accountService.UpdateAccount(3, "Updated", 999);

            Assert.Equal("Updated", account.Name);
            Assert.Equal(999, account.Balance);
            _mockAccountRepo.Verify(r => r.Update(account), Times.Once);
            _mockUnitOfWork.Verify(u => u.Complete(), Times.Once);
        }
    }
}
