using System;
using System.Xml.Linq;


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
                TahunAjaran = dto.TahunAjaran
            };

            foreach (var prop in dto.Kategori.GetType().GetProperties())
            {
                string key = prop.Name;
                string? value = prop.GetValue(dto.Kategori)?.ToString();

                if (!string.IsNullOrEmpty(value) &&
                    Enum.TryParse(typeof(KategoriPerkembangan.Kategori), key, true, out var result))
                {
                    var kategori = (KategoriPerkembangan.Kategori)result;

                    if (value.Length > runtimeConfig.Config.MaxKarakterCatatan)
                        throw new Exception($"Catatan untuk kategori {kategori} terlalu panjang");

                    catatan.CatatanPerKategori[kategori] = value;
                }
            }

            database.Add(catatan);
            return catatan;
        }

        public PerkembanganSiswa? GetById(int id) => database.FirstOrDefault(p => p.Id == id);

        public bool Update(int id, InputPerkembanganDto dto)
        {
            var existing = GetById(id);
            if (existing == null) return false;

            existing.NamaSiswa = dto.NamaSiswa;
            existing.NomorInduk = dto.NomorInduk;
            existing.Kelas = dto.Kelas;
            existing.Semester = dto.Semester;
            existing.TahunAjaran = dto.TahunAjaran;
            existing.CatatanPerKategori.Clear();

            foreach (var prop in dto.Kategori.GetType().GetProperties())
            {
                string key = prop.Name;
                string? value = prop.GetValue(dto.Kategori)?.ToString();

                if (!string.IsNullOrEmpty(value) &&
                    Enum.TryParse(typeof(KategoriPerkembangan.Kategori), key, true, out var result))
                {
                    var kategori = (KategoriPerkembangan.Kategori)result;
                    existing.CatatanPerKategori[kategori] = value;
                }
            }

            return true;
        }

        public bool Delete(int id)
        {
            var target = GetById(id);
            return target != null && database.Remove(target);
        }

        public List<PerkembanganSiswa> GetAll() => database;
    }

}
