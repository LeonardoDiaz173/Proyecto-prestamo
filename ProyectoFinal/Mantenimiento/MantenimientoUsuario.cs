using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 

namespace ProyectoFinal.Consultas
{
    public partial class ConsultaUsuarios : Form
    {
        public ConsultaUsuarios()
        {
            InitializeComponent();
            lblUsuario.Text = user.UserName;
            Llenar();
        }
        Funciones funciones = new Funciones();

        private void Llenar()
        {
            funciones.BusquedaGenerar("usuario", dgvUsuarios);
        }

        private void Limpiar()
        {
            foreach (Control item in groupBox1.Controls)
            {
                if(item is TextBox || item is ComboBox)
                {
                    item.Text = "";
                    
                }
            }

            errorProvider1.Clear();
            btnAgregar.Visible = true;
            btnEditar.Visible = false;
            btnEliminar.Visible = false;
        }

        private  void btnAgregar_Click(object sender, EventArgs e)
        {
            bool validar = false;

            foreach (Control item in groupBox1.Controls)
            {
                if (item.Text != "" && item != txtCodigo)
                {
                    errorProvider1.Clear();
                    validar = true;
                }
                else if(item != txtCodigo)
                {
                    errorProvider1.SetError(item, "Campo requerido!");
                    validar = false;
                    break;
                }
            }

            if (validar)
            {
                string comando = "insert into usuario values (" + (funciones.ObtenerUltimoID("usuario") + 1) + ", '" + txtNombre.Text + "', '" + txtPassword.Text + "', '" + cbLevel.SelectedValue + "', '" + txtEmail.Text + "', '0')";
                funciones.comando(comando);

                MessageBox.Show("Usuario agregado!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Llenar();
                Limpiar();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string comando = "update usuario set name = '"+txtNombre.Text+"', password = '"+txtPassword.Text+"', level = '"+cbLevel.Text+"', email = '"+ txtEmail.Text +"' where id=" + txtCodigo.Text + "";

            funciones.comando(comando);
            MessageBox.Show("Usuario editado exitosamente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Llenar();
            Limpiar();
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEliminar.Visible = true;
            btnEditar.Visible = true;
            btnAgregar.Visible = false;


            txtCodigo.Text = dgvUsuarios.SelectedCells[0].Value.ToString();
            txtNombre.Text = dgvUsuarios.SelectedCells[1].Value.ToString();
            txtPassword.Text = dgvUsuarios.SelectedCells[2].Value.ToString();
            cbLevel.Text = dgvUsuarios.SelectedCells[3].Value.ToString();
            txtEmail.Text = dgvUsuarios.SelectedCells[4].Value.ToString();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string comando = "update usuario set estado = '1' where id=" + txtCodigo.Text + "";

            funciones.comando(comando);
            MessageBox.Show("Usuario eliminado exitosamente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Llenar();
            Limpiar();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
            Llenar();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            General.Inicio frmInicio = new General.Inicio();
            frmInicio.Show();
            Hide();
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            if(txtBusqueda.Text != "")
            {
                funciones.BusquedaEspecifica(
                    "select * from usuario where " +
                    " id like '%"+ txtBusqueda.Text + "%' AND estado = 0 OR " +
                    "name like '%"+ txtBusqueda.Text + "%' AND estado = 0 OR " +
                    "level like '%"+ txtBusqueda.Text + "%' AND estado = 0 OR " +
                    "email like '%"+ txtBusqueda.Text +"%' AND estado = 0",dgvUsuarios);
                      

            }
            else
            {
                Llenar();
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            funciones.ValidarCampo(txtNombre, "Nombre", errorProvider1, "Debe ingresar el formato valido para nombre!");
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            funciones.ValidarCampo(txtPassword, "password", errorProvider1, "Debe ingresar el formato valido para la contraseña!");
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            funciones.ValidarCampo(txtEmail, "correo", errorProvider1, "Debe ingresar el formato valido para el correo!");
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "")
                lblAccion.Text = "Creando";
            else
                lblAccion.Text = "Modificando";
        }
    }
}
