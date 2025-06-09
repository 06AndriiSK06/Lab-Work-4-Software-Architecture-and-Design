using AutoMapper;
using FinanceManager.BLL.DTOs;
using FinanceManager.DB.Models;



namespace FinanceManager.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<BaseTransactionItem, TransactionDto>()
                .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.Account.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.CategoryType, opt => opt.MapFrom(src => src.Category.Type.ToString()));
        }
    }
}
