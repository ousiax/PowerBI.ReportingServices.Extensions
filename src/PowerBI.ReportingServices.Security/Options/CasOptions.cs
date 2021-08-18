namespace PowerBI.ReportingServices.Security.Options
{
    public sealed class CasOptions
    {
        /// <summary>
        /// https://cas.fosun.com:8443
        /// </summary>
        [KeyName("cas.baseaddress")]
        public string BaseAddress { get; set; }

        /// <summary>
        /// /cas/login
        /// </summary>
        [KeyName("cas.login.path")]
        public string LoginPath { get; set; }

        /// <summary>
        /// /cas/serviceValidate
        /// </summary>
        [KeyName("cas.service.validate.path")]
        public string ServiceValidatePath { get; set; }
    }
}
