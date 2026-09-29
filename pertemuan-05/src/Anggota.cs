namespace Pertemuan05;

// Kelas dasar (base class) untuk semua jenis anggota perpustakaan.
public class Anggota
{
    public string Id { get; }
    public string Nama { get; }

    // Komposisi: Anggota memiliki Alamat.
    public Alamat Alamat { get; }
    public int BatasPinjam { get; protected set; } = 2;
    public int JumlahPinjam { get; private set; }

    private readonly LogAktivitas _log = new();
    public IReadOnlyList<string> Riwayat
    {
        get
        {
            return _log.Semua;
        }
    }

    public Anggota(string id, string nama, Alamat alamat)
    {
        if (string.IsNullOrWhiteSpace(nama))
            throw new ArgumentException();
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException();
        if (alamat == null)
            throw new ArgumentNullException();
        Id = id;
        Nama = nama;
        Alamat = alamat;
    }

    public string Info()
    {
        return Id + " - " + Nama;
    }

    public void Pinjam(string judul)
    {
        if (string.IsNullOrWhiteSpace(judul))
            throw new ArgumentException();
        if (JumlahPinjam >= BatasPinjam)
            throw new InvalidOperationException();
        JumlahPinjam++;
        _log.Catat($"Pinjam: {judul}");
    }
}
