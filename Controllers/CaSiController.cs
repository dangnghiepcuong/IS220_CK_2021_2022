using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using IS220_CK_2021_2022.Models;


namespace IS220_CK_2021_2022.Controllers
{
    public class CaSiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LietKe(DateTime NamSinh)
        {
            SqlConnection conn = new SqlConnection("Data source=localhost; Initial Catalog=Contacts; Integrated Security=True; Database=IS220_CK_2021_2022");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from CASI where NamSinh = @nam", conn);
            cmd.Parameters.AddWithValue("@nam", NamSinh);
            var reader = cmd.ExecuteReader();

            List<string> lcs = new List<string>();
            while (reader.Read())
            {
                lcs.Add(reader["TenCaSi"] as string);
            }
            ViewBag.lcs = lcs;
            return View("Index");
        }
    }
}
