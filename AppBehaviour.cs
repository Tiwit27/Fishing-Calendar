using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppBehaviour : MonoBehaviour
{
    [SerializeField] GameObject topMenu;
    [SerializeField] public GameObject addNew;
    [SerializeField] GameObject mainSide;
    void Start()
    {
        addNew.SetActive(false);
        topMenu.SetActive(true);
        mainSide.SetActive(true);
    }
}
