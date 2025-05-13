using Microsoft.AspNetCore.Mvc;
using SharedModels;

namespace PerkembanganApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerkembanganController : ControllerBase
    {
        private static readonly List<ReportData> _siswaList = new()
        {
            new ReportData
            {
                Id = "1",
                NamaSiswa = "Ahmad Fauzi",
                NomorInduk = "123456",
                Kelas = "TK B",
                Semester = "2",
                TahunAjaran = "2024/2025",
                NilaiAgama = "Ahmad menunjukkan sikap religius dan rajin beribadah.",
                JatiDiri = "Mandiri dan tanggung jawab dalam kegiatan sekolah.",
                LiterasiMatematika = "Mampu mengenal huruf, angka, dan menyukai eksperimen sains.",
                SainsTeknologiRekayasa = "Suka bereksperimen dan memahami konsep dasar teknologi.",
                ProyekPancasila = "Aktif dalam proyek lingkungan dan kerja sama tim."
            },
            new ReportData
            {
                Id = "2",
                NamaSiswa = "Siti Aminah",
                NomorInduk = "789012",
                Kelas = "TK A",
                Semester = "1",
                TahunAjaran = "2024/2025",
                NilaiAgama = "Berakhlak baik, hormat kepada guru, dan taat beribadah.",
                JatiDiri = "Percaya diri dan penuh inisiatif.",
                LiterasiMatematika = "Menunjukkan kemampuan membaca dasar dan menyanyi.",
                SainsTeknologiRekayasa = "Mampu merakit mainan sederhana dan menjelaskan fungsinya.",
                ProyekPancasila = "Terlibat dalam proyek budaya dan kerajinan tangan."
            }
        };

        [HttpGet]
        public ActionResult<IEnumerable<ReportData>> GetAll()
        {
            return Ok(_siswaList);
        }

        [HttpGet("{id}")]
        public ActionResult<ReportData> GetById(string id)
        {
            var data = _siswaList.FirstOrDefault(s => s.Id == id);
            if (data == null)
                return NotFound(new { message = "Siswa tidak ditemukan" });

            return Ok(data);
        }
    }
}
