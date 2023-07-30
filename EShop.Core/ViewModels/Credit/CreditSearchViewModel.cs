using EShop.DataLayer.Enum;
using System.ComponentModel.DataAnnotations;

namespace EShop.Core.ViewModels.Credit
{
    public class CreditSearchViewModel
    {
 

 
        public int Pagenumber { get; set; }

     
    } 
    public class ColleagueSearchViewModel
    {

        public string Name { get; set; }

        public string Code { get; set; }
        public EnumCooperationRequestStatus Status { get; set; }


        public int Pagenumber { get; set; }

     
    } 
    public class UserColleagueSerchViewModel
    {

        public string Name { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }

        public int Pagenumber { get; set; }

     
    }
    public class UserCustomerSerchViewModel
    {

        public string Name { get; set; }
        public string CodeMeli { get; set; }
        public string Phone { get; set; }

        public int Pagenumber { get; set; }


    }
    public class UserAddressSerchViewModel
    {

        public string Name { get; set; }
         public string Phone { get; set; }
         public string CodeMeli { get; set; }

        public int Pagenumber { get; set; }


    }
    public class IncreaseCreditUserColleaugeViewModel
    {

        public long Id { get; set; }
        [Display(Name = "نام")]

        public string Name { get; set; }
        [Display(Name = "نام کاربری")]

        public string Username { get; set; }
        [Display(Name = "موبایل")]

        public string Phone { get; set; }
        [Display(Name = "مبلغ اعتبار")]

        public long  Price { get; set; }


    } 
    
    
    public class CreditBillSearchViewModel
    {
 

 
        public int Pagenumber { get; set; }

     
    }
}