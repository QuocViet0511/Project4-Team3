using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTO
{
	public class GioHangDTO : Models.BaseEntity
	{
		/*public GioHangDTO(String Image, String Name, decimal? DonGia, int SoLuong, decimal? TongTien)
		{
 			this.Image = Image;
			this.Name = Name;
			this.DonGia = DonGia;
			this.SoLuong = SoLuong;
			this.TongTien = TongTien;
		}*/
		public string Image { get; set; }
		public string Name { get; set; }
		public decimal? DonGia { get; set; }
		public int SoLuong { get; set; }
		public decimal? TongTien { get; set; }

	}
}
