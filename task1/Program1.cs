var payPalProcessor = new PayPalProcessor();
var payPalService = new PaymentService(payPalProcessor); // Без валидатора
payPalService.MakePayment(200m, "user@example.com");

// Пример для криптовалют
var cryptoProcessor = new CryptoCurrencyProcessor();
var cryptoService = new PaymentService(cryptoProcessor);
cryptoService.MakePayment(0.5m, "BTC_WALLET_ADDRESS");