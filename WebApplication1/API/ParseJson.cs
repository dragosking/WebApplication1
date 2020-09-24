using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using WebApplication1.Model;
using Newtonsoft.Json;
using static WebApplication1.Model.YRModel;
using System.Globalization;

namespace WebApplication1.API
{
    public class ParseJson
    {

        public WeatherDetail[] ParseUrlSMHI(String Url, String UrlParameters)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Url);
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responseMessage = client.GetAsync(UrlParameters).Result;


            if (responseMessage.IsSuccessStatusCode)
            {

                var data = responseMessage.Content.ReadAsStringAsync().Result;    
                var answer = JsonConvert.DeserializeObject<Rootobject>(data);

                return CleanDataSMHI(answer);

            }
            else
            {
                return null;
            }

        }

        public WeatherDetail[] ParseUrlYR(String Url, String UrlParameters)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Url);

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

            HttpResponseMessage responseMessage = client.GetAsync(UrlParameters).Result;


            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                var answer = JsonConvert.DeserializeObject<RootobjectYR>(data);
                return CleanDataYR(answer);

            }
            else
            {
                return null;
            }

        }

        private WeatherDetail[] CleanDataSMHI(Rootobject first)
        {

            WeatherDetail[] data=new WeatherDetail[48];
            DateTime[] time= new DateTime[10];
        

            for (int i = 0; i < 48; i++)
            {
                DateTime timeTemp = first.timeSeries[i].validTime;
                String temp=null;
                String tempCoverage = null;

                for (int j = 0; j < first.timeSeries[i].parameters.Length; j++)
                {
                    if (first.timeSeries[i].parameters[j].name.Equals("t"))
                    {
                        temp = first.timeSeries[i].parameters[j].values[0].ToString();
                    }

                    if (first.timeSeries[i].parameters[j].name.Equals("Wsymb2"))
                    {

                        tempCoverage = first.timeSeries[i].parameters[j].values[0].ToString();
                        tempCoverage = ChangeValue(tempCoverage);
                    }

                 
                }

                temp = temp.Replace(",", ".");
                Decimal tempLon = Decimal.Parse(temp, CultureInfo.InvariantCulture);

                temp = tempLon.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture);
                temp = string.Format("{0}°C", temp);

                data[i] = new WeatherDetail
                {
                    temperature = temp,
                    coverage=tempCoverage,
                    time=timeTemp,
                    hour= timeTemp.ToString("HH:mm"),
                };
            }

            return data;
        }


        //fixa bättre
        private string ChangeValue(string input)
        {
            string temp = null;
            if (input.Equals("1"))
            {
                temp = "clear";
            }
            else if (input.Equals("2")){
                temp = "fair";
            }
            else if (input.Equals("3") || input.Equals("4"))
            {
                temp = "partly";
            }
            else if (input.Equals("5") || input.Equals("6"))
            {
                temp = "cloudy";
            }
            else if (input.Equals("7"))
            {
                temp = "foggy";
            }
            else if (input.Equals("8") || input.Equals("9") || input.Equals("10") || input.Equals("18") || input.Equals("19") || input.Equals("20") )
            {
                temp = "rain";
            }

            return temp;
        }


        private string ChangeValueYR(string input)
        {
            string temp = null;
            if (input.Equals("clearsky_day") || input.Equals("clearsky_night"))
            {
                temp = "clear";
            }
            else if (input.Equals("fair_day") || input.Equals("fair_night"))
            {
                temp = "fair";
            }
            else if (input.Equals("partlycloudy_day") || input.Equals("partlycloudy_night"))
            {
                temp = "partly";
            }
            else if (input.Equals("cloudy"))
            {
                temp = "cloudy";
            }
            else if (input.Equals("lightrain") || input.Equals("rain") || input.Equals("heavyrain") || input.Equals("lightrainshowers_day") || input.Equals("lightrainshowers_night") || input.Equals("rainshowers_night") || input.Equals("rainshowers_day") || input.Equals("heavyrainshowers_night") || input.Equals("heavyrainshowers_day"))
            {
                temp = "rain";
            }
            else if (input.Equals("fog"))
            {
                temp = "foggy";
            }


            return temp;
        }


        private WeatherDetail[] CleanDataYR(RootobjectYR first)
        {

            WeatherDetail[] data = new WeatherDetail[49];
            DateTime[] time = new DateTime[10];


            for (int i = 0; i < 49; i++)
            {
                DateTime timeTemp = first.properties.timeseries[i].time;
                string temp = first.properties.timeseries[i].data.instant.details.air_temperature.ToString();
                string tempCoverage = first.properties.timeseries[i].data.next_1_hours.summary.symbol_code.ToString();
                tempCoverage = ChangeValueYR(tempCoverage);


                temp = temp.Replace(",", ".");
                Decimal tempLon = Decimal.Parse(temp, CultureInfo.InvariantCulture);

                temp=tempLon.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture);
                temp = string.Format("{0}°C", temp);

                data[i] = new WeatherDetail
                {
                    temperature = temp.Replace(",", "."),
                    coverage = tempCoverage ,
                    time = timeTemp,
                    hour = timeTemp.ToString("HH:mm"),
                };
            }

            return data;
        }

      

    

    }
}
