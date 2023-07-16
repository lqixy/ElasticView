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
using System.Windows.Shapes;

namespace ElasticView.UI.Windows
{
    /// <summary>
    /// IndexInfo.xaml 的交互逻辑
    /// </summary>
    public partial class IndexInfo : Window
    {
        public IndexInfo(string title, string indexName, string content)
        {
            InitializeComponent();

            IndicesTitleLabel.Text = title;
            IndicesNameLabel.Text = indexName;
            var textRange = new TextRange(IndicesContent.Document.ContentStart, IndicesContent.Document.ContentEnd);
            textRange.Text = content;
             
        }
         
        private void CopyBtn_Click(object sender, RoutedEventArgs e)
        {
            var textRange = new TextRange(IndicesContent.Document.ContentStart, IndicesContent.Document.ContentEnd);
            Clipboard.SetDataObject(textRange.Text);
        }
    }
}
