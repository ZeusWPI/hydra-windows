﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
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
    public sealed partial class RestoMapControl : UserControl {

        private const string restoApiUrl = "https://zeus.ugent.be/hydra/api/1.0/resto/";

        public RestoMapControl() {
            this.InitializeComponent();

            GetRestoLocations();
            ConfigureMap();
        }

        public async void GetRestoLocations() {
            RestoMeta restoMeta = (RestoMeta)await new RestoMenuFactory().FromRestApi(typeof(RestoMeta), restoApiUrl, "meta.json");

            foreach(RestoLocation resto in restoMeta.locations) {
                MapIcon pin = new MapIcon() {
                    Location = resto.Location,
                    Title = resto.name,
                    ZIndex = 0,
                    Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms:appx:///Assets/Icons/RestoMapIcon.png")),
                    CollisionBehaviorDesired = MapElementCollisionBehavior.RemainVisible
                };
                RestoLocationMap.MapElements.Add(pin);
            }
        }

        public void ConfigureMap() {
            RestoLocationMap.MapServiceToken = "d8jmVUfn9j2AvDUkjg4x~Uc_mgbDPMhW1C2cm7reMbg~AuFJC-cJJ0dng3bZlsuKhWxOAXsJsXuC18KtKzIEP8mCQpIarZTs9-BGDNQWaeNG";
            BasicGeoposition ghent = new BasicGeoposition() {
                Latitude = 51.053458,
                Longitude = 3.73038
            };
            RestoLocationMap.ZoomLevel = 13;
            RestoLocationMap.Center = new Geopoint(ghent);
        }
    }
}