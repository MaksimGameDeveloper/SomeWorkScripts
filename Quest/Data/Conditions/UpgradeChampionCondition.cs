using _Project.GameFeatures.MonsterInfoWindow;
using _Project.GameFeatures.SettingsWindow.Scripts;
using _Project.Scripts.Infrastructure.SaveLoad;
using _Project.Scripts.Meta.Services.Quest.Configs;
using Squad.Windows;
using UnityEngine;

namespace _Project.Scripts.Meta.Services.Quest.Data
{
  public class UpgradeChampionCondition : QuestCondition
  {
    private readonly QuestTaskTracker.QuestTaskTracker _questTaskTracker;
    private readonly ISquadWindowService _squadWindowService;
    
    public UpgradeChampionCondition(QuestTaskTracker.QuestTaskTracker questTaskTracker, ISaveLoadService saveLoadService, 
      ISquadWindowService squadWindowService ,string name, ConditionType type, int value, int targetValue) 
      : base(saveLoadService, name, type, value, targetValue)
    {
      _questTaskTracker = questTaskTracker;
      _squadWindowService = squadWindowService;

      _questTaskTracker.OnChampionUpgraded += OnChampionUpgraded;
    }


    public override void Dispose()
    {
      _questTaskTracker.OnChampionUpgraded -= OnChampionUpgraded;
    }

    public override void GoTo(QuestWindowPresenter questWindowPresenter)
    {
      questWindowPresenter.Hide();
      
      _squadWindowService.Show(UnitType.Champion);
    }

    private void OnChampionUpgraded()
    {
      if(Value < TargetValue)
        Value++;
    }
    
    public override Vector2Int GetProgress() => new Vector2Int(Value, TargetValue);
  }
}