using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class CursorChanger : MonoBehaviour
    {
        public static CursorChanger instance;
        private void Awake()
        {
            instance = this;
        }

        public Texture2D hCursor, gCursor;
        Texture2D currentCursor;


        [ContextMenu("Hunter Cursor")]
        public void SetHunterCursor()
        {
            currentCursor = hCursor;
            ApplyCurrentCursor();
        }

        [ContextMenu("Gorilla Cursor")]
        public void SetGorillaCursor()
        {
            currentCursor = gCursor;
            ApplyCurrentCursor();
        }

        public void ApplyCurrentCursor()
        {
            if (currentCursor == null) return;
            Cursor.SetCursor(currentCursor, Vector3.one * currentCursor.width * 0.5f, CursorMode.Auto);
        }

        [ContextMenu("None Cursor")]
        public void ResetCursor()
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }


    }
}
