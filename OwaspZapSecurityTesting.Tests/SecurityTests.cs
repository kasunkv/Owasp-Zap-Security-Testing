using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OWASPZAPDotNetAPI;
using System.Threading;

namespace OwaspZapSecurityTesting.Tests
{
    [TestClass]
    public class SecurityTests
    {
        private readonly static string _zapApiKey = "62simrufj37n98f5r0dmj68q8q";
        private readonly static string _zapUrl = "zap.k2vsoftware.com";
        private readonly static int _zapPort = 80;
        private readonly string _targetUrl = "http://k2vowasptestsite.azurewebsites.net/"; // Web App Hosted on Azure

        private static ClientApi _zapClient;
        private IApiResponse _response;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            _zapClient = new ClientApi(_zapUrl, _zapPort, _zapApiKey);
        }

        [TestMethod]
        public void ExecuteSpider()
        {
            var spiderId = StartSpidering();
            CheckSpideringProgress(spiderId);
        }

        

        private string StartSpidering()
        {
            _response = _zapClient.spider.scan(_zapApiKey, _targetUrl, "", "", "", "");
            return ((ApiResponseElement)_response).Value;
        }

        private void CheckSpideringProgress(string spideringId)
        {
            int progress;
            while (true)
            {
                Thread.Sleep(10000);
                progress = int.Parse(((ApiResponseElement)_zapClient.spider.status(spideringId)).Value);
                if (progress >= 100)
                {
                    break;
                }
            }

            Thread.Sleep(5000);
        }        
    }
}
