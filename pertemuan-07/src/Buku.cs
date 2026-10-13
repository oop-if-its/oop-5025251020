namespace Pertemuan07;

public class Buku : Item, IDapatDipinjam, IDapatDiperpanjang, IDapatDicetak
{
    public string Penulis { get; }

    public Buku(string judul, string penulis) : base(judul)
    {
        if (string.IsNullOrWhiteSpace(penulis))
            throw new ArgumentException("Penulis tidak boleh kosong.", nameof(penulis));
        Penulis = penulis;
    }


    public override int HitungDenda(int hariTerlambat)
    {
        if (hariTerlambat <= 0) return 0;
        return hariTerlambat * 2000;
    }

    public override int MasaPinjamHari => 14;

    private readonly StatusPinjam _status = new();
    public int JumlahPerpanjangan { get; private set; } = 0;
    public bool Tersedia => !_status.Dipinjam;
    public void Pinjam() => _status.Tandai();
    public void Kembalikan()
    {
        _status.Lepas();
        JumlahPerpanjangan = 0;
    }

    public bool Perpanjang()
    {
        if (Tersedia) throw new InvalidOperationException();

        if (JumlahPerpanjangan >= 2) return false;

        JumlahPerpanjangan += 1;
        return true;
    }

    public string Cetak() => $"Buku: {Judul} oleh {Penulis}";
}
