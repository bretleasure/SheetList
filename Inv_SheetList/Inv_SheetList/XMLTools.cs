using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Inv_SheetList
{
    public class XMLTools
    {
        public static void CreateXML(Object YourClassObject, string path)
        {
            XmlDocument xmlDoc = new XmlDocument();   //Represents an XML document, 
                                                      // Initializes a new instance of the XmlDocument class.   

            List<Type> Types = new List<Type>() { typeof(SheetList) };

            XmlSerializer xmlSerializer = new XmlSerializer(YourClassObject.GetType(), Types.ToArray());
            // Creates a stream whose backing store is memory. 
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, YourClassObject);
                xmlStream.Position = 0;
                //Loads the XML document from the specified string.
                xmlDoc.Load(xmlStream);

                System.IO.File.WriteAllText(path, xmlDoc.InnerXml);
            }
        }

        /// <summary>
        /// Returns an Object from a specified XML file.  You are required to pass it the object type you want to receive
        /// </summary>
        /// <param name="path"></param>
        /// <param name="MyClassObject"></param>
        /// <returns></returns>
        public static Object Get_ObjectFromXML(string path, Object MyClassObject)
        {
            string xmlString = Get_TextFromXML(path);

            XmlSerializer oXmlSerializer = new XmlSerializer(MyClassObject.GetType());
            //XmlSerializer oXmlSerializer = new XmlSerializer(MyClassObject.GetType());

            //The StringReader will be the stream holder for the existing XML file 
            MyClassObject = oXmlSerializer.Deserialize(new StringReader(xmlString));
            //initially deserialized, the data is represented by an object without a defined type 

            return MyClassObject;
        }

        private static string Get_TextFromXML(string filepath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);

            StringWriter sw = new StringWriter();
            XmlTextWriter tx = new XmlTextWriter(sw);
            doc.WriteTo(tx);

            string str = sw.ToString();
            return str;
        }

    }
}
