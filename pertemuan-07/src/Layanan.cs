// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan07;

// Bekerja dengan KONTRAK (interface), bukan kelas konkret.
public static class Katalog
{
    public static int PinjamSemua(IEnumerable<IDapatDipinjam> daftar)
    {
        if (daftar == null) throw new ArgumentNullException();
        int result = 0;
        foreach (IDapatDipinjam idd in daftar)
        {
            try
            {
                idd.Pinjam();
                result++;
            }
            catch (System.Exception) { }
        }
        return result;
    }

    public static List<IDapatDipinjam> HanyaBisaDipinjam(IEnumerable<Item> daftar)
    {
        if (daftar == null)
            throw new ArgumentNullException();

        List<IDapatDipinjam> result = new();

        foreach (Item item in daftar)
        {
            if (item is BukuReferensi) continue;
            if (item is IDapatDipinjam dapatDipinjam)
                result.Add(dapatDipinjam);
        }
        return result;
    }
}

// Dependency Injection: LayananPeminjaman TIDAK membuat pemberitahuannya
// sendiri -- ia menerimanya dari luar lewat konstruktor, sebagai interface.
public class LayananPeminjaman
{
    private readonly INotifikasi _notifikasi;

    public LayananPeminjaman(INotifikasi notifikasi)
    {
        if (notifikasi == null) throw new ArgumentNullException();
        _notifikasi = notifikasi;
    }

    public void Pinjamkan(Item item, string namaAnggota)
    {
        if (item == null) throw new ArgumentNullException();
        if (item is not IDapatDipinjam) throw new InvalidOperationException();
        try
        {

        }
        catch (System.Exception)
        {

            throw;
        }
        // TODO(Level 6): item null -> ArgumentNullException; item BUKAN
        //   IDapatDipinjam -> InvalidOperationException; selain itu panggil
        //   Pinjam() (kalau melempar, biarkan menjalar dan JANGAN kirim
        //   notifikasi), lalu kirim pesan "<namaAnggota> meminjam <Judul>" lewat
        //   INotifikasi.
        throw new NotImplementedException("Level 6 belum diimplementasikan");
    }
}
