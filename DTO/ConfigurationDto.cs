using System.Xml.Serialization;

namespace Rocket.DTO
{
    public class ConfigurationDto
    {
        public static ConfigurationDto.Root Configuration { get; set; }
        
        [XmlRoot(ElementName = "Database")]
        public class Database
        {
            [XmlAttribute(AttributeName = "DataSource")]
            public string DataSource { get; set; }
            [XmlAttribute(AttributeName = "IntegratedSecurity")]
            public bool IntegratedSecurity { get; set; }
            [XmlAttribute(AttributeName = "InitialCatalog")]
            public string InitialCatalog { get; set; }
            [XmlAttribute(AttributeName = "UserID")]
            public string UserID { get; set; }
            [XmlAttribute(AttributeName = "Password")]
            public string Password { get; set; }
        }

        [XmlRoot(ElementName = "CentralServer")]
        public class CentralServer
        {
            [XmlAttribute(AttributeName = "IP")]
            public string IP { get; set; }
        }
        
        [XmlRoot(ElementName = "Configurations")]
        public class Root
        {

            [XmlElement(ElementName = "Database")]
            public Database Database { get; set; }
            [XmlElement(ElementName = "CentralServer")]
            public CentralServer CentralServer { get; set; }

            public string SQLConnectionString { get; set; }
            public string SQLConnectionStringMaster { get; set; }
            
        }
    }
}