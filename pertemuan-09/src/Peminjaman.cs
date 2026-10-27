namespace Pertemuan08;

public class Peminjaman
{
    public static int JumlahInisialisasi { get; private set; }

    public string Judul { get; }
    public string Anggota { get; }
    public int LamaHari { get; }
    public DateOnly Tanggal { get; }

    public Peminjaman(string judul, string anggota, int lamaHari, DateOnly tanggal)
    {
        if (string.IsNullOrWhiteSpace(judul))
            throw new ArgumentException("Judul tidak boleh kosong.", nameof(judul));
        if (string.IsNullOrWhiteSpace(anggota))
            throw new ArgumentException("Anggota tidak boleh kosong.", nameof(anggota));
        if (lamaHari < 1)
            throw new ArgumentOutOfRangeException(nameof(lamaHari), "Lama pinjam minimal 1 hari.");

        Judul = judul;
        Anggota = anggota;
        LamaHari = lamaHari;
        Tanggal = tanggal;
        JumlahInisialisasi++;
    }

    public Peminjaman(string judul, string anggota, int lamaHari)
      : this(judul, anggota, lamaHari, DateOnly.FromDateTime(DateTime.Today)) { }

    public Peminjaman(string judul, string anggota)
      : this(judul, anggota, 7) { }

    public Peminjaman(string judul)
      : this(judul, "Umum") { }
}
