
using System;
using System.Collections.Generic;
using System.Text;



public class CardService
{
    // Dictionary to hold cards in-memory
    private readonly Dictionary<string, Card> cards = new Dictionary<string, Card>();

    // Generate a random 15-digit ID for the card
    private string GenerateId()
    {
        var rng = new Random();
        var idBuilder = new StringBuilder();
        
        // Append a random digit (from 0 to 9) fifteen times
        for (int i = 0; i < 15; i++)
        {
            idBuilder.Append(rng.Next(0, 10));
        }

        return idBuilder.ToString();
    }

    // Create a new card
    public Card CreateCard()
    {
        // Generate a new unique ID for the card
        string id = GenerateId();
        
        // Create new Card object
        var card = new Card { Id = id, Balance = 0M };
        
        // Store the card in the Dictionary
        cards[id] = card;

        // Return the newly created card
        return card;
    }

    // Retrieve the balance of a card by its ID
    public decimal GetBalance(string id)
    {
        // Check if the card exists in the Dictionary
        if (cards.TryGetValue(id, out var card))
        {
            // If it exists, return the balance
            return card.Balance;
        }

        // If the card doesn't exist, return 0
        return 0M;
    }

    // Make a payment using a card by its ID
    public void Pay(string id, decimal amount)
    {
        // Check if the card exists in the Dictionary
        if (cards.TryGetValue(id, out var card))
        {
            // If it exists, update the balance
            card.Balance += amount;
        }
    }
}
