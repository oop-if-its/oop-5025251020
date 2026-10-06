namespace Pertemuan06;

public class Item
{
    public string Judul { get; }

    // SUDAH LENGKAP -- jangan diubah.
    public Item(string judul)
    {
        if (string.IsNullOrWhiteSpace(judul))
            throw new ArgumentException("Judul tidak boleh kosong.", nameof(judul));
        Judul = judul;
    }

    public virtual int HitungDenda(int hariTerlambat)
    {
        if (hariTerlambat <= 0) return 0;
        return 1000 * hariTerlambat;
    }

    public virtual int MasaPinjamHari => 7;

    public virtual string Deskripsi()
    {
        return $"[{Judul}]";
    }

    public override string ToString()
    {
        return Deskripsi();
    }

    // TODO(Level 10 (bonus)): override Equals(object?) dan GetHashCode(): dua
    //   Item dianggap SAMA bila jenis (GetType()) sama DAN Judul sama (huruf
    //   besar/kecil diabaikan). GetHashCode harus konsisten dengan Equals.
    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (obj.GetType() != this.GetType()) return false;
        Item? item = (Item?)obj;
        if (item == null) return false;

        if (item.Judul.ToLower() != this.Judul.ToLower()) return false;
        return true;
    }

    public override int GetHashCode()
    {
        return 1346913646;
    }
}
