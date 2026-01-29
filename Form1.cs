using System.IdentityModel.Tokens.Jwt;
using System.Windows.Forms;
using Microsoft.VisualBasic.Logging;
using NextBiometrics.Devices;
using RAR.IdCard.Sdk.Reader;
using RAR.IdCard.Sdk.Reader.HN212;
namespace eidca.sample
{
    public partial class Form1 : Form
    {
        private VnHn212Reader _reader = new VnHn212Reader();
        public Form1()
        {
            InitializeComponent();
            InitReader();
        }
        private void InitReader()
        {
            /*Register all reader status event*/
            _reader.OnStatusChanged += Reader_OnStatusChanged;

            // var config = new VnPcscsConfig();
            var config = new VnHn212Config();

            /* set this to false: input access code or scan & read as required
             * true: Auto scan card & read data*/
            // config.AutoReadWhenPresent = false;

            /*Set this flag to process VerifySOD*/
            config.DoVerifySOD = true;

            /*Set this flag to process AA/CA Authen*/
            config.CheckAACAAuthen = true;

            /* Read DG2 */
            config.DoReadDg2Face = true;

            /* Capture face, Minimum time have real face*/
            /* Default is 200ms */
            config.AutoCaptureFaceMinimumMs = 200;

            /* Retry count */
            config.ReadCardRetryCount = 5;

            /* Seq */
            //config.ProcessSequence = HN212ProSeq.CardOnly;
            config.ProcessSequence = HN212ProSeq.CardThenFace;

            /* Use an external camera that is not plugged directly into the HN212-Reader */
            config.CaptureFaceUseFirstCamAvaiable = true;
            config.AutoCaptureFaceAntiSpoofing = true;
            config.CaptureFaceAllowMultiFace = true;
            config.CaptureFaceMinFaceWidth = 300;

            config.AutoCaptureFaceEnabled = true;
            config.CaptureFaceDetectFace = true;
            config.CaptureFaceDisplayTimeout = true;
            config.CaptureFaceTimeout = 200;

            config.HidLibUsed = HN212HidLibrary.HidApi;

            config.internalDecodeQrcodeType = InternalDecodeQrcodeType.DECODE_ONCE;
            config.AllowExtAa = true;
            /*Start reader monitoring ...*/
            _reader.StartMonitor(config);

            _reader.FaceAntiSpoofMode = HN212SpoofMode.MID;
            _reader.OnVideoFrame += reader_OnVideoFrame;

        }

        private void reader_OnVideoFrame(object sender, StatusEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Reader_OnStatusChanged(object sender, StatusEventArgs e)
        {
            //Call from other thread
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)(() =>
                {
                    OnEvent(sender, e);
                }));
            }
            else
            {
                OnEvent(sender, e);
            }
        }
        private void OnEvent(object sender, StatusEventArgs e)
        {
            try
            {


                switch (e.EventName)
                {
                    case EVENT_NAMES.READER:
                        ProcessReaderEvent(e as StatusEventReaderArgs);
                        break;
                    case EVENT_NAMES.CARD:
                        ProcessCardEvent(e as StatusEventCardArgs);
                        break;
                    case EVENT_NAMES.READ:
                        ProcessReadEvent(e as StatusEventReadArgs);
                        break;
                    case EVENT_NAMES.READER_CAMERA:
                        ProcessCameraEvent(e as StatusEventCameraArgs);
                        break;
                }
            }
            catch (Exception ex)
            {
                // Log.Error("Error", ex);
            }
        }

        private void ProcessReaderEvent(StatusEventReaderArgs? ev)
        {
            if (ev == null)
                return;


        }

        private void ProcessCameraEvent(StatusEventCameraArgs? ev)
        {
            if (ev == null)
                return;
        }

        private void ProcessCardEvent(StatusEventCardArgs? ev)
        {
            if (ev == null)
                return;
            //Log.Debug($"----> [{DateTime.Now.ToString("HH:mm:ss")}] Card in Device {ev.ReaderSerialNumber} change stated from: {ev.LastState} to: {ev.NewState}.");
        }
        private void ProcessReadEvent(StatusEventReadArgs? ev)
        {
            try
            {
                if (ev == null)
                    return;
                //Log.Debug($"----> [{DateTime.Now.ToString("HH:mm:ss")}] Device {ev.ReaderSerialNumber} do step: {ev.Step}; status: {ev.Status}; message={ev.Message}.");
                switch (ev.Step)
                {
                    case READ_CARD_STEPS.SCANCARD:
                    case READ_CARD_STEPS.START:
                    case READ_CARD_STEPS.CONNECT_CARD:
                    case READ_CARD_STEPS.PACE:
                    case READ_CARD_STEPS.READ_DGS:
                    case READ_CARD_STEPS.VERIFY_SOD:
                    case READ_CARD_STEPS.AACA_AUTHEN:
                        break;
                    case READ_CARD_STEPS.FINISH:
                        OnReadCardFinish(ev);
                        break;
                }
            }
            catch (Exception exc)
            {
                // Log.Error("Error", exc);
            }
        }

        private void OnReadCardFinish(StatusEventReadArgs ev)
        {

            if (_reader.CardData.Dg2File != null && !string.IsNullOrEmpty(_reader.CardData.Dg2File.FaceImage))
            {
                lblFullName.Text = _reader.CardData.Dg13File?.Name;
                LoadImageToPictureBox(_reader.CardData.Dg2File.FaceImage, picAvatar);              

               
            }
        }
        private void UnInitReader()
        {
            _reader.OnVideoFrame -= reader_OnVideoFrame;
            _reader.OnStatusChanged -= Reader_OnStatusChanged;
            _reader.StopMonitor();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtChallenge.Text = DecodeJwtToken(token_challenge.Text);
        }

        public string DecodeJwtToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            // Kiểm tra token có hợp lệ không
            if (!handler.CanReadToken(token))
            {
                throw new ArgumentException("Token không hợp lệ");
            }

            // Chỉ giải mã mà không xác thực (cho mục đích đọc thông tin)
            var jwtToken = handler.ReadJwtToken(token);

            // Tìm claim có Type = "type" và Value = "challenge"
            var typeClaim = jwtToken.Claims
                .FirstOrDefault(c => c.Type == "challenge");

            return typeClaim?.Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var rst = _reader.StartAA(Convert.FromBase64String(txtChallenge.Text));
            
            
            signature.Text = Convert.ToBase64String(rst.Signature);
            
        }

        private void signature_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            UnInitReader();
        }

        public static Bitmap Base64ToBitmap(string base64String)
        {
            try
            {
                // Loại bỏ data URI scheme nếu có
                string base64Data = CleanBase64String(base64String);

                byte[] imageBytes = Convert.FromBase64String(base64Data);

                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    return new Bitmap(ms);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải hình ảnh: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private static string CleanBase64String(string base64String)
        {
            // Loại bỏ data URI scheme (data:image/png;base64,)
            if (base64String.Contains("base64,"))
            {
                return base64String.Split(',')[1];
            }
            return base64String;
        }

        public static void LoadImageToPictureBox(string base64String, PictureBox pictureBox)
        {
            try
            {
                var bitmap = Base64ToBitmap(base64String);
                if (bitmap != null)
                {
                    pictureBox.Image?.Dispose(); // Giải phóng hình cũ
                    pictureBox.Image = bitmap;
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom; // Tự động scale
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải hình ảnh: {ex.Message}");
            }
        }

    }
}
