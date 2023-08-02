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

namespace SheetList
{
    public partial class ConfigureUI : Form
    { 
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

            if (AddinGlobal.AppSettings != null)
            {
				var sheetListSettings = AddinGlobal.AppSettings.SheetListSettings;

				ckb_ShowTitle.Checked = sheetListSettings.ShowTitle;
                txb_Title.Text = sheetListSettings.Title;

                if (sheetListSettings.Direction == TableDirectionEnum.kTopDownDirection)
                    rad_DirectionBtm.Checked = true;
                else
                    rad_DirectionTop.Checked = true;

                switch (sheetListSettings.HeadingPlacement)
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

                txb_SheetNoColName.Text = sheetListSettings.SheetNoColName;
                txb_SheetNameColName.Text = sheetListSettings.SheetNameColName;

                ckb_EnableAutoWrap.Checked = sheetListSettings.EnableAutoWrap;

                if (sheetListSettings.WrapLeft)
                    rad_WrapDirectionLeft.Checked = true;
                else
                    rad_WrapDirectionRight.Checked = true;

                txb_MaxRows.Text = sheetListSettings.MaxRows.ToString();
                txb_SectionNumber.Text = sheetListSettings.NumberOfSections.ToString();
                rad_MaxRows.Checked = AddinGlobal.AppSettings.ControlMaxRows;
                rad_NumberOfSections.Checked = AddinGlobal.AppSettings.ControlNumberOfSections;

				ckb_UpdateBeforeSave.Checked = AddinGlobal.AppSettings.UpdateBeforeSave;
            }
        }

        private void btn_SaveSettings_Click(object sender, EventArgs e)
        {
            HeadingPlacementEnum headingPlacement;
            if (rad_ColHeadingTop.Checked)
                headingPlacement = HeadingPlacementEnum.kHeadingAtTop;
            else if (rad_ColHeadingBtm.Checked)
                headingPlacement = HeadingPlacementEnum.kHeadingAtBottom;
            else
                headingPlacement = HeadingPlacementEnum.kNoHeading;

            var sheetListSettings = new SheetListSettings
            {
                Title = txb_Title.Text,
                ShowTitle = ckb_ShowTitle.Checked,
                SheetNoColName = txb_SheetNoColName.Text,
                SheetNameColName = txb_SheetNameColName.Text,
                Direction = rad_DirectionBtm.Checked ? TableDirectionEnum.kTopDownDirection : TableDirectionEnum.kBottomUpDirection,
                HeadingPlacement = headingPlacement,
                WrapLeft = rad_WrapDirectionLeft.Checked,
                EnableAutoWrap = ckb_EnableAutoWrap.Checked,
                MaxRows = Convert.ToInt32(txb_MaxRows.Text),
                NumberOfSections = Convert.ToInt32(txb_SectionNumber.Text)
            };

            AddinGlobal.AppSettings = new AddinSettings
            {
                ControlMaxRows = rad_MaxRows.Checked,
                ControlNumberOfSections = rad_NumberOfSections.Checked,
                UpdateBeforeSave = ckb_UpdateBeforeSave.Checked,
                SheetListSettings = sheetListSettings
            };

            SheetListTools.SaveSettings();

			SheetListTools.CreateEventListener();

            this.Close();
        }

        private void ckb_ShowTitle_CheckedChanged(object sender, EventArgs e)
        {
            txb_Title.Enabled = ckb_ShowTitle.Checked;
        }

        private void ckb_EnableAutoWrap_CheckedChanged(object sender, EventArgs e)
        {
            pnl_AutoWrap.Enabled = ckb_EnableAutoWrap.Checked;
            if (!ckb_EnableAutoWrap.Checked)
            {
                rad_NumberOfSections.Checked = true;
                txb_SectionNumber.Text = "1";
            }
                           
        }

        private void rad_NumberOfSections_CheckedChanged(object sender, EventArgs e)
        {
            txb_SectionNumber.Enabled = true;
            txb_MaxRows.Enabled = false;
            if (Convert.ToInt32(txb_SectionNumber.Text) == 0)
                txb_SectionNumber.Text = "1";
        }

        private void rad_MaxRows_CheckedChanged(object sender, EventArgs e)
        {
            txb_MaxRows.Enabled = true;
            txb_SectionNumber.Enabled = false;
            txb_SectionNumber.Text = "0";
        }
    }
}
