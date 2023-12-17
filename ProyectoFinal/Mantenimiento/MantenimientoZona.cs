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
    public partial class MantenimientoZona : Form
    {
        public MantenimientoZona()
        {
            InitializeComponent();
            Llenar();
        }

        Funciones funciones = new Funciones();

        private void Llenar()
        {
            funciones.BusquedaEspecifica("select zona.id, zona.nombre as zona, provincia.nombre as provincia from zona LEFT JOIN provincia ON zona.provincia = provincia.id where zona.estado = 0", dgvZona);
            funciones.BusquedaEspecificaComboBox("select nombre from provincia where estado = 0", "nombre", cbProvincia);
        }
        
        private void Limpiar()
        {
            foreach (Control item in panel1.Controls)
            {
                if (item is TextBox || item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text != "")
            {
                funciones.comando("insert into zona (id, nombre, provincia, estado) values " +
                    "('" + funciones.ObtenerUltimoID("zona") + "', " +
                    "'" + txtNombre.Text + "', " +
                    "'"+ funciones.ObtenerID("select id from provincia where nombre = '"+ cbProvincia.Text +" and estado = 0'") +"', '0')");
                MessageBox.Show("Registro insertado correctamente!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                Llenar();
            }
            else
            {
                MessageBox.Show("No se puede ingresar un nombre vacio!", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text != "")
            {
                funciones.comando("update zona set nombre = '" + txtNombre.Text + "', " +
                    "provincia = '"+ funciones.ObtenerID("select id from provincia where nombre = '" + cbProvincia.Text + "' and estado = 0") + "' " +
                    "where id = '" + txtCodigo.Text + "'");
                MessageBox.Show("Registro actualizado correctamente!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                Llenar();
                btnAgregar.Visible = true;
                btnEditar.Visible = false;
                btnEliminar.Visible = false;
            }
            else
            {
                MessageBox.Show("Debe de seleccionar un registro!", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text != "")
            {
                var opcion = MessageBox.Show("¿Seguro que desea eliminar?, Esta accion no se puede deshacer", "ADVRTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (opcion == DialogResult.Yes)
                {
                    funciones.comando("update zona set estado = '1' where id = '" + txtCodigo.Text + "'");
                    MessageBox.Show("Registro eliminado correctamente!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Llenar();
                    Limpiar();
                    btnAgregar.Visible = true;
                    btnEditar.Visible = false;
                    btnEliminar.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Debe de seleccionar un registro!", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvZona_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAgregar.Visible = false;
            btnEditar.Visible = true;
            btnEliminar.Visible = true;

            txtCodigo.Text = dgvZona.SelectedCells[0].Value.ToString();
            txtNombre.Text = dgvZona.SelectedCells[1].Value.ToString();
            cbProvincia.Text = dgvZona.SelectedCells[2].Value.ToString();
        }


        private void MantenimientoZona_FormClosed(object sender, FormClosedEventArgs e)
        {
            Consultas.MantenimientoCliente frmMantenimientoCliente = new Consultas.MantenimientoCliente();
            funciones.BusquedaEspecificaComboBox("select nombre from zona", "nombre", frmMantenimientoCliente.cbZona);
        }
    }
}
