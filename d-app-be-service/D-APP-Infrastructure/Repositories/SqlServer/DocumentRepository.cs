using D_APP_Core.Entities;
using D_APP_Core.Interfaces.IRepositories;
using D_APP_Infrastructure.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_APP_Infrastructure.Repositories.SqlServer
{
    public class DocumentRepository : IDocumentRepository
    {
        private DAppContext _context;

        public DocumentRepository(DAppContext context)
        {
            _context = context;
        }

        public int CreateDocument(Documents docuement)
        {
            _context.Documents.Add(docuement);
            return _context.SaveChanges();
        }

        public int DeleteDocument(Guid uuid)
        {
            if (GetDocument(uuid) != null)
            {
                _context.Documents.Remove(GetDocument(uuid));
            return _context.SaveChanges();
            }
            return 0;
        }

        public IList<Documents> GetAllDocuments(Guid id)
        {
            var list = _context.Documents.Where(x => x.UserId == id).ToList();

            return list;
        }

        public Documents GetDocument(Guid id)
        {
            var document = _context.Documents.FirstOrDefault(x => x.DocId == id);
            return document;
        }

        public IList<Documents> GetDocumentsFrom(DateTime startdate, DateTime endDate)
        {
            var documents = _context.Documents
            .Where(d => d.DocCreateDate >= startdate && d.DocCreateDate <= endDate)
            .ToList();

            return documents;
        }

        public int UpdateDocument(Documents docuement)
        {
            var entity = _context.Documents.FirstOrDefault(item => item.DocId == docuement.DocId);

            // Validate entity is not null
            if (entity != null)
            {
                entity.DocTitle = docuement.DocTitle;
                entity.DocCont = docuement.DocCont;
                entity.DocModifiDate = DateTime.Now;
                entity.DocStatus = docuement.DocStatus;
                entity.linkImg = docuement.linkImg;
                return _context.SaveChanges();
            }
            return 0;
        }
    }
}
