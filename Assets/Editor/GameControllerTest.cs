using NUnit.Framework;
using Controller;
using Model;

public class GameControllerTest
{
    private GameController gameController = NSubstitute.Substitute.For<GameController>();

    [Test]
    public void CalculateHandWithoutA()
    {
        Hand hand = new Hand();
        hand.AddCard(new Card(Suit.Clubs, 4));
        hand.AddCard(new Card(Suit.Clubs, 5));
        hand.AddCard(new Card(Suit.Diamonds, 12));
        Assert.That(19 == GameController.CalculateHand(hand));
    }

    [Test]
    public void CalculateHandWithOneAAbove21()
    {
        Hand hand = new Hand();
        hand.AddCard(new Card(Suit.Clubs, 1));
        hand.AddCard(new Card(Suit.Clubs, 7));
        hand.AddCard(new Card(Suit.Clubs, 8));
        Assert.That(16 == GameController.CalculateHand(hand));
    }

    [Test]
    public void CalculateHandWithTwoABelow21()
    {
        Hand hand = new Hand();
        hand.AddCard(new Card(Suit.Clubs, 1));
        hand.AddCard(new Card(Suit.Hearts, 1));
        Assert.That(12 == GameController.CalculateHand(hand));
    }

    [Test]
    public void IsBlackjack()
    {
        Hand hand = new Hand();
        hand.AddCard(new Card(Suit.Clubs, 1));
        hand.AddCard(new Card(Suit.Diamonds, 12));
        Assert.IsTrue(gameController.IsBlackjack(hand));
    }

    [Test]
    public void IsNotBlackjack()
    {
        Hand hand = new Hand();
        hand.AddCard(new Card(Suit.Clubs, 1));
        hand.AddCard(new Card(Suit.Diamonds, 4));
        hand.AddCard(new Card(Suit.Hearts, 6));
        Assert.IsFalse(gameController.IsBlackjack(hand));
    }
}
