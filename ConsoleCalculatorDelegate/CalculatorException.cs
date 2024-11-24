namespace ConsoleCalculatorDelegate;

public class CalculatorException : Exception
{
    private Stack<CalculatorActionsHistory> ActionsHistory { get; }

    private CalculatorException(string message, Stack<CalculatorActionsHistory> actionsHistory) : base(message)
    {
        ActionsHistory = actionsHistory;
    }

    private CalculatorException(string message, Stack<CalculatorActionsHistory> actionsHistory,
        Exception innerException) : base(message, innerException)
    {
        ActionsHistory = actionsHistory;
    }

    public override string ToString()
    {
        return Message + ". " + string.Join(" - ",
            ActionsHistory.Select(x => $"Action: {x.ActionType}, number: {x.CalculatorArg}."));
    }

    internal class CalculatorDivideByZeroException : CalculatorException
    {
        public CalculatorDivideByZeroException(string message, Stack<CalculatorActionsHistory> actionsHistory) : base(
            message, actionsHistory)
        {
        }

        public CalculatorDivideByZeroException(string message, Stack<CalculatorActionsHistory> actionsHistory,
            Exception innerException) : base(message, actionsHistory, innerException)
        {
        }
    }

    internal class CalculatorStackOverflowException : CalculatorException
    {
        public CalculatorStackOverflowException(string message, Stack<CalculatorActionsHistory> actionsHistory) : base(
            message, actionsHistory)
        {
        }

        public CalculatorStackOverflowException(string message, Stack<CalculatorActionsHistory> actionsHistory,
            Exception innerException) : base(message, actionsHistory, innerException)
        {
        }
    }
}