// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan07;

// TODO(Level 8): implementasikan IDapatDicetak (implementasi biasa / implisit --
//   method Cetak() bersifat public).
public class Buku : Item, IDapatDipinjam, IDapatDiperpanjang
{
    public string Penulis { get; }

    // SUDAH LENGKAP -- jangan diubah.
    public Buku(string judul, string penulis) : base(judul)
    {
        if (string.IsNullOrWhiteSpace(penulis))
            throw new ArgumentException("Penulis tidak boleh kosong.", nameof(penulis));
        Penulis = penulis;
    }

    public override int HitungDenda(int hariTerlambat)
    {
        if (hariTerlambat <= 0) return 0;
        return hariTerlambat * 2000;
    }

    public override int MasaPinjamHari => 14;

    private readonly StatusPinjam _status = new();
    public int JumlahPerpanjangan { get; private set; } = 0;
    public bool Tersedia => !_status.Dipinjam;
    public void Pinjam() => _status.Tandai();
    public void Kembalikan()
    {
        _status.Lepas();
        JumlahPerpanjangan = 0;
    }

    public bool Perpanjang()
    {
        if (Tersedia) throw new InvalidOperationException();

        if (JumlahPerpanjangan >= 2) return false;

        JumlahPerpanjangan += 1;
        return true;
    }

    // TODO(Level 8): implementasi IDapatDicetak -> "Buku: <Judul> oleh
    //   <Penulis>".
}
