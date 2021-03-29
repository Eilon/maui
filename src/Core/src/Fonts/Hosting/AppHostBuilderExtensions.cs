using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Maui.Hosting.Internal;

namespace Microsoft.Maui.Hosting
{
	public static partial class AppHostBuilderExtensions
	{
		public static IAppHostBuilder ConfigureFonts(this IAppHostBuilder builder)
		{
			builder.ConfigureServices<FontCollectionBuilder>((a, b) => { });
			return builder;
		}

		public static IAppHostBuilder ConfigureFonts(this IAppHostBuilder builder, Action<HostBuilderContext, IFontCollection> configureDelegate)
		{
			builder.ConfigureServices<FontCollectionBuilder>(configureDelegate);
			return builder;
		}

		class FontCollectionBuilder : FontCollection, IServiceCollectionBuilder
		{
			public void Build(IServiceCollection services)
			{
				services.AddSingleton<IEmbeddedFontLoader, EmbeddedFontLoader>();
				services.AddSingleton<IFontRegistrar>(provider => new FontRegistrar(provider.GetRequiredService<IEmbeddedFontLoader>()));
				services.AddSingleton<IFontManager>(provider => new FontManager(provider.GetRequiredService<IFontRegistrar>()));
			}

			public void Configure(IServiceProvider services)
			{
				var fontRegistrar = services.GetService<IFontRegistrar>();
				if (fontRegistrar == null)
					return;

				foreach (var font in this)
				{
					fontRegistrar.Register(font.Filename, font.Alias);
				}
			}
		}
	}
}