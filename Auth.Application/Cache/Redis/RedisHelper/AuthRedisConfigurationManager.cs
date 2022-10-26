using Microsoft.Extensions.Configuration;

namespace Auth.Application.Cache.Redis.RedisHelper;

public static class AuthRedisConfigurationManager
{
    public static IConfiguration AppSetting { get; }

    static AuthRedisConfigurationManager()
    {
        AppSetting = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
    }
}
