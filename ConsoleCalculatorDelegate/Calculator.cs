namespace ConsoleCalculatorDelegate;

public class Calculator : ICalculator
{
    private readonly Stack<double> _resultStack = new();

    private readonly Stack<CalculatorActionsHistory> _history = new();

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

            var parsedAction = GetActionType(action);

            if (TryParseIntFromNumber(number, out var intNumber))
            {
                InvokeActionWithIntValue(intNumber, parsedAction);
            }
            else if (TryParseDoubleFromNumber(number, out var doubleNumber))
            {
                InvokeActionWithDoubleValue(doubleNumber, parsedAction);
            }
        }
    }

    private void InvokeActionWithDoubleValue(double value, CalculatorActionType action)
    {
        try
        {
            switch (action)
            {
                case CalculatorActionType.Sum:
                    Sum(value);
                    break;
                case CalculatorActionType.Subtract:
                    Substract(value);
                    break;
                case CalculatorActionType.Multiply:
                    Multiply(value);
                    break;
                case CalculatorActionType.Divide:
                    Divide(value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        catch (CalculatorException exception)
        {
            Console.WriteLine(exception);
        }
    }

    private void InvokeActionWithIntValue(int value, CalculatorActionType action)
    {
        try
        {
            switch (action)
            {
                case CalculatorActionType.Sum:
                    Sum(value);
                    break;
                case CalculatorActionType.Subtract:
                    Substract(value);
                    break;
                case CalculatorActionType.Multiply:
                    Multiply(value);
                    break;
                case CalculatorActionType.Divide:
                    Divide(value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        catch (CalculatorException exception)
        {
            Console.WriteLine(exception);
        }
    }

    private bool TryParseDoubleFromNumber(string? number, out double value)
    {
        if (double.TryParse(number, out value))
        {
            return true;
        }
        else
        {
            return false;
        }

        ;
    }

    private bool TryParseIntFromNumber(string? number, out int value)
    {
        if (int.TryParse(number, out value))
        {
            return true;
        }
        else
        {
            return false;
        }

        ;
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
        if (Result + value > double.MaxValue)
        {
            _history.Push(new CalculatorActionsHistory(CalculatorActionType.Divide, value));
            throw new CalculatorException.CalculatorStackOverflowException("Stack overflow", _history);
        }

        _resultStack.Push(Result);
        Result += value;
        RaiseEvent();
    }

    public void Substract(double value)
    {
        if (Result - value < double.MinValue)
        {
            _history.Push(new CalculatorActionsHistory(CalculatorActionType.Subtract, value));
            throw new CalculatorException.CalculatorStackOverflowException("Stack overflow", _history);
        }

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
        if (value == 0)
        {
            _history.Push(new CalculatorActionsHistory(CalculatorActionType.Divide, value));
            throw new CalculatorException.CalculatorDivideByZeroException("Cannot divide by zero", _history);
        }

        if (Result / value < double.MinValue)
        {
            _history.Push(new CalculatorActionsHistory(CalculatorActionType.Divide, value));
            throw new CalculatorException.CalculatorStackOverflowException("Stack overflow", _history);
        }

        _resultStack.Push(Result);
        Result /= value;
        RaiseEvent();
    }

    public void Sum(int value)
    {
        if (Result + value > int.MaxValue)
        {
            _history.Push(new CalculatorActionsHistory(CalculatorActionType.Divide, value));
            throw new CalculatorException.CalculatorStackOverflowException("Stack overflow", _history);
        }

        _resultStack.Push(Result);
        Result += value;
        RaiseEvent();
    }

    public void Substract(int value)
    {
        if (Result - value < int.MinValue)
        {
            _history.Push(new CalculatorActionsHistory(CalculatorActionType.Subtract, value));
            throw new CalculatorException.CalculatorStackOverflowException("Stack overflow", _history);
        }

        _resultStack.Push(Result);
        Result -= value;
        RaiseEvent();
    }

    public void Multiply(int value)
    {
        _resultStack.Push(Result);
        Result *= value;
        RaiseEvent();
    }

    public void Divide(int value)
    {
        if (value == 0)
        {
            _history.Push(new CalculatorActionsHistory(CalculatorActionType.Divide, value));
            throw new CalculatorException.CalculatorDivideByZeroException("Cannot divide by zero", _history);
        }

        if (Result / value < int.MinValue)
        {
            _history.Push(new CalculatorActionsHistory(CalculatorActionType.Divide, value));
            throw new CalculatorException.CalculatorStackOverflowException("Stack overflow", _history);
        }

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