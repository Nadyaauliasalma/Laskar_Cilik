using System;
public class PerkembanganSiswa
{
    public int Id { get; set; }
    public string NamaSiswa { get; set; }
    public string NomorInduk { get; set; }
    public string Kelas { get; set; }
    public int Semester { get; set; }
    public int TahunAjaran { get; set; }
    public DateTime Tanggal { get; set; } = DateTime.Now;
    public Dictionary<KategoriPerkembangan.Kategori, string> CatatanPerKategori { get; set; } = new();
}
