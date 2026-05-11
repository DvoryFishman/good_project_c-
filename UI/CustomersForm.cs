using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using BL.BlApi;
using BL.BO;

namespace UI;

public class CustomersForm : Form
{
    private readonly IBlManager _bl = Factory.Get;
    private readonly DataGridView _dgv = new() { Dock = DockStyle.Top, Height = 350, AutoGenerateColumns = true };
    private readonly TextBox _txtFilter = new() { Top = 370, Left = 20, Width = 300, Font = new Font("Segoe UI", 10F) };
    private readonly Button _btnFilter = new() { Text = "Filter by name", Top = 368, Left = 330 };
    private readonly Button _btnAdd = new() { Text = "Add", Top = 420, Left = 20 };
    private readonly Button _btnEdit = new() { Text = "Edit", Top = 420, Left = 130 };
    private readonly Button _btnDelete = new() { Text = "Delete", Top = 420, Left = 240 };
    private DataGridView dataGridViewCustomers;
    private Button button1;
    private TextBox textBox1;
    private Button button2;
    private Button button3;
    private Button button4;

    private readonly BindingSource _bs = new();

    public CustomersForm()
    {
        Text = "Customers Management";
        Width = 900;
        Height = 550;
        Controls.AddRange(new Control[] { _dgv, _txtFilter, _btnFilter, _btnAdd, _btnEdit, _btnDelete });

        // --- פלטת צבעים בוטיק ---
        Color beigeBg = Color.FromArgb(248, 245, 235);
        Color softBrown = Color.FromArgb(181, 142, 98);
        Color darkBrown = Color.FromArgb(64, 44, 29);
        this.BackColor = beigeBg;

        // --- עיצוב טבלה (DataGridView) ---
        _dgv.BackgroundColor = Color.White;
        _dgv.BorderStyle = BorderStyle.None;
        _dgv.EnableHeadersVisualStyles = false;
        _dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dgv.MultiSelect = false;
        _dgv.GridColor = Color.FromArgb(230, 230, 230);
        _dgv.RowTemplate.Height = 35;

        // עיצוב כותרות הטבלה
        _dgv.ColumnHeadersDefaultCellStyle.BackColor = softBrown;
        _dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        _dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        _dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = softBrown;
        _dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        _dgv.ColumnHeadersHeight = 40;

        // עיצוב שורות
        _dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(225, 215, 198);
        _dgv.DefaultCellStyle.SelectionForeColor = darkBrown;
        _dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9F);

        // --- עיצוב כפתורים עם "פדינג" ויזואלי ---
        // הגדלת הכפתורים ושימוש ב-Flat Design
        ConfigureButtonStyle(_btnFilter, softBrown, Color.White, new Size(130, 35));
        ConfigureButtonStyle(_btnAdd, Color.FromArgb(144, 190, 109), Color.White, new Size(100, 45));
        ConfigureButtonStyle(_btnEdit, Color.FromArgb(210, 180, 140), darkBrown, new Size(100, 45));
        ConfigureButtonStyle(_btnDelete, Color.FromArgb(230, 120, 120), Color.White, new Size(100, 45));

        _dgv.DataSource = _bs;
        _btnFilter.Click += (_, _) => LoadData();
        _btnAdd.Click += (_, _) => AddCustomer();
        _btnEdit.Click += (_, _) => EditCustomer();
        _btnDelete.Click += (_, _) => DeleteCustomer();

