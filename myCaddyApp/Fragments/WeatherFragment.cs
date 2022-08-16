using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace myCaddyApp.Fragments
{
    public class WeatherFragment : AndroidX.Fragment.App.Fragment
    {

        Button getWeatherButton;
        TextView placeTextView;
        TextView temperatureTextView;
        TextView weatherDescriptionTextView;
        EditText cityNameEditText;

        ProgressDialogFragment progressDialog;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);



        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View inf = inflater.Inflate(Resource.Layout.fragment_Weather, container, false);

            cityNameEditText = (EditText)inf.FindViewById(Resource.Id.cityNameText);
            placeTextView = (TextView)inf.FindViewById(Resource.Id.placeText);
            temperatureTextView = (TextView)inf.FindViewById(Resource.Id.temperatureTextView);
            weatherDescriptionTextView = (TextView)inf.FindViewById(Resource.Id.weatherDescriptionText);
            getWeatherButton = (Button)inf.FindViewById(Resource.Id.getWeatherButton);

            getWeatherButton.Click += GetWeatherButton_Click;

            return inf;
        }

        private void GetWeatherButton_Click(object sender, EventArgs e)
        {
            string place = cityNameEditText.Text;
            GetWeather(place);
        }


        async void GetWeather(string place)
        {
            string apiKey = "17e861c64054af9ecd117a25ad0c975d";
            string apiBase = "https://api.openweathermap.org/data/2.5/weather?q=";
            string unit = "metric";

            if (string.IsNullOrEmpty(place))
            {
                Toast.MakeText(Activity,"Please enter a valid city name", ToastLength.Short).Show();
                return;
            }

            if (!CrossConnectivity.Current.IsConnected)
            {
                Toast.MakeText(Activity,"No internet connection", ToastLength.Short).Show();
                return;
            }

            ShowProgressDialogue("Fetching weather...");

            // Asynchronous API call using HttpClient
            string url = apiBase + place + "&appid=" + apiKey + "&units=" + unit;
            var handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            string result = await client.GetStringAsync(url);

            Console.WriteLine(result);

            var resultObject = JObject.Parse(result);
            string weatherDescription = resultObject["weather"][0]["description"].ToString();
            string icon = resultObject["weather"][0]["icon"].ToString();
            string temperature = resultObject["main"]["temp"].ToString();
            string placename = resultObject["name"].ToString();
            string country = resultObject["sys"]["country"].ToString();
            weatherDescription = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(weatherDescription);

            weatherDescriptionTextView.Text = weatherDescription;
            placeTextView.Text = placename + ", " + country;
            temperatureTextView.Text = "Temperature: " + temperature;

            CloseProgressDialogue();

        }

        void ShowProgressDialogue(string status)
        {
            progressDialog = new ProgressDialogFragment(status);
            var trans = Activity.SupportFragmentManager.BeginTransaction();
            progressDialog.Cancelable = false;
            progressDialog.Show(trans, "Progress");
        }

        void CloseProgressDialogue()
        {
            if (progressDialog != null)
            {
                progressDialog.Dismiss();
                progressDialog = null;
            }
        }
    }
}