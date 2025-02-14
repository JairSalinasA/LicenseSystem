namespace LicenceManager.Presentation.Forms.Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnLogin = new RJCodeAdvance.RJControls.RJButton();
            this.txtUsername = new RJCodeAdvance.RJControls.RJTextBox();
            this.txtPassword = new RJCodeAdvance.RJControls.RJTextBox();
            this.LOGIN = new System.Windows.Forms.Label();
            this.errorUserLabel = new System.Windows.Forms.Label();
            this.errorPasswordLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btncerrar = new FontAwesome.Sharp.IconButton();
            this.btnminimizar = new FontAwesome.Sharp.IconButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(22)))), ((int)(((byte)(58)))));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 330);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(27, 77);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(183, 177);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnLogin.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.btnLogin.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnLogin.BorderRadius = 0;
            this.btnLogin.BorderSize = 0;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(332, 225);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(312, 44);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Text = "INICIAR  SESION";
            this.btnLogin.TextColor = System.Drawing.Color.White;
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click_1);
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.SystemColors.Window;
            this.txtUsername.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(22)))), ((int)(((byte)(58)))));
            this.txtUsername.BorderFocusColor = System.Drawing.Color.MediumSlateBlue;
            this.txtUsername.BorderRadius = 0;
            this.txtUsername.BorderSize = 2;
            this.txtUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtUsername.Location = new System.Drawing.Point(332, 84);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUsername.Multiline = false;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtUsername.PasswordChar = false;
            this.txtUsername.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtUsername.PlaceholderText = "Usuario";
            this.txtUsername.Size = new System.Drawing.Size(312, 31);
            this.txtUsername.TabIndex = 2;
            this.txtUsername.Texts = "";
            this.txtUsername.UnderlinedStyle = true;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.SystemColors.Window;
            this.txtPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(22)))), ((int)(((byte)(58)))));
            this.txtPassword.BorderFocusColor = System.Drawing.Color.MediumSlateBlue;
            this.txtPassword.BorderRadius = 0;
            this.txtPassword.BorderSize = 2;
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPassword.Location = new System.Drawing.Point(332, 144);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPassword.Multiline = false;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtPassword.PasswordChar = false;
            this.txtPassword.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtPassword.PlaceholderText = "Contraseña";
            this.txtPassword.Size = new System.Drawing.Size(312, 31);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.Texts = "";
            this.txtPassword.UnderlinedStyle = true;
            // 
            // LOGIN
            // 
            this.LOGIN.AutoSize = true;
            this.LOGIN.Font = new System.Drawing.Font("Century Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LOGIN.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.LOGIN.Location = new System.Drawing.Point(428, 19);
            this.LOGIN.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LOGIN.Name = "LOGIN";
            this.LOGIN.Size = new System.Drawing.Size(100, 33);
            this.LOGIN.TabIndex = 4;
            this.LOGIN.Text = "LOGIN";
            // 
            // errorUserLabel
            // 
            this.errorUserLabel.AutoSize = true;
            this.errorUserLabel.Location = new System.Drawing.Point(336, 119);
            this.errorUserLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.errorUserLabel.Name = "errorUserLabel";
            this.errorUserLabel.Size = new System.Drawing.Size(35, 13);
            this.errorUserLabel.TabIndex = 5;
            this.errorUserLabel.Text = "label1";
            // 
            // errorPasswordLabel
            // 
            this.errorPasswordLabel.AutoSize = true;
            this.errorPasswordLabel.Location = new System.Drawing.Point(336, 180);
            this.errorPasswordLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.errorPasswordLabel.Name = "errorPasswordLabel";
            this.errorPasswordLabel.Size = new System.Drawing.Size(35, 13);
            this.errorPasswordLabel.TabIndex = 5;
            this.errorPasswordLabel.Text = "label1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Gainsboro;
            this.label3.Location = new System.Drawing.Point(336, 272);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "¿Haz  Olvidado tu cantraceña?";
            // 
            // btncerrar
            // 
            this.btncerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncerrar.ForeColor = System.Drawing.Color.Transparent;
            this.btncerrar.IconChar = FontAwesome.Sharp.IconChar.RectangleXmark;
            this.btncerrar.IconColor = System.Drawing.Color.DimGray;
            this.btncerrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btncerrar.IconSize = 20;
            this.btncerrar.Location = new System.Drawing.Point(680, 0);
            this.btncerrar.Name = "btncerrar";
            this.btncerrar.Size = new System.Drawing.Size(42, 30);
            this.btncerrar.TabIndex = 6;
            this.btncerrar.UseVisualStyleBackColor = true;
            this.btncerrar.Click += new System.EventHandler(this.btncerrar_Click);
            // 
            // btnminimizar
            // 
            this.btnminimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnminimizar.ForeColor = System.Drawing.Color.Transparent;
            this.btnminimizar.IconChar = FontAwesome.Sharp.IconChar.WindowMinimize;
            this.btnminimizar.IconColor = System.Drawing.Color.DimGray;
            this.btnminimizar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnminimizar.IconSize = 20;
            this.btnminimizar.Location = new System.Drawing.Point(639, 0);
            this.btnminimizar.Name = "btnminimizar";
            this.btnminimizar.Size = new System.Drawing.Size(42, 30);
            this.btnminimizar.TabIndex = 7;
            this.btnminimizar.UseVisualStyleBackColor = true;
            this.btnminimizar.Click += new System.EventHandler(this.btnminimizar_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(722, 330);
            this.Controls.Add(this.btnminimizar);
            this.Controls.Add(this.btncerrar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.errorPasswordLabel);
            this.Controls.Add(this.errorUserLabel);
            this.Controls.Add(this.LOGIN);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "LoginForm";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginForm";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LoginForm_MouseDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private RJCodeAdvance.RJControls.RJButton btnLogin;
        private RJCodeAdvance.RJControls.RJTextBox txtUsername;
        private RJCodeAdvance.RJControls.RJTextBox txtPassword;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label LOGIN;
        private System.Windows.Forms.Label errorUserLabel;
        private System.Windows.Forms.Label errorPasswordLabel;
        private System.Windows.Forms.Label label3;
        private FontAwesome.Sharp.IconButton btncerrar;
        private FontAwesome.Sharp.IconButton btnminimizar;
    }
}