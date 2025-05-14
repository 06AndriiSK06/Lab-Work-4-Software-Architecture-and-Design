

namespace FinanceManager.DB.Models
{
    public class Category
    {
        public int ID { get; set; }
        public required string Name { get; set; } 
        public required CategoryType Type { get; set; }

        public List<BaseTransactionItem> Transactions { get; set; } = new();
    }

    public enum CategoryType
    {
        Income,
        Expense
    }
}
