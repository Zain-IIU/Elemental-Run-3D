
using UnityEngine;


[CreateAssetMenu(fileName = "New Color", menuName = "Obstacle Colors/Colors", order = 0)]
public class ObstacleColor : BodyType
{
    [SerializeField]   public Color color;  
}   
