using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoorFamily.Simulation
{
    [Serializable]
    public sealed class Human
    {
        [Header("Income")]
        [Range(0f, 102400f)] public float Income;
        [Range(0f, 10f)] public float DoublingsOfIncome;
        [Range(0f, 1f)] public float NormalizedIncome;
        public List<float> ScheduledTransfers;

        [Header("Reproduction")]
        [Range(0f, 130f)] public float Age;
        [Range(0f, 130f)] public float LifeExpectancy = 55f;
        public bool IsFemale;
        [Range(0f, 0.07f)] public float BirthRate;
        public FloatRange FertileAgeRange;

        public static void AddAgeToEach(List<Human> humans, float deltaYears)
        {
            foreach (Human human in humans)
            {
                human.Age += deltaYears;
            }
        }
    }
}
