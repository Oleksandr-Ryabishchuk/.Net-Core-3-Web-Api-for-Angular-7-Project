using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.SettingsModels
{
    public class ApplicationSettings
    {
        public string JWT_Secret { get; set; }
        public string Client_Url { get; set; }
    }
}
