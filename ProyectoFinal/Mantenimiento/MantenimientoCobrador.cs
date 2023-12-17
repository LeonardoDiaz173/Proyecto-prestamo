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
    public partial class MantenimientoCobrador : Form
    {
        public MantenimientoCobrador()
        {
            InitializeComponent();
            LlenarCobrador();
        }

        Funciones funciones = new Funciones();

        private void LlenarCobrador()
        {
            
            cbProvincia.Items.Clear();

            funciones.BusquedaEspecifica("Select " +
                                         "a.id," +
                                         "d.nombre 'empleado'," +
                                         "b.nombre 'Zona', " +
                                         "c.nombre 'Provincia' " +
                                         "from cobradores a " +
                                         "LEFT JOIN zona b ON a.zona = b.id " +
                                         "LEFT JOIN provincia c ON a.provincia = c.id " +
                                         "LEFT JOIN empleado d ON a.empleado = d.id " ,dgvCobradores);

            funciones.BusquedaEspecificaComboBox("select nombre from provincia where estado = 0" , "nombre", cbProvincia);
            
        }

        private void Limpiar()
        {
            foreach (Control item in gbDatosEmpleado.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }

            foreach (Control item in gbDatosUbicacion.Controls)
            {
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
            btnAgregar.Visible = true;
            btnEditar.Visible = false;
            btnEliminar.Visible = false;
            LlenarCobrador();
        }

        private void txtCodigoEmpleado_DoubleClick(object sender, EventArgs e)
        {
            Consultas.ConsultaEmpleado frmEmpleado = new Consultas.ConsultaEmpleado();
            AddOwnedForm(frmEmpleado);
            frmEmpleado.Show();
            frmEmpleado.Focus();
        }

        private void txtCodigoEmpleado_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void cbProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbZona.Items.Clear();
            funciones.BusquedaEspecificaComboBox("select nombre from zona where estado = 0 and provincia = '" + funciones.ObtenerID("select id from provincia where nombre = '" + cbProvincia.Text + "'") + "'", "nombre", cbZona);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            General.Inicio frmInicio = new General.Inicio();
            frmInicio.Show();
            Hide();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            bool validar = false;

            foreach (Control item in gbDatosEmpleado.Controls)
            {
                if (item is TextBox )
                {
                    if(item.Text == "")
                    {
                        errorProvider.SetError(item, "Debe rellenar campos requeridos!");
                        validar = false;
                        break;
                    }
                    else
                    {
                        errorProvider.Clear();
                        validar = true;
                    }
                }
            }

            foreach (Control item in gbDatosUbicacion.Controls)
            {
                if (item is ComboBox)
                {
                    if (item.Text == "")
                    {
                        errorProvider.SetError(item, "Debe rellenar campos requeridos!");
                        validar = false;
                        break;
                    }
                    else
                    {
                        errorProvider.Clear();
                        validar = true;
                    }
                }
            }

            if (validar)
            {
                funciones.comando("insert into cobradores values (" +
                                  "'" + funciones.ObtenerUltimoID("cobradores") + "', " +
                                  "'" + funciones.ObtenerID("select id from zona where estado = 0 and nombre = '" + cbZona.Text + "'") + "', " +
                                  "'" + funciones.ObtenerID("select id from provincia where estado = 0 and nombre = '" + cbProvincia.Text + "'") + "', " +
                                  "'" + txtCodigoEmpleado.Text + "', 0)");

                Limpiar();
                MessageBox.Show("Registro Insertado Exitosamente!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvCobradores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAgregar.Visible = false;
            btnEditar.Visible = true;
            btnEliminar.Visible = true;

            txtCodigoEmpleado.Enabled = false;

            txtCodigoEmpleado.Text = dgvCobradores.SelectedCells[0].Value.ToString();
            txtNombreEmpleado.Text = dgvCobradores.SelectedCells[1].Value.ToString();
            cbProvincia.Text = dgvCobradores.SelectedCells[2].Value.ToString();
            cbZona.Text = dgvCobradores.SelectedCells[3].Value.ToString();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtCodigoEmpleado.Text != "")
            {
                funciones.comando("update cobradores set " +
                    "zona = '"+ funciones.ObtenerID("select id from zona where nombre = '"+ cbZona.Text +"' and estado = 0") +"', " +
                    "provincia = '"+ funciones.ObtenerID("select id from provincia where nombre = '"+ cbProvincia.Text +"' and estado = 0") +"'");

                Limpiar();
                MessageBox.Show("Registro editado exitosamente!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtCodigoEmpleado.Text != "")
            {
                funciones.comando("update cobradores set estado = 1 where id = '" + txtCodigoEmpleado.Text + "'");
                Limpiar();
                MessageBox.Show("Registro eliminado exitosamente!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
    }
}
