using System;
using System.Collections.Generic;
using System.IO;
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
using System.Net;
using System.Net.Http;
using System.Web.Script.Serialization;


namespace ProtoType
{
    /// <summary>
    /// Interaction logic for MainDockpaneView.xaml
    /// </summary>
    public partial class MainDockpaneView : UserControl
    {

        // this is copied from Cyrus Authetication Code
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        static readonly HttpClient client = new HttpClient();
        private async Task GetInstances()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(System.IO.Path.Combine(InstanceURL.Text, "search/resources"));
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(System.IO.Path.Combine(InstanceURL.Text, "search/resources"));

                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var serializer = new JavaScriptSerializer();
                dynamic responseJSON = serializer.Deserialize<dynamic>(@responseBody);
                JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                dynamic results = responseJSON["results"]["hits"]["hits"];
                int count = 0;
                string names = "";
                foreach (dynamic element in results)
                {
                    count++;
                    string displayname = element["_source"]["displayname"];
                    names += $"{count}. {displayname} \n";
                }
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show($"{count} Instances:\n{names}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                System.ArgumentException argEx = new System.ArgumentException("Addres is wrong", e);
                throw argEx;
            }
        }

        private async Task<string> GetClientId()
        {
            string clientid = "";
            try
            {
                var serializer = new JavaScriptSerializer();
                var stringContent = new FormUrlEncodedContent(new[]
                    {
                            new KeyValuePair<string, string>("username", Username.Text),
                            new KeyValuePair<string, string>("password", Password.Password),
                        });
                //var response = await client.PostAsync("http://qa.archesproject.org/auth/get_client_id", stringContent);
                var response = await client.PostAsync(System.IO.Path.Combine(InstanceURL.Text, "auth/get_client_id"), stringContent);
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(System.IO.Path.Combine(InstanceURL.Text, "auth/get_client_id"));

                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                dynamic responseJSON = serializer.Deserialize<dynamic>(@responseBody);
                dynamic results = responseJSON;
                clientid = results["clientid"];
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return clientid;
        }

        private async Task<Dictionary<string, string>> GetToken(string clientid)
        {
            Dictionary<String, String> result = new Dictionary<String, String>();
            try
            {
                var serializer = new JavaScriptSerializer();
                var stringContent = new FormUrlEncodedContent(new[]
                    {
                            /*new KeyValuePair<string, string>("username", "admin"),
                            new KeyValuePair<string, string>("password", "admin"),*/
                            new KeyValuePair<string, string>("username", Username.Text),
                            new KeyValuePair<string, string>("password", Password.Password),
                            new KeyValuePair<string, string>("client_id", clientid),
                            new KeyValuePair<string, string>("grant_type", "password"),
                        });
                //var response = await client.PostAsync("http://qa.archesproject.org/o/token/", stringContent);
                var response = await client.PostAsync(System.IO.Path.Combine(InstanceURL.Text, "o/token/"), stringContent);

                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                dynamic responseJSON = serializer.Deserialize<dynamic>(@responseBody);
                dynamic results = responseJSON;
                result.Add("access_token", results["access_token"]);
                result.Add("refresh_token", results["refresh_token"]);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return result;
        }

        private async Task<Dictionary<string, string>> GetResource(string resourceid, string token)
        {
            Dictionary<String, String> result = new Dictionary<String, String>();
            try
            {
                var serializer = new JavaScriptSerializer();
                string header = "Bearer " + token;
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", header);
                }
                catch (System.FormatException e)
                {
                    Console.WriteLine("Message :{0} ", e.Message);
                }
                //var response = await client.GetAsync($"http://qa.archesproject.org/resources/{resourceid}?format=json");
                var response = await client.GetAsync(InstanceURL.Text + $"resources/{resourceid}?format=json");
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show($"http://qa.archesproject.org/resources/{resourceid}?format=json");
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(InstanceURL.Text + $"resources/{resourceid}?format=json");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                dynamic responseJSON = serializer.Deserialize<dynamic>(@responseBody);
                result.Add("resourceid", responseJSON["resourceinstanceid"]);
                result.Add("graphid", responseJSON["graph_id"]);
                result.Add("displayname", responseJSON["displayname"]);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return result;
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            /*try
            {*/
                await GetInstances();
                string clientid = await GetClientId();
                Dictionary<string, string> tokendata = await GetToken(clientid);
                //Dictionary<string, string> resource = await GetResource("28ef157a-5706-11ea-93c2-025323b7202a", tokendata["access_token"]);
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show($"" +
                    $"clientid: {clientid} " +
                    $"\naccess token: {tokendata["access_token"]} " +
                    $"\nrefresh token: {tokendata["refresh_token"]} " /*+
                $"\ngraph: {resource["graphid"]}" +
                $"\nresource: {resource["resourceid"]}" +
                $"\nresource name: {resource["displayname"]}"*/);
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("You are connecting " + InstanceURL.Text + " using credentials " + Username.Text + "/" + Password.Password);

            //}
            /* catch (Exception e) {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(e.Message);
            }*/
        }

        /// end of Cyrus Authentication Code

        public MainDockpaneView()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /*
        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
        */
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            InstanceURL.Text = "";
            Username.Text = "";
            Password.Password = "";
        }
    }
}
