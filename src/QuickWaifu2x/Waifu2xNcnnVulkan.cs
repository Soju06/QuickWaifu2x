using System.Diagnostics;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace QuickWaifu2x {
    internal static class Waifu2xNcnnVulkan {
        public static Waifu2xNcnnVulkanProcess Create(Waifu2xNcnnVulkanParameter parameter, string? path = null) =>
            new(parameter, path);

        public static readonly ImageFormat[] AvailableTypes = new ImageFormat[] { ImageFormat.Png, ImageFormat.Jpeg };
    }

    internal class Waifu2xNcnnVulkanProcess : IDisposable {
        internal Waifu2xNcnnVulkanProcess(Waifu2xNcnnVulkanParameter parameter, string? path = null) {
            if (path.IsNullOrWhiteSpace()) path = Application.StartupPath.JoinDir("waifu2x-ncnn-vulkan.exe");
            Process = new();
            var args = "";
            parameter.GetParameter().All(r => _ = r.IsNullOrWhiteSpace() 
                ? null : (args += (r.Contains(' ') ? $"\"{r}\"" : r) + " "));
            Process.StartInfo.FileName = path;
            Process.StartInfo.Arguments = args;
            Process.StartInfo.RedirectStandardOutput = true;
            Process.StartInfo.UseShellExecute = false;
            Process.StartInfo.CreateNoWindow = true;
            Process.StartInfo.RedirectStandardError = true;
            Process.StartInfo.RedirectStandardOutput = true;
            Process.OutputDataReceived += OutputDataReceived;
            Process.ErrorDataReceived += OutputDataReceived;
            Process.Exited += Exited;
        }

        public string Result { get; private set; }

        public void Start() {
            Process?.Start();
            Process?.BeginErrorReadLine();
            Process?.BeginOutputReadLine();
        }

        public void Join() =>
            Process?.WaitForExit();

        private void Exited(object? sender, EventArgs e) =>
            OnExited?.Invoke(this, new(Process.TotalProcessorTime, Process.ExitCode));
        private void OutputDataReceived(object sender, DataReceivedEventArgs e) {
            OnReceived?.Invoke(this, e.Data); Result += e.Data;
        }

        public event EventHandler<string?>? OnReceived;
        public event EventHandler<(TimeSpan, int)>? OnExited;

        Process? Process;
        bool disposedValue;

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    Process?.Kill();
                    Process?.Dispose();
                }

                Process = null;
                disposedValue = true;
            }
        }

        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    public static class Waifu2xNcnnVulkanModels {
        public const string Cunet = "models-cunet";
        public const string AnimeStyle = "models-upconv_7_anime_style_art_rgb";
        public const string Photo = "models-upconv_7_photo";
    }

    public class Waifu2xNcnnVulkanParameter : ICloneable {
        /// <summary>
        /// 소스 파일
        /// </summary>
        public string? Source { get; set; }

        /// <summary>
        /// 타겟
        /// </summary>
        public string? Target { get; set; }

        /// <summary>
        /// 노이즈 레벨
        /// </summary>
        public Waifu2xNcnnVulkanNoiseLevel NoiseLevel { get; set; } = Waifu2xNcnnVulkanNoiseLevel.X0;

        /// <summary>
        /// 스케일
        /// </summary>
        public Waifu2xNcnnVulkanScale Scale { get; set; } = Waifu2xNcnnVulkanScale.X2;

        /// <summary>
        /// tile size 32 >=, 0 == auto
        /// </summary>
        public int TileSize { get; set; } = 0;

        /// <summary>
        /// 모델
        /// </summary>
        public string? Model { get; set; } = Waifu2xNcnnVulkanModels.Cunet;

        /// <summary>
        /// 장치
        /// </summary>
        public Waifu2xNcnnVulkanDevice Device { get; set; } = Waifu2xNcnnVulkanDevice.Auto;

        /// <summary>
        /// 로드 스레드 수
        /// </summary>
        public int? LoadThread { get; set; } = null;
        /// <summary>
        /// 업스케일링 스레드 수
        /// </summary>
        public int? UpcalingThread { get; set; } = null;
        /// <summary>
        /// 인코딩 스레드 수
        /// </summary>
        public int? EncodingThread { get; set; } = null;

        /// <summary>
        /// tta mode
        /// </summary>
        public bool TTAMode {  get; set; } = false;

        public Waifu2xNcnnVulkanFormat Format { get; set; } = Waifu2xNcnnVulkanFormat.Png;

        public string[] GetParameter() {
            if (Source == null || Source.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(Source));
            else if (Target == null || Target.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(Target));
            else if (Model == null || Model.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(Model));
            else if (TileSize != 0 && TileSize < 32) throw new ArgumentOutOfRangeException(nameof(TileSize));
            if (LoadThread == 0) LoadThread = null;
            if (UpcalingThread == 0) UpcalingThread = null;
            if (EncodingThread == 0) EncodingThread = null;
            var p = Environment.ProcessorCount;
            return new string[] { 
                "-v",
                "-i", Source, 
                "-o", Target, 
                "-n", ((int)NoiseLevel).S(), 
                "-s", ((int)Scale).S(),
                "-t", TileSize.S(),
                "-m", Model,
                "-g", Device != 0 ? ((int)Device).S() : "auto",
                "-j", $"{LoadThread ?? p}:{UpcalingThread ?? p}:{EncodingThread ?? p}",
                TTAMode ? "-x" : "",
                "-f", Format.S().ToLower()
            };
        }
        
        public object Clone() => MemberwiseClone();
    }

    /// <summary>
    /// 포멧
    /// </summary>
    public enum Waifu2xNcnnVulkanFormat {
        Jpg = -1,
        Png = 0,
        WebP = 1
    }

    /// <summary>
    /// 디바이스
    /// </summary>
    public enum Waifu2xNcnnVulkanDevice {
        /// <summary>CPU</summary>
        CPU = -1,
        /// <summary>Auto</summary>
        Auto = 0,
        /// <summary>Gpu0</summary>
        GPU0 = 1,
        /// <summary>Gpu1</summary>
        GPU1 = 2,
        /// <summary>Gpu2</summary>
        GPU2 = 3,
        /// <summary>Gpu3</summary>
        GPU3 = 4,
        /// <summary>Gpu4</summary>
        GPU4 = 5,
        /// <summary>Gpu5</summary>
        GPU5 = 6,
        /// <summary>Gpu6</summary>
        GPU6 = 7,
        /// <summary>Gpu7</summary>
        GPU7 = 8,
        /// <summary>Gpu8</summary>
        GPU8 = 9,
        /// <summary>Gpu9</summary>
        GPU9 = 10,
        /// <summary>Gpu10</summary>
        GPU10 = 11,
        /// <summary>Gpu11</summary>
        GPU11 = 12,
        /// <summary>Gpu12</summary>
        GPU12 = 13,
        /// <summary>Gpu13</summary>
    }

    /// <summary>
    /// 노이즈 레벨
    /// </summary>
    public enum Waifu2xNcnnVulkanNoiseLevel {
        /// <summary>비활성화</summary>
        Disabled = -1,
        /// <summary>0x</summary>
        X0 = 0,
        /// <summary>1x</summary>
        X1 = 1, 
        /// <summary>2x</summary>
        X2 = 2, 
        /// <summary>3x</summary>
        X3 = 3
    }

    /// <summary>
    /// 스케일
    /// </summary>
    public enum Waifu2xNcnnVulkanScale {
        /// <summary>1x</summary>
        X1 = 1,
        /// <summary>2x</summary>
        X2 = 2,
        /// <summary>4x</summary>
        X4 = 4,
        /// <summary>8x</summary>
        X8 = 8,
        /// <summary>16x</summary>
        X16 = 16,
        /// <summary>32x</summary>
        X32 = 32
    }
}
