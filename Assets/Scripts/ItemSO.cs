using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class ItemSO : ScriptableObject
{
    public string nameItem;
    public Sprite icon;
    public GameObject prefab;
    public UnitType unitType;
}

public enum UnitType
{
    Piece,
    Centiliter,
    Milliliter,
    Gram,
}