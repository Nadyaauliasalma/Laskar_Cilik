using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PerkembanganSiswa;

namespace PerkembanganSiswa_Test
{
    [TestClass]
    public class PerkembanganServiceTests
    {
        private PerkembanganService service;

        [TestInitialize]
        public void Setup()
        {
            PerkembanganService.database.Clear();
            var config = new RuntimeConfigService();
            service = new PerkembanganService(config);
        }

        [TestMethod]
        public void Post_ValidInput_ShouldReturnCorrectSiswa()
        {
            var dto = new InputPerkembanganDto
            {
                NamaSiswa = "Yusuf",
                NomorInduk = "111222",
                Kelas = "TK A",
                Semester = 1,
                TahunAjaran = 2024,
                Kategori = new KategoriPerkembangan
                {
                    NilaiAgama = "Berdoa sebelum belajar",
                    JatiDiri = "Berani tampil di depan kelas",
                    LiterasiDanSTEM = "Sudah lancar berhitung sampai 100",
                    ProyekPenguatanPancasila = "Sudah hafal seluruh sila Pancasila"
                }
            };

            var result = service.Post(dto);

            Assert.AreEqual("Yusuf", result.NamaSiswa);
            Assert.AreEqual("Berdoa sebelum belajar", result.CatatanPerKategori[KategoriPerkembangan.Kategori.NilaiAgama]);
            Assert.AreEqual("Berani tampil di depan kelas", result.CatatanPerKategori[KategoriPerkembangan.Kategori.JatiDiri]);
            Assert.AreEqual("Sudah lancar berhitung sampai 100", result.CatatanPerKategori[KategoriPerkembangan.Kategori.LiterasiDanSTEM]);
            Assert.AreEqual("Sudah hafal seluruh sila Pancasila", result.CatatanPerKategori[KategoriPerkembangan.Kategori.ProyekPenguatanPancasila]);

        }

        [TestMethod]
        public void Update_ChangesDataSuccessfully()
        {
            var dto = new InputPerkembanganDto
            {
                NamaSiswa = "Yusuf",
                NomorInduk = "111223",
                Kelas = "TK A",
                Semester = 1,
                TahunAjaran = 2024,
                Kategori = new KategoriPerkembangan
                {
                    NilaiAgama = "Berdoa sebelum belajar",
                    JatiDiri = "Berani tampil di depan kelas",
                    LiterasiDanSTEM = "Sudah lancar berhitung sampai 100",
                    ProyekPenguatanPancasila = "Sudah hafal seluruh sila Pancasila"
                }
            };

            var inserted = service.Post(dto);

            var updateDto = new InputPerkembanganDto
            {
                NamaSiswa = "Yusuf Updated",
                NomorInduk = "111223",
                Kelas = "TK B",
                Semester = 2,
                TahunAjaran = 2025,
                Kategori = new KategoriPerkembangan
                {
                    NilaiAgama = "Menolong teman",
                    JatiDiri = "Berani tampil di depan kelas",
                    LiterasiDanSTEM = "Sudah lancar berhitung sampai 100",
                    ProyekPenguatanPancasila = "Sudah hafal seluruh sila Pancasila"
                }
            };

            bool success = service.Update(inserted.Id, updateDto);
            Assert.IsTrue(success);

            var updated = service.GetById(inserted.Id);
            Assert.AreEqual("Yusuf Updated", updated.NamaSiswa);
            Assert.AreEqual("TK B", updated.Kelas);
            Assert.AreEqual("Menolong teman", updated.CatatanPerKategori[KategoriPerkembangan.Kategori.NilaiAgama]);
        }

        [TestMethod]
        public void Delete_RemovesDataSuccessfully()
        {
            var dto = new InputPerkembanganDto
            {
                NamaSiswa = "Dina",
                NomorInduk = "777777",
                Kelas = "TK C",
                Semester = 1,
                TahunAjaran = 2024,
                Kategori = new KategoriPerkembangan
                {
                    JatiDiri = "Mandiri"
                }
            };

            var result = service.Post(dto);
            bool deleted = service.Delete(result.Id);
            Assert.IsTrue(deleted);

            var fetched = service.GetById(result.Id);
            Assert.IsNull(fetched);
        }

        [TestMethod]
        public void GetById_InvalidId_ReturnsNull()
        {
            var result = service.GetById(9999); 
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Post_EmptyNomorInduk_ShouldThrowException()
        {
            var dto = new InputPerkembanganDto { NomorInduk = "" };
            service.Post(dto);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Post_NomorIndukTerlaluPanjang_ShouldThrowException()
        {
            var dto = new InputPerkembanganDto
            {
                NamaSiswa = "Test",
                NomorInduk = "123456789", // panjang lebih dari 6
                Kelas = "TK A",
                Semester = 1,
                TahunAjaran = 2024,
                Kategori = new KategoriPerkembangan()
            };

            service.Post(dto);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Post_KategoriTerlaluPanjang_ShouldThrowException()
        {
            var panjang = new string('x', 1000); // > batas config
            var dto = new InputPerkembanganDto
            {
                NamaSiswa = "Test",
                NomorInduk = "123456",
                Kelas = "TK A",
                Semester = 1,
                TahunAjaran = 2024,
                Kategori = new KategoriPerkembangan
                {
                    NilaiAgama = panjang
                }
            };

            service.Post(dto);
        }


    }
}
