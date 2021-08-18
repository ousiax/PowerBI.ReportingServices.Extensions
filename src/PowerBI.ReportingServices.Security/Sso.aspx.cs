using System;
using System.Web;
using System.Web.Security;
using PowerBI.ReportingServices.Security.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace PowerBI.ReportingServices.Security
{
    public class Sso : System.Web.UI.Page
    {
        private void Page_Load(object sender, EventArgs e)
        {
            Auth(HttpContext.Current);
        }

        private void Auth(HttpContext context)
        {
            if (context.User != null
                && context.User.Identity != null
                && context.User.Identity.IsAuthenticated)
            {
                if (string.IsNullOrEmpty(FormsAuthentication.GetRedirectUrl(context.User.Identity.Name, false)))
                {
                    Response.Redirect("/reports");
                }
                return;
            }

            var reqUrl = context.Request.Url;
            var port = reqUrl.Port == 80 ? string.Empty : $":{reqUrl.Port}";
            var query = HttpUtility.ParseQueryString(context.Request.Url.Query);
            query.Remove("ticket");
            var service = $"{reqUrl.Scheme}://{reqUrl.Host}{port}{reqUrl.AbsolutePath}?{query}";
            var casUrlPart = HttpUtility.UrlDecode(context.Request.Url.PathAndQuery);
            var casSrv = ServiceUtility.Provider.GetService<ICasService>();
            var ticket = casSrv.GetTicket(casUrlPart);
            if (!string.IsNullOrEmpty(ticket))
            {
                var userName = casSrv.ServiceValidateAsync(ticket, service).Result;
                if (!string.IsNullOrEmpty(userName))
                {
                    ServiceUtility.Provider.GetService<IUserService>().CreateIfNotExists(userName);

                    FormsAuthentication.SetAuthCookie(userName, false);
                    context.Response.Redirect(FormsAuthentication.GetRedirectUrl(userName, false));
                    return;
                }
            }

            HttpContext.Current.Response.Redirect(casSrv.GetLoginUrl(service));
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new EventHandler(this.Page_Load);
        }
        #endregion
    }
}
