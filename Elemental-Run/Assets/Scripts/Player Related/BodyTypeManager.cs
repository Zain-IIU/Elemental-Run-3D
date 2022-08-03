using UnityEngine;
using UnityEngine.UI;


public class BodyTypeManager : MonoBehaviour
    {
        [SerializeField] private PlayerBody bodyColor;
        [SerializeField] private ElementalSprite elementalSprite;
         public BodyElement curBodyElement;

         public Image elementalImage;
        [SerializeField] private Material bodyMaterial;
        [SerializeField] private Image elementalFiller;

        private void Start()
        {
            SetColor();
        }

        private void SetColor()
        {
            curBodyElement.bodyTexture = bodyColor.bodyTexture;
            curBodyElement.colorName = bodyColor.elementName;
            bodyMaterial.mainTexture = curBodyElement.bodyTexture;
            elementalImage.sprite = elementalSprite.elementalSprite;
            elementalFiller.color = elementalSprite.color;
        }

        private void OnValidate()
        {
            SetColor();
        }

        public void UpdateColor(PlayerBody newBody,ElementalSprite newSprite)
        {
            bodyColor = newBody;
            elementalSprite = newSprite;
            SetColor();
        }

        public ElementName GetBodyColor() => bodyColor.elementName;
    }
