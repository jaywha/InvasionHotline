using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for InvasionView.xaml
    /// </summary>
    public partial class InvasionView : UserControl
    {
        public static readonly DependencyProperty CogImageProperty = DependencyProperty.Register("CogImage", typeof(PackIconKind), typeof(InvasionView), new PropertyMetadata(PackIconKind.Gear));
        public static readonly DependencyProperty DistrictProperty = DependencyProperty.Register("District", typeof(string), typeof(InvasionView), new PropertyMetadata("Thwackville"));
        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(long), typeof(InvasionView), new PropertyMetadata(1583026777L));
        public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register("Progress", typeof(string), typeof(InvasionView), new PropertyMetadata("0/0"));
        public static readonly DependencyProperty CogNameProperty = DependencyProperty.Register("CogName", typeof(string), typeof(InvasionView), new PropertyMetadata("Penny Pincher"));

        public PackIconKind CogImage
        {
            get => (PackIconKind)GetValue(CogImageProperty);
            set { SetValue(CogImageProperty, value); }
        }

        public string CogName
        {
            get => (string)GetValue(CogNameProperty);
            set { SetValue(CogNameProperty, value); }
        }

        public string District {
            get => (string)GetValue(DistrictProperty);
            set { SetValue(DistrictProperty, value); }
        }

        public string Progress
        {
            get => (string)GetValue(ProgressProperty);
            set { SetValue(ProgressProperty, value); }
        }

        public long Time
        {
            get => (long)GetValue(TimeProperty);
            set { SetValue(TimeProperty, value); }
        }

        public InvasionView()
        {
            InitializeComponent();
        }
    }
}
