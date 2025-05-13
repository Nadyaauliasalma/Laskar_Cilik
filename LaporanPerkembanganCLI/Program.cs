using LaporanPerkembanganCLI.Models;
using LaporanPerkembanganCLI.Services;

Console.WriteLine("=== CLI: Cetak Laporan Perkembangan Siswa ===");

Console.Write("Masukkan ID siswa (contoh: 1): ");
string id = Console.ReadLine();

ReportData? data = await ApiClient.GetReportByIdAsync(id);

if (data == null)
{
    Console.WriteLine("❌ Gagal mengambil data dari API.");
    return;
}

string fileName = $"Laporan_{data.NamaSiswa.Replace(" ", "_")}.pdf";
PdfGenerator.GenerateReport(data, fileName);

Console.WriteLine($"\n✅ PDF berhasil dibuat: {Path.GetFullPath(fileName)}");
Console.WriteLine($"\n📄 PDF disimpan di: {Path.GetFullPath(fileName)}");
Console.WriteLine("\nTekan sembarang tombol untuk keluar...");
Console.ReadKey();