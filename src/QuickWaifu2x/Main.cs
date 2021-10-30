using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace QuickWaifu2x {
    internal partial class Main : Form {
        readonly Dictionary<int, Action> HotKeys = new();

        public static Icon Icon { get; private set; }

        public Main() {
            InitializeComponent();
            Icon = notify.Icon;
            RegisterHotKey(KeyModifiers.Windows | KeyModifiers.Shift, Keys.E, OnWaifu2x);
            RegisterHotKey(KeyModifiers.Windows | KeyModifiers.Shift, Keys.R, OnSettings);
            Visible = false;
        }

        SettingsDialog Settings;

        void OnSettings() => ((Settings ??= new()).IsDisposed ? Settings = new() : Settings).Show();

        void OnWaifu2x() => OnWaifu2x("");

        void OnWaifu2x(string fileName) {
            var e = Clipboard.GetDataObject();
            (string, string)[]? files = null;
            if (fileName.IsNullOrWhiteSpace()) {
                if (e.GetDataPresent(DataFormats.FileDrop))
                    files = GetConvertibleFiles((string[])e.GetData(DataFormats.FileDrop));
                else if (e.GetDataPresent(DataFormats.Bitmap)) {
                    var f = GetTempImagePath((Bitmap)e.GetData(DataFormats.Bitmap));
                    files = new (string, string)[] { new(f, f) };
                }
            }
            else if (fileName.Contains(';'))
                files = GetConvertibleFiles(fileName.Split(';'));
            else files = GetConvertibleFiles(new[] { fileName });
            if (files == null || files.Length < 1) return;
            if(files?.Length > 1) {
                (var o, var d) = ImageMultipleMessageBox.ShowDialog(files);
                if (!(o == DialogResult.OK)) return;
                files = d;
            }
            var parms = Waifu2xParameterDialog.ShowDialog();
            if (parms == null) return;
            if (files == null || files.Length < 1) return;

            new ProgressDialog(files, parms).Show();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        public (string, string)[] GetConvertibleFiles(string[] pa) {
            var result = new List<(string, string)>();
            foreach (string file in pa) {
                if (!File.Exists(file)) continue;
                try {
                    using var img = Image.FromFile(file);
                    if (!Waifu2xNcnnVulkan.AvailableTypes.Contains(img.RawFormat)) continue;
                    result.Add(new (file, GetTempImagePath(img)));
                    img.Dispose();
                } catch {

                }
            } return result.ToArray();
        }

        public string GetTempImagePath(Image bitmap) =>
            (Path.GetTempFileName() + ".png").TryRun(f => { bitmap.Save(f, ImageFormat.Png); return f; });

        protected override void WndProc(ref Message m) {
            base.WndProc(ref m);
            if (m.Msg == 0x312 && HotKeys.TryGetValue((int)m.WParam, out var a)) 
                new Thread(() => Invoke(() => a?.Invoke())).Start();
        }

        int hotKeyId = 0;

        void RegisterHotKey(KeyModifiers kid, Keys key, Action action) {
            HotKeys.Add(hotKeyId, action);
            _ = RegisterHotKey((int)Handle, hotKeyId++, (int)kid, (int)key);
        }

        void UnregisterHotKeys() => HotKeys.All(e =>
            _ = UnregisterHotKey((int)Handle, e.Key));


        [DllImport("user32.dll")]
        static extern int RegisterHotKey(int hwnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        static extern int UnregisterHotKey(int hwnd, int id);

        void OnClosing(object sender, FormClosingEventArgs e) =>
            UnregisterHotKeys();

        void OnSettings(object sender, EventArgs e) => OnSettings();

        void OnExit(object sender, EventArgs e) => Close();

        void OnUpscaling(object sender, EventArgs e) => OnWaifu2x();

        void OnFileUpscaling(object sender, EventArgs e) {
            using var fd = new OpenFileDialog();
            fd.Filter = "All file|*.png;*.jpg|Png file|*.png|Jpg file|*.jpg";
            fd.Title = "이미지 선택";
            fd.Multiselect = true;
            if (fd.ShowDialog() == DialogResult.OK) {
                var files = fd.FileNames;
                if (files == null || files.Length < 1) return;
                for (int i = 0; i < files.Length; i++)
                    if (!File.Exists(files[i])) return;
                OnWaifu2x(files.Join(";"));
            }
        }

        void Invoke(Action action) =>
            base.Invoke(action);

        private void OnNotify(object sender, MouseEventArgs e) =>
            파일로실행ToolStripMenuItem.PerformClick();

        private void OnShown(object sender, EventArgs e) {
            Hide();
            if (TimeSpan.FromMilliseconds(Environment.TickCount64) >= new TimeSpan(0, 3, 0))
                notify.ShowBalloonTip(2000, "QuickWaifu2x가 시스템 트레이에서 실행중입니다.", "QuickWaifu2x", ToolTipIcon.Info);
        }
    }

    [Flags]
    public enum KeyModifiers {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
        Windows = 8
    }
}
