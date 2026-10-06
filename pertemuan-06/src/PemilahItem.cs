namespace Pertemuan06;

public static class PemilahItem
{
    public static List<Buku> AmbilBuku(IEnumerable<Item> daftar)
    {
        ArgumentNullException.ThrowIfNull(daftar);

        List<Buku> daftarBuku = daftar.OfType<Buku>().ToList();
        return daftarBuku;
    }

    public static string? PenulisAtauNull(Item item)
    {
        if (item is not Buku) return null;
        Buku buku = (Buku)item;
        return buku.Penulis;
    }

    public static Buku KeBuku(Item item)
    {
        ArgumentNullException.ThrowIfNull(item);
        return (Buku)item;
    }

    public static bool CobaKeBuku(Item? item, out Buku? buku)
    {
        if (item is not Buku)
        {
            buku = null;
            return false;
        }
        buku = (Buku)item;
        return true;
    }
}
