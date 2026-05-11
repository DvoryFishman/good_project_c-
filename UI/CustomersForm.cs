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
    private readonly DataGridView _dgv = new() { Dock = DockStyle.Top, Height = 380, AutoGenerateColumns = true };
    private readonly TextBox _txtFilter = new() { Top = 390, Left = 10, Width = 300 };
    private readonly Button _btnFilter = new() { Text = "Filter by name", Top = 390, Left = 320 };
    private readonly Button _btnAdd = new() { Text = "Add", Top = 420, Left = 10 };
    private readonly Button _btnEdit = new() { Text = "Edit", Top = 420, Left = 90 };
    private readonly Button _btnDelete = new() { Text = "Delete", Top = 420, Left = 170 };
    private DataGridView dataGridViewCustomers;
    private Button button1;
    private TextBox textBox1;
    private Button button2;
    private Button button3;
    private Button button4;

    // Use a BindingSource to mediate between BL collections and the UI
    private readonly BindingSource _bs = new();

    public CustomersForm()
    {
        Text = "Customers";
        Width = 800;
        Height = 520;
        Controls.AddRange(new Control[] { _dgv, _txtFilter, _btnFilter, _btnAdd, _btnEdit, _btnDelete });

        // bind DataGridView to BindingSource
        _dgv.DataSource = _bs;

        _btnFilter.Click += (_, _) => LoadData();
        _btnAdd.Click += (_, _) => AddCustomer();
        _btnEdit.Click += (_, _) => EditCustomer();
        _btnDelete.Click += (_, _) => DeleteCustomer();

        LoadData();
    }

    private void LoadData()
    {
        var filterText = _txtFilter.Text?.Trim() ?? "";
        Func<Customer, bool>? filter = string.IsNullOrEmpty(filterText)
            ? null
            : new Func<Customer, bool>(c => (c.Name ?? "").IndexOf(filterText, StringComparison.OrdinalIgnoreCase) >= 0);

        var list = _bl.Customer.ReadAll(filter).ToList();
        var bindingList = new BindingList<Customer>(list);

        // set BindingSource.DataSource so UI updates and current item management work correctly
        _bs.DataSource = bindingList;
        _bs.ResetBindings(false);
    }

    private void AddCustomer()
    {
        var name = Prompt("Name:");
        if (name == null) return;
        var address = Prompt("Address:") ?? "";
        var phone = Prompt("Phone:") ?? "";

        var newCust = new Customer(0, name, address, phone);
        try
        {
            _bl.Customer.Create(newCust);
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

        var updated = new Customer(c.CustomerId, name, address, phone);
        try
        {
            _bl.Customer.Update(updated);
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
        // 
        // dataGridViewCustomers
        // 
        dataGridViewCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridViewCustomers.Location = new Point(150, 43);
        dataGridViewCustomers.Name = "dataGridViewCustomers";
        dataGridViewCustomers.RowHeadersWidth = 51;
        dataGridViewCustomers.Size = new Size(300, 188);
        dataGridViewCustomers.TabIndex = 0;
        dataGridViewCustomers.CellContentClick += dataGridViewCustomers_CellContentClick;
        // 
        // button1
        // 
        button1.Location = new Point(655, 115);
        button1.Name = "button1";
        button1.Size = new Size(94, 29);
        button1.TabIndex = 1;
        button1.Text = "חפש";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click_1;
        // 
        // textBox1
        // 
        textBox1.Location = new Point(645, 246);
        textBox1.Name = "textBox1";
        textBox1.Size = new Size(125, 27);
        textBox1.TabIndex = 2;
        // 
        // button2
        // 
        button2.Location = new Point(736, 426);
        button2.Name = "button2";
        button2.Size = new Size(94, 29);
        button2.TabIndex = 3;
        button2.Text = "עריכה";
        button2.UseVisualStyleBackColor = true;
        button2.Click += button2_Click;
        // 
        // button3
        // 
        button3.Location = new Point(607, 426);
        button3.Name = "button3";
        button3.Size = new Size(94, 29);
        button3.TabIndex = 4;
        button3.Text = "הוספה";
        button3.UseVisualStyleBackColor = true;
        button3.Click += button3_Click;
        // 
        // button4
        // 
        button4.Location = new Point(482, 426);
        button4.Name = "button4";
        button4.Size = new Size(94, 29);
        button4.TabIndex = 5;
        button4.Text = "מחיקה";
        button4.UseVisualStyleBackColor = true;
        // 
        // CustomersForm
        // 
        ClientSize = new Size(1094, 585);
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
        var form = new Form { Width = 400, Height = 140, Text = caption };
        var tb = new TextBox { Left = 10, Top = 10, Width = 360, Text = defaultValue ?? "" };
        var ok = new Button { Text = "OK", Left = 200, Width = 80, Top = 40, DialogResult = DialogResult.OK };
        var cancel = new Button { Text = "Cancel", Left = 290, Width = 80, Top = 40, DialogResult = DialogResult.Cancel };
        form.Controls.AddRange(new Control[] { tb, ok, cancel });
        form.AcceptButton = ok; form.CancelButton = cancel;
        return form.ShowDialog() == DialogResult.OK ? tb.Text : null;
    }

    private void button2_Click(object? sender, EventArgs e)
    {
        EditCustomer();
    }

    private void button3_Click(object? sender, EventArgs e)
    {
        AddCustomer();
    }
    private void button4_Click(object? sender, EventArgs e)
    {
        DeleteCustomer();
    }
    private void button1_Click_1(object? sender, EventArgs e)
    {
        LoadData();
    }

    private void dataGridViewCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
}