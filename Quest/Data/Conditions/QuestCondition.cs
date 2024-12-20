using System;
using _Project.GameFeatures.SettingsWindow.Scripts;
using _Project.Scripts.Infrastructure.SaveLoad;
using _Project.Scripts.Meta.Services.Quest.Configs;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Scripts.Meta.Services.Quest.Data
{
  public abstract class QuestCondition : QuestConditionSaveData, IDisposable
  {
    public ConditionType Type;
    public int Value
    {
      get => base.Value;
      set
      {
        base.Value = value;
        _saveLoadService.SaveProgress(SaveReason.QuestStateUpdated, $"{GetType()} changed value to: {Value}");
      }
    }

    public int TargetValue;

    private readonly ISaveLoadService _saveLoadService;

    protected QuestCondition(ISaveLoadService saveLoadService, string name, ConditionType type, int value, int targetValue) : base(name, value)
    {
      _saveLoadService = saveLoadService;
      Type = type;
      TargetValue = targetValue;
    }
    
    public abstract Vector2Int GetProgress();
    public virtual void GoTo(QuestWindowPresenter questWindowPresenter) { }
    public abstract void Dispose();
  }
  
  public class QuestConditionSaveData
  {
    [JsonProperty] public string Name;
    [JsonProperty] public int Value;
    
    public QuestConditionSaveData(string name, int value)
    {
      Name = name;
      Value = value;
    }
  }
}