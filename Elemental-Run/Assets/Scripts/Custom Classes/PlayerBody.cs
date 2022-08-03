  using UnityEngine;

  [CreateAssetMenu(fileName = "New Body", menuName = "Player ody Colors/Colors", order = 0)]
    public class PlayerBody : BodyType
    {
      [SerializeField] public Texture bodyTexture;
    }
