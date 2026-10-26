namespace Proyek;

using System.Security.Cryptography;

public class Kontainer : IBisaDilacak
{
    public string NomorId { get; }
    private readonly List<Paket> _daftarPaket = new();
    private string Lokasi { get; set; } = "Gudang";
    private StatusPaket Status { get; set; }
    private readonly List<RiwayatPelacakan> _riwayatPelacakan = new();
    public IReadOnlyList<RiwayatPelacakan> RiwayatPelacakan => _riwayatPelacakan.AsReadOnly();

    public Kontainer()
    {
        NomorId = RandomNumberGenerator.GetInt32(100000, 999999).ToString();
        Status = StatusPaket.Disiapkan;
        _riwayatPelacakan.Add(new RiwayatPelacakan(DateTime.Now, Lokasi, Status));
    }

    public void TambahPaket(Paket paket)
    {
        ArgumentNullException.ThrowIfNull(paket);

        if (_daftarPaket.Any(p => p.NomorResi == paket.NomorResi))
            throw new InvalidOperationException($"Paket dengan resi {paket.NomorResi} sudah berada di kontainer ini.");

        paket.MasukkanKontainer(NomorId, Lokasi);
        _daftarPaket.Add(paket);
    }

    public IReadOnlyList<Paket> DaftarPaketTerurut =>
        _daftarPaket
            .Order()
            .ToList();

    public void UpdateLokasi(string lokasi)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(lokasi);
        Lokasi = lokasi;
        _riwayatPelacakan.Add(new RiwayatPelacakan(DateTime.Now, Lokasi, Status));
    }

    public void UpdateStatus(StatusPaket statusBaru, string lokasi)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(lokasi);

        if (statusBaru - Status != 1)
            throw new StatusTidakValidException(Status, statusBaru);

        Status = statusBaru;
        Lokasi = lokasi;
        _riwayatPelacakan.Add(new RiwayatPelacakan(DateTime.Now, Lokasi, Status));
    }

    public string Lacak()
    {
        return $"Kontainer {NomorId} berisi {_daftarPaket.Count} paket, berstatus {Status}, di {Lokasi}";
    }

    public float NilaiKontainer => _daftarPaket.Sum(p => p.HitungOngkir());
}
