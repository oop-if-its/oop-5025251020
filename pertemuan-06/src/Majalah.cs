// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan06;

public class Majalah : Item
{
    public int Edisi { get; }

    // SUDAH LENGKAP -- jangan diubah.
    public Majalah(string judul, int edisi) : base(judul)
    {
        if (edisi <= 0)
            throw new ArgumentOutOfRangeException(nameof(edisi), "Edisi harus positif.");
        Edisi = edisi;
    }

    public override int HitungDenda(int hariTerlambat)
    {
        return 500 * Math.Max(hariTerlambat, 0);
    }

    public override int MasaPinjamHari => 3;

    public override string Deskripsi()
    {
        return $"{base.Deskripsi()} edisi {Edisi}";
    }
}
