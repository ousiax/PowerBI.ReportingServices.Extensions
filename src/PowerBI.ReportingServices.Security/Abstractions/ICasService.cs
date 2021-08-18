using System.Threading.Tasks;

namespace PowerBI.ReportingServices.Security.Abstractions
{
    /// <summary>
    /// <see cref="https://apereo.github.io/cas/4.2.x/protocol/CAS-Protocol-Specification.html#24-validate-cas-10"/>
    /// /login credential requestor / acceptor
    /// /validate service ticket validation
    /// /serviceValidate service ticket validation[CAS 2.0]
    /// /proxyValidate service/proxy ticket validation[CAS 2.0]
    /// /proxy proxy ticket service[CAS 2.0]
    /// /p3/serviceValidate service ticket validation[CAS 3.0]
    /// /p3/proxyValidate service/proxy ticket validation[CAS 3.0]
    /// </summary>
    public interface ICasService
    {
        string GetTicket(string queryString);

        string GetLoginUrl(string service);

        Task<string> ServiceValidateAsync(string ticket, string service);
    }
}
