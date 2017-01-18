namespace TestApp
{
    partial class DebugForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.btStart = new System.Windows.Forms.Button();
            this.pbLeftEye = new System.Windows.Forms.PictureBox();
            this.pbRightEye = new System.Windows.Forms.PictureBox();
            this.cbLeftEyeClosed = new System.Windows.Forms.CheckBox();
            this.cbRightEyeClosed = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbBrowLowerer = new System.Windows.Forms.TextBox();
            this.tbBrowRaiser = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbLeftEye)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRightEye)).BeginInit();
            this.SuspendLayout();
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(12, 145);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(75, 23);
            this.btStart.TabIndex = 1;
            this.btStart.Text = "Start";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // pbLeftEye
            // 
            this.pbLeftEye.Location = new System.Drawing.Point(117, 12);
            this.pbLeftEye.Name = "pbLeftEye";
            this.pbLeftEye.Size = new System.Drawing.Size(83, 64);
            this.pbLeftEye.TabIndex = 2;
            this.pbLeftEye.TabStop = false;
            // 
            // pbRightEye
            // 
            this.pbRightEye.Location = new System.Drawing.Point(12, 12);
            this.pbRightEye.Name = "pbRightEye";
            this.pbRightEye.Size = new System.Drawing.Size(75, 64);
            this.pbRightEye.TabIndex = 3;
            this.pbRightEye.TabStop = false;
            // 
            // cbLeftEyeClosed
            // 
            this.cbLeftEyeClosed.AutoSize = true;
            this.cbLeftEyeClosed.Location = new System.Drawing.Point(12, 82);
            this.cbLeftEyeClosed.Name = "cbLeftEyeClosed";
            this.cbLeftEyeClosed.Size = new System.Drawing.Size(99, 17);
            this.cbLeftEyeClosed.TabIndex = 14;
            this.cbLeftEyeClosed.Text = "Left Eye closed";
            this.cbLeftEyeClosed.UseVisualStyleBackColor = true;
            // 
            // cbRightEyeClosed
            // 
            this.cbRightEyeClosed.AutoSize = true;
            this.cbRightEyeClosed.Location = new System.Drawing.Point(117, 82);
            this.cbRightEyeClosed.Name = "cbRightEyeClosed";
            this.cbRightEyeClosed.Size = new System.Drawing.Size(106, 17);
            this.cbRightEyeClosed.TabIndex = 15;
            this.cbRightEyeClosed.Text = "Right Eye closed";
            this.cbRightEyeClosed.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Brow Lowerer Value:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(117, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Brow Raiser Value:";
            // 
            // tbBrowLowerer
            // 
            this.tbBrowLowerer.Location = new System.Drawing.Point(12, 119);
            this.tbBrowLowerer.Name = "tbBrowLowerer";
            this.tbBrowLowerer.Size = new System.Drawing.Size(100, 20);
            this.tbBrowLowerer.TabIndex = 18;
            // 
            // tbBrowRaiser
            // 
            this.tbBrowRaiser.Location = new System.Drawing.Point(118, 118);
            this.tbBrowRaiser.Name = "tbBrowRaiser";
            this.tbBrowRaiser.Size = new System.Drawing.Size(100, 20);
            this.tbBrowRaiser.TabIndex = 19;
            // 
            // DebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 181);
            this.Controls.Add(this.tbBrowRaiser);
            this.Controls.Add(this.tbBrowLowerer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbRightEyeClosed);
            this.Controls.Add(this.cbLeftEyeClosed);
            this.Controls.Add(this.pbRightEye);
            this.Controls.Add(this.pbLeftEye);
            this.Controls.Add(this.btStart);
            this.Name = "DebugForm";
            this.Text = "Face Debug Output";
            ((System.ComponentModel.ISupportInitialize)(this.pbLeftEye)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRightEye)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.PictureBox pbLeftEye;
        private System.Windows.Forms.PictureBox pbRightEye;
        private System.Windows.Forms.CheckBox cbLeftEyeClosed;
        private System.Windows.Forms.CheckBox cbRightEyeClosed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbBrowLowerer;
        private System.Windows.Forms.TextBox tbBrowRaiser;
    }
}

