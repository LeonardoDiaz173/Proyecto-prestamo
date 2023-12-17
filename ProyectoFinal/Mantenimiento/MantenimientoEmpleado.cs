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

namespace ProyectoFinal.Mantenimiento
{
    public partial class MantenimientoEmpleado : Form
    {
        public MantenimientoEmpleado()
        {
            InitializeComponent();
            Llenar();
        }

        Funciones funciones = new Funciones();

        private void Llenar()
        {
            funciones.BusquedaEspecifica("select " +
                                         "a.id, a.nombre,  a.sexo, a.dni, b.numero 'telefono', " +
                                         "CONCAT(d.nombre, ' ', c.sector, ' ', c.calle, ' ', c.numero, ' ', c.referencia_adicional) as 'direccion' " +
                                         "from empleado a " +
                                         "LEFT JOIN empleado_vs_telefono ab ON ab.empleado = a.id " +
                                         "LEFT JOIN telefono b ON ab.telefono = b.id " +
                                         "LEFT JOIN empleado_vs_direccion ac ON ac.empleado = a.id " +
                                         "LEFT JOIN direccion c ON ac.direccion = c.id " +
                                         "LEFT JOIN provincia d ON c.id_provincia = d.id", dgvEmpleado);

            funciones.BusquedaEspecificaComboBox("Select nombre from provincia where estado = 0", "nombre", cbProvincia);
        }

