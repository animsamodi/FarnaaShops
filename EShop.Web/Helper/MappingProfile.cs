using AutoMapper;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Credit;
using EShop.DataLayer.Entities.Credit;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Credit
        CreateMap<Credit, CreditHaghighiViewModel>();
        CreateMap<CreditHaghighiViewModel, Credit>();
        CreateMap<Credit, CreditHoghughiViewModel>();
        CreateMap<CreditHoghughiViewModel, Credit>();

        CreateMap<CreditAccountViewModel, CreditAccount>();
        CreateMap<CreditAccount, CreditAccountViewModel>();

        CreateMap<CreditPartnerViewModel, CreditPartner>();
        CreateMap<CreditPartner, CreditPartnerViewModel>();

        CreateMap<CreditBillViewModel, CreditBill>();
        CreateMap<CreditBill, CreditBillViewModel>();

        CreateMap<CreditDocumentViewModel, CreditDocument>();
        CreateMap<CreditDocument, CreditDocumentViewModel>();



        #endregion

        #region User
        CreateMap<UserEditProfile, UserLegal>();
        CreateMap<UserLegal, UserEditProfile>();

        CreateMap<CooperationRequest, CooperationRequestRealViewModel>();
        CreateMap<CooperationRequestRealViewModel, CooperationRequest>();


        CreateMap<CooperationRequest, CooperationRequestLegalViewModel>();
        CreateMap<CooperationRequestLegalViewModel, CooperationRequest>();

        #endregion
    }
}