using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

namespace InvasionHotline
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer InvasionPoller = new DispatcherTimer();
        public DispatcherTimer DistrictPoller = new DispatcherTimer();
        public ObservableCollection<Invasion> Invasions { get; set; }  = new ObservableCollection<Invasion>();

        public ObservableCollection<District> Districts { get; set; } = new ObservableCollection<District>();

        public MainWindow()
        {
            InitializeComponent();

            InvasionPoller.Interval = TimeSpan.FromSeconds(3);
            InvasionPoller.Tick += InvasionPoller_Tick;
            InvasionPoller.Start();

            DistrictPoller.Interval = TimeSpan.FromSeconds(4);
            DistrictPoller.Tick += DistrictPoller_Tick;
            DistrictPoller.Start();
        }

        private void DistrictPoller_Tick(object sender, EventArgs e)
        {
            Districts.Clear();
            var client = new RestClient("https://www.toontownrewritten.com/");
            var request = new RestRequest("api/population", RestSharp.DataFormat.Json);
            var response = client.Get(request);

            var jsonResult = JsonConvert.DeserializeObject<DistrictRequest>(response.Content);
            if (jsonResult == null || string.IsNullOrWhiteSpace(jsonResult.ToString())) return;
            Console.WriteLine(jsonResult.ToString());
            foreach(var apiDistrct in jsonResult.populationByDistrict)
            {
                var newDistrict = new District()
                {
                    Name = apiDistrct.Key,
                    Population = int.Parse(apiDistrct.Value.ToString())
                };

                Districts.Add(newDistrict);
            }
        }

        private void InvasionPoller_Tick(object sender, EventArgs e)
        {
            Invasions.Clear();
            var client = new RestClient("https://toonhq.org/");
            var request = new RestRequest("api/v1/invasion", RestSharp.DataFormat.Json);
            var response = client.Get(request);

            var jsonResult = JsonConvert.DeserializeObject<ToonHQInvasionRequest>(response.Content);
            if (jsonResult == null || jsonResult.invasions == null) return;
            Console.WriteLine(jsonResult.ToString());
            foreach(JObject apiInvasion in jsonResult.invasions)
            {
                var invData = JsonConvert.DeserializeObject<ToonHQInvasionData>(apiInvasion.ToString());
                var maxNumberOfCogs = Convert.ToDouble(invData.total);
                var tickTime = new TimeSpan(0, 0, (int) ((invData.total - invData.defeated) / invData.defeat_rate));
                var timeStarted = new DateTime(invData.start_time).ToLocalTime();
                var endTime = timeStarted.Add(tickTime);
                var durationTime = DateTime.Now.Subtract(endTime);


                var newInvasion = new Invasion() {
                    District = invData.district,
                    Cog = invData.cog,
                    Ticks = durationTime.Ticks,
                    Progress = $"{invData.defeated}/{invData.total}",
                    CogLogo = DetermineLogoBasedOn(invData.cog)
                };

                Invasions.Add(newInvasion);
            }
        }

        private PackIconKind DetermineLogoBasedOn(string cogName)
        {
            Console.WriteLine(cogName);
            switch(cogName.Trim())
            {
                // All Cashbots
                case "Short Change":
                case "Penny Pincher":
                case "Tightwad":
                case "Bean Counter":
                case "Number Cruncher":
                case "Money Bags":
                case "Loan Shark":
                case "Robber Baron":
                    return PackIconKind.CashUsdOutline;
                // All Lawbots
                case "Bottom Feeder":
                case "Bloodsucker":
                case "Bloodsucker": // only for this one but, why?
                case "Double Talker":
                case "Ambulance Chaser":
                case "Back Stabber":
                case "Spin Doctor":
                case "Legal Eagle":
                case "Big Wig":
                    return PackIconKind.Gavel;
                // All Bossbots
                case "Flunky":
                case "Pencil Pusher":
                case "Yesman":
                case "Micromanager":
                case "Downsizer":
                case "Head Hunter":
                case "Corporate Raider":
                case "The Big Cheese":
                    return PackIconKind.Tie;
                // All Sellbots
                case "Cold Caller":
                case "Telemarketer":
                case "Name Dropper":
                case "Glad Hander":
                case "Mover & Shaker":
                case "Two-Face":
                case "The Mingler":
                case "Mr. Hollywood":
                    return PackIconKind.ChartBar;
                default:
                    return PackIconKind.Gear;
            }
        }
    }
}
