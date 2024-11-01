using System.Globalization;

public static class NumberFormater
{
    public static string FormatNumber(int number)
    {
       
        if(number >= 1000) //thousands
        {
            return (number / 1000f).ToString("0.##a", CultureInfo.InvariantCulture);
        }
        if (number >= 1000000) //million
        {
            return (number / 1000000f).ToString("0.##b", CultureInfo.InvariantCulture);
        }
        if (number >= 1000000000) //billion
        {
            return (number / 1000000000f).ToString("0.##c", CultureInfo.InvariantCulture);
        }
        return number.ToString();
    }
}
