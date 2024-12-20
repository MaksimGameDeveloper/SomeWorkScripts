using Entitas;

namespace _Project.Scripts.Gameplay.FixedUpdateFeatures.Castle.Systems
{
  public class CastleDeathSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _castles;

    public CastleDeathSystem(GameContext game)
    {
      _castles = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Castle, 
          GameMatcher.Dead,
          GameMatcher.ProcessingDeath));
    }

    public void Execute()
    {
      foreach (GameEntity castle in _castles)
      {
        castle.isDestructed = true;
      }
    }
  }

}