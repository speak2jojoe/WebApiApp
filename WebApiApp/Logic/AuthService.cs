using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiApp.Entities;
using WebApiApp.Models;

namespace WebApiApp.Logic
{
    public class AuthService
    {
        private readonly WebApiAppEntities dbContext = new WebApiAppEntities();

        public Auth CreateAuthUser(AuthModel model)
        {
            try
            {
                Auth newAuth = new Auth
                {
                    username = model.username,
                    password = model.password
                };

                dbContext.Auths.Add(newAuth);
                dbContext.SaveChanges();

                return newAuth;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}