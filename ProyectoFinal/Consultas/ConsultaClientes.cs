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
    public partial class ConsultaClientes : Form
    {
        public ConsultaClientes()
        {
            InitializeComponent();
            LlenarCliente();
        }

        Funciones funciones = new Funciones();

        private void LlenarCliente()
        {
            funciones.BusquedaEspecifica("select a.id, a.nombre, a.DNI, c.numero 'telefono', a.trabajo, " +
                                         "e.nombre 'provincia', f.nombre 'zona' , " +
                                         "Concat(b.sector ,' ' , b.calle ,' '  , b.numero, ' ' ,b.referencia_adicional) as 'direccion' " +
                                         "from cliente a " +
                                         "LEFT JOIN direccion_vs_cliente ab ON ab.cliente = a.id " +
                                         "LEFT JOIN direccion b ON ab.direccion =  b.id " +
                                         "LEFT JOIN cliente_vs_telefono ac ON ac.cliente = a.id " +
                                         "LEFT JOIN telefono c ON  ac.telefono = c.id " +
                                         "LEFT JOIN cliente_vs_representante ad ON ad.cliente = a.id " +
                                         "LEFT JOIN Representante d ON ad.representante = d.id " +
                                         "LEFT JOIN provincia e ON b.id_provincia = e.id " +
                                         "LEFT JOIN zona f ON a.zona = f.id " +
                                         "WHERE a.estado = 0", dgvCliente);

            funciones.BusquedaEspecifica("select a.id, a.nombre, a.dni, a.sexo, a.telefono " +
                                         "from representante a " +
                                         "LEFT JOIN cliente_vs_representante ab ON ab.representante = a.id " +
                                         "LEFT JOIN cliente b ON ab.cliente = b.id ", dgvRepresentante);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text != "")
            {
                funciones.BusquedaEspecifica("select " +
                                         "id, nombre, dni, trabajo " +
                                         "from cliente " +
                                         "Where estado = 0 and " +
                                         "id like '%" + txtBuscar.Text + "%' " +
                                         "or nombre like '%" + txtBuscar.Text + "%' " +
                                         "or dni like '" + txtBuscar.Text + "' " +
                                         "or trabajo like '" + txtBuscar.Text + "'", dgvCliente);
            }
            else
            {
                LlenarCliente();
            }
        }

        private void dgvCliente_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Movimientos.Calculos_de_prestamos MC = Owner as Movimientos.Calculos_de_prestamos;

            if(MC != null)
            {
                MC.txtCodigoCliente.Text = dgvCliente.SelectedCells[0].Value.ToString();
                MC.txtNombreCliente.Text = dgvCliente.SelectedCells[1].Value.ToString();
                MC.txtZonaCliente.Text = dgvCliente.SelectedCells[6].Value.ToString();

                MC.txtCodigoRepresentante.Text = dgvRepresentante.SelectedCells[0].Value.ToString();
                MC.txtNombreRepresentante.Text = dgvRepresentante.SelectedCells[1].Value.ToString();
                MC.txtTelefonoRepresentante.Text = dgvRepresentante.SelectedCells[2].Value.ToString();

                this.Close();
            }
        }
    }
}
