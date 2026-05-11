using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalXml
{
    internal class Config
    {
       
        // נתיב לקובץ ה-config שלך
        private static string s_config_xml = @"..\xml\data-config.xml";

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
