// See https://aka.ms/new-console-template for more information
using Payments.Lib;

Console.WriteLine("Hello, World!");

var paymentsService = new PaymentsService();
paymentsService.Pay(new Money(100, "USD"));

var m1 = new Money(100, "USD");
var m2 = new Money(100, "USD");

Console.WriteLine(m1==m2);