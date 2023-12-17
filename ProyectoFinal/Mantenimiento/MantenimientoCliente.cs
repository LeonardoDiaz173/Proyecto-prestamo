using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace ProyectoFinal.Consultas
{
    public partial class MantenimientoCliente : Form
    {
        public MantenimientoCliente()
        {
            InitializeComponent();
            Llenar_cliente();
        }

        Funciones funciones = new Funciones();

        private void Limpiar()
        {
            foreach (Control item in panel1.Controls)
            {
                if(item is TextBox || item is ComboBox)
                {
                    item.Text = "";
                }
                
            }

            foreach (Control item in gbDatosCliente.Controls)
            {
                if (item is TextBox || item is ComboBox)
                {
                    item.Text = "";
                    errorProvider.Clear();
                }
            }

            foreach (Control item in gbDireccion.Controls)
            {
                if (item is TextBox || item is ComboBox)
                {
                    item.Text = "";
                    errorProvider.Clear();
                }
            }

            foreach (Control item in gbRepresentante.Controls)
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
            Llenar_cliente();
        }

        public void Llenar_cliente()
        {
            funciones.BusquedaEspecifica("select a.id, a.nombre, a.DNI, c.numero 'telefono', a.trabajo, " +
                                         "concat(e.nombre, ', ', f.nombre)  'zona' , " +
                                         "Concat(b.sector ,' ' , b.calle ,' '  , b.numero, ' ' ,b.referencia_adicional) as 'direccion', " +
                                         "d.Nombre 'representante' , d.DNI 'DNI Representante', d.telefono 'Telefono Representante'" +
                                         "from cliente a " +
                                         "LEFT JOIN direccion_vs_cliente ab ON ab.cliente = a.id " +
                                         "LEFT JOIN direccion b ON ab.direccion =  b.id " +
                                         "LEFT JOIN cliente_vs_telefono ac ON ac.cliente = a.id " +
                                         "LEFT JOIN telefono c ON  ac.telefono = c.id " +
                                         "LEFT JOIN cliente_vs_representante ad ON ad.cliente = a.id " +
                                         "LEFT JOIN Representante d ON ad.representante = d.id " +
                                         "LEFT JOIN provincia e ON b.id_provincia = e.id " +
                                         "LEFT JOIN zona f ON a.zona = f.id " +
                                         "WHERE a.estado = 0", dgvCiente);

            
            funciones.BusquedaEspecificaComboBox("select nombre from provincia where estado = 0", "nombre", cbProvincia);

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            bool validacion = false;

            foreach (Control item in gbDatosCliente.Controls)
            {
                if (item is TextBox && item.Enabled == true || item is ComboBox && item.Enabled == true)
                {
                    if (item.Text != "" && item != txtCodigo)
                    {
                        errorProvider.Clear();

                    }
                    else if(item != txtCodigo)
                    {
                        errorProvider.SetError(item, "Campo requerido!");
                        break;
                    }
                }
            }

            foreach (Control item in gbDireccion.Controls)
            {
                if (item is TextBox && item.Enabled == true || item is ComboBox && item.Enabled == true)
                {
                    if (item.Text != "" && item != txtReferenciaAdicional)
                    {
                        errorProvider.Clear();

                    }
                    else if(item != txtReferenciaAdicional)
                    {
                        errorProvider.SetError(item, "Campo requerido!");
                        break;
                    }
                }
            }
            

            foreach (Control item in gbDatosCliente.Controls)
            {
                if(item is TextBox || item is ComboBox)
                {
                    if (errorProvider.GetError(item) == "")
                    {
                        validacion = true;
                    }
                    else
                    {
                        validacion = false;
                        break;
                    }
                }
            }

            foreach (Control item in gbDireccion.Controls)
            {
                if (item is TextBox || item is ComboBox)
                {
                    if (errorProvider.GetError(item) == "")
                    {
                        validacion = true;
                    }
                    else
                    {
                        validacion = false;
                        break;
                    }
                }
            }

            if (validacion)
            {
                funciones.comando("insert into direccion (id, id_provincia, sector, calle, numero, referencia_adicional) values" +
                            "('" + funciones.ObtenerUltimoID("direccion") + "', '"+ funciones.ObtenerID("select id from provincia where nombre = '" + cbProvincia.Text + "' and estado = 0") +"', " +
                            "'" + txtSector.Text + "', '" + txtCalle.Text + "', '" + txtNumeroPiso.Text + "', '" + txtReferenciaAdicional.Text + "') ");

                funciones.comando("insert into telefono values ('"+ funciones.ObtenerUltimoID("telefono") +"', '"+ txtTelefono.Text +"', '0')");
               
                funciones.comando("insert into representante values ('" + funciones.ObtenerUltimoID("representante") + "', '" + txtNombreRepresentante.Text + "', '" + txtDNIRepresentante.Text + "', '" + cbSexoRepresentante.Text + "', '" + txtTelefonoRepresentante.Text + "')");

                funciones.comando("insert into cliente " +
                    "(id, nombre, DNI, sexo, trabajo, zona, estado) values " +
                    "('"+ funciones.ObtenerUltimoID("cliente") +"', '"+ txtNombre.Text +"', " +
                    "'"+ txtDNI.Text +"', " +
                    " '"+ cbSexo.Text +"', '"+ txtTrabajo.Text +"', '"+ funciones.ObtenerID("select id from zona where estado = 0 and nombre = '"+ cbZona.Text +"'") +"', 0)");

                funciones.comando("insert into cliente_vs_representante values ('" + funciones.ObtenerID("select top 1 id from cliente order by id desc") + "', '" + funciones.ObtenerID("select top 1 id from representante order by id desc") + "')");
                funciones.comando("insert into direccion_vs_cliente values ('" + funciones.ObtenerID("select top 1 * from cliente order by id desc") + "', '" + funciones.ObtenerID("select top 1 * from direccion order by id desc") + "')");
                funciones.comando("insert into cliente_vs_telefono values " +
                    "('"+ funciones.ObtenerID("select top 1 id from cliente order by id desc") +"', '"+ funciones.ObtenerID("select top 1 id from telefono order by id desc") +"')");


                Llenar_cliente();
                Limpiar();
                MessageBox.Show("Registro insertado exitosamente!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Debe de llenar todos los campos requeridos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnEditarProvincia_Click(object sender, EventArgs e)
        {
            Mantenimiento.MantenimientoProvincia frmMantenimientoProvincia = new Mantenimiento.MantenimientoProvincia();
            frmMantenimientoProvincia.Show();
        }

        private void btnEditarZona_Click(object sender, EventArgs e)
        {
            Mantenimiento.MantenimientoZona frmMantenimientoZona = new Mantenimiento.MantenimientoZona();
            frmMantenimientoZona.Show();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            General.Inicio frmInicio = new General.Inicio();
            frmInicio.Show();
            Hide();
        }

        private void cbProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            funciones.BusquedaEspecificaComboBox("select a.nombre from zona a LEFT JOIN provincia b ON a.provincia = b.id where a.estado = 0 and b.nombre ='" + cbProvincia.Text +"'", "nombre", cbZona);
            
        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {
            funciones.ValidarCampo(txtTelefono, "telefono", errorProvider, "Debe de ingresar formato valido para telefono!");
        }

        private void txtDNI_TextChanged(object sender, EventArgs e)
        {
            funciones.ValidarCampo(txtDNI, "dni", errorProvider, "Debe de ingresar formato valido para documento de identidad!");
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            funciones.ValidarCampo(txtNombre, "nombre", errorProvider, "Debe de ingresar formato valido para representante!");
        }
        
        private void txtNombreRepresentante_TextChanged(object sender, EventArgs e)
        {
            funciones.ValidarCampo(txtNombreRepresentante, "nombre", errorProvider, "Debe de ingresar formato valido!");
        }

        private void txtDNIRepresentante_TextChanged(object sender, EventArgs e)
        {
            funciones.ValidarCampo(txtDNIRepresentante, "dni", errorProvider, "Debe de ingresar formato valido!");
        }

        private void txtTelefonoRepresentante_TextChanged(object sender, EventArgs e)
        {
            funciones.ValidarCampo(txtTelefonoRepresentante, "telefono", errorProvider, "Debe ingrear formato valido!");
        }

        private void cbSexo_TextChanged(object sender, EventArgs e)
        {
            funciones.ValidarCampo(cbSexo, "sexo", errorProvider, "Solo femenino y masculino");

        }

        private void cbSexoRepresentante_TextChanged(object sender, EventArgs e)
        {
            funciones.ValidarCampo(cbSexoRepresentante, "sexo", errorProvider, "Solo femenino y masculino");
        }

        private void dgvCiente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditar.Visible = true;
            btnEliminar.Visible = true;
            btnAgregar.Visible = false;

            txtCodigo.Text = dgvCiente.SelectedCells[0].Value.ToString();

            
            SqlCommand comando = new SqlCommand("" +
                "select" +
                " a.nombre, a.DNI, d.numero 'telefono', a.Sexo 'sexo_cliente', a.trabajo, e.nombre 'provincia', f.nombre 'zona'," +
                " b.sector, b.calle, b.numero 'numeroCasa', b.referencia_adicional," +
                " c.Nombre 'representante', c.Sexo 'sexo_representante', c.DNI 'dni_representante', c.telefono " +
                "FROM cliente a " +
                "LEFT JOIN direccion_vs_cliente ab ON ab.cliente = a.id " +
                "LEFT join direccion b ON ab.direccion = b.id " +
                "LEFT JOIN cliente_vs_representante ac ON ac.cliente = a.id " +
                "LEFT JOIN Representante c ON ac.representante = c.id " +
                "LEFT JOIN cliente_vs_telefono ad ON ad.cliente = a.id " +
                "LEFT JOIN telefono d ON ad.telefono = d.id " +
                "LEFT JOIN provincia e ON b.id_provincia = e.id " +
                "LEFT JOIN zona f ON a.zona = f.id " +
                "WHERE a.id = '" + txtCodigo.Text + "'", Funciones.Connection());
            SqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                txtNombre.Text = reader["nombre"].ToString();
                txtDNI.Text = reader["dni"].ToString();
                cbSexo.Text = reader["sexo_cliente"].ToString();
                txtTelefono.Text = reader["telefono"].ToString();
                txtTrabajo.Text = reader["trabajo"].ToString();

                cbProvincia.Text = reader["provincia"].ToString();
                cbZona.Text = reader["zona"].ToString();
                txtSector.Text = reader["sector"].ToString();
                txtCalle.Text = reader["calle"].ToString();
                txtNumeroPiso.Text = reader["numeroCasa"].ToString();
                txtReferenciaAdicional.Text = reader["referencia_adicional"].ToString();

                txtNombreRepresentante.Text = reader["representante"].ToString();
                cbSexoRepresentante.Text = reader["sexo_representante"].ToString();
                txtDNIRepresentante.Text = reader["dni_representante"].ToString();
                txtTelefonoRepresentante.Text = reader["telefono"].ToString();
            }
            Funciones.Connection().Close();

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text != "")
            {
                DialogResult desicion = MessageBox.Show("¿Seguro que desea eliminar este regsitro?, esta accion no se puede deshacer.", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if(desicion == DialogResult.Yes)
                {
                    funciones.comando("update cliente set estado = 1 where id = '" + txtCodigo.Text + "'");
                    MessageBox.Show("Registro eliminado correctamente!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Llenar_cliente();
                    Limpiar();
                }

            }
            else
            {
                errorProvider.SetError(txtCodigo, "Debe seleccionar un cliente!");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if(txtCodigo.Text != "")
            {
                errorProvider.Clear();

                funciones.comando("update telefono set " +
                                  "numero = '" + txtTelefono.Text + "' " +
                                  "where id = '" + funciones.ObtenerID("select telefono 'id' from cliente_vs_telefono where cliente = '" + txtCodigo.Text + "'") + "' and estado = 0");

                funciones.comando("update representante set " +
                                  "nombre = '" + txtNombreRepresentante.Text + "', " +
                                  "dni = '" + txtDNIRepresentante.Text + "', " +
                                  "sexo = '" + cbSexoRepresentante.Text + "', " +
                                  "telefono = '" + txtTelefonoRepresentante.Text + "' " +
                                  "WHERE id in (select b.id From cliente a LEFT JOIN cliente_vs_representante ab on ab.cliente = a.id LEFT JOIN Representante b on ab.representante = b.id where a.id = '"+ txtCodigo.Text +"')");

                funciones.comando("update direccion set " +
                                  "id_provincia = '"+ funciones.ObtenerID("select id from provincia where nombre = '"+ cbProvincia.Text +"'") +"', " +
                                  "sector = '"+ txtSector.Text +"', " +
                                  "calle = '"+ txtCalle.Text +"', " +
                                  "numero = '"+ txtNumeroPiso.Text +"', " +
                                  "referencia_adicional = '"+ txtReferenciaAdicional.Text +"' " +
                                  "where id in (select b.id from cliente a LEFT JOIN direccion_vs_cliente ab on ab.cliente = a.id LEFT JOIN direccion b on ab.direccion = b.id where a.id = '"+ txtCodigo.Text +"')");

                funciones.comando("update cliente set " +
                                  "nombre = '"+ txtNombre.Text +"', " +
                                  "dni = '"+ txtDNI.Text +"', " +
                                  "zona = '"+ funciones.ObtenerID("select top 1 id from zona where nombre = '"+ cbZona.Text +"'") +"', " +
                                  "trabajo = '"+ txtTrabajo.Text +"', " +
                                  "sexo = '"+ cbSexo.Text +"' " +
                                  "where id = '"+ txtCodigo.Text +"'");

                MessageBox.Show("Edicion exitosa!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Llenar_cliente();
                Limpiar();

            }
            else
            {
                errorProvider.SetError(txtCodigo, "Debe seleccionar un cliente!");
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if(txtBuscar.Text != "")
            {
                funciones.BusquedaEspecifica("select a.id, a.nombre, a.DNI, c.numero 'telefono', a.trabajo, " +
                                         "e.nombre 'provincia', f.nombre 'zona', b.sector, b.calle, b.numero, " +
                                         "d.Nombre 'representante' , d.DNI 'DNI Representante', d.Sexo 'Sexo representante', d.telefono 'Telefono Representante'" +
                                         "from cliente a " +
                                         "LEFT JOIN direccion_vs_cliente ab ON ab.cliente = a.id " +
                                         "LEFT JOIN direccion b ON ab.direccion =  b.id " +
                                         "LEFT JOIN cliente_vs_telefono ac ON ac.cliente = a.id " +
                                         "LEFT JOIN telefono c ON  ac.telefono = c.id " +
                                         "LEFT JOIN cliente_vs_representante ad ON ad.cliente = a.id " +
                                         "LEFT JOIN Representante d ON ad.representante = d.id " +
                                         "LEFT JOIN provincia e ON b.id_provincia = e.id " +
                                         "LEFT JOIN zona f ON a.zona = f.id " +
                                         "WHERE a.estado = 0 and (a.id like '%"+ txtBuscar.Text +"%' or a.nombre like '%"+ txtBuscar.Text +"%')", dgvCiente);
            }
            else
            {
                Llenar_cliente();
            }
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
