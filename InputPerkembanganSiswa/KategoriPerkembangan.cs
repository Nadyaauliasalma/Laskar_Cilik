using System;
using System.Collections.Generic;



namespace PerkembanganSiswa
{

    public class KategoriPerkembangan
    {
        public string NilaiAgama { get; set; }
        public string JatiDiri { get; set; }
        public string LiterasiDanSTEM { get; set; }
        public string ProyekPenguatanPancasila { get; set; }

        public enum Kategori
        {
            NilaiAgama,
            JatiDiri,
            LiterasiDanSTEM,
            ProyekPenguatanPancasila
        }

        public static readonly IReadOnlyDictionary<Kategori, string> Deskripsi = new Dictionary<Kategori, string>
    {
        { Kategori.NilaiAgama, "Nilai Agama" },
        { Kategori.JatiDiri, "Jati Diri" },
        { Kategori.LiterasiDanSTEM, "Literasi & Matematika, Sains, Teknologi dan Rekayasa" },
        { Kategori.ProyekPenguatanPancasila, "Proyek Penguatan Pancasila" }
    };
    }

}