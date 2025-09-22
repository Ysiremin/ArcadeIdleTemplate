using UnityEngine;

public class StackableItem : MonoBehaviour, IStackable
{
    [field: SerializeField] public ItemType type { get; set; }
    public bool isInStack;
}