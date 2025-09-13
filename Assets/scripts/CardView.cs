using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text costText;

    /// <summary>
    /// Assigns the icon and cost to this card.
    /// </summary>
    public void Setup(Sprite icon, int cost)
    {
        if (iconImage != null)
            iconImage.sprite = icon;

        if (costText != null)
            costText.text = cost.ToString();
    }
}