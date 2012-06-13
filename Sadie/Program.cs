using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Sadie
{
    static class Program
    {
        public static String fontFamily;
        public static bool wordWrap;
        public static String headerPath;
        public static String footerPath;
        public static String generatePath;
        public static String timestamp;
        public static float emSize;

        private static String appPath = Path.GetDirectoryName(Application.ExecutablePath);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(args));
        }

        public static void loadConfiguration()
        {
            try
            {
                
                TextReader tr = new StreamReader(appPath + "\\conf.ini");
                readConfig(tr.ReadToEnd());
                tr.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Ayarlar okunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                configDefaults();
            }
        }

        public static void saveConfiguration()
        {
            try
            {
                TextWriter tw = new StreamWriter(appPath + "\\conf.ini");
                tw.Write(generateConfig());
                tw.Close();
            }catch(Exception)
            {
                MessageBox.Show("Ayarlar kaydedilemedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void readConfig(String config)
        {
            char[] separator = { '\r', '\n'};
            String[] configs = config.Split(separator);
            fontFamily = configs[0];
            emSize = float.Parse(configs[2]);
            wordWrap = bool.Parse(configs[4]);
            headerPath = configs[6];
            footerPath = configs[8];
            generatePath = configs[10];
            timestamp = configs[12];
        }

        private static String generateConfig()
        {
            String config = "";
            config += fontFamily;
            config += "\r\n";
            config += emSize.ToString();
            config += "\r\n";
            config += wordWrap.ToString();
            config += "\r\n";
            config += headerPath;
            config += "\r\n";
            config += footerPath;
            config += "\r\n";
            config += generatePath;
            config += "\r\n";
            config += timestamp;


            return config;
        }

        private static void configDefaults()
        {
            fontFamily = "Courier New";
            emSize = (float)9.75;
            wordWrap = true;
            headerPath = appPath+"\\header.ini";
            footerPath = appPath + "\\footer.ini";
            generatePath = "/";
            timestamp = "yyyy-MM-dd HH:m";
            saveConfiguration();
        }
    }
}
