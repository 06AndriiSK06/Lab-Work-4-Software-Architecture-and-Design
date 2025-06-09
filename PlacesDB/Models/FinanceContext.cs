//using Microsoft.EntityFrameworkCore;
//using FinanceManager.DB.Models;

//namespace FinanceManager.DB
//{
//    public class FinanceContext : DbContext
//    {
//        public DbSet<Account> Accounts { get; set; }
//        public DbSet<Category> Categories { get; set; }
//        public DbSet<Expense> Expenses { get; set; }
//        public DbSet<Income> Incomes { get; set; }

//        public string DBPath { get; }

//        public FinanceContext()
//        {
//            DBPath = @"C:\Users\User-PC\source\repos\4LabaAppz(1)\PlacesDB\finance.d";
//        }

//        public FinanceContext(DbContextOptions<FinanceContext> options)
//            : base(options)
//        {
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//                optionsBuilder.UseSqlite($"Data Source={DBPath}");
//            }
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<BaseTransactionItem>()
//                .HasDiscriminator<string>("TransactionType")
//                .HasValue<Expense>("Expense")
//                .HasValue<Income>("Income");
//        }
//    }
//}

using Microsoft.EntityFrameworkCore;
using FinanceManager.DB.Models;

namespace FinanceManager.DB
{
    public class FinanceContext : DbContext
    {
        //таблиці БД
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Income> Incomes { get; set; }


        public string DBPath;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlite($"Data Source={DBPath}");

        }

        //налаштування пути
        public FinanceContext()
        {
            DBPath = "C:\\Users\\User-PC\\source\\repos\\4LabaAppz(1)\\PlacesDB\\finance.db";
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseTransactionItem>()  // TransactionType определяє чи Expense чи Income 
                .HasDiscriminator<string>("TransactionType")
                .HasValue<Expense>("Expense")
                .HasValue<Income>("Income");
        }
    }
}