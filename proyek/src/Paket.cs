namespace Proyek;
using System.Security.Cryptography;

public enum StatusPaket
{
    Disiapkan,
    Dikirim,
    Sampai,
    Diterima
}

public abstract class Paket : IBisaDilacak, IEquatable<Paket>
{
    public string NomorResi { get; }
    protected string Penerima { get; }
    protected string Pengirim { get; }

    protected float BeratKg { get; }
    protected float JarakKm { get; }

    protected bool Asuransi { get; set; }

    protected StatusPaket Status { get; set; }
    protected string? NomorKontainer { get; set; }

    protected abstract float HargaPerKgKm { get; }

    private readonly List<RiwayatPelacakan> _riwayatPelacakan = new();
    public IReadOnlyList<RiwayatPelacakan> RiwayatPelacakan => _riwayatPelacakan.AsReadOnly();

    public Paket(string pengirim, string penerima, float beratKg, float jarakKm)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(pengirim);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(penerima);

        if (beratKg <= 0)
            throw new ArgumentOutOfRangeException();
        if(jarakKm <= 0)
            throw new ArgumentOutOfRangeException();

        NomorResi = RandomNumberGenerator.GetInt32(1000000, 9999999).ToString();
        Penerima = penerima;
        Pengirim = pengirim;
        BeratKg = beratKg;
        JarakKm = jarakKm;

        Status = StatusPaket.Disiapkan;
        _riwayatPelacakan.Add(new RiwayatPelacakan(DateTime.Now, "Gudang", Status));
    }

    public void UpdateStatus(StatusPaket newStatus, string lokasi)
    {
        if (newStatus - Status != 1)
            throw new StatusTidakValidException(Status, newStatus);
        Status = newStatus;
        _riwayatPelacakan.Add(new RiwayatPelacakan(DateTime.Now, lokasi, Status));
    }

    public abstract float HitungOngkir();
    public void MasukkanKontainer(string nomorKontainer, string lokasi)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(nomorKontainer);

        if (NomorKontainer is not null)
            throw new InvalidOperationException($"Paket {NomorResi} sudah berada di kontainer {NomorKontainer}.");

        NomorKontainer = nomorKontainer;
        _riwayatPelacakan.Add(new RiwayatPelacakan(DateTime.Now, lokasi, Status));
    }
    public string Lacak()
    {
        string? kontainer = NomorKontainer;
        string lacak = $"Paket {NomorResi} dari {Pengirim} ke {Penerima} berstatus {Status}";

        if (kontainer is not null)
            lacak += $" dan berada di kontainer {kontainer}";

        return lacak;
    }

    public  bool Equals(Paket? other)
    {
        if (other is null)
            return false;

        return NomorResi == other.NomorResi;
    }
}
