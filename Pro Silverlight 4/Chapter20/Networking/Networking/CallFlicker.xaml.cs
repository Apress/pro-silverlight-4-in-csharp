using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Net;
using System.Xml.Linq;
using System.Windows.Media.Imaging;
using System.IO;
using System.Xml;
using System.Windows.Browser;

namespace Networking
{
    public partial class CallFlicker : UserControl
    {
        public CallFlicker()
        {
            InitializeComponent();                       
        }

	// NOTE: FOR THESE EXAMPLES TO WORK, YOU MUST SIGN UP FOR YOUR OWN API KEY.
	// Enter the API key in the places where you find the ... in the code below.

        private void cmdGetDataRest_Click(object sender, RoutedEventArgs e)
        {
            WebClient client = new WebClient();
            Uri address = new Uri(
                "http://api.flickr.com/services/rest/?" + "method=flickr.photos.search" +
                "&tags=" + HttpUtility.UrlEncode(txtSearchKeyword.Text) + "&api_key=..." +
                "&perpage=10");

            client.DownloadStringCompleted += client_DownloadStringCompleted;
            client.DownloadStringAsync(address);
        }

        private void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {               
            XDocument document = XDocument.Parse(e.Result);

            // Approach 1 (dynamic control creation).
            if (images.ItemsSource != null) images.ItemsSource = null;
            images.Items.Clear();
            images.ItemTemplate = null;
            foreach (XElement element in document.Descendants("photo"))
            {                
                    string imageUrl = String.Format(
                        "http://farm{0}.static.flickr.com/{1}/{2}_{3}_m.jpg",
                        (string)element.Attribute("farm"),
                        (string)element.Attribute("server"),
                        (string)element.Attribute("id"),
                        (string)element.Attribute("secret")
                        );
                    Image img = new Image();
                    img.Stretch = Stretch.Uniform;
                    img.Width = 200; img.Height = 200;
                    img.Margin = new Thickness(10);
                    img.Source = new BitmapImage(new Uri(imageUrl));
                    images.Items.Add(img);
            }            
            return;

            // Approach 2 (LINQ to XML).
            var photos = from results in document.Descendants("photo")
                         select new FlickrImage
                         {
                             
                             imageUrl =
                             String.Format(
                    "http://farm{0}.static.flickr.com/{1}/{2}_{3}_m.jpg",
                    results.Attribute("farm").Value.ToString(), results.Attribute("server").Value.ToString(),
                    results.Attribute("id").Value.ToString(), results.Attribute("secret").Value.ToString())
                         };
            images.ItemTemplate = (DataTemplate)this.Resources["imageTemplate"];
            images.ItemsSource = photos;
         
        }

        private void cmdGetDataXmlHttp_Click(object sender, RoutedEventArgs e)
        {            
            Uri address = new Uri("http://api.flickr.com/services/xmlrpc/");
            WebRequest request = WebRequest.Create(address);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            
            // Prepare the request asynchronously.
            request.BeginGetRequestStream(CreateRequest, request);           
        }

        private void CreateRequest(IAsyncResult asyncResult)
        {                       
            WebRequest request = (WebRequest)asyncResult.AsyncState;

            XDocument document = null;
            Dispatcher.BeginInvoke(
                delegate()
                {
                    document = new XDocument(
                        new XElement("methodCall",
                            new XElement("methodName", "flickr.photos.search"),
                            new XElement("params",
                                new XElement("param",
                                    new XElement("value",
                                        new XElement("struct",
                                            new XElement("member",
                                                new XElement("name", "tags"),
                                                new XElement("value",
                                                    new XElement("string", HttpUtility.HtmlEncode(txtSearchKeyword.Text))
                                                )
                                            ),
                                            new XElement("member",
                                                new XElement("name", "api_key"),
                                                new XElement("value",
                                                    new XElement("string", "...")
                                                )
                                            ),
                                            new XElement("member",
                                                new XElement("name", "perpage"),
                                                new XElement("value",
                                                    new XElement("string", "10")
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    );
                });
                        
            Stream requestStream = request.EndGetRequestStream(asyncResult);
            StreamWriter writer = new StreamWriter(requestStream);            
            writer.Write(document.ToString());

 /*           writer.Write(@"<methodCall>
	<methodName>flickr.photos.search</methodName>
	<params>
		<param>
			<value>
				<struct>
					<member>
						<name>tags</name>
						<value><string>" + HttpUtility.HtmlEncode(txtSearchKeyword.Text) + @"</string></value>
					</member>
					<member>
						<name>api_key</name>
						<value><string>...</string></value>
					</member>
					<member>
						<name>perpage</name>
						<value><string>10</string></value>
					</member>

				</struct>
			</value>
		</param>
	</params>
</methodCall>"); */

            writer.Close();
            requestStream.Close();

            // Read the response asynchronously.
            request.BeginGetResponse(ReadResponse, request);
        }

        private void ReadResponse(IAsyncResult asyncResult)
        {
            WebRequest request = (WebRequest)asyncResult.AsyncState;

            // Get the respone stream.
            WebResponse response = request.EndGetResponse(asyncResult);
            Stream responseStream = response.GetResponseStream();


            // Read the returned text.
            StreamReader reader = new StreamReader(responseStream);
            string responseText = HttpUtility.HtmlDecode(reader.ReadToEnd());
            response.Close();
            XDocument document = XDocument.Parse(responseText);

            var photos = from results in document.Descendants("photo")
                         select new FlickrImage
                         {

                             imageUrl =
                             String.Format(
                    "http://farm{0}.static.flickr.com/{1}/{2}_{3}_m.jpg",
                    results.Attribute("farm").Value.ToString(), results.Attribute("server").Value.ToString(),
                    results.Attribute("id").Value.ToString(), results.Attribute("secret").Value.ToString())
                         };

            // Update the display.
            Dispatcher.BeginInvoke(
                delegate()
                {
                    if (images.ItemsSource != null) images.ItemsSource = null;                    
                    images.Items.Clear();
                    images.ItemTemplate = (DataTemplate)this.Resources["imageTemplate"];
                    images.ItemsSource = photos;
                });            
            
        }
            
            
    }

    public class FlickrImage
    {
        public string imageUrl { get; set; }
    }


}
