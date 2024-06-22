namespace SharedLib;

public static class Extensions
{
    public static T? Get<T>(this INavigationParameters parameters)
        => parameters.GetValue<T>(typeof(T).Name);

    public static (string, TRequest) ToNavParam<TRequest>(this TRequest request) where TRequest : IRequest
        => (request.GetType().Name, request);
}