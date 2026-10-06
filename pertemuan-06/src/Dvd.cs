namespace Pertemuan06;

public class Dvd : Item
{
    public int DurasiMenit { get; }

    // SUDAH LENGKAP -- jangan diubah.
    public Dvd(string judul, int durasiMenit) : base(judul)
    {
        if (durasiMenit <= 0)
            throw new ArgumentOutOfRangeException(nameof(durasiMenit), "Durasi harus positif.");
        DurasiMenit = durasiMenit;
    }

    public override int HitungDenda(int hariTerlambat)
    {
        return Math.Min(5000 * Math.Max(hariTerlambat, 0), 50000);
    }

    public override int MasaPinjamHari => 2;

    public override string Deskripsi()
    {
        return $"{base.Deskripsi()} ({DurasiMenit} menit)";
    }
}
