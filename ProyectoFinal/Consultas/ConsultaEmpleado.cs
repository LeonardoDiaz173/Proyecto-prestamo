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
    public partial class ConsultaEmpleado : Form
    {
        public ConsultaEmpleado()
        {
            InitializeComponent();
            LlenarEmpleado();
        }

        Funciones funciones = new Funciones();

        private void LlenarEmpleado()
        {
            funciones.BusquedaEspecifica("select " +
                                         "a.id, " +
                                         "a.nombre, " +
                                         "a.sexo, " +
                                         "a.DNI, " +
                                         "b.numero 'Telefono', " +
                                         "concat(e.nombre, c.sector, c.calle, c.numero, c.referencia_adicional) 'Direccion', " +
                                         "d.nombre 'tipoempleado' " +
                                         "from empleado a " +
                                         "LEFT JOIN empleado_vs_telefono ab ON ab.empleado = a.id " +
                                         "LEFT JOIN telefono b ON ab.telefono = b.id " +
                                         "LEFT JOIN empleado_vs_direccion ac ON ac.empleado = a.id " +
                                         "LEFT JOIN direccion c ON ac.direccion = c.id " +
                                         "LEFT JOIN tipo_empleado d ON a.tipo_empleado = d.id " +
                                         "LEFT JOIN provincia e ON c.id_provincia = e.id", dgvEmpleado);

            
        }

        private void dgvEmpleado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Movimientos.Calculos_de_prestamos MC = Owner as Movimientos.Calculos_de_prestamos;
            Mantenimiento.MantenimientoCobrador MCobrador = Owner as Mantenimiento.MantenimientoCobrador;

            if (MC != null)
            {
                MC.txtCodigoAgente.Text = dgvEmpleado.SelectedCells[0].Value.ToString();
                MC.txtNombreAgente.Text = dgvEmpleado.SelectedCells[1].Value.ToString();
                MC.txtTelefonoAgente.Text = dgvEmpleado.SelectedCells[2].Value.ToString();

                this.Close();
            }
            else if(MCobrador != null)
            {
                MCobrador.txtCodigoEmpleado.Text = dgvEmpleado.SelectedCells[0].Value.ToString();
                MCobrador.txtNombreEmpleado.Text = dgvEmpleado.SelectedCells[1].Value.ToString();

                this.Close();
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text != "")
            {
                funciones.BusquedaEspecifica("select " +
                                         "a.id, " +
                                         "a.nombre, " +
                                         "a.sexo, " +
                                         "a.DNI, " +
                                         "b.numero 'Telefono', " +
                                         "concat(e.nombre, c.sector, c.calle, c.numero, c.referencia_adicional) 'Direccion', " +
                                         "d.nombre 'tipoempleado' " +
                                         "from empleado a " +
                                         "LEFT JOIN empleado_vs_telefono ab ON ab.empleado = a.id " +
                                         "LEFT JOIN telefono b ON ab.telefono = b.id " +
                                         "LEFT JOIN empleado_vs_direccion ac ON ac.empleado = a.id " +
                                         "LEFT JOIN direccion c ON ac.direccion = c.id " +
                                         "LEFT JOIN tipo_empleado d ON a.tipo_empleado = d.id " +
                                         "LEFT JOIN provincia e ON c.id_provincia = e.id " +
                                         "WHERE a.estado = 0 and (a.id like '%" + txtBuscar.Text + "%' or " +
                                         "a.nombre like '%"+ txtBuscar.Text +"%' or " +
                                         "a.dni like '%"+ txtBuscar.Text +"%')", dgvEmpleado);



            }
            else
            {
                LlenarEmpleado();
            }
        }
    }
}
