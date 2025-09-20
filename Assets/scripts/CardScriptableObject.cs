using UnityEngine;

[CreateAssetMenu(menuName = "Card/New Plant Card", fileName = "New Plant Card", order = 51)]
public class CardScriptableObject : ScriptableObject
{
    [Header("Card Settings")]
    public Sprite plantSprite;   // for UI (Image)
    public Texture plantIcon;    // for RawImage if you still need Texture
    public GameObject prefab;    // actual plant prefab
    public int cost;             // sun or resource cost
    public float cooldown;       // time between uses
}
