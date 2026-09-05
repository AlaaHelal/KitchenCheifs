using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{

    public List<KitchenObjectsSO> recipeKitchenObjectsSOList;
    public string nameOfRecipe;
}
