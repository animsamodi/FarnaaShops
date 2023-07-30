using System;
using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels.Credit;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Interfaces
{
    public interface ICooperationRequestService : IBaseService
    {
         Tuple<int, List<CooperationRequest>> GetListForAdmin(ColleagueSearchViewModel search, int count);

        bool Add(CooperationRequest model);
        bool Update(CooperationRequest model);
        bool Delete(CooperationRequest model);
        CooperationRequest FindById(long id);
        bool ChangeStatus(long id, string description, EnumCooperationRequestStatus status);
        bool ConvertRequestToUser(long id,string pass);
        CooperationRequest CheckExistPhoneInRequest(string phone, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
        bool EditRequest(CooperationRequest model);
        long AddRequest(CooperationRequest request);
    }
}