using _Project.Scripts.Gameplay.FixedUpdateFeatures.Castle.Behaviours;
using Entitas;

namespace _Project.Scripts.Gameplay.FixedUpdateFeatures.Castle
{
  [Game] public class Castle : IComponent { }
  
  [Game] public class CastleAnimatorComponent : IComponent { public CastleAnimator Value; }
}