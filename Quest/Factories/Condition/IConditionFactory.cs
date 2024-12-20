using _Project.Scripts.Meta.Services.Quest.Configs;
using _Project.Scripts.Meta.Services.Quest.Data;

namespace _Project.Scripts.Meta.Services.Quest.Factory
{
  public interface IConditionFactory
  {
    QuestCondition CreateCondition(string name, ConditionType type, int value, int targetValue);
  }
}