using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FPWebAutomation_MSTests.API
{
    public class Strips
    {
        public String  InsertMissionStrip(string strTDAssetTypeID, string strTDPaneID, string strTDPersonID, string strTDSlotNumber, string strTDWeatherStateID, string Details, string strPlannedStartTime, string strPlannedEndTime)
        {
            Int64 AssetTypeID = Convert.ToInt64(strTDAssetTypeID);
            Int64 PaneID = Convert.ToInt64(strTDPaneID);            
            Int64 PersonID = Convert.ToInt64(strTDPersonID);
            int SlotNumber = Convert.ToInt16(strTDSlotNumber);
            Int64 WeatherStateID = Convert.ToInt64(strTDWeatherStateID);
            string Type = "Mission";

            var client = new RestClient(@ConfigurationManager.AppSettings["FACT_API_URL"]);

            var request = new RestRequest("Strips/", Method.POST);
            request.AddHeader("Content-Type", @ConfigurationManager.AppSettings["Content-Type"]);
            request.AddHeader("X-ExternalRequest-ID", @ConfigurationManager.AppSettings["X-ExternalRequest-ID"]);
            request.AddHeader("X-ExternalSystem-ID", @ConfigurationManager.AppSettings["X-ExternalSystem-ID"]);
            request.AddHeader("X-Date", @ConfigurationManager.AppSettings["X-Date"]);
            request.AddParameter("undefined", "<?xml version=\"1.0\"?>\r\n<Strip>\r\n  <AssetTypeID>" + AssetTypeID + "</AssetTypeID>\r\n  <PaneID>" + PaneID + "</PaneID>\r\n  <MinimumWeatherStateID>" + WeatherStateID + "</MinimumWeatherStateID>\r\n    <MaximumWeatherStateID>" + WeatherStateID + "</MaximumWeatherStateID>\r\n  <Details>" + Details + "</Details>\r\n    <People>\r\n    <StripPerson>\r\n      <PersonID>" + PersonID + "</PersonID>\r\n      <SlotNumber>" + SlotNumber + "</SlotNumber>\r\n     </StripPerson>\r\n  </People>\r\n  <PlannedEndTime>" + strPlannedEndTime + "</PlannedEndTime>\r\n  <PlannedStartTime>" + strPlannedStartTime + "</PlannedStartTime>\r\n  <Type>" + Type + "</Type>\r\n</Strip>\r\n", ParameterType.RequestBody);

            var response = client.Execute(request);
            Console.WriteLine("Response Status Code: " + response.StatusCode);

            HttpStatusCode statusCode = response.StatusCode;
            int intRespCode = (int)statusCode;

            if (intRespCode == 200)
            {
                var doc = new XmlDocument();

                doc.LoadXml(response.Content);

                String StripID = doc.GetElementsByTagName("ID")[2].InnerText;

                Console.WriteLine("StripID: " + StripID);

                return StripID;
            }
            else
            {
                return "";
            }

        }

        public String InsertTaskStrip(string strTDAssetTypeID, string strTDPaneID, string strTDPersonID, string strTDSlotNumber,  string Details, string strPlannedStartTime, string strPlannedEndTime)
        {
            Int64 AssetTypeID = Convert.ToInt64(strTDAssetTypeID);
            Int64 PaneID = Convert.ToInt64(strTDPaneID);
            Int64 PersonID = Convert.ToInt64(strTDPersonID);
            int SlotNumber = Convert.ToInt16(strTDSlotNumber);
            string Type = "Task";

            var client = new RestClient(@ConfigurationManager.AppSettings["FACT_API_URL"]);

            var request = new RestRequest("Strips/", Method.POST);
            request.AddHeader("Content-Type", @ConfigurationManager.AppSettings["Content-Type"]);
            request.AddHeader("X-ExternalRequest-ID", @ConfigurationManager.AppSettings["X-ExternalRequest-ID"]);
            request.AddHeader("X-ExternalSystem-ID", @ConfigurationManager.AppSettings["X-ExternalSystem-ID"]);
            request.AddHeader("X-Date", @ConfigurationManager.AppSettings["X-Date"]);
            request.AddParameter("undefined", "<?xml version=\"1.0\"?>\r\n<Strip>\r\n  <AssetTypeID>" + AssetTypeID + "</AssetTypeID>\r\n  <PaneID>" + PaneID + "</PaneID>\r\n  <Details>" + Details + "</Details>\r\n    <People>\r\n    <StripPerson>\r\n      <PersonID>" + PersonID + "</PersonID>\r\n      <SlotNumber>" + SlotNumber + "</SlotNumber>\r\n     </StripPerson>\r\n  </People>\r\n  <PlannedEndTime>" + strPlannedEndTime + "</PlannedEndTime>\r\n  <PlannedStartTime>" + strPlannedStartTime + "</PlannedStartTime>\r\n  <Type>" + Type + "</Type>\r\n</Strip>\r\n", ParameterType.RequestBody);

            var response = client.Execute(request);
            Console.WriteLine("Response Status Code: " + response.StatusCode);

            HttpStatusCode statusCode = response.StatusCode;
            int intRespCode = (int)statusCode;

            if (intRespCode == 200)
            {
                var doc = new XmlDocument();

                doc.LoadXml(response.Content);

                String StripID = doc.GetElementsByTagName("ID")[2].InnerText;

                Console.WriteLine("StripID: " + StripID);

                return StripID;
            }
            else
            {
                return "";
            }
        }

        public String InsertBriefStrip(string strTDAssetTypeID, string strTDPaneID, string strTDPersonID, string strTDSlotNumber, string Details, string strPlannedStartTime, string strPlannedEndTime)
        {
            Int64 AssetTypeID = Convert.ToInt64(strTDAssetTypeID);
            Int64 PaneID = Convert.ToInt64(strTDPaneID);
            Int64 PersonID = Convert.ToInt64(strTDPersonID);
            int SlotNumber = Convert.ToInt16(strTDSlotNumber);
            string Type = "Brief";

            var client = new RestClient(@ConfigurationManager.AppSettings["FACT_API_URL"]);

            var request = new RestRequest("Strips/", Method.POST);
            request.AddHeader("Content-Type", @ConfigurationManager.AppSettings["Content-Type"]);
            request.AddHeader("X-ExternalRequest-ID", @ConfigurationManager.AppSettings["X-ExternalRequest-ID"]);
            request.AddHeader("X-ExternalSystem-ID", @ConfigurationManager.AppSettings["X-ExternalSystem-ID"]);
            request.AddHeader("X-Date", @ConfigurationManager.AppSettings["X-Date"]);
            request.AddParameter("undefined", "<?xml version=\"1.0\"?>\r\n<Strip>\r\n  <AssetTypeID>" + AssetTypeID + "</AssetTypeID>\r\n  <PaneID>" + PaneID + "</PaneID>\r\n  <Details>" + Details + "</Details>\r\n    <People>\r\n    <StripPerson>\r\n      <PersonID>" + PersonID + "</PersonID>\r\n      <SlotNumber>" + SlotNumber + "</SlotNumber>\r\n     </StripPerson>\r\n  </People>\r\n  <PlannedEndTime>" + strPlannedEndTime + "</PlannedEndTime>\r\n  <PlannedStartTime>" + strPlannedStartTime + "</PlannedStartTime>\r\n  <Type>" + Type + "</Type>\r\n</Strip>\r\n", ParameterType.RequestBody);

            var response = client.Execute(request);
            Console.WriteLine("Response Status Code: " + response.StatusCode);

            HttpStatusCode statusCode = response.StatusCode;
            int intRespCode = (int)statusCode;

            if (intRespCode == 200)
            {
                var doc = new XmlDocument();

                doc.LoadXml(response.Content);

                String StripID = doc.GetElementsByTagName("ID")[2].InnerText;

                Console.WriteLine("StripID: " + StripID);

                return StripID;
            }
            else
            {
                return "";
            }
        }

        public String InsertStickyNote(string strTDPaneID, string strTDPersonID, string strTDSlotNumber, string Details, string strPlannedStartTime, string strPlannedEndTime)
        {
            Int64 PaneID = Convert.ToInt64(strTDPaneID);
            Int64 PersonID = Convert.ToInt64(strTDPersonID);
            int SlotNumber = Convert.ToInt16(strTDSlotNumber);
            string Type = "Sticky Note";

            var client = new RestClient(@ConfigurationManager.AppSettings["FACT_API_URL"]);

            var request = new RestRequest("Strips/", Method.POST);
            request.AddHeader("Content-Type", @ConfigurationManager.AppSettings["Content-Type"]);
            request.AddHeader("X-ExternalRequest-ID", @ConfigurationManager.AppSettings["X-ExternalRequest-ID"]);
            request.AddHeader("X-ExternalSystem-ID", @ConfigurationManager.AppSettings["X-ExternalSystem-ID"]);
            request.AddHeader("X-Date", @ConfigurationManager.AppSettings["X-Date"]);
            request.AddParameter("undefined", "<?xml version=\"1.0\"?>\r\n<Strip>\r\n  <PaneID>" + PaneID + "</PaneID>\r\n  <Details>" + Details + "</Details>\r\n    <People>\r\n    <StripPerson>\r\n      <PersonID>" + PersonID + "</PersonID>\r\n      <SlotNumber>" + SlotNumber + "</SlotNumber>\r\n     </StripPerson>\r\n  </People>\r\n  <PlannedEndTime>" + strPlannedEndTime + "</PlannedEndTime>\r\n  <PlannedStartTime>" + strPlannedStartTime + "</PlannedStartTime>\r\n  <Type>" + Type + "</Type>\r\n</Strip>\r\n", ParameterType.RequestBody);

            var response = client.Execute(request);
            Console.WriteLine("Response Status Code: " + response.StatusCode);

            HttpStatusCode statusCode = response.StatusCode;
            int intRespCode = (int)statusCode;

            if (intRespCode == 200)
            {
                var doc = new XmlDocument();

                doc.LoadXml(response.Content);

                String StripID = doc.GetElementsByTagName("ID")[2].InnerText;

                Console.WriteLine("StripID: " + StripID);

                return StripID;
            }
            else
            {
                return "";
            }
        }

        public void InsertFormationGroup(string strTDGroupHeaderAssetTypeID, string strTDAssetTypeID, string strTDPaneID, string strTDPersonID1, string strTDSlotNumber1, string strTDPersonID2, string strTDSlotNumber2, string strTDWeatherStateID, string Details, string strPlannedStartTime, string strPlannedEndTime, out string StripIDHeader, out string StripID1, out string StripID2)
        {
            Int64 GroupHeaderAssetTypeID = Convert.ToInt64(strTDGroupHeaderAssetTypeID);
            Int64 AssetTypeID = Convert.ToInt64(strTDAssetTypeID);
            Int64 PaneID = Convert.ToInt64(strTDPaneID);
            Int64 PersonID1 = Convert.ToInt64(strTDPersonID1);
            int SlotNumber1 = Convert.ToInt16(strTDSlotNumber1);
            Int64 PersonID2 = Convert.ToInt64(strTDPersonID2);
            int SlotNumber2 = Convert.ToInt16(strTDSlotNumber2);
            Int64 WeatherStateID = Convert.ToInt64(strTDWeatherStateID);
            string Type = "Mission";
            string GroupType = "Formation Group";
            
            var client = new RestClient(@ConfigurationManager.AppSettings["FACT_API_URL"]);

            var request = new RestRequest("Strips/", Method.POST);
            request.AddHeader("Content-Type", @ConfigurationManager.AppSettings["Content-Type"]);
            request.AddHeader("X-ExternalRequest-ID", @ConfigurationManager.AppSettings["X-ExternalRequest-ID"]);
            request.AddHeader("X-ExternalSystem-ID", @ConfigurationManager.AppSettings["X-ExternalSystem-ID"]);
            request.AddHeader("X-Date", @ConfigurationManager.AppSettings["X-Date"]);
            request.AddParameter("undefined", "<?xml version=\"1.0\"?>\r\n<Strip>\r\n  <AssetTypeID>" + GroupHeaderAssetTypeID + "</AssetTypeID>\r\n  <PaneID>" + PaneID + "</PaneID>\r\n    <GroupMembers>\r\n  <Strip>\r\n  <AssetTypeID>" + AssetTypeID + "</AssetTypeID>\r\n  <PaneID>" + PaneID + "</PaneID>\r\n  <MinimumWeatherStateID>" + WeatherStateID + "</MinimumWeatherStateID>\r\n    <MaximumWeatherStateID>" + WeatherStateID + "</MaximumWeatherStateID>\r\n  <Details>" + Details + "</Details>\r\n    <People>\r\n    <StripPerson>\r\n      <PersonID>" + PersonID1 + "</PersonID>\r\n      <SlotNumber>" + SlotNumber1 + "</SlotNumber>\r\n     </StripPerson>\r\n  </People>\r\n  <PlannedEndTime>" + strPlannedEndTime + "</PlannedEndTime>\r\n  <PlannedStartTime>" + strPlannedStartTime + "</PlannedStartTime>\r\n  <Type>" + Type + "</Type>\r\n</Strip>\r\n  \r\n<Strip>\r\n  <AssetTypeID>" + AssetTypeID + "</AssetTypeID>\r\n  <PaneID>" + PaneID + "</PaneID>\r\n  <MinimumWeatherStateID>" + WeatherStateID + "</MinimumWeatherStateID>\r\n    <MaximumWeatherStateID>" + WeatherStateID + "</MaximumWeatherStateID>\r\n  <Details>" + Details + "</Details>\r\n    <People>\r\n    <StripPerson>\r\n      <PersonID>" + PersonID2 + "</PersonID>\r\n      <SlotNumber>" + SlotNumber2 + "</SlotNumber>\r\n     </StripPerson>\r\n  </People>\r\n  <PlannedEndTime>" + strPlannedEndTime + "</PlannedEndTime>\r\n  <PlannedStartTime>" + strPlannedStartTime + "</PlannedStartTime>\r\n  <Type>" + Type + "</Type>\r\n  </Strip>\r\n  </GroupMembers>\r\n  <Type>" + GroupType + "</Type>\r\n  </Strip > ", ParameterType.RequestBody);

            var response = client.Execute(request);
            Console.WriteLine("Response Status Code: " + response.StatusCode);

            HttpStatusCode statusCode = response.StatusCode;
            int intRespCode = (int)statusCode;

            StripIDHeader = "";
            StripID1 = "";
            StripID2 = "";

            if (intRespCode == 200)
            {
                var doc = new XmlDocument();

                doc.LoadXml(response.Content);

                StripIDHeader = doc.GetElementsByTagName("ID")[25].InnerText;
                StripID1 = doc.GetElementsByTagName("ID")[4].InnerText;
                StripID2 = doc.GetElementsByTagName("ID")[16].InnerText;

                Console.WriteLine("StripID: " + StripIDHeader);
                Console.WriteLine("StripID1: " + StripID1);
                Console.WriteLine("StripID2: " + StripID2);
            }
        }

        public void InsertSortieGroup(string strTDGroupHeaderAssetTypeID, string strTDAssetTypeID, string strTDPaneID, string strTDPersonID1, string strTDSlotNumber1, string strTDPersonID2, string strTDSlotNumber2, string strTDWeatherStateID, string Details, string strPlannedStartTime, string strPlannedEndTime, out string StripIDHeader, out string StripID1, out string StripID2)
        {
            Int64 GroupHeaderAssetTypeID = Convert.ToInt64(strTDGroupHeaderAssetTypeID);
            Int64 AssetTypeID = Convert.ToInt64(strTDAssetTypeID);
            Int64 PaneID = Convert.ToInt64(strTDPaneID);
            Int64 PersonID1 = Convert.ToInt64(strTDPersonID1);
            int SlotNumber1 = Convert.ToInt16(strTDSlotNumber1);
            Int64 PersonID2 = Convert.ToInt64(strTDPersonID2);
            int SlotNumber2 = Convert.ToInt16(strTDSlotNumber2);
            Int64 WeatherStateID = Convert.ToInt64(strTDWeatherStateID);
            DateTime Strip2PlannedStartTime = Convert.ToDateTime(strPlannedEndTime).AddHours(1);
            DateTime Strip2PlannedEndTime = Strip2PlannedStartTime.AddHours(1);
           
            string Type = "Mission";
            string GroupType = "Sortie Group";            

            String strStrip2PlannedStartTime = Strip2PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
            String strStrip2PlannedEndTime = Strip2PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

            Console.WriteLine(strStrip2PlannedStartTime);
            Console.WriteLine(strStrip2PlannedEndTime);
           
            var client = new RestClient(@ConfigurationManager.AppSettings["FACT_API_URL"]);

            var request = new RestRequest("Strips/", Method.POST);
            request.AddHeader("Content-Type", @ConfigurationManager.AppSettings["Content-Type"]);
            request.AddHeader("X-ExternalRequest-ID", @ConfigurationManager.AppSettings["X-ExternalRequest-ID"]);
            request.AddHeader("X-ExternalSystem-ID", @ConfigurationManager.AppSettings["X-ExternalSystem-ID"]);
            request.AddHeader("X-Date", @ConfigurationManager.AppSettings["X-Date"]);
            request.AddParameter("undefined", "<?xml version=\"1.0\"?>\r\n<Strip>\r\n  <AssetTypeID>" + GroupHeaderAssetTypeID + "</AssetTypeID>\r\n  <PaneID>" + PaneID + "</PaneID>\r\n    <GroupMembers>\r\n  <Strip>\r\n  <AssetTypeID>" + AssetTypeID + "</AssetTypeID>\r\n  <PaneID>" + PaneID + "</PaneID>\r\n  <MinimumWeatherStateID>" + WeatherStateID + "</MinimumWeatherStateID>\r\n    <MaximumWeatherStateID>" + WeatherStateID + "</MaximumWeatherStateID>\r\n  <Details>" + Details + "</Details>\r\n    <People>\r\n    <StripPerson>\r\n      <PersonID>" + PersonID1 + "</PersonID>\r\n      <SlotNumber>" + SlotNumber1 + "</SlotNumber>\r\n     </StripPerson>\r\n  </People>\r\n  <PlannedEndTime>" + strPlannedEndTime + "</PlannedEndTime>\r\n  <PlannedStartTime>" + strPlannedStartTime + "</PlannedStartTime>\r\n  <Type>" + Type + "</Type>\r\n</Strip>\r\n  \r\n<Strip>\r\n  <AssetTypeID>" + AssetTypeID + "</AssetTypeID>\r\n  <PaneID>" + PaneID + "</PaneID>\r\n  <MinimumWeatherStateID>" + WeatherStateID + "</MinimumWeatherStateID>\r\n    <MaximumWeatherStateID>" + WeatherStateID + "</MaximumWeatherStateID>\r\n  <Details>" + Details + "</Details>\r\n    <People>\r\n    <StripPerson>\r\n      <PersonID>" + PersonID2 + "</PersonID>\r\n      <SlotNumber>" + SlotNumber2 + "</SlotNumber>\r\n     </StripPerson>\r\n  </People>\r\n  <PlannedEndTime>" + strStrip2PlannedEndTime + "</PlannedEndTime>\r\n  <PlannedStartTime>" + strStrip2PlannedStartTime + "</PlannedStartTime>\r\n  <Type>" + Type + "</Type>\r\n  </Strip>\r\n  </GroupMembers>\r\n  <Type>" + GroupType + "</Type>\r\n  </Strip > ", ParameterType.RequestBody);

            var response = client.Execute(request);
            Console.WriteLine("Response Status Code: " + response.StatusCode);
            Console.WriteLine(response.Content);

            HttpStatusCode statusCode = response.StatusCode;
            int intRespCode = (int)statusCode;

            StripIDHeader = "";
            StripID1 = "";
            StripID2 = "";

            if (intRespCode == 200)
            {                
            var doc = new XmlDocument();

            doc.LoadXml(response.Content);

            StripIDHeader = doc.GetElementsByTagName("ID")[25].InnerText;
            StripID1 = doc.GetElementsByTagName("ID")[4].InnerText;
            StripID2 = doc.GetElementsByTagName("ID")[16].InnerText;

            Console.WriteLine("StripID: " + StripIDHeader);
            Console.WriteLine("StripID1: " + StripID1);
            Console.WriteLine("StripID2: " + StripID2);
            }        
        }

        public String InsertPeopleUnavailability(string strTDPersonID, string strTDUnavailailitySubTypeID, string Details, string strPlannedStartTime, string strPlannedEndTime)
        {
            Int64 PersonID = Convert.ToInt64(strTDPersonID);
            Int64 UnavailailitySubTypeID = Convert.ToInt64(strTDUnavailailitySubTypeID);
            string Type = "Person Unavailability";

            var client = new RestClient(@ConfigurationManager.AppSettings["FACT_API_URL"]);

            var request = new RestRequest("Strips/", Method.POST);
            request.AddHeader("Content-Type", @ConfigurationManager.AppSettings["Content-Type"]);
            request.AddHeader("X-ExternalRequest-ID", @ConfigurationManager.AppSettings["X-ExternalRequest-ID"]);
            request.AddHeader("X-ExternalSystem-ID", @ConfigurationManager.AppSettings["X-ExternalSystem-ID"]);
            request.AddHeader("X-Date", @ConfigurationManager.AppSettings["X-Date"]);
            request.AddParameter("undefined", "<?xml version=\"1.0\"?>\r\n<Strip>\r\n  <Details>" + Details + "</Details>\r\n    <People>\r\n    <StripPerson>\r\n      <PersonID>" + PersonID + "</PersonID>\r\n   </StripPerson>\r\n  </People>\r\n  <SubTypeID>" + UnavailailitySubTypeID + "</SubTypeID>\r\n    <PlannedEndTime>" + strPlannedEndTime + "</PlannedEndTime>\r\n  <PlannedStartTime>" + strPlannedStartTime + "</PlannedStartTime>\r\n  <Type>" + Type + "</Type>\r\n</Strip>\r\n", ParameterType.RequestBody);

            var response = client.Execute(request);
            Console.WriteLine("Response Status Code: " + response.StatusCode);
            Console.WriteLine(response.Content);
            HttpStatusCode statusCode = response.StatusCode;
            int intRespCode = (int)statusCode;

            if (intRespCode == 200)
            {
                var doc = new XmlDocument();

                doc.LoadXml(response.Content);

                String StripID = doc.GetElementsByTagName("ID")[2].InnerText;

                Console.WriteLine("StripID: " + StripID);

                return StripID;
            }
            else
            {
                return "";
            }
        }

        public Int64 Search_Outlook_Strip()
        {

            var client = new RestClient(@ConfigurationManager.AppSettings["FACT_API_URL"]);

            var request = new RestRequest("Strips/Search", Method.DELETE);
            request.AddHeader("Content-Type", @ConfigurationManager.AppSettings["Content-Type"]);
            request.AddHeader("X-ExternalRequest-ID", @ConfigurationManager.AppSettings["X-ExternalRequest-ID"]);
            request.AddHeader("X-ExternalSystem-ID", @ConfigurationManager.AppSettings["X-ExternalSystem-ID"]);
            request.AddHeader("X-Date", @ConfigurationManager.AppSettings["X-Date"]);
            request.AddParameter("undefined", "<?xml version=\"1.0\"?>\r\n<Strip>\r\n < ID >\r\n < IncludeInResponse > true </ IncludeInResponse >\r\n </ ID >\r\n  < Details >\r\n      < IncludeInResponse > true </ IncludeInResponse >< Filters >\r\n < FilterItem > \r\n < Value > AT Outlook Unavailability </ Value >\r\n        < SearchMode > StartsWith </ SearchMode > \r\n </ FilterItem >\r\n </ Filters >\r\n  </ Details >\r\n</ Strip >\r\n", ParameterType.RequestBody);

            var response = client.Execute(request);
            Console.WriteLine("Response Status Code: " + response.StatusCode);
            Console.WriteLine(response.Content);
            HttpStatusCode statusCode = response.StatusCode;
            int intRespCode = (int)statusCode;

            if (intRespCode == 200)
            {
                var doc = new XmlDocument();

                doc.LoadXml(response.Content);

                String StripID = doc.GetElementsByTagName("ID")[0].InnerText;

                Console.WriteLine("StripID: " + StripID);

                return Convert.ToInt64(StripID);
            }
            else
            {
                return 0;
            }
        }
        public void Delete_Strip(String StripID)
        {

            var client = new RestClient(@ConfigurationManager.AppSettings["FACT_API_URL"]);

            var request = new RestRequest("Strips/" + StripID, Method.DELETE);
            request.AddHeader("Content-Type", @ConfigurationManager.AppSettings["Content-Type"]);
            request.AddHeader("X-ExternalRequest-ID", @ConfigurationManager.AppSettings["X-ExternalRequest-ID"]);
            request.AddHeader("X-ExternalSystem-ID", @ConfigurationManager.AppSettings["X-ExternalSystem-ID"]);
            request.AddHeader("X-Date", @ConfigurationManager.AppSettings["X-Date"]);

            var response = client.Execute(request);
            var content = client.Execute(request).Content;
        }
    }
}
