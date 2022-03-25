namespace FolderCrawl
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
            this.searchAll = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dfsButton = new System.Windows.Forms.RadioButton();
            this.bfsButton = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.fileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.answerLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 49);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Change Folder...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // searchAll
            // 
            this.searchAll.AutoSize = true;
            this.searchAll.Location = new System.Drawing.Point(23, 171);
            this.searchAll.Name = "searchAll";
            this.searchAll.Size = new System.Drawing.Size(118, 17);
            this.searchAll.TabIndex = 2;
            this.searchAll.Text = "Find all occurences";
            this.searchAll.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dfsButton);
            this.groupBox1.Controls.Add(this.bfsButton);
            this.groupBox1.Location = new System.Drawing.Point(23, 223);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // dfsButton
            // 
            this.dfsButton.AutoSize = true;
            this.dfsButton.Location = new System.Drawing.Point(7, 44);
            this.dfsButton.Name = "dfsButton";
            this.dfsButton.Size = new System.Drawing.Size(46, 17);
            this.dfsButton.TabIndex = 1;
            this.dfsButton.TabStop = true;
            this.dfsButton.Text = "DFS";
            this.dfsButton.UseVisualStyleBackColor = true;
            // 
            // bfsButton
            // 
            this.bfsButton.AutoSize = true;
            this.bfsButton.Location = new System.Drawing.Point(7, 20);
            this.bfsButton.Name = "bfsButton";
            this.bfsButton.Size = new System.Drawing.Size(45, 17);
            this.bfsButton.TabIndex = 0;
            this.bfsButton.TabStop = true;
            this.bfsButton.Text = "BFS";
            this.bfsButton.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(23, 344);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Search";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // fileName
            // 
            this.fileName.Location = new System.Drawing.Point(23, 114);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(100, 20);
            this.fileName.TabIndex = 5;
            this.fileName.Text = "Insert File Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(123, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 6;
            // 
            // answerLabel
            // 
            this.answerLabel.Location = new System.Drawing.Point(649, 349);
            this.answerLabel.Name = "answerLabel";
            this.answerLabel.Size = new System.Drawing.Size(100, 15);
            this.answerLabel.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.answerLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fileName);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.searchAll);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox searchAll;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton dfsButton;
        private System.Windows.Forms.RadioButton bfsButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox fileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label answerLabel;
    }
}

