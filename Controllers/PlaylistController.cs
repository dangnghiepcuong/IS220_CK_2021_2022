using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using IS220_CK_2021_2022.Models;

namespace IS220_CK_2021_2022.Controllers
{
    public class PlaylistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NguoiNghe()
        {
            SqlConnection conn = new SqlConnection("Data source=localhost; Initial Catalog=Contacts; Integrated Security=True; Database=IS220_CK_2021_2022");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from NGUOINGHE", conn);
            var reader = cmd.ExecuteReader();
            string html = "";
            List<NguoiNghe> lnn = new List<NguoiNghe>();
            while (reader.Read())
            {
                NguoiNghe nn = new NguoiNghe();
                nn.MaNN = reader["MaNN"] as string;
                nn.TenNN = reader["TenNN"] as string;
                lnn.Add(nn);
            }
            ViewBag.lnn = lnn;
            return View("Index");
        }

        public IActionResult BaiHat(string nn)
        {
            SqlConnection conn = new SqlConnection("Data source=localhost; Initial Catalog=Contacts; Integrated Security=True; Database=IS220_CK_2021_2022");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from PLAYLIST where MaNN = @nn");
            cmd.Parameters.AddWithValue("nn", nn);
            var reader = cmd.ExecuteReader();
            List<Playlist> pl = new List<Playlist>();
            while (reader.Read())
            {
                Playlist l = new Playlist();
                l.MaPlayList = reader["MaPlayList"] as string;
                l.TenPlayList = reader["TenPlayList"] as string;
                pl.Add(l);
            }
            ViewBag.pl = pl;
            return View("Index");
        }

        public IActionResult Xoa(string pl)
        {
            SqlConnection conn = new SqlConnection("Data source=localhost; Initial Catalog=Contacts; Integrated Security=True; Database=IS220_CK_2021_2022");
            conn.Open();
            SqlCommand cmd = new SqlCommand("delete from PLAYLIST_BAIHAT where MaPlayList = @pl", conn);
            cmd.Parameters.AddWithValue("pl", pl);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("delete from PLAYLIST where MaPlayList = @pl", conn);
            cmd.Parameters.AddWithValue("pl", pl);
            cmd.ExecuteNonQuery();
            return View();
        }
    }
}
