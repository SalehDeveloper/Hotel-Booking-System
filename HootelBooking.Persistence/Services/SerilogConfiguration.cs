using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Persistence.Services
{
    public  static class SerilogConfiguration 
    {
        public static void ConfigureSerilog (IConfiguration configuration  )
        {
            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration) // Read from appsettings.json
            .CreateLogger();
        }
    }
}
