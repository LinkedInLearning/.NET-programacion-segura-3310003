namespace Payments.Lib;

public class PaymentsService
{
    public void Pay(Money money)
    {
        //... procesamos el pago
    }
}

public record Money
{
    private static readonly HashSet<string> _currencies = new() { "MXN", "EUR", "USD" };
    public decimal Amount { get; init; }
    public string Currency { get; init; }

    public Money(decimal amount, string currency)
    {
        ValidateAmount(amount);
        ValidateCurrency(currency);

        Amount = amount;
        Currency = currency;
    }

    private static void ValidateAmount(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("El monto no es válido");
        }
    }

    private static void ValidateCurrency(string currency)
    {
        if (!_currencies.Contains(currency))
        {
            throw new ArgumentException("La moneda no es válida");
        }    
    }
}