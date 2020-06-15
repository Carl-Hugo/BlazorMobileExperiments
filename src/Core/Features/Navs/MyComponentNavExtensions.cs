namespace Core
{
    using Core.Features.Navs;
    using System.Threading.Tasks;

    public static class MyComponentNavExtensions
    {
        public static Task GoToPage2Async(this MyComponent component)
        {
            return component.GoToAsync(Constants.Page2);
        }

        public static Task GoHomeAsync(this MyComponent component)
        {
            return component.GoToAsync(Constants.Home);
        }
    }
}