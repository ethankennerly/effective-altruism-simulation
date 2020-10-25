using FineGameDesign.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoorFamily.Simulation
{
    [Serializable]
    public sealed class Simulator
    {
        public readonly ActionEvent<Simulator> Updated = new ActionEvent<Simulator>();

        public YearTimer YearTimer { get; } = new YearTimer(2013, 100); 

        public List<Human> Humans = new List<Human>();

        public BirthSimulator Birth;
        private DeathSimulator m_Death;
        public IncomeSimulator Income;

        public Simulator()
        {
            Birth = new BirthSimulator(Humans);
            Income = new IncomeSimulator(Humans);
            m_Death = new DeathSimulator(Humans);
            Updated.SetValue(this);
        }

        public void AddTime(float deltaTime)
        {
            YearTimer.AddYears(deltaTime);
            float deltaYears = YearTimer.DeltaYears;
            Birth.AddYears(deltaYears);
            Income.AddYears(deltaYears);
            m_Death.TryDeath();

            Updated.TryInvoke(this);
        }
    }
}
