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
    public partial class ConsultaPrestamoPorRangoFecha : Form
    {
        public ConsultaPrestamoPorRangoFecha()
        {
            InitializeComponent();
            llenar();
        }

        Funciones funciones = new Funciones();

        private void llenar()
        {
           
            funciones.BusquedaEspecifica("select " +
                                         "a.id, b.nombre, d2.nombre 'agente', d.nombre 'cobrador', a.fecha_pre, a.tipo_pre, a.cuotas_pagada_pre, " +
                                         "a.balance_pre, a.monto_pre, a.interes_pre, a.cuotas_pre, a.dia_pre, a.dias_cuota_pre, a.estado_pre " +
                                         "from prestamos a " +
                                         "LEFT JOIN cliente b ON a.cod_cli_pre = b.id " +
                                         "LEFT join cobradores c ON a.cobrador_cob_pre = c.id " +
                                         "LEFT JOIN empleado d ON c.empleado = d.id " +
                                         "LEFT JOIN empleado d2 ON a.agente_age_pre = d2.id ", dgvConsultaPorfecha);
        }

        private void txtBuscar_Click(object sender, EventArgs e)
        {
            funciones.BusquedaEspecifica("select " +
                                         "a.id, b.nombre, d2.nombre 'agente', d.nombre 'cobrador', a.fecha_pre, a.tipo_pre, a.cuotas_pagada_pre, " +
                                         "a.balance_pre, a.monto_pre, a.interes_pre, a.cuotas_pre, a.dia_pre, a.dias_cuota_pre, a.estado_pre " +
                                         "from prestamos a " +
                                         "LEFT JOIN cliente b ON a.cod_cli_pre = b.id " +
                                         "LEFT join cobradores c ON a.cobrador_cob_pre = c.id " +
                                         "LEFT JOIN empleado d ON c.empleado = d.id " +
                                         "LEFT JOIN empleado d2 ON a.agente_age_pre = d2.id " +
                                         "WHERE a.fecha_pre between '"+ dtpFechaDesde.Value.ToShortDateString() +"' and '"+ dtpFechaHasta.Value.ToShortDateString() +"'", dgvConsultaPorfecha);
        }

        private void btnBuscarEstado_Click(object sender, EventArgs e)
        {
            if(cbEstadoCliente.Text != "")
            {
                funciones.BusquedaEspecifica("select " +
                                         "a.id, b.nombre, d2.nombre 'agente', d.nombre 'cobrador', a.fecha_pre, a.tipo_pre, a.cuotas_pagada_pre, " +
                                         "a.balance_pre, a.monto_pre, a.interes_pre, a.cuotas_pre, a.dia_pre, a.dias_cuota_pre, a.estado_pre " +
                                         "from prestamos a " +
                                         "LEFT JOIN cliente b ON a.cod_cli_pre = b.id " +
                                         "LEFT join cobradores c ON a.cobrador_cob_pre = c.id " +
                                         "LEFT JOIN empleado d ON c.empleado = d.id " +
                                         "LEFT JOIN empleado d2 ON a.agente_age_pre = d2.id " +
                                         "WHERE a.estado_pre = '"+ cbEstadoCliente.Text +"'",dgvConsultaPorfecha);
            }
            else
            {
                llenar();
            }
        }
    }
}
