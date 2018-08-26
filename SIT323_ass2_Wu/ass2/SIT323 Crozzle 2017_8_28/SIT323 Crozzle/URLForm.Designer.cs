namespace SIT323Crozzle
{
    partial class URLForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.historyComboBox = new System.Windows.Forms.ComboBox();
            this.openURLButton = new System.Windows.Forms.Button();
            this.historyLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // historyComboBox
            // 
            this.historyComboBox.FormattingEnabled = true;
            this.historyComboBox.Items.AddRange(new object[] {
            "http://www.it.deakin.edu.au/SIT323/Task2/MarkingTest1.czl",
            "http://www.it.deakin.edu.au/SIT323/Task2/MarkingTest2.czl",
            "http://www.it.deakin.edu.au/SIT323/Task2/MarkingTest3.czl"});
            this.historyComboBox.Location = new System.Drawing.Point(107, 35);
            this.historyComboBox.Name = "historyComboBox";
            this.historyComboBox.Size = new System.Drawing.Size(380, 20);
            this.historyComboBox.TabIndex = 0;
            // 
            // openURLButton
            // 
            this.openURLButton.Location = new System.Drawing.Point(248, 71);
            this.openURLButton.Name = "openURLButton";
            this.openURLButton.Size = new System.Drawing.Size(75, 21);
            this.openURLButton.TabIndex = 2;
            this.openURLButton.Text = "Open URL";
            this.openURLButton.UseVisualStyleBackColor = true;
            this.openURLButton.Click += new System.EventHandler(this.openURLButton_Click);
            // 
            // historyLabel
            // 
            this.historyLabel.AutoSize = true;
            this.historyLabel.Location = new System.Drawing.Point(51, 38);
            this.historyLabel.Name = "historyLabel";
            this.historyLabel.Size = new System.Drawing.Size(23, 12);
            this.historyLabel.TabIndex = 4;
            this.historyLabel.Text = "URL";
            // 
            // URLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 115);
            this.Controls.Add(this.historyLabel);
            this.Controls.Add(this.openURLButton);
            this.Controls.Add(this.historyComboBox);
            this.Name = "URLForm";
            this.Text = "URLForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.URLForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox historyComboBox;
        private System.Windows.Forms.Button openURLButton;
        private System.Windows.Forms.Label historyLabel;
    }
}