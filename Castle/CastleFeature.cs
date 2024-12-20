using _Project.Scripts.Gameplay.FixedUpdateFeatures.Castle.Systems;
using _Project.Scripts.Infrastructure.Systems;

namespace _Project.Scripts.Gameplay.FixedUpdateFeatures.Castle
{
  public sealed class CastleFeature : Feature
  {
    public CastleFeature(ISystemFactory systems)
    {
      Add(systems.Create<InitializeCastleSystem>());
      
      Add(systems.Create<FarmCastleManaSystem>());
      
      Add(systems.Create<UpdateCastleHPViewSystem>());
      Add(systems.Create<UpdateCastleManaViewSystem>());
      
      Add(systems.Create<CastleDeathSystem>());
      Add(systems.Create<FinalizeCastleDeathProcessingSystem>());
    }
  }

}