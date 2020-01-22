using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Xml;
using System.Configuration;

namespace UnitTestProject3
{
    [TestClass]
    public class GetCountries
    {
        [TestMethod]
        public void Get_Countries()
        {

            var client = new RestClient(@ConfigurationManager.AppSettings["BaseURL"]);

            var request = new RestRequest("countries", Method.GET);
            request.AddHeader("Content-Type", @ConfigurationManager.AppSettings["Content-Type"]);
            request.AddHeader("X-ExternalRequest-ID", @ConfigurationManager.AppSettings["X-ExternalRequest-ID"]);
            request.AddHeader("X-ExternalSystem-ID", @ConfigurationManager.AppSettings["X-ExternalSystem-ID"]);
            request.AddHeader("X-Date", ConfigurationManager.AppSettings["X-Date"]);
            request.AddHeader("X-AuthenticatedPerson", "d300a2c2b5394a13807eea513f40c0c2");
            request.AddHeader("X-Hash", "SmfOoxSL5UVKas5A76QzObzabt/WjovRcwisGej6Mion8ydnNo9NhsEk3STHmsQC1rAkyjwp71U2nR4RLm6dZ2FjYTljYjVmMDQ4ODQzOTM2ZjgyMmE5ZDJmZTY2ZTVh");
            request.AddHeader("Authorization", "Bearer eyJ0eXAiOiAiSldUIn0.eyAicGVvcGxlSWQiOiAiMSIsImV4cCI6IDE1NjQwMTMxNDcgfQ.fyiG9Pi2y/eEU368XvVmJ7OZ8q2eOwlSZL/7DoA69OEag9f583uGo4MzZhIe4nYJvY99kcby8ar1Xv1vJD0WZumTgu+YkuOFud645rCX672L76+X47q15K6K47SVxajmqLvnhZjqqZvmoYDihYTjhIPmmqXskrfripLvv73mtazni4bjjK7kt5Hvv73qm6Htgabki6Lvv73jtKjjvbjjgZ/grp/svZPkupDiv4Lipa3Sp+eJg+Oil+eNveWLt+uQveODv+W2vOShmuWHjOOavO6NmuKMge6SqOy8kueTlOO7ueKMs+Cng+ybh+6+tOuysuaAseqPouaknu6Nmue4sOezkeCnteOblOK0meWXjeWqieWLmw==");


            var response = client.Execute(request);
            var content = client.Execute(request).Content;

            Console.WriteLine("Content Length: " + content.Length);
            Console.WriteLine("Response Status: " + response.ResponseStatus);

            Console.WriteLine("Response Status Code: " + response.StatusCode);
            Console.WriteLine("Headers Count: " + response.Headers.Count);

            var doc = new XmlDocument();

            doc.LoadXml(response.Content);

            string Countryname = doc.GetElementsByTagName("CountryName")[1].InnerText;

            Assert.IsNotNull(doc.GetElementsByTagName("CountryName")[1].InnerText);

            Console.WriteLine("CountryName" + Countryname);



        }
    }
}
