using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LngBridgeItem : MonoBehaviour
{
    public int ID;
    private LevelEditorManager editor;
    // Start is called before the first frame update
    void Start()
    {
        editor = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Destroy(this.gameObject);
            editor.ItemsButtons[ID].quantity++;
            editor.ItemsButtons[ID].quantityText.text = editor.ItemsButtons[ID].quantity.ToString();
        }
    }
}
