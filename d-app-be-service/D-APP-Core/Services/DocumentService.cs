using D_APP_Core.Entities;
using D_APP_Core.Entities.Program;
using D_APP_Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using D_APP_Core.Entities.ModelsInput;
using D_APP_Core.Interfaces.IRepositories;
using System.Reflection.Metadata;
using System.Xml.Linq;
using System.Security.Principal;
using System.Text.Json;

namespace D_APP_Core.Services
{
    public class DocumentService : IDocumentService
    {
        public IDocumentRepository repository;
        public IUserRepository userRepository;

        public DocumentService(IDocumentRepository repository, IUserRepository userRepository)
        {
            this.repository = repository;
            this.userRepository = userRepository;
        }

        public ServiceResult createNewDocumentForToday(User user)
        {
            Documents document = new Documents()
            {
                DocId = Guid.NewGuid(),
                UserId = user.UserId,
                DocTitle = "New Document",
                DocCreateDate = DateTime.Now,
                DocCont = "",
                DocModifiDate = DateTime.Now,
                DocStatus = 0,
                User = null
            };
            return new ServiceResult()
            {
                devMsg = "Create new successfully!",
                userMsg = "Tạo document thành công!",
                statusCode = HttpStatusCode.OK,
                data = document
            };
        }

        public ServiceResult deleteDocument(Guid docid)
        {
            var res = repository.DeleteDocument(docid);
            if (res <= 0)
            {
                return new ServiceResult()
                {
                    devMsg = "Delete doc failed!",
                    userMsg = "Xóa document thất bại!",
                    statusCode = HttpStatusCode.BadRequest,
                    data = res
                };
            }

            return new ServiceResult()
            {
                devMsg = "Delete doc successfully!",
                userMsg = "Xóa document thành công!",
                statusCode = HttpStatusCode.OK,
                data = res
            };
        }

        public ServiceResult GetAllDocumentsByUser(string userName)
        {
            var user = userRepository.getUserByUsername(userName);
            List<Documents> res = (List<Documents>)repository.GetAllDocuments(user.UserId);
            List<DtoDoc> ress = new List<DtoDoc>();
            foreach (var item in res)
            {
                ress.Add(new DtoDoc()
                {
                    DocId = item.DocId,
                    UserId = item.UserId,
                    DocTitle = item.DocTitle,
                    DocCreateDate = item.DocCreateDate,
                    DocCont = item.DocCont,
                    DocModifiDate = item.DocModifiDate,
                    DocStatus = item.DocStatus,
                });
            }
            return new ServiceResult()
            {
                devMsg = "Get all document successfully!",
                userMsg = "Lấy toàn bộ doc người dùng thành công!",
                statusCode = HttpStatusCode.OK,
                data = ress
            };
        }

        public ServiceResult getDocByTime(string username, DateDis date)
        {
            var user = userRepository.getUserByUsername(username);
            DateTime dateTime = date.startDate.ToDateTime(TimeOnly.MinValue);
            DateTime dateTime2 = date.endDate.ToDateTime(TimeOnly.MinValue);

            // You can also specify the time and DateTimeKind
            TimeOnly specificTime = TimeOnly.Parse("10:30:00");
            DateTimeKind dateTimeKind = DateTimeKind.Utc;  // Or DateTimeKind.Local

            dateTime = date.startDate.ToDateTime(specificTime, dateTimeKind);
            dateTime2 = date.endDate.ToDateTime(specificTime, dateTimeKind);
            var res = repository.GetDocumentsFrom(dateTime, dateTime2);

            return new ServiceResult()
            {
                devMsg = "Get document successfully!",
                userMsg = "Lấy doc thành công!",
                statusCode = HttpStatusCode.OK,
                data = res
            };
        }

