using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal class Config
{
    private static string s_config_xml = @"..\xml\data-config.xml";

    public static int NextProductNum
    {
        get
        {
            XElement root = XElement.Load(s_config_xml);
            int currentNum = (int)root.Element("ProductNum");
            root.Element("ProductNum").SetValue(currentNum + 1);
            root.Save(s_config_xml);
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