using Microsoft.Maui.Platform;
using prototype.Handlers;
using prototype.Models;
using prototype.Pages;
using prototype.ViewModels;

namespace prototype;

public partial class App : Application
{
    public static UserBasicInfo UserDetails;
    public App()
    {
        InitializeComponent();
        MainPage = new NavigationPage(new prototype.Pages.LoginPage());
        
        //Border less entry
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(BorderlessEntry), (handler, view) =>
        {
            if (view is BorderlessEntry)
            {
#if __ANDROID__
                handler.PlatformView.SetBackgroundColor(Colors.Transparent.ToPlatform());
#elif __IOS__
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
            }
        });

        
    }
}