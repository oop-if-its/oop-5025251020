using System.Runtime.CompilerServices;

namespace Pertemuan08;

public static class Sumber
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void Gagal() => throw new InvalidOperationException("boom");
}

public class PencatatError
{
    private readonly List<string> _log = new();

    public IReadOnlyList<string> Log => _log.AsReadOnly();

    public void JalankanDenganLog(Action aksi)
    {
        try
        {
            aksi();
        }
        catch (Exception ex)
        {
            _log.Add($"ERROR: {ex.Message}");
            throw;
        }
    }
}
