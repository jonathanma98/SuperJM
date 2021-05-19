using CapaEntidades;
using CapaNegocio;
using CapaUtilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SuperJM
{
    public partial class frmPrincipal : Form
    {
        tbProductos producto = new tbProductos();
        csInventarioNegocio inventario = new csInventarioNegocio();

        //instancia para cargar la lista
        List<tbProductos> listaproductos;
       
        int n =1;
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            tbProductos producto = new tbProductos();

            if (ValidarDatos())
            {
                int Impuestx;

                if (cbImpuesto.Text == "no")
                {
                    Impuestx = 0;
                }
                else
                {
                    Impuestx = 13;
                }
                // MessageBox.Show(Impuestx.ToString());
                producto.Id = txtId.Text;
                producto.Nombre = txtNombre.Text;
                producto.Precio = int.Parse(txtPrecio.Text);
                producto.Impuesto = Impuestx;
                producto.Categoria = cbCategoria.Text;
                producto.Tamanno = mskTamaño.Text;
                producto.FIngreso = dtIngreso.Value;
                producto.FVenderAntes = dtVender.Value;
                producto.Estado = 1; // (int)Enums.Estado.Activo;

                if (inventario.guardar(producto))
                {
                    MessageBox.Show("Se ingreso el producto", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiar();
                }
                else
                {
                    MessageBox.Show("No se guardo el producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private bool ValidarDatos()
        {
            if (txtId.Text == string.Empty)
            {
                MessageBox.Show("Debes ingresar un codigo para el producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtId.Focus();
                return false;
            }
            if (txtNombre.Text == string.Empty)
            {
                MessageBox.Show("Nombre vacio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombre.Focus();
                return false;
            }
            if (txtPrecio.Text == string.Empty)
            {
                MessageBox.Show("Precio vacio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPrecio.Focus();
                return false;
            }
            if (cbImpuesto.Text == string.Empty)
            {
                MessageBox.Show("Impuesto vacio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbImpuesto.Focus();
                return false;
            }
            if (cbCategoria.Text == string.Empty)
            {
                MessageBox.Show("Nombre vacio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbCategoria.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ImpuestoClick(object sender, EventArgs e)
        {
            cbImpuesto.DataSource = Enum.GetValues(typeof(Enums.Impues));
        }

        private void cbTipoMedida_SelectedIndexChanged(object sender, EventArgs e)
        {
            mascara((int)cbTipoMedida.SelectedValue);
        }

        private void MedidaClick(object sender, EventArgs e)
        {
            cbTipoMedida.DataSource = Enum.GetValues(typeof(Enums.Medida));
        }

        private void mascara(int msk)
        {
            switch (msk)
            {
                case 1:
                    mskTamaño.Mask = "####G";
                    break;
                case 2:
                    mskTamaño.Mask = "#####Kg";
                    break;
                case 3:
                    mskTamaño.Mask = "####Ml";
                    break;
                case 4:
                    mskTamaño.Mask = "#####Lt";
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            llamarlista();
        }

        private void llamarlista()
        {
            dataGridInventario.Rows.Clear();
            dataGridInventario.Refresh();

            listaproductos = inventario.obtenerlista(n);
            CargarDatos(listaproductos);
        }

        private void CargarDatos(List<tbProductos> listaproductos)
        {
            foreach (tbProductos productos in listaproductos)
            {
                    int nr = dataGridInventario.Rows.Add();
                    dataGridInventario.Rows[nr].Cells[0].Value = productos.Id;
                    dataGridInventario.Rows[nr].Cells[1].Value = productos.Nombre;
                    dataGridInventario.Rows[nr].Cells[2].Value = productos.Precio;
                    dataGridInventario.Rows[nr].Cells[3].Value = productos.Impuesto;
                    dataGridInventario.Rows[nr].Cells[4].Value = productos.Categoria;
                    dataGridInventario.Rows[nr].Cells[5].Value = productos.Tamanno;
                    dataGridInventario.Rows[nr].Cells[6].Value = productos.FIngreso;
                    dataGridInventario.Rows[nr].Cells[7].Value = productos.FVenderAntes;
                    //dataGridInventario.Rows[nr].Cells[0].Value = productos.Estado;
            }
        }

        private void CategoriaClik(object sender, EventArgs e)
        {
            cbCategoria.DataSource = Enum.GetValues(typeof(Enums.Categoria));
        }

        private void pasaDatos(tbProductos productos)
        {
            txtId.Text = productos.Id;
            txtNombre.Text = productos.Nombre;
            txtPrecio.Text = productos.Precio.ToString();
            cbImpuesto.Text = productos.Impuesto.ToString();
            cbCategoria.Text = productos.Categoria;
            dtIngreso.Value = productos.FIngreso.Value;
            dtVender.Value = productos.FVenderAntes.Value;

            int msk = 1;
            string P = productos.Tamanno;
            for(int i=0; i == P.Length; i++)
            {
                if(P[i].ToString() == "K")
                {
                    msk = 2;
                }
                else if (P[i].ToString() == "M")
                {
                    msk = 3;
                }
                else if (P[i].ToString() == "L")
                {
                    msk = 4;
                }
            }
            mascara(msk);
            mskTamaño.Text = productos.Tamanno;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Seguro que desear eliminar el producto:" + txtNombre.Text, "Informativo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                producto.Estado = (int)Enums.Estado.Inactivo;

                if (inventario.eliminar(producto))
                {
                    MessageBox.Show("Se elimino el producto", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No sea podido eliminar el producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                limpiar();
                llamarlista();
            }
        }

        private void limpiar()
        {
            txtId.ResetText();
            txtNombre.ResetText();
            txtPrecio.ResetText();
            cbImpuesto.ResetText();
            mskTamaño.ResetText();
            dtIngreso.ResetText();
            dtVender.ResetText();
            cbCategoria.ResetText();
        }

        private void btnEdiatar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                this.ModificacionDatos();
            }
        }

        private void ModificacionDatos()
        {
            DialogResult result = MessageBox.Show("Seguro que desear editar el producto:" + txtNombre.Text, "Informativo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                int Impuestx;

                if (cbImpuesto.Text == "no")
                {
                    Impuestx = 0;
                }
                else
                {
                    Impuestx = 13;
                }
                producto.Id = txtId.Text;
                producto.Nombre = txtNombre.Text;
                producto.Precio = int.Parse(txtPrecio.Text);
                producto.Impuesto = Impuestx;
                producto.Categoria = cbCategoria.Text;
                producto.Tamanno = mskTamaño.Text;
                producto.FIngreso = dtIngreso.Value;
                producto.FVenderAntes = dtVender.Value;
                producto.Estado = (int)Enums.Estado.Activo;

                if (inventario.modificar(producto))
                {
                    MessageBox.Show("Se realizaron los cambios", "Editado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se ha podido editar el producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridInventario_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int n = e.RowIndex;

            if (n != -1)
            {
                string id = dataGridInventario.Rows[n].Cells[0].Value.ToString();
                tbProductos productos;

                productos = listaproductos.Where(x => x.Id.Trim() == id.Trim()).SingleOrDefault();

                pasaDatos(productos);
            }
        }
    }
}
