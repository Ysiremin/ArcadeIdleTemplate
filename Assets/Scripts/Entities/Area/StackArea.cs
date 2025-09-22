using System;
using UnityEngine;

public class StackArea : BaseStack
{
    public StackableItem testStackableItem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            AddItem(testStackableItem);
    }
}
