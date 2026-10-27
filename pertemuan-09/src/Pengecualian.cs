namespace Pertemuan08;

public class BukuTidakTersediaException : Exception
{
    public string Judul { get; }

    public BukuTidakTersediaException(string judul)
        : base($"Buku '{judul}' tidak tersedia.")
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(judul);
        Judul = judul;
    }

    public BukuTidakTersediaException(string judul, Exception inner)
        : base($"Buku '{judul}' tidak tersedia.", inner)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(judul);
        Judul = judul;
    }
}

public class DataTidakValidException : Exception
{
    public DataTidakValidException(string pesan) : base(pesan) { }
    public DataTidakValidException(string pesan, Exception inner) : base(pesan, inner) { }
}

// Dipakai Level 10 (bonus): exception yang membawa SEMUA kesalahan sekaligus.
public class ValidasiException : Exception
{
    public IReadOnlyList<string> Daftar { get; }

    public ValidasiException(IReadOnlyList<string> daftar)
        : base($"Ditemukan {daftar.Count} kesalahan validasi.")
    {
        Daftar = daftar;
    }
}
