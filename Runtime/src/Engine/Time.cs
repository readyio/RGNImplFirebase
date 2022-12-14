using RGN.Dependencies.Engine;

namespace RGN.Impl.Firebase.Engine
{
    public sealed class Time : ITime
    {
        float ITime.time => UnityEngine.Time.time;

        float ITime.timeScale { get => UnityEngine.Time.timeScale; set => UnityEngine.Time.timeScale = value; }
    }
}
