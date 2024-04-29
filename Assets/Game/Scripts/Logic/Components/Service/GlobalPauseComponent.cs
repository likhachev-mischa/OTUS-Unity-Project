using Unity.Entities;

namespace Game.Components
{
    /// <summary>
    /// Singleton, if exists - systems, marked with this component, update
    /// otherwise don't.
    /// Destroyed on pause, created on resume
    /// </summary>
    public struct GlobalPauseComponent : IComponentData
    {
    }
}