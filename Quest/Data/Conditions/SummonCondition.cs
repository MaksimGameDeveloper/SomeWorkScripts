using _Project.GameFeatures.SettingsWindow.Scripts;
using _Project.Scripts.Infrastructure.SaveLoad;
using _Project.Scripts.Meta.Services.Quest.Configs;
using Bank.Window;
using Scripts.Tutorial;
using UnityEngine;

namespace _Project.Scripts.Meta.Services.Quest.Data
{
  public class SummonCondition : QuestCondition
  {
    private readonly QuestTaskTracker.QuestTaskTracker _questTaskTracker;
    private readonly IBankWindowService _bankWindowService;
    private readonly IQuestWindowService _questWindowService;
    
    public SummonCondition(QuestTaskTracker.QuestTaskTracker questTaskTracker, ISaveLoadService saveLoadService,
      IBankWindowService bankWindowService,
      string name, ConditionType type, int value, int targetValue) 
      : base(saveLoadService, name, type, value, targetValue)
    {
      _questTaskTracker = questTaskTracker;
      _bankWindowService = bankWindowService;
      _questTaskTracker.OnSummoned += OnSummoned;
    }

    public override void GoTo(QuestWindowPresenter questWindowPresenter)
    {
      questWindowPresenter.Hide();
      _bankWindowService.Show();
    }

    public override void Dispose()
    {
      _questTaskTracker.OnSummoned -= OnSummoned;
    }
    
    private void OnSummoned(int summonAmount)
    {
      if (Value < TargetValue) 
        Value = Mathf.Min(Value + summonAmount, TargetValue);
    }
    
    public override Vector2Int GetProgress() => new Vector2Int(Value, TargetValue);
  }
}