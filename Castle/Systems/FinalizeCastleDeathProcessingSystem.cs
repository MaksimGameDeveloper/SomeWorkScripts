using System.Collections.Generic;
using Entitas;

namespace _Project.Scripts.Gameplay.FixedUpdateFeatures.Castle.Systems
{
  public class FinalizeCastleDeathProcessingSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _castles;
    private readonly List<GameEntity> _buffer = new(1);

    public FinalizeCastleDeathProcessingSystem(GameContext game)
    {
      _castles = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Castle,
          GameMatcher.Dead,
          GameMatcher.ProcessingDeath));
    }

    public void Execute()
    {
      foreach (GameEntity hero in _castles.GetEntities(_buffer))
      {
        hero.isProcessingDeath = false;
      }
    }
  }
}