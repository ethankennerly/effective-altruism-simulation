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

        public YearTimer YearTimer { get; } = new YearTimer(2013, 100);

        public List<Human> Humans = new List<Human>();

        public Donor Donor;

        public BirthRateSimulator BirthRate;
        public BirthSimulator Birth;
        private DeathSimulator m_Death;
        public IncomeSimulator Income;

        public Simulator()
        {
            Donor = new Donor();
            BirthRate = new BirthRateSimulator(Humans);
            Birth = new BirthSimulator(Humans);
            Income = new IncomeSimulator(Humans);
            m_Death = new DeathSimulator(Humans);
            Updated.SetValue(this);
        }

        public void AddTime(float deltaTime)
        {
            YearTimer.AddYears(deltaTime * TimeScale);
            float deltaYears = YearTimer.DeltaYears;

            Donor.AddYears(deltaYears);
            BirthRate.CalculateEachByIncome();
            Birth.AddYears(deltaYears);
            Income.AddYears(deltaYears);
            m_Death.TryDeath();

            Updated.TryInvoke(this);
        }
    }
}
