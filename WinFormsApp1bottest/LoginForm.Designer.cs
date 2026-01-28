namespace WinFormsApp1bottest
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtUser = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtPass = new TextBox();
            button1 = new Button();
            button2 = new Button();
            label3 = new Label();
            SuspendLayout();
            // 
            // txtUser
            // 
            txtUser.Location = new Point(354, 192);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(163, 25);
            txtUser.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(264, 195);
            label1.Name = "label1";
            label1.Size = new Size(69, 17);
            label1.TabIndex = 1;
            label1.Text = "Username";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(264, 240);
            label2.Name = "label2";
            label2.Size = new Size(66, 17);
            label2.TabIndex = 3;
            label2.Text = "Password";
            // 
            // txtPass
            // 
            txtPass.Location = new Point(354, 237);
            txtPass.Name = "txtPass";
            txtPass.Size = new Size(163, 25);
            txtPass.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new Point(354, 282);
            button1.Name = "button1";
            button1.Size = new Size(89, 41);
            button1.TabIndex = 4;
            button1.Text = "Login in";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(465, 282);
            button2.Name = "button2";
            button2.Size = new Size(52, 41);
            button2.TabIndex = 5;
            button2.Text = "+";
            button2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 48F, FontStyle.Bold, GraphicsUnit.Point, 178);
            label3.Location = new Point(240, 54);
            label3.Name = "label3";
            label3.Size = new Size(331, 86);
            label3.TabIndex = 6;
            label3.Text = "G - Bot Ai";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 510);
            Controls.Add(label3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(txtPass);
            Controls.Add(label1);
            Controls.Add(txtUser);
            Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 178);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "LoginForm";
            Text = "LoginForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtUser;
        private Label label1;
        private Label label2;
        private TextBox txtPass;
        private Button button1;
        private Button button2;
        private Label label3;
    }
}