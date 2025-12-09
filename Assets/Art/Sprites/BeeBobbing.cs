using UnityEngine;

public class BeeBobbing : MonoBehaviour
{
    public float amplitude = 10f;    // how high it moves
    public float frequency = 2f;     // how fast it moves

    private RectTransform rect;
    private Vector2 startPos;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        startPos = rect.anchoredPosition;
    }

    void Update()
    {
        float offsetY = Mathf.Sin(Time.time * frequency) * amplitude;
        rect.anchoredPosition = startPos + new Vector2(0f, offsetY);
    }
}
