using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ProyectoFinal
{
    public partial class InicioSesion : Form
    {
        public InicioSesion()
        {
            InitializeComponent();
        }

        private void Validar()
        {
            if (txtUsuario.Text != "" && txtPassword.Text != "")
            {
                try
                {
                    Funciones.Connection();
                    SqlCommand command = new SqlCommand(
                        "select * from usuario " +
                        "where name='" + txtUsuario.Text + "' and " +
                        "password = '" + txtPassword.Text + "'", Funciones.Connection());

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            user.UserName = reader["name"].ToString();
                            user.UserLevel = reader["level"].ToString();
                        }

                        General.Inicio frmInicio = new General.Inicio();
                        frmInicio.Show();
                        Hide();
                    }
                    else
                    {
                        MessageBox.Show("Usuario u/o contraseña incorrecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        foreach (Control item in panel2.Controls)
                        {
                            if (item is TextBox)
                            {
                                item.Text = "";
                            }
                        }
                        txtUsuario.Focus();
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error 1 : " + error.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Funciones.Connection().Close();
                }
            }
            else
                MessageBox.Show("Todos los campos deben ser llenos", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            Validar();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;

            
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                Validar();
            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                txtPassword.Focus();
            }
        }
    }
}
