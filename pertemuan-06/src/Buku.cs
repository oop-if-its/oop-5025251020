// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan06;

public class Buku : Item
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
        return 2000 * Math.Max(hariTerlambat, 0);
    }

    public override int MasaPinjamHari => 14;

    public override string Deskripsi()
    {
        return $"{base.Deskripsi()} oleh {Penulis}";
    }
}
