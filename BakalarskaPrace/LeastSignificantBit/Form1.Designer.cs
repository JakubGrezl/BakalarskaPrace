namespace LeastSignificantBit
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.loadImageButton = new System.Windows.Forms.Button();
            this.codeRbtn = new System.Windows.Forms.RadioButton();
            this.decodeRbtn = new System.Windows.Forms.RadioButton();
            this.loadedImage = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.readFromFileButton = new System.Windows.Forms.Button();
            this.experimentToggle = new System.Windows.Forms.CheckBox();
            this.originalImageButton = new System.Windows.Forms.Button();
            this.addjustedImageButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.pathTextbox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.saveToFileButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lsbRbtn = new System.Windows.Forms.RadioButton();
            this.pvdRbtn = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.loadedImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // loadImageButton
            // 
            this.loadImageButton.Location = new System.Drawing.Point(12, 12);
            this.loadImageButton.Name = "loadImageButton";
            this.loadImageButton.Size = new System.Drawing.Size(265, 64);
            this.loadImageButton.TabIndex = 0;
            this.loadImageButton.Text = "Načíst obrázek";
            this.loadImageButton.UseVisualStyleBackColor = true;
            this.loadImageButton.Click += new System.EventHandler(this.loadImageButton_Click);
            // 
            // codeRbtn
            // 
            this.codeRbtn.AutoSize = true;
            this.codeRbtn.Checked = true;
            this.codeRbtn.Location = new System.Drawing.Point(23, 30);
            this.codeRbtn.Name = "codeRbtn";
            this.codeRbtn.Size = new System.Drawing.Size(112, 29);
            this.codeRbtn.TabIndex = 1;
            this.codeRbtn.TabStop = true;
            this.codeRbtn.Text = "Kódování";
            this.codeRbtn.UseVisualStyleBackColor = true;
            this.codeRbtn.CheckedChanged += new System.EventHandler(this.codeRbtn_CheckedChanged);
            // 
            // decodeRbtn
            // 
            this.decodeRbtn.AutoSize = true;
            this.decodeRbtn.Location = new System.Drawing.Point(23, 65);
            this.decodeRbtn.Name = "decodeRbtn";
            this.decodeRbtn.Size = new System.Drawing.Size(133, 29);
            this.decodeRbtn.TabIndex = 2;
            this.decodeRbtn.Text = "Dekódování";
            this.decodeRbtn.UseVisualStyleBackColor = true;
            this.decodeRbtn.CheckedChanged += new System.EventHandler(this.decodeRbtn_CheckedChanged);
            // 
            // loadedImage
            // 
            this.loadedImage.BackColor = System.Drawing.Color.White;
            this.loadedImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.loadedImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.loadedImage.Location = new System.Drawing.Point(13, 232);
            this.loadedImage.Name = "loadedImage";
            this.loadedImage.Size = new System.Drawing.Size(628, 463);
            this.loadedImage.TabIndex = 3;
            this.loadedImage.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 201);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Načtený obrázek";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(496, 12);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(180, 31);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(283, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(207, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Počet bitů pro kódování:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(646, 232);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(887, 463);
            this.richTextBox1.TabIndex = 8;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(646, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 25);
            this.label4.TabIndex = 9;
            this.label4.Text = "Tajná zpráva";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(1139, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(404, 46);
            this.label5.TabIndex = 10;
            this.label5.Text = "Velikost zprávy (ASCII):";
            this.label5.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // readFromFileButton
            // 
            this.readFromFileButton.BackgroundImage = global::LeastSignificantBit.Properties.Resources.read_from_file_icon;
            this.readFromFileButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.readFromFileButton.Location = new System.Drawing.Point(1469, 245);
            this.readFromFileButton.Name = "readFromFileButton";
            this.readFromFileButton.Size = new System.Drawing.Size(51, 47);
            this.readFromFileButton.TabIndex = 11;
            this.readFromFileButton.UseVisualStyleBackColor = true;
            this.readFromFileButton.Click += new System.EventHandler(this.readFromFileButton_Click);
            // 
            // experimentToggle
            // 
            this.experimentToggle.AutoSize = true;
            this.experimentToggle.Location = new System.Drawing.Point(719, 12);
            this.experimentToggle.Name = "experimentToggle";
            this.experimentToggle.Size = new System.Drawing.Size(126, 29);
            this.experimentToggle.TabIndex = 12;
            this.experimentToggle.Text = "Experiment";
            this.experimentToggle.UseVisualStyleBackColor = true;
            // 
            // originalImageButton
            // 
            this.originalImageButton.Location = new System.Drawing.Point(13, 698);
            this.originalImageButton.Name = "originalImageButton";
            this.originalImageButton.Size = new System.Drawing.Size(201, 64);
            this.originalImageButton.TabIndex = 13;
            this.originalImageButton.Text = "Originální obrázek";
            this.originalImageButton.UseVisualStyleBackColor = true;
            this.originalImageButton.Click += new System.EventHandler(this.originalImageButton_Click);
            // 
            // addjustedImageButton
            // 
            this.addjustedImageButton.Location = new System.Drawing.Point(440, 698);
            this.addjustedImageButton.Name = "addjustedImageButton";
            this.addjustedImageButton.Size = new System.Drawing.Size(201, 64);
            this.addjustedImageButton.TabIndex = 14;
            this.addjustedImageButton.Text = "Upravený obrázek";
            this.addjustedImageButton.UseVisualStyleBackColor = true;
            this.addjustedImageButton.Click += new System.EventHandler(this.addjustedImageButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(1288, 698);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(245, 64);
            this.startButton.TabIndex = 15;
            this.startButton.Text = "Kódovat";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // pathTextbox
            // 
            this.pathTextbox.Location = new System.Drawing.Point(652, 726);
            this.pathTextbox.Name = "pathTextbox";
            this.pathTextbox.Size = new System.Drawing.Size(573, 31);
            this.pathTextbox.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(652, 698);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(573, 25);
            this.label6.TabIndex = 17;
            this.label6.Text = "Cesta k výstupnímu souboru (nechte prázdný, pokud nechcete ukládat)";
            // 
            // saveToFileButton
            // 
            this.saveToFileButton.BackgroundImage = global::LeastSignificantBit.Properties.Resources.read_from_file_icon;
            this.saveToFileButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.saveToFileButton.Location = new System.Drawing.Point(1231, 715);
            this.saveToFileButton.Name = "saveToFileButton";
            this.saveToFileButton.Size = new System.Drawing.Size(51, 47);
            this.saveToFileButton.TabIndex = 18;
            this.saveToFileButton.UseVisualStyleBackColor = true;
            this.saveToFileButton.Click += new System.EventHandler(this.saveToFileButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(295, 169);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(346, 60);
            this.label7.TabIndex = 19;
            this.label7.Text = "Celková/asdasd";
            this.label7.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.codeRbtn);
            this.groupBox1.Controls.Add(this.decodeRbtn);
            this.groupBox1.Location = new System.Drawing.Point(13, 82);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(216, 109);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kódování/Dekódování";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lsbRbtn);
            this.groupBox2.Controls.Add(this.pvdRbtn);
            this.groupBox2.Location = new System.Drawing.Point(652, 82);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(300, 109);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Metoda";
            // 
            // lsbRbtn
            // 
            this.lsbRbtn.AutoSize = true;
            this.lsbRbtn.Checked = true;
            this.lsbRbtn.Location = new System.Drawing.Point(23, 30);
            this.lsbRbtn.Name = "lsbRbtn";
            this.lsbRbtn.Size = new System.Drawing.Size(188, 29);
            this.lsbRbtn.TabIndex = 1;
            this.lsbRbtn.TabStop = true;
            this.lsbRbtn.Text = "Least significant bit";
            this.lsbRbtn.UseVisualStyleBackColor = true;
            this.lsbRbtn.CheckedChanged += new System.EventHandler(this.lsbRbtn_CheckedChanged);
            // 
            // pvdRbtn
            // 
            this.pvdRbtn.AutoSize = true;
            this.pvdRbtn.Location = new System.Drawing.Point(23, 65);
            this.pvdRbtn.Name = "pvdRbtn";
            this.pvdRbtn.Size = new System.Drawing.Size(217, 29);
            this.pvdRbtn.TabIndex = 2;
            this.pvdRbtn.Text = "Pixel value differencing";
            this.pvdRbtn.UseVisualStyleBackColor = true;
            this.pvdRbtn.CheckedChanged += new System.EventHandler(this.pvdRbtn_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(515, 768);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 64);
            this.button1.TabIndex = 23;
            this.button1.Text = "Uložit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(983, 147);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(220, 29);
            this.checkBox1.TabIndex = 24;
            this.checkBox1.Text = "Zkusit omezit na 0-255";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1555, 929);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.saveToFileButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pathTextbox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.addjustedImageButton);
            this.Controls.Add(this.originalImageButton);
            this.Controls.Add(this.experimentToggle);
            this.Controls.Add(this.readFromFileButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loadedImage);
            this.Controls.Add(this.loadImageButton);
            this.Name = "Form1";
            this.Text = "Least Significant Bit";
            ((System.ComponentModel.ISupportInitialize)(this.loadedImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button loadImageButton;
        private RadioButton codeRbtn;
        private RadioButton decodeRbtn;
        private PictureBox loadedImage;
        private Label label1;
        private NumericUpDown numericUpDown1;
        private Label label2;
        private RichTextBox richTextBox1;
        private Label label4;
        private Label label5;
        private Button readFromFileButton;
        private CheckBox experimentToggle;
        private Button originalImageButton;
        private Button addjustedImageButton;
        private Button startButton;
        private TextBox pathTextbox;
        private Label label6;
        private Button saveToFileButton;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private Label label7;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private RadioButton lsbRbtn;
        private RadioButton pvdRbtn;
        private Button button1;
        private CheckBox checkBox1;
    }
}