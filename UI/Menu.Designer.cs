namespace UI
{
    partial class Menu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            SuspendLayout();

            // --- פלטת צבעים אחידה לכל המסכים ---
            Color beigeBg = Color.FromArgb(248, 245, 235);
            Color softBrown = Color.FromArgb(181, 142, 98);
            Color darkBrown = Color.FromArgb(64, 44, 29);
            Font menuFont = new Font("Segoe UI", 12F, FontStyle.Bold);

            // 
            // button1 (ניהול מוצרים)
            // 
            button1.Location = new Point(450, 110);
            button1.Name = "button1";
            button1.Size = new Size(250, 60); // גודל אחיד עם "פדינג" ויזואלי
            button1.TabIndex = 0;
            button1.Text = "ניהול מוצרים";
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.BackColor = softBrown; // צבע אחיד
            button1.ForeColor = Color.White;
            button1.Font = menuFont;
            button1.Cursor = Cursors.Hand;
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;

            // 
            // button2 (ניהול לקוחות)
            // 
            button2.Location = new Point(450, 45);
            button2.Name = "button2";
            button2.Size = new Size(250, 60);
            button2.TabIndex = 1;
            button2.Text = "ניהול לקוחות";
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.BackColor = softBrown; // צבע אחיד
            button2.ForeColor = Color.White;
            button2.Font = menuFont;
            button2.Cursor = Cursors.Hand;
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;

            // 
            // button3 (ביצוע הזמנה)
            // 
            button3.Location = new Point(450, 220);
            button3.Name = "button3";
            button3.Size = new Size(250, 60);
            button3.TabIndex = 2;
            button3.Text = "ביצוע הזמנה";
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;
            button3.BackColor = softBrown; // צבע אחיד
            button3.ForeColor = Color.White;
            button3.Font = menuFont;
            button3.Cursor = Cursors.Hand;
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;

            // 
            // Menu (טופס)
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = beigeBg;
            ClientSize = new Size(800, 450);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Menu";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "תפריט ראשי - Boutique System";
            ResumeLayout(false);
        }

  

private void button1_Click(object sender, EventArgs e)
        {
            // הלוגיקה שלך כאן, למשל:
            new ProductForm().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // לוגיקה לניהול לקוחות
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // לוגיקה לניהול מכירות
        }
        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
    }
}