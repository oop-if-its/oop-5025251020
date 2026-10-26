namespace Proyek;

public class Ekspedisi
{
    private string _nama;
    public string Nama => _nama;

    private readonly List<Paket> _paket = new();
    public IReadOnlyList<Paket> DaftarPaket => _paket.Order().ToList();

    private readonly List<Kontainer> _kontainer = new();
    public IReadOnlyList<Kontainer> DaftarKontainer => _kontainer.AsReadOnly();

    public Ekspedisi(string nama)
    {
        _nama = nama;
    }
    public Kontainer CariKontainer(string idKontainer)
    {
        Kontainer? kontainer = _kontainer.FirstOrDefault(k => k.NomorId == idKontainer);
        if (kontainer is null)
            throw new ArgumentNullException();

        return kontainer;
    }

    public Paket CariPaket(string resiPaket)
    {
        Paket? paket = _paket.FirstOrDefault(p => p.NomorResi == resiPaket);
        if (paket is null)
            throw new PaketTidakDitemukanException(resiPaket);

        return paket;
    }

    public void TambahPaket(Paket paket, string idKontainer = "")
    {
        ArgumentNullException.ThrowIfNull(paket);
        Paket? adaPaket = _paket.FirstOrDefault(p => p.NomorResi == paket.NomorResi);
        if (adaPaket is not null)
            throw new InvalidOperationException();

        _paket.Add(paket);
        if (!string.IsNullOrEmpty(idKontainer))
        {
            Kontainer kontainer = CariKontainer(idKontainer);
            kontainer.TambahPaket(paket);
        }
    }

    public void TambahKontainer(Kontainer kontainer)
    {
        ArgumentNullException.ThrowIfNull(kontainer);
        Kontainer? adaKontainer = _kontainer.FirstOrDefault(k => k.NomorId == kontainer.NomorId);
        if (adaKontainer is not null)
            throw new InvalidOperationException();
        _kontainer.Add(kontainer);
    }

    public void MasukkanPaketKeKontainer(string resiPaket, string resiKontainer)
    {
        Paket paket = CariPaket(resiPaket);

        Kontainer kontainer = CariKontainer(resiKontainer);
        kontainer.TambahPaket(paket);
    }
}
