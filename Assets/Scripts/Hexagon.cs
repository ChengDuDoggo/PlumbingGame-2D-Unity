using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hexagon : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private WaterConnectionPipeGamePage parentPage;
    private Slot parentSlot;
    private Image hexagonImage;
    private Color originalColor;
    private RectTransform rectTransform;
    public WaterPipeOfType HexagonWaterPipeOfType;
    public List<WaterPipeWaterInletDirection> InletAndOutlet = new List<WaterPipeWaterInletDirection>();
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        Warning();
        parentPage = GetComponentInParent<WaterConnectionPipeGamePage>();
        parentSlot = GetComponentInParent<Slot>();
        rectTransform = GetComponent<RectTransform>();
        hexagonImage = GetComponent<Image>();
        originalColor = hexagonImage.color;
    }
    private void Warning()
    {
        switch (HexagonWaterPipeOfType)
        {
            case WaterPipeOfType.WaterTap:
                if (InletAndOutlet.Count != 1)
                {
                    Debug.LogError("水龙头应该只设置一个出水口！");
                }
                break;
            case WaterPipeOfType.CommonWaterPipe:
                if (InletAndOutlet.Count != 2)
                {
                    Debug.LogError("普通水管应该只设置两个出入水口！");
                }
                break;
            case WaterPipeOfType.EmptyBlock:
                if (InletAndOutlet.Count != 0)
                {
                    Debug.LogError("空格子应该不设置出入水口！");
                }
                break;
            case WaterPipeOfType.Sewer:
                if (InletAndOutlet.Count != 1)
                {
                    Debug.LogError("下水道应该只设置一个入水口！");
                }
                break;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (Input.touchCount > 1 || parentSlot.IsFixedSlot)
        {
            return;//禁止多点触控，并且水龙头和下水道无法拖拽
        }
        eventData.pointerDrag.transform.SetParent(parentSlot.transform.parent);
        eventData.pointerDrag.transform.SetAsFirstSibling();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPos);
        rectTransform.localPosition = localPos;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Hexagon slotEndHexagon = eventData.pointerCurrentRaycast.gameObject.GetComponent<Hexagon>();//拖拽结束时拿到的对象
        Hexagon slotDragHexagon = eventData.pointerDrag.GetComponent<Hexagon>();//拖拽时的对象
        if (slotDragHexagon.HexagonWaterPipeOfType == WaterPipeOfType.WaterTap || slotDragHexagon.HexagonWaterPipeOfType == WaterPipeOfType.Sewer)
        {
            return;
        }
        if (slotEndHexagon == null)
        {
            slotDragHexagon.transform.SetParent(slotDragHexagon.parentSlot.transform);
            //slotDragHexagon.rectTransform.localPosition = Vector3.zero;
            rectTransform.DOAnchorPos(Vector3.zero, 1.0f);
            return;
        }
        if (slotEndHexagon.HexagonWaterPipeOfType == WaterPipeOfType.WaterTap || slotEndHexagon.HexagonWaterPipeOfType == WaterPipeOfType.Sewer || slotEndHexagon == this)
        {
            slotDragHexagon.transform.SetParent(slotDragHexagon.parentSlot.transform);
            //slotDragHexagon.rectTransform.localPosition = Vector3.zero;
            rectTransform.DOAnchorPos(Vector3.zero, 1.0f);
        }
        else
        {
            Transform transformCup = slotDragHexagon.parentSlot.transform;
            Slot slotCup = slotDragHexagon.parentSlot;
            slotDragHexagon.transform.SetParent(slotEndHexagon.parentSlot.transform);
            slotDragHexagon.parentSlot = slotEndHexagon.parentSlot;
            slotDragHexagon.parentSlot.ChildHexagon = slotDragHexagon;
            //slotDragHexagon.rectTransform.localPosition = Vector3.zero;
            slotDragHexagon.rectTransform.DOAnchorPos(Vector3.zero, 1.2f);
            slotEndHexagon.transform.SetParent(transformCup);
            slotEndHexagon.parentSlot = slotCup;
            slotEndHexagon.parentSlot.ChildHexagon = slotEndHexagon;
            //slotEndHexagon.rectTransform.localPosition = Vector3.zero;
            slotEndHexagon.rectTransform.DOAnchorPos(Vector3.zero, 1.2f);
        }
        slotDragHexagon.UpdateHexagonTransparency(1.0f);
        slotEndHexagon.UpdateHexagonTransparency(1.0f);
        parentPage.DetectGameWins();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsDraggedHexagon(eventData) && !IsFixedSlotHexagon())
        {
            UpdateHexagonTransparency(0.5f); // 将透明度设置为0.5
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!IsDraggedHexagon(eventData) && !IsFixedSlotHexagon())
        {
            RestoreHexagonTransparency(); // 恢复原来的透明度
        }
    }
    private bool IsDraggedHexagon(PointerEventData eventData)
    {
        Hexagon draggedHexagon = eventData.pointerDrag?.GetComponent<Hexagon>();
        return draggedHexagon != null && draggedHexagon == this;
    }
    private bool IsFixedSlotHexagon()
    {
        return HexagonWaterPipeOfType == WaterPipeOfType.WaterTap || HexagonWaterPipeOfType == WaterPipeOfType.Sewer;
    }
    private void UpdateHexagonTransparency(float alpha)
    {
        Color color = hexagonImage.color;
        color.a = alpha;
        hexagonImage.color = color;
    }
    private void RestoreHexagonTransparency()
    {
        hexagonImage.color = originalColor;
    }
}
