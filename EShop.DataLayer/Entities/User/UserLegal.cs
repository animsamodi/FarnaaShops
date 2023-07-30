using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.User;
using EShop.DataLayer.Enum;

public class UserLegal:BaseEntity
{
    public long UserId { get; set; }
    public string CompanyName { get; set; }
    public string CodeEghtesadi { get; set; }
    public string ShomareSabt { get; set; }
    public string CodeNaghshTajer { get; set; }
    public string TarikhTasis { get; set; }
    public string ShenaseMeli { get; set; }
    public EnumNoeSherkat NoeSherkat { get; set; }
    public EnumNoeMalekiyat NoeMalekiyat { get; set; }
    public EnumNoeTamalok NoeTamalok { get; set; }
    public string TozihatSherkat { get; set; }
    public string NeshaniMahaleKar { get; set; }
    public string CodePostiNeshaniMahaleKar { get; set; }
    public string TelephoneSabet { get; set; }
    //
    public string FileRuznameRasmi { get; set; }
    public string FileAkharinTaghirat { get; set; }
    public string FileSahebanEmza { get; set; }
    public string FileAgahiTasis { get; set; }
    //


    [ForeignKey("UserId")]
    public User User { get; set; }
}