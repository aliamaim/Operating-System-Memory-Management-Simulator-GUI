namespace WindowsFormsApplication1
{
    partial class start
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
            this.button1 = new System.Windows.Forms.Button();
            this.method_box = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.total_memory = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.no_of_holes = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.features = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(452, 332);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 58);
            this.button1.TabIndex = 0;
            this.button1.Text = "Next";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // method_box
            // 
            this.method_box.FormattingEnabled = true;
            this.method_box.Items.AddRange(new object[] {
            "First Fit",
            "Best Fit",
            "Worst Fit"});
            this.method_box.Location = new System.Drawing.Point(251, 96);
            this.method_box.Name = "method_box";
            this.method_box.Size = new System.Drawing.Size(121, 21);
            this.method_box.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Method";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(193, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Total Memory Size";
            // 
            // total_memory
            // 
            this.total_memory.Location = new System.Drawing.Point(251, 172);
            this.total_memory.Name = "total_memory";
            this.total_memory.Size = new System.Drawing.Size(100, 20);
            this.total_memory.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 239);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 25);
            this.label4.TabIndex = 7;
            this.label4.Text = "No. of holes";
            // 
            // no_of_holes
            // 
            this.no_of_holes.Location = new System.Drawing.Point(251, 244);
            this.no_of_holes.Name = "no_of_holes";
            this.no_of_holes.Size = new System.Drawing.Size(100, 20);
            this.no_of_holes.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(119, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(392, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "Memory Management Simulator V1";
            // 
            // features
            // 
            this.features.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.features.Location = new System.Drawing.Point(571, 12);
            this.features.Name = "features";
            this.features.Size = new System.Drawing.Size(85, 42);
            this.features.TabIndex = 10;
            this.features.Text = "Features";
            this.features.UseVisualStyleBackColor = true;
            this.features.Click += new System.EventHandler(this.features_Click);
            // 
            // start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 428);
            this.Controls.Add(this.features);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.no_of_holes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.total_memory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.method_box);
            this.Controls.Add(this.button1);
            this.Name = "start";
            this.Text = "start";
            this.Load += new System.EventHandler(this.start_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox method_box;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox total_memory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox no_of_holes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button features;
    }
}

