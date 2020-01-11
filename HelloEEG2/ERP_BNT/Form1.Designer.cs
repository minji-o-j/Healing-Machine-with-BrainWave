namespace ERP_BNT
{
    partial class Form1
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.EEG_btn = new System.Windows.Forms.Button();
            this.click = new System.Windows.Forms.Button();
            this.save = new System.Windows.Forms.Button();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.stop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(682, 127);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(221, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Stimuli";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(23, 27);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Horizontal;
            this.richTextBox1.Size = new System.Drawing.Size(617, 352);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // EEG_btn
            // 
            this.EEG_btn.Location = new System.Drawing.Point(682, 57);
            this.EEG_btn.Name = "EEG_btn";
            this.EEG_btn.Size = new System.Drawing.Size(221, 23);
            this.EEG_btn.TabIndex = 2;
            this.EEG_btn.Text = "EEG Connect";
            this.EEG_btn.UseVisualStyleBackColor = true;
            this.EEG_btn.Click += new System.EventHandler(this.EEG_btn_Click);
            // 
            // click
            // 
            this.click.Location = new System.Drawing.Point(682, 209);
            this.click.Name = "click";
            this.click.Size = new System.Drawing.Size(221, 168);
            this.click.TabIndex = 3;
            this.click.Text = "Click";
            this.click.UseVisualStyleBackColor = true;
            this.click.Click += new System.EventHandler(this.click_btn_Click);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(682, 165);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(221, 23);
            this.save.TabIndex = 4;
            this.save.Text = "File save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(682, 27);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(221, 25);
            this.nameBox.TabIndex = 5;
            this.nameBox.Text = "nameBox";
            // 
            // stop
            // 
            this.stop.Location = new System.Drawing.Point(682, 89);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(221, 23);
            this.stop.TabIndex = 6;
            this.stop.Text = "Stop";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 391);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.save);
            this.Controls.Add(this.click);
            this.Controls.Add(this.EEG_btn);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button EEG_btn;
        private System.Windows.Forms.Button click;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Button stop;
    }
}

