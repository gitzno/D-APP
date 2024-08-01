using D_APP_Core.Entities;
using D_APP_Core.Entities.ModelsInput;
using D_APP_Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Reflection.Metadata;

namespace D_APP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {

        private IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [Authorize]
        [HttpGet("newDocument")]
        public IActionResult newDocumentFortoday()
        {
            string username = User?.Identity?.Name;
            var res = _documentService.newDocumentForUser(username);
            if (res.statusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [EnableQuery]
        [Authorize]
        [HttpGet("getAllDocByUser")]
        public IActionResult getAllDocforUser()
        {
            string username = User?.Identity?.Name;
            var res = _documentService.GetAllDocumentsByUser(username);
            ;
            return Ok(res);
        }
        [EnableQuery]
        [Authorize]
        [HttpGet("getAllDocByUserFavorite")]
        public IActionResult favorate()
        {
            string username = User?.Identity?.Name;
            var res = _documentService.GetAllDocumentsByUserF(username);
            ;
            return Ok(res);
        }


        [Authorize]
        [HttpPost("updateDocument")]
        public IActionResult UpdateDocument([FromBody] DocumentUpdate doc, Guid docid)
        {
            var res = _documentService.updateDocument(docid, doc);
            return Ok(res);
        }
        [Authorize]
        [HttpGet("getDocument")]
        public IActionResult GetDocumentById(Guid docId)
        {
            string username = User?.Identity?.Name;
            var res = _documentService.getDocumentById(username, docId);
            if (res.statusCode == HttpStatusCode.OK)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
        [Authorize]
        [HttpDelete("Delete")]
        public IActionResult DeleteDocumentById(Guid docId)
        {
            var res = _documentService.deleteDocument(docId);
            if (res.statusCode == HttpStatusCode.OK)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
        [Authorize]
        [HttpGet("getDocByTime")]
        public IActionResult getDocByTime([FromBody] DateDis date)
        {
            
            string username = User?.Identity?.Name;
            if(username.IsNullOrEmpty())
            {
                return BadRequest("Mất token");
            }
            var res = _documentService.getDocByTime(username, date);
            if (res.statusCode == HttpStatusCode.OK)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
    }
}
