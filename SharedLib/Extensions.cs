namespace SharedLib;

public static class Extensions
{
    public static T? Get<T>(this INavigationParameters parameters)
        => parameters.GetValue<T>(typeof(T).Name);
}