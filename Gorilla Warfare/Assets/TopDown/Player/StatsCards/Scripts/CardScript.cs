using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

namespace TopDown
{
    public class CardScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public Color rareColor;
        public GameObject rareUI;
        public Image spriteUI;
        public TextMeshProUGUI nameMainText, detailsText;

        public GameObject[] displayHoverText;

        Action callback;


        public void SetupCard(string nameText, string detailText, Sprite sprite, bool isRare, Action callback)
        {
            OnPointerExit(null);

            spriteUI.color = isRare ? rareColor : Color.white;
            rareUI.SetActive(isRare);
            nameMainText.text = nameText;
            detailsText.text = detailText;
            spriteUI.sprite = sprite;
            this.callback = callback;
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.localScale = Vector3.one * 1.2f;
            foreach (var item in displayHoverText)
            {
                item.SetActive(true);
            }

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = Vector3.one;
            foreach (var item in displayHoverText)
            {
                item.SetActive(false);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            callback?.Invoke();
        }

    }
}