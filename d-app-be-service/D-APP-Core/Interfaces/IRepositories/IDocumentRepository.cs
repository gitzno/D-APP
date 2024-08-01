using D_APP_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_APP_Core.Interfaces.IRepositories
{
    public interface IDocumentRepository
    {
        public int CreateDocument(Documents docuement);
        public int UpdateDocument(Documents docuement);
        public int DeleteDocument(Guid uuid);
        public Documents GetDocument(Guid id);
        public IList<Documents> GetAllDocuments(Guid id);
        public IList<Documents> GetDocumentsFrom(DateTime startdate, DateTime endDate);
    }
}
