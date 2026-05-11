using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BL.BlApi;
using BL.BO;

namespace UI
{
    public partial class ProductForm : Form
    {
        private readonly IBlManager _bl = Factory.Get;

        private DataGridView dataGridViewProducts;
        private TextBox textBoxSearch;
        private CheckBox checkBoxInStockOnly;
        private Button btnRefresh;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private TextBox txtName;
        private TextBox txtPrice;
        private NumericUpDown numQty;
        private ComboBox comboBoxCategory;
        private Label lblTotalCount;

        // צבעי עיצוב - גווני בז' וחום
        private readonly Color COLOR_BG = Color.FromArgb(248, 245, 235);      // רקע בז' בהיר מאוד
        private readonly Color COLOR_ACCENT = Color.FromArgb(210, 180, 140);  // בז' כהה/חום עדין
        private readonly Color COLOR_DARK_BROWN = Color.FromArgb(70, 50, 40); // חום כהה לטקסט
        private readonly Color COLOR_INPUT_BG = Color.FromArgb(255, 253, 248); // קרם לתיבות טקסט

        private List<Product> _currentList = new List<Product>();

        public ProductForm()
        {
            InitializeCustomComponents();
            LoadProducts();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "ניהול מוצרים - בוטיק";
            this.Size = new Size(1000, 680);
            this.RightToLeft = RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.BackColor = COLOR_BG;
            this.Font = new Font("Segoe UI", 10F);

            // Search Area
            var lblSearch = new Label { Text = "חפש מוצר:", Location = new Point(25, 25), AutoSize = true, ForeColor = COLOR_DARK_BROWN, Font = new Font("Segoe UI", 11F, FontStyle.Bold) };
            textBoxSearch = new TextBox { Location = new Point(25, 50), Width = 300, BackColor = COLOR_INPUT_BG, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 12F) };
            textBoxSearch.TextChanged += TextBoxSearch_TextChanged;

            checkBoxInStockOnly = new CheckBox { Text = "במלאי בלבד", Location = new Point(340, 52), AutoSize = true, ForeColor = COLOR_DARK_BROWN };
            checkBoxInStockOnly.CheckedChanged += FilterControls_Changed;

            btnRefresh = new Button { Text = "רענון", Location = new Point(460, 45), Width = 110 };
            btnRefresh.Click += (s, e) => LoadProducts();

            // Grid
            dataGridViewProducts = new DataGridView
            {
                Location = new Point(25, 100),
                Size = new Size(935, 350),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true,
                AllowUserToAddRows = false,
                BackgroundColor = COLOR_INPUT_BG,
                BorderStyle = BorderStyle.None
            };
            StyleDataGridView(dataGridViewProducts);
            dataGridViewProducts.SelectionChanged += DataGridViewProducts_SelectionChanged;

            // Editor Panel
            var panelEditor = new Panel { Location = new Point(25, 470), Size = new Size(935, 150), BackColor = Color.Transparent };

            var lblName = new Label { Text = "שם מוצר:", Location = new Point(10, 10), AutoSize = true, ForeColor = COLOR_DARK_BROWN };
            txtName = new TextBox { Location = new Point(10, 35), Width = 250, BackColor = COLOR_INPUT_BG, Font = new Font("Segoe UI", 11F) };

            var lblPrice = new Label { Text = "מחיר:", Location = new Point(280, 10), AutoSize = true, ForeColor = COLOR_DARK_BROWN };
            txtPrice = new TextBox { Location = new Point(280, 35), Width = 100, BackColor = COLOR_INPUT_BG, Font = new Font("Segoe UI", 11F) };

            var lblCategory = new Label { Text = "קטגוריה:", Location = new Point(400, 10), AutoSize = true, ForeColor = COLOR_DARK_BROWN };
            comboBoxCategory = new ComboBox { Location = new Point(400, 35), Width = 140, DropDownStyle = ComboBoxStyle.DropDownList, BackColor = COLOR_INPUT_BG };
            comboBoxCategory.DataSource = Enum.GetValues(typeof(Category)).Cast<Category>().ToList();

            var lblQty = new Label { Text = "מלאי:", Location = new Point(560, 10), AutoSize = true, ForeColor = COLOR_DARK_BROWN };
            numQty = new NumericUpDown { Location = new Point(560, 35), Width = 80, BackColor = COLOR_INPUT_BG };

            // Buttons - Big and Clean
            btnAdd = new Button { Text = "הוסף מוצר", Location = new Point(10, 85) };
            btnUpdate = new Button { Text = "עדכן מוצר", Location = new Point(170, 85) };
            btnDelete = new Button { Text = "מחק מוצר", Location = new Point(330, 85) };

            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;

            lblTotalCount = new Label { Text = "סה\"כ: 0", Location = new Point(800, 100), AutoSize = true, ForeColor = COLOR_DARK_BROWN, Font = new Font("Segoe UI", 10F, FontStyle.Italic) };

            // Apply modern styles to buttons
            StyleModernButton(btnAdd, Color.FromArgb(181, 201, 154));   // ירוק מרווה עדין
            StyleModernButton(btnUpdate, COLOR_ACCENT);                 // בז' מוקה
            StyleModernButton(btnDelete, Color.FromArgb(217, 174, 174)); // ורוד עתיק/אדום רך
            StyleModernButton(btnRefresh, Color.FromArgb(200, 190, 180));

