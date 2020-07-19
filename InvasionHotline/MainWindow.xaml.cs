﻿using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;

namespace InvasionHotline
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region PropertyChange Impl
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        #endregion

        public DispatcherTimer InvasionPoller = new DispatcherTimer();
        public DispatcherTimer DistrictPoller = new DispatcherTimer();

        private ObservableCollection<Invasion> _invasions = new ObservableCollection<Invasion>();
        public ObservableCollection<Invasion> Invasions {
            get => _invasions;
            set {
                _invasions = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<District> _districts = new ObservableCollection<District>();
        public ObservableCollection<District> Districts {
            get => _districts;
            set {
                _districts = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Bossbots { get; set; } = new ObservableCollection<string>() {
            "Flunky",
            "Pencil Pusher",
            "Yesman",
            "Micromanager",
            "Micromanager",
            "Downsizer",
            "Head Hunter",
            "Corporate Raider",
            "The Big Cheese"
        };

        public ObservableCollection<string> Lawbots { get; set; } = new ObservableCollection<string>()
        {
            "Bottom Feeder",
            "Bloodsucker",
            "Bloodsucker",
            "Double Talker",
            "Ambulance Chaser",
            "Back Stabber",
            "Spin Doctor",
            "Legal Eagle",
            "Big Wig"
        };

        public ObservableCollection<string> Cashbots { get; set; } = new ObservableCollection<string>()
        {
            "Short Change",
            "Penny Pincher",
            "Tightwad",
            "Bean Counter",
            "Number Cruncher",
            "Money Bags",
            "Loan Shark",
            "Robber Baron"
        };

        public ObservableCollection<string> Sellbots { get; set; } = new ObservableCollection<string>()
        {
            "Cold Caller",
            "Telemarketer",
            "Name Dropper",
            "Glad Hander",
            "Mover & Shaker",
            "Two-Face",
            "The Mingler",
            "Mr. Hollywood"
        };

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
            // Console.WriteLine(jsonResult.ToString());
            foreach (var apiDistrct in jsonResult.populationByDistrict)
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
            // Console.WriteLine(jsonResult.ToString());
            foreach (JObject apiInvasion in jsonResult.invasions)
            {
                var invData = JsonConvert.DeserializeObject<ToonHQInvasionData>(apiInvasion.ToString());
                var maxNumberOfCogs = Convert.ToDouble(invData.total);
                var tickTime = new TimeSpan(0, 0, (int)((invData.total - invData.defeated) / invData.defeat_rate));
                var timeStarted = new DateTime(invData.start_time).ToLocalTime();
                var endTime = timeStarted.Add(tickTime);
                var durationTime = DateTime.Now.Subtract(endTime);


                var newInvasion = new Invasion()
                {
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
            // Console.WriteLine(cogName);
            switch (cogName.Trim())
            {
                // All Bossbots
                case "Flunky":
                case "Pencil Pusher":
                case "Yesman":
                case "Micromanager":
                case "Micromanager":
                case "Downsizer":
                case "Head Hunter":
                case "Corporate Raider":
                case "The Big Cheese":
                    return PackIconKind.Tie;
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

        private void grdspltColumns_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            colInvasions.Width = new GridLength(50, GridUnitType.Star);
            colDistricts.Width = new GridLength(50, GridUnitType.Star);
        }

        private bool ToggleFilter = false;
        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            CollectionViewSource filterSource = new CollectionViewSource() { 
                IsLiveFilteringRequested = true
            };
            filterSource.Source = Invasions;            

            Console.WriteLine("=== Filter Options ===");

            Console.WriteLine("Bossbots {\n");
            foreach (string value in stkBossbots.SelectedItems)
            {
                Console.WriteLine("\t"+value);
            }
            Console.WriteLine("}\n");

            Console.WriteLine("Lawbots {\n");
            foreach (string value in stkLawbots.SelectedItems)
            {
                Console.WriteLine("\t" + value);
            }
            Console.WriteLine("}\n");

            Console.WriteLine("Cashbots {\n");
            foreach (string value in stkCashbots.SelectedItems)
            {
                Console.WriteLine("\t" + value);
            }
            Console.WriteLine("}\n");

            Console.WriteLine("Sellbots {\n");
            foreach (string value in stkSellbots.SelectedItems)
            {
                Console.WriteLine("\t" + value);
            }
            Console.WriteLine("}\n");

            Console.WriteLine("=====================");

            filterSource.Filter += (object filterSender, FilterEventArgs filterArgs) => {
                Invasion invasion = filterArgs.Item as Invasion;
                string cogName = invasion.Cog;

                if (stkBossbots.SelectedItems.Count + stkLawbots.SelectedItems.Count + stkCashbots.SelectedItems.Count + stkSellbots.SelectedItems.Count == 0)
                {
                    filterArgs.Accepted = true;
                    return;
                }

                if(stkBossbots.SelectedItems.Contains(cogName) || stkLawbots.SelectedItems.Contains(cogName) || stkCashbots.SelectedItems.Contains(cogName) || stkSellbots.SelectedItems.Contains(cogName))
                {
                    filterArgs.Accepted = true;
                } else
                {
                    filterArgs.Accepted = false;
                }

                ToggleFilter = !ToggleFilter;
                if (ToggleFilter)
                {
                    btnFilterInvasions.IsPopupOpen = false;
                    btnFilterInvasions.Foreground = Brushes.Green;
                } else
                {
                    btnFilterInvasions.Foreground = Brushes.Black;
                }
            };
        }
    }
}