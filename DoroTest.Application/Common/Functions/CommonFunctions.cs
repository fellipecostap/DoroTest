using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace DoroTest.Application.Common.Functions;
public class CommonFunctions
{
    public static string CreateMD5Hash(string input)
    {
        MD5 md5 = MD5.Create();
        byte[] inputBytes = Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = md5.ComputeHash(inputBytes);

        StringBuilder sb = new();
        for (int i = 0; i < hashBytes.Length; i++)
        {
            sb.Append(hashBytes[i].ToString("X2"));
        }
        return sb.ToString();
    }
    public static DateTime ConvertStringToDateTime(string date)
    {
        var culture = new CultureInfo("en-US");
        var ResponseDate = Convert.ToDateTime(date, culture);

        return ResponseDate;
    }
    public static string CleanStringOfNonDigits(string s)
    {
        Regex rxNonDigits = new(@"[^\d]+");
        if (string.IsNullOrEmpty(s)) return s;
        string cleaned = rxNonDigits.Replace(s, "");
        return cleaned;

    }

    public static bool VerifyAllCharEqual(ref Span<int> input)
    {
        for (var i = 1; i < 11; i++)
        {
            if (input[i] != input[0])
            {
                return false;
            }
        }

        return true;
    }
}
