// See https://aka.ms/new-console-template for more information
using System;
namespace LoanApp;
class Programm
{
    static void Main()
    {
        Console.WriteLine("Loan Application System");

        try
        {
            LoanApplication application = GetLoanApplicationFromUser();
            LoanApplicationResult result = application.Evaluate();

            if (result.Accepted)
            {
                Console.WriteLine("Loan application accepted!");
            }
            else
            {
                Console.WriteLine($"Loan application rejected. Reason: {result.RejectionReason}");

                if (result.SuggestedAmount.HasValue)
                {
                    Console.WriteLine($"suggested loan amount: {result.SuggestedAmount:C}");
                    Console.Write("Accept the suggested amount? (yes/no): ");
                    string acceptSuggestion = Console.ReadLine().ToLower();
                    if (acceptSuggestion == "yes")
                    {
                        Console.WriteLine($"Loan application accepted with the suggested amount: {result.SuggestedAmount:C}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }


    static LoanApplication GetLoanApplicationFromUser()
    {
        Console.Write("Enter your annual income: ");
        double annualIncome = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter the loan amount you are requesting: ");
        double loanAmount = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter the loan duration (in years): ");
        int loanDuration = Convert.ToInt32(Console.ReadLine());

        return new LoanApplication(annualIncome, loanAmount, loanDuration);
    }
}

class LoanApplication
{
    private double AnnualIncome { get; }
    private double LoanAmount { get; }
    private int LoanDuration { get; }

    public LoanApplication(double annualIncome, double loanAmount, int loanDuration)
    {
        AnnualIncome = annualIncome;
        LoanAmount = loanAmount;
        LoanDuration = loanDuration;
    }

    public LoanApplicationResult Evaluate()
{
    double minIncome = 200000;
    double minLoanAmount = 100000;
    double maxLoanAmount = 1000000;
    int minLoanDuration = 1;
    int maxLoanDuration = 10;

    double suggestedAmount = 0; // Initialize suggestedAmount

    if (AnnualIncome < minIncome)
    {
        return new LoanApplicationResult(false, "Insufficient annual income");
    }

    if (LoanAmount < minLoanAmount)
    {
        suggestedAmount = minLoanAmount;
        return new LoanApplicationResult(false, "Loan amount is below the minimum allowed", suggestedAmount);
    }

    if (LoanAmount > maxLoanAmount)
    {
        suggestedAmount = maxLoanAmount;
        return new LoanApplicationResult(false, "Loan amount exceeds the maximum allowed", suggestedAmount);
    }

    if (LoanDuration < minLoanDuration)
    {
        return new LoanApplicationResult(false, "Loan duration is too short");
    }

    if (LoanDuration > maxLoanDuration)
    {
        
        return new LoanApplicationResult(false, "Loan duration is too long");
    }

    suggestedAmount = LoanAmount * 0.8;
    return new LoanApplicationResult(true, "Criteria met", suggestedAmount);
}
}

class LoanApplicationResult
{
    public bool Accepted { get; }
    public string RejectionReason { get; }
    public double? SuggestedAmount { get; }

    public LoanApplicationResult(bool accepted, string rejectionReason, double? suggestedAmount = null)
    {
        Accepted = accepted;
        RejectionReason = rejectionReason;
        SuggestedAmount = suggestedAmount;
    }
}

