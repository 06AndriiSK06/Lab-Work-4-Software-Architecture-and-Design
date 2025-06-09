namespace FinanceManager.BLL.DTOs
{
    public class AccountDto
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }
}
