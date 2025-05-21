using System;
using System.Collections.Generic;
using System.Linq;

namespace PerkembanganSiswa
{
    public class PerkembanganService
    {
        private readonly RuntimeConfigService runtimeConfig;
        private static int idCounter = 1;
        public static List<PerkembanganSiswa> database = new();

        public PerkembanganService(RuntimeConfigService config)
        {
            runtimeConfig = config;
        }

        public PerkembanganSiswa Post(InputPerkembanganDto dto)
        {
            if (string.IsNullOrEmpty(dto.NomorInduk))
                throw new ArgumentException("Nomor Induk tidak boleh kosong");

            if (dto.NomorInduk.Length > runtimeConfig.Config.MaxKarakterNomorInduk)
                throw new ArgumentException("Nomor Induk terlalu panjang");

            var catatan = new PerkembanganSiswa
            {
                Id = idCounter++,
                NamaSiswa = dto.NamaSiswa,
                NomorInduk = dto.NomorInduk,
                Kelas = dto.Kelas,
                Semester = dto.Semester,
                TahunAjaran = dto.TahunAjaran,
                CatatanPerKategori = ValidasiDanEkstrakKategori(dto.Kategori)
            };

            database.Add(catatan);
            return catatan;
        }

        public bool Update(int id, InputPerkembanganDto dto)
        {
            var existing = GetById(id);
            if (existing == null) return false;

            existing.NamaSiswa = dto.NamaSiswa;
            existing.NomorInduk = dto.NomorInduk;
            existing.Kelas = dto.Kelas;
            existing.Semester = dto.Semester;
            existing.TahunAjaran = dto.TahunAjaran;
            existing.CatatanPerKategori = ValidasiDanEkstrakKategori(dto.Kategori);

            return true;
        }

        public bool Delete(int id)
        {
            var target = GetById(id);
            return target != null && database.Remove(target);
        }

        public PerkembanganSiswa? GetById(int id) => database.FirstOrDefault(p => p.Id == id);

        public List<PerkembanganSiswa> GetAll() => database;

        private Dictionary<KategoriPerkembangan.Kategori, string> ValidasiDanEkstrakKategori(KategoriPerkembangan kategoriDto)
        {
            var hasil = new Dictionary<KategoriPerkembangan.Kategori, string>();

            foreach (var prop in kategoriDto.GetType().GetProperties())
            {
                string key = prop.Name;
                string? value = prop.GetValue(kategoriDto)?.ToString();

                if (!string.IsNullOrEmpty(value) &&
                    Enum.TryParse(typeof(KategoriPerkembangan.Kategori), key, true, out var result))
                {
                    var kategori = (KategoriPerkembangan.Kategori)result;

                    if (runtimeConfig.Config.MaxKarakterPerKategori != null &&
                        runtimeConfig.Config.MaxKarakterPerKategori.TryGetValue(kategori.ToString(), out int batas))
                    {
                        if (value.Length > batas)
                            throw new Exception($"Catatan untuk kategori {kategori} terlalu panjang (maksimal {batas} karakter).");
                    }

                    hasil[kategori] = value;
                }
            }

            return hasil;
        }
    }
}
