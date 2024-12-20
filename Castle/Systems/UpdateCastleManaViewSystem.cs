using Entitas;
using UnityEngine;
using Widgets.TopBars;

namespace _Project.Scripts.Gameplay.FixedUpdateFeatures.Castle.Systems
{
  public class UpdateCastleManaViewSystem : IExecuteSystem
  {
    private readonly TopBarsWidget _topBarsWidget;
    private readonly IGroup<GameEntity> _castles;

    public UpdateCastleManaViewSystem(GameContext game, ITopBarsWidgetService topBarsWidget)
    {
      _topBarsWidget = topBarsWidget.Widget;
      _castles = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Castle, 
          GameMatcher.ManaValue));
    }

    public void Execute()
    {
      foreach (GameEntity castle in _castles)
      {
        if(Mathf.Approximately(_topBarsWidget.CurrentMana, (int)castle.ManaValue) == false) 
          _topBarsWidget.SetMana((int)castle.ManaValue);
      }
    }
  }

}