using FinanceManager.DB.Models;


namespace FinanceManager.BLL.DTOs
{
    public class CategoryDto
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public CategoryType Type { get; set; }
    }
}
