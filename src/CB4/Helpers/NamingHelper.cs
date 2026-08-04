using CB4.Models;

namespace CB4.Helpers;

internal static class NamingHelper
{
    internal static string GetBuilderName(BuildableModel model) => $"{model.Name}Builder";
    internal static string GetProxyName(BuildableModel model) => $"{model.Name}Proxy";
}
