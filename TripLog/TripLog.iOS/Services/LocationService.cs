using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using TripLog.Models;
using TripLog.Services;
using UIKit;
using Xamarin.Essentials;

namespace TripLog.iOS.Services
{
    public class LocationService : ILocationService
    {
        public async Task<GeoCoords> GetGeoCoordinatesAsync()
        {
            var location = await Geolocation.GetLocationAsync();
            return new GeoCoords
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };
        }
    }
}