        public ServiceResult getDocumentById(string username, Guid docId)
        {

            var doc = repository.GetDocument(docId);
            DtoDoc res = new DtoDoc()
            {
                DocId = doc.DocId,

                UserId = doc.UserId,
                DocTitle = doc.DocTitle,
                DocCreateDate = doc.DocCreateDate,
                DocCont = doc.DocCont,
                DocModifiDate = doc.DocModifiDate,
                linkImg = doc.linkImg,
                DocStatus = doc.DocStatus

            };
            if (res == null)
            {
                return new ServiceResult()
                {
                    devMsg = "Get document Failed!",
                    userMsg = "Lấy doc thất bại!",
                    statusCode = HttpStatusCode.BadRequest,
                    data = res
                };
            }
            var user = userRepository.getUserByUsername(username);
            if (res.UserId != user.UserId)
            {
                return new ServiceResult()
                {
                    devMsg = "Get document Failed, Error token or username!",
                    userMsg = "Bận không có quyền truy cập vào!",
                    statusCode = HttpStatusCode.BadRequest,
                    data = res
                };
            }
            return new ServiceResult()
            {
                devMsg = "Get document successfully!",
                userMsg = "Lấy doc thành công!",
                statusCode = HttpStatusCode.OK,
                data = res
            };
        }

        public ServiceResult updateDocument(Guid docid, DocumentUpdate document)
        {

            Documents doc = repository.GetDocument(docid);
            doc.DocTitle = document.DocTitle;
            doc.DocCont = document.DocCont;
            doc.DocStatus = document.DocStatus;
            doc.linkImg = document.linkImg;
            var res = repository.UpdateDocument(doc);
            if (res == 0)
            {
                return new ServiceResult()
                {
                    devMsg = "Update doc Failed!",
                    userMsg = "Cập nhật tài liệu thất bại!",
                    statusCode = HttpStatusCode.BadRequest,
                    data = res
                };
            }
            return new ServiceResult()
            {
                devMsg = "Update doc successfully!",
                userMsg = "Cập nhật tài liệu thành công!",
                statusCode = HttpStatusCode.OK,
                data = res
            };
        }
        public ServiceResult newDocumentForUser(string userName)
        {

            var user = userRepository.getUserByUsername(userName);
            Documents document = new Documents()
            {
                DocId = new Guid(),
                UserId = user.UserId,
                DocTitle = "This is new Doc in " + DateTime.Now,
                DocCont = "",
                DocCreateDate = DateTime.Now,
                DocModifiDate = DateTime.Now,
                DocStatus = 0,
                User = null,
            };
            var res = repository.CreateDocument(document);
            if (res <= 0)
            {
                return new ServiceResult()
                {
                    devMsg = "Validate account failed",
                    userMsg = "Tạo doc thành công!",
                    statusCode = HttpStatusCode.BadRequest,
                    data = document.DocId
                };
            }
            return new ServiceResult()
            {
                devMsg = "Create new doc successfully",
                userMsg = "Tạo doc thành công!",
                statusCode = HttpStatusCode.OK,
                data = document.DocId

            };
        }

        public ServiceResult GetAllDocumentsByUserF(string userName)
        {
            var user = userRepository.getUserByUsername(userName);
            List<Documents> res = (List<Documents>)repository.GetAllDocuments(user.UserId).Where(s => s.DocStatus == 1).ToList();
            List<DtoDoc> ress = new List<DtoDoc>();
            foreach (var item in res)
            {
                if (item.DocStatus == 1)
                {
                    ress.Add(new DtoDoc()
                    {
                        DocId = item.DocId,
                        UserId = item.UserId,
                        DocTitle = item.DocTitle,
                        DocCreateDate = item.DocCreateDate,
                        DocCont = item.DocCont,
                        DocModifiDate = item.DocModifiDate,
                        DocStatus = item.DocStatus,
                        linkImg = item.linkImg
                    });
                }
            }
            return new ServiceResult()
            {
                devMsg = "Get all document successfully!",
                userMsg = "Lấy toàn bộ doc người dùng thành công!",
                statusCode = HttpStatusCode.OK,
                data = ress
            };
        }
    }
}
