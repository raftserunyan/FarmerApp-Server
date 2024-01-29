namespace FarmerApp
{
    public class ApplicationSettings
    {
        public string ConnectionString { get; set; }
        public string JwtSecretKey { get; set; }
        public string JwtRefreshSecretKey { get; set; }
        public int AccessTokenExpiryMinutes { get; set; }
        public int RefreshTokenExpiryMinutes { get; set; }

        public ApplicationSettings(IConfiguration configuration)
        {
            ConnectionString = GetSetting(configuration, "DB_CONNECTION_STRING", "DefaultConnection");
            JwtSecretKey = GetSetting(configuration, "JWT_SECRET_KEY", "JwtSecretKey");
            JwtRefreshSecretKey = GetSetting(configuration, "JWT_REFRESH_SECRET_KEY", "JwtRefreshSecretKey");
            AccessTokenExpiryMinutes = int.Parse(GetSetting(configuration, "ACCESS_TOKEN_EXPIRY_MINUTES", "AccessTokenExpiryMinutes"));
            RefreshTokenExpiryMinutes = int.Parse(GetSetting(configuration, "REFRESH_TOKEN_EXPIRY_MINUTES", "RefreshTokenExpiryMinutes"));
        }

        private string GetSetting(IConfiguration configuration, string envVarName, string configName)
        {
            return Environment.GetEnvironmentVariable(envVarName, EnvironmentVariableTarget.Process)
                   ?? configuration[configName];
        }
    }
}
