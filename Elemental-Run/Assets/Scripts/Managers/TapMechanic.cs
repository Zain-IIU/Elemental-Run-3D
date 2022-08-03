using System;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.UI;

public class TapMechanic : MonoBehaviour
{
    [SerializeField] private Image tapBar;
    [Header("Projectile Prefabs")]
    [SerializeField] private Ball projectilePrefabFire;
    [SerializeField] private Ball projectilePrefabIce;
    [SerializeField] private Ball projectilePrefabWater;
    [SerializeField] private Ball projectilePrefabEarth;
    [SerializeField] private Transform projectileSpawnPoint;
    [Space]
    [SerializeField] private PlayerParticles playerParticles;
    [SerializeField] private BodyTypeManager playerBody;
    [SerializeField] private AnimationController playerAnimator;
    [Space]
    [Header("Projectile Color Scriptable Objects")]
    [SerializeField] private ObstacleColor fireColor;
    [SerializeField] private ObstacleColor iceColor;
    [SerializeField] private ObstacleColor waterColor;
    [SerializeField] private ObstacleColor earthColor;
    public ObstacleElement color;
    
    private ElementName _element;
    private bool _hasShot;
  
    
    //called from pointer click event
    public void FillTapBar()
    {
        EventsManager.instance.Haptic(HapticTypes.MediumImpact);
        tapBar.DOFillAmount(tapBar.fillAmount + 0.2f, .15f);
        playerParticles.GetEndParticle()
            .DOScale(playerParticles.GetEndParticle().transform.localScale + Vector3.one * .1f, .15f);
        
        if (!(tapBar.fillAmount >= 0.8f) || _hasShot) return;
        HideFillBar();
        playerAnimator.PlayAttackAnimation();
        _hasShot = true;
    }

    private void ShootTheProjectile()
    {
        _element = playerBody.GetBodyColor();
        Ball projectile = null;
        switch (_element)
        {
            case ElementName.Fire:
                projectile = Instantiate(projectilePrefabFire, projectileSpawnPoint, true);
                color.color = fireColor.color;
                color.colorName = fireColor.elementName;
                break;
            case ElementName.Earth:
                projectile = Instantiate(projectilePrefabEarth, projectileSpawnPoint, true);
                color.color = earthColor.color;
                color.colorName = earthColor.elementName;
                break;
            case ElementName.Water:
                projectile = Instantiate(projectilePrefabWater, projectileSpawnPoint, true);
                color.color = waterColor.color;
                color.colorName = waterColor.elementName;
                break;
            case ElementName.Ice:
                projectile = Instantiate(projectilePrefabIce, projectileSpawnPoint, true);
                color.color = iceColor.color;
                color.colorName = iceColor.elementName;
                break;
            case ElementName.Normal:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (projectile is null) return;
        
        projectile.SetBallColor(color);
        projectile.gameObject.SetActive(false);
        projectile.transform.DOScale(Vector3.one * 2f, 0);
        projectile.transform.DOLocalRotate(Vector3.zero, 0);
        projectile.transform.DOLocalMove(Vector3.zero, 0).OnComplete(() =>
        {
            projectile.gameObject.SetActive(true);
            projectile.ThrowBall(30, transform,false);
            playerParticles.StopEndVFX();
        });
    }



    #region Event Callbacks

    private void HideFillBar()
    {
        tapBar.transform.parent.parent.GetComponent<RectTransform>().DOScale(Vector3.zero, .14f);
    }

    #endregion
}
