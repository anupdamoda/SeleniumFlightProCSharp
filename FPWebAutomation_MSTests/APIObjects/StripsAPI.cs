﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Configuration;
using System.Xml;
using FPWebAutomation_MSTests.Database;
using UnitTestProject3.Database;

namespace FPWebAutomation_MSTests.APIObjects
{
    [TestClass]
    public class InsertStrips
    {
        string StripID = null;
        String strtblname = "automation_strips";
        String strTestType = "Regression";
        String strTestCaseNo = "TC001";


        [TestMethod]
        public void Insert_Strip()
        {

            var connection = new ConnectToMySQL_Fetch_TestData();
            var testdataStrip = connection.Select(strtblname, strTestCaseNo, strTestType);

            var client = new RestClient(@ConfigurationManager.AppSettings["BaseURL"]);

            var request = new RestRequest("Strips/", Method.POST);
            request.AddHeader("Content-Type", @ConfigurationManager.AppSettings["Content-Type"]);
            request.AddHeader("X-ExternalRequest-ID", @ConfigurationManager.AppSettings["X-ExternalRequest-ID"]);
            request.AddHeader("X-ExternalSystem-ID", @ConfigurationManager.AppSettings["X-ExternalSystem-ID"]);
            request.AddHeader("X-Date", @ConfigurationManager.AppSettings["X-Date"]);
            request.AddParameter("undefined", "<?xml version=\"1.0\"?>\r\n<Strip>\r\n  <AssetRegistration>"+testdataStrip[4]+"</AssetRegistration>\r\n  <AssetTypeID>170000000000001</AssetTypeID>\r\n  <Callsign>"+testdataStrip[6]+ "</Callsign>\r\n  <LocationFromID>170000000000003</LocationFromID>\r\n  <LocationToID>170000000000004</LocationToID>\r\n  <PaneID>170000000000003</PaneID>\r\n  <People>\r\n    <StripPerson>\r\n      <PersonID>170000000000017</PersonID>\r\n      <SlotNumber>1</SlotNumber>\r\n    </StripPerson>\r\n    <StripPerson>\r\n      <PersonID>170000000000016</PersonID>\r\n      <SlotNumber>2</SlotNumber>\r\n    </StripPerson>\r\n  </People>\r\n  <PlannedEndTime>2019-08-21T03:15:00</PlannedEndTime>\r\n  <PlannedStartTime>2019-08-21T01:30:00</PlannedStartTime>\r\n  <Task>"+testdataStrip[12]+"</Task>\r\n  <Type>Mission</Type>\r\n</Strip>\r\n", ParameterType.RequestBody);

            var response = client.Execute(request);
            var content = client.Execute(request).Content;

            Console.WriteLine("Content Length: " + content.Length);
            Console.WriteLine("Response Status: " + response.ResponseStatus);

            Console.WriteLine("Response Status Code: " + response.StatusCode);
            Console.WriteLine("Headers Count: " + response.Headers.Count);

            var doc = new XmlDocument();

            doc.LoadXml(response.Content);

            StripID = doc.GetElementsByTagName("ID")[2].InnerText;

            Console.WriteLine("StripID: " + StripID);

            Assert.IsNotNull(doc.GetElementsByTagName("ID")[2].InnerText);

            var connection1 = new ConnectToMySQL_Update_TestData();
            connection1.Update(strtblname,"StripID",StripID,"TC001");



        }


        [TestMethod]
        public void Delete_Strip()
        {

            var connection = new ConnectToMySQL_Fetch_TestData();
            var testdataStrip = connection.Select(strtblname, strTestCaseNo, strTestType);

            var client = new RestClient(@ConfigurationManager.AppSettings["BaseURL"]);

            var request = new RestRequest("strips/" + testdataStrip[14], Method.DELETE);
            request.AddHeader("Content-Type", @ConfigurationManager.AppSettings["Content-Type"]);
            request.AddHeader("X-ExternalRequest-ID", @ConfigurationManager.AppSettings["X-ExternalRequest-ID"]);
            request.AddHeader("X-ExternalSystem-ID", @ConfigurationManager.AppSettings["X-ExternalSystem-ID"]);
            request.AddHeader("X-Date", ConfigurationManager.AppSettings["X-Date"]);

            var response = client.Execute(request);
            var content = client.Execute(request).Content;

            var doc = new XmlDocument();

            doc.LoadXml(response.Content);







        }


    }
}

