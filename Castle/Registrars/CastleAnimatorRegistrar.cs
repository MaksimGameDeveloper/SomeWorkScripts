using System;
using _Project.Scripts.Gameplay.FixedUpdateFeatures.Castle.Behaviours;
using _Project.Scripts.Infrastructure.View.Registrars;

namespace _Project.Scripts.Gameplay.FixedUpdateFeatures.Castle.Registrars
{
  public class CastleAnimatorRegistrar : EntityComponentRegistrar
  {
    public CastleAnimator CastleAnimator;
    
    public override void RegisterComponents()
    {
      if(CastleAnimator == null)
        throw new Exception("Registration error: object is null");
      
      Entity
        .AddCastleAnimator(CastleAnimator)
        .AddAbilityTakenAnimator(CastleAnimator)
        ;
    }
    
    public override void UnregisterComponents()
    {
      if(Entity.hasCastleAnimator)
        Entity.RemoveCastleAnimator();
      
      if(Entity.hasAbilityTakenAnimator)
        Entity.RemoveAbilityTakenAnimator();
    }
  }
}