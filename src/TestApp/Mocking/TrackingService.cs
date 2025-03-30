using System;
using System.IO;
using System.Text.Json;

namespace TestApp.Mocking
{
    public class TrackingService
    {
        public Location Get()
        {
            string json = File.ReadAllText("tracking.txt");

            Location location = JsonSerializer.Deserialize<Location>(json);

            if (location == null)
                throw new ApplicationException("Error parsing the location");

            return location;
        }
    }


    public class Location
    {
        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public override string ToString()
        {
            return $"{Latitude} {Longitude}";
        }

    }
}
