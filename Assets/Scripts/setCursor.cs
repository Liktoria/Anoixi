using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setCursor : MonoBehaviour
{
    public Texture2D cursorImage;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 cursorOffset = new Vector2(cursorImage.width / 2, cursorImage.height / 2);
        Cursor.SetCursor(cursorImage, cursorOffset, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
