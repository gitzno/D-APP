using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace D_APP_Core.Entities
{
    public partial class User
    {
        [Key]
        public Guid UserId { get; set; }

        [MaxLength(100)]
        public string? UserFname { get; set; }

        [MaxLength(100)]
        public string? UserLname { get; set; }

        [EmailAddress]
        [MaxLength(200)]
        public string? UserEmail { get; set; }

        [MaxLength(100)]
        public string? UserName { get; set; }

        public DateTime? UserDateOfBirth { get; set; }

        public DateTime? UserCreatedDate { get; set; }

        public DateTime? UserModifiDate { get; set; }

        public byte? UserRole { get; set; }

        public byte? UserStatus { get; set; }

        [Phone]
        [MaxLength(15)]
        public string? UserPhoneNumber { get; set; }

        [MaxLength(200)]
        public string? UserPassword { get; set; }

        public virtual ICollection<Documents> Documents { get; set; } = new List<Documents>();
    }
}
