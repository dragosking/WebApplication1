using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using WebApplication1.Model;
using Newtonsoft.Json;
using static WebApplication1.Model.YRModel;

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

            WeatherDetail[] data=new WeatherDetail[first.timeSeries.Length];
            DateTime[] time= new DateTime[10];
        

            for (int i = 0; i < first.timeSeries.Length; i++)
            {
                DateTime timeTemp = first.timeSeries[i].validTime;
                String temp=null;

                for (int j = 0; j < first.timeSeries[i].parameters.Length; j++)
                {
                    if (first.timeSeries[i].parameters[j].name.Equals("t"))
                    {
                        temp = first.timeSeries[i].parameters[j].values[0].ToString();
                    }
                }

                data[i] = new WeatherDetail
                {
                    temperature = temp,
                    time=timeTemp,
                };
            }

            return data;
        }

        private WeatherDetail[] CleanDataYR(RootobjectYR first)
        {

            WeatherDetail[] data = new WeatherDetail[first.properties.timeseries.Length];
            DateTime[] time = new DateTime[10];


            for (int i = 0; i < first.properties.timeseries.Length; i++)
            {
                DateTime timeTemp = first.properties.timeseries[i].time;
                String temp = first.properties.timeseries[i].data.instant.details.air_temperature.ToString();

                /*for (int j = 0; j < first.properties.timeseries[i]..Length; j++)
                {
                    if (first.timeSeries[i].parameters[j].name.Equals("t"))
                    {
                        temp = first.timeSeries[i].parameters[j].values[0].ToString();
                    }
                }*/

                data[i] = new WeatherDetail
                {
                    temperature = temp,
                    time = timeTemp,
                };
            }

            return data;
        }

        private Rootobject cleanData(Rootobject first)
        {
            Rootobject after;
            Model.Timesery[] seriesTemp=new Model.Timesery[first.timeSeries.Length];
            Parameter[] parasTemp=new Parameter[1];

        
            DateTime appTemp = first.approvedTime;
            for (int i = 0; i < first.timeSeries.Length; i++)
            {
                DateTime validTemp = first.timeSeries[i].validTime;

                for(int j=0; j < first.timeSeries[i].parameters.Length; j++ )
                {
                    if (first.timeSeries[i].parameters[j].name.Equals("t"))
                    {
                        parasTemp[0] = first.timeSeries[i].parameters[j];
                    }

                }

                seriesTemp[i] = new Model.Timesery
                {
                    validTime = validTemp,
                    parameters = parasTemp
                };
         
            }


            after = new Rootobject
            {
                approvedTime = appTemp,
                timeSeries=seriesTemp
            };

            return after;

        }

    

    }
}
