using System;
using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Credit;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Credit;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Interfaces
{
    public interface ICreditService : IBaseService<CreditBill>
    {
        Tuple<int, List<CreditUserListViewModel>> GetListForAdmin(CreditSearchViewModel search, int count);
        List<CreditUserListViewModel> GetListForUser(long userId);
        bool Add(Credit model);
        bool Update(Credit model);
        bool Delete(Credit model);
        Credit FindById(long id);
        List<CreditDocumentType> GetTypeFiles(EnumRealLegal type);
        bool CheckOpenRequest( long userId);
        bool AddWhitRelated(Credit credit);
        bool ChangeAdminStatus( long id, string adminMessage, EnumCreditStatus creditStatus, string creditExpDate, int acceptPrice);
        //
        bool AddCreditBill(CreditBill creditBill);
        bool EditCreditBill(CreditBill creditBill);
        bool DeleteCreditBill(CreditBill creditBill);
        bool DeleteCreditBillById(long id);
        CreditBill FindCreditBillById(long id);
        CreditBillViewModel FindCreditBillForAdminById(long id);
        Tuple<int, List<CreditBillViewModel>> GetAllCreditBill(CreditBillSearchViewModel search, int count);
        List<CreditBill> GetAllUserCreditBillByCreditId(long creditId);
        List<CreditBill> GetAllUserCreditBillByUserId(long userId);
        bool ChangeBillAdminStatus(long id, string adminMessage, EnumCreditBillStatus status,  int confirmPrice);


    }
}