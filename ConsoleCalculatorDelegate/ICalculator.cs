namespace ConsoleCalculatorDelegate;

public interface ICalculator
{
    public void Start(EventHandler<EventArgs> getResultDelegate);
    
    public void Sum(double value);
    public void Substract(double value);
    public void Multiply(double value);
    public void Divide(double value);

    public event EventHandler<EventArgs> GetResult;
    public void CancelLastResult();
}