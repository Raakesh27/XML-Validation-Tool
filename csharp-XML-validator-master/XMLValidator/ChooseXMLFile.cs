using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml; 
using System.Xml.Schema;

namespace XMLValidator
{
    public partial class ChooseXMLFile : Form
    {
        String fileName;
        String schemaName;

        public ChooseXMLFile()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFDxml.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFDschema.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (fileName == null)
            {
                MessageBox.Show("Select an XML file!");
            }
            else if (schemaName == null)
            {
                MessageBox.Show("Select a Schema file!");
            }
            else
            {
                // validate the XML file
                ValidateXmlWithXsd(fileName, schemaName);
                //if (true)
                //{
                //    MessageBox.Show("valid XML!");
                //}
                //else
                //{
                //    MessageBox.Show("Not valid XML");
                //}
            }
            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            fileName = openFDxml.FileName;
            filePathTextBox.Text = fileName;
        }

        private void openFileDialog1_FileOk_1(object sender, CancelEventArgs e)
        {
            schemaName = openFDschema.FileName;
            schemaPathTextBox.Text = schemaName;
        }

        public void ValidateXmlWithXsd(string xmlUri, string xsdUri)
        {
            XmlReader reader1 = null;
            try
            {
                XmlReaderSettings xmlSettings = new XmlReaderSettings();
                //xmlSettings.Schemas = new System.Xml.Schema.XmlSchemaSet();
                xmlSettings.ValidationType = ValidationType.Schema;
                //xmlSettings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
                xmlSettings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
                xmlSettings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
                xmlSettings.ValidationEventHandler += new ValidationEventHandler(this.ValidationCallBack);

                reader1 = XmlReader.Create(xmlUri, xmlSettings);

                // Parse the file.
                while (reader1.Read()) ;
                


            }
            catch (System.Xml.XmlException ex)
            {
                MessageBox.Show("Error Validating: " + ex.Message);
            }
            finally
            {
                if (reader1 != null)
                {
                    reader1.Close();
                }
            }

        }
        public void ValidationCallBack(object sender,ValidationEventArgs args)
            {
                MessageBox.Show("Validation Error: " + args.Message);

            }
        
            
    }
}
