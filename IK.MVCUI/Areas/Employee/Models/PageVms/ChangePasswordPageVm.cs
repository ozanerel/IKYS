using System.ComponentModel.DataAnnotations;

namespace IK.MVCUI.Areas.Employee.Models.PageVms
{
    public class ChangePasswordPageVm
    {
        [Required (ErrorMessage= "Eski Şifre Boş Geçilemez!")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Yeni Girmek İstediğiniz Şifre Boş Geçilemez!")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Şifre (Tekrar) Boş Geçilemez!")]
        [Compare("NewPassword", ErrorMessage = "Girmiş Olduğunuz Şifreler Uyuşmuyor!")]
        public string ConfirmNewPassword { get; set; }
    }
}