        LoadData();
    }

    // פונקציית עזר לעיצוב כפתור (מדמה פדינג ע"י גודל ויישור)
    private void ConfigureButtonStyle(Button btn, Color backColor, Color foreColor, Size size)
    {
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.BackColor = backColor;
        btn.ForeColor = foreColor;
        btn.Size = size;
        btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btn.Cursor = Cursors.Hand;
    }

    private void LoadData()
    {
        var filterText = _txtFilter.Text?.Trim() ?? "";
        Func<Customer, bool>? filter = string.IsNullOrEmpty(filterText)
            ? null
            : new Func<Customer, bool>(c => (c.Name ?? "").IndexOf(filterText, StringComparison.OrdinalIgnoreCase) >= 0);

        var list = _bl.Customer.ReadAll(filter).ToList();
        _bs.DataSource = new BindingList<Customer>(list);
        _bs.ResetBindings(false);
    }

    private void AddCustomer()
    {
        var name = Prompt("Name:");
        if (name == null) return;
        var address = Prompt("Address:") ?? "";
        var phone = Prompt("Phone:") ?? "";
        try
        {
            _bl.Customer.Create(new Customer(0, name, address, phone));
            LoadData();
        }
        catch (Exception e) { MessageBox.Show($"Create failed: {e.Message}"); }
    }

    private void EditCustomer()
    {
        if (_dgv.CurrentRow?.DataBoundItem is not Customer c) return;
        var name = Prompt("Name:", c.Name) ?? c.Name;
        var address = Prompt("Address:", c.Address) ?? c.Address;
        var phone = Prompt("Phone:", c.Phone) ?? c.Phone;
        try
        {
            _bl.Customer.Update(new Customer(c.CustomerId, name, address, phone));
            LoadData();
        }
        catch (Exception e) { MessageBox.Show($"Update failed: {e.Message}"); }
    }

    private void DeleteCustomer()
    {
        if (_dgv.CurrentRow?.DataBoundItem is not Customer c) return;
        if (MessageBox.Show($"Delete customer {c.CustomerId}?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
        try
        {
            _bl.Customer.Delete(c.CustomerId);
            LoadData();
        }
        catch (Exception e) { MessageBox.Show($"Delete failed: {e.Message}"); }
    }

    private void InitializeComponent()
    {
        dataGridViewCustomers = new DataGridView();
        button1 = new Button();
        textBox1 = new TextBox();
        button2 = new Button();
        button3 = new Button();
        button4 = new Button();
        ((ISupportInitialize)dataGridViewCustomers).BeginInit();
        SuspendLayout();

        // החלת אותו סגנון על כפתורי ה-Designer אם הם בשימוש
        Color softBrown = Color.FromArgb(181, 142, 98);

        dataGridViewCustomers.Location = new Point(150, 43);
        dataGridViewCustomers.Name = "dataGridViewCustomers";
        dataGridViewCustomers.Size = new Size(300, 188);
        dataGridViewCustomers.TabIndex = 0;

        button1.Location = new Point(655, 115);
        button1.Size = new Size(110, 40); // פדינג ויזואלי
        button1.FlatStyle = FlatStyle.Flat;
        button1.BackColor = softBrown;
        button1.ForeColor = Color.White;
        button1.Text = "חפש";
        button1.Click += button1_Click_1;

        button2.Location = new Point(736, 426);
        button2.Size = new Size(100, 40);
        button2.FlatStyle = FlatStyle.Flat;
        button2.Text = "עריכה";
        button2.Click += button2_Click;

        button3.Location = new Point(607, 426);
        button3.Size = new Size(100, 40);
        button3.FlatStyle = FlatStyle.Flat;
        button3.Text = "הוספה";
        button3.Click += button3_Click;

        button4.Location = new Point(482, 426);
        button4.Size = new Size(100, 40);
        button4.FlatStyle = FlatStyle.Flat;
        button4.Text = "מחיקה";
        button4.Click += button4_Click;

        Controls.Add(button4);
        Controls.Add(button3);
        Controls.Add(button2);
        Controls.Add(textBox1);
        Controls.Add(button1);
        Controls.Add(dataGridViewCustomers);
        Name = "CustomersForm";
        ((ISupportInitialize)dataGridViewCustomers).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private static string? Prompt(string caption, string? defaultValue = "")
    {
        var form = new Form { Width = 400, Height = 160, Text = caption, StartPosition = FormStartPosition.CenterParent, BackColor = Color.FromArgb(248, 245, 235) };
        var tb = new TextBox { Left = 20, Top = 20, Width = 340, Text = defaultValue ?? "", Font = new Font("Segoe UI", 10F) };
        var ok = new Button { Text = "OK", Left = 180, Width = 80, Height = 35, Top = 65, DialogResult = DialogResult.OK, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(181, 142, 98), ForeColor = Color.White };
        var cancel = new Button { Text = "Cancel", Left = 280, Width = 80, Height = 35, Top = 65, DialogResult = DialogResult.Cancel, FlatStyle = FlatStyle.Flat };
        form.Controls.AddRange(new Control[] { tb, ok, cancel });
        return form.ShowDialog() == DialogResult.OK ? tb.Text : null;
    }

    private void button2_Click(object? sender, EventArgs e) => EditCustomer();
    private void button3_Click(object? sender, EventArgs e) => AddCustomer();
    private void button4_Click(object? sender, EventArgs e) => DeleteCustomer();
    private void button1_Click_1(object? sender, EventArgs e) => LoadData();
    private void dataGridViewCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
}