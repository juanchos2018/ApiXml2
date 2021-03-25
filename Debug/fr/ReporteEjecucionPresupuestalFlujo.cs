using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEstilo;
using Microsoft.Reporting.WinForms;
using CapaNegocios;

namespace Presupuesto.Reportes
{
    public partial class ReporteEjecucionPresupuestalFlujo : Form
    {
        public ReporteEjecucionPresupuestalFlujo()
        {
            InitializeComponent();
        }

        public bool iscorporative { set; get; }

        ReportViewer rv = new ReportViewer();
        CapaEstilo.ClsEstilo lo_Estilo = new ClsEstilo();
        NPresupuesto_Partidas pa = new NPresupuesto_Partidas();
        NPtEntidad pe = new NPtEntidad();
        NTablaGeneral tg = new NTablaGeneral();
        private void ReporteEjecucionPresupuestalFlujo_Load(object sender, EventArgs e)
        {
            lo_Estilo.Control_Estilos(this, FormWindowState.Maximized);
            lo_Estilo.Control_Evento(this);
            tg.IdGeneral = "SED";


            DataTable dtsed = tg.Lista(tg);

            groupBox4.Visible = iscorporative;

            CboSedes.DataSource = dtsed;
            CboSedes.ValueMember = "IdCodigo";
            CboSedes.DisplayMember = "Descripcion";
            CboMesInicial.SelectedIndex = 0;
            CboMesFinal.SelectedIndex = 0;
            pnlVisor.Controls.Add(rv);
            rv.Dock = DockStyle.Fill;
            rv.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            pe.identidad = "001";
            pe = pe.Registro(pe);
                        if (CboMesInicial.Text == "")
            {
                MessageBox.Show("Debe seleccionar un mes");
                return;
            }
            if (CboMesFinal.Text == "")
            {
                MessageBox.Show("Debe seleccionar un mes");
                return;
            }
            string[] ini = CboMesInicial.Text.Split('-');
            string[] fini = CboMesFinal.Text.Split('-');
            DataTable dt = new DataTable();
            if (iscorporative == false)
            {
                if (RbtnTodos.Checked == true)
                {
                    dt = pa.Resumen_Presupuesto(ini[0], fini[0]);
                }
                else
                {
                    dt = pa.Resumen_Presupuesto(ini[0].Trim(), fini[0].Trim(), CboSedes.SelectedValue.ToString().Trim());
                }

                string[] campos = { "RazonSocial", "MesInicial", "MesFinal" };
                string[] valores = { pe.nombre, CboMesInicial.Text, CboMesFinal.Text };

                rv.Reset();
                rv.Clear();
                rv = lo_Estilo.ImprimirReporteRV(rv, dt, Convert.ToByte(3), campos, valores, "RptDetallePresupuesto.rdl");
            }
            else {

                if (RbtnTodos.Checked == true)
                {
                    dt = pa.Reporte_corporativo(ini[0], fini[0]);
                }
                else
                {
                    dt = pa.Reporte_corporativo_sede(ini[0].Trim(), fini[0].Trim(), CboSedes.SelectedValue.ToString().Trim());
                }

                string[] campos = { "RazonSocial", "MesInicial", "MesFinal", "Showpartida" };
                string mostrar = "";
                if (RbtnTrue.Checked)
                { mostrar = "1"; }
                else {
                    mostrar = "0";
                }
                string[] valores = { pe.nombre, CboMesInicial.Text, CboMesFinal.Text,mostrar};

                rv.Reset();
                rv.Clear();
                if (RbtnDetallado.Checked == true)
                {
                    rv = lo_Estilo.ImprimirReporteRV(rv, dt, Convert.ToByte(4), campos, valores, "Reporte_ejecucion_corporativo.rdl");
                }
                else {
                    rv = lo_Estilo.ImprimirReporteRV(rv, dt, Convert.ToByte(4), campos, valores, "Reporte_ejecucion_corporativo_Resumen.rdl");
                }
                
                
            }


        }

        private void RbtnSedes_CheckedChanged(object sender, EventArgs e)
        {
            CboSedes.Enabled = RbtnSedes.Checked;
        }
    }
}
