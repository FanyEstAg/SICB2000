using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace Punto_de_Venta.MODULOS
{
    public partial class md_Login : Form
    {
        int cont;
        
        public md_Login()
        {
            InitializeComponent();
        }
        
        public void dibujarUsuarios()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = CONEXION.Conexion_Maestra.conexion;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("SELECT * FROM Usuario WHERE Estado = 'ACTIVO'", con); //Consulta directa
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())//Bucle para recorrer la lectura de la consulta
            {
                Label lbl = new Label(); //nombre de usuario
                Panel panel = new Panel(); //contenedor
                PictureBox pb = new PictureBox(); //icono de usuario

                lbl.Text = rdr["Login"].ToString(); //Jalar columna Login
                lbl.Name = rdr["idUsuario"].ToString(); //Id usuario
                lbl.Size = new System.Drawing.Size(175, 25); //tamaño del label
                lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 13); //Tipo de letra
                lbl.BackColor = Color.FromArgb(20, 20, 20); //Color del label
                lbl.ForeColor = Color.White;//color de la letra
                lbl.Dock = DockStyle.Bottom;//Ubicaicón
                lbl.TextAlign = ContentAlignment.MiddleCenter; //Alienación del texto
                
                lbl.Cursor = Cursors.Hand;//Tipo de cursor al pasar or la etiqueta

                panel.Size = new System.Drawing.Size(155, 167);//Dibujar contenedor
                panel.BorderStyle = BorderStyle.None;//Sin borde
                panel.BackColor = Color.FromArgb(20, 20, 20);//color de fondo

                pb.Size = new System.Drawing.Size(175, 132); //Dibujar picture box para el icono
                pb.Dock = DockStyle.Top;//Ubicación
                pb.BackgroundImage = null;//Sin color de imagen
                byte[] bi = (Byte[])rdr["Icono"];//extracción de bytes del icono
                MemoryStream ms = new MemoryStream(bi); //Guardar en memoria
                pb.Image = Image.FromStream(ms);//Insertar imagen 
                pb.SizeMode = PictureBoxSizeMode.Zoom;//Modo de ajuste de la imagen
                pb.Tag = rdr["Login"].ToString();//Guardar en caché el Login
                pb.Cursor = Cursors.Hand;//Tipo de cursor al pasar por el picturebox
                lbl.Tag = Image.FromStream(ms);//Guardar la imagen en el tag del label

                panel.Controls.Add(lbl);//Añadir al panel la etiqueta
                panel.Controls.Add(pb);//Añadir al panel el picturebox
                panel.BringToFront();//Traer el label adelante
                flowLayoutPanel1.Controls.Add(panel);//Añadir el panel al flowLayout 
                //Evento handler: no hay datos del evento, se usa cuando se programa desde 0
                lbl.Click += new EventHandler(miEventoLabel);// Evento al hacer click
                pb.Click += new EventHandler(miEventoImagen);//Evento al hacer click
            }
            con.Close();//cerrar conexión
        }
        private void miEventoImagen(System.Object sender, EventArgs e)
        {
            lblUsuario.Text = ((PictureBox)sender).Tag.ToString();//Trer propiedad tag del label
            pbIconoLogin.Image= ((PictureBox)sender).Image;
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void miEventoLabel(System.Object sender, EventArgs e)
        {
            lblUsuario.Text = ((Label)sender).Text;//Trer propiedad text del label
            pbIconoLogin.Image = (Image)((Label)sender).Tag;//extarer el label del tag con conversión explicita
            panel2.Visible = true;
            panel1.Visible = false;
        }


        private void md_Login_Load(object sender, EventArgs e)
        {
            dibujarUsuarios();//Arranque con usuarios
            panel2.Visible = false;
        }

        private void txtContrasenia_TextChanged(object sender, EventArgs e)
        {
            iniciarSesion();
        }

        private void iniciarSesion()
        {
            cargarUsuarios();
            contar();
            if (cont > 0)
            {
                Caja.md_AperturaCaja frm_AperturaCaja = new Caja.md_AperturaCaja();//Acceso a Caja
                this.Hide();
                frm_AperturaCaja.ShowDialog();
            }
        }

        private void cargarUsuarios()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.Conexion_Maestra.conexion;
                con.Open();

                da = new SqlDataAdapter("validar_usuario", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@password", txtContrasenia.Text);
                da.SelectCommand.Parameters.AddWithValue("@login", lblUsuario.Text);

                da.Fill(dt);
                
                dgvUsuarios.DataSource = dt;
                //MessageBox.Show("" + dgvUsuarios.Rows.Count);
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void contar()
        {
            cont = dgvUsuarios.Rows.Count;
        }

        private void mostrar_correos()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.Conexion_Maestra.conexion;
                con.Open();

                da = new SqlDataAdapter("SELECT Correo FROM Usuario WHERE Estado='ACTIVO'", con);//Extraer Correos

                da.Fill(dt);
                cbxCorreo.DisplayMember = "Correo";//Variable que irá
                cbxCorreo.ValueMember = "Correo";//Traer correo
                cbxCorreo.DataSource = dt; //pasa rlos datos al combobox
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOlvContrasenia_Click(object sender, EventArgs e)
        {
            panelRestaurarCuenta.Visible = true;
            mostrar_correos();

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            panelRestaurarCuenta.Visible = false;

        }
        private void mostrar_usuarios_por_correo()
        {
            try
            {
                string resultado;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.Conexion_Maestra.conexion;
                SqlCommand da = new SqlCommand("buscar_USUARIO_por_correo", con);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@correo", cbxCorreo.Text);
                con.Open();
                lblResultadoContraseña.Text = Convert.ToString(da.ExecuteScalar());//Obtener datos unicos y conversión a String
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Método para enviar correro
        internal void enviarCorreo(string emisor, string password, string mensaje, string asunto, string destinatario, string ruta)
        {
            try
            {
                MailMessage correos = new MailMessage();
                SmtpClient envios = new SmtpClient();
                correos.To.Clear();
                correos.Body = "";
                correos.Subject = "";
                correos.Body = mensaje;
                correos.Subject = asunto;
                correos.IsBodyHtml = true;
                correos.To.Add((destinatario));
                correos.From = new MailAddress(emisor);
                envios.Credentials = new NetworkCredential(emisor, password);

                envios.Host = "smtp.gmail.com";
                envios.Port = 587; //puerto para gmail
                envios.EnableSsl = true;

                envios.Send(correos);
                lblEstado_de_envio.Text = "ENVIADO";
                MessageBox.Show("Contraseña Enviada, revisa tu correo Electronico", "Restauración de contraseña", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panelRestaurarCuenta.Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR, Intentelo de neuvo", "Restauración de contraseña", MessageBoxButtons.OK, MessageBoxIcon.Error);

                lblEstado_de_envio.Text = "Correo no registrado";
            }

        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            mostrar_usuarios_por_correo();
            richTextBox1.Text = richTextBox1.Text.Replace("@pass", lblResultadoContraseña.Text);//Buscar la variable @pass para remplazarla con la contraseña
            enviarCorreo("club.billar2000.technical@gmail.com", "SICB2000Tec", richTextBox1.Text, "Solicitud para Recuperación de Contraseña", cbxCorreo.Text, "");
            //Cambio de seguridad conexion de terceros de la cuenta de gmail para poder enviar los correos

        }
    }
}
