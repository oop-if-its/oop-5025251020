namespace Proyek;
using System.Security.Cryptography;

public class Kontainer : IBisaDilacak
{
    private string NomorId { get; init; }
    private readonly List<Paket> _daftarPaket = new();
    private string Lokasi { get; set; } = "Gudang";

    public Kontainer()
    {
        NomorId = RandomNumberGenerator.GetInt32(100000, 999999).ToString();
    }

    public void TambahPaket(Paket paket)
    {
        ArgumentNullException.ThrowIfNull(paket);
        _daftarPaket.Add(paket);
        paket.MasukkanKontainer(NomorId);
    }

    public IReadOnlyList<Paket> DaftarPaketTerurut =>
        _daftarPaket
            .OrderBy(paket => paket switch
            {
                PaketKilat => 0,
                PaketKargo => 1,
                PaketReguler => 2,
                _ => 3
            })
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
}