namespace Pertemuan08;

public static class Jejak
{
    private static readonly List<string> _isi = new();

    public static IReadOnlyList<string> Isi => _isi.AsReadOnly();

    public static string Catat(string pesan)
    {
        _isi.Add(pesan);
        return pesan;
    }

    public static void Reset() => _isi.Clear();
}

public class Tingkat1
{
    private readonly string _f1 = Jejak.Catat("field Tingkat1");

    public Tingkat1()
    {
        Jejak.Catat("ctor Tingkat1");
    }
}

public class Tingkat2 : Tingkat1
{
    private readonly string _f2 = Jejak.Catat("field Tingkat2");

    public Tingkat2()
    {
        Jejak.Catat("ctor Tingkat2");
    }
}

public class Tingkat3 : Tingkat2
{
    private readonly string _f3 = Jejak.Catat("field Tingkat3");

    public Tingkat3()
    {
        Jejak.Catat("ctor Tingkat3");
    }

    public Tingkat3(string tambahan) : this()
    {
        Jejak.Catat($"ctor Tingkat3({tambahan})");
    }
}
