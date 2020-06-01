# BlazorMobileExperiments

This project is an experimental app that I built to try out combining the following:

-   Vertical Slice Architecture.
-   Flux/Redux-inspired central app state store; it is not Flux/Redux, the states are mutated (currently at least).
    -   The Blazor libraries that were out there were targeting .Net Standard 2.1, which was not compatible with this project. That is the only reason why I programmed that part. Otherwise, I would have used an existing library. It turns out that I don't dislike my implementation even if it would need more work before being reusable and before following the immutability part of the Flux/Redux pattern (if I want to go there).
-   Mobile App using [Blazor](https://blazor.net/), [Xamarin.Forms](https://dotnet.microsoft.com/apps/xamarin/xamarin-forms), and the experimental [MobileBlazorBindings](https://github.com/xamarin/MobileBlazorBindings).
-   Testing Blazor/Xamarin.Forms components using [bUnit](https://github.com/egil/bunit), [xUnit](https://xunit.net/) and [.Net 5](https://dotnet.microsoft.com/download/dotnet/5.0).

## Libraries

-   [MediatR](https://github.com/jbogard/MediatR) ([MediatR.Extensions.Microsoft.DependencyInjection](https://github.com/jbogard/MediatR.Extensions.Microsoft.DependencyInjection))
-   [FluentValidation](https://fluentvalidation.net/)
-   [Scrutor](https://github.com/khellang/Scrutor)
    -   Registering open generics ended up in a total mess with extra weird exceptions and white/black screens (emulator and Android), so Scrutor became a life safe there.
-   [AutoMapper](https://automapper.org/) (not used yet)

## How to run

Open the solution with Visual Studio, make sure the Xamarin components are installed. Have an Android emulator or an android phone. Press F5 (Run) to run the Android project.

## What's next?

I don't know, I liked developing this demo app over the weekend (and learn a bit about Xamarin.Form), so I may fiddle with it more or create something more real... we will see...
