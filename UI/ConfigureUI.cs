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
				SheetListSettings settings = AddinGlobal.AppSettings;

				ckb_ShowTitle.Checked = settings.ShowTitle;
                txb_Title.Text = settings.Title;

                if (settings.Direction == TableDirectionEnum.kTopDownDirection)
                    rad_DirectionBtm.Checked = true;
                else
                    rad_DirectionTop.Checked = true;

                switch (settings.HeadingPlacement)
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

                txb_SheetNoColName.Text = settings.SheetNoColName;
                txb_SheetNameColName.Text = settings.SheetNameColName;

                ckb_EnableAutoWrap.Checked = settings.EnableAutoWrap;

                if (settings.WrapLeft)
                    rad_WrapDirectionLeft.Checked = true;
                else
                    rad_WrapDirectionRight.Checked = true;

                txb_MaxRows.Text = settings.MaxRows.ToString();
                txb_SectionNumber.Text = settings.NumberOfSections.ToString();
                rad_MaxRows.Checked = settings.ControlMaxRows;
                rad_NumberOfSections.Checked = settings.ControlNumberOfSections;

				ckb_UpdateBeforeSave.Checked = settings.UpdateBeforeSave;
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

            AddinGlobal.AppSettings = new SheetListSettings
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
                NumberOfSections = Convert.ToInt32(txb_SectionNumber.Text),
                ControlMaxRows = rad_MaxRows.Checked,
                ControlNumberOfSections = rad_NumberOfSections.Checked,
                UpdateBeforeSave = ckb_UpdateBeforeSave.Checked,
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
