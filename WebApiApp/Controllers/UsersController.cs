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
    public class UsersController : ApiController
    {
        private readonly UserService userRepo = new UserService();

        [HttpPost, AuthenticationFilter]
        public IHttpActionResult Users(UserModel user)
        {
            try
            {
                if (user == null)
                    return BadRequest("Invalid input data");

                if (!ModelState.IsValid)
                {
                    return Content(HttpStatusCode.BadRequest, ModelState.GetErrorsAsList());
                }

                var response = userRepo.CreateUser(user);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, AuthenticationFilter]
        public IHttpActionResult Users(string sort_field, string sort_order_mode, string filter_field, string filter_value, int? page, int? page_size)
        {
            try
            {
                var response = userRepo.SelectAllUsers(sort_field, sort_order_mode, filter_field, filter_value, page, page_size);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult Users(int id)
        {
            try
            {
                var response = userRepo.SelectUserById(id);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut, AuthenticationFilter]
        public IHttpActionResult Users(int id, EditUserModel user)
        {
            try
            {
                if (user == null)
                    return BadRequest("Invalid input data");

                if (!ModelState.IsValid)
                {
                    return Content(HttpStatusCode.BadRequest, ModelState.GetErrorsAsList());
                }

                var response = userRepo.EditUser(id, user);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete, AuthenticationFilter]
        public IHttpActionResult DeleteUser(int id)
        {
            try
            {
                var response = userRepo.DeleteUser(id);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
