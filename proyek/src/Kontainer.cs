namespace Proyek;
using System.Security.Cryptography;

public class Kontainer
{
    public string NomorId { get; init; }
    private readonly List<Paket> _daftarPaket = new();

    public Kontainer()
    {
        NomorId = RandomNumberGenerator.GetInt32(100000, 999999).ToString();
    }

    public void TambahPaket(Paket paket)
    {
        ArgumentNullException.ThrowIfNull(paket);
        _daftarPaket.Add(paket);
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
}