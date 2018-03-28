using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventor;

namespace Inv_SheetList
{
    public partial class ConfigureUI : Form
    {

        string UserFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
        string AppFolder = @"\S5H3E1E2T1L2I6S2T";

        string Title;
        bool ShowTitle;

        string SheetNoColName;
        string SheetNameColName;

        TableDirectionEnum Direction;

        HeadingPlacementEnum HeadingPlacement;

        bool WrapLeft;

        bool EnableAutoWrap;

        bool WrapByMaxRows;
        bool WrapByNumberOfSections;

        int MaxRows;
        int NumberOfSections;

        public ConfigureUI()
        {
            InitializeComponent();
        }

        private void ConfigureUI_Load(object sender, EventArgs e)
        {

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void CollectInputs()
        {
            Title = txb_Title.Text;
            ShowTitle = ckb_ShowTitle.Checked;

            if (rad_DirectionBtm.Checked)
                Direction = TableDirectionEnum.kTopDownDirection;
            else if (rad_DirectionTop.Checked)
                Direction = TableDirectionEnum.kBottomUpDirection;

            if (rad_ColHeadingTop.Checked)
                HeadingPlacement = HeadingPlacementEnum.kHeadingAtTop;
            else if (rad_ColHeadingBtm.Checked)
                HeadingPlacement = HeadingPlacementEnum.kHeadingAtBottom;
            else if (rad_ColHeadingHide.Checked)
                HeadingPlacement = HeadingPlacementEnum.kNoHeading;

            SheetNoColName = txb_SheetNoColName.Text;
            SheetNameColName = txb_SheetNameColName.Text;

            EnableAutoWrap = ckb_EnableAutoWrap.Checked;

            if (rad_WrapDirectionLeft.Checked)
                WrapLeft = true;
            else if (rad_WrapDirectionRight.Checked)
                WrapLeft = false;

            MaxRows = Convert.ToInt32(txb_MaxRows.Text);
            NumberOfSections = Convert.ToInt32(txb_SectionNumber.Text);

        }

        private void btn_SaveSettings_Click(object sender, EventArgs e)
        {
            CollectInputs();

            SheetList oSheetList = new SheetList();

            oSheetList.Title = Title;
            oSheetList.ShowTitle = ShowTitle;
            oSheetList.SheetNoColName = SheetNoColName;
            oSheetList.SheetNameColName = SheetNameColName;
            oSheetList.Direction = Direction;
            oSheetList.HeadingPlacement = HeadingPlacement;
            oSheetList.WrapLeft = WrapLeft;
            oSheetList.EnableAutoWrap = EnableAutoWrap;
            oSheetList.MaxRows = MaxRows;
            oSheetList.NumberOfSections = NumberOfSections;

            //Export Object to XML in User Folder
            CreateXML(oSheetList, UserFolder + AppFolder + "SLSettings.xml");

        }

        private void ckb_ShowTitle_CheckedChanged(object sender, EventArgs e)
        {
            txb_Title.Enabled = ckb_ShowTitle.Checked;
        }

        private void ckb_EnableAutoWrap_CheckedChanged(object sender, EventArgs e)
        {
            pnl_AutoWrap.Enabled = ckb_EnableAutoWrap.Checked;
        }

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
        public static Object get_ObjectFromXML(string path, Object MyClassObject)
        {
            string xmlString = get_TextFromXML(path);

            XmlSerializer oXmlSerializer = new XmlSerializer(MyClassObject.GetType());
            //XmlSerializer oXmlSerializer = new XmlSerializer(MyClassObject.GetType());

            //The StringReader will be the stream holder for the existing XML file 
            MyClassObject = oXmlSerializer.Deserialize(new StringReader(xmlString));
            //initially deserialized, the data is represented by an object without a defined type 

            return MyClassObject;
        }

        private static string get_TextFromXML(string filepath)
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
