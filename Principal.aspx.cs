using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;

namespace CRUD
{

    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGrilla();
            }
        }

        private void LlenarGrilla()
        {
            List<Empleado> todasLosEmpleados;

            using (var dbContext = new MyDbContext())
            {
                // Consultar todos los registros de la tabla Empleados
                todasLosEmpleados = dbContext.Empleados.ToList();
            }
            GridView1.DataSource = todasLosEmpleados;
            GridView1.DataBind();
        }


        #region BOTONES

        protected void btnGuardarNuevo_Click(object sender, EventArgs e)
        {

            if (ValidarCampos())
            {
                if (txtAccion.Text.ToUpper() == "NUEVO".ToUpper())
                {
                    GuardarNuevo();
                }
                if (txtAccion.Text.ToUpper() == "EDITAR".ToUpper())
                {
                    Editar(Convert.ToInt32(txtID.Text));
                }

                LlenarGrilla();
                pnlAgregarEditar.Visible = false;
                LimpiarCampos();

            }
        }


        protected void btnEditar_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                txtID.Text = e.CommandArgument.ToString();
                txtAccion.Text = "EDITAR";
                pnlAgregarEditar.Visible = true;

                LlenarCamposEditar();
            }
        }

        protected void btnEliminar_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                txtID.Text = e.CommandArgument.ToString();

                Eliminar(Convert.ToInt32(txtID.Text));
                LlenarGrilla();
            }
        }

        protected void btnCerrarNuevo_Click(object sender, EventArgs e)
        {
            pnlAgregarEditar.Visible = false;
            LimpiarCampos();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            pnlAgregarEditar.Visible = true;
            txtAccion.Text = "NUEVO";
        }


        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            if (txtBuscar.Text != "")
            {
                string Nombre = txtBuscar.Text;
                List<Empleado> todosLosEmpleados;

                using (var dbContext = new MyDbContext())
                {
                    todosLosEmpleados = dbContext.Empleados.Where(p => p.Nombre == Nombre).ToList();
                }

                if (todosLosEmpleados.Count == 0)
                {
                    pnlNoResultados.Visible = true;
                }

                GridView1.DataSource = todosLosEmpleados;
                GridView1.DataBind();
            }
            else
            {
                pnlNoResultados.Visible = false;
                LlenarGrilla();
            }
        }

        protected void btnCerrarNoEcontrados_Click(object sender, EventArgs e)
        {
            pnlNoResultados.Visible = false;
        }


        #endregion


        #region GUARDAR, EDITAR, ELIMINAR

        private void GuardarNuevo()
        {
            using (var dbContext = new MyDbContext()) // Crea el contexto de datos
            {
                // Crear un nuevo registro
                var nuevoRegistro = new Empleado
                {
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Email = txtEmail.Text,
                    Salario = Convert.ToInt32(txtSalario.Text)
                };

                // Agregar el nuevo registro al contexto de datos
                dbContext.Empleados.Add(nuevoRegistro);

                // Guardar los cambios en la base de datos
                dbContext.SaveChanges();
            }
        }

        private void Editar(int id)
        {
            using (var dbContext = new MyDbContext())
            {
                var registroAEditar = dbContext.Empleados.FirstOrDefault(r => r.Id == id);

                if (registroAEditar != null)
                {
                    // Realiza la edición de los campos del registro según tus necesidades
                    registroAEditar.Nombre = txtNombre.Text;
                    registroAEditar.Apellido = txtApellido.Text;
                    registroAEditar.Email = txtEmail.Text;
                    registroAEditar.Salario = Convert.ToInt32(txtSalario.Text);

                    // Guarda los cambios en la base de datos
                    dbContext.SaveChanges();
                }
            }
        }

        private void Eliminar(int id)
        {
            using (var dbContext = new MyDbContext())
            {
                var registroAEliminar = dbContext.Empleados.FirstOrDefault(r => r.Id == id);

                if (registroAEliminar != null)
                {
                    dbContext.Empleados.Remove(registroAEliminar);
                    dbContext.SaveChanges();
                }
            }
        }

        #endregion


        #region LIMPIAR, VALIDAR

        private void LlenarCamposEditar()
        {
            int id = Convert.ToInt32(txtID.Text);

            using (var dbContext = new MyDbContext())
            {
                var empleado = dbContext.Empleados.FirstOrDefault(p => p.Id == id);
                txtNombre.Text = empleado.Nombre;
                txtApellido.Text = empleado.Apellido;
                txtEmail.Text = empleado.Email;
                txtSalario.Text = empleado.Salario.ToString();
            }
        }

        private bool ValidarCampos()
        {
            bool Resultado = true;
            string patronMail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(patronMail);


            if (txtNombre.Text == "")
            {
                Resultado = false;
                lblNombre.Visible = true;
            }
            else
            {
                lblNombre.Visible = false;
            }

            if (txtApellido.Text == "")
            {
                Resultado = false;
                lblApellido.Visible = true;
            }
            else
            {
                lblApellido.Visible = false;
            }

            if (txtEmail.Text == "")
            {
                Resultado = false;
                lblEmail.Visible = true;
            }
            else if (regex.IsMatch(txtEmail.Text))
            {
                lblEmail.Visible = false;
            }
            else
            {
                Resultado = false;
                lblEmail.Text = "Mail inválido";
                lblEmail.Visible = true;
            }

            if (txtSalario.Text == "")
            {
                Resultado = false;
                lblSalario.Visible = true;
            }
            else
            {
                lblSalario.Visible = false;
            }



            return Resultado;
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEmail.Text = "";
            txtSalario.Text = "";
        }

        #endregion

    }
}