namespace Pertemuan08;

public class Koneksi : IDisposable
{
    private static int _jumlahTerbuka;
    public static int JumlahTerbuka => _jumlahTerbuka;

    public bool Terbuka { get; private set; }

    public Koneksi()
    {
        Terbuka = true;
        _jumlahTerbuka++;
    }

    public string Query(string sql)
    {
        if (!Terbuka)
            throw new ObjectDisposedException(nameof(Koneksi));
        return $"hasil: {sql}";
    }

    public void Dispose()
    {
        if (Terbuka)
            _jumlahTerbuka--;
        Terbuka = false;
    }
}

public static class Pemakai
{
    public static string Jalankan(string sql, bool sengajaGagal)
    {
        using Koneksi koneksi = new Koneksi();
        try
        {
            var hasil = koneksi.Query(sql);
            if (sengajaGagal)
                throw new InvalidOperationException("gagal");
            return hasil;
        }
        finally
        {
            koneksi.Dispose();
        }
    }
}
