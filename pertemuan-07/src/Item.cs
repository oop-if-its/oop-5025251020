namespace Pertemuan07;

public abstract class Item : IComparable<Item>
{
    public string Judul { get; }

    protected Item(string judul)
    {
        if (string.IsNullOrWhiteSpace(judul))
            throw new ArgumentException("Judul tidak boleh kosong.", nameof(judul));
        Judul = judul;
    }

    public abstract int HitungDenda(int hariTerlambat);

    public abstract int MasaPinjamHari { get; }

    public DateOnly HitungTanggalKembali(DateOnly tanggalPinjam)
    {
        return tanggalPinjam.AddDays(MasaPinjamHari);
    }

    protected virtual string Catatan() => "";

    public string BuatStruk(int hariTerlambat)
    {
        int hari = Math.Max(0, hariTerlambat);

        string result = $"STRUK PENGEMBALIAN\n" +
                        $"Judul: {Judul}\n" +
                        $"Terlambat: {hari} hari\n" +
                        $"Denda: Rp{HitungDenda(hari)}";

        string catatan = Catatan();
        if (!string.IsNullOrEmpty(catatan))
            result += $"\nCatatan: {catatan}";

        return result;
    }

    public int CompareTo(Item? other)
    {
        if (other == null) return 1;
        return string.Compare(Judul, other.Judul, StringComparison.OrdinalIgnoreCase);
    }
}
