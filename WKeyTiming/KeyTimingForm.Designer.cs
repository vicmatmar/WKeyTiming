namespace WKeyTiming
{
    partial class KeyTimingForm
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
                _globalKeyboardHook?.Dispose();

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
            this.Status_textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Status_textBox
            // 
            this.Status_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Status_textBox.Location = new System.Drawing.Point(0, 0);
            this.Status_textBox.Multiline = true;
            this.Status_textBox.Name = "Status_textBox";
            this.Status_textBox.ReadOnly = true;
            this.Status_textBox.Size = new System.Drawing.Size(630, 228);
            this.Status_textBox.TabIndex = 0;
            this.Status_textBox.WordWrap = false;
            // 
            // KeyTimingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 228);
            this.Controls.Add(this.Status_textBox);
            this.Name = "KeyTimingForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.KeyTimingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Status_textBox;
    }
}

