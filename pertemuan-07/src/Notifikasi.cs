namespace Pertemuan07;

public class NotifikasiEmail : INotifikasi
{
    private readonly List<string> _terkirim = new();

    public IReadOnlyList<string> Terkirim => _terkirim.AsReadOnly();

    public void Kirim(string pesan)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(pesan);
        _terkirim.Add($"EMAIL: {pesan}");
    }
}

public class NotifikasiSms : INotifikasi
{
    private readonly List<string> _terkirim = new();

    public IReadOnlyList<string> Terkirim => _terkirim.AsReadOnly();

    public void Kirim(string pesan)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(pesan);
        if (pesan.Length > 40)
            pesan = pesan[0..40];
        _terkirim.Add($"SMS: {pesan}");
    }
}
