using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BaseItemCarrier : MonoBehaviour
{
    public BaseStack stack;
    private BaseStack currentStackArea;

    public virtual void Collect(StackableItem item)
    {
        if (item == null || item.isInStack)
            return;

        if (stack == null || stack.IsFull || (stack.acceptedItemTypes.Any() && !stack.acceptedItemTypes.Contains(item.type)))
            return;

        stack.AddItem(item);
    }

    /// <summary>
    /// Stackteki itemleri targetStacke aktarır
    /// </summary>
    /// <param name="targetStack"></param>
    public virtual void GiveItems()
    {
        Debug.LogError("BaseItemCarrier - GiveItems" + currentStackArea.gameObject.name);
        TransferItems(stack, currentStackArea);
    }

    /// <summary>
    /// targetStackteki itemleri kendi stackine aktarır
    /// </summary>
    /// <param name="targetStack"></param>
    public virtual void GetItems()
    {
        Debug.LogError("BaseItemCarrier - GetItems" + currentStackArea.gameObject.name);
        TransferItems(currentStackArea, stack);
    }

    public void TransferItems(BaseStack sourceStack, BaseStack targetStack)
    {
        var itemsToTransfer = sourceStack.GetItems().ToList();
        foreach (var item in itemsToTransfer)
        {
            if (targetStack.AddItem(item))
                sourceStack.RemoveItem(item);
        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        Debug.LogError("BaseItemCarrier - OnTriggerEnter" + other.gameObject.name);
        if (other.TryGetComponent<StackableItem>(out var item))
            Collect(item);

        if (other.TryGetComponent<BaseStack>(out var otherStack))
        {
            currentStackArea = otherStack;
            switch (otherStack.areaType)
            {
                case StackAreaType.Both:
                    // Both
                    break;
                case StackAreaType.InputOnly:
                    GiveItems();
                    //otherStack.onItemAdded.AddListener(GiveItems); // Event için çalışacak method ayırlacak.
                    break;
                case StackAreaType.OutputOnly:
                    GetItems();
                    //otherStack.onItemAdded.AddListener(GetItems);
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<BaseStack>(out var otherStack))
        {
            currentStackArea = null;
            switch (otherStack.areaType)
            {
                case StackAreaType.Both:
                    // Both
                    break;
                case StackAreaType.InputOnly:
                    //otherStack.onItemAdded.RemoveListener(GiveItems);
                    break;
                case StackAreaType.OutputOnly:
                    //otherStack.onItemAdded.RemoveListener(GetItems);
                    break;
            }

        }
    }
}