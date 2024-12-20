using _Project.GameFeatures.SettingsWindow.Scripts;
using _Project.Scripts.Infrastructure.SaveLoad;
using _Project.Scripts.Meta.Services.Quest.Configs;
using Scripts.Tutorial;
using UnityEngine;

namespace _Project.Scripts.Meta.Services.Quest.Data
{
  public class PlayBattleCondition : QuestCondition
  {
    private readonly QuestTaskTracker.QuestTaskTracker _questTaskTracker;
    
    public PlayBattleCondition(QuestTaskTracker.QuestTaskTracker questTaskTracker, ISaveLoadService saveLoadService, 
      string name, ConditionType type, int value, int targetValue)
      : base(saveLoadService, name, type, value, targetValue)
    {
      _questTaskTracker = questTaskTracker;
    
      _questTaskTracker.OnBattleStarted += OnBattleStarted;
    }

    public override void Dispose()
    {
      _questTaskTracker.OnBattleStarted -= OnBattleStarted;
    }

    public override void GoTo(QuestWindowPresenter questWindowPresenter)
    {
      questWindowPresenter.Hide();
      
      TutorialService.SendTrigger("go_to_quest_play_battle");
    }

    private void OnBattleStarted()
    {
      if(Value < TargetValue)
        Value++;
    }
    
    public override Vector2Int GetProgress() => new Vector2Int(Value, TargetValue);
  }
}