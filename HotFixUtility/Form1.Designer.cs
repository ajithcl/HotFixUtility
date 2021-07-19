
namespace HotFixUtility
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbEnvironment = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnTransferAsciiFiles = new System.Windows.Forms.Button();
            this.btnTransferFiles = new System.Windows.Forms.Button();
            this.btnAddProlibFiles = new System.Windows.Forms.Button();
            this.btnAddAsciiFileProlib = new System.Windows.Forms.Button();
            this.btnRTBTransfer = new System.Windows.Forms.Button();
            this.btnCreateHF = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnFileSelect = new System.Windows.Forms.Button();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(893, 38);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(76, 24);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Environment :";
            // 
            // cmbEnvironment
            // 
            this.cmbEnvironment.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEnvironment.FormattingEnabled = true;
            this.cmbEnvironment.Location = new System.Drawing.Point(125, 38);
            this.cmbEnvironment.Name = "cmbEnvironment";
            this.cmbEnvironment.Size = new System.Drawing.Size(224, 28);
            this.cmbEnvironment.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Input File :";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(125, 80);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(474, 27);
            this.textBox1.TabIndex = 4;
            // 
            // btnTransferAsciiFiles
            // 
            this.btnTransferAsciiFiles.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransferAsciiFiles.Location = new System.Drawing.Point(125, 120);
            this.btnTransferAsciiFiles.Name = "btnTransferAsciiFiles";
            this.btnTransferAsciiFiles.Size = new System.Drawing.Size(224, 35);
            this.btnTransferAsciiFiles.TabIndex = 6;
            this.btnTransferAsciiFiles.Text = "Ascii Files";
            this.btnTransferAsciiFiles.UseVisualStyleBackColor = true;
            this.btnTransferAsciiFiles.Click += new System.EventHandler(this.btnTransferAsciiFiles_Click);
            // 
            // btnTransferFiles
            // 
            this.btnTransferFiles.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransferFiles.Location = new System.Drawing.Point(375, 120);
            this.btnTransferFiles.Name = "btnTransferFiles";
            this.btnTransferFiles.Size = new System.Drawing.Size(224, 35);
            this.btnTransferFiles.TabIndex = 7;
            this.btnTransferFiles.Text = "All Files";
            this.btnTransferFiles.UseVisualStyleBackColor = true;
            // 
            // btnAddProlibFiles
            // 
            this.btnAddProlibFiles.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddProlibFiles.Location = new System.Drawing.Point(125, 161);
            this.btnAddProlibFiles.Name = "btnAddProlibFiles";
            this.btnAddProlibFiles.Size = new System.Drawing.Size(224, 35);
            this.btnAddProlibFiles.TabIndex = 8;
            this.btnAddProlibFiles.Text = "Add files";
            this.btnAddProlibFiles.UseVisualStyleBackColor = true;
            // 
            // btnAddAsciiFileProlib
            // 
            this.btnAddAsciiFileProlib.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAsciiFileProlib.Location = new System.Drawing.Point(375, 161);
            this.btnAddAsciiFileProlib.Name = "btnAddAsciiFileProlib";
            this.btnAddAsciiFileProlib.Size = new System.Drawing.Size(224, 35);
            this.btnAddAsciiFileProlib.TabIndex = 9;
            this.btnAddAsciiFileProlib.Text = "Add Ascii files";
            this.btnAddAsciiFileProlib.UseVisualStyleBackColor = true;
            // 
            // btnRTBTransfer
            // 
            this.btnRTBTransfer.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRTBTransfer.Location = new System.Drawing.Point(125, 202);
            this.btnRTBTransfer.Name = "btnRTBTransfer";
            this.btnRTBTransfer.Size = new System.Drawing.Size(474, 35);
            this.btnRTBTransfer.TabIndex = 10;
            this.btnRTBTransfer.Text = "RTB Files Transfer";
            this.btnRTBTransfer.UseVisualStyleBackColor = true;
            // 
            // btnCreateHF
            // 
            this.btnCreateHF.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateHF.Location = new System.Drawing.Point(125, 243);
            this.btnCreateHF.Name = "btnCreateHF";
            this.btnCreateHF.Size = new System.Drawing.Size(474, 35);
            this.btnCreateHF.TabIndex = 11;
            this.btnCreateHF.Text = "Create";
            this.btnCreateHF.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnFileSelect
            // 
            this.btnFileSelect.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFileSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnFileSelect.Image")));
            this.btnFileSelect.Location = new System.Drawing.Point(605, 80);
            this.btnFileSelect.Name = "btnFileSelect";
            this.btnFileSelect.Size = new System.Drawing.Size(46, 37);
            this.btnFileSelect.TabIndex = 12;
            this.btnFileSelect.UseVisualStyleBackColor = true;
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadFile.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadFile.Image")));
            this.btnLoadFile.Location = new System.Drawing.Point(657, 80);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(46, 37);
            this.btnLoadFile.TabIndex = 13;
            this.btnLoadFile.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 24);
            this.label3.TabIndex = 14;
            this.label3.Text = "Transfer :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 24);
            this.label4.TabIndex = 15;
            this.label4.Text = "Prolibrary :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 210);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 24);
            this.label5.TabIndex = 16;
            this.label5.Text = "RTB Files :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 251);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 24);
            this.label6.TabIndex = 17;
            this.label6.Text = "Hotfix :";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 296);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnLoadFile);
            this.Controls.Add(this.btnFileSelect);
            this.Controls.Add(this.btnCreateHF);
            this.Controls.Add(this.btnRTBTransfer);
            this.Controls.Add(this.btnAddAsciiFileProlib);
            this.Controls.Add(this.btnAddProlibFiles);
            this.Controls.Add(this.btnTransferFiles);
            this.Controls.Add(this.btnTransferAsciiFiles);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbEnvironment);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Hotfix Utility";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbEnvironment;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnTransferAsciiFiles;
        private System.Windows.Forms.Button btnTransferFiles;
        private System.Windows.Forms.Button btnAddProlibFiles;
        private System.Windows.Forms.Button btnAddAsciiFileProlib;
        private System.Windows.Forms.Button btnRTBTransfer;
        private System.Windows.Forms.Button btnCreateHF;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnFileSelect;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}

