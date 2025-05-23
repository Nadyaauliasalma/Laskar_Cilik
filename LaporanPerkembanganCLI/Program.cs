using SharedModels;
using LaporanPerkembanganCLI.Services;
using System;

Console.WriteLine("=== Cetak Laporan Perkembangan Siswa ===");

// Ambil semua siswa
var semuaSiswa = await ApiClient.GetAllReportsAsync();

if (semuaSiswa.Count == 0)
{
    Console.WriteLine(" Tidak ada data siswa yang tersedia.");
    return;
}

// Tampilkan daftar
Console.WriteLine("\n=== Daftar Siswa ===");
foreach (var siswa in semuaSiswa)
{
    Console.WriteLine($"[{siswa.Id}] {siswa.NamaSiswa}");
}

// Input
Console.Write("\nMasukkan ID siswa yang ingin dicetak: ");
string? id = Console.ReadLine();

if (string.IsNullOrWhiteSpace(id))
{
    Console.WriteLine(" ID tidak boleh kosong.");
    return;
}

// Ambil data berdasarkan ID
ReportData? data = await ApiClient.GetReportByIdAsync(id);

if (data == null)
{
    Console.WriteLine("Data siswa tidak ditemukan.");
    return;
}

// Buat nama file dan cetak
string fileName = $"Laporan_{data.NamaSiswa.Replace(" ", "_")}.pdf";
string downloadsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
string fullPath = Path.Combine(downloadsFolder, fileName);

bool success = PdfGenerator.GenerateReport(data, fullPath);

if (success)
{
    Console.WriteLine($"\n PDF berhasil dibuat di: {fullPath}");
}
else
{
    Console.WriteLine("Gagal membuat PDF.");
}

Console.WriteLine("\nTekan sembarang tombol untuk keluar...");
Console.ReadKey();
