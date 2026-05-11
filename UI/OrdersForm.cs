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
            InitializeCustomComponents();
            LoadInitialData();
            RefreshOrderView();
        }

        private void InitializeCustomComponents()
        {
            // --- פלטת צבעים ---
            Color beigeBg = Color.FromArgb(248, 245, 235);
            Color softBrown = Color.FromArgb(181, 142, 98);
            Color darkBrown = Color.FromArgb(64, 44, 29);
            Color accentGreen = Color.FromArgb(144, 190, 109);

            this.Text = "Boutique - מערכת הזמנות";
            this.Size = new Size(1050, 650);
            this.RightToLeft = RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = beigeBg;

            // כותרת חיפוש
            lblSearchTitle = new Label
            {
                Text = "חיפוש מוצר:",
                Location = new Point(20, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = darkBrown
            };

            // תיבת חיפוש
            textBoxSearch = new TextBox
            {
                Location = new Point(20, 45),
                Width = 200,
                Font = new Font("Segoe UI", 11F)
            };
            textBoxSearch.TextChanged += TextBoxSearch_TextChanged;

            // בחירת מוצר
            comboBoxProducts = new ComboBox
            {
                Location = new Point(235, 45),
                Width = 250,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 11F),
                FlatStyle = FlatStyle.Flat
            };

            // צ'קבוקס מלאי
            checkBoxInStockOnly = new CheckBox
            {
                Text = "מוצרים במלאי בלבד",
                Location = new Point(500, 47),
                AutoSize = true,
                Checked = false,
                Font = new Font("Segoe UI", 9F)
            };
            checkBoxInStockOnly.CheckedChanged += CheckBoxInStockOnly_CheckedChanged;

            // כמות
            numericUpDownQty = new NumericUpDown
            {
                Location = new Point(650, 46),
                Width = 70,
                Minimum = 1,
                Value = 1,
                Font = new Font("Segoe UI", 11F),
                TextAlign = HorizontalAlignment.Center
            };

            // כפתור הוספה לסל (עם פדינג ויזואלי)
            btnAddToCart = new Button
            {
                Text = "הוסף לסל",
                Location = new Point(735, 40),
                Width = 130,
                Height = 40,
                BackColor = accentGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnAddToCart.FlatAppearance.BorderSize = 0;
            btnAddToCart.Click += BtnAddToCart_Click;

            // --- עיצוב טבלת עגלת הקניות ---
            dataGridViewCart = new DataGridView
            {
                Location = new Point(20, 100),
                Size = new Size(990, 350),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true,
                AllowUserToAddRows = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                EnableHeadersVisualStyles = false,
                GridColor = Color.FromArgb(235, 235, 235),
                RowTemplate = { Height = 40 }
            };
            dataGridViewCart.ColumnHeadersDefaultCellStyle.BackColor = softBrown;
            dataGridViewCart.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewCart.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCart.ColumnHeadersHeight = 45;

            // סה"כ לתשלום
            lblTotal = new Label
            {
                Text = "סה\"כ לתשלום: 0.00 ₪",
                Location = new Point(20, 480),
                Size = new Size(400, 40),
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = darkBrown
            };

            // כפתור סיום הזמנה
            btnFinalize = new Button
            {
                Text = "בצע הזמנה ועדכן מלאי",
                Location = new Point(760, 480),
                Width = 250,
                Height = 50,
                BackColor = softBrown,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnFinalize.FlatAppearance.BorderSize = 0;
            btnFinalize.Click += BtnFinalize_Click;

            this.Controls.AddRange(new Control[] {
                lblSearchTitle, textBoxSearch, comboBoxProducts, checkBoxInStockOnly,
                numericUpDownQty, btnAddToCart, dataGridViewCart, lblTotal, btnFinalize
            });
        }

        // --- שאר הלוגיקה נשארת ללא שינוי בכלל ---

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
            comboBoxProducts.DataSource = null;
            if (products != null && products.Any())
            {
                comboBoxProducts.DataSource = products.ToList();
                comboBoxProducts.DisplayMember = "Name";
                comboBoxProducts.ValueMember = "ProductId";
            }
            else
            {
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

                if (dataGridViewCart.Columns["ProductId"] != null) dataGridViewCart.Columns["ProductId"].HeaderText = "קוד";
                if (dataGridViewCart.Columns["Name"] != null) dataGridViewCart.Columns["Name"].HeaderText = "שם מוצר";
                if (dataGridViewCart.Columns["Amount"] != null) dataGridViewCart.Columns["Amount"].HeaderText = "כמות";
                if (dataGridViewCart.Columns["Price"] != null) dataGridViewCart.Columns["Price"].HeaderText = "מחיר";
                if (dataGridViewCart.Columns["TotalPrice"] != null) dataGridViewCart.Columns["TotalPrice"].HeaderText = "סה\"כ";

                if (dataGridViewCart.Columns["Sales"] != null) dataGridViewCart.Columns["Sales"].Visible = false;
            }

            lblTotal.Text = $"סה\"כ לתשלום: {_currentOrder.TotalPrice:N2} ₪";
        }

        private void BtnFinalize_Click(object sender, EventArgs e)
        {
            if (_currentOrder.ProductsList.Count == 0) return;

            try
            {
                _bl.Order.DoOrder(_currentOrder);
                MessageBox.Show("ההזמנה בוצעה בהצלחה!", "Boutique Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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