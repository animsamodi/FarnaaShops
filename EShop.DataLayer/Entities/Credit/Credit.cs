using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Enum;

namespace EShop.DataLayer.Entities.Credit
{
    public class Credit : BaseEntity
    {
        public string TrakingCode { get; set; }
        public long UserId { get; set; }
        public EnumRealLegal RealLegal { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string NationalNumber { get; set; }
        public string ShomareShenasname { get; set; }
        public string Email { get; set; }
        public string BDate { get; set; }
        public string Father { get; set; }
        public string PlaceIssue { get; set; }
        public string CompanyName { get; set; }
        public string CodeEghtesadi { get; set; }
        public string ShomareSabt { get; set; }
        public string CodeNaghshTajer { get; set; }
        public string TarikhTasis { get; set; }
        #region Haghighi
        public string TarikhEngheza { get; set; }
        public EnumVaziyatMahal VaziyatMahal { get; set; }
        public EnumNoeTamalokMahal NoeTamalokMahal { get; set; }
        #endregion
        #region Hoghughi
        public string ShenaseMeli { get; set; }
        public EnumNoeSherkat NoeSherkat { get; set; }
        public EnumNoeMalekiyat NoeMalekiyat { get; set; }
        public EnumNoeTamalok NoeTamalok { get; set; }
        #endregion
        public string TozihatSherkat { get; set; }
        public string NeshaniMahaleKar { get; set; }
        public string CodePostiNeshaniMahaleKar { get; set; }
        public string NeshaniMahaleErsal { get; set; }
        public string CodePostiNeshaniMahaleErsal { get; set; }
        public string TelephoneSabet { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        #region Hoghughi
        public string SahamdarHagheEmza { get; set; }
        #endregion
        public EnumNoeKharid NoeKharid { get; set; }
        public double? ZemanatBankiMablagh { get; set; }
        public double? VasigheMelkiMablagh { get; set; }
        public EnumMudiMaliyati MudiMaliyati { get; set; }
        public string AdminMessage { get; set; }
        public string UserMessage { get; set; }
        public EnumCreditStatus CreditStatus { get; set; }
        public int AcceptPrice { get; set; }

        public DateTime? CreditExpDate { get; set; }
        //
        [ForeignKey("UserId")]
        public User.User User { get; set; }
        public List<CreditPartner> CreditPartners { get; set; }
        public List<CreditAccount> CreditAccounts { get; set; }
        public List<CreditDocument> CreditDocument { get; set; }
        public List<CreditBill> CreditBills { get; set; }
    }


}
