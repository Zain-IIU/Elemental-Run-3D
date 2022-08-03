using System;
using DG.Tweening;
using UnityEngine;

public class GateManager : MonoBehaviour
{
    [SerializeField] private GateType type;
    [SerializeField] private float xTweenPos;
    [SerializeField] private float tweenTime;
    [SerializeField] private ObstacleColor obstacleColor;
    [SerializeField] private PlayerBody bodyToChange;
    [SerializeField] private ElementalSprite spriteToChange;
    [SerializeField] private SpriteRenderer gateRenderer;
    public ObstacleElement gateColor;
    [SerializeField] private MeshRenderer[] childMesh;
    
    
    void Start()
    {
        TakeAction();
        SetGateColor();
    }
    
    private void TakeAction()
    {
        switch (type)
        {
            case GateType.Static:
                break;
            case GateType.MoveAble:
                transform.DOLocalMoveX(xTweenPos, tweenTime).SetLoops(-1, LoopType.Yoyo);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void SetGateColor()
    {
        gateColor.color = obstacleColor.color;
        gateColor.colorName = obstacleColor.elementName;
        gateRenderer.color = obstacleColor.color;
        foreach (var mesh in childMesh)
        {
            mesh.material.color = obstacleColor.color;
        }
    }

    public PlayerBody GetGateColor() => bodyToChange;
    public ElementalSprite GetSpriteColor() => spriteToChange;
    
    
}
