using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace DalXml
{
    internal class Config
    {
       
        // נתיב לקובץ ה-config שלך (ממוגר יחסית לתיקיית ההרצה של האפליקציה)
        private static string s_config_xml;

        static Config()
        {
            var xmlDir = FindXmlDirectory();
            s_config_xml = Path.Combine(xmlDir, "data-config.xml");

            // ensure directory and default config exist
            var dir = Path.GetDirectoryName(s_config_xml);
            if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);

            if (!File.Exists(s_config_xml))
            {
                var root = new XElement("Config",
                    new XElement("ProductNum", 10000),
                    new XElement("SaleNum", 3000)
                );
                root.Save(s_config_xml);
            }
        }

        private static string FindXmlDirectory()
        {
            var dir = AppContext.BaseDirectory;
            for (int i = 0; i < 8; i++)
            {
                var candidate = Path.GetFullPath(Path.Combine(dir, "..", "xml"));
                if (Directory.Exists(candidate))
                {
                    if (File.Exists(Path.Combine(candidate, "data-config.xml")) || File.Exists(Path.Combine(candidate, "products.xml")))
                        return candidate;
                }
                dir = Path.GetFullPath(Path.Combine(dir, ".."));
            }

            return Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "xml"));
        }

        public static int NextProductNum
        {
            get
            {
                // 1. טעינת הקובץ (Deserialize של הערך הבודד)
                XElement root = XElement.Load(s_config_xml);

                // 2. שליפת הערך הקיים בתגית <ProductNum>
                int currentNum = (int)root.Element("ProductNum");

                // 3. עדכון הקובץ למספר הבא (Increment)
                root.Element("ProductNum").SetValue(currentNum + 1);

                // 4. שמירה (Serialize של הערך המעודכן)
                root.Save(s_config_xml);

                // 5. החזרת המספר המקורי לשימוש ב-ID של האובייקט החדש
                return currentNum;
            }
        }

        public static int NextSaleNum
        {
            get
            {
                XElement root = XElement.Load(s_config_xml);
                int currentNum = (int)root.Element("SaleNum");

                root.Element("SaleNum").SetValue(currentNum + 1);
                root.Save(s_config_xml);

                return currentNum;
            }
        }

    }
}
