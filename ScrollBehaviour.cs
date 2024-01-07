using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScrollBehaviour : MonoBehaviour
{
    [SerializeField] GameObject ScrollRect;
    [SerializeField] GameObject MainSide;
    [SerializeField] GameObject FishScrollRect;
    [SerializeField] GameObject topMenu;
    [SerializeField] GameObject MainSideFishes;
    private void Start()
    {
        CheckScroll();
    }
    public void CheckScroll()
    {
        if(MainSide.transform.childCount > 0)
        {
            if (MainSide.transform.GetChild(MainSide.transform.childCount - 1).transform.position.y < Screen.safeArea.yMin)
            {
                ScrollRect.GetComponent<ScrollRect>().enabled = true;
            }
            else
            {
                ScrollRect.GetComponent<ScrollRect>().enabled = false;
            }
        }
    }
    public void MainMenuScroll()
    {
        for (int i = 0; i < MainSide.transform.childCount; i++)
        {
            if (MainSide.transform.GetChild(i).transform.position.y > topMenu.transform.position.y)
            {
                MainSide.transform.GetChild(i).transform.localScale = new Vector2(0, 1);
            }
            else
            {
                MainSide.transform.GetChild(i).transform.localScale = new Vector2(1, 1);
            }
        }
    }
    public void SetStartPosition()
    {
        MainSide.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }
    public void SetStartPositionFishes()
    {
        MainSideFishes.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }
    public void MainMenuFishesScroll()
    {
        for (int i = 0; i < MainSideFishes.transform.childCount; i++)
        {
            if (MainSideFishes.transform.GetChild(i).transform.position.y > topMenu.transform.position.y)
            {
                MainSideFishes.transform.GetChild(i).transform.localScale = new Vector2(0, 1);
            }
            else
            {
                MainSideFishes.transform.GetChild(i).transform.localScale = new Vector2(1, 1);
            }
        }
    }
    public void CheckFishesScroll()
    {
        if (MainSideFishes.transform.childCount > 0)
        {
            if (MainSideFishes.transform.GetChild(MainSideFishes.transform.childCount - 1).transform.position.y < Screen.safeArea.yMin)
            {
                FishScrollRect.GetComponent<ScrollRect>().enabled = true;
            }
            else
            {
                FishScrollRect.GetComponent<ScrollRect>().enabled = false;
            }
        }
    }
}
