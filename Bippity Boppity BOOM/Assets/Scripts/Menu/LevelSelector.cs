/* #region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class LevelSelector : MonoBehaviour
{
    public GameObject levelHolder;
    public GameObject levelIcon;
    public int numberOfLevels = 7;

    void Start() // Start is called before the first frame update
    {
        Rect panelDimensions = levelHolder.GetComponent<RectTransform>().rect;
        Rect iconDimensions = levelIcon.GetComponent<RectTransform>().rect;
        int maxInARow = Mathf.FloorToInt(panelDimensions.width / iconDimensions.width);
        int MaxInACol = Mathf.FloorToInt(panelDimensions.height / iconDimensions.height);
        int amountPerPage = maxInARow * MaxInACol;
        int totalPages = Mathf.CeilToInt((float)numberOfLevels / amountPerPage);
        LoadPanels(totalPages);
    }

    void LoadPanels(int numberOfPanels)
    {
        Debug.Log(numberOfPanels);
    }

    void Update() // Update is called once per frame
    {
        
    }
}
*/
