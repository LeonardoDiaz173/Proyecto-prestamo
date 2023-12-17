using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal.Movimientos
{
    public partial class Calculos_de_prestamos : Form
    {
        public Calculos_de_prestamos()
        {
            InitializeComponent();
        }

        Funciones funciones = new Funciones();

        private void limpiar()
        {
            foreach (Control item in gbCliente.Controls)
            {
                if (item is TextBox || item is ComboBox)
                {
                    item.Text = "";
                }
            }

            foreach (Control item in gbDatosPrestamo.Controls)
            {
                if (item is TextBox || item is ComboBox)
                {
                    item.Text = "";
                }
            }

            foreach (Control item in gbEmpleado.Controls)
            {
                if (item is TextBox || item is ComboBox)
                {
                    item.Text = "";
                }
            }

            dgvPrestamos.Rows.Clear();

        }


        private void txtBuscarCliente_Click(object sender, EventArgs e)
        {
            Consultas.ConsultaClientes frmCliente = new Consultas.ConsultaClientes();
            AddOwnedForm(frmCliente);
            frmCliente.Show();
            frmCliente.Focus();
        }

        private void btnBuscarAgente_Click(object sender, EventArgs e)
        {
            Consultas.ConsultaEmpleado frmEmpleado = new Consultas.ConsultaEmpleado();
            AddOwnedForm(frmEmpleado);
            frmEmpleado.Show();
            frmEmpleado.Focus();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            dgvPrestamos.Rows.Clear();

            bool validar = false;

            foreach (Control item in gbDatosPrestamo.Controls)
            {
                if (item is TextBox || item is ComboBox)
                {
                    if (item.Text == "")
                    {
                        errorProvider.SetError(item, "Debe de rellenar los campos necesarios!");
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
                #region Interes Fijo
                int cuotas = int.Parse(txtCuotas.Text);
                float monto = float.Parse(txtMonto.Text), interes = float.Parse(txtIntereses.Text);
                float pagocita = 0, intereses = 0, capital = 0, montoPagado = monto, balance = (cuotas * ((interes / 100) * monto)) + monto;
                DateTime fecha = Convert.ToDateTime(dtpFechaPrestamo.Value);
                
                if (cbTipoPrestamo.Text == "Interes Fijo")
                {
                    for (int i = 0; i < cuotas; i++)
                    {
                        fecha = fecha.AddDays(int.Parse(txtDia.Text));
                        dgvPrestamos.Rows.Add((i + 1) + "/" + cuotas, fecha.ToShortDateString(), montoPagado, (monto / cuotas) + ((interes / 100) * monto), (monto / cuotas), (interes / 100) * monto, balance);
                        balance -= (monto / cuotas) + ((interes / 100) * monto);
                        montoPagado -= (monto / cuotas);
                    }
                }
                #endregion
            }
        }

        private void cbDiaCuota_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbDiaCuota.Text)
            {
                case "Mensual":
                    txtDia.Text = "30";
                    break;
                case "Quincenal":
                    txtDia.Text = "15";
                    break;
                case "Semanal":
                    txtDia.Text = "7";
                    break;
                case "Diario":
                    txtDia.Text = "1";
                    break;
                default:
                    break;
            }
        }

        private void btnBuscarCobrador_Click(object sender, EventArgs e)
        {
            Consultas.ConsultaCobrador frmConsultaCobrador = new Consultas.ConsultaCobrador();
            AddOwnedForm(frmConsultaCobrador);
            frmConsultaCobrador.Show();
            frmConsultaCobrador.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool validar = false;

            validar = funciones.ValidarCampo(gbCliente, errorProvider);
            if (validar)
            {
                validar = funciones.ValidarCampo(gbEmpleado, errorProvider);
                if (validar)
                {
                    validar = funciones.ValidarCampo(gbDatosPrestamo, errorProvider);
                    if (validar)
                    {
                        DialogResult confirmar = MessageBox.Show("¿Desea confirmar este prestamo?", "ATENCION", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                        if (confirmar == DialogResult.Yes)
                        {
                            string comando = "insert into prestamos values(" +
                                          "'" + funciones.ObtenerUltimoID("prestamos") + "', " +
                                          "'" + txtCodigoCliente.Text + "', " +
                                          "'" + txtCodigoCobrador.Text + "', " +
                                          "'" + txtCodigoAgente.Text + "', " +
                                          "'" + txtCodigoRepresentante.Text + "', " +
                                          "'" + funciones.ObtenerID("select id from zona where estado = 0 and nombre = '"+ txtZonaCliente.Text +"'") + "', " +
                                          "'" + txtDetalle.Text + "', " +
                                          "'Peso Dominicano', " +
                                          "'" + dtpFechaPrestamo.Value.ToShortDateString() + "', " +
                                          "'" + cbTipoPrestamo.Text + "', " +
                                          "'0', " +
                                          "'" + dgvPrestamos[6, 0].Value.ToString() + "'," +
                                          "'" + txtMonto.Text + "', " +
                                          "'" + txtIntereses.Text + "', " +
                                          "'" + txtCuotas.Text + "', " +
                                          "'" + cbDiaCuota.Text + "', " +
                                          "'" + txtDia.Text + "'," +
                                          "'pendiente')";

                            funciones.comando(comando);
                            MessageBox.Show("Prestamo realizado exitosamente!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            limpiar();
                        }
                    }
                }
            }
            
            
            
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            General.Inicio frmInicio = new General.Inicio();
            AddOwnedForm(frmInicio);
            frmInicio.Show();
            frmInicio.Focus();
        }
    }
}
