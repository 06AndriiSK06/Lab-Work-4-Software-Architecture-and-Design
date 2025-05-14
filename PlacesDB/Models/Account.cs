
namespace FinanceManager.DB.Models
{
    public class Account
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public decimal Balance { get; set; }

        public List<BaseTransactionItem> Transactions { get; set; } = new();
    }
}
