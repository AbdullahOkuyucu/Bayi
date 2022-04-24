using AppCore.Bayi.Bases;
using Microsoft.Extensions.Configuration;

namespace AppCore.Bayi
{
    public class AppSettingsUtil : AppSettingsUtilBase
    {
        public AppSettingsUtil(IConfiguration configuration) : base(configuration)
        {

        }
    }
}
