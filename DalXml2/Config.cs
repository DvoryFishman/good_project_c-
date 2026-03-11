using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Dal
{
    internal class Config
    {

        const string file="data-config";
        private int ProductNum;
        private int SaleNum;

        XElement fileXmlName = XElement.Load(file);

        int currentProductNum = int.Parse(fileXmlName.Element("ProductNum"));
        fileXmlName.Element('ProductNum').setValue(currentProductNum+1);

        currentSaleNum=fileXmlName.Element('SaleNum').value();
        fileXmlName.Element('SaleNum').setValue(currentSaleNum+1);

        fileXmlName.Save();
    }
}
