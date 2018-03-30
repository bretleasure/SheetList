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
using CAP.Utilities;

namespace Inv_SheetList
{
    public partial class ConfigureUI : Form
    {
        string AppFolder = AddinGlobal.AppFolder;

        string SettingsFile = AddinGlobal.SettingsFile;

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
            ImportInputs();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void ImportInputs()
        {
            //SheetList oSheetList = new SheetList();
            //oSheetList = (SheetList)XMLTools.Get_ObjectFromXML(AppFolder + SettingsFile, oSheetList);

            SheetList oSheetList = AddinGlobal.oSheetList;

            if (oSheetList != null)
            {
                ckb_ShowTitle.Checked = oSheetList.ShowTitle;
                txb_Title.Text = oSheetList.Title;

                if (oSheetList.Direction == TableDirectionEnum.kTopDownDirection)
                    rad_DirectionBtm.Checked = true;
                else
                    rad_DirectionTop.Checked = true;

                switch (oSheetList.HeadingPlacement)
                {
                    case HeadingPlacementEnum.kHeadingAtBottom:
                        rad_ColHeadingBtm.Checked = true;
                        break;
                    case HeadingPlacementEnum.kHeadingAtTop:
                        rad_ColHeadingTop.Checked = true;
                        break;
                    case HeadingPlacementEnum.kNoHeading:
                        rad_ColHeadingHide.Checked = true;
                        break;
                }

                txb_SheetNoColName.Text = oSheetList.SheetNoColName;
                txb_SheetNameColName.Text = oSheetList.SheetNameColName;

                ckb_EnableAutoWrap.Checked = oSheetList.EnableAutoWrap;

                if (oSheetList.WrapLeft)
                    rad_WrapDirectionLeft.Checked = true;
                else
                    rad_WrapDirectionRight.Checked = true;

                txb_MaxRows.Text = oSheetList.MaxRows.ToString();
                txb_SectionNumber.Text = oSheetList.NumberOfSections.ToString();
            }

            

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

            //Save SheetList Object to AddinGlobal
            AddinGlobal.oSheetList = oSheetList;

            //Export Object to XML in User Folder
            XMLTools.CreateXML(oSheetList, AppFolder + SettingsFile);

            this.Close();
        }

        private void ckb_ShowTitle_CheckedChanged(object sender, EventArgs e)
        {
            txb_Title.Enabled = ckb_ShowTitle.Checked;
        }

        private void ckb_EnableAutoWrap_CheckedChanged(object sender, EventArgs e)
        {
            pnl_AutoWrap.Enabled = ckb_EnableAutoWrap.Checked;
        }


    }
}
