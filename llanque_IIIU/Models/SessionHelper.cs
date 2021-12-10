using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Configuration;
using llanque_IIIU.Models;

namespace llanque_IIIU.Helpers {
    public class SessionHelper {
        public static bool ExistUserInSession() {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }
        public static void DestroyUserSession() {
            FormsAuthentication.SignOut();
        }
        public static USUARIO GetUser() {
            USUARIO user = new USUARIO();
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity) {
                FormsAuthenticationTicket ticket = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket;
                if (ticket != null) {
                    user = USUARIO.getUserByNick(ticket.ToString());
                }
            }
            return user;
        }
        public static void AddUserToSession(string nick) {
            bool persist = true;
            var cookie = FormsAuthentication.GetAuthCookie("usuario", persist);

            cookie.Name = FormsAuthentication.FormsCookieName;
            cookie.Expires = DateTime.Now.AddMonths(3);

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate,
                                                          ticket.Expiration, ticket.IsPersistent, nick);

            cookie.Value = FormsAuthentication.Encrypt(newTicket);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}