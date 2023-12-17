using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal.General
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
            timer1.Start();
            lblFecha.Text = DateTime.Today.ToLongDateString();
            lblUsuario.Text = user.UserName;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToLongTimeString();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Consultas.ConsultaUsuarios frmConsultasUsuarios = new Consultas.ConsultaUsuarios();
            frmConsultasUsuarios.Show();
            Hide();
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Consultas.MantenimientoCliente fmrConsultaCliente = new Consultas.MantenimientoCliente();
            fmrConsultaCliente.Show();
            Hide();
            
        }

        private void PrestamosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Movimientos.Calculos_de_prestamos frmPrestamos = new Movimientos.Calculos_de_prestamos();
            AddOwnedForm(frmPrestamos);
            frmPrestamos.Show();
            frmPrestamos.Focus();

            this.Hide();
        }

        private void cobradorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mantenimiento.MantenimientoCobrador frmMantenimientoCobrador = new Mantenimiento.MantenimientoCobrador();
            AddOwnedForm(frmMantenimientoCobrador);
            frmMantenimientoCobrador.Show();
            frmMantenimientoCobrador.Focus();

            
        }

        private void prestamoPorRangoDeFechasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Consultas.ConsultaPrestamoPorRangoFecha frmRangoFecha = new Consultas.ConsultaPrestamoPorRangoFecha();
            AddOwnedForm(frmRangoFecha);
            frmRangoFecha.Show();
            frmRangoFecha.Focus();

            
        }

        private void cobradorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Consultas.ConsultaCobrador frmConsultasCobrador = new Consultas.ConsultaCobrador();
            AddOwnedForm(frmConsultasCobrador);
            frmConsultasCobrador.Show();
            frmConsultasCobrador.Focus();
        }

        private void consultaDeClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Consultas.ConsultaClientes frmConsultaCliente = new Consultas.ConsultaClientes();
            AddOwnedForm(frmConsultaCliente);
            frmConsultaCliente.Show();
            frmConsultaCliente.Focus();
        }

        private void empleadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Consultas.ConsultaEmpleado frmConsultaEmpleado = new Consultas.ConsultaEmpleado();
            AddOwnedForm(frmConsultaEmpleado);
            frmConsultaEmpleado.Show();
            frmConsultaEmpleado.Focus();
        }
    }
}
