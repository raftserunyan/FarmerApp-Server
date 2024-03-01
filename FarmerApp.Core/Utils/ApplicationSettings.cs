using Microsoft.Extensions.Configuration;

namespace FarmerApp.Core.Utils
{
    public class ApplicationSettings
    {
        public string ConnectionString { get; set; }
        public string JwtSecretKey { get; set; }
        public string JwtRefreshSecretKey { get; set; }
        public int AccessTokenExpiryMinutes { get; set; }
        public int RefreshTokenExpiryMinutes { get; set; }

        public SeqSettings Seq { get; set; }

        public ApplicationSettings(IConfiguration configuration)
        {
            ConnectionString = GetSetting(configuration, "DB_CONNECTION_STRING", "ConnectionStrings:DefaultConnection");
            JwtSecretKey = GetSetting(configuration, "JWT_SECRET_KEY", "JwtSecretKey");
            JwtRefreshSecretKey = GetSetting(configuration, "JWT_REFRESH_SECRET_KEY", "JwtRefreshSecretKey");
            AccessTokenExpiryMinutes = int.Parse(GetSetting(configuration, "ACCESS_TOKEN_EXPIRY_MINUTES", "AccessTokenExpiryMinutes"));
            RefreshTokenExpiryMinutes = int.Parse(GetSetting(configuration, "REFRESH_TOKEN_EXPIRY_MINUTES", "RefreshTokenExpiryMinutes"));

            //Seq = new SeqSettings(configuration);
        }

        private static string GetSetting(IConfiguration configuration, string envVarName, string configName)
        {
            return Environment.GetEnvironmentVariable(envVarName, EnvironmentVariableTarget.Process)
                   ?? configuration[configName];
        }

        public class SeqSettings
        {
            public string ServerUrl { get; set; }
            public string ApiKey { get; set; }

            public SeqSettings(IConfiguration configuration)
            {
                ServerUrl = GetSetting(configuration, "SEQ_SERVER_URL", "Seq:ServerUrl");
                ApiKey = GetSetting(configuration, "SEQ_API_KEY", "Seq:ApiKey");
            }
        }
    }
}
