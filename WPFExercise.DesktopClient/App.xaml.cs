using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using WPFExercise.DesktopClient.ViewModels;
using WPFExercise.DesktopClient.Views;
using WPFExercise.DesktopClient.Extension;
using WPFExercise.DesktopClient.Helper;

namespace WPFExercise.DesktopClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            GlobalExceptionHandler();
        }


        private void GlobalExceptionHandler()
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                    ExceptionHandler.GlobalUnhandledExceptionHandler((Exception)e.ExceptionObject, "AppDomain.CurrentDomain.UnhandledException");
        }
    }
}
