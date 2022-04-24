using AppCore.Business.Utils.JsonWebToken.Bases;
using AppCore.Bayi.Bases;

namespace AppCore.Business.Utils.JsonWebToken
{
    public class JwtUtil : JwtUtilBase
    {
        public JwtUtil(AppSettingsUtilBase appSettingsUtil) : base(appSettingsUtil)
        {

        }
    }
}
