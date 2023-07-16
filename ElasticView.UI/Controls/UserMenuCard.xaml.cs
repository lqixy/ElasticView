using ElasticView.UI.Controllers;
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

namespace ElasticView.UI.Controls
{
    /// <summary>
    /// UserMenuCard.xaml 的交互逻辑
    /// </summary>
    public partial class UserMenuCard : UserControl
    {
        public UserMenuCard()
        {
            InitializeComponent();
        }


        public string Total
        {
            get { return (string)GetValue(TotalProperty); }
            set { SetValue(TotalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Total.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalProperty =
            DependencyProperty.Register("Total", typeof(string), typeof(UserMenuCard), new PropertyMetadata("0"));



        //public SolidColorBrush BackgrondColor
        //{
        //    get { return (SolidColorBrush)GetValue(BackgrondColorProperty); }
        //    set { SetValue(BackgrondColorProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for BackgrondColor.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty BackgrondColorProperty =
        //    DependencyProperty.Register("BackgrondColor", typeof(SolidColorBrush), typeof(UserMenuCard));



        //public SolidColorBrush ForegrondColor
        //{
        //    get { return (SolidColorBrush)GetValue(ForegrondColorProperty); }
        //    set { SetValue(ForegrondColorProperty, value); }
        //}

        // Using a DependencyProperty as the backing store for ForegrondColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForegrondColorProperty =
            DependencyProperty.Register("ForegrondColor", typeof(SolidColorBrush), typeof(UserMenuCard));



        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Description.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(UserMenuCard));





        //public PackIcon ImageName
        //{
        //    get { return (PackIcon)GetValue(ImageNameProperty); }
        //    set { SetValue(ImageNameProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for ImageName.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ImageNameProperty =
        //    DependencyProperty.Register("ImageName", typeof(PackIcon), typeof(UserMenuCard));



        public PackIconKind IconKind
        {
            get { return (PackIconKind)GetValue(IconKindProperty); }
            set { SetValue(IconKindProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconKind.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconKindProperty =
            DependencyProperty.Register("IconKind", typeof(PackIconKind), typeof(UserMenuCard));



    }
}
