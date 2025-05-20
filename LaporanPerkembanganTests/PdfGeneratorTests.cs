using Xunit;
using LaporanPerkembanganCLI.Services;
using SharedModels;
using System.IO;
using System;

namespace LaporanPerkembanganTests
{
    public class PdfGeneratorTests
    {
        [Fact]
        public void GenerateReport_CreatesPdfFileSuccessfully()
        {
            // Arrange
            var data = new ReportData
            {
                NamaSiswa = "Test Siswa",
                NomorInduk = "0001",
                Kelas = "TK A",
                Semester = "1",
                TahunAjaran = "2024/2025",
                NilaiAgama = "Baik",
                JatiDiri = "Mandiri",
                LiterasiMatematika = "Cukup",
                SainsTeknologiRekayasa = "Baik",
                ProyekPancasila = "Aktif"
            };

            string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";
            string fileName = "Laporan_TestSiswa.pdf";
            string filePath = Path.Combine(downloadsPath, fileName);

            // Act
            bool result = PdfGenerator.GenerateReport(data, filePath);

            // Assert
            Assert.True(result);                   // memastikan fungsi tidak error
            Assert.True(File.Exists(filePath));    // memastikan file benar-benar dibuat

            // Cleanup
            File.Delete(filePath);
        }
    }
}
