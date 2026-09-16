namespace jaburrow.Pages.Code;

public partial class PalindromicNumbers
{
    public record PalindromeRow(int Number, int Difference);

    public List<PalindromeRow> Palindromes { get; set; } = [];

    public string Output { get; set; } = string.Empty;

    protected override void OnInitialized()
    {
        GenerateOutput();
        ParseOutputToTable();
    }

    private void GenerateOutput()
    {
        var previousNumber = 0;

        for (var i = 10; i <= 1000; i++)
        {
            var numberAsString = i.ToString();
            var reversedNumberString = ReverseString(numberAsString);

            if (numberAsString == reversedNumberString)
            {
                var diff = previousNumber == 0 ? 0 : i - previousNumber;

                Output += $"{i} {diff}\n";
                previousNumber = i;
            }
        }
    }

    private void ParseOutputToTable()
    {
        if (string.IsNullOrWhiteSpace(Output))
            return;

        var lines = Output.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length >= 2 &&
                int.TryParse(parts[0], out int number) &&
                int.TryParse(parts[1], out int diff))
            {
                Palindromes.Add(new PalindromeRow(number, diff));
            }
        }
    }

    private string ReverseString(string str)
    {
        char[] chars = str.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }
}
