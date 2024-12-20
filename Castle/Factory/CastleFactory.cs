using _Project.GameFeatures.Castle;
using _Project.GameFeatures.Castle.Configs;
using _Project.Scripts.Common.Entity;
using _Project.Scripts.Common.Extensions;
using _Project.Scripts.Infrastructure.SaveLoad;
using _Project.Scripts.Infrastructure.View;

namespace _Project.Scripts.Gameplay.FixedUpdateFeatures.Castle.Factory
{
  public class CastleFactory : ICastleFactory
  {
    private readonly CastleConfig _config;
    private readonly CastleService _castleService;
    private readonly ISaveLoadService _saveLoadService;

    private int CastleLevel => _saveLoadService.Progress.Player.CastleLevel;
    
    public CastleFactory(CastleConfig config, CastleService castleService, ISaveLoadService saveLoadService)
    {
      _config = config;
      _castleService = castleService;
      _saveLoadService = saveLoadService;
    }
    
    public void CreateCastle(EntityBehaviour castleView)
    {
      var data = _config.Data[CastleLevel];
      
      GameEntity castle = CreateGameEntity.Empty()
        .With(x => x.isCastle = true)
        .With(x => x.isEnemyTarget = true)

        .AddCurrentHp(_castleService.MaxHp)
        .AddMaxHp(_castleService.MaxHp)
        .AddViewWorldPosition(castleView.transform.position)
        .With(x => x.isNotDestructableView = true);
      
      castleView.SetEntity(castle);
      
      castle.PhysicObject.UpdatePosition(castleView.transform.position);
    }

  }
}