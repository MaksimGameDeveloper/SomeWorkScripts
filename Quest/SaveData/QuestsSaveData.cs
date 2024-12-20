using System;
using System.Collections.Generic;
using _Project.Scripts.Meta.Services.Quest.Data;
using Newtonsoft.Json;

namespace _Project.Scripts.Meta.Services.Quest.SaveData
{
  public class QuestsSaveData
  {
    [JsonProperty] public Dictionary<int, QuestSaveData> Quests;
    [JsonProperty] public Dictionary<int, ChestSaveData> Chests;
    
    [JsonProperty] public DateTime DailyQuestsStartsTime;
    [JsonProperty] public DateTime WeeklyQuestsStartsTime;
    
    [JsonProperty] public int DailyActivityPoints;

    public QuestsSaveData()
    {
      Quests = null;
      Chests = null;
      DailyQuestsStartsTime = DateTime.UtcNow;
      WeeklyQuestsStartsTime = DateTime.UtcNow;
      DailyActivityPoints = 0;
    }

    [JsonConstructor]
    public QuestsSaveData(Dictionary<int, QuestSaveData> quests, Dictionary<int, ChestSaveData> chests, DateTime dailyQuestsStartsTime, DateTime weeklyQuestsStartsTime,
      int dailyActivityPoints)
    {
      Quests = quests ?? new();
      Chests = chests ?? new();

      DailyQuestsStartsTime = dailyQuestsStartsTime;
      WeeklyQuestsStartsTime = weeklyQuestsStartsTime;
      DailyActivityPoints = dailyActivityPoints;
    }
  }

  public class QuestSaveData
  {
    [JsonProperty] public IEnumerable<QuestConditionSaveData> Conditions;
    [JsonProperty] public bool Completed;

    public QuestSaveData()
    {
      Conditions = new List<QuestConditionSaveData>();
      Completed = false;
    }
    
    [JsonConstructor]
    public QuestSaveData(IEnumerable<QuestConditionSaveData> conditions, bool completed)
    {
      Conditions = conditions;
      Completed = completed;
    }
  }
  
  public class ChestSaveData
  {
    [JsonProperty] public bool Completed;

    public ChestSaveData()
    {
      Completed = false;
    }
    
    [JsonConstructor]
    public ChestSaveData(bool completed)
    {
      Completed = completed;
    }
  }
}