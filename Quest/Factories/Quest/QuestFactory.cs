using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Infrastructure.SaveLoad;
using _Project.Scripts.Meta.Services.Quest.Configs;
using _Project.Scripts.Meta.Services.Quest.Data;
using _Project.Scripts.Meta.Services.Quest.SaveData;

namespace _Project.Scripts.Meta.Services.Quest.Factory
{
  public class QuestFactory : IQuestFactory
  {
    private readonly QuestConditionsConfig _questConditionsConfig;
    private readonly IConditionFactory _conditionFactory;
    private readonly ISaveLoadService _saveLoadService;
    private readonly QuestsConfig _questsConfig;
    
    public QuestFactory(QuestConditionsConfig questConditionsConfig, QuestsConfig questsConfig, IConditionFactory conditionFactory,
      ISaveLoadService saveLoadService)
    {
      _questConditionsConfig = questConditionsConfig;
      _questsConfig = questsConfig;
      _conditionFactory = conditionFactory;
      _saveLoadService = saveLoadService;
    }
    
    
    public Data.Quest CreateQuest(int questId, Dictionary<int, QuestSaveData> questsData)
    {
      List<QuestCondition> conditions = new();
      QuestData questData = _questsConfig.Data[questId];
      
      
      foreach (QuestConditionSaveData conditionData in questsData[questId].Conditions)
      {
        var conditionConfigData = _questConditionsConfig.Data[conditionData.Name];
    
        QuestCondition questCondition = _conditionFactory.CreateCondition(conditionData.Name, conditionConfigData.Type, conditionData.Value, questData.ConditionsData.Conditions
          .First(x => x.Name == conditionData.Name).TargetValue);
        conditions.Add(questCondition);
      }
    
      Data.Quest quest = new Data.Quest(_saveLoadService)
      {
        Id = questData.Id,
        Completed = questsData[questId].Completed,
        Name = questData.Name,
        Rewards = questData.Rewards,
        Conditions = conditions,
      };
      
      questsData[questId] = quest;
      return quest;
    }
    
    public Data.Quest CreateQuest(int questId)
    {
      List<QuestCondition> conditions = new();
      
      QuestData questData = _questsConfig.Data[questId];
    
      foreach (QuestConditionData conditionData in questData.ConditionsData.Conditions)
      {
        var conditionConfigData = _questConditionsConfig.Data[conditionData.Name];
        QuestCondition questCondition = _conditionFactory.CreateCondition(conditionConfigData.Name, conditionConfigData.Type, 0, conditionData.TargetValue);
        conditions.Add(questCondition);
      }
      
      Data.Quest quest = new Data.Quest(_saveLoadService)
      {
        Id = questData.Id,
        Completed = false,
        Name = questData.Name,
        Rewards = questData.Rewards,
        Conditions = conditions,
      };
      
      return quest;
    }
  }
}