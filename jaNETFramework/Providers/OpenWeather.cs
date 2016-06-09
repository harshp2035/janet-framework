﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace jaNETFramework
{
    public class OpenWeather : IWeather
    {
        public string TodayConditions { get; set; }
        public string TodayLow { get; set; }
        public string TodayHigh { get; set; }
        public string TodayDay { get; set; }
        public string TomorrowConditions { get; set; }
        public string TomorrowLow { get; set; }
        public string TomorrowHigh { get; set; }
        public string TomorrowDay { get; set; }
        public string CurrentTemp { get; set; }
        public string CurrentPresure { get; set; }
        public string CurrentHumidity { get; set; }
        public string CurrentCity { get; set; }

        public class Coord
        {
            public double lon { get; set; }
            public double lat { get; set; }
        }

        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class Main
        {
            public double temp { get; set; }
            public int pressure { get; set; }
            public int humidity { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set; }
        }

        public class Wind
        {
            public double speed { get; set; }
            public int deg { get; set; }
            public double gust { get; set; }
        }

        public class Clouds
        {
            public int all { get; set; }
        }

        public class Sys
        {
            public int type { get; set; }
            public int id { get; set; }
            public double message { get; set; }
            public string country { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
        }

        public class RootObject
        {
            public Coord coord { get; set; }
            public List<Weather> weather { get; set; }
            public string @base { get; set; }
            public Main main { get; set; }
            public Wind wind { get; set; }
            public Clouds clouds { get; set; }
            public int dt { get; set; }
            public Sys sys { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public int cod { get; set; }
        }

        public OpenWeather()
        {
            Action getWeather = () =>
            {
                try
                {
                    string endpoint = Helpers.Xml.AppConfigQuery("jaNET/System/Others/Weather").Item(0).InnerText;
                    var oJS = new JavaScriptSerializer();
                    var oRootObject = new RootObject();
                    oRootObject = oJS.Deserialize<RootObject>(Helpers.Http.Get(endpoint));
                    TodayConditions = oRootObject.weather[0].main;
                    TodayHigh = oRootObject.main.temp_max.ToString();
                    TodayLow = oRootObject.main.temp_min.ToString();
                    CurrentCity = oRootObject.name;
                    CurrentTemp = oRootObject.main.temp.ToString();
                    CurrentHumidity = oRootObject.main.humidity.ToString();
                    CurrentPresure = oRootObject.main.pressure.ToString();
                }
                catch { }
            };
            Process.CallWithTimeout(getWeather, 10000);
        }
    }
}
