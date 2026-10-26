namespace Proyek;

public class LayananPelacakan
{
    private readonly IBisaDilacak _objek;

    public LayananPelacakan(IBisaDilacak objek)
    {
        ArgumentNullException.ThrowIfNull(objek);
        _objek = objek;
    }

    public string DapatkanInformasiPelacakan()
    {
        return _objek.Lacak();
    }
}
