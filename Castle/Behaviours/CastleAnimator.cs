using _Project.Scripts.Gameplay.Common.Visuals;
using _Project.Scripts.Utils.Gui.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Gameplay.FixedUpdateFeatures.Castle.Behaviours
{
  public class CastleAnimator : AnimatorWrapper, IAbilityTakenAnimator
  {
    [SerializeField] private GameObject _healVFX;

    private readonly int HitStateHash = Animator.StringToHash("Hit");
    
    
    public void PlayAppear()
    {
      
    }
    public void PlayAbilityTaken(string targetVFX)
    {
      AnimatorStateInfo stateInfo = Animator.GetCurrentAnimatorStateInfo(0);

      if(stateInfo.shortNameHash == HitStateHash) 
        PlayAnimation(HitStateHash, 0.05f);
      else
        PlayAnimation(HitStateHash);
    }
    
    public async UniTaskVoid PlayHeal()
    {
      _healVFX.SetActive(true);
      await UniTask.Delay(3000);
      _healVFX.SetActive(false);
    }
    
    public void PlayDeath()
    {
      
    }
  }
}