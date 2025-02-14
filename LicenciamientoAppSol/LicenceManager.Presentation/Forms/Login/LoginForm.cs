 
using LicenceManager.Data.Repositories;
using LicenceManager.Common.Helpers;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LicenceManager.Data.Helpers;
using System.Security.Policy;
using System.Runtime.InteropServices;

namespace LicenceManager.Presentation.Forms.Login
{
    public partial class LoginForm : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private readonly UsuarioRepository _usuarioRepository;
        public LoginForm()
        {
            InitializeComponent();
            var dbHelper = new DatabaseHelper(AppConfig.ConnectionString);
            _usuarioRepository = new UsuarioRepository(dbHelper);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            string username = txtUsername.Texts.Trim();
            string password = txtPassword.Texts.Trim();

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            Console.WriteLine(hashedPassword);

            var usuario = _usuarioRepository.GetByUsername(username);

            if (usuario == null)
            {
                MessageBox.Show("Usuario no encontrado en la base de datos.");
                return;
            }

            bool passwordValida = BCrypt.Net.BCrypt.Verify(password, usuario.ContrasenaHash.Trim());

            MessageBox.Show($"Usuario encontrado: {usuario.NombreUsuario}\n" +
                            $"Hash en BD: {usuario.ContrasenaHash.Trim()}\n" +
                            $"Contraseña válida: {passwordValida}");

            if (passwordValida)
            {
                MessageBox.Show("Login exitoso");
            }
            else
            {
                MessageBox.Show("Credenciales inválidas");
            }
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void LoginForm_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
