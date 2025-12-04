using UnityEngine;

public class BeeFollowMouse : MonoBehaviour
{
    private RectTransform beeRect;
    private Canvas canvas;

    void Awake()
    {
        beeRect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    void Update()
    {
        Vector2 mouseScreenPos = Input.mousePosition;
        Vector2 uiPos;

        // For Screen Space Overlay, camera can be null.
        RectTransform canvasRect = canvas.transform as RectTransform;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            mouseScreenPos,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out uiPos
        );

        beeRect.anchoredPosition = uiPos;
    }
}
