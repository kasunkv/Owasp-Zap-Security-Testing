using System.Xml.Serialization;
using System.Collections.Generic;

namespace OwaspZapSecurityTesting.Tests.ZapReportModels
{
    [XmlRoot(ElementName = "alerts")]
    public class Alerts
    {
        [XmlElement(ElementName = "alertitem")]
        public List<Alertitem> Alertitem { get; set; }
    }

}
