using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

using PowerBI.ReportingServices.Security.Abstractions;
using PowerBI.ReportingServices.Security.Options;

namespace PowerBI.ReportingServices.Security.Services
{
    sealed class CasService : ICasService
    {
        private readonly Regex Ticket = new Regex(@"(?<=&?ticket=)([^&]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private readonly CasOptions options;

        private readonly HttpClient httpClient;

        public CasService(IOptions<CasOptions> options)
        {
            this.options = options.Value;
            this.httpClient = new HttpClient
            {
                BaseAddress = new Uri(this.options.BaseAddress)
            };
        }

        public string GetTicket(string queryString)
        {
            var match = Ticket.Match(queryString);
            if (match.Success)
            {
                var ticket = match.Value;
                return ticket;
            }

            return null;
        }

        public string GetLoginUrl(string service)
        {
            var loginUrl = $"{this.options.BaseAddress}{this.options.LoginPath}?service=" + HttpUtility.UrlEncode(service);
            return loginUrl;
        }

        public async Task<string> ServiceValidateAsync(string ticket, string service)
        {
            var resp = await httpClient.GetAsync($"{this.options.ServiceValidatePath}?ticket={ticket}&service={HttpUtility.UrlEncode(service)}");
            if (resp.IsSuccessStatusCode)
            {
                var stream = await resp.Content.ReadAsStreamAsync();
                var xml = new XmlDocument();
                xml.Load(stream);
                return xml.GetElementsByTagName("cas:user").Item(0)?.InnerText;
            }

            return null;
        }
    }
}

