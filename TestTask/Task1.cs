using System.Text;

namespace TestTask;

public class Task1
{
    public static string Compress(string input)
    {
        if (string.IsNullOrEmpty(input))
            return "";

        var result = new StringBuilder();
        int count = 1;

        for (int i = 1; i < input.Length; i++)
        {
            if (input[i] == input[i - 1])
            {
                count++;
            }
            else
            {
                result.Append(input[i - 1]);
                if (count > 1)
                    result.Append(count);
                count = 1;
            }
        }

        result.Append(input[^1]);
        if (count > 1)
            result.Append(count);

        return result.ToString();
    }

    public static string Decompress(string input)
    {
        if (string.IsNullOrEmpty(input))
            return "";

        var result = new StringBuilder();
        for (int i = 0; i < input.Length;)
        {
            char ch = input[i];
            i++;
            var countStr = new StringBuilder();
            while (i < input.Length && char.IsDigit(input[i]))
            {
                countStr.Append(input[i]);
                i++;
            }

            int count = countStr.Length > 0 ? int.Parse(countStr.ToString()) : 1;
            result.Append(new string(ch, count));
        }

        return result.ToString();
    }
}