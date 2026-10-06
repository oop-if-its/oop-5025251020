// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan06;

// SUDAH LENGKAP -- jangan diubah. Satu baris peminjaman yang terlambat.
public record Peminjaman(Item Item, int HariTerlambat);

// Kasir TIDAK perlu tahu jenis item apa pun: ia hanya memanggil
// item.HitungDenda(...) dan objek yang sebenarnya menentukan hasilnya.
public class Kasir
{
    public int TotalDenda(IEnumerable<Peminjaman> daftar)
    {
        ArgumentNullException.ThrowIfNull(daftar);
        int result = 0;
        foreach (var d in daftar)
        {
            result += d.Item.HitungDenda(d.HariTerlambat);
        }
        return result;
    }

    public Item? ItemDenganDendaTertinggi(IEnumerable<Peminjaman> daftar)
    {
        // TODO(Level 6): null -> ArgumentNullException; kembalikan Item dengan
        //   denda TERTINGGI (kalau seri, ambil yang pertama muncul); daftar
        //   kosong -> null.
        ArgumentNullException.ThrowIfNull(daftar);

        Item? result = null;
        int temp = 0;
        foreach (var d in daftar)
        {
            if (d.Item.HitungDenda(d.HariTerlambat) > temp)
            {
                result = d.Item;
                temp = d.Item.HitungDenda(d.HariTerlambat);
            }
        }

        return result;
    }
}
