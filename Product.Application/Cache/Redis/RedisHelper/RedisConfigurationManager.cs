using Microsoft.Extensions.Configuration;

namespace Product.Application.Cache.Redis.RedisHelper;

public static class RedisConfigurationManager
{
    public static IConfiguration AppSetting { get; }

    static RedisConfigurationManager()
    {
        AppSetting = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
    }
}
