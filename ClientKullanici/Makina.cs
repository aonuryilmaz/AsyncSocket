using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientKullanici
{
    public class Makina
    {
        public int MakinaId { get; set; }
        public string MakinaAdi { get; set; }
        public MakinaType Tip { get; set; }
        public int UretimHizi { get; set; }
        public MakinaStatus Durum { get; set; }
        public List<IsEmri> Isler { get; set; }
    }
    public enum MakinaType
    {
        CNC,
        DÖKÜM,
        KILIF,
        KAPLAMA
    }
    public enum MakinaStatus
    {
        BUSY,
        EMPTY
    }
}
