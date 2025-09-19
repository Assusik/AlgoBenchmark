namespace AlgoBenchmark.Core.Forms
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
            Button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            SuspendLayout();
            // 
            // Button1
            // 
            Button1.Location = new Point(106, 163);
            Button1.Name = "Button1";
            Button1.Size = new Size(133, 94);
            Button1.TabIndex = 0;
            Button1.Text = "button1";
            Button1.UseVisualStyleBackColor = true;
            Button1.Click += Button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(506, 163);
            button2.Name = "button2";
            button2.Size = new Size(133, 94);
            button2.TabIndex = 1;
            button2.Text = "button1";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(915, 163);
            button3.Name = "button3";
            button3.Size = new Size(133, 94);
            button3.TabIndex = 2;
            button3.Text = "button1";
            button3.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1134, 561);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(Button1);
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button Button1;
        private Button button2;
        private Button button3;
    }
}