            panelEditor.Controls.AddRange(new Control[] { lblName, txtName, lblPrice, txtPrice, lblCategory, comboBoxCategory, lblQty, numQty, btnAdd, btnUpdate, btnDelete, lblTotalCount });

            this.Controls.AddRange(new Control[] { lblSearch, textBoxSearch, checkBoxInStockOnly, btnRefresh, dataGridViewProducts, panelEditor });

            // Final recursion to ensure all fonts are clean
            ApplyGlobalDesign(this);
        }

        private void StyleModernButton(Button btn, Color baseColor)
        {
            btn.Size = new Size(150, 50);
            btn.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = baseColor;
            btn.ForeColor = COLOR_DARK_BROWN;
            btn.Cursor = Cursors.Hand;
            btn.Padding = new Padding(5);
        }

        private void StyleDataGridView(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.RowTemplate.Height = 40;
            dgv.RowHeadersVisible = false;
            dgv.GridColor = Color.FromArgb(235, 230, 220);
            dgv.DefaultCellStyle.BackColor = COLOR_INPUT_BG;
            dgv.DefaultCellStyle.SelectionBackColor = COLOR_ACCENT;
            dgv.DefaultCellStyle.SelectionForeColor = COLOR_DARK_BROWN;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = COLOR_ACCENT;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = COLOR_DARK_BROWN;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 45;
        }

        private void ApplyGlobalDesign(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is Label lbl) lbl.ForeColor = COLOR_DARK_BROWN;
                if (c is TextBox || c is ComboBox || c is NumericUpDown)
                {
                    c.Font = new Font("Segoe UI", 11F);
                }
                if (c.HasChildren) ApplyGlobalDesign(c);
            }
        }

        // --- לוגיקה (לא שונתה) ---

        private void LoadProducts()
        {
            try
            {
                var products = _bl.Product.ReadAll();
                _currentList = products ?? new List<Product>();
                ApplyFiltersAndBind();
            }
            catch (Exception ex) { MessageBox.Show($"שגיאה: {ex.Message}"); }
        }

        private void ApplyFiltersAndBind()
        {
            IEnumerable<Product> filtered = _currentList;
            var search = textBoxSearch.Text?.Trim().ToLower();
            if (!string.IsNullOrEmpty(search))
                filtered = filtered.Where(p => p.ToString().ToLower().Contains(search) || p.ProductId.ToString().Contains(search));

            if (checkBoxInStockOnly.Checked)
                filtered = filtered.Where(p => p.QuantityInStock > 0);

            var list = filtered.ToList();
            dataGridViewProducts.DataSource = null;
            dataGridViewProducts.DataSource = list;

            if (dataGridViewProducts.Columns["ProductId"] != null) dataGridViewProducts.Columns["ProductId"].HeaderText = "קוד";
            if (dataGridViewProducts.Columns["Price"] != null) dataGridViewProducts.Columns["Price"].HeaderText = "מחיר";
            if (dataGridViewProducts.Columns["QuantityInStock"] != null) dataGridViewProducts.Columns["QuantityInStock"].HeaderText = "מלאי";

            lblTotalCount.Text = $"סה\"כ מוצרים: {list.Count}";
        }

        private void TextBoxSearch_TextChanged(object sender, EventArgs e) => ApplyFiltersAndBind();
        private void FilterControls_Changed(object sender, EventArgs e) => ApplyFiltersAndBind();

        private void DataGridViewProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count > 0 && dataGridViewProducts.SelectedRows[0].DataBoundItem is Product row)
            {
                txtName.Text = row.ToString();
                txtPrice.Text = row.Price.ToString();
                numQty.Value = Math.Min(numQty.Maximum, Math.Max(numQty.Minimum, row.QuantityInStock));
                try { comboBoxCategory.SelectedItem = row.Category; } catch { }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var price = ParseNullableDouble(txtPrice.Text);
                if (price == null) return;
                var prod = new Product
                {
                    Price = price.Value,
                    QuantityInStock = (int)numQty.Value,
                    Category = (BL.BO.Category)(comboBoxCategory.SelectedItem ?? default(BL.BO.Category))
                }; var created = _bl.Product.Create(prod);
                LoadProducts();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count == 0) return;
            try
            {
                var selected = dataGridViewProducts.SelectedRows[0].DataBoundItem as Product;
                var price = ParseNullableDouble(txtPrice.Text);
                if (price == null || selected == null) return;
                selected.Price = price.Value;
                selected.QuantityInStock = (int)numQty.Value;
                selected.Category = (BL.BO.Category)(comboBoxCategory.SelectedItem ?? default(BL.BO.Category));
                _bl.Product.Update(selected);
                LoadProducts();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count == 0) return;
            try
            {
                var selected = dataGridViewProducts.SelectedRows[0].DataBoundItem as Product;
                if (MessageBox.Show("למחוק?", "מחיקה", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _bl.Product.Delete(selected.ProductId);
                    LoadProducts();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private double? ParseNullableDouble(string s) => double.TryParse(s, out var v) ? v : (double?)null;
    }
}