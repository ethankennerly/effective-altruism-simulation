using FineGameDesign.Events;
using PoorFamily.Simulation.Donation;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoorFamily.Simulation
{
    [Serializable]
    public sealed class Simulator
    {
        public readonly ActionEvent<Simulator> Updated = new ActionEvent<Simulator>();

        public float TimeScale = 1f;

        public YearTimer YearTimer = new YearTimer(2013, 100);

        public List<Human> Humans = new List<Human>();
        public int NumHumans;
        public string NumHumansString;

        public Donor Donor;

        public BirthRateSimulator BirthRate;
        public BirthSimulator Birth;
        private DeathSimulator m_Death;
        public IncomeSimulator Income;
        public LiteracyTeacher Teacher;

        public Simulator()
        {
            Donor = new Donor();
            Teacher = new LiteracyTeacher(Humans, Donor);
            BirthRate = new BirthRateSimulator(Humans);
            Birth = new BirthSimulator(Humans);
            Income = new IncomeSimulator(Humans, Donor);
            m_Death = new DeathSimulator(Humans);
            Updated.SetValue(this);
        }

        public void AddTime(float deltaTime)
        {
            YearTimer.AddYears(deltaTime * TimeScale);
            float deltaYears = YearTimer.DeltaYears;

            Donor.AddYears(deltaYears);
            Teacher.TryTeachEach();
            BirthRate.CalculateEach();
            Birth.AddYears(deltaYears);
            Income.AddYears(deltaYears);
            m_Death.TryDeath();

            int nextHumans = Humans.Count;
            if (NumHumans != nextHumans)
            {
                NumHumans = nextHumans;
                NumHumansString = nextHumans.ToString();
            }

            Updated.TryInvoke(this);
        }
    }
}
