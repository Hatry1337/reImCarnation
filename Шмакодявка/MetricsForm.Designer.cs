namespace Shmak
{
    partial class MetricsForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MetricsForm));
            this.metBox = new System.Windows.Forms.RichTextBox();
            this.saveToFileBtn = new System.Windows.Forms.Button();
            this.copyMetrics = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // metBox
            // 
            this.metBox.Location = new System.Drawing.Point(12, 12);
            this.metBox.Name = "metBox";
            this.metBox.ReadOnly = true;
            this.metBox.Size = new System.Drawing.Size(360, 289);
            this.metBox.TabIndex = 0;
            this.metBox.Text = "";
            // 
            // saveToFileBtn
            // 
            this.saveToFileBtn.Location = new System.Drawing.Point(12, 318);
            this.saveToFileBtn.Name = "saveToFileBtn";
            this.saveToFileBtn.Size = new System.Drawing.Size(174, 31);
            this.saveToFileBtn.TabIndex = 1;
            this.saveToFileBtn.Text = "Сохранить в файл";
            this.saveToFileBtn.UseVisualStyleBackColor = true;
            this.saveToFileBtn.Click += new System.EventHandler(this.saveToFileBtn_Click);
            // 
            // copyMetrics
            // 
            this.copyMetrics.Enabled = false;
            this.copyMetrics.Location = new System.Drawing.Point(198, 318);
            this.copyMetrics.Name = "copyMetrics";
            this.copyMetrics.Size = new System.Drawing.Size(174, 31);
            this.copyMetrics.TabIndex = 2;
            this.copyMetrics.Text = "Скопировать в буфер обмена";
            this.copyMetrics.UseVisualStyleBackColor = true;
            this.copyMetrics.Click += new System.EventHandler(this.copyMetrics_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipTitle = "Шмакодявка";
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // MetricsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.copyMetrics);
            this.Controls.Add(this.saveToFileBtn);
            this.Controls.Add(this.metBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(400, 400);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "MetricsForm";
            this.Text = "Шмакодявка - Метрика";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox metBox;
        private System.Windows.Forms.Button saveToFileBtn;
        private System.Windows.Forms.Button copyMetrics;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}