        private void Limpiar()
        {
            foreach (Control item in gbDatosDireccion.Controls)
            {
                if (item is TextBox || item is ComboBox)
                {
                    item.Text = "";
                    errorProvider.Clear();
                }
            }

            foreach (Control item in gbDatosEmpleados.Controls)
            {
                if (item is TextBox || item is ComboBox)
                {
                    item.Text = "";
                    errorProvider.Clear();
                }
            }

            btnAgregar.Visible = true;
            btnEditar.Visible = false;
            btnEliminar.Visible = false;
            Llenar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            bool validacion = false;

            foreach (Control item in gbDatosEmpleados.Controls)
            {
                if (item is TextBox && item.Enabled == true || item is ComboBox && item.Enabled == true)
                {
                    if (item.Text != "" && item != txtCodigo)
                    {
                        errorProvider.Clear();
                        validacion = true;
                    }
                    else if (item != txtCodigo)
                    {
                        errorProvider.SetError(item, "Campo requerido!");
                        validacion = false;
                        break;
                    }
                }
            }

            foreach (Control item in gbDatosDireccion.Controls)
            {
                if (item is TextBox && item.Enabled == true || item is ComboBox && item.Enabled == true)
                {
                    if (item.Text != "" && item != txtReferenciaAdicional)
                    {
                        errorProvider.Clear();
                        validacion = true;

                    }
                    else if (item != txtReferenciaAdicional)
                    {
                        errorProvider.SetError(item, "Campo requerido!");
                        validacion = false;
                        break;
                    }
                }
            }

            if (validacion)
            {
                funciones.comando("insert into direccion values (" +
                    " '" + funciones.ObtenerUltimoID("direccion") + "', " +
                    "'" + funciones.ObtenerID("select top 1 id from provincia where nombre = '" + cbProvincia.Text + "' and estado = 0") + "', " +
                    "'" + txtSector.Text + "', " +
                    "'" + txtCalle.Text + "', " +
                    "'" + txtNumero.Text + "', " +
                    "'" + txtReferenciaAdicional.Text + "')");

                funciones.comando("insert into telefono (id, numero)  values (" +
                    "'" + funciones.ObtenerUltimoID("telefono") + "', " +
                    "'" + txtTelefono.Text + "')");

                funciones.comando("insert into empleado (id, nombre, sexo, dni, estado) values " +
                    "('" + funciones.ObtenerUltimoID("empleado") + "', " +
                    "'" + txtNombre.Text + "', " +
                    "'" + cbSexo.Text + "', " +
                    "'" + txtDNI.Text + "', " +
                    "'0')");

                funciones.comando("insert into empleado_vs_telefono values (" +
                    "'" + funciones.ObtenerID("select top 1 id from empleado order by id desc") + "', '" + funciones.ObtenerID("select top 1 id from telefono order by id desc") + "')");
                funciones.comando("insert into empleado_vs_direccion values (" +
                    "'"+ funciones.ObtenerID("select top 1 id from empleado order by id desc") +"', '"+ funciones.ObtenerID("select top 1 id from direccion order by id desc") +"')");

                Limpiar();
                MessageBox.Show("Registro insertado exitosamente!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if(txtCodigo.Text != "")
            {
                funciones.comando("update direccion set " +
                    "id_provincia = '" + funciones.ObtenerID("select top 1 id from provincia where nombre = '" + cbProvincia.Text + "' and estado = 0") + "', " +
                    " sector = '" + txtSector.Text + "', " +
                    "calle = '" + txtCalle.Text + "', " +
                    "numero = '" + txtNumero.Text + "', " +
                    "referencia_adicional = '" + txtReferenciaAdicional.Text + "' " +
                    "where id = '" + funciones.ObtenerID("select direccion as id from empleado_vs_direccion where empleado = '" + txtCodigo.Text + "'") + "'");

                funciones.comando("update telefono set " +
                    "numero = '" + txtTelefono.Text + "' " +
                    "Where id = '" + funciones.ObtenerID("select telefono as id from empleado_vs_telefono where empleado = '" + txtCodigo.Text + "'") + "'");

                funciones.comando("update empleado set " +
                    "nombre = '" + txtNombre.Text + "', " +
                    "sexo = '" + cbSexo.Text + "', " +
                    "dni = '" + txtDNI.Text + "' " +
                    "where id = '" + txtCodigo.Text + "'");

                MessageBox.Show("Edicion exitosa!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
            }
            else
            {
                errorProvider.SetError(txtCodigo, "Debe seleccionar un empleado!");
            }
        }

        private void dgvEmpleado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditar.Visible = true;
            btnEliminar.Visible = true;
            btnAgregar.Visible = false;

            txtCodigo.Text = dgvEmpleado.SelectedCells[0].Value.ToString();

            SqlCommand command = new SqlCommand("select " +
                "a.id, " +
                "a.nombre, " +
                "a.sexo, " +
                "a.dni, " +
                "b.numero 'telefono'," +
                " d.nombre as 'provincia', " +
                "c.sector, " +
                "c.calle, " +
                "c.numero, " +
                "c.referencia_adicional " +
                "from empleado a " +
                "LEFT JOIN empleado_vs_telefono ab ON ab.empleado = a.id " +
                "LEFT JOIN telefono b ON ab.telefono = b.id " +
                "LEFT JOIN empleado_vs_direccion ac ON ac.empleado = a.id " +
                "LEFT JOIN direccion c ON ac.direccion = c.id " +
                "LEFT JOIN provincia d ON c.id_provincia = d.id;", Funciones.Connection());
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                txtNombre.Text = reader["nombre"].ToString();
                cbSexo.Text = reader["sexo"].ToString();
                txtDNI.Text = reader["dni"].ToString();
                txtTelefono.Text = reader["telefono"].ToString();

                cbProvincia.Text = reader["provincia"].ToString();
                txtSector.Text = reader["sector"].ToString();
                txtCalle.Text = reader["calle"].ToString();
                txtNumero.Text = reader["numero"].ToString();
                txtReferenciaAdicional.Text = reader["referencia_adicional"].ToString();
            }
            Funciones.Connection().Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text != "")
            {
                DialogResult desicion = MessageBox.Show("¿Seguro que desea eliminar este regsitro?, esta accion no se puede deshacer.", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (desicion == DialogResult.Yes)
                {
                    funciones.comando("update empleado set estado = 1 where id = '" + txtCodigo.Text + "'");
                    MessageBox.Show("Registro eliminado correctamente!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Llenar();
                    Limpiar();
                }

            }
            else
            {
                errorProvider.SetError(txtCodigo, "Debe seleccionar un cliente!");
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            General.Inicio frmInicio = new General.Inicio();
            frmInicio.Show();
            Hide();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text != "")
            {
                funciones.BusquedaEspecifica("select " +
                                         "a.id, a.nombre,  a.sexo, a.dni, b.numero 'telefono', " +
                                         "CONCAT(d.nombre, ' ', c.sector, ' ', c.calle, ' ', c.numero, ' ', c.referencia_adicional) as 'direccion' " +
                                         "from empleado a " +
                                         "LEFT JOIN empleado_vs_telefono ab ON ab.empleado = a.id " +
                                         "LEFT JOIN telefono b ON ab.telefono = b.id " +
                                         "LEFT JOIN empleado_vs_direccion ac ON ac.empleado = a.id " +
                                         "LEFT JOIN direccion c ON ac.direccion = c.id " +
                                         "LEFT JOIN provincia d ON c.id_provincia = d.id " +
                                         "WHERE a.estado = 0 and (a.id like '%"+ txtBuscar.Text +"%' or a.nombre like '%"+ txtBuscar.Text +"%' or a.dni like '%"+ txtBuscar.Text +"%' or a.sexo like '%"+ txtBuscar.Text +"%')", dgvEmpleado);
            }
            else
            {
                Llenar();
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            funciones.ValidarCampo(txtNombre, "apellido", errorProvider, "Debe de ingresar formato valido para nombre!");
        }

        private void cbSexo_SelectedIndexChanged(object sender, EventArgs e)
        {
            funciones.ValidarCampo(cbSexo, "Sexo", errorProvider, "Solo femenino y masculino!");
        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {
            funciones.ValidarCampo(txtTelefono, "telefono", errorProvider, "Debe ingresar formato valido!");
        }

        private void txtDNI_TextChanged(object sender, EventArgs e)
        {
            funciones.ValidarCampo(txtTelefono, "dni", errorProvider, "Debe de ingresar formato valido!");
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
