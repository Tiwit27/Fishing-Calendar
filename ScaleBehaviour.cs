using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleBehaviour : MonoBehaviour
{
    public RectTransform rectTransform;
    public RawImage background;
    public GameObject canvas;
    public GameObject TopMenu;

    private void Awake()
    {
        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            if (canvas.transform.GetChild(i).GetComponent<RectTransform>() != null)
            {
                rectTransform = canvas.transform.GetChild(i).GetComponent<RectTransform>();
                Rect safeArea = Screen.safeArea;
                Vector2 minAnchor = safeArea.position;
                Vector2 maxAnchor = minAnchor + safeArea.size;

                minAnchor.x /= Screen.width;
                minAnchor.y /= Screen.height;
                maxAnchor.x /= Screen.width;
                maxAnchor.y /= Screen.height;

                rectTransform.anchorMin = minAnchor;
                rectTransform.anchorMax = maxAnchor;
            }
        }
    }
}
