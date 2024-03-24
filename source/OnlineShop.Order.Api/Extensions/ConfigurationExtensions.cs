namespace OnlineShop.Order.Api.Extensions;

public static class ConfigurationExtensions
{
    public static T GetOptions<T>(this ConfigurationManager configuration) where T : new()
    {
        var instance = new T();
        configuration.GetSection(typeof(T).Name).Bind(instance);
        return instance;
    }
}
