using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using RestSharp;

namespace FPAPIAutomation_MSTests.TestCases.ULToolAPI
{
    [TestClass]
    public class GetAssets
    {

        [TestMethod]
        public void GetAssets_api()
        {

            var client = new RestClient();
            client.BaseUrl = new Uri("http://oc-svr-at1/Fltpro_Automation_main/API/v1");

            

            var request = new RestRequest();
            request.Resource = "";



            IRestResponse response = client.Execute(request);





        }


    }
}
