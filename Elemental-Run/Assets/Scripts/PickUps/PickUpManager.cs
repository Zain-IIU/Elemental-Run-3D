using System;
using UnityEngine;


[SelectionBase]
    public class PickUpManager : MonoBehaviour
    {
        [SerializeField] private PickUpType type;
        [SerializeField] private ObstacleColor elementColor;
        public ObstacleElement pickUpColor;

        //testing
        [SerializeField] private new MeshRenderer renderer;
        //
        
        private void Start()
        {
            pickUpColor.colorName = elementColor.elementName;
            pickUpColor.color = elementColor.color;
            //testing
            renderer.material.color = elementColor.color;
            //
        }


        public void TakeAction(BodyTypeManager playerBody,PlayerParticles particles)
        {
            switch (type)
            {
                case PickUpType.Coin:
                   // EventsManager.PositiveEffect();
                    break;
                case PickUpType.Element:
                    if (playerBody.GetBodyColor() == pickUpColor.colorName)
                    {
                        particles.PlaySuitableVfx(playerBody.GetBodyColor());
                        EventsManager.PositiveEffect();
                    }
                    else
                        EventsManager.NegativeEffect();
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
