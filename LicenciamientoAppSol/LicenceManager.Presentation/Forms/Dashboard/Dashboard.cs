using LicenceManager.Presentation.Forms.Configuracion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LicenceManager.Presentation.Forms.Dashboard
{
    public partial class Dashboard: Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private Form popup = null; // Variable a nivel de clase

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        // Agregar este método al formulario principal para asegurar la limpieza
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (popup != null)
            {
                popup.Close();
                popup = null;
            }
            base.OnFormClosing(e);
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            // Si el popup ya existe, lo cerramos y salimos
            if (popup != null)
            {
                popup.Close();
                popup = null;
                return;
            }

            // Crear nuevo popup
            popup = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.Manual,
                Size = new Size(180, 120),
                BackColor = Color.White,
                ShowInTaskbar = false,
                TopMost = true
            };

            // Configurar ImageList
            var imageList = new ImageList
            {
                ImageSize = new Size(20, 20)
            };
            imageList.Images.Add("open", Properties.Resources.Ajustes);

            // Configurar ListView
            var listView = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                HeaderStyle = ColumnHeaderStyle.None,
                SmallImageList = imageList
            };
            listView.Columns.Add("", 150);

            // Agregar items al ListView
            listView.Items.Add(new ListViewItem("Abrir", "open"));
            listView.Items.Add(new ListViewItem("Editar", "open"));
            listView.Items.Add(new ListViewItem("Eliminar", "open"));

            // Configurar evento de clic
            listView.Click += (s, ev) =>
            {
                if (listView.SelectedItems.Count > 0)
                {
                    string selectedOption = listView.SelectedItems[0].Text;
                    popup.Close();
                    popup = null;

                    switch (selectedOption)
                    {
                        case "Abrir":
                            using (var form = new FormAbrir())
                            {
                                form.ShowDialog();
                            }
                            break;
                        case "Editar":
                            using (var form = new FormEditar())
                            {
                                form.ShowDialog();
                            }
                            break;
                        case "Eliminar":
                            MessageBox.Show("Función de eliminar en desarrollo", "Eliminar",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                    }
                }
            };

            // Posicionar el popup
            Point screenPoint = iconButton4.PointToScreen(new Point(0, iconButton4.Height));
            Rectangle workingArea = Screen.FromControl(this).WorkingArea;
            int x = Math.Min(screenPoint.X, workingArea.Right - popup.Width);
            int y = Math.Min(screenPoint.Y, workingArea.Bottom - popup.Height);
            popup.Location = new Point(x, y);

            // Agregar ListView al form
            popup.Controls.Add(listView);

            // Evento para cerrar cuando se hace clic fuera
            popup.Deactivate += (s, ev) =>
            {
                popup.Close();
                popup = null;
            };

            // Mostrar el popup
            popup.Show();
        }
    }
}
 
