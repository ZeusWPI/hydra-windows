using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.IO;
using System.Linq;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Hydra.Controls;

namespace Hydra.Views {
    /// <summary>
    /// The basic UI for the app.
    /// </summary>
    public sealed partial class Shell : Page {
        
        /*
        private List<NavMenuItem> navMenuList = new List<NavMenuItem>(
            new[] {
                new NavMenuItem() {
                    Symbol = Symbol.Home,
                    Label = "Home",
                    DestPage = typeof(Home.HomePage)
                },
                new NavMenuItem() {
                    Symbol = Symbol.Like,
                    Label = "Resto",
                    DestPage = typeof(Resto.RestoPage)
                },
                new NavMenuItem() {
                    Symbol = Symbol.Help,
                    Label = "Info",
                    DestPage = typeof(Resto.RestoPage)
                },
                new NavMenuItem() {
                    Symbol = Symbol.Calendar,
                    Label = "Activiteiten",
                    DestPage = typeof(Resto.RestoPage)
                },
                new NavMenuItem() {
                    Symbol = Symbol.Comment,
                    Label = "Schamper",
                    DestPage = typeof(Resto.RestoPage)
                },
                new NavMenuItem() {
                    Symbol = Symbol.Audio,
                    Label = "Urgent.fm",
                    DestPage = typeof(Resto.RestoPage)
                },
                new NavMenuItem() {
                    Symbol = Symbol.Setting,
                    Label = "Instellingen",
                    DestPage = typeof(Resto.RestoPage)
                },
            }
        );*/

        public Shell() {
            InitializeComponent();
            
            CustomiseTitleBar();
        }

        private void CustomiseTitleBar() {
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            Color blueUGent = Color.FromArgb(255, 16, 75, 125);
            titleBar.BackgroundColor = blueUGent;
            titleBar.ButtonBackgroundColor = blueUGent;

            titleBar.ForegroundColor = Colors.White;
            titleBar.ButtonForegroundColor = Colors.White;
        }

        public void SetContentFrame(Frame frame) {
            RootSplitView.Content = frame;
        }

        public void SetMenuPaneContent(UIElement content) {
            RootSplitView.Pane = content;
        }
    }
}
