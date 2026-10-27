namespace Pertemuan08;

public record HasilPinjam(bool Berhasil, string Pesan);

public class Rak
{
    private readonly Dictionary<string, int> _stok;

    public Rak(IDictionary<string, int> stokAwal)
    {
        _stok = new Dictionary<string, int>(stokAwal, StringComparer.OrdinalIgnoreCase);
    }

    public int Stok(string judul) => _stok.TryGetValue(judul, out var s) ? s : 0;

    public void Ambil(string judul)
    {
        ArgumentException.ThrowIfNullOrEmpty(judul);
        if (!_stok.ContainsKey(judul) || _stok.GetValueOrDefault(judul, 0) == 0)
            throw new BukuTidakTersediaException(judul);

        _stok[judul]--;
    }
}

public class LayananPinjam
{
    private readonly Rak _rak;

    public int JumlahPercobaan { get; private set; }

    public LayananPinjam(Rak rak)
    {
        ArgumentNullException.ThrowIfNull(rak);
        _rak = rak;
    }

    public HasilPinjam CobaPinjam(string judul)
    {
        // TODO(Level 5): bungkus _rak.Ambil(judul) dengan try/catch/finally.
        //   Sukses -> HasilPinjam(true, "Berhasil meminjam: <judul>"). Menangkap
        //   BukuTidakTersediaException -> HasilPinjam(false, ex.Message).
        //   Exception LAIN (mis. ArgumentException) JANGAN ditangkap -- biarkan
        //   menjalar. Di blok `finally`, naikkan JumlahPercobaan -- harus selalu
        //   bertambah tiap CobaPinjam dipanggil, apa pun hasilnya (sukses,
        //   gagal, atau exception yang menjalar).
        try
        {
            _rak.Ambil(judul);
            return new HasilPinjam(true, $"Berhasil meminjam: {judul}");
        }
        catch (BukuTidakTersediaException e)
        {
            return new HasilPinjam(false, e.Message);
        }
        finally
        {
            JumlahPercobaan++;
        }
    }
}
