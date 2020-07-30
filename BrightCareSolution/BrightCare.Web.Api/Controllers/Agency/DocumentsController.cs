using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrightCare.Dtos.Agency.Document;
using BrightCare.Service.Interface.Agency.Documents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrightCare.Web.Api.Controllers.Agency
{
    [Route("api/agency/[controller]")]
    [ApiController]
    public class DocumentsController : BaseController
    {
        public readonly IUserDocumentService _documentService;

        public DocumentsController(IUserDocumentService _documentService)
        {
            this._documentService = _documentService;
        }

        [HttpPost]
        public ActionResult CreateUpdateDocument(UserDocumentDTO documentDTO)
        {
            return Ok(_documentService.CreateUpdateDocument(documentDTO, GetToken(HttpContext)));

        }
        [HttpDelete]
        public ActionResult DeleteUserDocument(int id)
        {
            return Ok(_documentService.DeleteUserDocument(id, GetToken(HttpContext)));
        }

        [HttpGet]
        public ActionResult GetUserDocumentById(int Documentid)
        {
            return Ok(_documentService.GetUserDocumentById(Documentid, GetToken(HttpContext)));
        }

        [HttpGet("GetDocumentByUserID")]
        public ActionResult GetUserDocument(int Userid,string key)
        {
            return Ok(_documentService.GetUserDocument(Userid, key, GetToken(HttpContext)));
        }
    }
}
