using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Extensions
{
    public static class XExtensions
    {
        public static IList<XElement> Controllers(this XDocument xDoc)
        {
            return xDoc.Descendants()
                       .Where(x => x.Name == "Controller")
                       .ToList();
        }

        public static IList<XElement> Actions(this XElement element)
        {
            return element.Descendants()
                          .Where(x => x.Name == "Action")
                          .ToList();
        }

        public static IList<XElement> Paths(this XElement element)
        {
            return element.Descendants("Path").ToList();
        }

        public static string ParamValue(this XElement element, string paramName)
        {
            var param = element.Descendants(paramName).FirstOrDefault();
            return param == null ? null : param.Value;
        }
    }
}
