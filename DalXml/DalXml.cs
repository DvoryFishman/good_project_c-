using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    using DalApi;
    using System;

    namespace Dal;

    // שימוש ב-sealed כדי למנוע ירושה
    public sealed class DalXml : IDal
    {
        // 1. יצירת המופע היחיד של המחלקה באופן סטטי (Thread-safe)
        private static readonly DalXml instance = new DalXml();

        // 2. מאפיין ציבורי סטטי שדרכו ניגשים למופע היחיד
        public static DalXml Instance { get { return instance; } }

        // 3. בנאי פרטי כדי למנוע יצירת מופע חדש מבחוץ (new DalXml())
        private DalXml() { }

        // --- מימוש תתי הממשקים כפי שנדרש ---

        public IProduct Product { get; } = new ProductImplementation();

        public ICustomer Customer { get; } = new CustomerImplementation();

        public ISalies Salies { get; } = new SaliesImplementation();
    }
}
