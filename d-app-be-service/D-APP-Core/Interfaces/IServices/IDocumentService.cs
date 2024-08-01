using D_APP_Core.Entities;
using D_APP_Core.Entities.ModelsInput;
using D_APP_Core.Entities.Program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_APP_Core.Interfaces.IServices
{
    public interface IDocumentService
    {
        public ServiceResult updateDocument(Guid docid, DocumentUpdate doc);
        public ServiceResult getDocumentById(string username, Guid docId);
        public ServiceResult GetAllDocumentsByUser(string userName);
        public ServiceResult GetAllDocumentsByUserF(string userName);
        public ServiceResult deleteDocument(Guid docid);
        public ServiceResult getDocByTime(string username, DateDis date);
        public ServiceResult newDocumentForUser(string userName);
    }
}
