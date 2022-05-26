using CefSharp;
using CefSharp.JavascriptBinding;
using jayteeo.Utilities.SystemInformation.Discovery.Cef.Core.Implementation.Services;
using System;
using System.Diagnostics;
using System.Windows;

namespace jayteeo.Utilities.SystemInformation.Discovery.Cef.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeChromium();
        }

        private void InitializeChromium()
        {
            var indexFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\Assets\\Pages\\index.html";
            chromiumBrowser.JavascriptMessageReceived += ChromiumBrowser_JavascriptMessageReceived;
            chromiumBrowser.FrameLoadEnd += ChromiumBrowser_FrameLoadEnd;
            chromiumBrowser.Address = indexFilePath;
            CefSharpSettings.WcfEnabled = true;
            chromiumBrowser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
            chromiumBrowser.JavascriptObjectRepository.ResolveObject += JavascriptObjectRepository_ResolveObject;
            chromiumBrowser.JavascriptObjectRepository.ObjectBoundInJavascript += JavascriptObjectRepository_ObjectBoundInJavascript;
        }

        private void JavascriptObjectRepository_ResolveObject(object sender, CefSharp.Event.JavascriptBindingEventArgs e)
        {
            var repo = e.ObjectRepository;
            if (e.ObjectName == "SystemDiscoveryService")
            {
                repo.NameConverter = new CamelCaseJavascriptNameConverter();
                repo.Register("SystemDiscoveryService",
                    new SystemDiscoveryService(),
                    isAsync: true,
                    options: null);
            }
        }

        private void JavascriptObjectRepository_ObjectBoundInJavascript(object sender, CefSharp.Event.JavascriptBindingCompleteEventArgs e)
        {
            Debug.WriteLine($"Object {e.ObjectName} was bound successfully.");
        }

        private void ChromiumBrowser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            chromiumBrowser.ShowDevTools();
        }

        private void ChromiumBrowser_JavascriptMessageReceived(object sender, JavascriptMessageReceivedEventArgs e)
        {
            //TODO
        }
    }
}
