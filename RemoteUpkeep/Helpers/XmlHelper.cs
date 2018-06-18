using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RemoteUpkeep.Helpers
{
    public static class XmlHelper
    {
        public static XElement GetComponentLink(string fieldName, string id, string title)
        {
            XNamespace ns = "http://www.w3.org/1999/xlink";

            if (string.IsNullOrEmpty(title))
                return new XElement(fieldName,
                    new XAttribute(XNamespace.Xmlns + "xlink", ns),
                    new XAttribute(ns + "type", "simple"),
                    new XAttribute(ns + "href", id));

            return new XElement(fieldName,
                new XAttribute(XNamespace.Xmlns + "xlink", ns),
                new XAttribute(ns + "type", "simple"),
                new XAttribute(ns + "href", id),
                new XAttribute(ns + "title", title));
        }

        public static XElement GetField(XNamespace ns, string fieldName, object value)
        {
            return new XElement(ns + fieldName, value);
        }

        public static XElement GetXhtmlField(XNamespace ns, string fieldName, string html)
        {
            XElement x = XElement.Parse(string.Format("<{0}>{1}</{0}>", fieldName, HttpUtility.HtmlDecode(html)));

            XElement res = new XElement(ns + fieldName);

            XNamespace nsXhtml = "http://www.w3.org/1999/xhtml";

            if (x.Elements().Any())
            {
                foreach (XElement p in x.Elements())
                {
                    res.Add(new XElement(nsXhtml + p.Name.LocalName, p.Nodes(), p.Attributes()));
                }
            }
            else
            {
                res.Add(x.Value);
            }

            return res;
        }

    }
}
