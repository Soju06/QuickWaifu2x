using System.Windows.Forms;

namespace QuickWaifu2x {
    internal partial class ImageMultipleMessageBox : Form {
        public ImageMultipleMessageBox((string, string)[] ps) {
            InitializeComponent();
            label1.Text = label1.Text.Format(ps.Length);
        }

        public static (DialogResult, (string, string)[]?) ShowDialog((string, string)[] ps) {
            DialogResult result;
            (string, string)[]? args = null;
            if (!Ini.Current["immb"]["acmi"].TryConvertBool(out var b)) {
                using var dialog = new ImageMultipleMessageBox(ps);
                if ((result = dialog.ShowDialog()) == DialogResult.OK) {
                    b = dialog.Value;
                    if (dialog.SettingSave) {
                        Ini.Current["immb"]["acmi"] = dialog.Value;
                        Ini.Current.Save();
                    }
                }
            } else result = DialogResult.OK;
            if(result == DialogResult.OK) {
                if (b) args = ps;
                else args = new[] { ps[0] };
            } return new(result, args);
        }

        public bool SettingSave { get; private set; }

        public bool Value { get; private set; }

        void OnClosing(object sender, FormClosingEventArgs e) {
            SettingSave = checkBox1.Checked;
            Value = radioButton1.Checked;
        }
    }
}
