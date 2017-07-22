using System.Xml.Serialization;

namespace OwaspZapSecurityTesting.Tests.ZapReportModels
{
    [XmlRoot(ElementName = "alertitem")]
    public class Alertitem
    {
        [XmlElement(ElementName = "pluginid")]
        public string Pluginid { get; set; }
        [XmlElement(ElementName = "alert")]
        public string Alert { get; set; }
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "riskcode")]
        public string Riskcode { get; set; }
        [XmlElement(ElementName = "confidence")]
        public string Confidence { get; set; }
        [XmlElement(ElementName = "riskdesc")]
        public string Riskdesc { get; set; }
        [XmlElement(ElementName = "desc")]
        public string Desc { get; set; }
        [XmlElement(ElementName = "instances")]
        public Instances Instances { get; set; }
        [XmlElement(ElementName = "count")]
        public string Count { get; set; }
        [XmlElement(ElementName = "solution")]
        public string Solution { get; set; }
        [XmlElement(ElementName = "reference")]
        public string Reference { get; set; }
        [XmlElement(ElementName = "cweid")]
        public string Cweid { get; set; }
        [XmlElement(ElementName = "wascid")]
        public string Wascid { get; set; }
        [XmlElement(ElementName = "sourceid")]
        public string Sourceid { get; set; }
        [XmlElement(ElementName = "otherinfo")]
        public string Otherinfo { get; set; }
    }

}
