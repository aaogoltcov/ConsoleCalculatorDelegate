namespace ConsoleCalculatorDelegate;

public class Calculator : ICalculator
{
    private readonly Stack<double> _resultStack = new();

    public double Result;

    public void Start(EventHandler<EventArgs> getResultDelegate)
    {
        GetResult += getResultDelegate;
        
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("For exit please input 'exit' in action");
            Console.WriteLine("Enter a number: ");
            var number = Console.ReadLine();
            Console.WriteLine("Enter action: ");
            var action = Console.ReadLine();
            
            if (action is "exit")
            {
                Console.WriteLine("Exit from calculator");
                GetResult -= getResultDelegate;
                break;
            }
            
            if (number == null || action == null || number.Equals("") || action.Equals(""))
            {
                Console.WriteLine("Invalid input");
                Console.WriteLine("Exit from calculator");
                GetResult -= getResultDelegate;
                break;
            }
            
            var parsedNumber = GetDoubleFromNumber(number);
            var parsedAction = GetActionType(action);

            switch (parsedAction)
            {
                case CalculatorActionType.Sum:
                    Sum(parsedNumber);
                    break;
                case CalculatorActionType.Subtract:
                    Substract(parsedNumber);
                    break;
                case CalculatorActionType.Multiply:
                    Multiply(parsedNumber);
                    break;
                case CalculatorActionType.Divide:
                    Divide(parsedNumber);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private double GetDoubleFromNumber(string? number)
    {
        return double.Parse(number ?? throw new ArgumentNullException(nameof(number)));
    }

    private CalculatorActionType GetActionType(string action)
    {
        switch (action)
        {
            case "+":
                return CalculatorActionType.Sum;
            case "-":
                return CalculatorActionType.Subtract;
            case "*":
                return CalculatorActionType.Multiply;
            case "/":
                return CalculatorActionType.Divide;
            default:
                throw new Exception("Invalid action");
        }
    }

    public void Sum(double value)
    {
        _resultStack.Push(Result);
        Result += value;
        RaiseEvent();
    }

    public void Substract(double value)
    {
        _resultStack.Push(Result);
        Result -= value;
        RaiseEvent();
    }

    public void Multiply(double value)
    {
        _resultStack.Push(Result);
        Result *= value;
        RaiseEvent();
    }

    public void Divide(double value)
    {
        _resultStack.Push(Result);
        Result /= value;
        RaiseEvent();
    }

    public event EventHandler<EventArgs>? GetResult;

    private void RaiseEvent()
    {
        GetResult?.Invoke(this, EventArgs.Empty);
    }
    
    public void CancelLastResult()
    {
        Result = _resultStack.Pop();
        RaiseEvent();
    }
}