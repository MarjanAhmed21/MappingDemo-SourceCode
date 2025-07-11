using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorManager : MonoBehaviour
{
    public ItemController[] ItemsButtons;
    public GameObject[] ItemPrefabs;
    public GameObject[] ItemImage;
    public int CurrentButtonPressed;

    private void Update()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        if(Input.GetMouseButtonDown(0) && ItemsButtons[CurrentButtonPressed].Clicked)
        {
            ItemsButtons[CurrentButtonPressed].Clicked = false;
            Instantiate(ItemPrefabs[CurrentButtonPressed], new Vector3(worldPosition.x, worldPosition.y, 0), Quaternion.identity);
            Destroy(GameObject.FindGameObjectWithTag("ItemImage"));
        }
    }
}
