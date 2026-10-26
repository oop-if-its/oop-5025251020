namespace Proyek;

using System.Security.Cryptography;

public class Kontainer : IBisaDilacak
{
    public string NomorId { get; }
    private readonly List<Paket> _daftarPaket = new();
    private string Lokasi { get; set; } = "Gudang";
    private readonly List<RiwayatPelacakan> _riwayatPelacakan = new();

    public Kontainer()
    {
        NomorId = RandomNumberGenerator.GetInt32(100000, 999999).ToString();
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
            .OrderBy(paket => paket.DapatkanPrioritas())
            .ToList();

    public void UpdateLokasi(string lokasi)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(lokasi);
        Lokasi = lokasi;
    }

    public string Lacak()
    {
        return $"Kontainer {NomorId} berisi {_daftarPaket.Count} paket di {Lokasi}";
    }

    public float NilaiKontainer => _daftarPaket.Sum(p => p.HitungOngkir());
}
