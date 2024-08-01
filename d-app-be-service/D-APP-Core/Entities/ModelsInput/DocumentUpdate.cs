using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_APP_Core.Entities.ModelsInput
{
    public class DocumentUpdate
    {
        public string DocTitle { get; set; }
        public string DocCont { get; set; }
        public int DocStatus { get; set; }
        public string linkImg { get; set; }
    }
}
