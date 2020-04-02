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


namespace ProtoType
{
    /// <summary>
    /// Interaction logic for ResourceModelDockpaneView.xaml
    /// </summary>
    public partial class ResourceModelDockpaneView : UserControl
    {
        public ResourceModelDockpaneView()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show($"" +
                $"clientid: {StaticVariables.myClientid} " +
                $"\naccess token: {StaticVariables.myToken["access_token"]} " +
                $"\nrefresh token: {StaticVariables.myToken["refresh_token"]} " /*+
                $"\ngraph: {resource["graphid"]}" +
                $"\nresource: {resource["resourceid"]}" +
                $"\nresource name: {resource["displayname"]}"*/);

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
