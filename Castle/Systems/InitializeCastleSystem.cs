using _Project.Scripts.Gameplay.FixedUpdateFeatures.Castle.Factory;
using _Project.Scripts.Infrastructure.View;
using Battlefields;
using Entitas;

namespace _Project.Scripts.Gameplay.FixedUpdateFeatures.Castle.Systems
{
  public class InitializeCastleSystem : IInitializeSystem
  {
    private readonly BattlefieldService _battlefieldService;
    private readonly ICastleFactory _castleFactory;
  
    public InitializeCastleSystem(BattlefieldService battlefieldService, ICastleFactory castleFactory)
    {
      _battlefieldService = battlefieldService;
      _castleFactory = castleFactory;
    }
  
    public void Initialize()
    {
      EntityBehaviour castleView = _battlefieldService.Current.CastleView.EntityBehaviour;
      _castleFactory.CreateCastle(castleView);
    }
  }
}
