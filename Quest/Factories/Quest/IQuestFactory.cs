using System.Collections.Generic;
using _Project.Scripts.Meta.Services.Quest.Data;
using _Project.Scripts.Meta.Services.Quest.SaveData;

namespace _Project.Scripts.Meta.Services.Quest.Factory
{
  public interface IQuestFactory
  {
    Data.Quest CreateQuest(int questId, Dictionary<int, QuestSaveData> questsData);
    Data.Quest CreateQuest(int questId);
  }
}