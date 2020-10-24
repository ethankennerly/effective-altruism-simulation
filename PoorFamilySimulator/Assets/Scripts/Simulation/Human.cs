using System;
using UnityEngine;

namespace PoorFamily.Simulation
{
    [Serializable]
    public sealed class Human
    {
        [Header("Income")]
        [Range(100f, 102400f)] public float Income;
        [Range(0f, 10f)] public float DoublingsOfIncome;
        [Range(0f, 1f)] public float NormalizedIncome;

        [Header("Reproduction")]
        [Range(0f, 130f)] public float Age;
        [Range(0f, 130f)] public float LifeExpectancy;
        public bool IsFemale;
        [Range(0f, 0.07f)] public float BirthRate;
        public FloatRange FertileAgeRange;
    }
}
