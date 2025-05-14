namespace FinanceManager.DB.Models
{
    public abstract class BaseTransactionItem
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Amount { get; set; }
        public required DateTime Date { get; set; }

        public required Category Category { get; set; }
        public required Account Account { get; set; }
    }
}
