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
    public partial class DistrictView : UserControl
    {
        public static readonly DependencyProperty PopulationProperty = DependencyProperty.Register("Population", typeof(int), typeof(DistrictView), new PropertyMetadata(0));
        public static readonly DependencyProperty DistrictNameProperty = DependencyProperty.Register("DistrictName", typeof(string), typeof(DistrictView), new PropertyMetadata("Thwackville"));
        public static readonly DependencyProperty PopColorProperty = DependencyProperty.Register("PopColor", typeof(Brush), typeof(DistrictView), new PropertyMetadata(Brushes.WhiteSmoke));

        public int Population
        {
            get => (int)GetValue(PopulationProperty);
            set { SetValue(PopulationProperty, value); }
        }

        public string DistrictName {
            get => (string)GetValue(DistrictNameProperty);
            set { SetValue(DistrictNameProperty, value); }
        }

        public Brush PopColor
        {
            get => (Brush)GetValue(PopColorProperty);
            set { SetValue(PopulationProperty, value); }
        }

        public DistrictView()
        {
            InitializeComponent();
        }
    }
}
