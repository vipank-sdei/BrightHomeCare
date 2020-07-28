
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Primitives;
using BrightCare.Common;
using BrightCare.Entity.SuperAdmin;
using HC.Common;

namespace BrightCare.Persistence
{
    public class ConfigurationBuilderContext
    {
        
        public string GetNewConnection(HttpContext httpContext)
        {

            string host = string.Empty;
            host = "brightcare";
            StringValues authorizationToken;
            var tokenExist = httpContext.Request.Headers.TryGetValue("Authorization", out authorizationToken);// get host name from token
            if (tokenExist)
            {
                //get the host name from request
                TokenModel token = CommonMethods.GetTokenDataModel(httpContext);
                host = token.DomainName;
            }


            ////return new connetion string which made from request host
            return GetDomain(host);
            
        }
        public string GetDomain(string host)
        {
            //TO DO Master connection string should be from app-setings
            var optionsBuilder = new DbContextOptionsBuilder<HCMasterContext>();
            optionsBuilder.UseSqlServer(CreateMasterConnectionString());
            HCMasterContext masterContext = new HCMasterContext(optionsBuilder.Options);

            string con = string.Empty;

            //get the organization from Business-Name
            MasterOrganization org = masterContext.MasterOrganization.Where(a => a.BusinessName == host && a.IsDeleted == false).FirstOrDefault();
            if (org != null)
            {
                //get the db credentials for new connection
                OrganizationDatabaseDetail orgData = masterContext.OrganizationDatabaseDetail.Where(a => a.Id == org.DatabaseDetailId && a.IsDeleted == false).FirstOrDefault();

                //initialize Domain token model to create new connection string
                DomainToken domainData = new DomainToken();
                domainData.ServerName = orgData.ServerName;
                domainData.DatabaseName = orgData.DatabaseName;
                domainData.Password = orgData.Password;
                domainData.UserName = orgData.UserName;
                con = ConnectionString(domainData);
            }
            //return new connection string
            return con;
        }
        /// <summary>
        /// create dynamically new connection string
        /// </summary>
        /// <param name="domainToken"></param>
        /// <returns></returns>
        public string ConnectionString(DomainToken domainToken)
        {
            string conn = @"Server=" + domainToken.ServerName + ";Database=" + domainToken.DatabaseName + ";Trusted_Connection=True;MultipleActiveResultSets=true;Integrated Security=false;User ID=" + domainToken.UserName + ";Password=" + domainToken.Password + ";";
            return conn;
        }

        public string CreateOrganizationConnectionString()
        {
            DomainToken domainToken = new DomainToken();
            domainToken.ServerName = HCOrganizationConnectionStringEnum.Server;
            domainToken.DatabaseName = HCOrganizationConnectionStringEnum.Database;
            domainToken.UserName = HCOrganizationConnectionStringEnum.User;
            domainToken.Password = HCOrganizationConnectionStringEnum.Password;
            return ConnectionString(domainToken);
        }

        public string CreateMasterConnectionString()
        {
            DomainToken domainToken = new DomainToken();
            domainToken.ServerName = HCMasterConnectionStringEnum.Server;
            domainToken.DatabaseName = HCMasterConnectionStringEnum.Database;
            domainToken.UserName = HCMasterConnectionStringEnum.User;
            domainToken.Password = HCMasterConnectionStringEnum.Password;
            return ConnectionString(domainToken);
        }
    }
}