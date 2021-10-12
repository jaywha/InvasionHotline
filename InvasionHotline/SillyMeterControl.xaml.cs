using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InvasionHotline
{
    /// <summary>
    /// Interaction logic for SillyMeterControl.xaml
    /// </summary>
    public partial class SillyMeterControl : UserControl
    {
        public static readonly DependencyProperty SillynessProperty = DependencyProperty.Register("Sillyness", typeof(long), typeof(SillyMeterControl), new PropertyMetadata(0L));
        public static readonly DependencyProperty SillyStatusProperty = DependencyProperty.Register("SillyStatus", typeof(SillyMeterStatus), typeof(SillyMeterControl), new PropertyMetadata(SillyMeterStatus.Inactive));
        public static readonly DependencyProperty RewardsProperty = DependencyProperty.Register("Rewards", typeof(List<string>), typeof(SillyMeterControl), new PropertyMetadata(new List<string>() { "Reward" }));
        public static readonly DependencyProperty RewardDescriptionsProperty = DependencyProperty.Register("RewardDescriptions", typeof(List<string>), typeof(SillyMeterControl), new PropertyMetadata(new List<string>() { "RewardDesc" }));

        public long Sillyness
        {
            get => (long)GetValue(SillynessProperty);
            set => SetValue(SillynessProperty, value);
        }

        public SillyMeterStatus SillyStatus
        {
            get => (SillyMeterStatus)GetValue(SillyStatusProperty);
            set => SetValue(SillyStatusProperty, value);
        }

        public List<string> Rewards
        {
            get => (List<string>)GetValue(RewardsProperty);
            set => SetValue(RewardsProperty, value);
        }
        public List<string> RewardDescriptions
        {
            get => (List<string>)GetValue(RewardDescriptionsProperty);
            set => SetValue(RewardDescriptionsProperty, value);
        }


        public SillyMeterControl()
        {
            InitializeComponent();
        }

        public void Init()
        {
            stkRewards.Children.Clear();

            if (Rewards?.Count > 0)
            {
                var rewardPairs = Rewards.Zip(RewardDescriptions, (reward, desc) => reward + "|" + desc);

                foreach (string pair in rewardPairs)
                {
                    string r = pair.Split('|')[0];
                    string d = pair.Split('|')[1];
                    _ = stkRewards.Children.Add(new Label() { Content = r, ToolTip = d, FontFamily = new FontFamily("Mickey") });
                    _ = stkRewards.Children.Add(new Separator() { Height = 10.0, Visibility = Visibility.Hidden });
                }
            }
        }
    }

    public enum SillyMeterStatus
    {
        Inactive = -1,
        Active = 0,
        Reward = 1
    }
}
