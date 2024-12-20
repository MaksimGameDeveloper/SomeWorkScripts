using _Project.Scripts.Infrastructure.SaveLoad;
using _Project.Scripts.Meta.Services.Quest.Configs;
using UnityEngine;

namespace _Project.Scripts.Meta.Services.Quest.Data
{
  public class EnterGameCondition : QuestCondition
  {
    private readonly QuestTaskTracker.QuestTaskTracker _questTaskTracker;
    public EnterGameCondition(QuestTaskTracker.QuestTaskTracker questTaskTracker, ISaveLoadService saveLoadService, string name, ConditionType type, int value, int targetValue) 
      : base(saveLoadService, name, type, value, targetValue) 
    {
      _questTaskTracker = questTaskTracker;
      
      _questTaskTracker.OnGameEntered += OnGameEntered;
    }


    public override void Dispose()
    {
      _questTaskTracker.OnGameEntered -= OnGameEntered;
    }
    
    private void OnGameEntered()
    {
      if(Value < TargetValue) 
        Value++;
    }

    public override Vector2Int GetProgress() => new Vector2Int(Value, TargetValue);
  }

}