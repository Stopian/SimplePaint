namespace SimplePaint
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblAppName = new Label();
            GroupBoxBtn = new GroupBox();
            btnLine = new Button();
            btnRectangle = new Button();
            btnCircle = new Button();
            GroupBoxColor = new GroupBox();
            cmbColor = new ComboBox();
            GroupBoxLineWidth = new GroupBox();
            trbLineWidth = new TrackBar();
            btnOpenFile = new Button();
            btnSaveFile = new Button();
            btnZoomIn = new Button();
            btnZoomOut = new Button();
            saveFileDialog1 = new SaveFileDialog();
            pnlCanvas = new Panel();
            picCanvas = new PictureBox();
            GroupBoxBtn.SuspendLayout();
            GroupBoxColor.SuspendLayout();
            GroupBoxLineWidth.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trbLineWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picCanvas).BeginInit();
            SuspendLayout();
            // 
            // lblAppName
            // 
            lblAppName.AutoSize = true;
            lblAppName.Font = new Font("맑은 고딕", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblAppName.ForeColor = SystemColors.ActiveCaption;
            lblAppName.Location = new Point(36, 22);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(177, 37);
            lblAppName.TabIndex = 0;
            lblAppName.Text = "Simple Paint";
            // 
            // GroupBoxBtn
            // 
            GroupBoxBtn.Controls.Add(btnCircle);
            GroupBoxBtn.Controls.Add(btnLine);
            GroupBoxBtn.Controls.Add(btnRectangle);
            GroupBoxBtn.Location = new Point(36, 94);
            GroupBoxBtn.Name = "GroupBoxBtn";
            GroupBoxBtn.Size = new Size(226, 100);
            GroupBoxBtn.TabIndex = 1;
            GroupBoxBtn.TabStop = false;
            GroupBoxBtn.Text = "도형 선택";
            // 
            // btnLine
            // 
            btnLine.BackColor = Color.White;
            btnLine.Image = Properties.Resources.KakaoTalk_20260430_100205471_02;
            btnLine.ImageAlign = ContentAlignment.TopCenter;
            btnLine.Location = new Point(6, 22);
            btnLine.Name = "btnLine";
            btnLine.Size = new Size(67, 63);
            btnLine.TabIndex = 2;
            btnLine.Text = "직선";
            btnLine.TextAlign = ContentAlignment.BottomCenter;
            btnLine.UseVisualStyleBackColor = false;
            // 
            // btnRectangle
            // 
            btnRectangle.BackColor = Color.White;
            btnRectangle.Image = Properties.Resources.KakaoTalk_20260430_100205471;
            btnRectangle.ImageAlign = ContentAlignment.TopCenter;
            btnRectangle.Location = new Point(79, 22);
            btnRectangle.Name = "btnRectangle";
            btnRectangle.Size = new Size(67, 63);
            btnRectangle.TabIndex = 3;
            btnRectangle.Text = "사각형";
            btnRectangle.TextAlign = ContentAlignment.BottomCenter;
            btnRectangle.UseVisualStyleBackColor = false;
            // 
            // btnCircle
            // 
            btnCircle.BackColor = Color.White;
            btnCircle.Image = Properties.Resources.KakaoTalk_20260430_100205471_01;
            btnCircle.ImageAlign = ContentAlignment.TopCenter;
            btnCircle.Location = new Point(152, 22);
            btnCircle.Name = "btnCircle";
            btnCircle.Size = new Size(67, 63);
            btnCircle.TabIndex = 4;
            btnCircle.Text = "원";
            btnCircle.TextAlign = ContentAlignment.BottomCenter;
            btnCircle.UseVisualStyleBackColor = false;
            // 
            // GroupBoxColor
            // 
            GroupBoxColor.Controls.Add(cmbColor);
            GroupBoxColor.Location = new Point(289, 94);
            GroupBoxColor.Name = "GroupBoxColor";
            GroupBoxColor.Size = new Size(117, 100);
            GroupBoxColor.TabIndex = 5;
            GroupBoxColor.TabStop = false;
            GroupBoxColor.Text = "색 선택";
            // 
            // cmbColor
            // 
            cmbColor.FormattingEnabled = true;
            cmbColor.Items.AddRange(new object[] { "Black 검정 ", "Red 빨강", "Blue 파랑 ", "Green 녹색" });
            cmbColor.Location = new Point(3, 43);
            cmbColor.Name = "cmbColor";
            cmbColor.Size = new Size(112, 23);
            cmbColor.TabIndex = 6;
            cmbColor.SelectedIndexChanged += cmbColor_SelectedIndexChanged;
            // 
            // GroupBoxLineWidth
            // 
            GroupBoxLineWidth.Controls.Add(trbLineWidth);
            GroupBoxLineWidth.Location = new Point(432, 94);
            GroupBoxLineWidth.Name = "GroupBoxLineWidth";
            GroupBoxLineWidth.Size = new Size(200, 100);
            GroupBoxLineWidth.TabIndex = 6;
            GroupBoxLineWidth.TabStop = false;
            GroupBoxLineWidth.Text = "선 두께";
            // 
            // trbLineWidth
            // 
            trbLineWidth.Location = new Point(6, 41);
            trbLineWidth.Name = "trbLineWidth";
            trbLineWidth.Size = new Size(188, 45);
            trbLineWidth.TabIndex = 7;
            // 
            // btnOpenFile
            // 
            btnOpenFile.BackColor = Color.Yellow;
            btnOpenFile.Font = new Font("맑은 고딕", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnOpenFile.Location = new Point(651, 106);
            btnOpenFile.Name = "btnOpenFile";
            btnOpenFile.Size = new Size(90, 81);
            btnOpenFile.TabIndex = 8;
            btnOpenFile.Text = "열기";
            btnOpenFile.UseVisualStyleBackColor = false;
            btnOpenFile.Click += btnLoad_Click;
            // 
            // btnZoomIn
            // 
            btnZoomIn.BackColor = Color.LightGray;
            btnZoomIn.Font = new Font("맑은 고딕", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnZoomIn.Location = new Point(651, 60);
            btnZoomIn.Name = "btnZoomIn";
            btnZoomIn.Size = new Size(90, 35);
            btnZoomIn.TabIndex = 11;
            btnZoomIn.Text = "확대";
            btnZoomIn.UseVisualStyleBackColor = false;
            btnZoomIn.Click += btnZoomIn_Click;
            // 
            // btnZoomOut
            // 
            btnZoomOut.BackColor = Color.LightGray;
            btnZoomOut.Font = new Font("맑은 고딕", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnZoomOut.Location = new Point(747, 60);
            btnZoomOut.Name = "btnZoomOut";
            btnZoomOut.Size = new Size(90, 35);
            btnZoomOut.TabIndex = 12;
            btnZoomOut.Text = "축소";
            btnZoomOut.UseVisualStyleBackColor = false;
            btnZoomOut.Click += btnZoomOut_Click;
            // 
            // btnSaveFile
            // 
            btnSaveFile.BackColor = Color.FromArgb(128, 255, 128);
            btnSaveFile.Font = new Font("맑은 고딕", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnSaveFile.Location = new Point(747, 106);
            btnSaveFile.Name = "btnSaveFile";
            btnSaveFile.Size = new Size(90, 81);
            btnSaveFile.TabIndex = 9;
            btnSaveFile.Text = "저장";
            btnSaveFile.UseVisualStyleBackColor = false;
            btnSaveFile.Click += btnSave_Click;
            // 
            // pnlCanvas
            // 
            pnlCanvas.Location = new Point(36, 204);
            pnlCanvas.Name = "pnlCanvas";
            pnlCanvas.Size = new Size(801, 468);
            pnlCanvas.TabIndex = 10;
            pnlCanvas.AutoScroll = true;
            // 
            // picCanvas
            // 
            picCanvas.Location = new Point(0, 0);
            picCanvas.Name = "picCanvas";
            picCanvas.Size = new Size(801, 468);
            picCanvas.TabIndex = 0;
            picCanvas.TabStop = false;
            picCanvas.Click += picCanvas_Click;
            picCanvas.SizeMode = PictureBoxSizeMode.Normal;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(881, 684);
            pnlCanvas.Controls.Add(picCanvas);
            Controls.Add(btnZoomOut);
            Controls.Add(btnZoomIn);
            Controls.Add(pnlCanvas);
            Controls.Add(btnSaveFile);
            Controls.Add(btnOpenFile);
            Controls.Add(GroupBoxLineWidth);
            Controls.Add(GroupBoxColor);
            Controls.Add(GroupBoxBtn);
            Controls.Add(lblAppName);
            Name = "Form1";
            Text = "Simple Paint v1.0";
            GroupBoxBtn.ResumeLayout(false);
            GroupBoxColor.ResumeLayout(false);
            GroupBoxLineWidth.ResumeLayout(false);
            GroupBoxLineWidth.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trbLineWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)picCanvas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblAppName;
        private GroupBox GroupBoxBtn;
        private Button btnLine;
        private Button btnRectangle;
        private Button btnCircle;
        private GroupBox GroupBoxColor;
        private ComboBox cmbColor;
        private GroupBox GroupBoxLineWidth;
        private TrackBar trbLineWidth;
        private Button btnOpenFile;
        private Button btnSaveFile;
        private PictureBox picCanvas;
        private Panel pnlCanvas;
        private Button btnZoomIn;
        private Button btnZoomOut;
        private SaveFileDialog saveFileDialog1;
    }
}
