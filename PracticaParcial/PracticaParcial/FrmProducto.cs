using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticaParcial
{
    public partial class FrmProducto : Form
    {
        public FrmProducto()
        {
            InitializeComponent();
        }

        private void FrmProducto_Load(object sender, EventArgs e)
        {
            CargarProveedores();
            CargarCategorias();
            listarProductos();
        }

        private void CargarProveedores()
        {
            DataTable dtProveedores = new DataTable();
            Proveedor objProveedor = new Proveedor();
            dtProveedores = objProveedor.ListarProveedores();

            cmbProveedor.DataSource = dtProveedores;
            cmbProveedor.DisplayMember = "Nombre";
            cmbProveedor.ValueMember = "IdProveedor";
        }

        private void CargarCategorias()
        {
            DataTable dtCategorias = new DataTable();
            Categoria objCategoria = new Categoria();
            dtCategorias = objCategoria.ListarCategorias();

            cmbCategoria.DataSource = dtCategorias;
            cmbCategoria.DisplayMember = "Nombre";
            cmbCategoria.ValueMember = "IdCategoria";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                string.IsNullOrWhiteSpace(txtStock.Text))
            {
                MessageBox.Show("Rellene todos los campos requeridos");
                return;
            }

            try
            {
                int resultado = 0;
                Producto producto = new Producto(
                    txtNombre.Text,
                    decimal.Parse(txtPrecio.Text),
                    int.Parse(txtStock.Text),
                    Convert.ToInt32(cmbProveedor.SelectedValue),
                    Convert.ToInt32(cmbCategoria.SelectedValue)
                );

                resultado = producto.GuardarProducto();

                if (resultado > 0)
                {
                    MessageBox.Show("Producto guardado exitosamente");
                    LimpiarCampos();
                    listarProductos();
                }
                else
                {
                    MessageBox.Show("Error al guardar el producto");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void listarProductos()
        {
            DataTable dtProductos = new DataTable();
            Producto objProducto = new Producto();
            dtProductos = objProducto.ListarProductos();
            listViewProductos.Items.Clear();

            foreach (DataRow fila in dtProductos.Rows)
            {
                ListViewItem listItem = new ListViewItem(fila["IdProducto"].ToString());
                listItem.SubItems.Add(fila["Nombre"].ToString());
                listItem.SubItems.Add(fila["Precio"].ToString());
                listItem.SubItems.Add(fila["CantidadStock"].ToString());
                listItem.SubItems.Add(fila["NombreProveedor"].ToString());
                listItem.SubItems.Add(fila["NombreCategoria"].ToString());
                listViewProductos.Items.Add(listItem);
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
        }
    }

}
