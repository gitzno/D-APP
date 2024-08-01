using System.ComponentModel.DataAnnotations;

namespace D_APP_Core.Entities.ModelsInput
{
    public class LoginInput
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
