
public interface IPaymentProcessor
{
    bool ProcessPayment(decimal amount);
    bool RefundPayment(decimal amount, string transactionId);
}

public interface IPaymentValidator
{
    bool ValidatePayment(decimal amount, string paymentDetails);
}

public class PayPalProcessor : IPaymentProcessor
{
    public bool ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Processing PayPal payment for {amount:C}");
        return true;
    }

    public bool RefundPayment(decimal amount, string transactionId)
    {
        Console.WriteLine($"Refunding {amount:C} via PayPal for transaction {transactionId}");
        return true;
    }
}

public class CreditCardProcessor : IPaymentProcessor, IPaymentValidator
{
    public bool ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Processing credit card payment for {amount:C}");
        return true;
    }

    public bool RefundPayment(decimal amount, string transactionId)
    {
        Console.WriteLine($"Refunding {amount:C} to credit card for transaction {transactionId}");
        return true;
    }

    public bool ValidatePayment(decimal amount, string paymentDetails)
    {
        if (string.IsNullOrEmpty(paymentDetails)) return false;
        if (amount <= 0) return false;

        Console.WriteLine($"Validating credit card payment: {paymentDetails}");
        return paymentDetails.Length == 16 && paymentDetails.All(char.IsDigit);
    }
}

public class CryptoCurrencyProcessor : IPaymentProcessor
{
    public bool ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Processing cryptocurrency payment for {amount:C}");
        return true;
    }

    public bool RefundPayment(decimal amount, string transactionId)
    {
        Console.WriteLine($"Refunding {amount:C} in cryptocurrency for transaction {transactionId}");
        return true;
    }
}

public class PaymentService
{
    private readonly IPaymentProcessor _paymentProcessor;
    private readonly IPaymentValidator _paymentValidator;

    public PaymentService(IPaymentProcessor paymentProcessor, IPaymentValidator paymentValidator = null)
    {
        _paymentProcessor = paymentProcessor;
        _paymentValidator = paymentValidator;
    }

    public bool MakePayment(decimal amount, string paymentDetails)
    {
        if (_paymentValidator != null && !_paymentValidator.ValidatePayment(amount, paymentDetails))
        {
            Console.WriteLine("Payment validation failed");
            return false;
        }

        return _paymentProcessor.ProcessPayment(amount);
    }

    public bool RefundPayment(decimal amount, string transactionId)
    {
        return _paymentProcessor.RefundPayment(amount, transactionId);
    }
}
