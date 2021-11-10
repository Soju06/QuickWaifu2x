using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickWaifu2x {
    internal partial class ProgressDialog : Form {
        public ProgressDialog((string, string)[] files, Waifu2xNcnnVulkanParameter parameter) {
            InitializeComponent();
            notifyIcon1.Icon = Main.Icon;
            Parameter = parameter;
            Files = files;
            TotalCurrentProgress = CurrentProgress = 0;
            closed = false;
        }

        (string, string)[] Files;
        List<(string, string)> OutFiles = new();
        Waifu2xNcnnVulkanParameter Parameter;
        Waifu2xNcnnVulkanProcess Process;
        Thread Thread;
        double progress;
        int currentCount, failedCount;
        bool closed;

        void SetState(object? _ = null) => notifyIcon1.Text = "QuickWaifu2x " + (label1.Text = $"{progress:F0}% {currentCount}/{Files.Length}");

        int CurrentCount {
            set {
                currentCount = value;
                SetState();
            }
        }

        double TotalCurrentProgress {
            set => SetState(progressBar1.Value = (int)(progress = value * 100D));
        }

        double CurrentProgress {
            set => progressBar2.Value = (int)(value * 100D);
        }

        bool CurrentProgressStyle { set => progressBar2.Style = value ? ProgressBarStyle.Marquee : ProgressBarStyle.Blocks; }

        string CurrentProgressText { set => label2.Text = value; }

        void OnStart() {
            try {
                Invoke(() => {
                    TotalCurrentProgress = CurrentProgress = 0;
                    CurrentCount = 0;
                    CurrentProgressStyle = true;
                    button1.Enabled = false;
                });
                failedCount = 0;
                OutFiles.Clear();
                for (int i = 0; i < Files.Length; i++)
                    OnStartWork(Files[i]);
                Invoke(() => {
                    if (OutFiles.Count > 1) {
                        var ss = new StringCollection();
                        foreach (var item in OutFiles)
                            ss.Add(item.Item2);
                        Clipboard.SetFileDropList(ss);
                    }

                    button1.Enabled = OutFiles.Count > 0;
                    button2.Text = "닫기 (&C)";
                    CurrentProgressStyle = false;
                    TotalCurrentProgress = CurrentProgress = 1;

                    notifyIcon1.ShowBalloonTip(2000, $"인코딩이 완료되었습니다.", $"총 {Files.Length - failedCount}개를 인코딩했습니다.", ToolTipIcon.Info);
                });
            } catch (Exception ex) {
                ShowException(ex);
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        void OnStartWork((string, string) e) {
            try {
                (var fullName, var source) = e;
                Task.Factory.StartNew(() => {
                    pictureBox1.Image?.Dispose();
                    if (new FileInfo(source).Length <= (15 * 1024) * 1024)
                        using (var stream = File.OpenRead(fullName))
                            pictureBox1.Image = Image.FromStream(stream);
                });

                Invoke(() => CurrentProgressText = Path.GetFileName(fullName));
                var p = (Waifu2xNcnnVulkanParameter)Parameter.Clone();
                p.Source = source;
                p.Target = Path.GetTempFileName() + $".{p.Format.ToString().ToLower()}";
                using var pr = Process = Waifu2xNcnnVulkan.Create(p);
                pr.OnReceived += OnReceived;
                pr.Start();
                pr.Join();
                Image? i = null;
                if (File.Exists(p.Source)) File.Delete(p.Source);
                if (!File.Exists(p.Target) || !Try.EndRun(() => Image.FromFile(p.Target).Declaration(out i) != null, _ => { File.Delete(p.Target); return false; }))
                    OnFailed(source, pr.Result);
                else {
                    OutFiles.Add(new(fullName, p.Target));

                    if (Files.Length == 1) Invoke(() => Clipboard.SetImage(i));
                }
                i?.Dispose();
                Invoke(() => {
                    currentCount++;
                    TotalCurrentProgress = (double)currentCount / Files.Length;
                });
            } catch (Exception ex) {
                ShowException(ex);
            }
        }

        void OnFailed(string s, string m) {
            failedCount++;
            Invoke(() => notifyIcon1.ShowBalloonTip(2000, $"인코딩 실패 {Path.GetFileName(s)}", $"{Path.GetFileName(s)} 을(를) 인코딩하지 못했습니다.\n{m}", ToolTipIcon.Error));
        }

        void ShowException(Exception ex) {
            if (!closed) MessageBox.Show($"처리되지 않은 예외가 발생했습니다.\n{ex}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void Log(string s) {
            textBox1.SuspendLayout();
            textBox1.AppendText(Environment.NewLine + s);
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
            textBox1.ResumeLayout();
        }

        private void OnBalloonTipClicked(object sender, EventArgs e) =>
            Show();

        private void OnNotifyClick(object sender, EventArgs e) =>
            Show();

        private void OnExport(object sender, EventArgs e) {
            if (OutFiles.Count > 1) {
                using var dialog = new CommonOpenFileDialog();
                dialog.IsFolderPicker = true;
                dialog.InitialDirectory = Path.GetDirectoryName(OutFiles[0].Item1);
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok) {
                    var dir = dialog.FileName;
                    foreach (var item in OutFiles) {
                        var fn = dir.JoinDir(Path.GetFileNameWithoutExtension(item.Item1));
                        var ext = Path.GetExtension(item.Item2);
                        string n;
                        while (File.Exists(n = Path.ChangeExtension($"{fn} waifu2x", ext))) { fn += Ran.NextBytes(2).ToHex(); }
                        File.Copy(item.Item2, n, true);
                    }
                }
            } else if (OutFiles.Count == 1) {
                var f = OutFiles[0];
                var ext = Path.GetExtension(f.Item2);
                saveFileDialog1.Filter = $"{ext} Files|*{ext}";
                saveFileDialog1.InitialDirectory = Path.GetDirectoryName(f.Item1);
                saveFileDialog1.FileName = Path.GetFileNameWithoutExtension(f.Item1);
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    File.Copy(f.Item2, saveFileDialog1.FileName, true);
            }
        }

        private void OnShown(object sender, EventArgs e) =>
            (Thread = new Thread(OnStart) { IsBackground = true }).Start();

        private void OnCancel(object sender, EventArgs e) => Close();

        private void OnClosing(object sender, FormClosingEventArgs e) {
            if (e.CloseReason != CloseReason.UserClosing || currentCount >= Files.Length) goto Close;

            if (MessageBox.Show("정말로 작업을 취소하시겠습니까?", "경고", MessageBoxButtons.YesNo, 
                MessageBoxIcon.Information) != DialogResult.Yes) {
                e.Cancel = true;
                return;
            }

            Close:
            closed = true;
            Thread?.Interrupt();
            Process?.Dispose();
            pictureBox1?.Image?.Dispose();
        }

        private void OnClosed(object sender, FormClosedEventArgs e) =>
            (sender as IDisposable)?.Dispose();

        private void OnReceived(object? sender, string? e) {
            if (e == null || e.IsNullOrWhiteSpace()) return;
            Invoke(() => Log(e));
        }

        void Invoke(Action action) => 
            base.Invoke(action);
    }
}
