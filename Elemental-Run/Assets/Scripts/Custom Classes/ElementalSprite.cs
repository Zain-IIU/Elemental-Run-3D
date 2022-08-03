using UnityEngine;


[CreateAssetMenu(fileName = "ElementalImage", menuName = "Elemental Sprite", order = 1)]
public class ElementalSprite : BodyType
{
    [SerializeField] public Sprite elementalSprite;
    [SerializeField] public Color color;
}