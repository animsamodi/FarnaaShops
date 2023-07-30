using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace EShop.DataLayer.Enum
{
    public enum EnumTypeSystem
    {
        [Display(Name = "فرنا")]
        Farnaa = 1,
        [Display(Name = "فرنا پلاس")]
        FarnaaPlus = 2,
 
    }
    public enum EnumBannerType
    {
        [Display(Name = "بنر بالای صفحه اصلی یک")]
        HomeTop1 = 1,
        [Display(Name = "بنر بالای صفحه اصلی دو")]
        HomeTop2 = 2,
        [Display(Name = "بنر پایین صفحه اصلی")]

        HomeDown = 3,
        [Display(Name = "بنر سایر صفحات 1")]

        Other1 = 4,
        [Display(Name = "بنر سایر صفحات 2")]

        Other2 = 5,
        [Display(Name = "بنر سایر صفحات 3")]

        Other3 = 6,
        [Display(Name = "بنر سایر صفحات 4")]

        Other4 = 7,
        [Display(Name = "بنر سرچ هدر")]

        HeaderSearchBanner = 8
    }

    public enum EnumOrderLimitType
    {
        [Display(Name = "فاکتور")]
        Factor = 1,
        [Display(Name = "روز")]

        Day = 2,
        [Display(Name = "ساعت")]

        Hour = 3
    }
    public enum EnumDefaultIpg
    {
        [Display(Name = "ملت")]
        Melat = 1,
        [Display(Name = "آسان پرداخت")]

        Ap = 2,
        [Display(Name = "ایران کیش")]

        Kish = 3,
        [Display(Name = "زرین پال")]

        ZarinPal = 4,
        [Display(Name = "ملی")]

        Meli =5
    }
    public enum EnumDefaultIpgPlus
    {
        //[Display(Name = "ملت")]
        //Melat = 1,
        //[Display(Name = "آسان پرداخت")]

        //Ap = 2,
        //[Display(Name = "ایران کیش")]

        //Kish = 3,
        //[Display(Name = "زرین پال")]

        //ZarinPal = 4,
        [Display(Name = "ملی")]

        Meli =5
    }
    public enum EnumTypeCategotyMain
    {
        [Display(Name = "براساس برند")]
        ByBrand = 1,
        [Display(Name = "بر اساس دسته بندی")]

        BySubCaegory = 2
    }
    public enum EnumTypeLayotIndex
    {
        [Display(Name = "محصول")]
        Product = 1,
        [Display(Name = "تک تصویر")]

        Image1 = 2,
        [Display(Name = "دو تصویر")]

        Image2 = 3,
        [Display(Name = "چهار تصویر")]

        Image4 = 4
    }
    public enum EnumSortLayoutIndex
    {
        [Display(Name = "پرفروش ترین")]
        Sell = 1,
        [Display(Name = "جدیدترین")]

        New = 2,
        [Display(Name = "پربازدید ترین")]

        View = 3
    }
    public enum EnumTypeSlider
    {
        [Display(Name = "مشتری")]
        User = 1,
        [Display(Name = "همکار")]

        Colleauge = 2,
        [Display(Name = "مشتری و همکار")]

        Both = 3
    }
    public enum EnumTypeShipment
    {
        [Display(Name = "مشتری")]
        User = 1,
        [Display(Name = "همکار")]

        Colleauge = 2,
        [Display(Name = "مشتری و همکار")]

        Both = 3
    }
    public enum EnumTypePacking
    {
        [Display(Name = "مشتری")]
        User = 1,
        [Display(Name = "همکار")]

        Colleauge = 2,
        [Display(Name = "مشتری و همکار")]

        Both = 3
    }
    public enum EnumTypeBlog
    {
        [Display(Name = "بلاگ")]
        normal = 1,
        [Display(Name = "ویژه صفحه اول 1")]
        spec1 = 2,
        [Display(Name = "ویژه صفحه اول 2")]
        spec2 = 3,
        [Display(Name = "ویژه صفحه اول 3")]
        spec3 = 4,
    }
    public enum EnumTypeUser
    {
        [Display(Name = "مدیریت")]
        Admin = 1,
        [Display(Name = "کاربر")]

        User = 2,
        [Display(Name = "Api")]

        Api = 3
    }
    public enum EnumRealLegal
    {
 
        [Display(Name = "حقیقی")]
        [Description("حقیقی")]
        Real = 1,
        [Display(Name = "حقوقی")]
        [Description("حقوقی")]

        Legal = 2
    }
    public enum EnumCooperationRequestStatus
    {
 
        [Display(Name = "در انتظار بررسی")]
        [Description("در انتظار بررسی")]
        Wating = 1,
        [Display(Name = "تایید شده")]
        [Description("تایید شده")]
        Confairm = 2,
        [Display(Name = "رد شده")]
        [Description("رد شده")]
        Reject = 3,
        [Display(Name = "در انتظار تایید شماره")]
        [Description("در انتظار تایید شماره")]
        WatingConfirmPhone = 4
    }
    public enum EnumTypeFile
    {
        [Display(Name = "تصویر")]
        Image = 1
    }
    public enum EnumTypeStaticPage
    {
        [Display(Name = "خدمات مشتریان")]
        Khadamat = 1,
        [Display(Name = "راهنمای خرید")]

        Rahnama = 2,
        [Display(Name = "با فرنا")]

        BaFarna = 3
    }
    public enum EnumTypeFotterMenu
    {
        [Display(Name = "خدمات مشتریان")]
        Khadamat = 1,
        [Display(Name = "راهنمای خرید")]

        Rahnama = 2,
        [Display(Name = "با فرنا")]

        BaFarna = 3
    }
    public enum EnumTypeFotterLink
    {
        [Display(Name = "اینماد")]
        Enamad = 1,
        [Display(Name = "ساماندهی")]

        SamanDehi = 2,
        [Display(Name = "کسب و کار مجازی")]

        KasboKar = 3,
        [Display(Name = "شبکه های اجتماعی")]

        Social = 20,
        [Display(Name = "مجوز ها")]

        Other = 100,

    }
    public enum EnumTypeFaq
    {
        [Display(Name = "ارسال محصول")]
        Ersal = 1,
        [Display(Name = "سفارش دادن و بازگرداندن")]

        Sefaresh = 2,
        [Display(Name = "پرداخت")]

        Pardakht = 3

    }
    public enum EnumProductOtherPage
    {
        [Display(Name = "همه صفحات")]
        All = 1

    }
    public enum EnumTypeMenu
    {
        [Display(Name = "دسته بندی اصلی")]
        Main = 1,
        [Display(Name = "چندتایی")]

        Multi = 2,
        [Display(Name = "تکی")]

        Single = 3,
        [Display(Name = "زیر منو")]

        SubMenu = 4,

    }
    public enum EnumPermission
    {

        [Display(Name = "اوپراتور مدیریت")]

        Operator = 27
    }
    public enum EnumVaziyatPardakht
    {
        [EnumMember()]
        [Display(Name = "پرداخت شده")]
        PardakhtShode = 1
        , [EnumMember()]
        [Display(Name = "پرداخت نشده")]

        PardakhtNashode = 2
    }
    public enum EnumPaymentStatus
    {
        [Display(Name = "در انتظار پرداخت")]
        Wait = 1,
        [Display(Name = "پرداخت شده")]
        Pay = 2,
        [Display(Name = "پرداخت نشده")]
        NotPay = 3,
        [Display(Name = "خطا")]
        Error = 4
    }
    public enum EnumPaymentType
    {
        [Display(Name = "تخفیف")]
        [Description("تخفیف")]
        Discount = 1,
        [Display(Name = "اعتباری")]
        [Description("اعتباری")]
        Credit = 2,
        [Display(Name = "آنلاین")]
        [Description("آنلاین")]
        Online = 3,
        [Display(Name = "فروش همکار/سیستمی/نقدی")]
        [Description("فروش همکار/سیستمی/نقدی")]
        CashColleague = 4,
        [Display(Name = "فروش همکار/سیستمی/اعتباری")]
        [Description("فروش همکار/سیستمی/اعتباری")]
        CreditColleague =5,
    }
    public enum EnumPaymentTypeColleaugeCart
    {
         
        [Display(Name = "فروش همکار/سیستمی/نقدی")]
        [Description("فروش همکار/سیستمی/نقدی")]
        CashColleague = 4,
        [Display(Name = "فروش همکار/سیستمی/اعتباری")]
        [Description("فروش همکار/سیستمی/اعتباری")]
        CreditColleague =5,
    }
    public enum EnumOrderStatus
    {

        [Display(Name = "در انتظار پرداخت")]
        WaitPay = 1,
        [Display(Name = "پرداخت شده")]
        Pay = 2,
        [Display(Name = "پرداخت نشده")]
        NotPay = 3,
        [Display(Name = "در حال پردازش")]
        InProccess = 4,
        [Display(Name = "ارسال به انبار")]
        SendToAnbar = 5,
        [Display(Name = "خروج از انبار")]
        OutAnbar = 6,
        [Display(Name = "تحویل شده به شرکت حمل و نقل")]
        SendPost = 7
    }
    public enum EnumYesNo
    {

        [Display(Name = "بله")]
        Yes = 1,
        [Display(Name = "خیر")]
        No = 2
    }
    public enum EnumStatusComment
    {

        [Display(Name = "در انتظار تایید")]
        Wait = 1,
        [Display(Name = "تایید شده")]
        Confirm = 2,
        [Display(Name = "رد شده")]
        Reject = 3
    }
    public enum EnumCreditStatus
    {
        [Display(Name = "در انتظار تایید")]
        [Description( "در انتظار تایید")]
        Wating = 1,
        [Display(Name = "تایید شده")]
        [Description("تایید شده")]
        Confirm = 2,
        [Display(Name = "رد شده")]
        [Description("رد شده")]
        Reject = 3

    }

    public enum EnumNoeSherkat
    {
        [Display(Name = "سهامی خاص")]
        Khas = 1,
        [Display(Name = "سهامی عام")]
        Am = 2,
        [Display(Name = "مسئولیت محدود")]
        MAhdud = 3,


    }
    public enum EnumNoeMalekiyat
    {

        [Display(Name = "خصوصی")]
        Khosusi = 1,
        [Display(Name = "دولتی")]
        Dilati = 2,
        [Display(Name = "تعاونی")]
        Taavoni = 3,



    }
    public enum EnumNoeTamalok
    {
        [Display(Name = "مالک")]
        Malek = 1,
        [Display(Name = "استیجاری")]
        Estijari = 2


    }
    public enum EnumNoeForush
    {
        [Display(Name = "خرده فروش")]
        Khodeh = 1,
        [Display(Name = "عمده فروش")]
        Omdeh = 2


    }

    public enum EnumNoeKharid
    {
        [Display(Name = "نقدی")]
        Naghdi = 1,
        [Display(Name = "اعتباری")]
        Estebari = 2,
        [Display(Name = "نقدی و اعتباری")]
        Both = 3


    }
    public enum EnumMudiMaliyati
    {

        [Display(Name = "مودی مشمول ثبت نام در نظام مالیاتی کشور")]
        Mashmul = 1,
        [Display(Name = "مشمولان حقیقی ماده ۸۱ ق.م.م")]
        MashmulHaghighi = 2,
        [Display(Name = "اشخاصی که مشمول ثبت نام در نظام مالیاتی نیستند")]
        NotMashmul = 3,
        [Display(Name = "مصرف کننده نهایی")]
        Nahayi = 4,


    }
    public enum EnumNoeTamalokMahal
    {
        [Display(Name = "سرقفلی")]
        Sarghofli = 1,
        [Display(Name = "شخصی")]
        Shakhsi = 2,
        [Display(Name = "استیجاری")]
        Estijari = 3
    }

    public enum EnumVaziyatMahal
    {
        [Display(Name = "تجاری")]
        Tejari = 1,
        [Display(Name = "اداری")]
        Edari = 2,
        [Display(Name = "مسکونی")]
        Maskuni = 3
    }
    public enum EnumCreditBillStatus
    {
        [Display(Name = "در انتظار تایید")]
        Wait = 1,
        [Display(Name = "تایید شده")]
        Confirm = 2,
        [Display(Name = "رد شده")]
        Reject = 3
    }

    //Ofogh
    public enum EnumStuffGroupType
    {
        [Display(Name = "تلفن همراه")]

        Mobile = 4,
        [Display(Name = "لوازم خانگی")]
        LavazemKhanegi = 49,
        [Display(Name = "اقلام دیجیتال")]
        AghlamDigital = 101
    }

    public enum EnumStatusAppointment
    {
        [Display(Name = "ثبت نهایی")]

        SabtNahayi = 1,
        [Display(Name = "ثبت اولیه")]
        SabtAvaliye = 2
    }
    public enum EnumActivityType
    {
        [Display(Name = "وارد کننده")]
        VaredKonandeh = 1,
        [Display(Name = "تولید کننده")]
        TolidKonandeh = 2,
        [Display(Name = "عمده فروش")]
        OmdeForush = 3,
        [Display(Name = "خرده فروش")]
        KhordeForush = 6,
        [Display(Name = "نماینده")]
        Namayandeh = 7,
        [Display(Name = "فروشگاه زنجیره ای")]
        ForushgahZangireiy = 8,
        [Display(Name = "نامشخص")]
        Namoshakhas = 10,
        [Display(Name = "میادین میوه و تره بار")]
        MayadinMiveh = 12,
        [Display(Name = "شرکت های پخش کالا")]
        SherkatPakhsh = 13,
        [Display(Name = "تعاونی مصرف")]
        TaavoniMasraf = 15,
        [Display(Name = "صاحب برند")]
        Sahebberand = 16
    }

    public enum EnumBuyerType
    {
        [Display(Name = "حقیقی")]

        Haghighi = 0,
        [Display(Name = "حقوقی")]
        Hoghughi = 1
    }
    public enum EnumTypeOfogh
    {
        [Display(Name = "خرده فروش")]

        KhordeForush = 1,
        [Display(Name = "همکار")]
        Hamkar = 2
    }
    public enum EnumProductPricePageOrder
    {
        [Display(Name = "الفبا")]

        Alphabet = 1,
        [Display(Name = "قیمت")]
        Price = 2
    }
    public enum EnumStatusOfogh
    {
        [Display(Name = "در انتظار ارسال")]

        Wating = 1,
        [Display(Name = "ارسال شده")]
        Send = 2,
        [Display(Name = "خطا")]
        Error = 3
    }
    /////////////////////////////////////////Ofogh
    public enum EnumSupplierFactorStatus
    {
        [Display(Name = "ثبت سفارش")]

        Register = 1,
        [Display(Name = "تایید تامین")]
        ConfirmTamin = 2,
        [Display(Name = "تایید انبار")]
        TaeedAnbar = 3,

    }
}
