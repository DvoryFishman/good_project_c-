using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using BL.BlApi;
using BL.BO;

namespace UI
{
    public partial class OrdersForm : Form
    {
        private readonly IBlManager _bl = Factory.Get;
        private Order _currentOrder = new Order { ProductsList = new List<ProductInOrder>(), ClientId = -1 };

        // פקדים
        private DataGridView dataGridViewCart;
        private ComboBox comboBoxProducts;
        private TextBox textBoxSearch;
        private NumericUpDown numericUpDownQty;
        private Button btnAddToCart;
        private Button btnFinalize;
        private CheckBox checkBoxInStockOnly;
        private Label lblTotal;
        private Label lblSearchTitle;

        public OrdersForm()
        {
            // הסירי את InitializeComponent אם הוא ריק או גורם לשגיאה
            InitializeCustomComponents();
            LoadInitialData();
            RefreshOrderView();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "מערכת הזמנות - קופה";
            this.Size = new Size(1000, 600);
            this.RightToLeft = RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = FormStartPosition.CenterScreen;

            lblSearchTitle = new Label { Text = "חפש מוצר (שם או קוד):", Location = new Point(20, 20), AutoSize = true };
            textBoxSearch = new TextBox { Location = new Point(20, 45), Width = 200 };
            textBoxSearch.TextChanged += TextBoxSearch_TextChanged;

            comboBoxProducts = new ComboBox { Location = new Point(230, 45), Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };

            checkBoxInStockOnly = new CheckBox { Text = "מוצרים במלאי בלבד", Location = new Point(500, 47), AutoSize = true, Checked = false };
            checkBoxInStockOnly.CheckedChanged += CheckBoxInStockOnly_CheckedChanged;

            numericUpDownQty = new NumericUpDown { Location = new Point(650, 46), Width = 60, Minimum = 1, Value = 1 };

            btnAddToCart = new Button { Text = "הוסף לסל", Location = new Point(720, 42), Width = 100, BackColor = Color.LightGreen };
            btnAddToCart.Click += BtnAddToCart_Click;

            dataGridViewCart = new DataGridView
            {
                Location = new Point(20, 100),
                Size = new Size(940, 350),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true,
                AllowUserToAddRows = false,
                BackgroundColor = Color.White,
                RowHeadersVisible = false
            };

            lblTotal = new Label { Text = "סה\"כ לתשלום: 0.00 ₪", Location = new Point(20, 470), Size = new Size(400, 30), Font = new Font("Arial", 14, FontStyle.Bold) };

            btnFinalize = new Button { Text = "בצע הזמנה ועדכן מלאי", Location = new Point(760, 470), Width = 200, Height = 40, BackColor = Color.LightSkyBlue };
            btnFinalize.Click += BtnFinalize_Click;

            this.Controls.AddRange(new Control[] {
                lblSearchTitle, textBoxSearch, comboBoxProducts, checkBoxInStockOnly,
                numericUpDownQty, btnAddToCart, dataGridViewCart, lblTotal, btnFinalize
            });
        }

        private void LoadInitialData()
        {
            try
            {
                var products = _bl.Product.ReadAll();
                UpdateProductList(products);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"שגיאה בטעינת מוצרים: {ex.Message}");
            }
        }

        private void UpdateProductList(IEnumerable<Product> products)
        {
            comboBoxProducts.DataSource = null; // ניקוי מקור נתונים קודם
            if (products != null && products.Any())
            {
                comboBoxProducts.DataSource = products.ToList();
                comboBoxProducts.DisplayMember = "Name";
                comboBoxProducts.ValueMember = "ProductId";
            }
            else
            {
                // הודעה שקטה או לוג במקום MessageBox שמציק למשתמש בכל הקלדה
                System.Diagnostics.Debug.WriteLine("רשימת מוצרים ריקה");
            }
        }

        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string search = textBoxSearch.Text.ToLower();
                var allProducts = _bl.Product.ReadAll();
                var filtered = allProducts.Where(p =>
                    (p.ToString() != null && p.ToString().ToLower().Contains(search)) ||
                    p.ProductId.ToString().Contains(search));

                UpdateProductList(filtered);
            }
            catch { }
        }

        private void CheckBoxInStockOnly_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                var products = _bl.Product.ReadAll();
                if (checkBoxInStockOnly.Checked)
                {
                    products = products.Where(p => p.QuantityInStock > 0).ToList();
                }
                UpdateProductList(products);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void BtnAddToCart_Click(object sender, EventArgs e)
        {
            if (comboBoxProducts.SelectedItem is Product selectedProduct)
            {
                try
                {
                    int qty = (int)numericUpDownQty.Value;

                    // כאן התיקון: במקום לשלוח ל-Salies (שהוא ריק), שלחי ל-Order
                    _bl.Order.AddProductToOrder(_currentOrder, selectedProduct.ProductId, qty);

                    RefreshOrderView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"שגיאה: {ex.Message}");
                }
            }
        }

        private void RefreshOrderView()
        {
            dataGridViewCart.DataSource = null;
            if (_currentOrder.ProductsList != null)
            {
                dataGridViewCart.DataSource = _currentOrder.ProductsList.ToList();

                // עיצוב כותרות
                if (dataGridViewCart.Columns["ProductId"] != null) dataGridViewCart.Columns["ProductId"].HeaderText = "קוד מוצר";
                if (dataGridViewCart.Columns["Name"] != null) dataGridViewCart.Columns["Name"].HeaderText = "שם מוצר";
                if (dataGridViewCart.Columns["Amount"] != null) dataGridViewCart.Columns["Amount"].HeaderText = "כמות";
                if (dataGridViewCart.Columns["Price"] != null) dataGridViewCart.Columns["Price"].HeaderText = "מחיר יחידה";
                if (dataGridViewCart.Columns["TotalPrice"] != null) dataGridViewCart.Columns["TotalPrice"].HeaderText = "סה\"כ שורה";

                // הסתרת עמודות מיותרות אם קיימות ב-BO
                if (dataGridViewCart.Columns["Sales"] != null) dataGridViewCart.Columns["Sales"].Visible = false;
            }

            lblTotal.Text = $"סה\"כ לתשלום: {_currentOrder.TotalPrice:N2} ₪";
        }

        private void BtnFinalize_Click(object sender, EventArgs e)
        {
            if (_currentOrder.ProductsList.Count == 0) return;

            try
            {
                // כאן התיקון: קריאה ל-Order ולא ל-Salies
                _bl.Order.DoOrder(_currentOrder);

                MessageBox.Show("ההזמנה בוצעה בהצלחה!");
                _currentOrder = new Order { ProductsList = new List<ProductInOrder>(), ClientId = -1 };
                RefreshOrderView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"שגיאה בסיום הזמנה: {ex.Message}");
            }
        }
    }
}