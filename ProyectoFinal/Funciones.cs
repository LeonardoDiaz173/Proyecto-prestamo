using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ProyectoFinal
{
    class Funciones
    {
        public static SqlConnection Connection()
        {
            try
            {
                SqlConnection connection = new SqlConnection(
                    "server=DESKTOP-I911LMR;database=EmpresaPrestamos;integrated security=true");

                connection.Open();
                return connection;
            }
            catch (Exception Error)
            {
                System.Windows.Forms.MessageBox.Show("Error: "+Error.Message,"ERROR",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
                throw;
            }
        }

        public void BusquedaGenerar(string tabla, DataGridView dgvTabla)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("Select * from "+ tabla +" where estado = 0", Connection());
                DataTable data = new DataTable();
                adapter.Fill(data);

                dgvTabla.DataSource = data;

            }
            catch (Exception Err)
            {
                MessageBox.Show("Error: " + Err.Message);
                throw;
            }
        }

        public void BusquedaEspecifica(string condicion, DataGridView dgvTabla)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(condicion, Connection());
                DataTable data = new DataTable();
                adapter.Fill(data);
                dgvTabla.DataSource = data;

            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            finally
            {
                Connection().Close();
            }
        }

        public void BusquedaEspecificaComboBox(string condicion, string campo, ComboBox cbCombo)
        {
            try
            {
                
                SqlCommand command = new SqlCommand(condicion, Connection());
                
                SqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    cbCombo.Items.Add(reader[campo].ToString());
                }
                
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            finally
            {
                Connection().Close();
            }
        }

        public int ObtenerID(string comando)
        {
            try
            {
                string cmd = comando;
                SqlCommand command = new SqlCommand(cmd, Connection());
                SqlDataReader reader = command.ExecuteReader();

                int registroClave = 0;
                while (reader.Read())
                {
                    registroClave = int.Parse(reader["id"].ToString());
                }
                return registroClave;
            }
            catch (Exception err)
            {
                MessageBox.Show("ERROR " + err.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }


        public int ObtenerUltimoID(string tabla)
        {
            try
            {
                string comando = "SELECT TOP 1 id FROM " + tabla + " ORDER BY id DESC";
                SqlCommand command = new SqlCommand(comando, Connection());
                SqlDataReader reader = command.ExecuteReader();
                int ultimoRegisto = 0;

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ultimoRegisto = int.Parse(reader["id"].ToString()) + 1;
                    }
                }
                else
                {
                    ultimoRegisto = 1;
                }
                Connection().Close();
                return ultimoRegisto;
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "ERROR", MessageBoxButtons.OK);
                throw;
                
            }
            finally
            {
                Connection().Close();
               
            }
        }

        public void comando(string cmd)
        {
            try
            {
                
                SqlCommand command = new SqlCommand(cmd, Connection());
                command.ExecuteNonQuery();
                Connection().Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " +error.Message);
                throw;
            }
        }

        

        public void ValidarCampo(Control entrada, string tipoValidacion, ErrorProvider errorProvider, string Error)
        {
            
            Regex regex;

            switch (tipoValidacion)
            {
                case "telefono":
                    regex = new Regex("^([0-9]{3})-([0-9]{3})-([0-9]{4})$");
                    if (!regex.IsMatch(entrada.Text))
                        errorProvider.SetError(entrada, Error);
                    else
                        errorProvider.Clear();
                    break;

                case "dni":
                    regex = new Regex("^([0-9]{3})-([0-9]{7})-([0-9]{1})$");
                    if (!regex.IsMatch(entrada.Text))
                        errorProvider.SetError(entrada, Error);
                    else
                        errorProvider.Clear();
                    break;

                case "nombre":
                    regex = new Regex("^([a-zA-Z ]{3,30})$");
                    if (!regex.IsMatch(entrada.Text))
                        errorProvider.SetError(entrada, Error);
                    else
                        errorProvider.Clear();
                    break;
                case "sexo":

                    if (entrada.Text.Equals("m") || entrada.Text.Equals("M") || entrada.Text.Equals("f") || entrada.Text.Equals("F"))
                    {
                        errorProvider.Clear();
                    }
                    else
                        errorProvider.SetError(entrada, Error);
                    break;
                case "password":
                    regex = new Regex("^([a-zA-Z0-9 ]{8,30})");
                    if (!regex.IsMatch(entrada.Text))
                        errorProvider.SetError(entrada, Error);
                    else
                        errorProvider.Clear();
                    break;
                case "correo":
                    regex = new Regex("^[a-zA-Z0-9_.]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$");
                    if (!regex.IsMatch(entrada.Text))
                        errorProvider.SetError(entrada, Error);
                    else
                        errorProvider.Clear();
                    break;

                default:
                    break;
            }
        }

        public bool ValidarCampo(Control agrupador, ErrorProvider errorProvider)
        {
            bool validar = false;
            foreach (Control item in agrupador.Controls)
            {
                if (item is TextBox && item.Enabled == true || item is ComboBox && item.Enabled == true)
                {
                    if (item.Text != "")
                    {
                        errorProvider.Clear();
                        validar = true;
                    }
                    else
                    {
                        errorProvider.SetError(item, "Campo requerido!");
                        validar = false;
                        break;
                    }
                }
            }

            return validar;
        }
    }
}
