namespace Rocket
{
    public class PathHelper
    {
        public static string CurrentPath()
        {
            return System.AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}