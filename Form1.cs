namespace SimplePaint
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows.Forms;
    public partial class Form1 : Form
    {
        enum ToolType { Line, Rectangle, Circle } // 사용할 도형 타입
        private Bitmap canvasBitmap; // 실제 그림이 저장되는 비트맵
        private Graphics canvasGraphics; // 비트맵 위에 그리기 위한객체
        private bool isDrawing = false; // 현재 드래그 중인지 여부
        private Point startPoint; // 드래그 시작점
        private Point endPoint; // 드래그 끝점
        private ToolType currentTool = ToolType.Line; // 현재 선택된 도형
        private Color currentColor = Color.Black; // 현재 색상
        private int currentLineWidth = 2; // 현재 선 두께
        private float zoomFactor = 1.0f; // 확대 축소 비율

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            // 캔버스 초기화
            // 기본 색상 콤보박스 선택을 초기화 (기본 Black)
            if (cmbColor != null && cmbColor.Items.Count > 0)
            {
                cmbColor.SelectedIndex = 0;
                currentColor = Color.Black;
            }
            canvasBitmap = new Bitmap(picCanvas.Width, picCanvas.Height);
            canvasGraphics = Graphics.FromImage(canvasBitmap);
            // 캔버스 그래픽 품질 설정
            canvasGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            canvasGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            canvasGraphics.Clear(Color.White); // 캔버스를 흰색으로 초기화
            // Ensure panel scroll min size matches initial canvas
            if (pnlCanvas != null)
            {
                pnlCanvas.AutoScrollMinSize = new Size((int)(canvasBitmap.Width * zoomFactor), (int)(canvasBitmap.Height * zoomFactor));
            }
            // 마우스 입력과 페인트 이벤트를 코드에서 연결
            picCanvas.MouseDown += picCanvas_MouseDown;
            picCanvas.MouseMove += picCanvas_MouseMove;
            picCanvas.MouseUp += picCanvas_MouseUp;
            picCanvas.Paint += picCanvas_Paint;
            // 버튼 및 트랙바 이벤트 연결 (디자이너와의 이름 차이 처리)
            btnLine.Click += btnLine_Click;
            btnRectangle.Click += btnRect_Click;
            btnCircle.Click += btnCircle_Click;
            trbLineWidth.ValueChanged += tkbWidth_ValueChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void picCanvas_Click(object sender, EventArgs e)
        {

        }

        // 직선 버튼 클릭 시
        private void btnLine_Click(object sender, EventArgs e)
        {
            currentTool = ToolType.Line;
        }

        // 사각형 버튼 클릭 시
        private void btnRect_Click(object sender, EventArgs e)
        {
            currentTool = ToolType.Rectangle;
        }

        // 원 버튼 클릭 시
        private void btnCircle_Click(object sender, EventArgs e)
        {
            currentTool = ToolType.Circle;
        }

        private void cmbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 콤보박스의 선택된 인덱스에 따라 색상 지정
            switch (cmbColor.SelectedIndex)
            {
                case 0: // Black
                    currentColor = Color.Black;
                    break;
                case 1: // Red
                    currentColor = Color.Red;
                    break;
                case 2: // Blue
                    currentColor = Color.Blue;
                    break;
                case 3: // Green
                    currentColor = Color.Green;
                    break;
            }
        }

        private void tkbWidth_ValueChanged(object sender, EventArgs e)
        {
            // sender에서 트랙바를 가져와 선 두께 변수에 저장
            if (sender is TrackBar tb)
            {
                currentLineWidth = tb.Value;
            }
        }

        // 마우스를 누를 때 (그리기 시작)
        private void picCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            isDrawing = true;
            // Convert control coordinates to bitmap coordinates according to zoom
            startPoint = new Point((int)(e.Location.X / zoomFactor), (int)(e.Location.Y / zoomFactor)); // 시작점 저장
            endPoint = startPoint;
        }

        // 마우스를 움직일 때 (실시간 미리보기 피드백)
        private void picCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                // Convert to bitmap coordinates
                endPoint = new Point((int)(e.Location.X / zoomFactor), (int)(e.Location.Y / zoomFactor)); // 현재 마우스 위치를 끝점으로 업데이트
                picCanvas.Invalidate(); // picCanvas_Paint 함수를 호출하여 화면을 다시 그림
            }
        }

        // 마우스를 뗄 때 (비트맵에 최종 그리기)
        private void picCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isDrawing)
                return;

            if (e.Button != MouseButtons.Left)
                return;

            isDrawing = false;
            // Convert to bitmap coordinates
            endPoint = new Point((int)(e.Location.X / zoomFactor), (int)(e.Location.Y / zoomFactor));

            // 비트맵(canvasBitmap)에 실제로 그림을 그림 (최종은 실선)
            DrawShape(canvasGraphics, preview: false);
            // Update display size and refresh
            UpdateCanvasDisplaySize();
            picCanvas.Invalidate();
        }

        // 실시간 미리보기를 위한 Paint 이벤트
        // PictureBox의 Paint 이벤트에 연결되어야 함
        private void picCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (canvasBitmap != null)
            {
                // Apply scaling so bitmap and preview use same logical coordinates
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // Scale transform according to zoomFactor
                if (Math.Abs(zoomFactor - 1.0f) > 0.0001f)
                {
                    e.Graphics.ScaleTransform(zoomFactor, zoomFactor);
                }

                e.Graphics.DrawImageUnscaled(canvasBitmap, Point.Empty);

                if (isDrawing)
                {
                    // Draw preview overlay (dashed)
                    DrawShape(e.Graphics, preview: true);
                }
            }
        }

        // 공통 그리기 로직 (도형 선택 기능 포함)
        private void DrawShape(Graphics g, bool preview)
        {
            // 그래픽 품질
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (var myPen = new Pen(currentColor, Math.Max(1, currentLineWidth)))
            {
                if (preview)
                {
                    // 점선을 더 잘 보이게 하기 위해 대시 패턴을 설정
                    myPen.DashStyle = DashStyle.Dash;
                    myPen.DashPattern = new float[] { 4f, 4f };
                }
                else
                {
                    myPen.DashStyle = DashStyle.Solid;
                }

                switch (currentTool)
                {
                    case ToolType.Line:
                        g.DrawLine(myPen, startPoint, endPoint);
                        break;
                    case ToolType.Rectangle:
                        g.DrawRectangle(myPen, GetRectangle());
                        break;
                    case ToolType.Circle:
                        g.DrawEllipse(myPen, GetRectangle());
                        break;
                }
            }
        }

        private void UpdateCanvasDisplaySize()
        {
            if (canvasBitmap == null || picCanvas == null) return;
            var w = (int)(canvasBitmap.Width * zoomFactor);
            var h = (int)(canvasBitmap.Height * zoomFactor);
            picCanvas.Size = new Size(Math.Max(1, w), Math.Max(1, h));
            if (pnlCanvas != null)
            {
                pnlCanvas.AutoScrollMinSize = picCanvas.Size;
            }
        }

        private void SetZoom(float newZoom)
        {
            zoomFactor = Math.Max(0.1f, Math.Min(4.0f, newZoom));
            UpdateCanvasDisplaySize();
            picCanvas.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.Oemplus || e.KeyCode == Keys.Add)
                {
                    SetZoom(zoomFactor * 1.25f);
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.OemMinus || e.KeyCode == Keys.Subtract)
                {
                    SetZoom(zoomFactor / 1.25f);
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.D0)
                {
                    SetZoom(1.0f);
                    e.Handled = true;
                }
            }
        }

        // 이미지 불러오기 버튼 클릭
        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "그림 불러오기";
                ofd.Filter = "이미지 파일(*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp|모든 파일(*.*)|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (Image loadedImage = Image.FromFile(ofd.FileName))
                        {
                            // Dispose previous resources
                            canvasGraphics?.Dispose();
                            canvasBitmap?.Dispose();

                            // Create bitmap from loaded image (preserve pixel format)
                            canvasBitmap = new Bitmap(loadedImage.Width, loadedImage.Height, PixelFormat.Format32bppArgb);
                            using (var g = Graphics.FromImage(canvasBitmap))
                            {
                                g.Clear(Color.White);
                                g.DrawImage(loadedImage, 0, 0, loadedImage.Width, loadedImage.Height);
                            }

                            canvasGraphics = Graphics.FromImage(canvasBitmap);
                            canvasGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                            canvasGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                            // Reset zoom to 1:1 and resize display
                            zoomFactor = 1.0f;
                            UpdateCanvasDisplaySize();
                            picCanvas.Invalidate();
                        }

                        MessageBox.Show("이미지를 성공적으로 불러왔습니다.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("이미지를 불러오는 중 오류가 발생했습니다: " + ex.Message);
                    }
                }
            }
        }

        // 사각형/원 그리기를 위한 좌표 계산 보조 함수
        private Rectangle GetRectangle()
        {
            return new Rectangle(
                Math.Min(startPoint.X, endPoint.X),
                Math.Min(startPoint.Y, endPoint.Y),
                Math.Abs(startPoint.X - endPoint.X),
                Math.Abs(startPoint.Y - endPoint.Y));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (canvasBitmap == null)
            {
                MessageBox.Show("저장할 그림이 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 파일 저장 다이얼로그 설정
            saveFileDialog1.Title = "그림 저장하기";
            saveFileDialog1.FileName = "MyDrawing";
            saveFileDialog1.Filter = "PNG 파일(*.png)|*.png|JPG 파일(*.jpg)|*.jpg|BMP 파일(*.bmp)|*.bmp";
            saveFileDialog1.DefaultExt = "png";
            saveFileDialog1.AddExtension = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fileName = saveFileDialog1.FileName;
                    string ext = Path.GetExtension(fileName).ToLowerInvariant();
                    if (string.IsNullOrEmpty(ext))
                    {
                        ext = ".png";
                        fileName += ext;
                    }

                    ImageFormat fmt;
                    switch (ext)
                    {
                        case ".jpg":
                        case ".jpeg":
                            fmt = ImageFormat.Jpeg;
                            break;
                        case ".bmp":
                            fmt = ImageFormat.Bmp;
                            break;
                        default:
                            fmt = ImageFormat.Png;
                            break;
                    }

                    if (fmt.Equals(ImageFormat.Jpeg))
                    {
                        using (var rgb = new Bitmap(canvasBitmap.Width, canvasBitmap.Height, PixelFormat.Format24bppRgb))
                        {
                            using (var g = Graphics.FromImage(rgb))
                            {
                                g.Clear(Color.White);
                                g.DrawImage(canvasBitmap, 0, 0);
                            }
                            rgb.Save(fileName, fmt);
                        }
                    }
                    else
                    {
                        canvasBitmap.Save(fileName, fmt);
                    }

                    MessageBox.Show("그림이 성공적으로 저장되었습니다!", "저장 성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("저장 중 오류가 발생했습니다: " + ex.Message, "저장 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            SetZoom(zoomFactor * 1.25f);
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            SetZoom(zoomFactor / 1.25f);
        }
    }
}
