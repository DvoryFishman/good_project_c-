using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalXml
{
    internal class Config
    {
        private fileXmlName="data-config";
        private ProductNum;
        private SaleNum;


        currentProductNum=fileXmlName.Element('ProductNum').value();
        fileXmlName.Element('ProductNum').setValue(currentProductNum+1);

        currentSaleNum=fileXmlName.Element('SaleNum').value();
        fileXmlName.Element('SaleNum').setValue(currentSaleNum+1);

        fileXmlName.Save();

    }
}
