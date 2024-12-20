using _Project.GameFeatures.SettingsWindow.Scripts;
using _Project.Scripts.Infrastructure.SaveLoad;
using _Project.Scripts.Meta.Services.Quest.Configs;
using Bank.Window;
using Scripts.Tutorial;
using Summons.Core;
using UnityEngine;

namespace _Project.Scripts.Meta.Services.Quest.Data
{
  public class OpenChestCondition : QuestCondition
  {
    private readonly QuestTaskTracker.QuestTaskTracker _questTaskTracker;
    
    public OpenChestCondition(QuestTaskTracker.QuestTaskTracker questTaskTracker, ISaveLoadService saveLoadService, 
      string name, ConditionType type, int value, int targetValue) 
      : base(saveLoadService, name, type, value, targetValue)
    {
      _questTaskTracker = questTaskTracker;

      _questTaskTracker.OnChestOpened += OnChestOpened;
    }

    public override void Dispose()
    {
      _questTaskTracker.OnChestOpened -= OnChestOpened;
    }

    public override void GoTo(QuestWindowPresenter questWindowPresenter)
    {
      TutorialService.SendTrigger("go_to_quest_open_chest");
    }

    private void OnChestOpened(int amount)
    {
      if (Value < TargetValue)
        Value = Mathf.Min(Value + amount, TargetValue);
    }
    
    public override Vector2Int GetProgress() => new Vector2Int(Value, TargetValue);
  }

}