namespace jaburrow.Pages.Code;

public partial class DifferenceBetweenTwoSquares
{
    public class DifferenceValues
    {
        public int FirstNumber { get; set; }

        public int SecondNumber { get; set; }

        public int Result { get; set; }
    }

    private List<int> Numbers { get; } =
        [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25];

    private readonly DifferenceValues CurrentValues = new();

    protected override void OnInitialized()
    {
        CurrentValues.FirstNumber = 1;
        CurrentValues.SecondNumber = 1;
        CalculateDifference();
    }

    private void CalculateDifference()
    {
        // Cheat and use the greater of the two numbers
        var firstNumber = Math.Max(CurrentValues.FirstNumber, CurrentValues.SecondNumber);
        var secondNumber = Math.Min(CurrentValues.FirstNumber, CurrentValues.SecondNumber);
        CurrentValues.Result = (firstNumber + secondNumber) * (firstNumber - secondNumber);
    }
}
