using System.Collections.Generic;
using _Project.Scripts.Infrastructure.SaveLoad;
using _Project.Scripts.Meta.Services.Quest.SaveData;
using Scripts.Infrastructure.Services.Rewards;

namespace _Project.Scripts.Meta.Services.Quest.Data
{
  public class Quest : QuestSaveData
  {
    public int Id;
    public string Name;
    public List<RewardData> Rewards;
    
    private readonly ISaveLoadService _saveLoadService;

    public new List<QuestCondition> Conditions
    {
      get => (List<QuestCondition>)base.Conditions;
      set
      {
        base.Conditions = value;
      }
    }

    public new bool Completed
    {
      get => base.Completed;
      set
      {
        base.Completed = value;
        _saveLoadService.SaveProgress(SaveReason.QuestStateUpdated, $"{Name} quest completed state: {base.Completed}!");
      }
    }
    
    public Quest(ISaveLoadService saveLoadService)
    {
      _saveLoadService = saveLoadService;
    }
  }
}