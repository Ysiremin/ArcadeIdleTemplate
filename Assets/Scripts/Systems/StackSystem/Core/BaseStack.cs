using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class BaseStack : MonoBehaviour
{
    public int capacity = 20;
    public SOGridSettings gridSettings;
    public List<ItemType> acceptedItemTypes;
    public StackAreaType areaType;
    public List<StackableItem> items;

    public bool IsFull => items.Count >= capacity;
    public bool HasItems => items.Count > 0;

    //public UnityEvent onItemAdded;
    
    public virtual bool AddItem(StackableItem stackableItem)
    {
        if (stackableItem == null || IsFull) 
            return false;

        if (acceptedItemTypes.Any() && !acceptedItemTypes.Contains(stackableItem.type))
            return false;

        items.Add(stackableItem);
        stackableItem.isInStack = true;
        
        stackableItem.transform.SetParent(transform);

        if (gridSettings != null)
        {
            var targetPos = CalculateItemPosition(items.Count - 1);
            stackableItem.transform.localPosition = targetPos;
            stackableItem.transform.localRotation = Quaternion.identity;
        }
        
        //onItemAdded?.Invoke();

        return true;
    }

    public virtual bool RemoveItem(StackableItem stackableItem)
    {
        if (stackableItem == null || !items.Contains(stackableItem)) 
            return false;

        return items.Remove(stackableItem);
    }

    public virtual Vector3 CalculateItemPosition(int index)
    {
        if (gridSettings == null) 
            return Vector3.zero;

        var perLayer = gridSettings.columns * gridSettings.rows;
        var layer = index / perLayer;
        var inLayerIndex = index % perLayer;

        var row = inLayerIndex / gridSettings.columns;
        var column = inLayerIndex % gridSettings.columns;

        var x = column * gridSettings.columnSpacing;
        var z = row * gridSettings.rowSpacing;
        var y = layer * gridSettings.heightSpacing;

        return new Vector3(x, y, z);
    }

    public List<StackableItem> GetItems()
    {
        return items;
    }
}