using Hydra.ViewModels.UrgentFm;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Hydra.Views.UrgentFm {
    public sealed partial class UrgentFmPage : SessionStateAwarePage {

        private UrgentFmPageViewModel viewModel;

        public UrgentFmPage() {
            InitializeComponent();

            this.viewModel = (UrgentFmPageViewModel)this.DataContext;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            UrgentPlayer.MediaFailed += UrgentPlayer_MediaFailed;
            PlayPauseButton.Click += PlayPauseButton_Click;

            base.OnNavigatedTo(e);
        }

        private void PlayPauseButton_Click(object sender, RoutedEventArgs e) {
            if (viewModel.Playing) {
                UrgentPlayer.Stop();
                PlayPauseSymbol.Symbol = Symbol.Play;
            } else {
                UrgentPlayer.Play();
                PlayPauseSymbol.Symbol = Symbol.Pause;
            }
        }

        private void UrgentPlayer_MediaFailed(object sender, ExceptionRoutedEventArgs e) {
            // TODO: properly notify user
            Debug.WriteLine("Something went wrong :( \n");
            Debug.WriteLine(e.ErrorMessage);
        }
        
    }
}
