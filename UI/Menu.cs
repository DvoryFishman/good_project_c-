using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DalTest1;

namespace UI
{
    public partial class Menu : Form
    {
        // הגדרת הכפתורים כמשתנים של המחלקה
        private Button btnCustomers;
        private Button btnProducts;
        private Button btnOrders;

        public Menu()
        {
            InitializeComponent();
            DalTest1.Initialization.initilize();
            InitializeCustomMenu();

            // בדיקה: אם אין מוצרים, נריץ את האתחול של ה-Console
            try
            {
                var bl = BL.BlApi.Factory.Get;
                if (!bl.Product.ReadAll().Any())
                {
                    DalTest1.Initialization.initilize();
                    MessageBox.Show("הנתונים אותחלו בהצלחה!");
                }
            }
            catch (Exception ex) { /* לוג שגיאה שקט */ }
        }

        private void InitializeCustomMenu()
        {
            // הגדרות כלליות של חלון התפריט
            this.Text = "חנות ניהול - תפריט ראשי";
            this.Width = 400;
            this.Height = 350;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;

            // יצירת כפתור ניהול לקוחות
            btnCustomers = new Button
            {
                Text = "ניהול לקוחות",
                Width = 200,
                Height = 50,
                Top = 50,
                Left = (this.ClientSize.Width - 200) / 2, // מרכז את הכפתור
                BackColor = Color.LightBlue,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            btnCustomers.Click += (s, e) => new CustomersForm().ShowDialog();

            // יצירת כפתור ניהול מוצרים
            btnProducts = new Button
            {
                Text = "ניהול מוצרים",
                Width = 200,
                Height = 50,
                Top = 120,
                Left = (this.ClientSize.Width - 200) / 2,
                BackColor = Color.LightGreen,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            btnProducts.Click += (s, e) => new ProductForm().ShowDialog();
            // יצירת כפתור קופה (הזמנות)
            btnOrders = new Button
            {
                Text = "קופה (ביצוע הזמנה)",
                Width = 200,
                Height = 50,
                Top = 190,
                Left = (this.ClientSize.Width - 200) / 2,
                BackColor = Color.LightCoral,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            btnOrders.Click += (s, e) => new OrdersForm().ShowDialog();

            // הוספת הכפתורים לטופס
            this.Controls.Add(btnCustomers);
            this.Controls.Add(btnProducts);
            this.Controls.Add(btnOrders);
        }
    }
}