using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_APP_Core.Entities.ModelsInput
{
    public class DtoDoc
    {
        public Guid DocId { get; set; }

        public Guid? UserId { get; set; }
        public string? DocTitle { get; set; }
        public DateTime? DocCreateDate { get; set; }
        public string? DocCont { get; set; }
        public DateTime? DocModifiDate { get; set; }
        public string linkImg { get; set; }
        public int? DocStatus { get; set; }
    }
}
