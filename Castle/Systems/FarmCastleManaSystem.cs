using _Project.Scripts.Gameplay.Common.Time;
using Entitas;
using UnityEngine;

namespace _Project.Scripts.Gameplay.FixedUpdateFeatures.Castle.Systems
{
  public class FarmCastleManaSystem : IExecuteSystem
  {
    private readonly ITimeService _time;
    private readonly IGroup<GameEntity> _castles;

    public FarmCastleManaSystem(GameContext game, ITimeService time)
    {
      _time = time;
      _castles = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Castle,
          GameMatcher.ManaValue,
          GameMatcher.ManaMaxValue,
          GameMatcher.ManaFarmSpeed));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _castles)
      {
        if (entity.ManaValue > entity.ManaMaxValue)
          entity.ReplaceManaValue(entity.ManaMaxValue);
        
        if(Mathf.Approximately(entity.ManaValue, entity.ManaMaxValue))
          continue;
        
        entity.ReplaceManaValue(entity.ManaValue + _time.DeltaTime * entity.ManaFarmSpeed);
      }
    }
  }

}