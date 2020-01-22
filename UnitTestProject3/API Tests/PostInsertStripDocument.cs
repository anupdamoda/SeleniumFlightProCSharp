using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Xml;
using System.Configuration;

namespace UnitTestProject3
{
    [TestClass]
    public class InsertStripDocument
    {
        [TestMethod]
        public void Post_InsertStripDocument()
        {

            var client = new RestClient(@ConfigurationManager.AppSettings["BaseURL"]);

            var request = new RestRequest("StripDocuments", Method.POST);
            request.AddHeader("Content-Type", @ConfigurationManager.AppSettings["Content-Type"]);
            request.AddHeader("X-ExternalRequest-ID", @ConfigurationManager.AppSettings["X-ExternalRequest-ID"]);
            request.AddHeader("X-ExternalSystem-ID", @ConfigurationManager.AppSettings["X-ExternalSystem-ID"]);
            request.AddHeader("X-Date", ConfigurationManager.AppSettings["X-Date"]);

            request.AddParameter("undefined", "<?xml version=\"1.0\"?>\n<StripDocuments>\n  <Data>T0MtV1NULUpBWU1JTg0KDQpJUDogMTAuMC4yLjgz</Data>\n  <FileName>TestDoc.doc</FileName>\n  <StripID>170000000153036</StripID>\n  <Title>Test Document 7</Title>\n  <Notes>Test Notes</Notes>\n</StripDocuments>", ParameterType.RequestBody);

            var response = client.Execute(request);
            var content = client.Execute(request).Content;

            Console.WriteLine("Content Length: " + content.Length);
            Console.WriteLine("Response Status: " + response.ResponseStatus);

            Console.WriteLine("Response Status Code: " + response.StatusCode);
            Console.WriteLine("Headers Count: " + response.Headers.Count);

            var doc = new XmlDocument();

            doc.LoadXml(response.Content);

            string DocumentID = doc.GetElementsByTagName("ID")[0].InnerText;

            //     Assert.IsNotNull(doc.GetElementsByTagName("CountryName")[1].InnerText);

            Console.WriteLine("Document ID: " + DocumentID);
        }
    }
}
