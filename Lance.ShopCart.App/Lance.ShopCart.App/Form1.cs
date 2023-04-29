using Lance.ShopCart.App.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Lance.ShopCart.App
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private List<Products> _productBuy;
		private List<Products> _productSearch;
		private List<Orders> _orders;

		private void Form1_Load(object sender, EventArgs e)
		{

			try
			{
				groupBox1.Text = string.Empty;
				labelPrice.Text = "0";
				labelTotalPrice.Text = "0";
				_orders = new List<Orders>();
				cbBuy.SelectedIndexChanged += cbBuy_SelectedIndexChanged;


				//顯示清單的column
				dataGridView1.Columns.Add("Name", "商品");
				dataGridView1.Columns.Add("Qty", "數量");
				dataGridView1.Columns.Add("Price", "單價");
				dataGridView1.Columns.Add("TotalPrice", "金額");


				//背景設定購買產品清單
				_productBuy = new List<Products>()
			{
				new Products("請選擇產品",0,0),
				new Products("台啤",1,40),
				new Products("百威",2,35),
				new Products("雪山",3,50)
			};
				//設定購買清單的值
				cbBuy.DataSource = _productBuy;
				cbBuy.DisplayMember = "Name";
				cbBuy.ValueMember = "Id";
				cbBuy.SelectedIndex = 0;



				//背景設定查詢清單
				_productSearch = new List<Products>()
			{
				new Products("全部",0,0),
				new Products("台啤",1,40),
				new Products("百威",2,35),
				new Products("雪山",3,50)
			};
				//預設查詢清單的值
				cbSearch.DataSource = _productSearch;
				cbSearch.DisplayMember = "Name";
				cbSearch.ValueMember = "Id";
				cbSearch.SelectedIndex = 0;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "系統初始錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		/// <summary>
		/// 產品改變，單價會連動show出
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cbBuy_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (cbBuy.SelectedIndex)
			{
				case 0: labelPrice.Text = "0"; break;
				case 1: labelPrice.Text = "40"; break;
				case 2: labelPrice.Text = "35"; break;
				case 3: labelPrice.Text = "50"; break;
				default: labelPrice.Text = "0"; break;
			}
		}






		private void btnOrder_Click(object sender, EventArgs e)
		{

			/// <summary>
			/// 購買按鈕，產品數量不得留空
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>

				try
				{
					if (cbBuy.SelectedIndex != 1 && cbBuy.SelectedIndex != 2 && cbBuy.SelectedIndex != 3)
					{
						MessageBox.Show("請選擇商品", "系統初始錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					if (numQty.Value <= 0)
					{
						MessageBox.Show("數量不得低於0", "系統初始錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}


					var buyproduct = _productBuy[cbBuy.SelectedIndex];
					int qty = (int)numQty.Value;
					var orders = new Orders(buyproduct, qty);
					_orders.Add(orders);

					dataGridView1.Rows.Add(buyproduct.Name, qty, buyproduct.Price, orders.TotalPrice);

					//秀出orders總金額
					labelTotalPrice.Text = _orders.GetTotalPrice().ToString();

					//按鍵復原
					cbBuy.SelectedIndex = 0;
					numQty.Value = 0;
					return;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "系統初始錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

			}

		/// <summary>
		/// 查詢按鈕
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSearch_Click_1(object sender, EventArgs e)
		{
			try
			{
				if (_orders.Count <= 0)
				{

					MessageBox.Show("沒有訂單", "系統初始錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				if (cbSearch.SelectedIndex == 0)
				{
					dataGridView1.Rows.Clear();
					foreach (var orders in _orders)
					{
						dataGridView1.Rows.Add(orders.Products.Name, orders.Qty, orders.Products.Price, orders.TotalPrice);
					}
					labelTotalPrice.Text = _orders.GetTotalPrice().ToString();
				}
				else if (cbSearch.SelectedIndex > 0 && cbSearch.SelectedIndex < 4)
				{
					dataGridView1.Rows.Clear();
					var selectedProduct = _productSearch.FirstOrDefault(p => p.Id == cbSearch.SelectedIndex);
					var filteredOrders = _orders.Where(o => o.Products.Id == selectedProduct.Id).ToList();
					foreach (var order in filteredOrders)
					{
						dataGridView1.Rows.Add(order.Products.Name, order.Qty, order.Products.Price, order.TotalPrice);
					}
					labelTotalPrice.Text = filteredOrders.GetTotalPrice().ToString();
				}
				else
				{
					dataGridView1.Rows.Clear();
					MessageBox.Show("請選擇查詢商品", "系統初始錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "系統初始錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

	}
}
