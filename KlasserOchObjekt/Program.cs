using System.Transactions;
using Microsoft.VisualBasic.CompilerServices;
using System.Dynamic;
using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;

namespace Bank
{
public class Customer{ //Creates the Customer class. "Customer" containes a first- and last name, the customers id(social security number) and an account number
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

    public void PrintCustomer(){ //Show the conents of an instance of "customer"
        Console.WriteLine("Customer name: " + firstName + " " +  lastName);
        Console.WriteLine("The customers account number is " + accountNumber);
    }
}

public class Account { // Creates the Account class. An account contains an owner id (the customers social security number) and a balance.
    public string ownerID;
    public double balance;

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
}

public class SavingsAccount : Account{
    double interest = 1.03;

    public SavingsAccount(String ownerID, double balance){
        this.ownerID = ownerID;
        this.balance = balance;
    }
}
public class TransactionAccount : Account{
    int latestTransaction;
    public List<int> TransactionList;
    public TransactionAccount(String ownerID, double balance){
        this.ownerID = ownerID;
        this.balance = balance;
    }

    public void Transaction(int latestTransaction){
        this.latestTransaction = latestTransaction;
        TransactionList.Add(latestTransaction);
    }

    public void PrintTransactionHistory(){
        Console.WriteLine("Transactions:\n");
        for (int i = 0; i < TransactionList.Count; i++)
        {
            Console.WriteLine(i+1 + ". " + TransactionList[i]);
        }
    }
}

public class Bank { // creates the Bank class. A bank contains a list of customers and a list of accounts.
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
        Console.WriteLine("(2) Create a new account");
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
                    Console.WriteLine("Enter the first and last name of the owner of the account you want to check. Press enter to continue\n");

                    ShowCustomer(ref bank);
                break;
            case "5":
                    ChangeAccBalance(ref bank);
                break;
            case "6":
                break;
            default:
                MainMenu(ref bank);
                break;
        }

}

static void SignCustomer(ref Bank bank){
    string firstName;
    string lastName;
    string id;
    bool isOnlyNumbers;
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
        isOnlyNumbers = long.TryParse(id, out _);
        if(isOnlyNumbers && id.Length == 12){
            break;
        }
        else{
            Console.Clear();
            Console.WriteLine("Hey! You need to type in a social security number with 12 numbers! Press enter to try again");
            Console.ReadLine();
        }
    }
    accountNumber++;
    bank.customers.Add(new Customer(firstName, lastName, id, accountNumber));
    MainMenu(ref bank);
}

static void SignAccount(ref Bank bank){
    string ownerID;
    bool isOnlyNumbers;
    bool savingsAccount = false;
    Console.Clear();
    Console.WriteLine("What kind of account would you like to create? Press the correspondning number and then press enter");
    Console.WriteLine("\n (1) Savings account");
    Console.WriteLine("\n (2) Transaction account");
    switch (Console.ReadLine())
    {
        case "1":
            savingsAccount = true;
            break;
        case "2":
            savingsAccount = false;
            break;
        default:
            Console.Clear();
            Console.WriteLine("You need to choose one of the two account types by entering a number and then pressing enter. Press enter to attempt it again!");
            Console.ReadLine();
            SignAccount(ref bank);
            break;
    }
    while(true){
        Console.Clear();
        Console.Write("To create an account, please enter the customers 12 digit social security number: ");
        ownerID = Console.ReadLine();
        isOnlyNumbers = long.TryParse(ownerID, out _);
        if(isOnlyNumbers && ownerID.Length == 12){
            break;
        }
        else{
            Console.Clear();
            Console.WriteLine("Hey! You need to type in a social security number with 12 numbers! Press enter to try again");
            Console.ReadLine();
        }
    }
    if (savingsAccount)
    {
        bank.accounts.Add(new SavingsAccount(ownerID, 0.00));
    }
    else if(!savingsAccount){
        bank.accounts.Add(new TransactionAccount(ownerID, 0.00));
    }
    MainMenu(ref bank);
}

static void ConnectAccAndCustomer(ref Bank bank){
    string currentID;
    bool isOnlyNumbers;
    while(true){
    Console.Clear();
    Console.Write("To connect an account to a customer, enter the customers 12 digit social security number: ");
    currentID = Console.ReadLine();
    isOnlyNumbers = long.TryParse(currentID, out _);
    if(isOnlyNumbers && currentID.Length == 12){
        break;
    }
    else{
        Console.Clear();
        Console.WriteLine("Hey! You need to type in a social security number with 12 numbers! Press enter to try again");
        Console.ReadLine();
    }
    }
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
    bool isACustomer = false;
    Console.Clear();
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
    for(int b = 0; b < bank.customers.Count; b++){
        if(firstName == bank.customers[b].GetFirstName() && lastName == bank.customers[b].GetLastName()){
        bank.customers[b].PrintCustomer();
        isACustomer = true;
        }
    }
    if (!isACustomer)
    {
        Console.WriteLine("Hey! This customer name does not exist. Try again by pressing enter!");
        Console.ReadLine();
        ShowCustomer(ref bank);
    }
    Console.ReadLine();
    MainMenu(ref bank);
}

static void ChangeAccBalance(ref Bank bank){
    string ownerID;
    bool isOnlyNumbers;
    bool isACustomer = false;
    double addBalance;
    while(true){
        Console.Clear();
        Console.Write("Enter the owners social security number to change the balance of that customers account: ");
        ownerID = Console.ReadLine();
        isOnlyNumbers = long.TryParse(ownerID, out _);
        if(isOnlyNumbers && ownerID.Length == 12){
            break;
        }
        else{
            Console.Clear();
            Console.WriteLine("Hey! You need to type in a social security number with 12 numbers! Press enter to try again");
            Console.ReadLine();
        }
    }
    for(int b = 0; b < bank.customers.Count; b++){
        if(ownerID == bank.accounts[b].GetOwnerID()){
            while (true)
            {
                Console.Clear();
                try
                {
                    Console.Write("Type the amount you want to add to the accont balance: ");
                    addBalance = double.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Hey! A balance needs to be a number! Press enter to attempet that once more.");
                    Console.ReadLine();
                }

            }
            bank.accounts[b].AddToBalance(addBalance);
            isACustomer = true;
        }
    }

    if (!isACustomer)
    {
        Console.WriteLine("Sorry, the social security number entered does not match any customer in the database. Press enter to try again.");
        Console.ReadLine();
        ChangeAccBalance(ref bank);
    }
    MainMenu(ref bank);
}


}
}



