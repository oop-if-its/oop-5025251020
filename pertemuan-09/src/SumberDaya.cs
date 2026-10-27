namespace Pertemuan08;

public class SumberDaya : IDisposable
{
    private static int _dibersihkanDispose;
    private static int _dibersihkanFinalizer;

    public static int DibersihkanDispose => _dibersihkanDispose;
    public static int DibersihkanFinalizer => _dibersihkanFinalizer;

    private bool _sudahDibersihkan;

    private static void CatatDispose() => Interlocked.Increment(ref _dibersihkanDispose);
    private static void CatatFinalizer() => Interlocked.Increment(ref _dibersihkanFinalizer);

    public void Gunakan()
    {
        // (pura-pura memakai sumber daya)
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_sudahDibersihkan) return;

        if (disposing)
            CatatDispose();
        else
            CatatFinalizer();

        _sudahDibersihkan = true;
    }

    ~SumberDaya()
    {
        Dispose(false);
    }
}
