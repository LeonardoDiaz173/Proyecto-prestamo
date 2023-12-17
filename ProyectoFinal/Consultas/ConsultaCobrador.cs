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
    public partial class ConsultaCobrador : Form
    {
        public ConsultaCobrador()
        {
            InitializeComponent();
            Llenar();
        }

        Funciones funciones = new Funciones();

        private void Llenar()
        {
            funciones.BusquedaEspecifica("select " +
                                         "a.id, " +
                                         "b.nombre, " +
                                         "e.numero 'telefono', " +
                                         "concat(c.nombre, ',', d.nombre) as 'zona' " +
                                         "from cobradores a " +
                                         "LEFT JOIN empleado b ON a.empleado = b.id " +
                                         "LEFT JOIN provincia c ON a.provincia = c.id " +
                                         "LEFT JOIN zona d ON a.zona = d.id " +
                                         "LEFT JOIN empleado_vs_telefono be ON be.empleado = b.id " +
                                         "LEFT JOIN telefono e ON be.telefono = e.id",dgvCobrador);
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            if (txtBusqueda.Text != "")
            {
                funciones.BusquedaEspecifica("select " +
                                         "a.id, " +
                                         "b.nombre, " +
                                         "e.numero" +
                                         "concat(c.nombre, ',', d.nombre) as 'zona' " +
                                         "from cobradores a " +
                                         "LEFT JOIN empleado b ON a.empleado = b.id " +
                                         "LEFT JOIN provincia c ON a.provincia = c.id " +
                                         "LEFT JOIN zona d ON a.zona = d.id " +
                                         "LEFT JOIN empleado_vs_telefono be ON be.empleado = b.id " +
                                         "LEFT JOIN telefono e ON be.telefono = e.id " +
                                         "WHERE b.estado = 0 and (a.id like '%"+ txtBusqueda.Text +"%', " +
                                         "b.nombre like '%"+ txtBusqueda.Text +"%', " +
                                         "c.nombre like '%"+txtBusqueda.Text+"%', " +
                                         "d.nombre like '"+ txtBusqueda.Text +"')", dgvCobrador);
            }
            else
            {
                Llenar();
            }
        }

        private void dgvCobrador_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Movimientos.Calculos_de_prestamos MC = Owner as Movimientos.Calculos_de_prestamos;

            if (MC != null)
            {
                MC.txtCodigoCobrador.Text = dgvCobrador.SelectedCells[0].Value.ToString();
                MC.txtNombreCobrador.Text = dgvCobrador.SelectedCells[1].Value.ToString();
                MC.txtTelefonoCobrador.Text = dgvCobrador.SelectedCells[2].Value.ToString();

                this.Close();
            }
        }
    }
}
