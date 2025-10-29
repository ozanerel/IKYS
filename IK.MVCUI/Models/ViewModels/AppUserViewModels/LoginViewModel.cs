using System.ComponentModel.DataAnnotations;

namespace IK.MVCUI.Models.ViewModels.AppUserViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı boş geçilemez")]
        
        public string UserName { get; set; }//Boş geçilemez

        [Required(ErrorMessage = "Şifre boş geçilemez")]
        public string Password { get; set; }//Boş geçilemez
    }
}
