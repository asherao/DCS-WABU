using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

/*TODO
 * -Make the "chose your dcs save game folder" button work.
 * -make it so that if a preset loadout is not already in the saved games location, then make one
//-Finish putting in context menu for the F18, if you want to.
-make loadout naming same name detection
//-document, document, document
//-after completely finishing the F18, document how to add another aircraft. To include ecel and notepad
//and how to get data fast and accurately
//-do another aircraft :) (likely the SU33 next because it uses all 12 store slots)
//make the arrays and weapon data more coding friendly
*/

namespace DCS_Loadout_Calculator_Utility
{
 
    public partial class Form1 : Form
    {
        
        //Initilize F18 Weapon Arrays
        //A2A
        public static int[] weaponWeight_FA18C_AIM120Bx2 = new int[] {0,1016,1016,0,0,0,1016,1016,0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_AIM120Cx2 = new int[] { 0, 1032, 1032, 0, 0, 0, 1032, 1032, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_AIM9Lx2 = new int[] { 0, 697, 0, 0, 0, 0, 0, 697, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_AIM9Mx2 = new int[] { 0, 701, 0, 0, 0, 0, 0, 701, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_AIM9Xx2 = new int[] { 0, 692, 0, 0, 0, 0, 0, 692, 0,0,0,0 };
        public static int[] weaponWeight_FA18C_CAP9Mx2 = new int[] { 0, 697, 0, 0, 0, 0, 0, 697, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_AIM120B = new int[] { 0, 467, 467, 348, 0, 348, 467, 467, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_AIM120C = new int[] { 0, 476, 476, 357, 0, 357, 476, 476, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_AIM7M = new int[] { 0, 626, 626, 507, 0, 507, 626, 626, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_AIM7F = new int[] { 0, 626, 626, 507, 0, 507, 626, 626, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_AIM7MH = new int[] { 0, 626, 626, 507, 0, 507, 626, 626, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_AIM9L = new int[] { 190, 408, 0, 0, 0, 0, 0, 408, 190, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_AIM9M = new int[] { 192, 410, 0, 0, 0, 0, 0, 410, 192, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_AIM9X = new int[] { 185, 406, 0, 0, 0, 0, 0, 406, 185, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_CAP9M = new int[] { 190, 408, 0, 0, 0, 0, 0, 408, 190, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_ANASQT50TCTS = new int[] { 139, 0, 0, 0, 0, 0, 0, 0, 139, 0, 0, 0 };
        //A2G
        public static int[] weaponWeight_FA18C_CBU99x2 = new int[] { 0, 1153, 1153, 0, 1153, 0, 1153, 1153, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_GBU12x2 = new int[] { 0, 1387, 1387, 0, 0, 0, 1387, 1387, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_Mk20Rockeyex2 = new int[] { 0, 1153, 1153, 0, 1153, 0, 1153, 1153, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_ = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_Mk82x2 = new int[] { 0, 1237, 1237, 0, 1237, 0, 1237, 1237, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_Mk82SnakeEyex2 = new int[] { 0, 1197, 1197, 0, 1197, 0, 1197, 1197, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_Mk82Yx2 = new int[] { 0, 1197, 1197, 0, 1197, 0, 1197, 1197, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_Mk83x2 = new int[] { 0, 2145, 2145, 0, 0, 0, 2145, 2145, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_BDU33x6 = new int[] { 0, 432, 432, 0, 0, 0, 432, 432, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_GBU38x2 = new int[] { 0, 1237, 1237, 0, 0, 0, 1237, 1237, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_CBU99 = new int[] { 0, 489, 489, 0, 489, 0, 489, 489, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_GBU10 = new int[] { 0, 2562, 2562, 0, 0, 0, 2562, 2562, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_GBU12 = new int[] { 0, 606, 606, 0, 0, 0, 606, 606, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_GBU16 = new int[] { 0, 1243, 1243, 0, 0, 0, 1243, 1243, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_GBU31 = new int[] { 0, 1971, 1971, 0, 0, 0, 1971, 1971, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_GBU31V3B = new int[] {0,2163,2163,0,0,0,2163,2163,0,0,0,0};
        public static int[] weaponWeight_FA18C_GBU38 = new int[] { 0, 531, 531, 0, 0, 0, 531, 531, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_Mk20 = new int[] { 0, 489, 489, 0, 489, 0, 489, 489, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_Mk82 = new int[] { 0, 531, 531, 0, 531, 0, 531, 531, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_Mk82SnakeEye = new int[] {0,511,511,0,511,0,511,511,0,0,0,0};
        public static int[] weaponWeight_FA18C_Mk82Y = new int[] { 0, 511, 511, 0, 511, 0, 511, 511, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_Mk83 = new int[] { 0, 985, 985, 0, 985, 0, 985, 985, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_Mk84 = new int[] { 0, 1971, 1971, 0, 1971, 0, 1971, 1971, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_AGM154A = new int[] { 0, 1069, 1069, 0, 0, 0, 1069, 1069, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_AGM154C = new int[] { 0, 1067, 1067, 0, 0, 0, 1067, 1067, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_AGM88 = new int[] { 0, 796, 796, 0, 0, 0, 796, 796, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_AGM154Ax2 = new int[] { 0, 2249, 2249, 0, 0, 0, 2249, 2249, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_AGM154Cx2 = new int[] { 0, 2244, 2244, 0, 0, 0, 2244, 2244, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_AGM65E = new int[] { 0, 761, 761, 0, 0, 0, 761, 761, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_AGM65F = new int[] { 0, 794, 794, 0, 0, 0, 794, 794, 0, 0, 0, 0 };
        public static int[] weaponWeight_FA18C_4ZUNIMK71x2 = new int[] {0,2114,2114,0,0,0,2114,2114,0,0,0,0};
        public static int[] weaponWeight_FA18C_rocketsM151HEx2 = new int[] {0,1455,1455,0,0,0,1455,1455,0,0,0,0};
        public static int[] weaponWeight_FA18C_rocketsMK151HEx2 = new int[] {0,679,679,0,0,0,679,679,0,0,0,0};
        public static int[] weaponWeight_FA18C_rocketsM5HEx2 = new int[] {0,628,0,0,0,0,0,628,0,0,0,0};
        public static int[] weaponWeight_FA18C_4ZUNIMK71 = new int[] {0,1144,1144,0,0,0,1144,1144,0,0,0,0};
        public static int[] weaponWeight_FA18C_rocketsM151HE = new int[] {0,816,816,0,0,0,816,816,0,0,0,0};
        public static int[] weaponWeight_FA18C_rocketsMK151HE = new int[] {0,428,428,0,0,0,428,428,0,0,0,0};
        public static int[] weaponWeight_FA18C_rocketsM5HE = new int[] {0,401,0,0,0,0,0,401,0,0,0,0};
        public static int[] weaponWeight_FA18C_FPU8AFuelTank330gallons = new int[] { 0, 0, 291, 0, 291, 0, 291, 0, 0, 0, 0, 0 };

        private int station1Weight;
        private int station2Weight;
        private int station3Weight;
        private int station4Weight;
        private int station5Weight;
        private int station6Weight;
        private int station7Weight;
        private int station8Weight;
        private int station9Weight;
        private int station10Weight;
        private int station11Weight;
        private int station12Weight;

        private int station1FuelWeight;
        private int station2FuelWeight;
        private int station3FuelWeight;
        private int station4FuelWeight;
        private int station5FuelWeight;
        private int station6FuelWeight;
        private int station7FuelWeight;
        private int station8FuelWeight;
        private int station9FuelWeight;
        private int station10FuelWeight;
        private int station11FuelWeight;
        private int station12FuelWeight;
        private int stationAllFuelWeight;
        private int totalFuelWeight;


        string userNamedExport = "123 Custom Loadout";//implement a box the suer can type in to name the export
        string station1StoreExport = "Station-1-ID-Placeholder";
        string station2StoreExport = "Station-2-ID-Placeholder";
        string station3StoreExport = "Station-3-ID-Placeholder";
        string station4StoreExport = "Station-4-ID-Placeholder";
        string station5StoreExport = "Station-5-ID-Placeholder";
        string station6StoreExport = "Station-6-ID-Placeholder";
        string station7StoreExport = "Station-7-ID-Placeholder";
        string station8StoreExport = "Station-8-ID-Placeholder";
        string station9StoreExport = "Station-9-ID-Placeholder";
        string station10StoreExport = "Station-10-ID-Placeholder";
        string station11StoreExport = "Station-11-ID-Placeholder";
        string station12StoreExport = "Station-12-ID-Placeholder";
        string emptyString = "";
        string unitType;

        private string selectedAircraft;
        private string userSavedGameLocation;
        private string folderNameFullPath;
        private string userFA18CLuaLocation;

      

        
        public Form1()
        {
            InitializeComponent();
            //is this where i put the main code?
            
            textBox_loadoutName.Enabled = false;//makes it so that the user does not enter a value for the box before selecting an airctaft
            button_exportLoadout.Enabled = false;
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            //when making the custom function have it do something like
            //int x = Addup(4,5,6);
            //except
            //int station1WeaponWeight = tableLookup(FA18C, station1, MK83)
            //and hopefully that would return station1WeaponWeight as something like 2480.
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void InternalFuelLabel_Click(object sender, EventArgs e)
        {

        }

        private void Label11_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void SelectAirctaftListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectAirctaftListBox.SelectedItem == "F/A-18")//this enables the export textbox on the mentioned aircraft
            {
                textBox_loadoutName.Enabled = true;
                button_exportLoadout.Enabled = true;
            }
            else
            {
                textBox_loadoutName.Enabled = false;
                button_exportLoadout.Enabled = false;
            }

            //when the user click the aircraft they want, the aircraft variable is set and the gui is changed to match the number of available stores for the aircraft
            string selectedAircraft = selectAirctaftListBox.GetItemText(selectAirctaftListBox.SelectedItem);
            //MessageBox.Show(selectedAircraft);//debugging. and it actually works! the text of the selected aircraft actually populates selectedAircraft every time
            gunTrackBar.Value = 0;
            internalFuelTrackBar.Value = 0;
            gunTrackBar.Maximum = 0;
            internalFuelTrackBar.Maximum = 0;

            //clears the right click menus every time that a plane selection is changed
            station1Label.ContextMenuStrip = contextMenuStrip_blank;
            station2Label.ContextMenuStrip = contextMenuStrip_blank;
            station3Label.ContextMenuStrip = contextMenuStrip_blank;
            station4Label.ContextMenuStrip = contextMenuStrip_blank;
            station5Label.ContextMenuStrip = contextMenuStrip_blank;
            station6Label.ContextMenuStrip = contextMenuStrip_blank;
            station7Label.ContextMenuStrip = contextMenuStrip_blank;
            station8Label.ContextMenuStrip = contextMenuStrip_blank;
            station9Label.ContextMenuStrip = contextMenuStrip_blank;
            station10Label.ContextMenuStrip = contextMenuStrip_blank;
            station11Label.ContextMenuStrip = contextMenuStrip_blank;
            station12Label.ContextMenuStrip = contextMenuStrip_blank;

            if (selectedAircraft == "F/A-18")
            {
                //ini for FA18
                station1Label.ContextMenuStrip = contextMenuStrip_Station1_FA18C3;
                station2Label.ContextMenuStrip = contextMenuStrip_Station2_FA18C3;
                station3Label.ContextMenuStrip = contextMenuStrip_Station3_FA18C3;
                station4Label.ContextMenuStrip = contextMenuStrip_Station4_FA18C3;
                station5Label.ContextMenuStrip = contextMenuStrip_Station5_FA18C3;
                station6Label.ContextMenuStrip = contextMenuStrip_Station6_FA18C3;
                station7Label.ContextMenuStrip = contextMenuStrip_Station7_FA18C3;
                station8Label.ContextMenuStrip = contextMenuStrip_Station8_FA18C3;
                station9Label.ContextMenuStrip = contextMenuStrip_Station9_FA18C3;               

                gunTrackBar.Maximum = 445;
                gunTrackBar.Value = 445;
                internalFuelTrackBar.Maximum = 10803;
                internalFuelTrackBar.Value = 10803;
                int internalFuelWeightInt = internalFuelTrackBar.Value;

                //fuelWeightTextBox.Text = internalFuelTextBox.Value + fuelValue //placeholders
                emptyTextBox.Text = "25642";
                int emptyWeightInt = int.Parse(emptyTextBox.Text);
                weaponsTextBox.Text = Convert.ToString(gunTrackBar.Value); //something+something+moreSomething
                int weaponsWeightInt = int.Parse(weaponsTextBox.Text);
                maxTextBox.Text = "51899";
               
                int gunWeightInt = gunTrackBar.Value;
                int totalWeightInt = internalFuelWeightInt + emptyWeightInt + gunWeightInt + station1Weight + station2Weight + station3Weight + station4Weight + station5Weight + station6Weight + station7Weight + station8Weight + station9Weight + station10Weight + station11Weight + station12Weight; //+externalFuelWeightInt+ weaponsWeightInt 
                string totalWeightString = totalWeightInt.ToString();
                totalTextBox.Text = totalWeightString;

                station1Label.Visible = true;
                station2Label.Visible = true;
                station3Label.Visible = true;
                station4Label.Visible = true;
                station5Label.Visible = true;
                station6Label.Visible = true;
                station7Label.Visible = true;
                station8Label.Visible = true;
                station9Label.Visible = true;
                station10Label.Visible = false;
                station11Label.Visible = false;
                station12Label.Visible = false;

                station1ComboBox.Visible = true;
                station2ComboBox.Visible = true;
                station3ComboBox.Visible = true;
                station4ComboBox.Visible = true;
                station5ComboBox.Visible = true;
                station6ComboBox.Visible = true;
                station7ComboBox.Visible = true;
                station8ComboBox.Visible = true;
                station9ComboBox.Visible = true;
                station10ComboBox.Visible = false;
                station11ComboBox.Visible = false;
                station12ComboBox.Visible = false;
                
                //ini for station store drop down options
                string[] station1Stores_FA18C = new string[] {"Empty", "AIM-9L", "AIM-9M", "AIM-9X","CAP-9M", "AN/ASQ-T50 TCTS Pod"};
                station1ComboBox.DataSource = station1Stores_FA18C;
                string[] station2Stores_FA18C = new string[] { "Empty", "AIM-120B x2", "AIM-120C x2", "AIM-9L x2",
                    "AIM-9M x2", "AIM-9X x2", "CAP-9M x2", "AIM-120B", "AIM-120C", "AIM-7M", "AIM-7F", "AIM-7MH",
                    "AIM-9L", "AIM-9M", "AIM-9X", "CAP-9M", "CBU-99 x2", "GBU-12 x2", "Mk-20 Rockeye x2", "Mk-82 x2",
                    "Mk-82 SnakeEye x2", "Mk-82Y x2", "Mk-83 x2", "BDU-33 x6", "GBU-38 x2", "CBU-99", "GBU-10",
                    "GBU-12", "GBU-16", "GBU-31", "GBU-31(V)3/B", "GBU-38", "Mk-20", "Mk-82", "Mk-82 SnakeEye",
                    "Mk-82Y", "Mk-83", "Mk-84", "AGM-154A", "AGM-154C", "AGM-88", "AGM-154A x2", "AGM-154C x2",
                    "AGM-65E", "AGM-65F", "4 ZUNI MK 71 x2", "19 2.75' rockets M151 HE x2",
                    "7 2.75' rockets MK151 (HE) x2", "7 2.75' rockets M5 (HE) x2", "4 ZUNI MK 71",
                    "19 2.75' rockets M151 HE", "7 2.75' rockets MK151 (HE)", "7 2.75' rockets M5 (HE)" };
                station2ComboBox.DataSource = station2Stores_FA18C;
                string[] station3Stores_FA18C = new string[] { "Empty", "AIM-120B x2", "AIM-120C x2", "AIM-120B",
                    "AIM-120C", "AIM-7M", "AIM-7F", "AIM-7MH", "CBU-99 x2", "GBU-12 x2", "Mk-20 Rockeye x2",
                    "Mk-82 x2", "Mk-82 SnakeEye x2", "Mk-82Y x2", "Mk-83 x2", "BDU-33 x6", "GBU-38 x2", "CBU-99",
                    "GBU-10", "GBU-12", "GBU-16", "GBU-31", "GBU-31(V)3/B", "GBU-38", "Mk-20", "Mk-82", "Mk-82 SnakeEye",
                    "Mk-82Y", "Mk-83", "Mk-84", "AGM-154A", "AGM-154C", "AGM-88", "AGM-154A x2", "AGM-154C x2", "AGM-65E",
                    "AGM-65F", "4 ZUNI MK 71 x2", "19 2.75' rockets M151 HE x2", "7 2.75' rockets MK151 (HE) x2",
                    "4 ZUNI MK 71", "19 2.75' rockets M151 HE", "7 2.75' rockets MK151 (HE)",
                    "FPU-8A Fuel Tank 330 gallons" };
                station3ComboBox.DataSource = station3Stores_FA18C;
                string[] station4Stores_FA18C = new string[] { "Empty", "AIM-120B", "AIM-120C", "AIM-7M", "AIM-7F", "AIM-7MH", };
                station4ComboBox.DataSource = station4Stores_FA18C;
                string[] station5Stores_FA18C = new string[] { "Empty","CBU-99 x2", "Mk-20 Rockeye x2",
                    "Mk-82 x2", "Mk-82 SnakeEye x2", "Mk-82Y x2", "CBU-99", "Mk-20", "Mk-82", "Mk-82 SnakeEye",
                    "Mk-82Y", "Mk-83", "Mk-84","FPU-8A Fuel Tank 330 gallons" };
                station5ComboBox.DataSource = station5Stores_FA18C;
                string[] station6Stores_FA18C = new string[] { "Empty", "AIM-120B", "AIM-120C", "AIM-7M", "AIM-7F", "AIM-7MH" };
                station6ComboBox.DataSource = station6Stores_FA18C;
                string[] station7Stores_FA18C = new string[] { "Empty", "AIM-120B x2", "AIM-120C x2", "AIM-120B",
                    "AIM-120C", "AIM-7M", "AIM-7F", "AIM-7MH", "CBU-99 x2", "GBU-12 x2", "Mk-20 Rockeye x2",
                    "Mk-82 x2", "Mk-82 SnakeEye x2", "Mk-82Y x2", "Mk-83 x2", "BDU-33 x6", "GBU-38 x2", "CBU-99",
                    "GBU-10", "GBU-12", "GBU-16", "GBU-31", "GBU-31(V)3/B", "GBU-38", "Mk-20", "Mk-82", "Mk-82 SnakeEye",
                    "Mk-82Y", "Mk-83", "Mk-84", "AGM-154A", "AGM-154C", "AGM-88", "AGM-154A x2", "AGM-154C x2", "AGM-65E",
                    "AGM-65F", "4 ZUNI MK 71 x2", "19 2.75' rockets M151 HE x2", "7 2.75' rockets MK151 (HE) x2",
                    "4 ZUNI MK 71", "19 2.75' rockets M151 HE", "7 2.75' rockets MK151 (HE)",
                    "FPU-8A Fuel Tank 330 gallons" };
                station7ComboBox.DataSource = station7Stores_FA18C;
                string[] station8Stores_FA18C = new string[] { "Empty", "AIM-120B x2", "AIM-120C x2", "AIM-9L x2",
                    "AIM-9M x2", "AIM-9X x2", "CAP-9M x2", "AIM-120B", "AIM-120C", "AIM-7M", "AIM-7F", "AIM-7MH",
                    "AIM-9L", "AIM-9M", "AIM-9X", "CAP-9M", "CBU-99 x2", "GBU-12 x2", "Mk-20 Rockeye x2", "Mk-82 x2",
                    "Mk-82 SnakeEye x2", "Mk-82Y x2", "Mk-83 x2", "BDU-33 x6", "GBU-38 x2", "CBU-99", "GBU-10",
                    "GBU-12", "GBU-16", "GBU-31", "GBU-31(V)3/B", "GBU-38", "Mk-20", "Mk-82", "Mk-82 SnakeEye",
                    "Mk-82Y", "Mk-83", "Mk-84", "AGM-154A", "AGM-154C", "AGM-88", "AGM-154A x2", "AGM-154C x2",
                    "AGM-65E", "AGM-65F", "4 ZUNI MK 71 x2", "19 2.75' rockets M151 HE x2",
                    "7 2.75' rockets MK151 (HE) x2", "7 2.75' rockets M5 (HE) x2", "4 ZUNI MK 71",
                    "19 2.75' rockets M151 HE", "7 2.75' rockets MK151 (HE)", "7 2.75' rockets M5 (HE)" };
                station8ComboBox.DataSource = station8Stores_FA18C;
                string[] station9Stores_FA18C = new string[] { "Empty", "AIM-9L", "AIM-9M", "AIM-9X", "CAP-9M", "AN/ASQ-T50 TCTS Pod" };
                station9ComboBox.DataSource = station9Stores_FA18C;

                CalculateWeights(); //makes sure that the numbers on the left change every time something is changed
            }
            else if (selectedAircraft == "Su-33")
            {
                //ini for SU33
                //in progress. make it look like the F18
                gunTrackBar.Maximum = 324;
                internalFuelTrackBar.Maximum = 20944;
                gunTrackBar.Value = 324;
                internalFuelTrackBar.Value = 20944;
                //fuelWeightTextBox.Text = "" + internalFuelTrackBar.Value;// + fuelValue //placeholders
                emptyTextBox.Text = "43387";//need to confirm
                //weaponsTextBox.Text = something+something+moreSomething
                maxTextBox.Text = "72753";

                station1Label.Visible = true;
                station2Label.Visible = true;
                station3Label.Visible = true;
                station4Label.Visible = true;
                station5Label.Visible = true;
                station6Label.Visible = true;
                station7Label.Visible = true;
                station8Label.Visible = true;
                station9Label.Visible = true;
                station10Label.Visible = true;
                station11Label.Visible = true;
                station12Label.Visible = true;

                station1ComboBox.Visible = true;
                station2ComboBox.Visible = true;
                station3ComboBox.Visible = true;
                station4ComboBox.Visible = true;
                station5ComboBox.Visible = true;
                station6ComboBox.Visible = true;
                station7ComboBox.Visible = true;
                station8ComboBox.Visible = true;
                station9ComboBox.Visible = true;
                station10ComboBox.Visible = true;
                station11ComboBox.Visible = true;
                station12ComboBox.Visible = true;

                CalculateWeights();

                string[] station1Stores_SU33 = new string[] { "Empty", "weapon11", "weapon12", "weapon13" };
                station1ComboBox.DataSource = station1Stores_SU33;
                string[] station2Stores_SU33 = new string[] { "Empty", "weapon21", "weapon22", "weapon23" };
                station2ComboBox.DataSource = station2Stores_SU33;
                string[] station3Stores_SU33 = new string[] { "Empty", "weapon31", "weapon32", "weapon33" };
                station3ComboBox.DataSource = station3Stores_SU33;
            }
            //TODO: Add more aircraft and their available stores positions
        }

        private void GunBar_Scroll(object sender, EventArgs e)
        {
            CalculateWeights();//recalculates values when the bar is moved
        }

        private void EmptyTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void InternalFuelTrackBar_Scroll(object sender, EventArgs e)
        {
            CalculateWeights();//recalculates values when the bar is moved
        }

        private void WeaponsTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        public void CalculateWeights()
        {
            //calculate weights does what you'd think it does. 

            int emptyWeightInt = int.Parse(emptyTextBox.Text);
            //this is the weight of all the fuel tanks to be used later
            stationAllFuelWeight = station1FuelWeight + station2FuelWeight + station3FuelWeight + station4FuelWeight
                + station5FuelWeight + station6FuelWeight + station7FuelWeight + station8FuelWeight + station9FuelWeight
                + station10FuelWeight + station11FuelWeight + station12FuelWeight;
            //time to make the stationAllFuelWeight modify the tracking bar
            totalFuelWeight = stationAllFuelWeight + internalFuelTrackBar.Value;
            fuelWeightTextBox.Text = Convert.ToString(totalFuelWeight);
            //fuelWeightTextBox.Text = "" + internalFuelTrackBar.Value + Convert.ToInt32(stationAllFuelWeight);// + fuelValue //placeholders
            int internalFuelWeightInt = internalFuelTrackBar.Value;
            //internalFuelTrackBar.Minimum = stationAllFuelWeight; wrong. you shouldnt be modifying the internal fuel slider, you
            //shouldbe changing the fuel text (ammount) itself

            //weaponsTextBox.Text = Convert.ToString(gunTrackBar.Value); //something+something+moreSomething
            weaponsTextBox.Text = Convert.ToString(gunTrackBar.Value + station1Weight + station2Weight + 
                station3Weight + station4Weight + station5Weight + station6Weight + station7Weight + 
                station8Weight + station9Weight + station10Weight + station11Weight + station12Weight ); //something+something+moreSomething
            //MessageBox.Show(Convert.ToString(gunTrackBar.Value + station3Weight));//debug the weight text
            int weaponsWeightInt = int.Parse(weaponsTextBox.Text);
            //the sum of all the weight is all the internal fuel + empty weight + 
            //weapons + external fuel(?) + gun
            //might make my own function for that
            //int externalFuelWeightInt = int32.Parse(xxx.Text);//future capes
            int gunWeightInt = gunTrackBar.Value;
            int totalWeightInt = internalFuelWeightInt + emptyWeightInt + gunWeightInt + station1Weight + 
                station2Weight + station3Weight + station4Weight + station5Weight + station6Weight + 
                station7Weight + station8Weight + station9Weight + station10Weight + station11Weight + 
                station12Weight + stationAllFuelWeight; //+externalFuelWeightInt+ weaponsWeightInt ; //+externalFuelWeightInt+ weaponsWeightInt 
            string totalWeightString = totalWeightInt.ToString();
            totalTextBox.Text = totalWeightString;           
            totalTrackBar.Value = ((totalWeightInt*100) / Convert.ToInt32(maxTextBox.Text));
            deltaTextBox.Text = Convert.ToString(totalWeightInt - Convert.ToInt32(maxTextBox.Text));
            //MessageBox.Show(Convert.ToString(stationAllFuelWeight));
            //set the percents labels
            label_GunPercent.Text = Convert.ToString((gunTrackBar.Value*100)/gunTrackBar.Maximum) + "%";
            label_InternalFuelPercent.Text = Convert.ToString((internalFuelTrackBar.Value * 100) / internalFuelTrackBar.Maximum) + "%";
            label_TotalPercent.Text = Convert.ToString(totalTrackBar.Value) +"%";
        }

        private void Total2Label_Click(object sender, EventArgs e)
        {
            CalculateWeights();//debug. click Total to calculate the weights
        }
        private void TotalLabel_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Convert.ToString(totalTrackBar.Value));//debug
        }
        private void Station1ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string station1Store = station1ComboBox.SelectedValue.ToString();
            int station1NumberTemp = 1;
            //int station1Weight = 0;//this was keeping the value zero for some reason
            station1Weight = GetStation1Weight_FA18C();//requests the weight of the named ' station3Store'
            //MessageBox.Show(Convert.ToString(station1Weight)); //debugging          
            CalculateWeights();
        }
        private void Station2ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string station2Store = station2ComboBox.SelectedValue.ToString();
            int stationNumberTemp = 2;
            station2Weight = GetStation2Weight_FA18C();//requests the weight of the named ' station3Store'         
            CalculateWeights();
        }
        private void Station3ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string station3Store = station3ComboBox.SelectedValue.ToString();
            int station3NumberTemp = 3;
            //int station3Weight = 0;//this was keeping the value zero for some reason
            station3Weight = GetStation3Weight_FA18C();//requests the weight of the named ' station3Store'
            //MessageBox.Show(Convert.ToString(station3Weight)); //debugging          
            
            //this sets the poundage of the fuel tank only when it is selected
            if (station3Store == "FPU-8A Fuel Tank 330 gallons")//adding ' && selectedAircraft == "F/A-18"' does not seem to work
            {          
                station3FuelWeight = 2244;
                //MessageBox.Show(Convert.ToString(station3FuelWeight));
            }
            else
            {
                station3FuelWeight = 0;
                //MessageBox.Show(Convert.ToString(station3FuelWeight));
            }
            CalculateWeights();
        }
        private void Station4ComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string station4Store = station4ComboBox.SelectedValue.ToString();
            station4Weight = GetStation4Weight_FA18C();
            //MessageBox.Show(Convert.ToString(station4Weight)); //debugging          
            CalculateWeights();
        }
        private void Station5ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string station5Store = station5ComboBox.SelectedValue.ToString();
            station5Weight = GetStation5Weight_FA18C();
            //MessageBox.Show(Convert.ToString(station5Weight)); //debugging  

            //this sets the poundage of the fuel tank only when it is selected
            if (station5Store == "FPU-8A Fuel Tank 330 gallons")//adding ' && selectedAircraft == "F/A-18"' does not seem to work
            {
                station5FuelWeight = 2244;
                //MessageBox.Show(Convert.ToString(station5FuelWeight));
            }
            else
            {
                station5FuelWeight = 0;
                //MessageBox.Show(Convert.ToString(station5FuelWeight));
            }
            CalculateWeights();
        }
        private void Station6ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string station6Store = station6ComboBox.SelectedValue.ToString();
            station6Weight = GetStation6Weight_FA18C();
            CalculateWeights();
        }
        private void Station7ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string station7Store = station7ComboBox.SelectedValue.ToString();
            station7Weight = GetStation7Weight_FA18C();

            //this sets the poundage of the fuel tank only when it is selected
            if (station7Store == "FPU-8A Fuel Tank 330 gallons")//adding ' && selectedAircraft == "F/A-18"' does not seem to work
            {
                station7FuelWeight = 2244;
                //MessageBox.Show(Convert.ToString(station7FuelWeight));
            }
            else
            {
                station7FuelWeight = 0;
                //MessageBox.Show(Convert.ToString(station7FuelWeight));
            }
            CalculateWeights();
        }
        private void Station8ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string station8Store = station8ComboBox.SelectedValue.ToString();
            station8Weight = GetStation8Weight_FA18C();
            CalculateWeights();
        }
        private void Station9ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string station9Store = station9ComboBox.SelectedValue.ToString();
            station9Weight = GetStation9Weight_FA18C();
            CalculateWeights();
        }
        public int GetStation1Weight_FA18C()
        {
            ///////////////////////////////////this is working, but i want to post the entire weapons arrays tables individually , then dynamically reference
            //////each one for the aircraft=============================
            int stationNumber = 1;


            if (station1ComboBox.SelectedValue.ToString() == "AIM-9L")
            {
                //int[] weaponWeight_FA18C_MK82 = new int[] { 0, 531, 531, 0, 531, 0, 531, 531, 0, 0, 0, 0 };//old news baby!!!
                return weaponWeight_FA18C_AIM9L[stationNumber - 1];
                //return 31;//debugging
            }
            else if (station1ComboBox.SelectedValue.ToString() == "AIM-9M")
            {
                return weaponWeight_FA18C_AIM9M[stationNumber - 1];
            }
            else if (station1ComboBox.SelectedValue.ToString() == "AIM-9X")
            {
                return weaponWeight_FA18C_AIM9X[stationNumber - 1];
            }
            else if (station1ComboBox.SelectedValue.ToString() == "CAP-9M")
            {
                return weaponWeight_FA18C_CAP9M[stationNumber - 1];
            }
            else if (station1ComboBox.SelectedValue.ToString() == "AN/ASQ-T50 TCTS Pod")
            {
                return weaponWeight_FA18C_ANASQT50TCTS[stationNumber - 1];
            }

            else
            {
                return 0;
            }
        }
        public int GetStation2Weight_FA18C()
        {
            ///////////////////////////////////this is working, but i want to post the entire weapons arrays tables individually , then dynamically reference
            //////each one for the aircraft=============================
            int stationNumber = 2;


            if (station2ComboBox.SelectedValue.ToString() == "AIM-120B x2")
            {
                //int[] weaponWeight_FA18C_MK82 = new int[] { 0, 531, 531, 0, 531, 0, 531, 531, 0, 0, 0, 0 };//old news baby!!!
                return weaponWeight_FA18C_AIM120Bx2[stationNumber - 1];
                //return 31;//debugging
            }
            else if (station2ComboBox.SelectedValue.ToString() == "AIM-120C x2")
            {
                return weaponWeight_FA18C_AIM120Cx2[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "AIM-9L x2")
            {
                return weaponWeight_FA18C_AIM9Lx2[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "AIM-9M x2")
            {
                return weaponWeight_FA18C_AIM9Mx2[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "AIM-9X x2")
            {
                return weaponWeight_FA18C_AIM9Xx2[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "CAP-9M x2")
            {
                return weaponWeight_FA18C_CAP9Mx2[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "AIM-120B")
            {
                return weaponWeight_FA18C_AIM120B[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "AIM-120C")
            {
                return weaponWeight_FA18C_AIM120C[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "AIM-7M")
            {
                return weaponWeight_FA18C_AIM7M[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "AIM-7F")
            {
                return weaponWeight_FA18C_AIM7F[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "AIM-7MH")
            {
                return weaponWeight_FA18C_AIM7MH[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "AIM-9L")
            {
                return weaponWeight_FA18C_AIM9L[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "AIM-9M")
            {
                return weaponWeight_FA18C_AIM9M[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "AIM-9X")
            {
                return weaponWeight_FA18C_AIM9X[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "CAP-9M")
            {
                return weaponWeight_FA18C_CAP9M[stationNumber - 1];
            }
            //a2g
            /*
            else if (station2ComboBox.SelectedValue.ToString() == "")
            {
                return weaponWeight_FA18C_[stationNumber - 1];
            }
            */
            else if (station2ComboBox.SelectedValue.ToString() == "CBU-99 x2")
            {
                return weaponWeight_FA18C_CBU99x2[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "GBU-12 x2")
            {
                return weaponWeight_FA18C_GBU12x2[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "Mk-20 Rockeye x2")
            {
                return weaponWeight_FA18C_Mk20Rockeyex2[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "Mk-82 x2")
            {
                return weaponWeight_FA18C_Mk82x2[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "Mk-82 SnakeEye x2")
            {
                return weaponWeight_FA18C_Mk82SnakeEyex2[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "Mk-82Y x2")
            {
                return weaponWeight_FA18C_Mk82Yx2[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "Mk-83 x2")
            {
                return weaponWeight_FA18C_Mk83x2[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "BDU-33 x6")
            {
                return weaponWeight_FA18C_BDU33x6[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "GBU-38 x2")
            {
                return weaponWeight_FA18C_GBU38x2[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "CBU-99")
            {
                return weaponWeight_FA18C_CBU99[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "GBU-10")
            {
                return weaponWeight_FA18C_GBU10[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "GBU-12")
            {
                return weaponWeight_FA18C_GBU12[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "GBU-16")
            {
                return weaponWeight_FA18C_GBU16[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "GBU-31")
            {
                return weaponWeight_FA18C_GBU31[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "GBU-31(V)3/B")
            {
                return weaponWeight_FA18C_GBU31V3B[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "GBU-38")
            {
                return weaponWeight_FA18C_GBU38[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "Mk-20")
            {
                return weaponWeight_FA18C_Mk20[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "Mk-82")
            {
                return weaponWeight_FA18C_Mk82[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "Mk-82 SnakeEye")
            {
                return weaponWeight_FA18C_Mk82SnakeEye[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "Mk-82Y")
            {
                return weaponWeight_FA18C_Mk82Y[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "Mk-83")
            {
                return weaponWeight_FA18C_Mk83[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "Mk-84")
            {
                return weaponWeight_FA18C_Mk84[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "AGM-154A")
            {
                return weaponWeight_FA18C_AGM154A[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "AGM-154C")
            {
                return weaponWeight_FA18C_AGM154C[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "AGM-88")
            {
                return weaponWeight_FA18C_AGM88[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "AGM-154A x2")
            {
                return weaponWeight_FA18C_AGM154Ax2[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "AGM-154C x2")
            {
                return weaponWeight_FA18C_AGM154Cx2[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "AGM-65E")
            {
                return weaponWeight_FA18C_AGM65E[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "AGM-65F")
            {
                return weaponWeight_FA18C_AGM65F[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "4 ZUNI MK 71 x2")
            {
                return weaponWeight_FA18C_4ZUNIMK71x2[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "19 2.75' rockets M151 HE x2")
            {
                return weaponWeight_FA18C_rocketsM151HEx2[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "7 2.75' rockets MK151 (HE) x2")
            {
                return weaponWeight_FA18C_rocketsMK151HEx2[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "7 2.75' rockets M5 (HE) x2")
            {
                return weaponWeight_FA18C_rocketsM5HEx2[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "4 ZUNI MK 71")
            {
                return weaponWeight_FA18C_4ZUNIMK71[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "19 2.75' rockets M151 HE")
            {
                return weaponWeight_FA18C_rocketsM151HE[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "7 2.75' rockets MK151 (HE)")
            {
                return weaponWeight_FA18C_rocketsMK151HE[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "7 2.75' rockets M5 (HE)")
            {
                return weaponWeight_FA18C_rocketsM5HE[stationNumber - 1];
            }
            else if (station2ComboBox.SelectedValue.ToString() == "FPU-8A Fuel Tank 330 gallons")
            {
                return weaponWeight_FA18C_FPU8AFuelTank330gallons[stationNumber - 1];
            }
            else
            {
                return 0;
            }
        }
        public int GetStation3Weight_FA18C()
        {
            ///////////////////////////////////this is working, but i want to post the entire weapons arrays tables individually , then dynamically reference
            //////each one for the aircraft=============================
            int stationNumber = 3;


            if (station3ComboBox.SelectedValue.ToString() == "AIM-120B x2")
            {
                //int[] weaponWeight_FA18C_MK82 = new int[] { 0, 531, 531, 0, 531, 0, 531, 531, 0, 0, 0, 0 };//old news baby!!!
                return weaponWeight_FA18C_AIM120Bx2[stationNumber - 1];
                //return 31;//debugging
            }
            else if (station3ComboBox.SelectedValue.ToString() == "AIM-120C x2")
            {
                return weaponWeight_FA18C_AIM120Cx2[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "AIM-9L x2")
            {
                return weaponWeight_FA18C_AIM9Lx2[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "AIM-9M x2")
            {
                return weaponWeight_FA18C_AIM9Mx2[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "AIM-9X x2")
            {
                return weaponWeight_FA18C_AIM9Xx2[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "CAP-9M x2")
            {
                return weaponWeight_FA18C_CAP9Mx2[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "AIM-120B")
            {
                return weaponWeight_FA18C_AIM120B[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "AIM-120C")
            {
                return weaponWeight_FA18C_AIM120C[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "AIM-7M")
            {
                return weaponWeight_FA18C_AIM7M[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "AIM-7F")
            {
                return weaponWeight_FA18C_AIM7F[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "AIM-7MH")
            {
                return weaponWeight_FA18C_AIM7MH[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "AIM-9L")
            {
                return weaponWeight_FA18C_AIM9L[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "AIM-9M")
            {
                return weaponWeight_FA18C_AIM9M[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "AIM-9X")
            {
                return weaponWeight_FA18C_AIM9X[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "CAP-9M")
            {
                return weaponWeight_FA18C_CAP9M[stationNumber - 1];
            }
            //a2g
            /*
            else if (station3ComboBox.SelectedValue.ToString() == "")
            {
                return weaponWeight_FA18C_[stationNumber - 1];
            }
            */
            else if (station3ComboBox.SelectedValue.ToString() == "CBU-99 x2")
            {
                return weaponWeight_FA18C_CBU99x2[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "GBU-12 x2")
            {
                return weaponWeight_FA18C_GBU12x2[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "Mk-20 Rockeye x2")
            {
                return weaponWeight_FA18C_Mk20Rockeyex2[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "Mk-82 x2")
            {
                return weaponWeight_FA18C_Mk82x2[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "Mk-82 SnakeEye x2")
            {
                return weaponWeight_FA18C_Mk82SnakeEyex2[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "Mk-82Y x2")
            {
                return weaponWeight_FA18C_Mk82Yx2[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "Mk-83 x2")
            {
                return weaponWeight_FA18C_Mk83x2[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "BDU-33 x6")
            {
                return weaponWeight_FA18C_BDU33x6[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "GBU-38 x2")
            {
                return weaponWeight_FA18C_GBU38x2[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "CBU-99")
            {
                return weaponWeight_FA18C_CBU99[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "GBU-10")
            {
                return weaponWeight_FA18C_GBU10[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "GBU-12")
            {
                return weaponWeight_FA18C_GBU12[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "GBU-16")
            {
                return weaponWeight_FA18C_GBU16[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "GBU-31")
            {
                return weaponWeight_FA18C_GBU31[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "GBU-31(V)3/B")
            {
                return weaponWeight_FA18C_GBU31V3B[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "GBU-38")
            {
                return weaponWeight_FA18C_GBU38[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "Mk-20")
            {
                return weaponWeight_FA18C_Mk20[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "Mk-82")
            {
                return weaponWeight_FA18C_Mk82[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "Mk-82 SnakeEye")
            {
                return weaponWeight_FA18C_Mk82SnakeEye[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "Mk-82Y")
            {
                return weaponWeight_FA18C_Mk82Y[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "Mk-83")
            {
                return weaponWeight_FA18C_Mk83[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "Mk-84")
            {
                return weaponWeight_FA18C_Mk84[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "AGM-154A")
            {
                return weaponWeight_FA18C_AGM154A[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "AGM-154C")
            {
                return weaponWeight_FA18C_AGM154C[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "AGM-88")
            {
                return weaponWeight_FA18C_AGM88[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "AGM-154A x2")
            {
                return weaponWeight_FA18C_AGM154Ax2[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "AGM-154C x2")
            {
                return weaponWeight_FA18C_AGM154Cx2[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "AGM-65E")
            {
                return weaponWeight_FA18C_AGM65E[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "AGM-65F")
            {
                return weaponWeight_FA18C_AGM65F[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "4 ZUNI MK 71 x2")
            {
                return weaponWeight_FA18C_4ZUNIMK71x2[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "19 2.75' rockets M151 HE x2")
            {
                return weaponWeight_FA18C_rocketsM151HEx2[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "7 2.75' rockets MK151 (HE) x2")
            {
                return weaponWeight_FA18C_rocketsMK151HEx2[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "7 2.75' rockets M5 (HE) x2")
            {
                return weaponWeight_FA18C_rocketsM5HEx2[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "4 ZUNI MK 71")
            {
                return weaponWeight_FA18C_4ZUNIMK71[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "19 2.75' rockets M151 HE")
            {
                return weaponWeight_FA18C_rocketsM151HE[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "7 2.75' rockets MK151 (HE)")
            {
                return weaponWeight_FA18C_rocketsMK151HE[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "7 2.75' rockets M5 (HE)")
            {
                return weaponWeight_FA18C_rocketsM5HE[stationNumber - 1];
            }
            else if (station3ComboBox.SelectedValue.ToString() == "FPU-8A Fuel Tank 330 gallons")
            {
                return weaponWeight_FA18C_FPU8AFuelTank330gallons[stationNumber - 1];
            }
            else
            {
                return 0;
            }
        }
        public int GetStation4Weight_FA18C()
        {
            ///////////////////////////////////this is working, but i want to post the entire weapons arrays tables individually , then dynamically reference
            //////each one for the aircraft=============================
            int stationNumber = 4;


            if (station4ComboBox.SelectedValue.ToString() == "AIM-120B x2")
            {
                //int[] weaponWeight_FA18C_MK82 = new int[] { 0, 531, 531, 0, 531, 0, 531, 531, 0, 0, 0, 0 };//old news baby!!!
                return weaponWeight_FA18C_AIM120Bx2[stationNumber - 1];
                //return 31;//debugging
            }
            else if (station4ComboBox.SelectedValue.ToString() == "AIM-120C x2")
            {
                return weaponWeight_FA18C_AIM120Cx2[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "AIM-9L x2")
            {
                return weaponWeight_FA18C_AIM9Lx2[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "AIM-9M x2")
            {
                return weaponWeight_FA18C_AIM9Mx2[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "AIM-9X x2")
            {
                return weaponWeight_FA18C_AIM9Xx2[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "CAP-9M x2")
            {
                return weaponWeight_FA18C_CAP9Mx2[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "AIM-120B")
            {
                return weaponWeight_FA18C_AIM120B[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "AIM-120C")
            {
                return weaponWeight_FA18C_AIM120C[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "AIM-7M")
            {
                return weaponWeight_FA18C_AIM7M[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "AIM-7F")
            {
                return weaponWeight_FA18C_AIM7F[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "AIM-7MH")
            {
                return weaponWeight_FA18C_AIM7MH[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "AIM-9L")
            {
                return weaponWeight_FA18C_AIM9L[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "AIM-9M")
            {
                return weaponWeight_FA18C_AIM9M[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "AIM-9X")
            {
                return weaponWeight_FA18C_AIM9X[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "CAP-9M")
            {
                return weaponWeight_FA18C_CAP9M[stationNumber - 1];
            }
            //a2g
            /*
            else if (station4ComboBox.SelectedValue.ToString() == "")
            {
                return weaponWeight_FA18C_[stationNumber - 1];
            }
            */
            else if (station4ComboBox.SelectedValue.ToString() == "CBU-99 x2")
            {
                return weaponWeight_FA18C_CBU99x2[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "GBU-12 x2")
            {
                return weaponWeight_FA18C_GBU12x2[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "Mk-20 Rockeye x2")
            {
                return weaponWeight_FA18C_Mk20Rockeyex2[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "Mk-82 x2")
            {
                return weaponWeight_FA18C_Mk82x2[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "Mk-82 SnakeEye x2")
            {
                return weaponWeight_FA18C_Mk82SnakeEyex2[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "Mk-82Y x2")
            {
                return weaponWeight_FA18C_Mk82Yx2[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "Mk-83 x2")
            {
                return weaponWeight_FA18C_Mk83x2[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "BDU-33 x6")
            {
                return weaponWeight_FA18C_BDU33x6[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "GBU-38 x2")
            {
                return weaponWeight_FA18C_GBU38x2[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "CBU-99")
            {
                return weaponWeight_FA18C_CBU99[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "GBU-10")
            {
                return weaponWeight_FA18C_GBU10[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "GBU-12")
            {
                return weaponWeight_FA18C_GBU12[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "GBU-16")
            {
                return weaponWeight_FA18C_GBU16[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "GBU-31")
            {
                return weaponWeight_FA18C_GBU31[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "GBU-31(V)3/B")
            {
                return weaponWeight_FA18C_GBU31V3B[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "GBU-38")
            {
                return weaponWeight_FA18C_GBU38[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "Mk-20")
            {
                return weaponWeight_FA18C_Mk20[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "Mk-82")
            {
                return weaponWeight_FA18C_Mk82[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "Mk-82 SnakeEye")
            {
                return weaponWeight_FA18C_Mk82SnakeEye[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "Mk-82Y")
            {
                return weaponWeight_FA18C_Mk82Y[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "Mk-83")
            {
                return weaponWeight_FA18C_Mk83[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "Mk-84")
            {
                return weaponWeight_FA18C_Mk84[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "AGM-154A")
            {
                return weaponWeight_FA18C_AGM154A[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "AGM-154C")
            {
                return weaponWeight_FA18C_AGM154C[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "AGM-88")
            {
                return weaponWeight_FA18C_AGM88[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "AGM-154A x2")
            {
                return weaponWeight_FA18C_AGM154Ax2[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "AGM-154C x2")
            {
                return weaponWeight_FA18C_AGM154Cx2[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "AGM-65E")
            {
                return weaponWeight_FA18C_AGM65E[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "AGM-65F")
            {
                return weaponWeight_FA18C_AGM65F[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "4 ZUNI MK 71 x2")
            {
                return weaponWeight_FA18C_4ZUNIMK71x2[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "19 2.75' rockets M151 HE x2")
            {
                return weaponWeight_FA18C_rocketsM151HEx2[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "7 2.75' rockets MK151 (HE) x2")
            {
                return weaponWeight_FA18C_rocketsMK151HEx2[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "7 2.75' rockets M5 (HE) x2")
            {
                return weaponWeight_FA18C_rocketsM5HEx2[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "4 ZUNI MK 71")
            {
                return weaponWeight_FA18C_4ZUNIMK71[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "19 2.75' rockets M151 HE")
            {
                return weaponWeight_FA18C_rocketsM151HE[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "7 2.75' rockets MK151 (HE)")
            {
                return weaponWeight_FA18C_rocketsMK151HE[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "7 2.75' rockets M5 (HE)")
            {
                return weaponWeight_FA18C_rocketsM5HE[stationNumber - 1];
            }
            else if (station4ComboBox.SelectedValue.ToString() == "FPU-8A Fuel Tank 330 gallons")
            {
                return weaponWeight_FA18C_FPU8AFuelTank330gallons[stationNumber - 1];
            }
            else
            {
                return 0;
            }
        }
        public int GetStation5Weight_FA18C()
        {
            ///////////////////////////////////this is working, but i want to post the entire weapons arrays tables individually , then dynamically reference
            //////each one for the aircraft=============================
            int stationNumber = 5;


            if (station5ComboBox.SelectedValue.ToString() == "AIM-120B x2")
            {
                //int[] weaponWeight_FA18C_MK82 = new int[] { 0, 531, 531, 0, 531, 0, 531, 531, 0, 0, 0, 0 };//old news baby!!!
                return weaponWeight_FA18C_AIM120Bx2[stationNumber - 1];
                //return 31;//debugging
            }
            else if (station5ComboBox.SelectedValue.ToString() == "AIM-120C x2")
            {
                return weaponWeight_FA18C_AIM120Cx2[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "AIM-9L x2")
            {
                return weaponWeight_FA18C_AIM9Lx2[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "AIM-9M x2")
            {
                return weaponWeight_FA18C_AIM9Mx2[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "AIM-9X x2")
            {
                return weaponWeight_FA18C_AIM9Xx2[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "CAP-9M x2")
            {
                return weaponWeight_FA18C_CAP9Mx2[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "AIM-120B")
            {
                return weaponWeight_FA18C_AIM120B[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "AIM-120C")
            {
                return weaponWeight_FA18C_AIM120C[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "AIM-7M")
            {
                return weaponWeight_FA18C_AIM7M[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "AIM-7F")
            {
                return weaponWeight_FA18C_AIM7F[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "AIM-7MH")
            {
                return weaponWeight_FA18C_AIM7MH[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "AIM-9L")
            {
                return weaponWeight_FA18C_AIM9L[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "AIM-9M")
            {
                return weaponWeight_FA18C_AIM9M[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "AIM-9X")
            {
                return weaponWeight_FA18C_AIM9X[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "CAP-9M")
            {
                return weaponWeight_FA18C_CAP9M[stationNumber - 1];
            }
            //a2g
            /*
            else if (station5ComboBox.SelectedValue.ToString() == "")
            {
                return weaponWeight_FA18C_[stationNumber - 1];
            }
            */
            else if (station5ComboBox.SelectedValue.ToString() == "CBU-99 x2")
            {
                return weaponWeight_FA18C_CBU99x2[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "GBU-12 x2")
            {
                return weaponWeight_FA18C_GBU12x2[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "Mk-20 Rockeye x2")
            {
                return weaponWeight_FA18C_Mk20Rockeyex2[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "Mk-82 x2")
            {
                return weaponWeight_FA18C_Mk82x2[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "Mk-82 SnakeEye x2")
            {
                return weaponWeight_FA18C_Mk82SnakeEyex2[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "Mk-82Y x2")
            {
                return weaponWeight_FA18C_Mk82Yx2[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "Mk-83 x2")
            {
                return weaponWeight_FA18C_Mk83x2[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "BDU-33 x6")
            {
                return weaponWeight_FA18C_BDU33x6[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "GBU-38 x2")
            {
                return weaponWeight_FA18C_GBU38x2[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "CBU-99")
            {
                return weaponWeight_FA18C_CBU99[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "GBU-10")
            {
                return weaponWeight_FA18C_GBU10[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "GBU-12")
            {
                return weaponWeight_FA18C_GBU12[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "GBU-16")
            {
                return weaponWeight_FA18C_GBU16[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "GBU-31")
            {
                return weaponWeight_FA18C_GBU31[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "GBU-31(V)3/B")
            {
                return weaponWeight_FA18C_GBU31V3B[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "GBU-38")
            {
                return weaponWeight_FA18C_GBU38[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "Mk-20")
            {
                return weaponWeight_FA18C_Mk20[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "Mk-82")
            {
                return weaponWeight_FA18C_Mk82[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "Mk-82 SnakeEye")
            {
                return weaponWeight_FA18C_Mk82SnakeEye[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "Mk-82Y")
            {
                return weaponWeight_FA18C_Mk82Y[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "Mk-83")
            {
                return weaponWeight_FA18C_Mk83[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "Mk-84")
            {
                return weaponWeight_FA18C_Mk84[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "AGM-154A")
            {
                return weaponWeight_FA18C_AGM154A[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "AGM-154C")
            {
                return weaponWeight_FA18C_AGM154C[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "AGM-88")
            {
                return weaponWeight_FA18C_AGM88[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "AGM-154A x2")
            {
                return weaponWeight_FA18C_AGM154Ax2[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "AGM-154C x2")
            {
                return weaponWeight_FA18C_AGM154Cx2[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "AGM-65E")
            {
                return weaponWeight_FA18C_AGM65E[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "AGM-65F")
            {
                return weaponWeight_FA18C_AGM65F[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "4 ZUNI MK 71 x2")
            {
                return weaponWeight_FA18C_4ZUNIMK71x2[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "19 2.75' rockets M151 HE x2")
            {
                return weaponWeight_FA18C_rocketsM151HEx2[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "7 2.75' rockets MK151 (HE) x2")
            {
                return weaponWeight_FA18C_rocketsMK151HEx2[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "7 2.75' rockets M5 (HE) x2")
            {
                return weaponWeight_FA18C_rocketsM5HEx2[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "4 ZUNI MK 71")
            {
                return weaponWeight_FA18C_4ZUNIMK71[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "19 2.75' rockets M151 HE")
            {
                return weaponWeight_FA18C_rocketsM151HE[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "7 2.75' rockets MK151 (HE)")
            {
                return weaponWeight_FA18C_rocketsMK151HE[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "7 2.75' rockets M5 (HE)")
            {
                return weaponWeight_FA18C_rocketsM5HE[stationNumber - 1];
            }
            else if (station5ComboBox.SelectedValue.ToString() == "FPU-8A Fuel Tank 330 gallons")
            {
                return weaponWeight_FA18C_FPU8AFuelTank330gallons[stationNumber - 1];
            }
            else
            {
                return 0;
            }
        }
        public int GetStation6Weight_FA18C()
        {
            ///////////////////////////////////this is working, but i want to post the entire weapons arrays tables individually , then dynamically reference
            //////each one for the aircraft=============================
            int stationNumber = 6;


            if (station6ComboBox.SelectedValue.ToString() == "AIM-120B x2")
            {
                //int[] weaponWeight_FA18C_MK82 = new int[] { 0, 531, 531, 0, 531, 0, 531, 531, 0, 0, 0, 0 };//old news baby!!!
                return weaponWeight_FA18C_AIM120Bx2[stationNumber - 1];
                //return 31;//debugging
            }
            else if (station6ComboBox.SelectedValue.ToString() == "AIM-120C x2")
            {
                return weaponWeight_FA18C_AIM120Cx2[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "AIM-9L x2")
            {
                return weaponWeight_FA18C_AIM9Lx2[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "AIM-9M x2")
            {
                return weaponWeight_FA18C_AIM9Mx2[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "AIM-9X x2")
            {
                return weaponWeight_FA18C_AIM9Xx2[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "CAP-9M x2")
            {
                return weaponWeight_FA18C_CAP9Mx2[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "AIM-120B")
            {
                return weaponWeight_FA18C_AIM120B[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "AIM-120C")
            {
                return weaponWeight_FA18C_AIM120C[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "AIM-7M")
            {
                return weaponWeight_FA18C_AIM7M[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "AIM-7F")
            {
                return weaponWeight_FA18C_AIM7F[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "AIM-7MH")
            {
                return weaponWeight_FA18C_AIM7MH[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "AIM-9L")
            {
                return weaponWeight_FA18C_AIM9L[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "AIM-9M")
            {
                return weaponWeight_FA18C_AIM9M[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "AIM-9X")
            {
                return weaponWeight_FA18C_AIM9X[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "CAP-9M")
            {
                return weaponWeight_FA18C_CAP9M[stationNumber - 1];
            }
            //a2g
            /*
            else if (station6ComboBox.SelectedValue.ToString() == "")
            {
                return weaponWeight_FA18C_[stationNumber - 1];
            }
            */
            else if (station6ComboBox.SelectedValue.ToString() == "CBU-99 x2")
            {
                return weaponWeight_FA18C_CBU99x2[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "GBU-12 x2")
            {
                return weaponWeight_FA18C_GBU12x2[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "Mk-20 Rockeye x2")
            {
                return weaponWeight_FA18C_Mk20Rockeyex2[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "Mk-82 x2")
            {
                return weaponWeight_FA18C_Mk82x2[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "Mk-82 SnakeEye x2")
            {
                return weaponWeight_FA18C_Mk82SnakeEyex2[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "Mk-82Y x2")
            {
                return weaponWeight_FA18C_Mk82Yx2[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "Mk-83 x2")
            {
                return weaponWeight_FA18C_Mk83x2[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "BDU-33 x6")
            {
                return weaponWeight_FA18C_BDU33x6[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "GBU-38 x2")
            {
                return weaponWeight_FA18C_GBU38x2[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "CBU-99")
            {
                return weaponWeight_FA18C_CBU99[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "GBU-10")
            {
                return weaponWeight_FA18C_GBU10[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "GBU-12")
            {
                return weaponWeight_FA18C_GBU12[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "GBU-16")
            {
                return weaponWeight_FA18C_GBU16[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "GBU-31")
            {
                return weaponWeight_FA18C_GBU31[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "GBU-31(V)3/B")
            {
                return weaponWeight_FA18C_GBU31V3B[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "GBU-38")
            {
                return weaponWeight_FA18C_GBU38[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "Mk-20")
            {
                return weaponWeight_FA18C_Mk20[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "Mk-82")
            {
                return weaponWeight_FA18C_Mk82[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "Mk-82 SnakeEye")
            {
                return weaponWeight_FA18C_Mk82SnakeEye[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "Mk-82Y")
            {
                return weaponWeight_FA18C_Mk82Y[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "Mk-83")
            {
                return weaponWeight_FA18C_Mk83[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "Mk-84")
            {
                return weaponWeight_FA18C_Mk84[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "AGM-154A")
            {
                return weaponWeight_FA18C_AGM154A[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "AGM-154C")
            {
                return weaponWeight_FA18C_AGM154C[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "AGM-88")
            {
                return weaponWeight_FA18C_AGM88[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "AGM-154A x2")
            {
                return weaponWeight_FA18C_AGM154Ax2[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "AGM-154C x2")
            {
                return weaponWeight_FA18C_AGM154Cx2[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "AGM-65E")
            {
                return weaponWeight_FA18C_AGM65E[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "AGM-65F")
            {
                return weaponWeight_FA18C_AGM65F[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "4 ZUNI MK 71 x2")
            {
                return weaponWeight_FA18C_4ZUNIMK71x2[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "19 2.75' rockets M151 HE x2")
            {
                return weaponWeight_FA18C_rocketsM151HEx2[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "7 2.75' rockets MK151 (HE) x2")
            {
                return weaponWeight_FA18C_rocketsMK151HEx2[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "7 2.75' rockets M5 (HE) x2")
            {
                return weaponWeight_FA18C_rocketsM5HEx2[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "4 ZUNI MK 71")
            {
                return weaponWeight_FA18C_4ZUNIMK71[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "19 2.75' rockets M151 HE")
            {
                return weaponWeight_FA18C_rocketsM151HE[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "7 2.75' rockets MK151 (HE)")
            {
                return weaponWeight_FA18C_rocketsMK151HE[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "7 2.75' rockets M5 (HE)")
            {
                return weaponWeight_FA18C_rocketsM5HE[stationNumber - 1];
            }
            else if (station6ComboBox.SelectedValue.ToString() == "FPU-8A Fuel Tank 330 gallons")
            {
                return weaponWeight_FA18C_FPU8AFuelTank330gallons[stationNumber - 1];
            }
            else
            {
                return 0;
            }
        }
        public int GetStation7Weight_FA18C()
        {
            ///////////////////////////////////this is working, but i want to post the entire weapons arrays tables individually , then dynamically reference
            //////each one for the aircraft=============================
            int stationNumber = 7;


            if (station7ComboBox.SelectedValue.ToString() == "AIM-120B x2")
            {
                //int[] weaponWeight_FA18C_MK82 = new int[] { 0, 531, 531, 0, 531, 0, 531, 531, 0, 0, 0, 0 };//old news baby!!!
                return weaponWeight_FA18C_AIM120Bx2[stationNumber - 1];
                //return 31;//debugging
            }
            else if (station7ComboBox.SelectedValue.ToString() == "AIM-120C x2")
            {
                return weaponWeight_FA18C_AIM120Cx2[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "AIM-9L x2")
            {
                return weaponWeight_FA18C_AIM9Lx2[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "AIM-9M x2")
            {
                return weaponWeight_FA18C_AIM9Mx2[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "AIM-9X x2")
            {
                return weaponWeight_FA18C_AIM9Xx2[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "CAP-9M x2")
            {
                return weaponWeight_FA18C_CAP9Mx2[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "AIM-120B")
            {
                return weaponWeight_FA18C_AIM120B[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "AIM-120C")
            {
                return weaponWeight_FA18C_AIM120C[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "AIM-7M")
            {
                return weaponWeight_FA18C_AIM7M[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "AIM-7F")
            {
                return weaponWeight_FA18C_AIM7F[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "AIM-7MH")
            {
                return weaponWeight_FA18C_AIM7MH[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "AIM-9L")
            {
                return weaponWeight_FA18C_AIM9L[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "AIM-9M")
            {
                return weaponWeight_FA18C_AIM9M[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "AIM-9X")
            {
                return weaponWeight_FA18C_AIM9X[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "CAP-9M")
            {
                return weaponWeight_FA18C_CAP9M[stationNumber - 1];
            }
            //a2g
            /*
            else if (station7ComboBox.SelectedValue.ToString() == "")
            {
                return weaponWeight_FA18C_[stationNumber - 1];
            }
            */
            else if (station7ComboBox.SelectedValue.ToString() == "CBU-99 x2")
            {
                return weaponWeight_FA18C_CBU99x2[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "GBU-12 x2")
            {
                return weaponWeight_FA18C_GBU12x2[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "Mk-20 Rockeye x2")
            {
                return weaponWeight_FA18C_Mk20Rockeyex2[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "Mk-82 x2")
            {
                return weaponWeight_FA18C_Mk82x2[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "Mk-82 SnakeEye x2")
            {
                return weaponWeight_FA18C_Mk82SnakeEyex2[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "Mk-82Y x2")
            {
                return weaponWeight_FA18C_Mk82Yx2[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "Mk-83 x2")
            {
                return weaponWeight_FA18C_Mk83x2[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "BDU-33 x6")
            {
                return weaponWeight_FA18C_BDU33x6[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "GBU-38 x2")
            {
                return weaponWeight_FA18C_GBU38x2[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "CBU-99")
            {
                return weaponWeight_FA18C_CBU99[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "GBU-10")
            {
                return weaponWeight_FA18C_GBU10[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "GBU-12")
            {
                return weaponWeight_FA18C_GBU12[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "GBU-16")
            {
                return weaponWeight_FA18C_GBU16[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "GBU-31")
            {
                return weaponWeight_FA18C_GBU31[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "GBU-31(V)3/B")
            {
                return weaponWeight_FA18C_GBU31V3B[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "GBU-38")
            {
                return weaponWeight_FA18C_GBU38[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "Mk-20")
            {
                return weaponWeight_FA18C_Mk20[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "Mk-82")
            {
                return weaponWeight_FA18C_Mk82[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "Mk-82 SnakeEye")
            {
                return weaponWeight_FA18C_Mk82SnakeEye[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "Mk-82Y")
            {
                return weaponWeight_FA18C_Mk82Y[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "Mk-83")
            {
                return weaponWeight_FA18C_Mk83[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "Mk-84")
            {
                return weaponWeight_FA18C_Mk84[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "AGM-154A")
            {
                return weaponWeight_FA18C_AGM154A[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "AGM-154C")
            {
                return weaponWeight_FA18C_AGM154C[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "AGM-88")
            {
                return weaponWeight_FA18C_AGM88[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "AGM-154A x2")
            {
                return weaponWeight_FA18C_AGM154Ax2[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "AGM-154C x2")
            {
                return weaponWeight_FA18C_AGM154Cx2[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "AGM-65E")
            {
                return weaponWeight_FA18C_AGM65E[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "AGM-65F")
            {
                return weaponWeight_FA18C_AGM65F[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "4 ZUNI MK 71 x2")
            {
                return weaponWeight_FA18C_4ZUNIMK71x2[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "19 2.75' rockets M151 HE x2")
            {
                return weaponWeight_FA18C_rocketsM151HEx2[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "7 2.75' rockets MK151 (HE) x2")
            {
                return weaponWeight_FA18C_rocketsMK151HEx2[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "7 2.75' rockets M5 (HE) x2")
            {
                return weaponWeight_FA18C_rocketsM5HEx2[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "4 ZUNI MK 71")
            {
                return weaponWeight_FA18C_4ZUNIMK71[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "19 2.75' rockets M151 HE")
            {
                return weaponWeight_FA18C_rocketsM151HE[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "7 2.75' rockets MK151 (HE)")
            {
                return weaponWeight_FA18C_rocketsMK151HE[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "7 2.75' rockets M5 (HE)")
            {
                return weaponWeight_FA18C_rocketsM5HE[stationNumber - 1];
            }
            else if (station7ComboBox.SelectedValue.ToString() == "FPU-8A Fuel Tank 330 gallons")
            {
                return weaponWeight_FA18C_FPU8AFuelTank330gallons[stationNumber - 1];
            }
            else
            {
                return 0;
            }
        }
        public int GetStation8Weight_FA18C()
        {
            ///////////////////////////////////this is working, but i want to post the entire weapons arrays tables individually , then dynamically reference
            //////each one for the aircraft=============================
            int stationNumber = 8;


            if (station8ComboBox.SelectedValue.ToString() == "AIM-120B x2")
            {
                //int[] weaponWeight_FA18C_MK82 = new int[] { 0, 531, 531, 0, 531, 0, 531, 531, 0, 0, 0, 0 };//old news baby!!!
                return weaponWeight_FA18C_AIM120Bx2[stationNumber - 1];
                //return 31;//debugging
            }
            else if (station8ComboBox.SelectedValue.ToString() == "AIM-120C x2")
            {
                return weaponWeight_FA18C_AIM120Cx2[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "AIM-9L x2")
            {
                return weaponWeight_FA18C_AIM9Lx2[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "AIM-9M x2")
            {
                return weaponWeight_FA18C_AIM9Mx2[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "AIM-9X x2")
            {
                return weaponWeight_FA18C_AIM9Xx2[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "CAP-9M x2")
            {
                return weaponWeight_FA18C_CAP9Mx2[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "AIM-120B")
            {
                return weaponWeight_FA18C_AIM120B[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "AIM-120C")
            {
                return weaponWeight_FA18C_AIM120C[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "AIM-7M")
            {
                return weaponWeight_FA18C_AIM7M[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "AIM-7F")
            {
                return weaponWeight_FA18C_AIM7F[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "AIM-7MH")
            {
                return weaponWeight_FA18C_AIM7MH[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "AIM-9L")
            {
                return weaponWeight_FA18C_AIM9L[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "AIM-9M")
            {
                return weaponWeight_FA18C_AIM9M[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "AIM-9X")
            {
                return weaponWeight_FA18C_AIM9X[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "CAP-9M")
            {
                return weaponWeight_FA18C_CAP9M[stationNumber - 1];
            }
            //a2g
            /*
            else if (station8ComboBox.SelectedValue.ToString() == "")
            {
                return weaponWeight_FA18C_[stationNumber - 1];
            }
            */
            else if (station8ComboBox.SelectedValue.ToString() == "CBU-99 x2")
            {
                return weaponWeight_FA18C_CBU99x2[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "GBU-12 x2")
            {
                return weaponWeight_FA18C_GBU12x2[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "Mk-20 Rockeye x2")
            {
                return weaponWeight_FA18C_Mk20Rockeyex2[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "Mk-82 x2")
            {
                return weaponWeight_FA18C_Mk82x2[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "Mk-82 SnakeEye x2")
            {
                return weaponWeight_FA18C_Mk82SnakeEyex2[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "Mk-82Y x2")
            {
                return weaponWeight_FA18C_Mk82Yx2[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "Mk-83 x2")
            {
                return weaponWeight_FA18C_Mk83x2[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "BDU-33 x6")
            {
                return weaponWeight_FA18C_BDU33x6[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "GBU-38 x2")
            {
                return weaponWeight_FA18C_GBU38x2[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "CBU-99")
            {
                return weaponWeight_FA18C_CBU99[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "GBU-10")
            {
                return weaponWeight_FA18C_GBU10[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "GBU-12")
            {
                return weaponWeight_FA18C_GBU12[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "GBU-16")
            {
                return weaponWeight_FA18C_GBU16[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "GBU-31")
            {
                return weaponWeight_FA18C_GBU31[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "GBU-31(V)3/B")
            {
                return weaponWeight_FA18C_GBU31V3B[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "GBU-38")
            {
                return weaponWeight_FA18C_GBU38[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "Mk-20")
            {
                return weaponWeight_FA18C_Mk20[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "Mk-82")
            {
                return weaponWeight_FA18C_Mk82[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "Mk-82 SnakeEye")
            {
                return weaponWeight_FA18C_Mk82SnakeEye[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "Mk-82Y")
            {
                return weaponWeight_FA18C_Mk82Y[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "Mk-83")
            {
                return weaponWeight_FA18C_Mk83[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "Mk-84")
            {
                return weaponWeight_FA18C_Mk84[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "AGM-154A")
            {
                return weaponWeight_FA18C_AGM154A[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "AGM-154C")
            {
                return weaponWeight_FA18C_AGM154C[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "AGM-88")
            {
                return weaponWeight_FA18C_AGM88[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "AGM-154A x2")
            {
                return weaponWeight_FA18C_AGM154Ax2[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "AGM-154C x2")
            {
                return weaponWeight_FA18C_AGM154Cx2[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "AGM-65E")
            {
                return weaponWeight_FA18C_AGM65E[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "AGM-65F")
            {
                return weaponWeight_FA18C_AGM65F[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "4 ZUNI MK 71 x2")
            {
                return weaponWeight_FA18C_4ZUNIMK71x2[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "19 2.75' rockets M151 HE x2")
            {
                return weaponWeight_FA18C_rocketsM151HEx2[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "7 2.75' rockets MK151 (HE) x2")
            {
                return weaponWeight_FA18C_rocketsMK151HEx2[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "7 2.75' rockets M5 (HE) x2")
            {
                return weaponWeight_FA18C_rocketsM5HEx2[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "4 ZUNI MK 71")
            {
                return weaponWeight_FA18C_4ZUNIMK71[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "19 2.75' rockets M151 HE")
            {
                return weaponWeight_FA18C_rocketsM151HE[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "7 2.75' rockets MK151 (HE)")
            {
                return weaponWeight_FA18C_rocketsMK151HE[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "7 2.75' rockets M5 (HE)")
            {
                return weaponWeight_FA18C_rocketsM5HE[stationNumber - 1];
            }
            else if (station8ComboBox.SelectedValue.ToString() == "FPU-8A Fuel Tank 330 gallons")
            {
                return weaponWeight_FA18C_FPU8AFuelTank330gallons[stationNumber - 1];
            }
            else
            {
                return 0;
            }
        }
        public int GetStation9Weight_FA18C()
        {
            ///////////////////////////////////this is working, but i want to post the entire weapons arrays tables individually , then dynamically reference
            //////each one for the aircraft=============================
            int stationNumber = 9;


            if (station9ComboBox.SelectedValue.ToString() == "AIM-120B x2")
            {
                //int[] weaponWeight_FA18C_MK82 = new int[] { 0, 531, 531, 0, 531, 0, 531, 531, 0, 0, 0, 0 };//old news baby!!!
                return weaponWeight_FA18C_AIM120Bx2[stationNumber - 1];
                //return 31;//debugging
            }
            else if (station9ComboBox.SelectedValue.ToString() == "AIM-120C x2")
            {
                return weaponWeight_FA18C_AIM120Cx2[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "AIM-9L x2")
            {
                return weaponWeight_FA18C_AIM9Lx2[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "AIM-9M x2")
            {
                return weaponWeight_FA18C_AIM9Mx2[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "AIM-9X x2")
            {
                return weaponWeight_FA18C_AIM9Xx2[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "CAP-9M x2")
            {
                return weaponWeight_FA18C_CAP9Mx2[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "AIM-120B")
            {
                return weaponWeight_FA18C_AIM120B[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "AIM-120C")
            {
                return weaponWeight_FA18C_AIM120C[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "AIM-7M")
            {
                return weaponWeight_FA18C_AIM7M[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "AIM-7F")
            {
                return weaponWeight_FA18C_AIM7F[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "AIM-7MH")
            {
                return weaponWeight_FA18C_AIM7MH[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "AIM-9L")
            {
                return weaponWeight_FA18C_AIM9L[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "AIM-9M")
            {
                return weaponWeight_FA18C_AIM9M[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "AIM-9X")
            {
                return weaponWeight_FA18C_AIM9X[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "CAP-9M")
            {
                return weaponWeight_FA18C_CAP9M[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "AN/ASQ-T50 TCTS Pod")
            {
                return weaponWeight_FA18C_ANASQT50TCTS[stationNumber - 1];
            }
            //a2g
            /*
            else if (station9ComboBox.SelectedValue.ToString() == "")
            {
                return weaponWeight_FA18C_[stationNumber - 1];
            }
            */
            else if (station9ComboBox.SelectedValue.ToString() == "CBU-99 x2")
            {
                return weaponWeight_FA18C_CBU99x2[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "GBU-12 x2")
            {
                return weaponWeight_FA18C_GBU12x2[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "Mk-20 Rockeye x2")
            {
                return weaponWeight_FA18C_Mk20Rockeyex2[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "Mk-82 x2")
            {
                return weaponWeight_FA18C_Mk82x2[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "Mk-82 SnakeEye x2")
            {
                return weaponWeight_FA18C_Mk82SnakeEyex2[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "Mk-82Y x2")
            {
                return weaponWeight_FA18C_Mk82Yx2[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "Mk-83 x2")
            {
                return weaponWeight_FA18C_Mk83x2[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "BDU-33 x6")
            {
                return weaponWeight_FA18C_BDU33x6[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "GBU-38 x2")
            {
                return weaponWeight_FA18C_GBU38x2[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "CBU-99")
            {
                return weaponWeight_FA18C_CBU99[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "GBU-10")
            {
                return weaponWeight_FA18C_GBU10[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "GBU-12")
            {
                return weaponWeight_FA18C_GBU12[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "GBU-16")
            {
                return weaponWeight_FA18C_GBU16[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "GBU-31")
            {
                return weaponWeight_FA18C_GBU31[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "GBU-31(V)3/B")
            {
                return weaponWeight_FA18C_GBU31V3B[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "GBU-38")
            {
                return weaponWeight_FA18C_GBU38[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "Mk-20")
            {
                return weaponWeight_FA18C_Mk20[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "Mk-82")
            {
                return weaponWeight_FA18C_Mk82[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "Mk-82 SnakeEye")
            {
                return weaponWeight_FA18C_Mk82SnakeEye[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "Mk-82Y")
            {
                return weaponWeight_FA18C_Mk82Y[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "Mk-83")
            {
                return weaponWeight_FA18C_Mk83[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "Mk-84")
            {
                return weaponWeight_FA18C_Mk84[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "AGM-154A")
            {
                return weaponWeight_FA18C_AGM154A[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "AGM-154C")
            {
                return weaponWeight_FA18C_AGM154C[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "AGM-88")
            {
                return weaponWeight_FA18C_AGM88[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "AGM-154A x2")
            {
                return weaponWeight_FA18C_AGM154Ax2[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "AGM-154C x2")
            {
                return weaponWeight_FA18C_AGM154Cx2[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "AGM-65E")
            {
                return weaponWeight_FA18C_AGM65E[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "AGM-65F")
            {
                return weaponWeight_FA18C_AGM65F[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "4 ZUNI MK 71 x2")
            {
                return weaponWeight_FA18C_4ZUNIMK71x2[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "19 2.75' rockets M151 HE x2")
            {
                return weaponWeight_FA18C_rocketsM151HEx2[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "7 2.75' rockets MK151 (HE) x2")
            {
                return weaponWeight_FA18C_rocketsMK151HEx2[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "7 2.75' rockets M5 (HE) x2")
            {
                return weaponWeight_FA18C_rocketsM5HEx2[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "4 ZUNI MK 71")
            {
                return weaponWeight_FA18C_4ZUNIMK71[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "19 2.75' rockets M151 HE")
            {
                return weaponWeight_FA18C_rocketsM151HE[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "7 2.75' rockets MK151 (HE)")
            {
                return weaponWeight_FA18C_rocketsMK151HE[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "7 2.75' rockets M5 (HE)")
            {
                return weaponWeight_FA18C_rocketsM5HE[stationNumber - 1];
            }
            else if (station9ComboBox.SelectedValue.ToString() == "FPU-8A Fuel Tank 330 gallons")
            {
                return weaponWeight_FA18C_FPU8AFuelTank330gallons[stationNumber - 1];
            }
            else
            {
                return 0;
            }
        }

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
           
        }

        private void AirtoAirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void AIM9LSidewinderIRAAMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station1ComboBox.Text = "AIM-9L";
        }

        private void AIM9MSidewinderIRAAMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station1ComboBox.Text = "AIM-9M";
        }

        private void AIM9XSidewinderIRAAMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station1ComboBox.Text = "AIM-9X";
        }

        private void CAP9MToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station1ComboBox.Text = "CAP-9M";
        }

        private void ContextMenuStrip_Station1_FA18C3_Opening(object sender, CancelEventArgs e)
        {

        }

        private void ToolStripMenuItem9_Click(object sender, EventArgs e)
        {
            station1ComboBox.Text = "AIM-9L";
        }

        private void ToolStripMenuItem10_Click(object sender, EventArgs e)
        {
            station1ComboBox.Text = "AIM-9M";
        }

        private void ToolStripMenuItem11_Click(object sender, EventArgs e)
        {
            station1ComboBox.Text = "AIM-9X";
        }

        private void ToolStripMenuItem12_Click(object sender, EventArgs e)
        {
            station1ComboBox.Text = "CAP-9M";
        }

        private void ToolStripMenuItem21_Click(object sender, EventArgs e)
        {

        }

        private void FuelToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItem14_Click(object sender, EventArgs e)
        {
            station1ComboBox.Text = "AN/ASQ-T50 TCTS Pod";
        }

        private void RemovePayloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station1ComboBox.Text = "Empty";
        }

        private void AIM120BX2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "AIM-120B x2";
        }

        private void AIM120CX2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "AIM-120C x2";
        }

        private void ToolStripMenuItem17_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "AIM-9L x2";
        }

        private void ToolStripMenuItem18_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "AIM-9M x2";
        }

        private void ToolStripMenuItem19_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "AIM-9X x2";
        }

        private void ToolStripMenuItem20_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "CAP-9M x2";
        }

        private void AIM120BToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "AIM-120B";
        }

        private void AIM120CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "AIM-120C";
        }

        private void AIM7MToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "AIM-7M";
        }

        private void AIM7FToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "AIM-7F";
        }

        private void AIM7MHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "AIM-7MH";
        }

        private void AIM9LToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "AIM-9L";
        }

        private void AIM9MToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "AIM-9M";
        }

        private void AIM9XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "AIM-9X";
        }

        private void CAP9MToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "CAP-9M";
        }

        private void ToolStripMenuItem22_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "CBU-99 x2";
        }

        private void GBU12X2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "GBU-12 x2";
        }

        private void Mk20RockeyeX2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "Mk-20 Rockeye x2";
        }

        private void Mk82X2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "Mk-82 x2";
        }

        private void Mk82SnakeEyeX2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "Mk-82 SnakeEye x2";
        }

        private void Mk82YX2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "Mk-82Y x2";
        }

        private void Mk83X2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "Mk-83 x2";
        }

        private void BDU33X6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "BDU-33 x6";
        }

        private void GBU38X2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "GBU-38 x2";
        }

        private void CBU99ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "CBU-99";
        }

        private void GBU10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "GBU-10";
        }

        private void GBU12ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "GBU-12";
        }

        private void GBU16ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "GBU-16";
        }

        private void GBU31ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "GBU-31";
        }

        private void GBU31V3BToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "GBU-31(V)3/B";
        }

        private void GBU38ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "GBU-38";
        }

        private void Mk20ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "Mk-20";
        }

        private void Mk82ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "Mk-82";
        }

        private void Mk82SnakeEyeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "Mk-82 SnakeEye";
        }

        private void Mk82YToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "Mk-82Y";
        }

        private void Mk83ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "Mk-83";
        }

        private void Mk84ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "Mk-84";
        }

        private void AGM154AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "AGM-154A";
        }

        private void AGM154CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "AGM-154C";
        }

        private void AGM88ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "AGM-88";
        }

        private void AGM154AX2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "AGM-154A x2";
        }

        private void AGM154CX2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "AGM-154C x2";
        }

        private void AGM65EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "AGM-65E";
        }

        private void AGM65FToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "AGM-65F";
        }

        private void ZUNIMK71X2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "4 ZUNI MK 71 x2";
        }

        private void RocketsM151HEX2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "19 2.75' rockets M151 HE x2";
        }

        private void RocketsMK151HEX2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "7 2.75' rockets MK151 (HE) x2";
        }

        private void RocketsM5HEX2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "7 2.75' rockets M5 (HE) x2";
        }

        private void ZUNIMK71ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "4 ZUNI MK 71";
        }

        private void RocketsM151HEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "19 2.75' rockets M151 HE";
        }

        private void RocketsMK151HEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "7 2.75' rockets MK151 (HE)";
        }

        private void RocketsM5HEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "7 2.75' rockets M5 (HE)";
        }

        private void RemovePayloadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            station2ComboBox.Text = "Empty";
        }

        private void ToolStripMenuItem23_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "AIM-120B x2";
        }

        private void ToolStripMenuItem24_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "AIM-120C x2";
        }

        private void ToolStripMenuItem29_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "AIM-120B";
        }

        private void ToolStripMenuItem30_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "AIM-120C";
        }

        private void ToolStripMenuItem31_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "AIM-7M";
        }

        private void ToolStripMenuItem32_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "AIM-7F";
        }

        private void ToolStripMenuItem33_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "AIM-7MH";
        }

        private void ToolStripMenuItem39_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "CBU-99 x2";
        }

        private void ToolStripMenuItem40_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "GBU-12 x2";
        }

        private void ToolStripMenuItem41_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "Mk-20 Rockeye x2";
        }

        private void ToolStripMenuItem42_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "Mk-82 x2";
        }

        private void ToolStripMenuItem43_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "Mk-82 SnakeEye x2";
        }

        private void ToolStripMenuItem44_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "Mk-82Y x2";
        }

        private void ToolStripMenuItem45_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "Mk-83 x2";
        }

        private void ToolStripMenuItem46_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "BDU-33 x6";
        }

        private void ToolStripMenuItem47_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "GBU-38 x2";
        }

        private void ToolStripMenuItem48_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "CBU-99";
        }

        private void ToolStripMenuItem49_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "GBU-10";
        }

        private void ToolStripMenuItem50_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "GBU-12";
        }

        private void ToolStripMenuItem51_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "GBU-16";
        }

        private void ToolStripMenuItem52_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "GBU-31";
        }

        private void ToolStripMenuItem53_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "GBU-31(V)3/B";
        }

        private void ToolStripMenuItem54_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "GBU-38";
        }

        private void ToolStripMenuItem55_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "Mk-20";
        }

        private void ToolStripMenuItem56_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "Mk-82";
        }

        private void ToolStripMenuItem57_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "Mk-82 SnakeEye";
        }

        private void ToolStripMenuItem58_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "Mk-82Y";
        }

        private void ToolStripMenuItem59_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "Mk-83";
        }

        private void ToolStripMenuItem60_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "Mk-84";
        }

        private void FPU8AFuelTank330GallonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "FPU-8A Fuel Tank 330 gallons";
        }

        private void ToolStripMenuItem62_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "AGM-154A";
        }

        private void ToolStripMenuItem63_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "AGM-154C";
        }

        private void ToolStripMenuItem64_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "AGM-88";
        }

        private void ToolStripMenuItem65_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "AGM-154A x2";
        }

        private void ToolStripMenuItem66_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "AGM-154C x2";
        }

        private void ToolStripMenuItem67_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "AGM-65E";
        }

        private void ToolStripMenuItem68_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "AGM-65F";
        }

        private void ToolStripMenuItem70_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "4 ZUNI MK 71 x2";
        }

        private void ToolStripMenuItem71_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "19 2.75' rockets M151 HE x2";
        }

        private void ToolStripMenuItem72_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "7 2.75' rockets MK151 (HE) x2";
        }

        private void ToolStripMenuItem73_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "7 2.75' rockets M5 (HE) x2";
        }

        private void ToolStripMenuItem74_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "4 ZUNI MK 71";
        }

        private void ToolStripMenuItem75_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "19 2.75' rockets M151 HE";
        }

        private void ToolStripMenuItem76_Click(object sender, EventArgs e)
        {
            station3ComboBox.Text = "7 2.75' rockets MK151 (HE)";
           
        }

        private void Button_exportLoadout_Click(object sender, EventArgs e)
        {
            if (textBox_loadoutName.Text == "")
            {
                MessageBox.Show("Please enter a name for your loadout");
                return;
            }

            userNamedExport = textBox_loadoutName.Text;


            //MessageBox.Show("You clicked the Export button");
            //Dummy File path C:\Users\Bailey\source\repos\DCS_Airctaft_Loadout_Generator\DCS_Airctaft_Loadout_Generator\Dummy DCS Files
            if (selectAirctaftListBox.GetItemText(selectAirctaftListBox.SelectedItem) == "F/A-18")
            {
                
                //MessageBox.Show("You clicked the Export button for F18");
                assignStation1ExportIDs();
                assignStation2ExportIDs();
                assignStation3ExportIDs();
                assignStation4ExportIDs();
                assignStation5ExportIDs();
                assignStation6ExportIDs();
                assignStation7ExportIDs();
                assignStation8ExportIDs();
                assignStation9ExportIDs();
                exportFileMakerFA18C();

                
                //MessageBox.Show(station1StoreExport);
            }
        }
        public void assignStation1ExportIDs()
        {
            if (station1ComboBox.Text == "Empty")
            {
                station1StoreExport = "";
            }
            else if (station1ComboBox.Text == "AIM-9L")
            {
                station1StoreExport = "{AIM-9L}";
            }
            else if (station1ComboBox.Text == "AIM-9M")
            {
                station1StoreExport = "{6CEB49FC-DED8-4DED-B053-E1F033FF72D3}";
            }
            else if (station1ComboBox.Text == "AIM-9X")
            {
                station1StoreExport = "{5CE2FF2A-645A-4197-B48D-8720AC69394F}";
            }
            else if (station1ComboBox.Text == "CAP-9M")
            {
                station1StoreExport = "CATM-9M";
            }
            else if (station1ComboBox.Text == "AIM-120B x2")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "AIM-120C x2")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "AIM-9L x2")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "AIM-9M x2")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "AIM-9X x2")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "CAP-9M x2")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "AIM-120B")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "AIM-120C")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "AIM-7M")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "AIM-7F")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "AIM-7MH")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "AN/ASQ-T50 TCTS Pod")
            {
                station1StoreExport = "{AIS_ASQ_T50}";
            }
            else if (station1ComboBox.Text == "CBU-99 x2")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "GBU-12 x2")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "Mk-20 Rockeye x2")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "Mk-82 x2")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "Mk-82 SnakeEye x2")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "Mk-82Y x2")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "Mk-83 x2")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "BDU-33 x6")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "GBU-38 x2")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "CBU-99")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "GBU-10")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "GBU-12")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "GBU-16")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "GBU-31")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "GBU-31(V)3/B")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "GBU-38")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "Mk-20")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "Mk-82")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "Mk-82 SnakeEye")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "Mk-82Y")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "Mk-83")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "Mk-84")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "AGM-154A")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "AGM-154C")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "AGM-88")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "AGM-154A x2")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "AGM-154C x2")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "AGM-65E")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "AGM-65F")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "4 ZUNI MK 71 x2")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "19 2.75' rockets M151 HE x2")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "7 2.75' rockets MK151 (HE) x2")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "7 2.75' rockets M5 (HE) x2")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "4 ZUNI MK 71")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "19 2.75' rockets M151 HE")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "7 2.75' rockets MK151 (HE)")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "7 2.75' rockets M5 (HE)")
            {
                station1StoreExport = "stationid";
            }
            else if (station1ComboBox.Text == "FPU-8A Fuel Tank 330 gallons")
            {
                station1StoreExport = "stationid";
            }
        }
        public void assignStation9ExportIDs()
        {
            if (station9ComboBox.Text == "Empty")
            {
                station9StoreExport = "";
            }
            else if (station9ComboBox.Text == "AIM-9L")
            {
                station9StoreExport = "{AIM-9L}";
            }
            else if (station9ComboBox.Text == "AIM-9M")
            {
                station9StoreExport = "{6CEB49FC-DED8-4DED-B053-E1F033FF72D3}";
            }
            else if (station9ComboBox.Text == "AIM-9X")
            {
                station9StoreExport = "{5CE2FF2A-645A-4197-B48D-8720AC69394F}";
            }
            else if (station9ComboBox.Text == "CAP-9M")
            {
                station9StoreExport = "CATM-9M";
            }
            else if (station9ComboBox.Text == "AIM-120B x2")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "AIM-120C x2")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "AIM-9L x2")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "AIM-9M x2")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "AIM-9X x2")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "CAP-9M x2")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "AIM-120B")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "AIM-120C")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "AIM-7M")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "AIM-7F")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "AIM-7MH")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "AN/ASQ-T50 TCTS Pod")
            {
                station9StoreExport = "{AIS_ASQ_T50}";
            }
            else if (station9ComboBox.Text == "CBU-99 x2")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "GBU-12 x2")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "Mk-20 Rockeye x2")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "Mk-82 x2")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "Mk-82 SnakeEye x2")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "Mk-82Y x2")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "Mk-83 x2")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "BDU-33 x6")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "GBU-38 x2")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "CBU-99")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "GBU-10")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "GBU-12")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "GBU-16")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "GBU-31")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "GBU-31(V)3/B")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "GBU-38")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "Mk-20")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "Mk-82")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "Mk-82 SnakeEye")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "Mk-82Y")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "Mk-83")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "Mk-84")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "AGM-154A")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "AGM-154C")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "AGM-88")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "AGM-154A x2")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "AGM-154C x2")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "AGM-65E")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "AGM-65F")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "4 ZUNI MK 71 x2")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "19 2.75' rockets M151 HE x2")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "7 2.75' rockets MK151 (HE) x2")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "7 2.75' rockets M5 (HE) x2")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "4 ZUNI MK 71")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "19 2.75' rockets M151 HE")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "7 2.75' rockets MK151 (HE)")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "7 2.75' rockets M5 (HE)")
            {
                station9StoreExport = "stationid";
            }
            else if (station9ComboBox.Text == "FPU-8A Fuel Tank 330 gallons")
            {
                station9StoreExport = "stationid";
            }
        }
        public void assignStation2ExportIDs()
        {
            if (station2ComboBox.Text == "Empty")
            {
                station2StoreExport = "";
            }
            else if (station2ComboBox.Text == "AIM-9L")
            {
                station2StoreExport = "LAU-115_LAU-127_AIM-9L";
            }
            else if (station2ComboBox.Text == "AIM-9M")
            {
                station2StoreExport = "LAU-115_LAU-127_AIM-9M";
            }
            else if (station2ComboBox.Text == "AIM-9X")
            {
                station2StoreExport = "LAU-115_LAU-127_AIM-9X";
            }
            else if (station2ComboBox.Text == "CAP-9M")
            {
                station2StoreExport = "LAU-115_LAU-127_CATM-9M";
            }
            else if (station2ComboBox.Text == "AIM-120B x2")
            {
                station2StoreExport = "LAU-115_2*LAU-127_AIM-120B";
            }
            else if (station2ComboBox.Text == "AIM-120C x2")
            {
                station2StoreExport = "LAU-115_2*LAU-127_AIM-120C";
            }
            else if (station2ComboBox.Text == "AIM-9L x2")
            {
                station2StoreExport = "LAU-115_2*LAU-127_AIM-9L";
            }
            else if (station2ComboBox.Text == "AIM-9M x2")
            {
                station2StoreExport = "LAU-115_2*LAU-127_AIM-9M";
            }
            else if (station2ComboBox.Text == "AIM-9X x2")
            {
                station2StoreExport = "LAU-115_2*LAU-127_AIM-9X";
            }
            else if (station2ComboBox.Text == "CAP-9M x2")
            {
                station2StoreExport = "LAU-115_2*LAU-127_CATM-9M";
            }
            else if (station2ComboBox.Text == "AIM-120B")
            {
                station2StoreExport = "{LAU-115 - AIM-120B}";
            }
            else if (station2ComboBox.Text == "AIM-120C")
            {
                station2StoreExport = "{LAU-115 - AIM-120C}";
            }
            else if (station2ComboBox.Text == "AIM-7M")
            {
                station2StoreExport = "{LAU-115 - AIM-7M}";
            }
            else if (station2ComboBox.Text == "AIM-7F")
            {
                station2StoreExport = "{LAU-115 - AIM-7F}";
            }
            else if (station2ComboBox.Text == "AIM-7MH")
            {
                station2StoreExport = "{LAU-115 - AIM-7H}";
            }
            else if (station2ComboBox.Text == "AN/ASQ-T50 TCTS Pod")//?
            {
                station2StoreExport = "{AIS_ASQ_T50}";
            }
            else if (station2ComboBox.Text == "CBU-99 x2")
            {
                station2StoreExport = "{BRU33_2X_CBU-99}";
            }
            else if (station2ComboBox.Text == "GBU-12 x2")
            {
                station2StoreExport = "{BRU33_2X_GBU-12}";
            }
            else if (station2ComboBox.Text == "Mk-20 Rockeye x2")
            {
                station2StoreExport = "{BRU33_2X_ROCKEYE}";
            }
            else if (station2ComboBox.Text == "Mk-82 x2")
            {
                station2StoreExport = "{BRU33_2X_MK-82}";
            }
            else if (station2ComboBox.Text == "Mk-82 SnakeEye x2")
            {
                station2StoreExport = "{BRU33_2X_MK-82_Snakeye}";
            }
            else if (station2ComboBox.Text == "Mk-82Y x2")
            {
                station2StoreExport = "{BRU33_2X_MK-82Y}";
            }
            else if (station2ComboBox.Text == "Mk-83 x2")
            {
                station2StoreExport = "{BRU33_2X_MK-83}";
            }
            else if (station2ComboBox.Text == "BDU-33 x6")
            {
                station2StoreExport = "{BRU41_6X_BDU-33}";
            }
            else if (station2ComboBox.Text == "GBU-38 x2")
            {
                station2StoreExport = "{BRU55_2*GBU-38}";
            }
            else if (station2ComboBox.Text == "CBU-99")
            {
                station2StoreExport = "{CBU_99}";
            }
            else if (station2ComboBox.Text == "GBU-10")
            {
                station2StoreExport = "{51F9AAE5-964F-4D21-83FB-502E3BFE5F8A}";
            }
            else if (station2ComboBox.Text == "GBU-12")
            {
                station2StoreExport = "{DB769D48-67D7-42ED-A2BE-108D566C8B1E}";
            }
            else if (station2ComboBox.Text == "GBU-16")
            {
                station2StoreExport = "{0D33DDAE-524F-4A4E-B5B8-621754FE3ADE}";
            }
            else if (station2ComboBox.Text == "GBU-31")
            {
                station2StoreExport = "{GBU-31}";
            }
            else if (station2ComboBox.Text == "GBU-31(V)3/B")
            {
                station2StoreExport = "{GBU-31V3B}";
            }
            else if (station2ComboBox.Text == "GBU-38")
            {
                station2StoreExport = "{GBU-38}";
            }
            else if (station2ComboBox.Text == "Mk-20")
            {
                station2StoreExport = "{ADD3FAE1-EBF6-4EF9-8EFC-B36B5DDF1E6B}";
            }
            else if (station2ComboBox.Text == "Mk-82")
            {
                station2StoreExport = "{BCE4E030-38E9-423E-98ED-24BE3DA87C32}";
            }
            else if (station2ComboBox.Text == "Mk-82 SnakeEye")
            {
                station2StoreExport = "{Mk82SNAKEYE}";
            }
            else if (station2ComboBox.Text == "Mk-82Y")
            {
                station2StoreExport = "{Mk_82Y}";
            }
            else if (station2ComboBox.Text == "Mk-83")
            {
                station2StoreExport = "{7A44FF09-527C-4B7E-B42B-3F111CFE50FB}";
            }
            else if (station2ComboBox.Text == "Mk-84")
            {
                station2StoreExport = "{AB8B8299-F1CC-4359-89B5-2172E0CF4A5A}";
            }
            else if (station2ComboBox.Text == "AGM-154A")
            {
                station2StoreExport = "{AGM-154A}";
            }
            else if (station2ComboBox.Text == "AGM-154C")
            {
                station2StoreExport = "{9BCC2A2B-5708-4860-B1F1-053A18442067}";//?
            }
            else if (station2ComboBox.Text == "AGM-88")
            {
                station2StoreExport = "{B06DD79A-F21E-4EB9-BD9D-AB3844618C93}";
            }
            else if (station2ComboBox.Text == "AGM-154A x2")
            {
                station2StoreExport = "{BRU55_2*AGM-154A}";
            }
            else if (station2ComboBox.Text == "AGM-154C x2")
            {
                station2StoreExport = "{BRU55_2*AGM-154C}";
            }
            else if (station2ComboBox.Text == "AGM-65E")
            {
                station2StoreExport = "{F16A4DE0-116C-4A71-97F0-2CF85B0313EC}";
            }
            else if (station2ComboBox.Text == "AGM-65F")
            {
                station2StoreExport = "LAU_117_AGM_65F";
            }
            else if (station2ComboBox.Text == "4 ZUNI MK 71 x2")
            {
                station2StoreExport = "{BRU33_2*LAU10}";
            }
            else if (station2ComboBox.Text == "19 2.75' rockets M151 HE x2")
            {
                station2StoreExport = "{BRU33_2*LAU61}";
            }
            else if (station2ComboBox.Text == "7 2.75' rockets MK151 (HE) x2")
            {
                station2StoreExport = "{BRU33_2*LAU68}";
            }
            else if (station2ComboBox.Text == "7 2.75' rockets M5 (HE) x2")
            {
                station2StoreExport = "{BRU33_2*LAU68_MK5}";
            }
            else if (station2ComboBox.Text == "4 ZUNI MK 71")
            {
                station2StoreExport = "{BRU33_LAU10}";
            }
            else if (station2ComboBox.Text == "19 2.75' rockets M151 HE")
            {
                station2StoreExport = "{BRU33_LAU61}";
            }
            else if (station2ComboBox.Text == "7 2.75' rockets MK151 (HE)")
            {
                station2StoreExport = "{BRU33_LAU68}";
            }
            else if (station2ComboBox.Text == "7 2.75' rockets M5 (HE)")
            {
                station2StoreExport = "{BRU33_LAU68_MK5}";
            }
            else if (station2ComboBox.Text == "FPU-8A Fuel Tank 330 gallons")
            {
                station2StoreExport = "{FPU_8A_FUEL_TANK}";
            }
        }
        public void assignStation8ExportIDs()
        {
            if (station8ComboBox.Text == "Empty")
            {
                station8StoreExport = "";
            }
            else if (station8ComboBox.Text == "AIM-9L")
            {
                station8StoreExport = "LAU-115_LAU-127_AIM-9L";
            }
            else if (station8ComboBox.Text == "AIM-9M")
            {
                station8StoreExport = "LAU-115_LAU-127_AIM-9M";
            }
            else if (station8ComboBox.Text == "AIM-9X")
            {
                station8StoreExport = "LAU-115_LAU-127_AIM-9X";
            }
            else if (station8ComboBox.Text == "CAP-9M")
            {
                station8StoreExport = "LAU-115_LAU-127_CATM-9M";
            }
            else if (station8ComboBox.Text == "AIM-120B x2")
            {
                station8StoreExport = "LAU-115_2*LAU-127_AIM-120B";
            }
            else if (station8ComboBox.Text == "AIM-120C x2")
            {
                station8StoreExport = "LAU-115_2*LAU-127_AIM-120C";
            }
            else if (station8ComboBox.Text == "AIM-9L x2")
            {
                station8StoreExport = "LAU-115_2*LAU-127_AIM-9L";
            }
            else if (station8ComboBox.Text == "AIM-9M x2")
            {
                station8StoreExport = "LAU-115_2*LAU-127_AIM-9M";
            }
            else if (station8ComboBox.Text == "AIM-9X x2")
            {
                station8StoreExport = "LAU-115_2*LAU-127_AIM-9X";
            }
            else if (station8ComboBox.Text == "CAP-9M x2")
            {
                station8StoreExport = "LAU-115_2*LAU-127_CATM-9M";
            }
            else if (station8ComboBox.Text == "AIM-120B")
            {
                station8StoreExport = "{LAU-115 - AIM-120B}";
            }
            else if (station8ComboBox.Text == "AIM-120C")
            {
                station8StoreExport = "{LAU-115 - AIM-120C}";
            }
            else if (station8ComboBox.Text == "AIM-7M")
            {
                station8StoreExport = "{LAU-115 - AIM-7M}";
            }
            else if (station8ComboBox.Text == "AIM-7F")
            {
                station8StoreExport = "{LAU-115 - AIM-7F}";
            }
            else if (station8ComboBox.Text == "AIM-7MH")
            {
                station8StoreExport = "{LAU-115 - AIM-7H}";
            }
            else if (station8ComboBox.Text == "AN/ASQ-T50 TCTS Pod")//?
            {
                station8StoreExport = "{AIS_ASQ_T50}";
            }
            else if (station8ComboBox.Text == "CBU-99 x2")
            {
                station8StoreExport = "{BRU33_2X_CBU-99}";
            }
            else if (station8ComboBox.Text == "GBU-12 x2")
            {
                station8StoreExport = "{BRU33_2X_GBU-12}";
            }
            else if (station8ComboBox.Text == "Mk-20 Rockeye x2")
            {
                station8StoreExport = "{BRU33_2X_ROCKEYE}";
            }
            else if (station8ComboBox.Text == "Mk-82 x2")
            {
                station8StoreExport = "{BRU33_2X_MK-82}";
            }
            else if (station8ComboBox.Text == "Mk-82 SnakeEye x2")
            {
                station8StoreExport = "{BRU33_2X_MK-82_Snakeye}";
            }
            else if (station8ComboBox.Text == "Mk-82Y x2")
            {
                station8StoreExport = "{BRU33_2X_MK-82Y}";
            }
            else if (station8ComboBox.Text == "Mk-83 x2")
            {
                station8StoreExport = "{BRU33_2X_MK-83}";
            }
            else if (station8ComboBox.Text == "BDU-33 x6")
            {
                station8StoreExport = "{BRU41_6X_BDU-33}";
            }
            else if (station8ComboBox.Text == "GBU-38 x2")
            {
                station8StoreExport = "{BRU55_2*GBU-38}";
            }
            else if (station8ComboBox.Text == "CBU-99")
            {
                station8StoreExport = "{CBU_99}";
            }
            else if (station8ComboBox.Text == "GBU-10")
            {
                station8StoreExport = "{51F9AAE5-964F-4D21-83FB-502E3BFE5F8A}";
            }
            else if (station8ComboBox.Text == "GBU-12")
            {
                station8StoreExport = "{DB769D48-67D7-42ED-A2BE-108D566C8B1E}";
            }
            else if (station8ComboBox.Text == "GBU-16")
            {
                station8StoreExport = "{0D33DDAE-524F-4A4E-B5B8-621754FE3ADE}";
            }
            else if (station8ComboBox.Text == "GBU-31")
            {
                station8StoreExport = "{GBU-31}";
            }
            else if (station8ComboBox.Text == "GBU-31(V)3/B")
            {
                station8StoreExport = "{GBU-31V3B}";
            }
            else if (station8ComboBox.Text == "GBU-38")
            {
                station8StoreExport = "{GBU-38}";
            }
            else if (station8ComboBox.Text == "Mk-20")
            {
                station8StoreExport = "{ADD3FAE1-EBF6-4EF9-8EFC-B36B5DDF1E6B}";
            }
            else if (station8ComboBox.Text == "Mk-82")
            {
                station8StoreExport = "{BCE4E030-38E9-423E-98ED-24BE3DA87C32}";
            }
            else if (station8ComboBox.Text == "Mk-82 SnakeEye")
            {
                station8StoreExport = "{Mk82SNAKEYE}";
            }
            else if (station8ComboBox.Text == "Mk-82Y")
            {
                station8StoreExport = "{Mk_82Y}";
            }
            else if (station8ComboBox.Text == "Mk-83")
            {
                station8StoreExport = "{7A44FF09-527C-4B7E-B42B-3F111CFE50FB}";
            }
            else if (station8ComboBox.Text == "Mk-84")
            {
                station8StoreExport = "{AB8B8299-F1CC-4359-89B5-2172E0CF4A5A}";
            }
            else if (station8ComboBox.Text == "AGM-154A")
            {
                station8StoreExport = "{AGM-154A}";
            }
            else if (station8ComboBox.Text == "AGM-154C")
            {
                station8StoreExport = "{9BCC2A2B-5708-4860-B1F1-053A18442067}";//?
            }
            else if (station8ComboBox.Text == "AGM-88")
            {
                station8StoreExport = "{B06DD79A-F21E-4EB9-BD9D-AB3844618C93}";
            }
            else if (station8ComboBox.Text == "AGM-154A x2")
            {
                station8StoreExport = "{BRU55_2*AGM-154A}";
            }
            else if (station8ComboBox.Text == "AGM-154C x2")
            {
                station8StoreExport = "{BRU55_2*AGM-154C}";
            }
            else if (station8ComboBox.Text == "AGM-65E")
            {
                station8StoreExport = "{F16A4DE0-116C-4A71-97F0-2CF85B0313EC}";
            }
            else if (station8ComboBox.Text == "AGM-65F")
            {
                station8StoreExport = "LAU_117_AGM_65F";
            }
            else if (station8ComboBox.Text == "4 ZUNI MK 71 x2")
            {
                station8StoreExport = "{BRU33_2*LAU10}";
            }
            else if (station8ComboBox.Text == "19 2.75' rockets M151 HE x2")
            {
                station8StoreExport = "{BRU33_2*LAU61}";
            }
            else if (station8ComboBox.Text == "7 2.75' rockets MK151 (HE) x2")
            {
                station8StoreExport = "{BRU33_2*LAU68}";
            }
            else if (station8ComboBox.Text == "7 2.75' rockets M5 (HE) x2")
            {
                station8StoreExport = "{BRU33_2*LAU68_MK5}";
            }
            else if (station8ComboBox.Text == "4 ZUNI MK 71")
            {
                station8StoreExport = "{BRU33_LAU10}";
            }
            else if (station8ComboBox.Text == "19 2.75' rockets M151 HE")
            {
                station8StoreExport = "{BRU33_LAU61}";
            }
            else if (station8ComboBox.Text == "7 2.75' rockets MK151 (HE)")
            {
                station8StoreExport = "{BRU33_LAU68}";
            }
            else if (station8ComboBox.Text == "7 2.75' rockets M5 (HE)")
            {
                station8StoreExport = "{BRU33_LAU68_MK5}";
            }
            else if (station8ComboBox.Text == "FPU-8A Fuel Tank 330 gallons")
            {
                station8StoreExport = "{FPU_8A_FUEL_TANK}";
            }
        }
        public void assignStation3ExportIDs()
        {
            if (station3ComboBox.Text == "Empty")
            {
                station3StoreExport = "";
            }
            else if (station3ComboBox.Text == "AIM-9L")
            {
                station3StoreExport = "LAU-115_LAU-127_AIM-9L";
            }
            else if (station3ComboBox.Text == "AIM-9M")
            {
                station3StoreExport = "LAU-115_LAU-127_AIM-9M";
            }
            else if (station3ComboBox.Text == "AIM-9X")
            {
                station3StoreExport = "LAU-115_LAU-127_AIM-9X";
            }
            else if (station3ComboBox.Text == "CAP-9M")
            {
                station3StoreExport = "LAU-115_LAU-127_CATM-9M";
            }
            else if (station3ComboBox.Text == "AIM-120B x2")
            {
                station3StoreExport = "LAU-115_2*LAU-127_AIM-120B";
            }
            else if (station3ComboBox.Text == "AIM-120C x2")
            {
                station3StoreExport = "LAU-115_2*LAU-127_AIM-120C";
            }
            else if (station3ComboBox.Text == "AIM-9L x2")
            {
                station3StoreExport = "LAU-115_2*LAU-127_AIM-9L";
            }
            else if (station3ComboBox.Text == "AIM-9M x2")
            {
                station3StoreExport = "LAU-115_2*LAU-127_AIM-9M";
            }
            else if (station3ComboBox.Text == "AIM-9X x2")
            {
                station3StoreExport = "LAU-115_2*LAU-127_AIM-9X";
            }
            else if (station3ComboBox.Text == "CAP-9M x2")
            {
                station3StoreExport = "LAU-115_2*LAU-127_CATM-9M";
            }
            else if (station3ComboBox.Text == "AIM-120B")
            {
                station3StoreExport = "{LAU-115 - AIM-120B}";
            }
            else if (station3ComboBox.Text == "AIM-120C")
            {
                station3StoreExport = "{LAU-115 - AIM-120C}";
            }
            else if (station3ComboBox.Text == "AIM-7M")
            {
                station3StoreExport = "{LAU-115 - AIM-7M}";
            }
            else if (station3ComboBox.Text == "AIM-7F")
            {
                station3StoreExport = "{LAU-115 - AIM-7F}";
            }
            else if (station3ComboBox.Text == "AIM-7MH")
            {
                station3StoreExport = "{LAU-115 - AIM-7H}";
            }
            else if (station3ComboBox.Text == "AN/ASQ-T50 TCTS Pod")//?
            {
                station3StoreExport = "{AIS_ASQ_T50}";
            }
            else if (station3ComboBox.Text == "CBU-99 x2")
            {
                station3StoreExport = "{BRU33_2X_CBU-99}";
            }
            else if (station3ComboBox.Text == "GBU-12 x2")
            {
                station3StoreExport = "{BRU33_2X_GBU-12}";
            }
            else if (station3ComboBox.Text == "Mk-20 Rockeye x2")
            {
                station3StoreExport = "{BRU33_2X_ROCKEYE}";
            }
            else if (station3ComboBox.Text == "Mk-82 x2")
            {
                station3StoreExport = "{BRU33_2X_MK-82}";
            }
            else if (station3ComboBox.Text == "Mk-82 SnakeEye x2")
            {
                station3StoreExport = "{BRU33_2X_MK-82_Snakeye}";
            }
            else if (station3ComboBox.Text == "Mk-82Y x2")
            {
                station3StoreExport = "{BRU33_2X_MK-82Y}";
            }
            else if (station3ComboBox.Text == "Mk-83 x2")
            {
                station3StoreExport = "{BRU33_2X_MK-83}";
            }
            else if (station3ComboBox.Text == "BDU-33 x6")
            {
                station3StoreExport = "{BRU41_6X_BDU-33}";
            }
            else if (station3ComboBox.Text == "GBU-38 x2")
            {
                station3StoreExport = "{BRU55_2*GBU-38}";
            }
            else if (station3ComboBox.Text == "CBU-99")
            {
                station3StoreExport = "{CBU_99}";
            }
            else if (station3ComboBox.Text == "GBU-10")
            {
                station3StoreExport = "{51F9AAE5-964F-4D21-83FB-502E3BFE5F8A}";
            }
            else if (station3ComboBox.Text == "GBU-12")
            {
                station3StoreExport = "{DB769D48-67D7-42ED-A2BE-108D566C8B1E}";
            }
            else if (station3ComboBox.Text == "GBU-16")
            {
                station3StoreExport = "{0D33DDAE-524F-4A4E-B5B8-621754FE3ADE}";
            }
            else if (station3ComboBox.Text == "GBU-31")
            {
                station3StoreExport = "{GBU-31}";
            }
            else if (station3ComboBox.Text == "GBU-31(V)3/B")
            {
                station3StoreExport = "{GBU-31V3B}";
            }
            else if (station3ComboBox.Text == "GBU-38")
            {
                station3StoreExport = "{GBU-38}";
            }
            else if (station3ComboBox.Text == "Mk-20")
            {
                station3StoreExport = "{ADD3FAE1-EBF6-4EF9-8EFC-B36B5DDF1E6B}";
            }
            else if (station3ComboBox.Text == "Mk-82")
            {
                station3StoreExport = "{BCE4E030-38E9-423E-98ED-24BE3DA87C32}";
            }
            else if (station3ComboBox.Text == "Mk-82 SnakeEye")
            {
                station3StoreExport = "{Mk82SNAKEYE}";
            }
            else if (station3ComboBox.Text == "Mk-82Y")
            {
                station3StoreExport = "{Mk_82Y}";
            }
            else if (station3ComboBox.Text == "Mk-83")
            {
                station3StoreExport = "{7A44FF09-527C-4B7E-B42B-3F111CFE50FB}";
            }
            else if (station3ComboBox.Text == "Mk-84")
            {
                station3StoreExport = "{AB8B8299-F1CC-4359-89B5-2172E0CF4A5A}";
            }
            else if (station3ComboBox.Text == "AGM-154A")
            {
                station3StoreExport = "{AGM-154A}";
            }
            else if (station3ComboBox.Text == "AGM-154C")
            {
                station3StoreExport = "{9BCC2A2B-5708-4860-B1F1-053A18442067}";//?
            }
            else if (station3ComboBox.Text == "AGM-88")
            {
                station3StoreExport = "{B06DD79A-F21E-4EB9-BD9D-AB3844618C93}";
            }
            else if (station3ComboBox.Text == "AGM-154A x2")
            {
                station3StoreExport = "{BRU55_2*AGM-154A}";
            }
            else if (station3ComboBox.Text == "AGM-154C x2")
            {
                station3StoreExport = "{BRU55_2*AGM-154C}";
            }
            else if (station3ComboBox.Text == "AGM-65E")
            {
                station3StoreExport = "{F16A4DE0-116C-4A71-97F0-2CF85B0313EC}";
            }
            else if (station3ComboBox.Text == "AGM-65F")
            {
                station3StoreExport = "LAU_117_AGM_65F";
            }
            else if (station3ComboBox.Text == "4 ZUNI MK 71 x2")
            {
                station3StoreExport = "{BRU33_2*LAU10}";
            }
            else if (station3ComboBox.Text == "19 2.75' rockets M151 HE x2")
            {
                station3StoreExport = "{BRU33_2*LAU61}";
            }
            else if (station3ComboBox.Text == "7 2.75' rockets MK151 (HE) x2")
            {
                station3StoreExport = "{BRU33_2*LAU68}";
            }
            else if (station3ComboBox.Text == "7 2.75' rockets M5 (HE) x2")
            {
                station3StoreExport = "{BRU33_2*LAU68_MK5}";
            }
            else if (station3ComboBox.Text == "4 ZUNI MK 71")
            {
                station3StoreExport = "{BRU33_LAU10}";
            }
            else if (station3ComboBox.Text == "19 2.75' rockets M151 HE")
            {
                station3StoreExport = "{BRU33_LAU61}";
            }
            else if (station3ComboBox.Text == "7 2.75' rockets MK151 (HE)")
            {
                station3StoreExport = "{BRU33_LAU68}";
            }
            else if (station3ComboBox.Text == "7 2.75' rockets M5 (HE)")
            {
                station3StoreExport = "{BRU33_LAU68_MK5}";
            }
            else if (station3ComboBox.Text == "FPU-8A Fuel Tank 330 gallons")
            {
                station3StoreExport = "{FPU_8A_FUEL_TANK}";
            }
        }
        public void assignStation7ExportIDs()
        {
            if (station7ComboBox.Text == "Empty")
            {
                station7StoreExport = "";
            }
            else if (station7ComboBox.Text == "AIM-9L")
            {
                station7StoreExport = "LAU-115_LAU-127_AIM-9L";
            }
            else if (station7ComboBox.Text == "AIM-9M")
            {
                station7StoreExport = "LAU-115_LAU-127_AIM-9M";
            }
            else if (station7ComboBox.Text == "AIM-9X")
            {
                station7StoreExport = "LAU-115_LAU-127_AIM-9X";
            }
            else if (station7ComboBox.Text == "CAP-9M")
            {
                station7StoreExport = "LAU-115_LAU-127_CATM-9M";
            }
            else if (station7ComboBox.Text == "AIM-120B x2")
            {
                station7StoreExport = "LAU-115_2*LAU-127_AIM-120B";
            }
            else if (station7ComboBox.Text == "AIM-120C x2")
            {
                station7StoreExport = "LAU-115_2*LAU-127_AIM-120C";
            }
            else if (station7ComboBox.Text == "AIM-9L x2")
            {
                station7StoreExport = "LAU-115_2*LAU-127_AIM-9L";
            }
            else if (station7ComboBox.Text == "AIM-9M x2")
            {
                station7StoreExport = "LAU-115_2*LAU-127_AIM-9M";
            }
            else if (station7ComboBox.Text == "AIM-9X x2")
            {
                station7StoreExport = "LAU-115_2*LAU-127_AIM-9X";
            }
            else if (station7ComboBox.Text == "CAP-9M x2")
            {
                station7StoreExport = "LAU-115_2*LAU-127_CATM-9M";
            }
            else if (station7ComboBox.Text == "AIM-120B")
            {
                station7StoreExport = "{LAU-115 - AIM-120B}";
            }
            else if (station7ComboBox.Text == "AIM-120C")
            {
                station7StoreExport = "{LAU-115 - AIM-120C}";
            }
            else if (station7ComboBox.Text == "AIM-7M")
            {
                station7StoreExport = "{LAU-115 - AIM-7M}";
            }
            else if (station7ComboBox.Text == "AIM-7F")
            {
                station7StoreExport = "{LAU-115 - AIM-7F}";
            }
            else if (station7ComboBox.Text == "AIM-7MH")
            {
                station7StoreExport = "{LAU-115 - AIM-7H}";
            }
            else if (station7ComboBox.Text == "AN/ASQ-T50 TCTS Pod")//?
            {
                station7StoreExport = "{AIS_ASQ_T50}";
            }
            else if (station7ComboBox.Text == "CBU-99 x2")
            {
                station7StoreExport = "{BRU33_2X_CBU-99}";
            }
            else if (station7ComboBox.Text == "GBU-12 x2")
            {
                station7StoreExport = "{BRU33_2X_GBU-12}";
            }
            else if (station7ComboBox.Text == "Mk-20 Rockeye x2")
            {
                station7StoreExport = "{BRU33_2X_ROCKEYE}";
            }
            else if (station7ComboBox.Text == "Mk-82 x2")
            {
                station7StoreExport = "{BRU33_2X_MK-82}";
            }
            else if (station7ComboBox.Text == "Mk-82 SnakeEye x2")
            {
                station7StoreExport = "{BRU33_2X_MK-82_Snakeye}";
            }
            else if (station7ComboBox.Text == "Mk-82Y x2")
            {
                station7StoreExport = "{BRU33_2X_MK-82Y}";
            }
            else if (station7ComboBox.Text == "Mk-83 x2")
            {
                station7StoreExport = "{BRU33_2X_MK-83}";
            }
            else if (station7ComboBox.Text == "BDU-33 x6")
            {
                station7StoreExport = "{BRU41_6X_BDU-33}";
            }
            else if (station7ComboBox.Text == "GBU-38 x2")
            {
                station7StoreExport = "{BRU55_2*GBU-38}";
            }
            else if (station7ComboBox.Text == "CBU-99")
            {
                station7StoreExport = "{CBU_99}";
            }
            else if (station7ComboBox.Text == "GBU-10")
            {
                station7StoreExport = "{51F9AAE5-964F-4D21-83FB-502E3BFE5F8A}";
            }
            else if (station7ComboBox.Text == "GBU-12")
            {
                station7StoreExport = "{DB769D48-67D7-42ED-A2BE-108D566C8B1E}";
            }
            else if (station7ComboBox.Text == "GBU-16")
            {
                station7StoreExport = "{0D33DDAE-524F-4A4E-B5B8-621754FE3ADE}";
            }
            else if (station7ComboBox.Text == "GBU-31")
            {
                station7StoreExport = "{GBU-31}";
            }
            else if (station7ComboBox.Text == "GBU-31(V)3/B")
            {
                station7StoreExport = "{GBU-31V3B}";
            }
            else if (station7ComboBox.Text == "GBU-38")
            {
                station7StoreExport = "{GBU-38}";
            }
            else if (station7ComboBox.Text == "Mk-20")
            {
                station7StoreExport = "{ADD3FAE1-EBF6-4EF9-8EFC-B36B5DDF1E6B}";
            }
            else if (station7ComboBox.Text == "Mk-82")
            {
                station7StoreExport = "{BCE4E030-38E9-423E-98ED-24BE3DA87C32}";
            }
            else if (station7ComboBox.Text == "Mk-82 SnakeEye")
            {
                station7StoreExport = "{Mk82SNAKEYE}";
            }
            else if (station7ComboBox.Text == "Mk-82Y")
            {
                station7StoreExport = "{Mk_82Y}";
            }
            else if (station7ComboBox.Text == "Mk-83")
            {
                station7StoreExport = "{7A44FF09-527C-4B7E-B42B-3F111CFE50FB}";
            }
            else if (station7ComboBox.Text == "Mk-84")
            {
                station7StoreExport = "{AB8B8299-F1CC-4359-89B5-2172E0CF4A5A}";
            }
            else if (station7ComboBox.Text == "AGM-154A")
            {
                station7StoreExport = "{AGM-154A}";
            }
            else if (station7ComboBox.Text == "AGM-154C")
            {
                station7StoreExport = "{9BCC2A2B-5708-4860-B1F1-053A18442067}";//?
            }
            else if (station7ComboBox.Text == "AGM-88")
            {
                station7StoreExport = "{B06DD79A-F21E-4EB9-BD9D-AB3844618C93}";
            }
            else if (station7ComboBox.Text == "AGM-154A x2")
            {
                station7StoreExport = "{BRU55_2*AGM-154A}";
            }
            else if (station7ComboBox.Text == "AGM-154C x2")
            {
                station7StoreExport = "{BRU55_2*AGM-154C}";
            }
            else if (station7ComboBox.Text == "AGM-65E")
            {
                station7StoreExport = "{F16A4DE0-116C-4A71-97F0-2CF85B0313EC}";
            }
            else if (station7ComboBox.Text == "AGM-65F")
            {
                station7StoreExport = "LAU_117_AGM_65F";
            }
            else if (station7ComboBox.Text == "4 ZUNI MK 71 x2")
            {
                station7StoreExport = "{BRU33_2*LAU10}";
            }
            else if (station7ComboBox.Text == "19 2.75' rockets M151 HE x2")
            {
                station7StoreExport = "{BRU33_2*LAU61}";
            }
            else if (station7ComboBox.Text == "7 2.75' rockets MK151 (HE) x2")
            {
                station7StoreExport = "{BRU33_2*LAU68}";
            }
            else if (station7ComboBox.Text == "7 2.75' rockets M5 (HE) x2")
            {
                station7StoreExport = "{BRU33_2*LAU68_MK5}";
            }
            else if (station7ComboBox.Text == "4 ZUNI MK 71")
            {
                station7StoreExport = "{BRU33_LAU10}";
            }
            else if (station7ComboBox.Text == "19 2.75' rockets M151 HE")
            {
                station7StoreExport = "{BRU33_LAU61}";
            }
            else if (station7ComboBox.Text == "7 2.75' rockets MK151 (HE)")
            {
                station7StoreExport = "{BRU33_LAU68}";
            }
            else if (station7ComboBox.Text == "7 2.75' rockets M5 (HE)")
            {
                station7StoreExport = "{BRU33_LAU68_MK5}";
            }
            else if (station7ComboBox.Text == "FPU-8A Fuel Tank 330 gallons")
            {
                station7StoreExport = "{FPU_8A_FUEL_TANK}";
            }
        }
        public void assignStation4ExportIDs()
        {
            if (station4ComboBox.Text == "Empty")
            {
                station4StoreExport = "";
            }
            else if (station4ComboBox.Text == "AIM-9L")
            {
                station4StoreExport = "LAU-115_LAU-127_AIM-9L";
            }
            else if (station4ComboBox.Text == "AIM-9M")
            {
                station4StoreExport = "LAU-115_LAU-127_AIM-9M";
            }
            else if (station4ComboBox.Text == "AIM-9X")
            {
                station4StoreExport = "LAU-115_LAU-127_AIM-9X";
            }
            else if (station4ComboBox.Text == "CAP-9M")
            {
                station4StoreExport = "LAU-115_LAU-127_CATM-9M";
            }
            else if (station4ComboBox.Text == "AIM-120B x2")
            {
                station4StoreExport = "LAU-115_2*LAU-127_AIM-120B";
            }
            else if (station4ComboBox.Text == "AIM-120C x2")
            {
                station4StoreExport = "LAU-115_2*LAU-127_AIM-120C";
            }
            else if (station4ComboBox.Text == "AIM-9L x2")
            {
                station4StoreExport = "LAU-115_2*LAU-127_AIM-9L";
            }
            else if (station4ComboBox.Text == "AIM-9M x2")
            {
                station4StoreExport = "LAU-115_2*LAU-127_AIM-9M";
            }
            else if (station4ComboBox.Text == "AIM-9X x2")
            {
                station4StoreExport = "LAU-115_2*LAU-127_AIM-9X";
            }
            else if (station4ComboBox.Text == "CAP-9M x2")
            {
                station4StoreExport = "LAU-115_2*LAU-127_CATM-9M";
            }
            else if (station4ComboBox.Text == "AIM-120B")
            {
                station4StoreExport = "{C8E06185-7CD6-4C90-959F-044679E90751}";//?
            }
            else if (station4ComboBox.Text == "AIM-120C")
            {
                station4StoreExport = "{40EF17B7-F508-45de-8566-6FFECC0C1AB8}";
            }
            else if (station4ComboBox.Text == "AIM-7M")
            {
                station4StoreExport = "{8D399DDA-FF81-4F14-904D-099B34FE7918}";
            }
            else if (station4ComboBox.Text == "AIM-7F")
            {
                station4StoreExport = "{AIM-7F}";
            }
            else if (station4ComboBox.Text == "AIM-7MH")
            {
                station4StoreExport = "{AIM-7H}";
            }
            else if (station4ComboBox.Text == "AN/ASQ-T50 TCTS Pod")//?
            {
                station4StoreExport = "{AIS_ASQ_T50}";
            }
            else if (station4ComboBox.Text == "CBU-99 x2")
            {
                station4StoreExport = "{BRU33_2X_CBU-99}";
            }
            else if (station4ComboBox.Text == "GBU-12 x2")
            {
                station4StoreExport = "{BRU33_2X_GBU-12}";
            }
            else if (station4ComboBox.Text == "Mk-20 Rockeye x2")
            {
                station4StoreExport = "{BRU33_2X_ROCKEYE}";
            }
            else if (station4ComboBox.Text == "Mk-82 x2")
            {
                station4StoreExport = "{BRU33_2X_MK-82}";
            }
            else if (station4ComboBox.Text == "Mk-82 SnakeEye x2")
            {
                station4StoreExport = "{BRU33_2X_MK-82_Snakeye}";
            }
            else if (station4ComboBox.Text == "Mk-82Y x2")
            {
                station4StoreExport = "{BRU33_2X_MK-82Y}";
            }
            else if (station4ComboBox.Text == "Mk-83 x2")
            {
                station4StoreExport = "{BRU33_2X_MK-83}";
            }
            else if (station4ComboBox.Text == "BDU-33 x6")
            {
                station4StoreExport = "{BRU41_6X_BDU-33}";
            }
            else if (station4ComboBox.Text == "GBU-38 x2")
            {
                station4StoreExport = "{BRU55_2*GBU-38}";
            }
            else if (station4ComboBox.Text == "CBU-99")
            {
                station4StoreExport = "{CBU_99}";
            }
            else if (station4ComboBox.Text == "GBU-10")
            {
                station4StoreExport = "{51F9AAE5-964F-4D21-83FB-502E3BFE5F8A}";
            }
            else if (station4ComboBox.Text == "GBU-12")
            {
                station4StoreExport = "{DB769D48-67D7-42ED-A2BE-108D566C8B1E}";
            }
            else if (station4ComboBox.Text == "GBU-16")
            {
                station4StoreExport = "{0D33DDAE-524F-4A4E-B5B8-621754FE3ADE}";
            }
            else if (station4ComboBox.Text == "GBU-31")
            {
                station4StoreExport = "{GBU-31}";
            }
            else if (station4ComboBox.Text == "GBU-31(V)3/B")
            {
                station4StoreExport = "{GBU-31V3B}";
            }
            else if (station4ComboBox.Text == "GBU-38")
            {
                station4StoreExport = "{GBU-38}";
            }
            else if (station4ComboBox.Text == "Mk-20")
            {
                station4StoreExport = "{ADD3FAE1-EBF6-4EF9-8EFC-B36B5DDF1E6B}";
            }
            else if (station4ComboBox.Text == "Mk-82")
            {
                station4StoreExport = "{BCE4E030-38E9-423E-98ED-24BE3DA87C32}";
            }
            else if (station4ComboBox.Text == "Mk-82 SnakeEye")
            {
                station4StoreExport = "{Mk82SNAKEYE}";
            }
            else if (station4ComboBox.Text == "Mk-82Y")
            {
                station4StoreExport = "{Mk_82Y}";
            }
            else if (station4ComboBox.Text == "Mk-83")
            {
                station4StoreExport = "{7A44FF09-527C-4B7E-B42B-3F111CFE50FB}";
            }
            else if (station4ComboBox.Text == "Mk-84")
            {
                station4StoreExport = "{AB8B8299-F1CC-4359-89B5-2172E0CF4A5A}";
            }
            else if (station4ComboBox.Text == "AGM-154A")
            {
                station4StoreExport = "{AGM-154A}";
            }
            else if (station4ComboBox.Text == "AGM-154C")
            {
                station4StoreExport = "{9BCC2A2B-5708-4860-B1F1-053A18442067}";//?
            }
            else if (station4ComboBox.Text == "AGM-88")
            {
                station4StoreExport = "{B06DD79A-F21E-4EB9-BD9D-AB3844618C93}";
            }
            else if (station4ComboBox.Text == "AGM-154A x2")
            {
                station4StoreExport = "{BRU55_2*AGM-154A}";
            }
            else if (station4ComboBox.Text == "AGM-154C x2")
            {
                station4StoreExport = "{BRU55_2*AGM-154C}";
            }
            else if (station4ComboBox.Text == "AGM-65E")
            {
                station4StoreExport = "{F16A4DE0-116C-4A71-97F0-2CF85B0313EC}";
            }
            else if (station4ComboBox.Text == "AGM-65F")
            {
                station4StoreExport = "LAU_117_AGM_65F";
            }
            else if (station4ComboBox.Text == "4 ZUNI MK 71 x2")
            {
                station4StoreExport = "{BRU33_2*LAU10}";
            }
            else if (station4ComboBox.Text == "19 2.75' rockets M151 HE x2")
            {
                station4StoreExport = "{BRU33_2*LAU61}";
            }
            else if (station4ComboBox.Text == "7 2.75' rockets MK151 (HE) x2")
            {
                station4StoreExport = "{BRU33_2*LAU68}";
            }
            else if (station4ComboBox.Text == "7 2.75' rockets M5 (HE) x2")
            {
                station4StoreExport = "{BRU33_2*LAU68_MK5}";
            }
            else if (station4ComboBox.Text == "4 ZUNI MK 71")
            {
                station4StoreExport = "{BRU33_LAU10}";
            }
            else if (station4ComboBox.Text == "19 2.75' rockets M151 HE")
            {
                station4StoreExport = "{BRU33_LAU61}";
            }
            else if (station4ComboBox.Text == "7 2.75' rockets MK151 (HE)")
            {
                station4StoreExport = "{BRU33_LAU68}";
            }
            else if (station4ComboBox.Text == "7 2.75' rockets M5 (HE)")
            {
                station4StoreExport = "{BRU33_LAU68_MK5}";
            }
            else if (station4ComboBox.Text == "FPU-8A Fuel Tank 330 gallons")
            {
                station4StoreExport = "{FPU_8A_FUEL_TANK}";
            }
        }
        public void assignStation6ExportIDs()
        {
            if (station6ComboBox.Text == "Empty")
            {
                station6StoreExport = "";
            }
            else if (station6ComboBox.Text == "AIM-9L")
            {
                station6StoreExport = "LAU-115_LAU-127_AIM-9L";
            }
            else if (station6ComboBox.Text == "AIM-9M")
            {
                station6StoreExport = "LAU-115_LAU-127_AIM-9M";
            }
            else if (station6ComboBox.Text == "AIM-9X")
            {
                station6StoreExport = "LAU-115_LAU-127_AIM-9X";
            }
            else if (station6ComboBox.Text == "CAP-9M")
            {
                station6StoreExport = "LAU-115_LAU-127_CATM-9M";
            }
            else if (station6ComboBox.Text == "AIM-120B x2")
            {
                station6StoreExport = "LAU-115_2*LAU-127_AIM-120B";
            }
            else if (station6ComboBox.Text == "AIM-120C x2")
            {
                station6StoreExport = "LAU-115_2*LAU-127_AIM-120C";
            }
            else if (station6ComboBox.Text == "AIM-9L x2")
            {
                station6StoreExport = "LAU-115_2*LAU-127_AIM-9L";
            }
            else if (station6ComboBox.Text == "AIM-9M x2")
            {
                station6StoreExport = "LAU-115_2*LAU-127_AIM-9M";
            }
            else if (station6ComboBox.Text == "AIM-9X x2")
            {
                station6StoreExport = "LAU-115_2*LAU-127_AIM-9X";
            }
            else if (station6ComboBox.Text == "CAP-9M x2")
            {
                station6StoreExport = "LAU-115_2*LAU-127_CATM-9M";
            }
            else if (station6ComboBox.Text == "AIM-120B")
            {
                station6StoreExport = "{C8E06185-7CD6-4C90-959F-044679E90751}";//?
            }
            else if (station6ComboBox.Text == "AIM-120C")
            {
                station6StoreExport = "{40EF17B7-F508-45de-8566-6FFECC0C1AB8}";
            }
            else if (station6ComboBox.Text == "AIM-7M")
            {
                station6StoreExport = "{8D399DDA-FF81-4F14-904D-099B34FE7918}";
            }
            else if (station6ComboBox.Text == "AIM-7F")
            {
                station6StoreExport = "{AIM-7F}";
            }
            else if (station6ComboBox.Text == "AIM-7MH")
            {
                station6StoreExport = "{AIM-7H}";
            }
            else if (station6ComboBox.Text == "AN/ASQ-T50 TCTS Pod")//?
            {
                station6StoreExport = "{AIS_ASQ_T50}";
            }
            else if (station6ComboBox.Text == "CBU-99 x2")
            {
                station6StoreExport = "{BRU33_2X_CBU-99}";
            }
            else if (station6ComboBox.Text == "GBU-12 x2")
            {
                station6StoreExport = "{BRU33_2X_GBU-12}";
            }
            else if (station6ComboBox.Text == "Mk-20 Rockeye x2")
            {
                station6StoreExport = "{BRU33_2X_ROCKEYE}";
            }
            else if (station6ComboBox.Text == "Mk-82 x2")
            {
                station6StoreExport = "{BRU33_2X_MK-82}";
            }
            else if (station6ComboBox.Text == "Mk-82 SnakeEye x2")
            {
                station6StoreExport = "{BRU33_2X_MK-82_Snakeye}";
            }
            else if (station6ComboBox.Text == "Mk-82Y x2")
            {
                station6StoreExport = "{BRU33_2X_MK-82Y}";
            }
            else if (station6ComboBox.Text == "Mk-83 x2")
            {
                station6StoreExport = "{BRU33_2X_MK-83}";
            }
            else if (station6ComboBox.Text == "BDU-33 x6")
            {
                station6StoreExport = "{BRU41_6X_BDU-33}";
            }
            else if (station6ComboBox.Text == "GBU-38 x2")
            {
                station6StoreExport = "{BRU55_2*GBU-38}";
            }
            else if (station6ComboBox.Text == "CBU-99")
            {
                station6StoreExport = "{CBU_99}";
            }
            else if (station6ComboBox.Text == "GBU-10")
            {
                station6StoreExport = "{51F9AAE5-964F-4D21-83FB-502E3BFE5F8A}";
            }
            else if (station6ComboBox.Text == "GBU-12")
            {
                station6StoreExport = "{DB769D48-67D7-42ED-A2BE-108D566C8B1E}";
            }
            else if (station6ComboBox.Text == "GBU-16")
            {
                station6StoreExport = "{0D33DDAE-524F-4A4E-B5B8-621754FE3ADE}";
            }
            else if (station6ComboBox.Text == "GBU-31")
            {
                station6StoreExport = "{GBU-31}";
            }
            else if (station6ComboBox.Text == "GBU-31(V)3/B")
            {
                station6StoreExport = "{GBU-31V3B}";
            }
            else if (station6ComboBox.Text == "GBU-38")
            {
                station6StoreExport = "{GBU-38}";
            }
            else if (station6ComboBox.Text == "Mk-20")
            {
                station6StoreExport = "{ADD3FAE1-EBF6-4EF9-8EFC-B36B5DDF1E6B}";
            }
            else if (station6ComboBox.Text == "Mk-82")
            {
                station6StoreExport = "{BCE4E030-38E9-423E-98ED-24BE3DA87C32}";
            }
            else if (station6ComboBox.Text == "Mk-82 SnakeEye")
            {
                station6StoreExport = "{Mk82SNAKEYE}";
            }
            else if (station6ComboBox.Text == "Mk-82Y")
            {
                station6StoreExport = "{Mk_82Y}";
            }
            else if (station6ComboBox.Text == "Mk-83")
            {
                station6StoreExport = "{7A44FF09-527C-4B7E-B42B-3F111CFE50FB}";
            }
            else if (station6ComboBox.Text == "Mk-84")
            {
                station6StoreExport = "{AB8B8299-F1CC-4359-89B5-2172E0CF4A5A}";
            }
            else if (station6ComboBox.Text == "AGM-154A")
            {
                station6StoreExport = "{AGM-154A}";
            }
            else if (station6ComboBox.Text == "AGM-154C")
            {
                station6StoreExport = "{9BCC2A2B-5708-4860-B1F1-053A18442067}";//?
            }
            else if (station6ComboBox.Text == "AGM-88")
            {
                station6StoreExport = "{B06DD79A-F21E-4EB9-BD9D-AB3844618C93}";
            }
            else if (station6ComboBox.Text == "AGM-154A x2")
            {
                station6StoreExport = "{BRU55_2*AGM-154A}";
            }
            else if (station6ComboBox.Text == "AGM-154C x2")
            {
                station6StoreExport = "{BRU55_2*AGM-154C}";
            }
            else if (station6ComboBox.Text == "AGM-65E")
            {
                station6StoreExport = "{F16A4DE0-116C-4A71-97F0-2CF85B0313EC}";
            }
            else if (station6ComboBox.Text == "AGM-65F")
            {
                station6StoreExport = "LAU_117_AGM_65F";
            }
            else if (station6ComboBox.Text == "4 ZUNI MK 71 x2")
            {
                station6StoreExport = "{BRU33_2*LAU10}";
            }
            else if (station6ComboBox.Text == "19 2.75' rockets M151 HE x2")
            {
                station6StoreExport = "{BRU33_2*LAU61}";
            }
            else if (station6ComboBox.Text == "7 2.75' rockets MK151 (HE) x2")
            {
                station6StoreExport = "{BRU33_2*LAU68}";
            }
            else if (station6ComboBox.Text == "7 2.75' rockets M5 (HE) x2")
            {
                station6StoreExport = "{BRU33_2*LAU68_MK5}";
            }
            else if (station6ComboBox.Text == "4 ZUNI MK 71")
            {
                station6StoreExport = "{BRU33_LAU10}";
            }
            else if (station6ComboBox.Text == "19 2.75' rockets M151 HE")
            {
                station6StoreExport = "{BRU33_LAU61}";
            }
            else if (station6ComboBox.Text == "7 2.75' rockets MK151 (HE)")
            {
                station6StoreExport = "{BRU33_LAU68}";
            }
            else if (station6ComboBox.Text == "7 2.75' rockets M5 (HE)")
            {
                station6StoreExport = "{BRU33_LAU68_MK5}";
            }
            else if (station6ComboBox.Text == "FPU-8A Fuel Tank 330 gallons")
            {
                station6StoreExport = "{FPU_8A_FUEL_TANK}";
            }
        }
        public void assignStation5ExportIDs()
        {
            if (station5ComboBox.Text == "Empty")
            {
                station5StoreExport = "";
            }
            else if (station5ComboBox.Text == "AIM-9L")
            {
                station5StoreExport = "LAU-115_LAU-127_AIM-9L";
            }
            else if (station5ComboBox.Text == "AIM-9M")
            {
                station5StoreExport = "LAU-115_LAU-127_AIM-9M";
            }
            else if (station5ComboBox.Text == "AIM-9X")
            {
                station5StoreExport = "LAU-115_LAU-127_AIM-9X";
            }
            else if (station5ComboBox.Text == "CAP-9M")
            {
                station5StoreExport = "LAU-115_LAU-127_CATM-9M";
            }
            else if (station5ComboBox.Text == "AIM-120B x2")
            {
                station5StoreExport = "LAU-115_2*LAU-127_AIM-120B";
            }
            else if (station5ComboBox.Text == "AIM-120C x2")
            {
                station5StoreExport = "LAU-115_2*LAU-127_AIM-120C";
            }
            else if (station5ComboBox.Text == "AIM-9L x2")
            {
                station5StoreExport = "LAU-115_2*LAU-127_AIM-9L";
            }
            else if (station5ComboBox.Text == "AIM-9M x2")
            {
                station5StoreExport = "LAU-115_2*LAU-127_AIM-9M";
            }
            else if (station5ComboBox.Text == "AIM-9X x2")
            {
                station5StoreExport = "LAU-115_2*LAU-127_AIM-9X";
            }
            else if (station5ComboBox.Text == "CAP-9M x2")
            {
                station5StoreExport = "LAU-115_2*LAU-127_CATM-9M";
            }
            else if (station5ComboBox.Text == "AIM-120B")
            {
                station5StoreExport = "{C8E06185-7CD6-4C90-959F-044679E90751}";//?
            }
            else if (station5ComboBox.Text == "AIM-120C")
            {
                station5StoreExport = "{40EF17B7-F508-45de-8566-6FFECC0C1AB8}";
            }
            else if (station5ComboBox.Text == "AIM-7M")
            {
                station5StoreExport = "{8D399DDA-FF81-4F14-904D-099B34FE7918}";
            }
            else if (station5ComboBox.Text == "AIM-7F")
            {
                station5StoreExport = "{AIM-7F}";
            }
            else if (station5ComboBox.Text == "AIM-7MH")
            {
                station5StoreExport = "{AIM-7H}";
            }
            else if (station5ComboBox.Text == "AN/ASQ-T50 TCTS Pod")//?
            {
                station5StoreExport = "{AIS_ASQ_T50}";
            }
            else if (station5ComboBox.Text == "CBU-99 x2")
            {
                station5StoreExport = "{BRU33_2X_CBU-99}";
            }
            else if (station5ComboBox.Text == "GBU-12 x2")
            {
                station5StoreExport = "{BRU33_2X_GBU-12}";
            }
            else if (station5ComboBox.Text == "Mk-20 Rockeye x2")
            {
                station5StoreExport = "{BRU33_2X_ROCKEYE}";
            }
            else if (station5ComboBox.Text == "Mk-82 x2")
            {
                station5StoreExport = "{BRU33_2X_MK-82}";
            }
            else if (station5ComboBox.Text == "Mk-82 SnakeEye x2")
            {
                station5StoreExport = "{BRU33_2X_MK-82_Snakeye}";
            }
            else if (station5ComboBox.Text == "Mk-82Y x2")
            {
                station5StoreExport = "{BRU33_2X_MK-82Y}";
            }
            else if (station5ComboBox.Text == "Mk-83 x2")
            {
                station5StoreExport = "{BRU33_2X_MK-83}";
            }
            else if (station5ComboBox.Text == "BDU-33 x6")
            {
                station5StoreExport = "{BRU41_6X_BDU-33}";
            }
            else if (station5ComboBox.Text == "GBU-38 x2")
            {
                station5StoreExport = "{BRU55_2*GBU-38}";
            }
            else if (station5ComboBox.Text == "CBU-99")
            {
                station5StoreExport = "{CBU_99}";
            }
            else if (station5ComboBox.Text == "GBU-10")
            {
                station5StoreExport = "{51F9AAE5-964F-4D21-83FB-502E3BFE5F8A}";
            }
            else if (station5ComboBox.Text == "GBU-12")
            {
                station5StoreExport = "{DB769D48-67D7-42ED-A2BE-108D566C8B1E}";
            }
            else if (station5ComboBox.Text == "GBU-16")
            {
                station5StoreExport = "{0D33DDAE-524F-4A4E-B5B8-621754FE3ADE}";
            }
            else if (station5ComboBox.Text == "GBU-31")
            {
                station5StoreExport = "{GBU-31}";
            }
            else if (station5ComboBox.Text == "GBU-31(V)3/B")
            {
                station5StoreExport = "{GBU-31V3B}";
            }
            else if (station5ComboBox.Text == "GBU-38")
            {
                station5StoreExport = "{GBU-38}";
            }
            else if (station5ComboBox.Text == "Mk-20")
            {
                station5StoreExport = "{ADD3FAE1-EBF6-4EF9-8EFC-B36B5DDF1E6B}";
            }
            else if (station5ComboBox.Text == "Mk-82")
            {
                station5StoreExport = "{BCE4E030-38E9-423E-98ED-24BE3DA87C32}";
            }
            else if (station5ComboBox.Text == "Mk-82 SnakeEye")
            {
                station5StoreExport = "{Mk82SNAKEYE}";
            }
            else if (station5ComboBox.Text == "Mk-82Y")
            {
                station5StoreExport = "{Mk_82Y}";
            }
            else if (station5ComboBox.Text == "Mk-83")
            {
                station5StoreExport = "{7A44FF09-527C-4B7E-B42B-3F111CFE50FB}";
            }
            else if (station5ComboBox.Text == "Mk-84")
            {
                station5StoreExport = "{AB8B8299-F1CC-4359-89B5-2172E0CF4A5A}";
            }
            else if (station5ComboBox.Text == "AGM-154A")
            {
                station5StoreExport = "{AGM-154A}";
            }
            else if (station5ComboBox.Text == "AGM-154C")
            {
                station5StoreExport = "{9BCC2A2B-5708-4860-B1F1-053A18442067}";//?
            }
            else if (station5ComboBox.Text == "AGM-88")
            {
                station5StoreExport = "{B06DD79A-F21E-4EB9-BD9D-AB3844618C93}";
            }
            else if (station5ComboBox.Text == "AGM-154A x2")
            {
                station5StoreExport = "{BRU55_2*AGM-154A}";
            }
            else if (station5ComboBox.Text == "AGM-154C x2")
            {
                station5StoreExport = "{BRU55_2*AGM-154C}";
            }
            else if (station5ComboBox.Text == "AGM-65E")
            {
                station5StoreExport = "{F16A4DE0-116C-4A71-97F0-2CF85B0313EC}";
            }
            else if (station5ComboBox.Text == "AGM-65F")
            {
                station5StoreExport = "LAU_117_AGM_65F";
            }
            else if (station5ComboBox.Text == "4 ZUNI MK 71 x2")
            {
                station5StoreExport = "{BRU33_2*LAU10}";
            }
            else if (station5ComboBox.Text == "19 2.75' rockets M151 HE x2")
            {
                station5StoreExport = "{BRU33_2*LAU61}";
            }
            else if (station5ComboBox.Text == "7 2.75' rockets MK151 (HE) x2")
            {
                station5StoreExport = "{BRU33_2*LAU68}";
            }
            else if (station5ComboBox.Text == "7 2.75' rockets M5 (HE) x2")
            {
                station5StoreExport = "{BRU33_2*LAU68_MK5}";
            }
            else if (station5ComboBox.Text == "4 ZUNI MK 71")
            {
                station5StoreExport = "{BRU33_LAU10}";
            }
            else if (station5ComboBox.Text == "19 2.75' rockets M151 HE")
            {
                station5StoreExport = "{BRU33_LAU61}";
            }
            else if (station5ComboBox.Text == "7 2.75' rockets MK151 (HE)")
            {
                station5StoreExport = "{BRU33_LAU68}";
            }
            else if (station5ComboBox.Text == "7 2.75' rockets M5 (HE)")
            {
                station5StoreExport = "{BRU33_LAU68_MK5}";
            }
            else if (station5ComboBox.Text == "FPU-8A Fuel Tank 330 gallons")
            {
                station5StoreExport = "{FPU_8A_FUEL_TANK}";
            }
        }

        public void exportFileMakerFA18C()
        {
            //need to add a 
            //deletes the last 4 lines to slip into the .lua in the right spot
            // original var lines = System.IO.File.ReadAllLines(@"C:\Users\Bailey\Saved Games\DCS.openbeta\MissionEditor\UnitPayloads\FA-18C.lua");

            //folderName = "ccca";
            
            var userIni = new IniFile();
            
            if (!userIni.KeyExists("SavedGamesLocation"))
            {
                MessageBox.Show("Please select your Saved Games DCS folder.");
                return;
            }
            userSavedGameLocation = userIni.Read("SavedGamesLocation");
            
            userFA18CLuaLocation = userSavedGameLocation + "\\MissionEditor\\UnitPayloads\\FA-18C.lua";
            string catchPhrase = userNamedExport;//text to be searched for
            string contents = File.ReadAllText(userFA18CLuaLocation);//file to be searched        
            bool bool_repeatingMatch = contents.Contains("\""+catchPhrase+"\"", StringComparison.OrdinalIgnoreCase);//contains is the container with the boolin result
            if (bool_repeatingMatch == true)
            {
                MessageBox.Show("Loadout name conflict. Please choose another");
                return;
            }
      
         



            var lines = System.IO.File.ReadAllLines(userFA18CLuaLocation);//oops, it does this twice
            // original System.IO.File.WriteAllLines(@"C:\Users\Bailey\Saved Games\DCS.openbeta\MissionEditor\UnitPayloads\FA-18C.lua", lines.Take(lines.Length - 4).ToArray());
            System.IO.File.WriteAllLines(userFA18CLuaLocation, lines.Take(lines.Length - 4).ToArray());
            //prep the squence number https://stackoverflow.com/questions/16032451/get-datetime-now-with-milliseconds-precision
            string timestamp = DateTime.UtcNow.ToString("yyMMddHHmmss", CultureInfo.InvariantCulture);//this allows unique sequence numbers abased on the date and time
 
            //now write the file
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(userFA18CLuaLocation, true))
            {
                //file.WriteLine("		[23] = {");//this is suposed the be the sequential number of the loadout. i dont think it actually matters. It matters
                file.WriteLine("		[" + timestamp + "] = {");
                file.WriteLine("            [\"name\"] = \"" + userNamedExport + "\",");//the name of the module
                file.WriteLine("			[\"pylons\"] = {");
                file.WriteLine("                [1] = {");//the [1] indicates this as the first entry
                file.WriteLine("					[\"CLSID\"] = \"" + station1StoreExport + "\",");//this contains the weaponID. if left blank it correctly blanks out the weapn in DCS
                file.WriteLine("					[\"num\"] = 1,");//i think the 'num' is the station. lets match this up with the entry#. 
                file.WriteLine("				},");
                file.WriteLine("                [2] = {");
                file.WriteLine("					[\"CLSID\"] = \"" + station2StoreExport + "\",");//if the user wants the station to be empty, make station2StoreExport = emptyString
                file.WriteLine("					[\"num\"] = 2,");
                file.WriteLine("				},");
                file.WriteLine("                [3] = {");
                file.WriteLine("					[\"CLSID\"] = \"" + station3StoreExport + "\",");
                file.WriteLine("					[\"num\"] = 3,");
                file.WriteLine("				},");
                file.WriteLine("                [4] = {");
                file.WriteLine("					[\"CLSID\"] = \"" + station4StoreExport + "\",");
                file.WriteLine("					[\"num\"] = 4,");
                file.WriteLine("				},");
                file.WriteLine("                [5] = {");
                file.WriteLine("					[\"CLSID\"] = \"" + station5StoreExport + "\",");
                file.WriteLine("					[\"num\"] = 5,");
                file.WriteLine("				},");
                file.WriteLine("                [6] = {");
                file.WriteLine("					[\"CLSID\"] = \"" + station6StoreExport + "\",");
                file.WriteLine("					[\"num\"] = 6,");
                file.WriteLine("				},");
                file.WriteLine("                [7] = {");
                file.WriteLine("					[\"CLSID\"] = \"" + station7StoreExport + "\",");
                file.WriteLine("					[\"num\"] = 7,");
                file.WriteLine("				},");
                file.WriteLine("                [8] = {");
                file.WriteLine("					[\"CLSID\"] = \"" + station8StoreExport + "\",");
                file.WriteLine("					[\"num\"] = 8,");
                file.WriteLine("				},");
                file.WriteLine("                [9] = {");
                file.WriteLine("					[\"CLSID\"] = \"" + station9StoreExport + "\",");
                file.WriteLine("					[\"num\"] = 9,");
                file.WriteLine("				},");
            }
            //and replace the last few lines that were delete earlier (actually just makes the new ones)
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(userFA18CLuaLocation, true))
            {
                file.WriteLine("			},");
                file.WriteLine("			[\"tasks\"] = {");
                file.WriteLine("				[1] = 30,");//i need a user set option and variable for the task numper, or not
                file.WriteLine("			},");
                file.WriteLine("		},");
                file.WriteLine("	},");
                file.WriteLine("    [\"unitType\"] = \"FA-18C_hornet\","); //uhhh, i have to figgure out how to include the " in this line
                //file.WriteLine("    unitType placeholder line A-10C");
                file.WriteLine("}");
                file.WriteLine("return unitPayloads");
            }
            MessageBox.Show("Loadout \"" + userNamedExport+ "\" exported to " + userFA18CLuaLocation + "\nExport complete!!!");
            //MessageBox.Show(userFA18CLuaLocation);
        }
        
        private void Label_loadoutName_Click(object sender, EventArgs e)
        {

        }

        private void Button_clearLoadout_Click(object sender, EventArgs e)
        {
            if (selectAirctaftListBox.SelectedItem == "F/A-18")
            {
                int index_clearLoadout = selectAirctaftListBox.FindString("F/A-18");
                selectAirctaftListBox.SetSelected(index_clearLoadout, true);
                //MessageBox.Show("clear button ready");

            }
        }

        private void Button_setDcsLocation_Click(object sender, EventArgs e)
        {
            //https://stackoverflow.com/questions/7330111/select-folder-path-with-savefiledialog
            var folderBrowserDialog1 = new FolderBrowserDialog();

            // Show the FolderBrowserDialog.
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                
                userSavedGameLocation = folderBrowserDialog1.SelectedPath;
                //Do your work here!              
                //MessageBox.Show("You have selected "+ userSavedGameLocation + " as your DCS Saved Games folder. \n Ensure that it looks similar to C:\\Users\\YOURNAME\\Saved Games\\DCS.openbeta");
                var userIni = new IniFile();
                userIni.Write("SavedGamesLocation", userSavedGameLocation);
               
                userSavedGameLocation = userIni.Read("SavedGamesLocation");
                MessageBox.Show("You have selected "+userSavedGameLocation+"\nMake sure that your selected file ends in one of the below:\nSaved Games\\DCS\nSaved Games\\DCS.openbeta\nSaved Games\\DCS.openalpha\n(Unless you really, really know what you are doing.)");
               
            }
        }

        private void TextBox_loadoutName_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void TotalTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void MaxTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void FuelWeightTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void TotalTrackBar_Scroll(object sender, EventArgs e)
        {

        }

        private void DeltaTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label_TotalPercent_Click(object sender, EventArgs e)
        {

        }

        private void Label_GunPercent_Click(object sender, EventArgs e)
        {

        }

        private void Label_InternalFuelPercent_Click(object sender, EventArgs e)
        {

        }

        private void DeltaLabel_Click(object sender, EventArgs e)
        {

        }

        private void GunLabel_Click(object sender, EventArgs e)
        {

        }

        private void MaxLabel_Click(object sender, EventArgs e)
        {

        }

        private void WeaponsLabel_Click(object sender, EventArgs e)
        {

        }

        private void FuelWeightLabel_Click(object sender, EventArgs e)
        {

        }
    }
    class IniFile   // revision 11
    {
        string Path;
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public IniFile(string IniPath = null)
        {
            Path = new FileInfo(IniPath ?? EXE + ".ini").FullName.ToString();
        }

        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }

        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }

        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }

    } //this is the backbone code for the ini reading and writing
    public static class StringExtensions//this method is required for the duplicate name checker to work
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }
    }
}
/*
 completed pop up station 1 and 2 and 3.
 do 4,5,6,7,8, and 9. 
 */
