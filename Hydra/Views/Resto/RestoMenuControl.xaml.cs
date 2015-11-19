using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Hydra.Models;

namespace Hydra.Views.Resto {

    public sealed partial class RestoMenuControl : UserControl {
        public RestoMenuControl() {
            InitializeComponent();
            GetRestoMenus();
        }

        public async void GetRestoMenus() {
            RestoMenuList.DataContext = await new RestoMenuFactory().GetRestoMenus();
        }
    }
}
