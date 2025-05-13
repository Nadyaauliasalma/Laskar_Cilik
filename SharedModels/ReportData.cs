using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class ReportData
    {
        public string Id { get; set; } = string.Empty;
        public string NamaSiswa { get; set; } = string.Empty;
        public string NomorInduk { get; set; } = string.Empty;
        public string Kelas { get; set; } = string.Empty;
        public string Semester { get; set; } = string.Empty;
        public string TahunAjaran { get; set; } = string.Empty;

        public string NilaiAgama { get; set; } = string.Empty;
        public string JatiDiri { get; set; } = string.Empty;
        public string LiterasiMatematika { get; set; } = string.Empty;
        public string SainsTeknologiRekayasa { get; set; } = string.Empty;
        public string ProyekPancasila { get; set; } = string.Empty;
    }
}
