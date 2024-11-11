using ConsoleCalculatorDelegate;

ICalculator calculator = new Calculator();
calculator.Start((sender, _) => Console.WriteLine("Result: " +((Calculator)sender).Result));