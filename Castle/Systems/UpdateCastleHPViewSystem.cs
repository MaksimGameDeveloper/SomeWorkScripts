using Entitas;
using UnityEngine;
using Widgets.TopBars;

namespace _Project.Scripts.Gameplay.FixedUpdateFeatures.Castle.Systems
{
  public class UpdateCastleHPViewSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _castleHps;
    
    private readonly TopBarsWidget _topBarsWidget;

    public UpdateCastleHPViewSystem(GameContext game, ITopBarsWidgetService topBarsWidget)
    {
      _topBarsWidget = topBarsWidget.Widget;
      _castleHps = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Castle,
          GameMatcher.CurrentHp, 
          GameMatcher.MaxHp));
    }

    public void Execute()
    {
      foreach (GameEntity castleHp in _castleHps)
      {
        if(Mathf.Approximately(_topBarsWidget.CurrentHp, castleHp.CurrentHp) == false)
          _topBarsWidget.SetHp((int)castleHp.CurrentHp);
      }
    }
  }

}