namespace Pertemuan06;

public class Pemroses
{
    public string Proses(Item item)
    {
        return $"Item: {item.Judul}";
    }

    public string Proses(Buku buku)
    {
        return $"Buku: {buku.Judul}";
    }

    public string Proses(Majalah majalah)
    {
        return $"Majalah: {majalah.Judul}";
    }

    public string Kategori(Item item)
    {
        ArgumentNullException.ThrowIfNull(item);
        switch (item)
        {
            case Buku:
                return "Buku";
            case Majalah:
                return "Majalah";
            case Dvd:
                return "DVD";

            default:
                return "Item";
        }
    }
}
