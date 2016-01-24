using Prism.Windows.Mvvm;
using Windows.Devices.Geolocation;

namespace Hydra.Views.Resto {
    public sealed partial class RestoMapPage : SessionStateAwarePage {

        public Geopoint Center
        {
            get
            {
                var ghentCenter = new BasicGeoposition() {
                    Latitude = 51.02,
                    Longitude = 3.73
                };

                return new Geopoint(ghentCenter);
            }
        }

        public RestoMapPage() {
            InitializeComponent();
        }
    }
}
