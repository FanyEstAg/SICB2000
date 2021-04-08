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
using System.Text.RegularExpressions;

namespace Punto_de_Venta
{
    public partial class md_UsuariosOK : Form
    {
        public md_UsuariosOK()
        {
            InitializeComponent();
        }
        private void estado_de_Iconos()
        {
            try
            {
                foreach (DataGridViewRow row in dgvUsuarios.Rows)
                {

                    try
                    {

                        string Icono = Convert.ToString(row.Cells["Nombre_de_icono"].Value);

                        switch (Icono)
                        {
                            case "1":
                                pictureBox3.Visible = false;
                                break;
                            case "2":
                                pictureBox4.Visible = false;
                                break;
                            case "3":
                                pictureBox5.Visible = false;
                                break;
                            case "4":
                                pictureBox6.Visible = false;
                                break;
                            case "5":
                                pictureBox7.Visible = false;
                                break;
                            case "6":
                                pictureBox8.Visible = false;
                                break;
                            case "7":
                                pictureBox9.Visible = false;
                                break;
                            case "8":
                                pictureBox10.Visible = false;
                                break;
                            case "9":
                                pictureBox11.Visible = false;
                                break;
                            case "10":
                                pictureBox12.Visible = false;
                                break;
                        }

                    }
                    catch (Exception ex)
                    {


                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public bool validar_Correo(string correo)
        {
            return Regex.IsMatch(correo, @"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$");
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {


            if (txtNombres.Text != "" || txtApellidos.Text != "" || txtContrasenia.Text != "" || txtCorreo.Text != ""
                || txtUsuario.Text != "" || cbxRol.SelectedIndex != 0 || lblAnuncioIcono.Visible == false)
            {
                if (validar_Correo(txtCorreo.Text) == false)
                {
                    MessageBox.Show("Dirección de correo electronico inválido.\nEl correo debe tener el formato: nombre@dominio.com, " + "\nPor favor ingrese un correo válido", "Validación de correo electronico", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCorreo.Focus();
                    txtCorreo.SelectAll();
                }
                else
                {
                    if (txtContrasenia.Text.Length >= 8)
                    {
                        if (txtUsuario.Text.Length >= 4)
                        {
                            try
                            {
                                SqlConnection con = new SqlConnection();
                                con.ConnectionString = CONEXION.Conexion_Maestra.conexion;
                                con.Open();
                                SqlCommand cmd = new SqlCommand();
                                cmd = new SqlCommand("insertar_usuario", con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@nombres", txtNombres.Text);
                                cmd.Parameters.AddWithValue("@apellidos", txtApellidos.Text);
                                cmd.Parameters.AddWithValue("@Login", txtUsuario.Text);
                                cmd.Parameters.AddWithValue("@Password", (txtContrasenia.Text));
                                cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                                cmd.Parameters.AddWithValue("@Rol", cbxRol.Text);
                                System.IO.MemoryStream ms = new System.IO.MemoryStream(); //Proceso para imagenes
                                pbIcono.Image.Save(ms, pbIcono.Image.RawFormat); //Transformar a tipo de datos de SQL
                                cmd.Parameters.AddWithValue("@Icono", ms.GetBuffer());//Transformar a binario- Referencias ruta
                                cmd.Parameters.AddWithValue("@Nombre_de_icono", lblNumIcono.Text);
                                cmd.Parameters.AddWithValue("@Estado", "ACTIVO");

                                cmd.ExecuteNonQuery();
                                con.Close();
                                mostrar();
                                panelRegistro.Visible = false;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        else
                        {
                            MessageBox.Show("El nombre de usuario debe tener un mínimo de 4 caracteres" +
                                "\nPor favor ingresa una nuevo nombre de usuario", "Usuario inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }
                    }
                    else
                    {
                        MessageBox.Show("La contraseña debe tener un mínimo de 8 caracteres" +
                                "\nPor favor ingresa una nueva contraseña", "Seguridad de contraseña", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                }

            }
            else
            {
                MessageBox.Show("Faltan datos por ingresar" + 
                    "\nPor favor rellena los campos faltantes", "Datos faltantes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }
        private void mostrar()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da; //Adaptar datos a un data grid
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.Conexion_Maestra.conexion;
                con.Open();

                da = new SqlDataAdapter("mostrar_usuario", con);
                da.Fill(dt); //Rellenar
                dgvUsuarios.DataSource = dt;
                con.Close();
                lblId.Visible = true;
                dgvUsuarios.Columns[1].Visible = false;
                dgvUsuarios.Columns[5].Visible = false;
                dgvUsuarios.Columns[6].Visible = false;
                dgvUsuarios.Columns[7].Visible = false;
                //dgvUsuarios.Columns[8].Visible = false;
                //dgvUsuarios.Columns[9].Visible = false;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Tamanio_automatico_dgv.Multilinea(ref dgvUsuarios);
            //Bases.Multilinea(ref datalistado);

        }
        private void lblAnuncioIcono_Click(object sender, EventArgs e)
        {
            estado_de_Iconos();
            panelIcono.Visible = true;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            iconos(pictureBox3);
        }


        private void pictureBox4_Click(object sender, EventArgs e)
        {
            iconos(pictureBox4);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            iconos(pictureBox5);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            iconos(pictureBox6);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            iconos(pictureBox7);
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            iconos(pictureBox11);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            iconos(pictureBox8);
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            iconos(pictureBox10);
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            iconos(pictureBox9);
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            iconos(pictureBox12);
        }

        private void iconos(PictureBox pb)
        {
            pbIcono.Image = pb.Image;
            lblAnuncioIcono.Visible = false;
            panelIcono.Visible = false;
            int index = pb.Name.Length - 1;
            int num = int.Parse(pb.Name[index].ToString()) - 2;
            lblNumIcono.Text = num.ToString();
        }

        private void md_UsuariosOK_Load(object sender, EventArgs e)
        {
            panelRegistro.Visible = false;
            panelIcono.Visible = false;
            mostrar();
        }

        private void pbAñadir_Click(object sender, EventArgs e)
        {
            panelRegistro.Visible = true;
            lblAnuncioIcono.Visible = true;
            limpiartxtUsuarios();
            btnGuardar.Enabled = true;
            btnGuardarCambios.Enabled = false;
        }

        private void dgvUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lblAnuncioIcono.Visible = false;
            lblId.Text = dgvUsuarios.SelectedCells[1].Value.ToString();
            txtNombres.Text = dgvUsuarios.SelectedCells[2].Value.ToString();
            txtApellidos.Text = dgvUsuarios.SelectedCells[3].Value.ToString();
            txtUsuario.Text = dgvUsuarios.SelectedCells[4].Value.ToString();

            txtContrasenia.Text = dgvUsuarios.SelectedCells[5].Value.ToString();

            pbIcono.BackgroundImage = null;//Extraer la imagen 
            byte[] b = (Byte[])dgvUsuarios.SelectedCells[6].Value;
            MemoryStream ms = new MemoryStream(b);
            pbIcono.Image = Image.FromStream(ms);


            lblNumIcono.Text = dgvUsuarios.SelectedCells[7].Value.ToString();
            txtCorreo.Text = dgvUsuarios.SelectedCells[8].Value.ToString();
            cbxRol.Text = dgvUsuarios.SelectedCells[9].Value.ToString();
            panelRegistro.Visible = true;
            btnGuardar.Enabled = false;
            btnGuardarCambios.Enabled = true;

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            panelRegistro.Visible = false;
        }

        private void limpiartxtUsuarios()
        {
            txtApellidos.Text = "";
            txtContrasenia.Text = "";
            txtCorreo.Text = "";
            txtNombres.Text = "";
            txtUsuario.Text = "";
            lblId.Text = "";
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (txtNombres.Text != "")
            {
                try
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = CONEXION.Conexion_Maestra.conexion;
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand("modificar_usuario", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idUsuario", lblId.Text);
                    cmd.Parameters.AddWithValue("@nombres", txtNombres.Text);
                    cmd.Parameters.AddWithValue("@apellidos", txtApellidos.Text);
                    cmd.Parameters.AddWithValue("@Login", txtUsuario.Text);
                    cmd.Parameters.AddWithValue("@Password", txtContrasenia.Text);

                    cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                    cmd.Parameters.AddWithValue("@Rol", cbxRol.Text);
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    pbIcono.Image.Save(ms, pbIcono.Image.RawFormat);


                    cmd.Parameters.AddWithValue("@Icono", ms.GetBuffer());
                    cmd.Parameters.AddWithValue("@Nombre_de_icono", lblNumIcono.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    mostrar();
                    panelRegistro.Visible = false;
                    lblId.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }

        private void pbIcono_Click(object sender, EventArgs e)
        {
            estado_de_Iconos();
            panelIcono.Visible = true;

        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.dgvUsuarios.Columns["Eliminar"].Index)
            {
                DialogResult result;
                result = MessageBox.Show("¿Desea eliminar este usuario?", "Eliminar registro de usuario", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    SqlCommand cmd;
                    try
                    {
                        foreach (DataGridViewRow row in dgvUsuarios.SelectedRows)
                        {

                            int onekey = Convert.ToInt32(row.Cells["idUsuario"].Value);
                            string usuario = Convert.ToString(row.Cells["Usuario"].Value);

                            try
                            {

                                try
                                {

                                    SqlConnection con = new SqlConnection();
                                    con.ConnectionString = CONEXION.Conexion_Maestra.conexion;
                                    con.Open();
                                    cmd = new SqlCommand("eliminar_usuario", con);
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@idusuario", onekey);
                                    cmd.Parameters.AddWithValue("@login", usuario);
                                    cmd.ExecuteNonQuery();

                                    con.Close();

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }

                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message);
                            }

                        }
                        mostrar();
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
        }

        private void pbSelecImagen_Click(object sender, EventArgs e)
        {
            dlgBuscarIamgen.InitialDirectory = "";
            dlgBuscarIamgen.Filter = "Imagenes|*.jpg;*.png";
            dlgBuscarIamgen.FilterIndex = 2;
            dlgBuscarIamgen.Title = "Cargador de Imagenes Club Billar 2000";
            if (dlgBuscarIamgen.ShowDialog() == DialogResult.OK)
            {
                pbIcono.BackgroundImage = null;
                pbIcono.Image = new Bitmap(dlgBuscarIamgen.FileName);
                pbIcono.SizeMode = PictureBoxSizeMode.Zoom;
                lblNumIcono.Text = Path.GetFileName(dlgBuscarIamgen.FileName);
                lblAnuncioIcono.Visible = false;
                panelIcono.Visible = false;
            }

        }

        private void buscarUsuario()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.Conexion_Maestra.conexion;
                con.Open();

                da = new SqlDataAdapter("buscar_usuario", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", txtBuscar.Text);//Mandar las letras al procedimiento
                da.Fill(dt);
                dgvUsuarios.DataSource = dt;
                con.Close();

                dgvUsuarios.Columns[1].Visible = false;
                dgvUsuarios.Columns[5].Visible = false;
                dgvUsuarios.Columns[6].Visible = false;
                dgvUsuarios.Columns[7].Visible = false;
                dgvUsuarios.Columns[8].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            Tamanio_automatico_dgv.Multilinea(ref dgvUsuarios);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            buscarUsuario();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
//Mpaximo 12 caracteres de contraseña y usuario}
//Minimo 8 para contraseña y 4 para usuario
//Admite cualquier caracter, solo numeros ver lección 17