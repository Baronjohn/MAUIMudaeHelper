namespace MauiApp1;

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
#if WINDOWS
        const int WindowWidth   = 1287;
        const int WindowHeight  = 580;
#endif

        var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
        {
#if WINDOWS
            var mauiWindow = handler.VirtualView;
            var nativeWindow = handler.PlatformView;
            nativeWindow.Activate();
            IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
            WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
            AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            appWindow.MoveAndResize(new RectInt32(0 ,0, WindowWidth, WindowHeight));

            if (appWindow.Presenter is OverlappedPresenter p)
            {
                p.IsResizable = false;
                p.IsMaximizable = false;
                //p.SetBorderAndTitleBar(true, false);
            }
#endif
        });

        return builder.Build();
	}
}
