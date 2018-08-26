namespace SIT323Crozzle
{
    partial class Crozzle
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.CrozzleMenu = new System.Windows.Forms.MenuStrip();
            this.File = new System.Windows.Forms.ToolStripMenuItem();
            this.loadCrozzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadCrozzletxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadCrozzleczlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createCrozzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCrozzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Help = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.CrozzleTab = new System.Windows.Forms.TabPage();
            this.CrozzleBrowser = new System.Windows.Forms.WebBrowser();
            this.ErrorTab = new System.Windows.Forms.TabPage();
            this.ErrorBrowser = new System.Windows.Forms.WebBrowser();
            this.CrozzleMenu.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.CrozzleTab.SuspendLayout();
            this.ErrorTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // CrozzleMenu
            // 
            this.CrozzleMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File,
            this.Help});
            this.CrozzleMenu.Location = new System.Drawing.Point(0, 0);
            this.CrozzleMenu.Name = "CrozzleMenu";
            this.CrozzleMenu.Size = new System.Drawing.Size(660, 24);
            this.CrozzleMenu.TabIndex = 0;
            this.CrozzleMenu.Text = "Menu";
            // 
            // File
            // 
            this.File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadCrozzleToolStripMenuItem,
            this.createCrozzleToolStripMenuItem,
            this.saveCrozzleToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.File.Name = "File";
            this.File.Size = new System.Drawing.Size(37, 20);
            this.File.Text = "File";
            // 
            // loadCrozzleToolStripMenuItem
            // 
            this.loadCrozzleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadCrozzletxtToolStripMenuItem,
            this.loadCrozzleczlToolStripMenuItem});
            this.loadCrozzleToolStripMenuItem.Name = "loadCrozzleToolStripMenuItem";
            this.loadCrozzleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadCrozzleToolStripMenuItem.Text = "Load Crozzle";
            this.loadCrozzleToolStripMenuItem.Click += new System.EventHandler(this.LoadCrozzleToolStripMenuItem_Click_1);
            // 
            // loadCrozzletxtToolStripMenuItem
            // 
            this.loadCrozzletxtToolStripMenuItem.Name = "loadCrozzletxtToolStripMenuItem";
            this.loadCrozzletxtToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.loadCrozzletxtToolStripMenuItem.Text = "Load Crozzle.txt";
            this.loadCrozzletxtToolStripMenuItem.Click += new System.EventHandler(this.loadCrozzletxtToolStripMenuItem_Click);
            // 
            // loadCrozzleczlToolStripMenuItem
            // 
            this.loadCrozzleczlToolStripMenuItem.Name = "loadCrozzleczlToolStripMenuItem";
            this.loadCrozzleczlToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.loadCrozzleczlToolStripMenuItem.Text = "Load Crozzle.czl";
            this.loadCrozzleczlToolStripMenuItem.Click += new System.EventHandler(this.loadCrozzleczlToolStripMenuItem_Click);
            // 
            // createCrozzleToolStripMenuItem
            // 
            this.createCrozzleToolStripMenuItem.Name = "createCrozzleToolStripMenuItem";
            this.createCrozzleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.createCrozzleToolStripMenuItem.Text = "Create Crozzle";
            this.createCrozzleToolStripMenuItem.Click += new System.EventHandler(this.createCrozzleToolStripMenuItem_Click);
            // 
            // saveCrozzleToolStripMenuItem
            // 
            this.saveCrozzleToolStripMenuItem.Name = "saveCrozzleToolStripMenuItem";
            this.saveCrozzleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveCrozzleToolStripMenuItem.Text = "Save Crozzle";
            this.saveCrozzleToolStripMenuItem.Click += new System.EventHandler(this.saveCrozzleToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Help
            // 
            this.Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.Help.Name = "Help";
            this.Help.Size = new System.Drawing.Size(44, 20);
            this.Help.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.CrozzleTab);
            this.TabControl.Controls.Add(this.ErrorTab);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 24);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(660, 475);
            this.TabControl.TabIndex = 1;
            // 
            // CrozzleTab
            // 
            this.CrozzleTab.Controls.Add(this.CrozzleBrowser);
            this.CrozzleTab.Location = new System.Drawing.Point(4, 22);
            this.CrozzleTab.Name = "CrozzleTab";
            this.CrozzleTab.Padding = new System.Windows.Forms.Padding(3);
            this.CrozzleTab.Size = new System.Drawing.Size(652, 449);
            this.CrozzleTab.TabIndex = 0;
            this.CrozzleTab.Text = "Crozzle";
            this.CrozzleTab.UseVisualStyleBackColor = true;
            // 
            // CrozzleBrowser
            // 
            this.CrozzleBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CrozzleBrowser.Location = new System.Drawing.Point(3, 3);
            this.CrozzleBrowser.MinimumSize = new System.Drawing.Size(20, 22);
            this.CrozzleBrowser.Name = "CrozzleBrowser";
            this.CrozzleBrowser.Size = new System.Drawing.Size(646, 443);
            this.CrozzleBrowser.TabIndex = 0;
            // 
            // ErrorTab
            // 
            this.ErrorTab.Controls.Add(this.ErrorBrowser);
            this.ErrorTab.Location = new System.Drawing.Point(4, 22);
            this.ErrorTab.Name = "ErrorTab";
            this.ErrorTab.Padding = new System.Windows.Forms.Padding(3);
            this.ErrorTab.Size = new System.Drawing.Size(652, 449);
            this.ErrorTab.TabIndex = 1;
            this.ErrorTab.Text = "Error List";
            this.ErrorTab.UseVisualStyleBackColor = true;
            // 
            // ErrorBrowser
            // 
            this.ErrorBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ErrorBrowser.Location = new System.Drawing.Point(3, 3);
            this.ErrorBrowser.MinimumSize = new System.Drawing.Size(20, 22);
            this.ErrorBrowser.Name = "ErrorBrowser";
            this.ErrorBrowser.Size = new System.Drawing.Size(646, 443);
            this.ErrorBrowser.TabIndex = 0;
            // 
            // Crozzle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 499);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.CrozzleMenu);
            this.MainMenuStrip = this.CrozzleMenu;
            this.Name = "Crozzle";
            this.Text = "SIT323 Crozzle";
            this.CrozzleMenu.ResumeLayout(false);
            this.CrozzleMenu.PerformLayout();
            this.TabControl.ResumeLayout(false);
            this.CrozzleTab.ResumeLayout(false);
            this.ErrorTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip CrozzleMenu;
        private System.Windows.Forms.ToolStripMenuItem File;
        private System.Windows.Forms.ToolStripMenuItem loadCrozzleToolStripMenuItem;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage CrozzleTab;
        private System.Windows.Forms.TabPage ErrorTab;
        private System.Windows.Forms.WebBrowser CrozzleBrowser;
        private System.Windows.Forms.WebBrowser ErrorBrowser;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Help;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createCrozzleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCrozzleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadCrozzletxtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadCrozzleczlToolStripMenuItem;
    }
}

