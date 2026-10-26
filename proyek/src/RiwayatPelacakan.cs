namespace Proyek;

public record class  RiwayatPelacakan
{
    public DateTime Waktu { get; init; }
    public string Lokasi { get; init; }
    public StatusPaket Status { get; init; }

    public RiwayatPelacakan(DateTime waktu, string lokasi, StatusPaket status)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(lokasi);
        Waktu = waktu;
        Lokasi = lokasi;
        Status = status;
    }

    public override string ToString()
    {
        return $"[{Waktu}] {Lokasi} - {Status}";
    }
}