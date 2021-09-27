using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickWaifu2x {
    public partial class SettingsDialog : Form { 
        public SettingsDialog() {
            InitializeComponent();
        }

        static readonly RegistryKey? startProgramRegKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        void OnLoad(object sender, EventArgs e) {
            checkBox1.Checked = Ini.Current["immb"].ContainsKey("acmi");
            checkBox2.Checked = startProgramRegKey.GetValue(AppName) != null;
        }

        void ToggleMultipleFile(object sender, EventArgs e) {
            if (!checkBox1.Checked) Ini.Current["immb"].Remove("acmi");
        }

        void ToggleStartProgram(object sender, EventArgs e) {
            if (checkBox2.Checked) startProgramRegKey.SetValue(AppName, Environment.CurrentDirectory + "\\" + AppDomain.CurrentDomain.FriendlyName);
            else startProgramRegKey.DeleteValue(AppName, false);
        }

        const string AppName = "QuickWaifu2x";

        void OnDefaultParameter(object sender, EventArgs e) {
            var parms = Waifu2xParameterDialog.ShowDialog();
            if (parms == null) return;
            AppConfig.SetParameter(parms);
            AppConfig.Save();
        }

        void OnClose(object sender, EventArgs e) => Close();
    }
}
