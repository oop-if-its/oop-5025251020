namespace Pertemuan07;

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
            ((IDapatDipinjam)item).Pinjam();
            _notifikasi.Kirim($"{namaAnggota} meminjam {item.Judul}");
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
