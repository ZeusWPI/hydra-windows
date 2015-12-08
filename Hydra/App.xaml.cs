using Hydra.DataSources;
using Hydra.Views;
using Microsoft.Practices.Unity;
using Prism.Mvvm;
using Prism.Unity.Windows;
using Prism.Windows.AppModel;
using Prism.Windows.Navigation;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources;
using Windows.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Hydra {
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : PrismUnityApplication {

        public App() {
            this.InitializeComponent();
        }

        protected override UIElement CreateShell(Frame rootFrame) {
            var shell = Container.Resolve<Shell>();
            shell.SetContentFrame(rootFrame);
            return shell;
        }

        protected override Task OnInitializeAsync(IActivatedEventArgs args) {
            // Get the right resource (localized strings etc.)
            Container.RegisterInstance<IResourceLoader>(new ResourceLoaderAdapter(new ResourceLoader()));

            // Inject the data sources
            Container.RegisterInstance<IRestoSource>(new ZeusRestoApi());
            DsaApi dsaApi = new DsaApi();
            Container.RegisterInstance<IAssociationSource>(dsaApi);
            Container.RegisterInstance<IActivitySource>(dsaApi);
            Container.RegisterInstance<INewsSource>(dsaApi);

            return base.OnInitializeAsync(args);
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args) {
            NavigationService.Navigate(PageTokens.HomePage, null);
            return Task.FromResult(true);
        }

        /// <summary>
        /// Allow subfolder for page views
        /// </summary>
        protected override Type GetPageType(string pageToken) {
            var pageTokenParts = pageToken.Split('/');

            var assemblyQualifiedAppType = GetType().AssemblyQualifiedName;
            var pageNameWithParameter = assemblyQualifiedAppType.Replace(GetType().FullName, GetType().Namespace + ".Views.{0}.{1}");
            var viewFullName = string.Format(CultureInfo.InvariantCulture, pageNameWithParameter, pageTokenParts[0], pageTokenParts[1]);
            var viewType = Type.GetType(viewFullName);

            return viewType;
        }
    }
}
