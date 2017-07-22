using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OWASPZAPDotNetAPI;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using OwaspZapSecurityTesting.Tests.ZapReportModels;
using System.Xml;
using System.Xml.Serialization;

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
        public void ExecuteSecurityScan()
        {
            // Spidering
            var spiderId = StartSpidering();
            CheckSpideringProgress(spiderId);

            //  Active Scan
            var activeScanId = StartActiveScan();
            CheckActiveScanProgress(activeScanId);

            // Assert
            var expectedResult = new SecurityScanResult
            {
                InformationalAlerts = 0,
                LowAlerts = 3,
                MediumAlerts = 2,
                HighAlerts = 0
            };

            var actualResult = GetScanResults();

            Assert.AreEqual(expectedResult.LowAlerts, actualResult.LowAlerts, "Low Alerts have exceeded the expected values.");
            Assert.AreEqual(expectedResult.MediumAlerts, actualResult.MediumAlerts, "Medium Alerts have exceeded the expected values.");
            Assert.AreEqual(expectedResult.HighAlerts, actualResult.HighAlerts, "High Alerts have exceeded the expected values.");
        }        

        [ClassCleanup]
        public static void CleanUpAndGenerateReport()
        {
            _zapClient.Dispose();
            
            var reportFilename = $"{DateTime.Now.ToString("dd-MMM-yyyy-hh-mm-ss")}_OWASP_ZAP_Report";
            GenerateXmlReport(reportFilename);
            GenerateHTMLReport(reportFilename);
            GenerateMarkdownReport(reportFilename);
        }

        private List<Alertitem> GetScanAlerts()
        {
            OWASPZAPReport report = null;
            using (var memoryStream = new MemoryStream(_zapClient.core.xmlreport(_zapApiKey)))
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(memoryStream);

                var serializer = new XmlSerializer(typeof(OWASPZAPReport));
                report = (OWASPZAPReport)serializer.Deserialize(new XmlNodeReader(xmlDoc.DocumentElement));
            }
            return report.Site.Alerts.Alertitem;
        }

        private SecurityScanResult GetScanResults()
        {
            var result = new SecurityScanResult();
            var scanAlerts = GetScanAlerts();

            foreach (var alert in scanAlerts)
            {
                switch (alert.Riskcode)
                {
                    case RiskCode.Informational:
                        result.InformationalAlerts++;
                        break;
                    case RiskCode.Low:
                        result.LowAlerts++;
                        break;
                    case RiskCode.Medium:
                        result.MediumAlerts++;
                        break;
                    case RiskCode.High:
                        result.HighAlerts++;
                        break;
                }
            }

            return result;
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
        
        private string StartActiveScan()
        {
            _response = _zapClient.ascan.scan(_zapApiKey, _targetUrl, "", "", "", "", "", "");
            return ((ApiResponseElement)_response).Value;
        }

        private void CheckActiveScanProgress(string activeScanId)
        {
            int progress;
            while (true)
            {
                Thread.Sleep(10000);
                progress = int.Parse(((ApiResponseElement)_zapClient.ascan.status(activeScanId)).Value);

                if (progress >= 100)
                {
                    break;
                }
            }

            Thread.Sleep(5000);
        }

        private static void GenerateXmlReport(string filename)
        {
            var fileName = $"{filename}.xml";
            File.WriteAllBytes(fileName, _zapClient.core.xmlreport(_zapApiKey));
        }

        private static void GenerateHTMLReport(string filename)
        {
            var fileName = $"{filename}.html";
            File.WriteAllBytes(fileName, _zapClient.core.htmlreport(_zapApiKey));
        }

        private static void GenerateMarkdownReport(string filename)
        {
            var fileName = $"{filename}.md";
            File.WriteAllBytes(fileName, _zapClient.core.mdreport(_zapApiKey));
        }
    }
}
