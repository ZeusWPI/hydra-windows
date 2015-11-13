using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Hydra.Models;
using Hydra.Models.Resto;

namespace Hydra.Views.Resto {
    public sealed partial class RestoPage : Page {

        private const string restoApiUrl = "https://zeus.ugent.be/hydra/api/1.0/resto/";

        public RestoPage() {
            InitializeComponent();
            
            GetRestoMeta(); 
        }

        public async void GetRestoMeta() {
            RestoMeta restoMeta = (RestoMeta) await new RestoMenuFactory().FromRestApi(typeof(RestoMeta), restoApiUrl, "meta.json");

            RestoLocationList.DataContext = restoMeta.locations;
        }
    }
}
