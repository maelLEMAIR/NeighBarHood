using UnityEngine;

[CreateAssetMenu(fileName = "Item Recipe",  menuName = "Scriptable Objects/Item Recipe")]
public class ItemRecipeSO1 : ScriptableObject
{
    public string recipeName;
    public RecipeType recipeType;

    public ItemTypeAndCount[] input;
    public ItemTypeAndCount[] output;
}

public enum RecipeType {  Cocktails, Trays, Basic };

[System.Serializable]
public class ItemTypeAndCount
{
    public ItemSO item;
    public int amount;

    public ItemTypeAndCount(ItemSO i, int c)
    {
        item = i;
        amount = c;
    }
}