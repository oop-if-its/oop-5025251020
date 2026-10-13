namespace Pertemuan07;

public class Dvd : Item, IDapatDipinjam
{
    public int DurasiMenit { get; }

    public Dvd(string judul, int durasiMenit) : base(judul)
    {
        if (durasiMenit <= 0)
            throw new ArgumentOutOfRangeException(nameof(durasiMenit), "Durasi harus positif.");
        DurasiMenit = durasiMenit;
    }

    public override int HitungDenda(int hariTerlambat)
    {
        if (hariTerlambat <= 0) return 0;
        int result = hariTerlambat * 5000;

        result = Math.Min(result, 50000);
        return result;
    }

    public override int MasaPinjamHari => 2;


    private readonly StatusPinjam _status = new();
    public bool Tersedia => !_status.Dipinjam;
    public void Pinjam() => _status.Tandai();
    public void Kembalikan()
    {
        _status.Lepas();
    }

    protected override string Catatan() => "Periksa kondisi disk";
}
