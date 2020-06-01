namespace Core
{
    public static class MobileBlazorBindingsBunitExtensions
    {
        public static string Blazorize(this string tagName)
        {
            return tagName.PrefixMobileBlazorBindings().EscapeDots();
        }

        public static string EscapeDots(this string tagName)
        {
            return tagName.Replace(".", "\\.");
        }

        public static string PrefixMobileBlazorBindings(this string tagName)
        {
            return $"Microsoft.MobileBlazorBindings.Elements.{tagName}";
        }
    }

}