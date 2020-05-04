using System;
using System.Text;
using System.Web.Mvc;

namespace CRIntranet.Filters
{
    public class HSTSAttribute : RequireHttpsAttribute
    {
        public TimeSpan MaxAge { get; private set; }

        public bool IncludeSubDomains { get; private set; }

        public bool Preload { get; private set; }

        public HSTSAttribute(TimeSpan? maxAge, 
            bool includeSubdomains = true, 
            bool preload=false): base()
        {
            MaxAge = maxAge.HasValue ? maxAge.Value : TimeSpan.FromDays(30);//default to 30 days
            IncludeSubDomains = includeSubdomains;
            Preload = preload;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //only send on a secure connection
            if(filterContext.HttpContext.Request.IsSecureConnection )
            {
                //set the max age
                StringBuilder headerBuilder =
                    new StringBuilder($"max-age={MaxAge.TotalSeconds}");
                
                //add subdomains if indicated
                if(IncludeSubDomains)
                {
                    headerBuilder.Append("; includeSubDomains");
                }

                //add preload if indicated
                if(Preload)
                {
                    headerBuilder.Append("; preload");
                }

                //set header on the response
                filterContext.HttpContext.Response.Headers.Add(
                    "Strict-Transport-Security", headerBuilder.ToString());
            }
            else
            {
                HandleNonHttpsRequest(filterContext);
            }
        }



        //optionally, you could exclude some hosts
        //use this method and check before sending 
        //the hsts header
        private bool ExcludedHost(string host)
        {
            //TODO: implement any logic to exclude hosts
            //such as localhost, 127.0.0.1,[::1] etc.
            return false;
        }

        //you could add an alternate port handling method
        //by overriding this method to create the HTTPS
        //redirect and including the port number in the location

        //protected override void HandleNonHttpsRequest(AuthorizationContext filterContext)
        //{
        //    base.HandleNonHttpsRequest(filterContext);
        //}
    }
}