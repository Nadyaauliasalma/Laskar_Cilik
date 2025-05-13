using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ManajemenAkunGuru.Models;
using ManajemenAkunGuru.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ManajemenAkunGuru.Tests
{
    [TestClass]
    public class GuruServiceTests
    {
        [TestMethod]
        public void TestTambahGuru_Valid()
        {
            var service = new GuruService();
            var result = service.Add(new GuruModel { Nama = "Ani", Username = "ani", Password = "rahasia" });

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestTambahGuru_InvalidUsername()
        {
            var service = new GuruService();
            var result = service.Add(new GuruModel { Nama = "Rina", Username = "rina123", Password = "rahasia" });

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestTambahGuru_PasswordTerlaluPendek()
        {
            var service = new GuruService();
            var result = service.Add(new GuruModel { Nama = "Lina", Username = "lina", Password = "123" });

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestTambahGuru_UsernameKosong()
        {
            var service = new GuruService();
            var result = service.Add(new GuruModel { Nama = "Tina", Username = "", Password = "rahasia" });

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestTambahGuru_NamaKosong()
        {
            var service = new GuruService();
            var result = service.Add(new GuruModel { Nama = "", Username = "yani", Password = "rahasia" });

            Assert.IsTrue(result); // Nama kosong tidak divalidasi
        }

        [TestMethod]
        public void TestHapusGuru_Berhasil()
        {
            var service = new GuruService();
            var guru = new GuruModel { Nama = "Joko", Username = "joko", Password = "rahasia" };
            service.Add(guru);

            var result = service.Delete(guru.Id);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestHapusGuru_IdTidakAda()
        {
            var service = new GuruService();

            var result = service.Delete("id-tidak-ada");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestGetAllGuru()
        {
            var service = new GuruService();
            service.Add(new GuruModel { Nama = "Budi", Username = "budi", Password = "password" });

            var result = service.GetAll();

            Assert.AreEqual(1, result.Count());
        }
    }
}
