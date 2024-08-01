using System.ComponentModel.DataAnnotations;

namespace D_APP_Core.Entities.ModelsInput
{
    public class RegisterInput
    {
        [Required]
        public string UserFname { get; set; }

        [Required]
        public string UserLname { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public DateOnly UserDateOfBirth { get; set; }
        public string? UserPhoneNumber { get; set; }

        [Required]
        public string UserPassword { get; set; }
    }
}
