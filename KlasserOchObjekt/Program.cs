using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;

namespace Bank
{
public class Customer{
    String firstName;
    String lastName;
    String id;

    public String GetFirstName() {
        return firstName;
    }

    public Customer(String firstName, String lastName, String id) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.id = id;
    }
}

public class Transaction {
    public string sentFrom;
    public string sentTo;
    public double amount;
}

public class Account {
    public String ownerID;
    public double balance;
    public Transaction latestTransaction;
}

public class Bank {
    public List<Customer> customers;
    public List<Account> accounts;

    public Bank(){
    customers = new List<Customer>();
    accounts = new List<Account>();
    }

}

static class Program {
    static void Main() {
        Bank bank = new Bank();
        string firstName;
        string lastName;
        string id;
        Console.WriteLine("Hi, Welcome to the bank. Please choose what you want to do by pressing a number and then pressing enter.");
        Console.WriteLine("(1) Sign up a new customer");
        Console.WriteLine("(2) Sign up a new account");
        Console.WriteLine("(3) Connect an account and a customer");

        switch (Console.ReadLine())
        {
            case "1":
                    Console.Clear();
                    Console.Write("Please enter the customers first name: ");
                    firstName = Console.ReadLine();

                    Console.Write("Please enter the customers last name: ");
                    lastName = Console.ReadLine();

                    Console.Write("Please enter the customers 12 digit social security number: ");
                    id = Console.ReadLine();

                    bank.customers.Add(new Customer(firstName, lastName, id));
                break;
            case "2":
                break;
            default:
                break;
        }
    }
}
}



