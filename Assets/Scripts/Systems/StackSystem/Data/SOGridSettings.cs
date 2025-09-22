using UnityEngine;

[CreateAssetMenu(fileName = "SOGridSettings", menuName = "Stacking/Grid Settings")]
public class SOGridSettings : ScriptableObject
{
    [Header("Grid Boyutları")]
    public int columns = 1;   
    public int rows = 1;      
    public int height = 1;    

    [Header("Aralıklar")]
    public float columnSpacing = 1f; 
    public float rowSpacing = 1f;    
    public float heightSpacing = 1f; 
}