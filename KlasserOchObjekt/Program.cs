using Microsoft.VisualBasic.CompilerServices;
using System.Dynamic;
using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;

namespace Bank
{
public class Customer{
    String firstName;
    String lastName;
    String id;
    int accountNumber;


    public Customer(String firstName, String lastName, String id, int accountNumber) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.id = id;
        this.accountNumber = accountNumber;
    }
        public void SetAccountNumber(int accountNumber){
        this.accountNumber = accountNumber;
    }

    public string GetFirstName(){
        return firstName;
    }
    public string GetLastName(){
        return lastName;
    }

    public void PrintCustomer(){
        Console.WriteLine("Customer name: " + firstName + lastName);
        Console.WriteLine("The customers account number is " + accountNumber);
    }
}

public class Account {
    string ownerID;
    double balance;
    int latestTransaction;

    public string GetOwnerID(){
        return ownerID;
    }

    public double GetBalance(){
        return balance;
    }
    public void ShowBalance(){
        Console.Write(balance);
    }

    public void AddToBalance(double addBalance){
        balance += addBalance;
    }

    public Account(String ownerID, double balance, int latestTransaction) {
        this.ownerID = ownerID;
        this.balance = balance;
        this.latestTransaction = latestTransaction;
    }
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
        Console.WriteLine("Hi, Welcome to the bank. ");
        Bank bank = new Bank();
        MainMenu(ref bank);
}

static void MainMenu(ref Bank bank){
        double totalBalance = 0;

        for(int a = 0; a < bank.accounts.Count; a++){
            totalBalance += bank.accounts[a].GetBalance();
        }

        Console.Clear();
        Console.WriteLine("Currently signed up customers: " + bank.customers.Count + "\nCurrent revenue of the bank: " + totalBalance + "\n");
        Console.WriteLine("Please choose what you want to do by pressing a number and then pressing enter.");
        Console.WriteLine("(1) Sign up a new customer");
        Console.WriteLine("(2) Sign up a new account");
        Console.WriteLine("(3) Connect an account and a customer");
        Console.WriteLine("(4) Show customer and connected account");
        Console.WriteLine("(5) Change the balance of an account");
        Console.WriteLine("(6) Close the program");

        switch (Console.ReadLine())
        {
            case "1":
                    SignCustomer(ref bank);
                break;
            case "2":
                    SignAccount(ref bank);
                break;
            case "3":
                    ConnectAccAndCustomer(ref bank);
                break;
            case "4":
                    ShowCustomer(ref bank);
                break;
            case "5":
                    ChangeAccBalance(ref bank);
                break;
            case "6":
                break;
            default:
                break;
        }

}

static void SignCustomer(ref Bank bank){
    string firstName;
    string lastName;
    string id;
    bool isOnlyNumbers = true;
    int accountNumber = -1;
    while (true){
        Console.Clear();
        Console.Write("Please enter the customers first name: ");
        firstName = Console.ReadLine();
        if (firstName.Length != 0)
        {
            break;
        }
        else{
            Console.WriteLine("You need to type in a name with at least 1 character. Press enter to try again");
            Console.ReadLine();
        }
    }
    while (true){
        Console.Clear();
        Console.Write("Please enter the customers last name: ");
        lastName = Console.ReadLine();
        if (lastName.Length != 0)
        {
            break;
        }
        else{
            Console.WriteLine("You need to type in a name with at least 1 character. Press enter to try again");
            Console.ReadLine();
        }
    }
    while(true){
        Console.Clear();
        Console.Write("Please enter the customers 12 digit social security number: ");
        id = Console.ReadLine();
        isOnlyNumbers = int.TryParse(id, out _);
        if(id.Length != 12 && isOnlyNumbers){
            Console.Clear();
            Console.WriteLine("Hey! You need to type in a social security number with 12 digits! Press enter to try again");
        }
        else{
            break;
        }
    }
    accountNumber++;
    bank.customers.Add(new Customer(firstName, lastName, id, accountNumber));
    MainMenu(ref bank);
}

static void SignAccount(ref Bank bank){
    string ownerID;
    bool isOnlyNumbers;
   while(true){
        Console.Clear();
        Console.Write("To create an account, please enter the customers 12 digit social security number: ");
        ownerID = Console.ReadLine();
        isOnlyNumbers = int.TryParse(ownerID, out _);
        if(ownerID.Length != 12 && isOnlyNumbers){
            Console.Clear();
            Console.WriteLine("Hey! You need to type in a social security number with 12 digits! Press enter to try again");
        }
        else{
            break;
        }
    }
    bank.accounts.Add(new Account(ownerID, 0.00, 0));
    MainMenu(ref bank);
}

static void ConnectAccAndCustomer(ref Bank bank){
    string currentID;
    Console.Clear();
    Console.Write("To connect an account to a customer, enter the customers 12 digit social security number: ");
    currentID = Console.ReadLine();

    for(int a = 0; a < bank.accounts.Count; a++){
        if(bank.accounts[a].GetOwnerID() == currentID){
            bank.customers[a].SetAccountNumber(a);
        }
    }
    MainMenu(ref bank);
}

static void ShowCustomer(ref Bank bank){
    string firstName;
    string lastName;
    Console.Clear();
    Console.WriteLine("Enter the first and last name of the customer which account you want to check\n");

    Console.Write("Please enter the customers first name: ");
    firstName = Console.ReadLine();

    Console.Write("Please enter the customers last name: ");
    lastName = Console.ReadLine();
    for(int b = 0; b < bank.customers.Count; b++){
        if(firstName == bank.customers[b].GetFirstName() && lastName == bank.customers[b].GetLastName()){
        bank.customers[b].PrintCustomer();
        }
        else{
        }
    }
    Console.ReadLine();
    MainMenu(ref bank);
}

static void ChangeAccBalance(ref Bank bank){
    string ownerID;
    Console.Clear();
    Console.Write("Enter the owners social security number to change the balance of that customers account: ");
    ownerID = Console.ReadLine();

    for(int b = 0; b < bank.customers.Count; b++){
        if(ownerID == bank.accounts[b].GetOwnerID()){
            Console.Write("Type the amount you want to add to the accont balance: ");
            double addBalance = double.Parse(Console.ReadLine());
            bank.accounts[b].AddToBalance(addBalance);
        }
    }
    MainMenu(ref bank);
}


}
}



