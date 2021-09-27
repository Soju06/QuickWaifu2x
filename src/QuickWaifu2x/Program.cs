using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickWaifu2x {
    static class Program {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            var m = new Mutex(true, $"QUICKWAIFU2X_A__V_S_T", out var alreadyRun);
            if (!alreadyRun) {
                MessageBox.Show("애플리케이션이 이미 실행중입니다.\n Win + Shift + E 또는 Win + Shift + R을 입력하세요");
                return;
            }
            else m.ReleaseMutex();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
