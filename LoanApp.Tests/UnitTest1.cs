using NUnit.Framework;
using LoanApp;
namespace YourProject.Tests;


    [TestFixture]
    public class LoanApplicationTests
    {
        [Test]
        public void TestLoanApplicationAccepted()
        {
            // Arrange
            var loanApp = new LoanApplication(250000, 200000, 5);

            // Act
            var result = loanApp.Evaluate();

            // Assert
            Assert.IsTrue(result.Accepted);
            Assert.AreEqual("Criteria met", result.RejectionReason);
            Assert.IsNull(result.SuggestedAmount);
        }

        [Test]
        public void TestLoanApplicationRejectedDueToIncome()
        {
            // Arrange
            var loanApp = new LoanApplication(150000, 200000, 5);

            // Act
            var result = loanApp.Evaluate();

            // Assert
            Assert.IsFalse(result.Accepted);
            Assert.AreEqual("Insufficient annual income", result.RejectionReason);
            Assert.IsNull(result.SuggestedAmount);
        }

    }
