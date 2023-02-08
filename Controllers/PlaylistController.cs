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
            NguoiNghe();
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
                nn.GioiTinh = "";
                lnn.Add(nn);
            }
            
            ViewBag.lnn = lnn;
            return View("Index");
        }

        public IActionResult Playlist(string MaNN)
        {
            SqlConnection conn = new SqlConnection("Data source=localhost; Initial Catalog=Contacts; Integrated Security=True; Database=IS220_CK_2021_2022");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from PLAYLIST where MaNN = @nn", conn);
            cmd.Parameters.AddWithValue("nn", MaNN);
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

        public IActionResult BaiHat(string pl)
        {
            SqlConnection conn = new SqlConnection("Data source=localhost; Initial Catalog=Contacts; Integrated Security=True; Database=IS220_CK_2021_2022");
            conn.Open();
            SqlCommand cmd = new SqlCommand(
                "select B.TenBaiHat, C.TenCaSi " +
                "from PLAYLIST_BAIHAT PB, BAIHAT B, CASI_BAIHAT CB, CASI C " +
                "where MaPlayList = @ma and PB.MaBaiHat = B.MaBaiHat " +
                "and B.MaBaiHat = CB.MaBaiHat and CB.MaCaSi = C.MaCaSi"
                , conn);
            cmd.Parameters.AddWithValue("ma", pl);
            var reader = cmd.ExecuteReader();
            List<BaiHat> lbh = new List<BaiHat>();
            while (reader.Read())
            {
                BaiHat bh = new BaiHat();
                bh.TenBaiHat = reader["TenBaiHat"] as string;
                bh.MaBaiHat = reader["TenCaSi"] as string;
                lbh.Add(bh);
            }
            ViewBag.BC = lbh;
            return View("BaiHat");
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
