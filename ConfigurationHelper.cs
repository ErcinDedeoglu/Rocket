using System.IO;
using System.Xml.Serialization;

namespace Rocket
{
    public class ConfigurationHelper
    {
        public static Rocket.DTO.ConfigurationDto.Root Configuration()
        {
            Rocket.DTO.ConfigurationDto.Root configuration = null;
            
            XmlSerializer serializer = new XmlSerializer(typeof(Rocket.DTO.ConfigurationDto.Root));
            StreamReader reader = new StreamReader(PathHelper.CurrentPath() + "//Configuration.xml");
            configuration = (Rocket.DTO.ConfigurationDto.Root)serializer.Deserialize(reader);
            reader.Close();

            configuration.SQLConnectionString = Rocket.DatabaseHelper.BuildConnectionString(configuration.Database.DataSource, configuration.Database.IntegratedSecurity, configuration.Database.InitialCatalog, configuration.Database.UserID, configuration.Database.Password);
            configuration.SQLConnectionStringMaster = Rocket.DatabaseHelper.BuildConnectionString(configuration.Database.DataSource, configuration.Database.IntegratedSecurity, null, configuration.Database.UserID, configuration.Database.Password);

            return configuration;
        }
    }
}