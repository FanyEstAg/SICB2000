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
using System.Management;

namespace Punto_de_Venta.MODULOS
{
    public partial class md_Login : Form
    {
        DataTable dtUsuarios = new DataTable();
        DataTable dtDetalleCierreCaja = new DataTable();
        string ResultadoContrasenia, serialPC, IdCaja, CajaDescripcion, IdUs, nombre, aperturaCaja, rol;
        string usuarioIniciaCaja, nombreCajero, permisodeCaja;
        DataTable dtCajas = new DataTable();
        public md_Login()
        {
            InitializeComponent();
             rol = mostrarPermisos();
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
            pbIconoLogin.Image = ((PictureBox)sender).Image;
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
            timer1.Start();
        }

        private void txtContrasenia_TextChanged(object sender, EventArgs e)
        {
            iniciarSesion();
        }

        private void iniciarSesion()
        {
            int cont, contadorCajas;
            cargarUsuarios();
            cont= contar(dtUsuarios);
            try
            {
                IdUs = dtUsuarios.Rows[0][0].ToString();
                nombre= dtUsuarios.Rows[0][1].ToString()+" "+ dtUsuarios.Rows[0][2].ToString();
                //MessageBox.Show(IdUs+nombre);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

            if (cont > 0)
            {
                listarAperturas_DetalleCierresCaja();
                contadorCajas=contar(dtDetalleCierreCaja);
                

                if (contadorCajas == 0 &  rol != "Jefe de inventario")
                {//No se habia aperturado
                    aperturarDetalle_CierreCaja();
                    //Quien la apertura??
                    aperturaCaja= "Nuevo*****";
                    timer2.Start();
                }
                else
                {
                    if (rol != "Jefe de inventario")
                    {
                        
                        int contador_MovimientosCaja = contar(mostrarMovimientos_CajaSerialUsuario());
                        try
                        {
                            usuarioIniciaCaja = dtDetalleCierreCaja.Rows[0][0].ToString();
                            nombreCajero = dtDetalleCierreCaja.Rows[0][1].ToString() + 
                                                    " " + dtDetalleCierreCaja.Rows[0][2].ToString();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        if (contador_MovimientosCaja == 0)
                        {

                            if (usuarioIniciaCaja != "admin" & lblUsuario.Text == "admin")
                            {
                                MessageBox.Show("Continuaras Turno de *" + nombreCajero + "*\nTodos los registros se guardaran con ese Usuario", "Caja Iniciada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                permisodeCaja = "correcto";
                            }
                            else if (usuarioIniciaCaja == "admin" & lblUsuario.Text == "admin")
                            {

                                permisodeCaja = "correcto";
                            }

                            else if (usuarioIniciaCaja != lblUsuario.Text)
                            {
                                MessageBox.Show("Para poder continuar con el Turno de *" + nombreCajero 
                                                + "*\nInicia sesion con el Usuario " + usuarioIniciaCaja
                                                + " -ó- el Usuario *admin*", "Caja Iniciada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                permisodeCaja = "vacio";

                            }
                            else if (usuarioIniciaCaja == lblUsuario.Text)
                            {
                                permisodeCaja = "correcto";
                            }
                        }
                        else
                        {
                            permisodeCaja = "correcto";
                        }

                        if (permisodeCaja == "correcto")
                        {
                            aperturaCaja = "Aperturado";
                            timer2.Start();
                        }

                    }
                    else
                    {
                        timer2.Start();
                    }

                }
            }
        }
        
        private void cargarUsuarios()
        {
            try
            {
               
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.Conexion_Maestra.conexion;
                con.Open();

                da = new SqlDataAdapter("validar_usuario", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@password", txtContrasenia.Text);
                da.SelectCommand.Parameters.AddWithValue("@login", lblUsuario.Text);

                da.Fill(dtUsuarios);
                //dgvUsuarios.DataSource = dtUsuarios;
                //Se encesitaba crear un dgv en el form para las lineas anteriores
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private DataTable mostrarMovimientos_CajaSerialUsuario()
        {
            DataTable dt = new DataTable();
            try
            {
                
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.Conexion_Maestra.conexion;
                con.Open();

                da = new SqlDataAdapter("mostrar_MOVIMIENTOS_Caja_por_Serial_Usuario", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@serial", serialPC);
                da.SelectCommand.Parameters.AddWithValue("@idusuario", IdUs);
                da.Fill(dt);
                //datalistado_movimientos_validar.DataSource = dt;
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            return dt;

        }
       
        private int contar(DataTable dt)
        {

            return dt.Rows.Count;
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

        private string mostrarPermisos()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = CONEXION.Conexion_Maestra.conexion;

            SqlCommand com = new SqlCommand("mostrar_PERMISOS_por_usuario_RolUNICO", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@login", lblUsuario.Text);
            string importe;
            try
            {
                con.Open();
                importe = Convert.ToString(com.ExecuteScalar());
                con.Close();
                return importe;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return "";
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
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.Conexion_Maestra.conexion;
                SqlCommand da = new SqlCommand("buscar_USUARIO_por_correo", con);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@correo", cbxCorreo.Text);
                con.Open();
                //Se necesita crea run lavel en el form
                //lblResultadoContraseña.Text = Convert.ToString(da.ExecuteScalar());//Obtener datos unicos y conversión a String
                ResultadoContrasenia = Convert.ToString(da.ExecuteScalar());//Obtener datos unicos 
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

        private void btnCambiarUsuario_Click(object sender, EventArgs e)
        {
            panel2.Hide();
            panel1.Visible = true;
            txtContrasenia.Text = "";

        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            mostrar_usuarios_por_correo();
            richTextBox1.Text = richTextBox1.Text.Replace("@pass", ResultadoContrasenia/* lblResultadoContraseña.Text*/);//Buscar la variable @pass para remplazarla con la contraseña
            enviarCorreo("club.billar2000.technical@gmail.com", "SICB2000Tec", richTextBox1.Text, "Solicitud para Recuperación de Contraseña", cbxCorreo.Text, "");
            //Cambio de seguridad conexion de terceros de la cuenta de gmail para poder enviar los correos

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            try
            {
                ManagementObjectSearcher MOS = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
                foreach (ManagementObject getserial in MOS.Get())
                {
                    //Se necesita un TextBox en el form para esta linea
                    //lblSerialPc.Text = getserial.Properties["SerialNumber"].Value.ToString();//Extrar serial de la PC
                    serialPC = getserial.Properties["SerialNumber"].Value.ToString();//Extrar serial de la PC
                    mostrarCaja_porSerial();
                    try
                    {
                        IdCaja= dtCajas.Rows[0][0].ToString();
                        CajaDescripcion = dtCajas.Rows[0][1].ToString();

                        //Se necesitan labels en el form para las lineas
                        //lblIdCaja.Text = dgvCajas.SelectedCells[1].Value.ToString();
                        //lblCajaDescripcion.Text = dgvCajas.SelectedCells[2].Value.ToString();
                        //Se encesitaba crear un dgv en el form para las lineas anteriores
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void timer2_Tick(object sender, EventArgs e)
            //Simulación de carga y paso a Cajeros y admin a la apertura de caja
        {
            if (progressBar1.Value < 100)
            {
                BackColor = Color.FromArgb(26, 26, 26);
                progressBar1.Value = progressBar1.Value + 10;
                PictureBox2.Visible = true;

            }
            else
            {
                progressBar1.Value = 0;
                timer2.Stop();
                if (aperturaCaja == "Nuevo*****" & rol != "Solo Ventas (no esta autorizado para manejar dinero)")
                {
                    this.Hide();
                    Caja.md_AperturaCaja frm = new Caja.md_AperturaCaja();
                    frm.ShowDialog();
                    this.Hide();
                }
                else
                {
                    this.Hide();
                    VentasMenuPrincipal.md_Ventas frm = new VentasMenuPrincipal.md_Ventas();
                    frm.ShowDialog();
                    this.Hide();
                }

            }
        }

        private void mostrarCaja_porSerial()
        {
            try
            {

                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.Conexion_Maestra.conexion;
                con.Open();

                da = new SqlDataAdapter("mostrar_CAJAS_por_serial_disco", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Serial", serialPC /*lblSerialPc.Text*/);
                da.Fill(dtCajas);
                //dgvCajas.DataSource = dtCajas;
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
               
        private void tsmOcultar_Click(object sender, EventArgs e)
        {
            txtContrasenia.PasswordChar = '*';
            tsmOcultar.Visible = false;
            tsmVer.Visible = true;
        }

        private void tsmVer_Click(object sender, EventArgs e)
        {
            txtContrasenia.PasswordChar = '\0';//Restaurar valores
            tsmOcultar.Visible = true;
            tsmVer.Visible = false;
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Usuario o contraseña incorrecto", "Datos de ingreso incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //La validación correcta de ingreso se lleva letra por letra, sin necesidad de pulsar "Ingresar"
            //Sin embargo, en caso de que la contraseña sea incorrecta, el error se mandará al dar click en el boton pues no lo redigirá automaticamente
        }

        private void listarAperturas_DetalleCierresCaja()
        {
            try
            {
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.Conexion_Maestra.conexion;
                con.Open();

                da = new SqlDataAdapter("mostrar_MOVIMIENTOS_caja_por_serial", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@serial", serialPC/*lblSerialPc.Text*/);
                da.Fill(dtDetalleCierreCaja);
                //datalistado_detalle_cierre_de_caja.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void aperturarDetalle_CierreCaja()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.Conexion_Maestra.conexion;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("insertar_DETALLE_cierre_caja", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fechaini", DateTime.Today);
                cmd.Parameters.AddWithValue("@fechafin", DateTime.Today);
                cmd.Parameters.AddWithValue("@fechacierre", DateTime.Today);
                cmd.Parameters.AddWithValue("@ingresos", "0.00");
                cmd.Parameters.AddWithValue("@egresos", "0.00");
                cmd.Parameters.AddWithValue("@saldo", "0.00");
                cmd.Parameters.AddWithValue("@idusuario", IdUs);
                cmd.Parameters.AddWithValue("@totalcaluclado", "0.00");
                cmd.Parameters.AddWithValue("@totalreal", "0.00");

                cmd.Parameters.AddWithValue("@estado", "CAJA APERTURADA");
                cmd.Parameters.AddWithValue("@diferencia", "0.00");
                cmd.Parameters.AddWithValue("@id_caja", IdCaja);

                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
