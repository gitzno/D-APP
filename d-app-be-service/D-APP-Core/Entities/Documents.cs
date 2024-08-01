using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace D_APP_Core.Entities
{
    public partial class Documents
    {
        [Key]
        public Guid DocId { get; set; }

        [ForeignKey("User")]
        public Guid? UserId { get; set; }
        [MaxLength(250)]
        public string? DocTitle { get; set; }
        public DateTime? DocCreateDate { get; set; }
        public string? DocCont { get; set; }
        public DateTime? DocModifiDate { get; set; }
        /// <summary>
        /// doc status:
        /// 0: private
        /// 1: publish for everyone
        /// </summary>
        public int? DocStatus { get; set; }
        public string linkImg { get; set; }

        public virtual User? User { get; set; }
    }
}
