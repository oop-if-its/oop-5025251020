namespace Pertemuan08;

public static class ParserAngka
{
    public static int Parse(string teks)
    {
        try
        {
            ArgumentNullException.ThrowIfNullOrEmpty(teks);
            return int.Parse(teks);
        }
        catch (FormatException ex)
        {
            throw new DataTidakValidException($"'{teks}' bukan angka yang valid.", ex);
        }
        catch (OverflowException ex)
        {
            throw new DataTidakValidException($"'{teks}' bukan angka yang valid.", ex);
        }
    }
}
