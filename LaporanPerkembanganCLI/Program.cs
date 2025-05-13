using SharedModels;
using LaporanPerkembanganCLI.Services;

Console.WriteLine("=== Cetak Laporan Perkembangan Siswa ===");

Console.Write("Masukkan ID siswa (contoh: 1): ");
string id = Console.ReadLine();

ReportData? data = await ApiClient.GetReportByIdAsync(id);

if (data == null)
{
    Console.WriteLine("Gagal mengambil data dari API.");
    return;
}

string fileName = $"Laporan_{data.NamaSiswa.Replace(" ", "_")}.pdf";
string downloadsFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";
string fullPath = Path.Combine(downloadsFolder, fileName);

PdfGenerator.GenerateReport(data, fullPath);

//Console.WriteLine($"\n PDF berhasil dibuat: {Path.GetFullPath(fileName)}");
//Console.WriteLine($"\n PDF disimpan di: {Path.GetFullPath(fileName)}");
Console.WriteLine("\nTekan sembarang tombol untuk keluar...");
Console.ReadKey();