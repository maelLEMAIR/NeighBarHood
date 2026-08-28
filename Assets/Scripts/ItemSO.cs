using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class ItemSO : ScriptableObject
{
    public string nameItem;
    public Sprite icon;
    public GameObject prefab;
    public UnitType unitType;
    public bool finalStat;
    public ItemType type;
}

public enum ItemType { Cocktail, Snack, Beer, Glass }

public enum UnitType
{
    Piece,
    Centiliter,
    Milliliter,
    Gram,
}