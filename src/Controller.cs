using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Grid grid = null;
    public GameObject cellPrefab;

    public Slider slider_XGridSize;
    public Text slider_Xtext;
    public Slider slider_YGridSize;
    public Text slider_Ytext;

    private void Start()
    {
        grid = new Grid();
    }

    public void newGrid()
    {
        grid.destroyCells();
        grid.generateNewGrid((int)slider_XGridSize.value, (int)slider_YGridSize.value, cellPrefab);
        grid.initializeRandomPos();
    }

    public void setText_X()
    {
        slider_Xtext.text = slider_XGridSize.value.ToString();
    }
    public void setText_Y()
    {
        slider_Ytext.text = slider_YGridSize.value.ToString();
    }

    public void nextGen()
    {
        if (!grid) grid.runNextGeneration();
    }

}
