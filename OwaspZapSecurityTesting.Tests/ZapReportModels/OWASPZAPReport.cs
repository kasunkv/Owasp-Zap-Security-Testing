using System.Xml.Serialization;

namespace OwaspZapSecurityTesting.Tests.ZapReportModels
{
    [XmlRoot(ElementName = "OWASPZAPReport")]
    public class OWASPZAPReport
    {
        [XmlElement(ElementName = "site")]
        public Site Site { get; set; }
        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }
        [XmlAttribute(AttributeName = "generated")]
        public string Generated { get; set; }
    }

}
