using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public WaterPipeOfType SlotWaterPipeOfType;
    public List<WaterPipeWaterInletDirection> InletAndOutlet = new List<WaterPipeWaterInletDirection>();
    [HideInInspector]
    public Hexagon ChildHexagon;
    public bool IsRoadStrengthBlock;
    [HideInInspector]
    public bool IsFixedSlot;
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        ChildHexagon = GetComponentInChildren<Hexagon>();
        if (IsRoadStrengthBlock == true && SlotWaterPipeOfType == WaterPipeOfType.WaterTap || SlotWaterPipeOfType == WaterPipeOfType.Sewer)
        {
            IsFixedSlot = true;
        }
    }
}
