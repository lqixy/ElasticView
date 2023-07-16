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
    /// UserTable.xaml 的交互逻辑
    /// </summary>
    public partial class UserTable : UserControl
    {
        public UserTable()
        {
            InitializeComponent();
        }



        public string Row0Key
        {
            get { return (string)GetValue(Row0KeyProperty); }
            set { SetValue(Row0KeyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Row0Key.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Row0KeyProperty =
            DependencyProperty.Register("Row0Key", typeof(string), typeof(UserTable));



        public string Row0Value
        {
            get { return (string)GetValue(Row0ValueProperty); }
            set { SetValue(Row0ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Row0Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Row0ValueProperty =
            DependencyProperty.Register("Row0Value", typeof(string), typeof(UserTable));


        public string Row1Key
        {
            get { return (string)GetValue(Row1KeyProperty); }
            set { SetValue(Row1KeyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Row1Key.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Row1KeyProperty =
            DependencyProperty.Register("Row1Key", typeof(string), typeof(UserTable));



        public string Row1Value
        {
            get { return (string)GetValue(Row1ValueProperty); }
            set { SetValue(Row1ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Row1Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Row1ValueProperty =
            DependencyProperty.Register("Row1Value", typeof(string), typeof(UserTable));

        public string Row2Key
        {
            get { return (string)GetValue(Row2KeyProperty); }
            set { SetValue(Row2KeyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Row2Key.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Row2KeyProperty =
            DependencyProperty.Register("Row2Key", typeof(string), typeof(UserTable));



        public string Row2Value
        {
            get { return (string)GetValue(Row2ValueProperty); }
            set { SetValue(Row2ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Row2Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Row2ValueProperty =
            DependencyProperty.Register("Row2Value", typeof(string), typeof(UserTable));

        public string Row3Key
        {
            get { return (string)GetValue(Row3KeyProperty); }
            set { SetValue(Row3KeyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Row3Key.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Row3KeyProperty =
            DependencyProperty.Register("Row3Key", typeof(string), typeof(UserTable));



        public string Row3Value
        {
            get { return (string)GetValue(Row3ValueProperty); }
            set { SetValue(Row3ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Row3Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Row3ValueProperty =
            DependencyProperty.Register("Row3Value", typeof(string), typeof(UserTable));

        public string Row4Key
        {
            get { return (string)GetValue(Row4KeyProperty); }
            set { SetValue(Row4KeyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Row4Key.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Row4KeyProperty =
            DependencyProperty.Register("Row4Key", typeof(string), typeof(UserTable));



        public string Row4Value
        {
            get { return (string)GetValue(Row4ValueProperty); }
            set { SetValue(Row4ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Row4Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Row4ValueProperty =
            DependencyProperty.Register("Row4Value", typeof(string), typeof(UserTable));

        public string Row5Key
        {
            get { return (string)GetValue(Row5KeyProperty); }
            set { SetValue(Row5KeyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Row5Key.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Row5KeyProperty =
            DependencyProperty.Register("Row5Key", typeof(string), typeof(UserTable));



        public string Row5Value
        {
            get { return (string)GetValue(Row5ValueProperty); }
            set { SetValue(Row5ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Row5Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Row5ValueProperty =
            DependencyProperty.Register("Row5Value", typeof(string), typeof(UserTable));

        public string Row6Key
        {
            get { return (string)GetValue(Row6KeyProperty); }
            set { SetValue(Row6KeyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Row6Key.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Row6KeyProperty =
            DependencyProperty.Register("Row6Key", typeof(string), typeof(UserTable));



        public string Row6Value
        {
            get { return (string)GetValue(Row6ValueProperty); }
            set { SetValue(Row6ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Row6Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Row6ValueProperty =
            DependencyProperty.Register("Row6Value", typeof(string), typeof(UserTable));

        public string Row7Key
        {
            get { return (string)GetValue(Row7KeyProperty); }
            set { SetValue(Row7KeyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Row7Key.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Row7KeyProperty =
            DependencyProperty.Register("Row7Key", typeof(string), typeof(UserTable));



        public string Row7Value
        {
            get { return (string)GetValue(Row7ValueProperty); }
            set { SetValue(Row7ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Row7Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Row7ValueProperty =
            DependencyProperty.Register("Row7Value", typeof(string), typeof(UserTable));


    }
}
