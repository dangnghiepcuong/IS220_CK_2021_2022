using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using IS220_CK_2021_2022.Models;

namespace IS220_CK_2021_2022.Controllers
{
    public class BaiHatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Them(BaiHat bh)
        {
            SqlConnection conn = new SqlConnection("Data source=localhost; Initial Catalog=Contacts; Integrated Security=True; Database=IS220_CK_2021_2022");
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into BAIHAT values(@ma, @ten, @loai)", conn);
            cmd.Parameters.AddWithValue("ma", bh.MaBaiHat);
            cmd.Parameters.AddWithValue("ten", bh.TenBaiHat);
            cmd.Parameters.AddWithValue("loai", bh.TheLoai);
            cmd.ExecuteNonQuery();
            return View();
        }
    }
}
