namespace QuickWaifu2x
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.notify = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.실행OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.파일로실행ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.설정SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.종료XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // notify
            // 
            this.notify.ContextMenuStrip = this.contextMenu;
            this.notify.Icon = ((System.Drawing.Icon)(resources.GetObject("icon")));
            this.notify.Text = "QuickWaifu2x";
            this.notify.Visible = true;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.실행OToolStripMenuItem,
            this.파일로실행ToolStripMenuItem,
            this.toolStripSeparator2,
            this.설정SToolStripMenuItem,
            this.toolStripSeparator1,
            this.종료XToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(205, 104);
            this.contextMenu.Text = "QuickWaifu2x";
            // 
            // 실행OToolStripMenuItem
            // 
            this.실행OToolStripMenuItem.Name = "실행OToolStripMenuItem";
            this.실행OToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.실행OToolStripMenuItem.Text = "엽스케일링 (&O)";
            this.실행OToolStripMenuItem.Click += new System.EventHandler(this.OnUpscaling);
            // 
            // 파일로실행ToolStripMenuItem
            // 
            this.파일로실행ToolStripMenuItem.Name = "파일로실행ToolStripMenuItem";
            this.파일로실행ToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.파일로실행ToolStripMenuItem.Text = "파일에서 엽스케일링 (&F)";
            this.파일로실행ToolStripMenuItem.Click += new System.EventHandler(this.OnFileUpscaling);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(201, 6);
            // 
            // 설정SToolStripMenuItem
            // 
            this.설정SToolStripMenuItem.Name = "설정SToolStripMenuItem";
            this.설정SToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.설정SToolStripMenuItem.Text = "설정 (&S)";
            this.설정SToolStripMenuItem.Click += new System.EventHandler(this.OnSettings);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(201, 6);
            // 
            // 종료XToolStripMenuItem
            // 
            this.종료XToolStripMenuItem.Name = "종료XToolStripMenuItem";
            this.종료XToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.종료XToolStripMenuItem.Text = "종료 (&X)";
            this.종료XToolStripMenuItem.Click += new System.EventHandler(this.OnExit);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(0, 0);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.Opacity = 0D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = " ";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notify;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem 실행OToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 파일로실행ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 설정SToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 종료XToolStripMenuItem;
    }
}

