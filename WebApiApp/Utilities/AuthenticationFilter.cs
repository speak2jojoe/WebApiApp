using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebApiApp.Entities;

namespace WebApiApp.Utilities
{
    public class AuthenticationFilter : AuthorizationFilterAttribute
    {
        private readonly WebApiAppEntities dbContext = new WebApiAppEntities();

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var request = actionContext.Request;
            //string authHeader = request.Headers["Authorization"];
            string authHeader = request.Headers?.Authorization.Parameter;

            if (!string.IsNullOrEmpty(authHeader))
            {
                string encodedUsernameAndPassword = null;
                if (authHeader.StartsWith("Basic"))
                    encodedUsernameAndPassword = authHeader.Substring("Basic ".Length).Trim();
                else
                    encodedUsernameAndPassword = authHeader.Trim();

                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernameAndPassword));

                int separatorIndex = usernamePassword.IndexOf(':');

                var username = usernamePassword.Substring(0, separatorIndex);
                var password = usernamePassword.Substring(separatorIndex + 1);

                var authUser = dbContext.Auths.SingleOrDefault(x => x.username == username && x.password == password);
                if(authUser == null)
                {
                    Challenge(actionContext, "Invalid authentication details");
                    return;
                }
                else
                {

                }
            }
            else
            {
                Challenge(actionContext, "Basic authentication details required");
                return;
            }

            base.OnAuthorization(actionContext);
        }

        void Challenge(HttpActionContext actionContext, string message)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, message);
        }
    }
}