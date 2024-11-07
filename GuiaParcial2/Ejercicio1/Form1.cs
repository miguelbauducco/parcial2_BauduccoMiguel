using ComercioLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejercicio1
{
    public partial class Form1 : Form
    {
        Comercio comercio = new Comercio();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FileStream fs = null;
            try
            {
                if (File.Exists("Datos.bin"))
                {
                    fs = new FileStream("Datos.bin", FileMode.OpenOrCreate, FileAccess.Read);
                    BinaryFormatter bf = new BinaryFormatter();
                    comercio = bf.Deserialize(fs) as Comercio;
                }
            }
            catch { }
            finally
            {
                if (fs != null) fs.Close();
            }

            if (comercio == null)
                comercio = new Comercio();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream("Datos.bin", FileMode.OpenOrCreate, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, comercio);
            }
            catch { }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (rbPagos.Checked) 
            {
                Ticket t = comercio.AtenderTicket(0);
                listBox1.Items.Remove(t);
                
            }
            else if (rbCompras.Checked) 
            {
                Ticket t = comercio.AtenderTicket(1);
                listBox1.Items.Remove(t);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //IMPORTAR

            openFileDialog1.Title = "Importando archivos";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog1.FileName;
                FileStream fs = null;
                StreamReader sr = null;

                try
                {
                    fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
                    sr = new StreamReader(fs);
                    sr.ReadLine();

                    while (sr.EndOfStream == false)
                    {
                        string linea = sr.ReadLine();
                        string[] campos = linea.Split(';');
                        int nro = Convert.ToInt32(campos[0]);
                        int dni = Convert.ToInt32(campos[1]);
                        double saldo = Convert.ToDouble(campos[2]);

                        comercio.AgregarCc(nro, dni, saldo);


                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR EN LA IMPORTACION");
                }
                finally
                {
                    if (sr != null) sr.Close();
                    if (fs != null) fs.Close();
                }

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ticket t = null;
            if (checkBox1.Checked)
            {
                int nroCuenta = Convert.ToInt32(txtCtacte.Text);
                CuentaCorriente cc = comercio.BuscarCuenta(nroCuenta);
                if (cc != null)
                {
                    t = new Pago(cc);
                }
            }
            else
            {
                int dni = Convert.ToInt32(tbDni.Text);
                try
                {
                    if (dni < 3000000 || dni > 45000000)
                        throw new DniInvalidoException("ERROR DNI INVALIDO");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR: DNI FUERA DE RANGO PERMITIDO");
                } 
                t = new Cliente(dni);
            }
            if (t != null)
            {
                comercio.AgregarTicket(t);
                listBox1.Items.Add(t);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //EXPORTAR

            saveFileDialog1.Title = "Exportando archivos";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog1.FileName;
                FileStream fs = null;
                StreamWriter sw = null;

                try
                {
                    fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                    sw = new StreamWriter(fs);
                    sw.WriteLine("TIPO;NRO;DNI;SALDO");
                    

                   for(int n=0; n<comercio.CantTicket;n++) 
                    {
                        Ticket t = comercio.VerTicketAtendido(n);
                        string linea = t.ToString();
                        sw.Write(linea);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR EN LA IMPORTACION");
                }
                finally
                {
                    if (sw != null) sw.Close();
                    if (fs != null) fs.Close();
                }

            }

        }
    }
}
