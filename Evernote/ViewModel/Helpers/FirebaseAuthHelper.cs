using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Evernote.Model;
using Newtonsoft.Json;

namespace Evernote.ViewModel.Helpers
{
    public class FirebaseAuthHelper
    {
        private static string api_key = "AIzaSyB2kg5xaxUbB-8hBO1gIxjD1rdyz_r4V0s";

        public static async Task<bool> Register(User user)
        {
            using var client = new HttpClient();
            var body = new
            {
                email = user.Username,
                password = user.Password,
                returnSecureToken = true
            };

            var bodyJson = JsonConvert.SerializeObject(body);
            var data = new StringContent(bodyJson,Encoding.UTF8,"application/json");

            var response = await client.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={api_key}", data);

            if (response.IsSuccessStatusCode)
            {
                var resultJson = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<FirebaseResult>(resultJson);
                App.UserId = result.localId;

                return true;
            }
            else
            {
                var errorJson = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject < Error>(errorJson);
                MessageBox.Show(error.error.message);

                return false;
            }
        }
    }

    public class FirebaseResult
    {
        public string kind { get; set; }
        public string idToken { get; set; }
        public string email { get; set; }
        public string refreshToken { get; set; }
        public string expiresIn { get; set; }
        public string localId { get; set; }
    }

    public class ErrorDetails
    {
        public int code { get; set; }
        public string message { get; set; }
    }

    public class Error
    {
        public ErrorDetails error { get; set; }
    }
}
