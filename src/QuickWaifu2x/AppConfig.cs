namespace QuickWaifu2x {
    internal static class AppConfig {
        static IniSection Parameter => Ini.Current["parm"];

        public static string Model {
            get => Parameter["model"].ToString() ?? Waifu2xNcnnVulkanModels.Cunet; 
            set => Parameter["model"] = value; 
        }
        
        public static Waifu2xNcnnVulkanNoiseLevel NoiseLevel { 
            get => Parameter["noiesLevel"].ToEnum(Waifu2xNcnnVulkanNoiseLevel.X0); 
            set => Parameter["noiesLevel"] = (int)value; 
        }
        
        public static Waifu2xNcnnVulkanScale Scale { 
            get => Parameter["scale"].ToEnum(Waifu2xNcnnVulkanScale.X2);
            set => Parameter["scale"] = (int)value; 
        }

        public static int TileSize {
            get => Parameter["tileSize"].ToInt(0);
            set => Parameter["tileSize"] = value;
        }

        public static bool TTAMode {
            get => Parameter["ttaMode"].ToBool();
            set => Parameter["ttaMode"] = value;
        }
        
        public static Waifu2xNcnnVulkanFormat Format { 
            get => Parameter["format"].ToEnum(Waifu2xNcnnVulkanFormat.Png);
            set => Parameter["format"] = (int)value; 
        }

        public static int LoadThread {
            get => Parameter["loadThread"].ToInt(2);
            set => Parameter["loadThread"] = value;
        }
        
        public static int UpcalingThread {
            get => Parameter["upcalingThread"].ToInt(2);
            set => Parameter["upcalingThread"] = value;
        }
        
        public static int EncodingThread {
            get => Parameter["encodingThread"].ToInt(2);
            set => Parameter["encodingThread"] = value;
        }
        public static Waifu2xNcnnVulkanDevice Device { 
            get => Parameter["device"].ToEnum(Waifu2xNcnnVulkanDevice.Auto);
            set => Parameter["device"] = (int)value; 
        }

        public static void Save() => Ini.Current.Save();

        public static Waifu2xNcnnVulkanParameter GetParameter() => new() { 
            Model = Model,
            NoiseLevel = NoiseLevel,
            Scale = Scale,
            TileSize = TileSize,
            TTAMode = TTAMode,
            LoadThread = LoadThread,
            UpcalingThread = UpcalingThread,
            EncodingThread = EncodingThread,
            Format = Format,
            Device = Device
        };
        
        public static void SetParameter(Waifu2xNcnnVulkanParameter p) {
            Model = p.Model ?? Waifu2xNcnnVulkanModels.Cunet;
            NoiseLevel = p.NoiseLevel;
            Scale = p.Scale;
            TileSize = p.TileSize;
            TTAMode = p.TTAMode;
            LoadThread = p.LoadThread ?? 0;
            UpcalingThread = p.UpcalingThread ?? 0;
            EncodingThread = p.EncodingThread ?? 0;
            Format = p.Format;
            Device = p.Device;
        }
    }
}
