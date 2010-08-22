namespace macro
{
    partial class MainForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Start = new System.Windows.Forms.Button();
            this.Slot1 = new System.Windows.Forms.CheckBox();
            this.Slot2 = new System.Windows.Forms.CheckBox();
            this.Slot3 = new System.Windows.Forms.CheckBox();
            this.Slot4 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TotalSlots = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TotalSlots)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Slot4);
            this.groupBox1.Controls.Add(this.Slot3);
            this.groupBox1.Controls.Add(this.Slot2);
            this.groupBox1.Controls.Add(this.Slot1);
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(68, 85);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tanks";
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(79, 12);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(75, 23);
            this.Start.TabIndex = 2;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // Slot1
            // 
            this.Slot1.AutoSize = true;
            this.Slot1.Checked = true;
            this.Slot1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Slot1.Location = new System.Drawing.Point(8, 14);
            this.Slot1.Name = "Slot1";
            this.Slot1.Size = new System.Drawing.Size(53, 17);
            this.Slot1.TabIndex = 3;
            this.Slot1.Text = "Slot 1";
            this.Slot1.UseVisualStyleBackColor = true;
            // 
            // Slot2
            // 
            this.Slot2.AutoSize = true;
            this.Slot2.Checked = true;
            this.Slot2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Slot2.Location = new System.Drawing.Point(8, 31);
            this.Slot2.Name = "Slot2";
            this.Slot2.Size = new System.Drawing.Size(53, 17);
            this.Slot2.TabIndex = 3;
            this.Slot2.Text = "Slot 2";
            this.Slot2.UseVisualStyleBackColor = true;
            // 
            // Slot3
            // 
            this.Slot3.AutoSize = true;
            this.Slot3.Checked = true;
            this.Slot3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Slot3.Location = new System.Drawing.Point(8, 48);
            this.Slot3.Name = "Slot3";
            this.Slot3.Size = new System.Drawing.Size(53, 17);
            this.Slot3.TabIndex = 3;
            this.Slot3.Text = "Slot 3";
            this.Slot3.UseVisualStyleBackColor = true;
            // 
            // Slot4
            // 
            this.Slot4.AutoSize = true;
            this.Slot4.Location = new System.Drawing.Point(8, 65);
            this.Slot4.Name = "Slot4";
            this.Slot4.Size = new System.Drawing.Size(53, 17);
            this.Slot4.TabIndex = 3;
            this.Slot4.Text = "Slot 4";
            this.Slot4.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Total Slots";
            // 
            // TotalSlots
            // 
            this.TotalSlots.Location = new System.Drawing.Point(139, 65);
            this.TotalSlots.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.TotalSlots.Name = "TotalSlots";
            this.TotalSlots.Size = new System.Drawing.Size(53, 20);
            this.TotalSlots.TabIndex = 4;
            this.TotalSlots.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 96);
            this.Controls.Add(this.TotalSlots);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "world of tanks macro";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TotalSlots)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.CheckBox Slot4;
        private System.Windows.Forms.CheckBox Slot3;
        private System.Windows.Forms.CheckBox Slot2;
        private System.Windows.Forms.CheckBox Slot1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown TotalSlots;
    }
}

