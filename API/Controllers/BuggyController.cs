using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController:BaseController
    {
        private readonly DbStoreContext _context;

        public BuggyController(DbStoreContext context){
            this._context = context;
        }

        [HttpGet]
        [Route("badrequest")]
        public ActionResult GetBadRequest(){
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet]
        [Route("notfound")]
        public ActionResult GetNotFound(){
            var temp = _context.Products.Find(32);
            if(temp == null){
                return NotFound(new ApiResponse(404));
            }
            return Ok();
        }

        [HttpGet]
        [Route("servererror")]
        public ActionResult GetServerError(){
            var temp = _context.Products.Find(32);
            var temp2 = temp.ToString();

            return Ok();
        }


        [HttpGet]
        [Route("validationerror/{id}")]
        public ActionResult GetValidationError(int id){

            return Ok();
        }
    }
}