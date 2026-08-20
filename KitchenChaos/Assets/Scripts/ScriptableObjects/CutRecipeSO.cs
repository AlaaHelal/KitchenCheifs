using UnityEngine;

[CreateAssetMenu()]
public class CutRecipeSO : ScriptableObject
{
    public KitchenObjectsSO input;
    public KitchenObjectsSO output;
    public int cuttingProgressMax;
}
