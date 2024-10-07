using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    //RESOURCES USED
    // <shttps://www.youtube.com/watch?v=C5bnWShD6ng
    // Turn based code- Zenva academy

    //VARIABLES
    public static GameManager instance;

    public List<Deck> decks = new List<Deck>();

    public Transform[] cardSlots;
    public bool[] availableCardSlots;

    public int handIndex = 0; //Cards in hand

    public TextMeshProUGUI deckSizeText;
    public TextMeshProUGUI discardPileText;

    public TextMeshProUGUI tackleBoxText;

    public TextMeshProUGUI trophyPointsText;

    
    public static PlayerController playerController;
    public Card card;

    void Awake()
    {
        instance = this;

        // Check if playerController is set
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>(); // Automatically finds the PlayerController in the scene
            
        }

        // Initialize PlayerController.me from the GameManager
        if (playerController != null)
        {
            PlayerController.me = playerController;
        }
        else
        {
            Debug.LogError("PlayerController not found!");
        }
    }

    public void DrawFromSpecificDeck(int deckIndex)
    {
        DrawCard(deckIndex);
    }

    public void DrawCard(int deckIndex)
    {
        //Make sure correct number of decks
        if(deckIndex < 0 || deckIndex >= decks.Count)
        {
            Debug.LogError("Invalid deck index.");
            return;
        }

        // Select the specified deck
        Deck selectedDeck = decks[deckIndex];

        //Make sure theres enough cards in the specified deck
        if (selectedDeck.cards.Count >= 1)
        {
            // randCard = deck[Random.Range(0,deck.Count)];
            Card randCard = selectedDeck.cards[Random.Range(0, selectedDeck.cards.Count)];

            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                if (availableCardSlots[i] == true)
                {
                    int baitCost = GetBaitCost(deckIndex);

                    if(playerController.baitCount < baitCost){
                        Debug.Log("Not enough Bait :( ");
                    }

                    randCard.gameObject.SetActive(true);
                    randCard.handIndex = i;
                    randCard.transform.position = cardSlots[i].position;
                    availableCardSlots[i] = false;

                    selectedDeck.cards.Remove(randCard);

                    // Assign the deck to the card (so it knows its origin)
                    randCard.originDeck = selectedDeck;


                    selectedDeck.cards.Remove(randCard); // Remove from the deck
                                    // Don't add to discard pile yet

                    // Add the card to the discard pile (Update this to fit your discard logic)
                    //selectedDeck.discardPile.Add(randCard);

                    // Update bait count
                    playerController.baitCount -= baitCost;
                    UpdateBaitUI(playerController.baitCount);

                    // Update deck and discard pile UI
                    UpdateDeckUI();

                    return;
                }
            }
        }
    }

    public void UpdateDeckUI()
    {
        for (int i = 0; i < decks.Count; i++)
        {
            deckSizeText.text = $"Remaining in deck {i+1}: {decks[i].cards.Count}";
            discardPileText.text = $"Discard Pile {i+1}: {decks[i].discardPile.Count}";
        }
    }

    public void UpdateTrophyPointsUI(int points)
    {
        trophyPointsText.text = "Trophy Points: " + points.ToString();
    }

    public void UpdateBaitUI(int baitCount)
    {
        tackleBoxText.text = "Bait: " + baitCount.ToString();
    }

    void IncreaseBait()
    {
        playerController.baitCount++;
        UpdateBaitUI(playerController.baitCount);
    }

    public int GetBaitCost(int deckIndex)
    {
        // Example of determining bait cost based on deck
        if (deckIndex == 0) return 1; // Deck 1 costs 1 bait
        if (deckIndex == 1) return 2; // Deck 2 costs 2 bait
        if (deckIndex == 2) return 3; // Deck 3 costs 3 bait
        return 0; // Default cost
    }

    public void ResetGame()
    {
        foreach (var deck in decks)
        {
            deck.cards.AddRange(deck.discardPile);  // Move all discarded cards back into the deck
            deck.discardPile.Clear();  // Clear the discard pile
        }
        
        // Update UI to reflect reset
        UpdateDeckUI();
        UpdateBaitUI(playerController.baitCount);
    }
}
