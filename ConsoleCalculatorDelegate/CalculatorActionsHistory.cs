namespace ConsoleCalculatorDelegate;

public class CalculatorActionsHistory(CalculatorActionType actionType, double calculatorArg)
{
    public CalculatorActionType ActionType { get; private set; } = actionType;
    public double CalculatorArg { get; private set; } = calculatorArg;
}