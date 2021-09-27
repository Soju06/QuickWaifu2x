using System.Windows.Forms;

namespace QuickWaifu2x {
    public partial class Waifu2xParameterDialog : Form {
        public Waifu2xParameterDialog() {
            InitializeComponent();
            InitCombobox();
        }

        public void InitCombobox() {
            comboBox1.Items.AddRange(Directory.GetDirectories(Application.StartupPath).Filter(r => {
                var d = new DirectoryInfo(r);
                if (!d.Exists || d.Name.IndexOf("models-") != 0) return null;
                return d.Name;
            }).NullClear().ToArray());
            comboBox2.DataSource = Enum.GetValues(typeof(Waifu2xNcnnVulkanNoiseLevel));
            comboBox3.DataSource = Enum.GetValues(typeof(Waifu2xNcnnVulkanScale));
            comboBox4.DataSource = Enum.GetValues(typeof(Waifu2xNcnnVulkanFormat));
            comboBox5.DataSource = Enum.GetValues(typeof(Waifu2xNcnnVulkanDevice));
        }
        
        void OnLoad(object sender, EventArgs e) {
            comboBox1.SelectedItem = Parameter?.Model ?? Waifu2xNcnnVulkanModels.Cunet;
            comboBox2.SelectedItem = Parameter?.NoiseLevel ?? Waifu2xNcnnVulkanNoiseLevel.X0;
            comboBox3.SelectedItem = Parameter?.Scale ?? Waifu2xNcnnVulkanScale.X2;
            uInt32InputBox2.Number = (uint)(Parameter?.TileSize ?? 0);
            checkBox1.Checked = Parameter?.TTAMode ?? false;
            comboBox4.SelectedItem = Parameter?.Format ?? Waifu2xNcnnVulkanFormat.Png;
            uInt32InputBox1.Number = (uint)(Parameter?.EncodingThread ?? 0);
            uInt32InputBox3.Number = (uint)(Parameter?.UpcalingThread ?? 0);
            uInt32InputBox4.Number = (uint)(Parameter?.LoadThread ?? 0);
        }

        void OnClosing(object sender, FormClosingEventArgs e) {
            Parameter = new() {
                Model = comboBox1.SelectedItem.S(),
                NoiseLevel = (Waifu2xNcnnVulkanNoiseLevel)comboBox2.SelectedItem,
                Scale = (Waifu2xNcnnVulkanScale)comboBox3.SelectedItem,
                TileSize = (int)uInt32InputBox2.Number,
                TTAMode = checkBox1.Checked,
                Format = (Waifu2xNcnnVulkanFormat)comboBox4.SelectedItem,
                LoadThread = (int)uInt32InputBox4.Number,
                UpcalingThread = (int)uInt32InputBox3.Number,
                EncodingThread = (int)uInt32InputBox1.Number,
                Device = (Waifu2xNcnnVulkanDevice)comboBox5.SelectedItem
            };
        }

        public Waifu2xNcnnVulkanParameter? Parameter { get; set; }

        public static new Waifu2xNcnnVulkanParameter? ShowDialog() {
            if (Ini.Current["parm"]["ign"].ToBool())
                return AppConfig.GetParameter();
            else {
                using var dialog = new Waifu2xParameterDialog();
                dialog.Parameter = AppConfig.GetParameter();
                if (((Form)dialog).ShowDialog() != DialogResult.OK
                    || dialog.Parameter == null) return null;
                AppConfig.SetParameter(dialog.Parameter);
                AppConfig.Save();
                return dialog.Parameter;
            }
        }
    }
}
