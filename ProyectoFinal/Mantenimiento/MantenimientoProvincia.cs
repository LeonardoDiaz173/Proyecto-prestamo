using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal.Mantenimiento
{
    public partial class MantenimientoProvincia : Form
    {
        public MantenimientoProvincia()
        {
            InitializeComponent();
            Llenar();
        }

        Funciones funciones = new Funciones();

        private void Llenar()
        {
            funciones.BusquedaGenerar("Provincia", dgvProvincia);
        }

        private void Limpiar()
        {
            foreach (Control item in panel1.Controls)
            {
                if(item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(txtNombre.Text != "")
            {
                funciones.comando("insert into provincia values (" + funciones.ObtenerUltimoID("provincia") + ", '" + txtNombre.Text + "', 0)");
                MessageBox.Show("Registro insertado exitosamente!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                Llenar();
            }
            else
            {
                MessageBox.Show("No se puede ingresar un nombre vacio!", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvProvincia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCodigo.Text = dgvProvincia.SelectedCells[0].Value.ToString();
            txtNombre.Text = dgvProvincia.SelectedCells[1].Value.ToString();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if(txtCodigo.Text != "")
            {
                funciones.comando("update provincia set nombre = '" + txtNombre.Text + "' where id = '" + txtCodigo.Text + "'");
                MessageBox.Show("Registro actualizado correctamente!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                Llenar();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro!", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if(txtCodigo.Text != "")
            {
                var opcion = MessageBox.Show("¿Seguro que desea eliminar?, si elimina no podra recuperar el registro.", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (opcion == DialogResult.Yes)
                {
                    funciones.comando("update provincia set estado = '1' where id = '" + txtCodigo.Text + "'");
                    MessageBox.Show("Registro eliminado correctamente!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Limpiar();
                Llenar();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro!", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void MantenimientoProvincia_FormClosed(object sender, FormClosedEventArgs e)
        {
            Consultas.MantenimientoCliente frmMantenimientoCliente = new Consultas.MantenimientoCliente();
            frmMantenimientoCliente.Llenar_cliente();
        }
    }
}
