namespace SharedLib;

public static class Extensions
{
    public static T GetRequired<T>(this INavigationParameters parameters)
    {
        var key = typeof(T).Name;
        if (!parameters.ContainsKey(key))
            throw new InvalidOperationException($"NavParameter '{key}' was not found");
        
        return parameters.GetValue<T>(key);
    }
}