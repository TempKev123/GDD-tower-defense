using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Coin Settings")]
    public int startingCoins = 50;
    private int currentCoins;

    [Header("UI")]
    public TMP_Text coinText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        currentCoins = startingCoins;
        UpdateCoinUI();
    }

    public bool CanAfford(int cost)
    {
        return currentCoins >= cost;
    }

    public void SpendCoins(int cost)
    {
        currentCoins -= cost;
        if (currentCoins < 0) currentCoins = 0;
        UpdateCoinUI();
    }

    public void AddCoins(int amount)
    {
        currentCoins += amount;
        UpdateCoinUI();
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = "" + currentCoins;
    }
}
