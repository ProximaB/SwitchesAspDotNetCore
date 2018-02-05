using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SwitchesAPI.DB;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Handlers;

namespace SwitchesAPI.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SwitchesContext context;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, SwitchesContext _context)
        {
            string authHeader = context.Request.Headers["Authorization"];
            string websocketHeader = context.Request.Headers["Upgrade"];
            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                //Extract credentials
                string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

                int seperatorIndex = usernamePassword.IndexOf(':');

                var username = usernamePassword.Substring(0, seperatorIndex);
                var password = usernamePassword.Substring(seperatorIndex + 1);

                if (username == "Admin" && password == "Admin")
                {
                    await _next.Invoke(context);
                }
                else
                {
                    User user = _context.Users.FirstOrDefault((u => u.Name == username));
                    if (user == null)
                    {
                        context.Response.StatusCode = 401; //Unauthorized
                    }

                    var passwordHash = AuthenticationHashHandler.GenerateSaltedHash(password, user.PasswordSalt);

                    bool passwordIsValid = AuthenticationHashHandler.CompareByteArrays(passwordHash, user.Password);

                    if (passwordIsValid == true)
                    {
                        await _next.Invoke(context);
                    }
                }
            }
            else if (websocketHeader == "websocket")
            {              
                    await _next.Invoke(context);               
            }
            else
            {
                // no authorization header
                context.Response.StatusCode = 401; //Unauthorized
                return;
            }
        }
    }
}
