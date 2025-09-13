using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardHolder : MonoBehaviour
{
    [Header("Cards Parameters")]
    public int amtOfCards;
    public Cards[] plantCardSO;          // ScriptableObjects (array)
    public GameObject cardPrefab;        // prefab for UI card
    public Transform cardHolderTransform;

    [Header("Plant Parameters (debug only)")]
    public GameObject[] plantCards;
    public float cooldown;
    public int cost;
    public Texture plantIcon;

    private void Start()
    {
        amtOfCards = plantCardSO.Length;
        plantCards = new GameObject[amtOfCards];

        for (int i = 0; i < amtOfCards; i++)
        {
            AddPlantCard(i);
        }
    }

    public void AddPlantCard(int index)
    {
        GameObject card = Instantiate(cardPrefab, cardHolderTransform);

        plantCards[index] = card;

        // Debug vars (optional)
        plantIcon = plantCardSO[index].plantIcon;
        cost = plantCardSO[index].cost;
        cooldown = plantCardSO[index].cooldown;

        // Updating UI
        RawImage rawImg = card.GetComponentInChildren<RawImage>();
        if (rawImg != null && plantIcon != null)
            rawImg.texture = plantIcon;

        TMP_Text text = card.GetComponentInChildren<TMP_Text>();
        if (text != null)
            text.text = cost.ToString();
    }
}
