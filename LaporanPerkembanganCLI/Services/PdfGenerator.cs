using System;
using System.Diagnostics;
using System.IO;
using LaporanPerkembanganCLI.Models;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;

namespace LaporanPerkembanganCLI.Services
{
    public class PdfGenerator
    {
        public static void GenerateReport(ReportData data, string filePath)
        {
            PdfDocument doc = new PdfDocument();
            doc.Info.Title = $"Laporan Perkembangan - {data.NamaSiswa}";

            PdfPage page = doc.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont titleFont = new XFont("Times New Roman", 16);
            XFont labelFont = new XFont("Times New Roman", 12);
            XFont textFont = new XFont("Times New Roman", 12);
            XTextFormatter tf = new XTextFormatter(gfx);

            double y = 40;

            // Judul
            gfx.DrawString("Laporan Perkembangan Siswa", titleFont, XBrushes.DarkBlue, new XRect(0, y, page.Width, 30), XStringFormats.TopCenter);
            y += 40;

            // Fungsi menampilkan label dan isinya
            void DrawLabelValue(string label, string value)
            {
                gfx.DrawString(label, labelFont, XBrushes.Black, 40, y);
                gfx.DrawString($": {value}", textFont, XBrushes.Black, 150, y);
                y += 20;
            }

            DrawLabelValue("Nama Anak", data.NamaSiswa);
            DrawLabelValue("Nomor Induk", data.NomorInduk);
            DrawLabelValue("Kelas", data.Kelas);
            DrawLabelValue("Semester", data.Semester);
            DrawLabelValue("Tahun Ajaran", data.TahunAjaran);

            y += 20;

            // Fungsi menampilkan bagian catatan
            void DrawSection(string title, string content)
            {
                gfx.DrawString(title, labelFont, XBrushes.Black, 40, y);
                y += 20;

                tf.DrawString(content, textFont, XBrushes.Black, new XRect(40, y, page.Width - 80, 100), XStringFormats.TopLeft);
                y += 110;
            }

            DrawSection("1. Nilai Agama dan Budi Pekerti", data.NilaiAgama);
            DrawSection("2. Jati Diri", data.JatiDiri);
            DrawSection("3. Literasi dan Matematika", data.LiterasiMatematika);
            DrawSection("4. Sains, Teknologi dan Rekayasa", data.SainsTeknologiRekayasa);
            DrawSection("5. Proyek Penguatan Profil Pelajar Pancasila", data.ProyekPancasila);

            doc.Save(filePath);
            doc.Close();

            Console.WriteLine($"\n✅ PDF berhasil dibuat: {Path.GetFullPath(filePath)}");
            Console.WriteLine("\nTekan sembarang tombol untuk keluar...");
            Console.ReadKey();
        }
    }
}
