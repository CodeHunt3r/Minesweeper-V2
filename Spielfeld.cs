public class Spielfeld
{
    public int Wert { get; set; }
    public bool istMine { get; set; }
    public bool istGeoeffnet { get; set; }
    public bool istMarkiert { get; set; }

    public Spielfeld()
    {
        Wert = 0;
        istMine = false;
        istGeoeffnet = false;
        istMarkiert = false;
    }

    public void SetzeMine()
    {
        istMine = true;
        Wert = -1;
    }

    public bool HatMine()
    {
        return istMine;
    }

    public void Oeffne()
    {
        istGeoeffnet = true;
    }

    public bool IstOffen()
    {
        return istGeoeffnet;
    }

    public void Markiere()
    {
        istMarkiert = !istMarkiert;
    }

    public bool IstMarkiert()
    {
        return istMarkiert;
    }
}
