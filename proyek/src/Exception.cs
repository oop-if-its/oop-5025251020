namespace Proyek;

public class StatusTidakValidException : Exception
{
    public StatusTidakValidException(StatusPaket currentStatus, StatusPaket newStatus)
        : base($"Status tidak valid: {currentStatus} -> {newStatus}") { }
}

public class PaketTidakDitemukanException : Exception
{
    public PaketTidakDitemukanException(string nomorResi)
        : base($"Paket dengan nomor resi {nomorResi} tidak ditemukan.") { }
}

public class KontainerTidakDitemukanException : Exception
{
    public KontainerTidakDitemukanException(string nomorId)
        : base($"Kontainer dengan ID {nomorId} tidak ditemukan.") { }
}