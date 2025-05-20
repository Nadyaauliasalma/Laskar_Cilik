using SharedModels;
using LaporanPerkembanganCLI.Services;
using System;

Console.WriteLine("=== Cetak Laporan Perkembangan Siswa ===");

Console.Write("Masukkan ID siswa (contoh: 1): ");
string? id = Console.ReadLine();

if (string.IsNullOrWhiteSpace(id))
{
    Console.WriteLine("❌ ID tidak boleh kosong.");
    return;
}

ReportData? data = await ApiClient.GetReportByIdAsync(id);

if (data == null)
{
    Console.WriteLine("❌ Gagal mengambil data dari API. Periksa koneksi atau ID.");
    return;
}

// Buat nama dan lokasi file
string fileName = $"Laporan_{data.NamaSiswa.Replace(" ", "_")}.pdf";
string downloadsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
string fullPath = Path.Combine(downloadsFolder, fileName);

// Cetak laporan ke PDF
bool success = PdfGenerator.GenerateReport(data, fullPath);

if (success)
{
    Console.WriteLine($"\n✅ PDF berhasil dibuat.");
    Console.WriteLine($"📁 Lokasi file: {fullPath}");
}
else
{
    Console.WriteLine("❌ Terjadi kesalahan saat membuat PDF.");
}

Console.WriteLine("\nTekan sembarang tombol untuk keluar...");
Console.ReadKey();
