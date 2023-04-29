using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lance.ShopCart.App.model
{
	public class Products
	{
		public string Name { get; private set; }
		public int Id { get; private set; }
		public int Price { get; private set; }
		public Products(string name, int id, int price)
		{
			this.Name = name;
			this.Id = id;
			this.Price = price;
		}
	}
}
