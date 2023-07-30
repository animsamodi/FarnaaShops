using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Address;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.User;
using EShop.DataLayer.Enum;

public class CooperationRequest:BaseEntity
{
    public EnumRealLegal Type { get; set; }
//
    public string Name { get; set; }//Name Sherkat
    public string Family { get; set; }
    public string CodeMeli { get; set; }//Code eghtesadi

    public string CodeNaghsh{ get; set; }
    public string Email { get; set; }
    public long ProvinceId { get; set; }
    public long CityId { get; set; }
    public long? UserId { get; set; }
    public string Address { get; set; }
    public string CodePosti { get; set; }
    public string Tozihat{ get; set; }
    
    public string ShomareTamas { get; set; }
    //
    public string FileKartMeli{ get; set; }
    public string FileParvaneKasb{ get; set; }
    public string FileSanad{ get; set; }
    //
    public bool IsConfirmMobile { get; set; }
    //
    [Display(Name = "کد فعال سازی تلفن")]
    public int PhoneActiveCode { get; set; }
    //
    [Display(Name = "نوع شرکت")]

    public EnumNoeSherkat? NoeSherkat { get; set; }

    [Display(Name = "نوع مالکیت")]

    public EnumNoeMalekiyat? NoeMalekiyat { get; set; }

    [Display(Name = "نوع تملک محل فعالیت")]

    public EnumNoeTamalok? NoeTamalok { get; set; }
    //
    [Display(Name = "نوع فروش")]

    public EnumNoeForush? NoeForush { get; set; }
    [Display(Name = "شناسه صنفی")]

    public string ShenaseSenfi { get; set; }
    //
    [Display(Name = "تلفن ثابت به همراه کد")]
    [RegularExpression(@"^[\u06F0-\u06F90-9]+$", ErrorMessage = "لطفا {0} را عدد وارد کنید")]
    [MaxLength(11, ErrorMessage = "مقدار {0} نباید بیشتر از{1} باشد")]


    public string TelephoneSabet { get; set; }
    //
    public DateTime? PhoneActiveCodeExpDate { get; set; }
    public EnumCooperationRequestStatus Status{ get; set; }
    public string PrDateCheck { get; set; }
    public string Description { get; set; }
    //
    [ForeignKey("CityId")]
    public City City { get; set; }
    [ForeignKey("ProvinceId")]
    public Province Province { get; set; }
    
    [ForeignKey("UserId")]
    public User User { get; set; }
    [Display(Name = "نوع سیستم")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public EnumTypeSystem TypeSystem { get; set; }

}