using System;
using UnityEngine;

namespace PoorFamily.Simulation
{
    [Serializable]
    public sealed class Human
    {
        [Range(100, 102400)] public float Income;
        [Range(0, 10)] public float DoublingsOfIncome;
        [Range(0f, 1f)] public float NormalizedIncome;
    }
}
