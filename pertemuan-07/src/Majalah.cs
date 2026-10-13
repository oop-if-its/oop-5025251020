namespace Pertemuan07;

public class Majalah : Item, IDapatDipinjam, IDapatDicetak
{
    public int Edisi { get; }

    public Majalah(string judul, int edisi) : base(judul)
    {
        if (edisi <= 0)
            throw new ArgumentOutOfRangeException(nameof(edisi), "Edisi harus positif.");
        Edisi = edisi;
    }

    public override int HitungDenda(int hariTerlambat)
    {
        if (hariTerlambat <= 0) return 0;
        return hariTerlambat * 500;
    }

    public override int MasaPinjamHari => 3;

    private readonly StatusPinjam _status = new();
    public bool Tersedia => !_status.Dipinjam;
    public void Pinjam() => _status.Tandai();
    public void Kembalikan() => _status.Lepas();

    string IDapatDicetak.Cetak() => $"Majalah: {Judul} edisi {Edisi}";
}
