using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiApp.Logic;
using WebApiApp.Models;
using WebApiApp.Utilities;

namespace WebApiApp.Controllers
{
    public class AuthController : ApiController
    {
        private readonly AuthService authRepo = new AuthService();

        [HttpPost]
        public IHttpActionResult Auth(AuthModel authUser)
        {
            try
            {
                if (authUser == null)
                    return BadRequest("Invalid input data");

                if (!ModelState.IsValid)
                {
                    return Content(HttpStatusCode.BadRequest, ModelState.GetErrorsAsList());
                }

                var response = authRepo.CreateAuthUser(authUser);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
