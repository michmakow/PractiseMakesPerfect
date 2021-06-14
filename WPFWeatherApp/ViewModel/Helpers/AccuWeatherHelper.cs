using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WPFWeatherApp.Model;

namespace WPFWeatherApp.ViewModel.Helpers
{
    public class AccuWeatherHelper
    {
        public const string BASE_URL = "http://dataservice.accuweather.com/";
        public const string AUTOCOMPLETE_ENDPOINT = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        public const string CURRENT_CONDITION_ENDPOINT = "currentconditions/v1/{0}?apikey={1}";
        public const string API_KEY = "EsHPhzokk32Bu1EGexrdUwGOD5Fhk8Af";

        public static async Task <List<City>> GetCities(string query)
        {
            var url = BASE_URL + string.Format(AUTOCOMPLETE_ENDPOINT, API_KEY, query);

            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<City>>(json);
        }

        public static async Task<CurrentConditions> GetCurrentConditions(string cityKey)
        {
            var currentConditions = new CurrentConditions();

            var url = BASE_URL + string.Format(CURRENT_CONDITION_ENDPOINT, cityKey, API_KEY);

            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<CurrentConditions>>(json).FirstOrDefault();
        }
    }
}