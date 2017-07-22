using System.Xml.Serialization;
using System.Collections.Generic;

namespace OwaspZapSecurityTesting.Tests.ZapReportModels
{
    [XmlRoot(ElementName = "instances")]
    public class Instances
    {
        [XmlElement(ElementName = "instance")]
        public List<Instance> Instance { get; set; }
    }

}
