using System.Xml.Serialization;

namespace OwaspZapSecurityTesting.Tests.ZapReportModels
{
    [XmlRoot(ElementName = "site")]
    public class Site
    {
        [XmlElement(ElementName = "alerts")]
        public Alerts Alerts { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "host")]
        public string Host { get; set; }
        [XmlAttribute(AttributeName = "port")]
        public string Port { get; set; }
        [XmlAttribute(AttributeName = "ssl")]
        public string Ssl { get; set; }
    }

}
