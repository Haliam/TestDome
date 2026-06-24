using TestDome.Library;

namespace TestDome.Tests;

public class AccountTests
{
    private readonly double epsilon = 1e-6;

    // 1. Deposit and Withdraw DO NOT accept negative numbers
    [Fact]
    public void Deposit_DoNotAccept_NegativeNumbers()
    {
        // Arrange
        var account = new AccountTest(100);

        // Act
        bool depositResult = account.Deposit(-50);

        //Assert
        Assert.False(depositResult);
        Assert.Equal(0, account.Balance, epsilon);
    }

    public void Withdraw_DoNotAccept_NegativeNumbers()
    {
        // Arrange
        var account = new AccountTest(100);

        // Act
        bool withdrawResult = account.Withdraw(-30);

        //Assert
        Assert.False(withdrawResult);
        Assert.Equal(0, account.Balance, epsilon);
    }

    // 2. Account cannot exceed overdraft limit
    [Fact]
    public void Account_CannotOverstep_OverdraftLimit()
    {
        // Arrange
        var account = new AccountTest(50); // overdraft = -50

        // Act
        bool result = account.Withdraw(60); // would go to -60

        // Assert
        Assert.False(result);
        Assert.Equal(0, account.Balance, epsilon);
    }

    // 3. Deposit and Withdraw modify balance correctly
    [Fact]
    public void DepositAndWithdraw_ModifyBalance_Correctly()
    {
        // Arrage
        var account = new AccountTest(100);

        // Act
        account.Deposit(200);   // balance = 200
        account.Withdraw(50);   // balance = 150

        // Assert
        Assert.Equal(150, account.Balance, epsilon);
    }

    // 4. Deposit and Withdraw return correct results
    [Fact]
    public void DepositAndWithdrawReturnCorrectResults()
    {
        // Arrange
        var account = new AccountTest(100);

        // Act
        bool depositResult = account.Deposit(100);    // valid
        bool withdrawResult = account.Withdraw(50);   // valid
        bool invalidWithdraw = account.Withdraw(500); // invalid

        // Assert
        Assert.True(depositResult);
        Assert.True(withdrawResult);
        Assert.False(invalidWithdraw);
    }
}

