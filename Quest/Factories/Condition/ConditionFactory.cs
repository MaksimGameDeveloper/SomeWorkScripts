using System;
using _Project.Scripts.Meta.Services.Quest.Configs;
using _Project.Scripts.Meta.Services.Quest.Data;
using _Project.Scripts.Utils.Creators;

namespace _Project.Scripts.Meta.Services.Quest.Factory
{
  public class ConditionFactory : IConditionFactory
  {
    public QuestCondition CreateCondition(string name, ConditionType type, int value, int targetValue)
    {
      switch(type)
      {
        case ConditionType.EnterGame:
          return DiWrapper.Create<EnterGameCondition>(name, type, value, targetValue);
        case ConditionType.PlayBattle:
          return DiWrapper.Create<PlayBattleCondition>(name, type, value, targetValue);
        case ConditionType.OpenChest:
          return DiWrapper.Create<OpenChestCondition>(name, type, value, targetValue);
        case ConditionType.UpgradeCastle:
          return DiWrapper.Create<UpgradeCastleCondition>(name, type, value, targetValue);
        case ConditionType.UpgradeArchers:
          return DiWrapper.Create<UpgradeArchersCondition>(name, type, value, targetValue);
        case ConditionType.UpgradeChampion:
          return DiWrapper.Create<UpgradeChampionCondition>(name, type, value, targetValue);
        case ConditionType.Summon:
          return DiWrapper.Create<SummonCondition>(name, type, value, targetValue);
          
        default:
          throw new Exception("Not implemented condition!");
      }
    }
  }
}