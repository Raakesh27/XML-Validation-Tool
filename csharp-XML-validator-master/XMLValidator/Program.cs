using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;

namespace XMLValidator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ChooseXMLFile());


            XmlReader reader = XmlReader.Create("dfstaf.xml");
            //XmlReader reader1 = XmlReader.Create("item2.xml");
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            XmlSchemaInference inference = new XmlSchemaInference();
            schemaSet = inference.InferSchema(reader);
            XmlWriter writer;
            // Display the inferred schema.
            Console.WriteLine("\n\nOriginal schema:\n");
            foreach (XmlSchema schema in schemaSet.Schemas())
            {
                writer = XmlWriter.Create("dfstaf.xsd");
                schema.Write(Console.Out);
                schema.Write(writer);
                writer.Close();
                
            }

           
        }
    }
}
