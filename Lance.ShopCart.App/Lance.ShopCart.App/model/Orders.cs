using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lance.ShopCart.App.model
{
	public class Orders
	{
		public Products Products { get; private set; }
		public int Qty { get; private set; }
		public int TotalPrice { get; private set; }

		public Orders(Products products, int qty)
		{
			this.Products = products;
			this.Qty = qty;
			this.TotalPrice = products.Price * qty;
		}
	}


	/// <summary>
	/// 擴充方法，列出Orders總金額
	/// </summary>
	public static class OrdersHelper
	{
		public static int GetTotalPrice(this List<Orders> orders) => orders.Sum(x => x.TotalPrice);
	}
}
