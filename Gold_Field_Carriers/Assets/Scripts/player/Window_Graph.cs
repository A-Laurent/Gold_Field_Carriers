using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Window_Graph : MonoBehaviour
{

    [SerializeField] private Sprite CircleSprite;
    private RectTransform _graphContainer;

    private void Awake()
    {
        _graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();

        CreateCircle(new Vector2(200, 200));
        List<int> _valueList = new List<int>() { };
        ShowGraph(_valueList);
    }

    private GameObject CreateCircle(Vector2 _anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(_graphContainer, false);
        gameObject.GetComponent<Image>().sprite = CircleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = _anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }


    private void ShowGraph(List<int> _valueList)
    {
        float graphHeight = _graphContainer.sizeDelta.y;
        float yMaximum = 100f;
        float Xsize = 50f;


        GameObject lastCircleGameObject = null;
        for (int i = 0; i < _valueList.Count; i++)
        {
            float xPosition = i * Xsize;
            float yPosition = (_valueList[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;
        }
    }

    private void CreateDotConnection(Vector2 _dotPositionA, Vector2 _dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(_graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (_dotPositionB - _dotPositionA).normalized;
        float distance = Vector2.Distance(_dotPositionA, _dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = _dotPositionA + dir * distance * 0.5f;
        rectTransform.localEulerAngles = new Vector3 (0,0,Vector2.Angle(_dotPositionB, _dotPositionA));
    }
}
