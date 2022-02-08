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

        public bool interactable = true;


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

        public void SetupCard(CardScript card)
        {
            OnPointerExit(null);

            interactable = false;
            spriteUI.color = card.spriteUI.color;
            rareUI.SetActive(card.rareUI.activeSelf);
            nameMainText.text = "";
            detailsText.text = "";
            spriteUI.sprite = card.spriteUI.sprite;
            this.callback = null;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!interactable) return;
            transform.localScale = Vector3.one * 1.2f;
            foreach (var item in displayHoverText)
            {
                item.SetActive(true);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!interactable) return;
            transform.localScale = Vector3.one;
            foreach (var item in displayHoverText)
            {
                item.SetActive(false);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!interactable) return;
            callback?.Invoke();
        }

    }
}