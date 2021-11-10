using System;
using System.Threading;
using System.Windows.Forms;

namespace QuickWaifu2x {
    static class Program {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var m = new Mutex(true, $"QUICKWAIFU2X_A__V_S_T", out var alreadyRun);
            if (!alreadyRun) {
                MessageBox.Show("���ø����̼��� �̹� �������Դϴ�.\n Win + Shift + E �Ǵ� Win + Shift + R�� �Է��ϼ���");
                return;
            }
            else m.ReleaseMutex();

            Application.Run(new Main());
        }
    }
}
