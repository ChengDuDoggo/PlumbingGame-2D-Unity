using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum WaterPipeOfType
{
    WaterTap,
    CommonWaterPipe,
    EmptyBlock,
    Sewer
}
public enum WaterPipeWaterInletDirection
{
    TopRight,
    CenterRigth,
    BottonRight,
    BottomLeft,
    CenterLeft,
    TopLeft
}
public class WaterConnectionPipeGamePage : MonoBehaviour
{
    private Slot[] allSlots;
    private List<Slot> correctPathSlots = new List<Slot>();
    public void Start()
    {
        allSlots = GetComponentsInChildren<Slot>();
        foreach (Slot slot in allSlots)
        {
            if (slot.IsRoadStrengthBlock)
            {
                correctPathSlots.Add(slot);
            }
        }
        Invoke(nameof(DetectGameWins), 1.0f);
    }
    public void DetectGameWins()
    {
        foreach (Slot slot in correctPathSlots)
        {
            if (slot.SlotWaterPipeOfType != slot.ChildHexagon.HexagonWaterPipeOfType || !slot.InletAndOutlet.SequenceEqual(slot.ChildHexagon.InletAndOutlet))
            {
                Debug.Log("水管未联通");
                return;
            }
        }
        Debug.Log("水管已联通");
    }
}
