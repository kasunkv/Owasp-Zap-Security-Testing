using System;
using System.Xml.Serialization;

namespace OwaspZapSecurityTesting.Tests.ZapReportModels
{
    [XmlRoot(ElementName = "instance")]
    public class Instance
    {
        [XmlElement(ElementName = "uri")]
        public string Uri { get; set; }
        [XmlElement(ElementName = "method")]
        public string Method { get; set; }
        [XmlElement(ElementName = "evidence")]
        public string Evidence { get; set; }
        [XmlElement(ElementName = "param")]
        public string Param { get; set; }
    }

}
