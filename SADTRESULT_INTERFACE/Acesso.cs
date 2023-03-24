using System;
using System.Data;
using System.Configuration;
using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
using System.DirectoryServices;

namespace SADTRESULT_INTERFACE
{
    public class Acesso
    {
        public bool Autentica(string username, string password)
        {
       

            DirectoryEntry entry = new DirectoryEntry("LDAP://10.0.70.......", username, password);
            try
            {
                DirectorySearcher search = new DirectorySearcher(entry);
                SearchResult result;
                result = search.FindOne();

                if (result != null) return true; else return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
