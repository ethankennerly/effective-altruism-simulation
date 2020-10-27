using PoorFamily.Simulation.Donation;
using System;
using System.Collections.Generic;

namespace PoorFamily.Simulation
{
    [Serializable]
    public sealed class LiteracyTeacher
    {
        public float TuitionPerIncome = 20f / 128f;
        public float MinTuition = 50f;
        public float LiteracyRate;
        public float Funds;

        private readonly List<Human> m_Humans;
        private readonly Donor m_Donor;

        public LiteracyTeacher(List<Human> humans, Donor donor = null)
        {
            m_Humans = humans;
            m_Donor = donor;
        }

        public void AddFunds(float funds)
        {
            Funds += funds;
        }

        public void TryTeachEach()
        {
            TryAddDonorFunds();
            LiteracyRate = TryTeachEachWithFunds();
            TrySetNoRoomForFunding(LiteracyRate >= 1f);
        }

        private float TryTeachEachWithFunds()
        {
            int numHumans = m_Humans.Count;
            if (numHumans == 0)
            {
                LiteracyRate = 0f;
            }

            float sumOfIsLiterates = 0f;
            foreach (Human human in m_Humans)
            {
                if (!human.IsLiterate)
                {
                    float tuition = human.Income * TuitionPerIncome;
                    if (Funds < tuition)
                    {
                        continue;
                    }

                    Funds -= tuition;
                    human.IsLiterate = true;
                }

                sumOfIsLiterates++;
            }

            return sumOfIsLiterates / numHumans;
        }

        #region Donor

        private void TryAddDonorFunds()
        {
            if (m_Donor == null)
            {
                return;
            }

            ADonorOption literacyOption = m_Donor.OptionMenu.Pratham;
            if (!literacyOption.Funded)
            {
                return;
            }
       
            UnityEngine.Debug.Log("Literacy funded");
            AddFunds(literacyOption.Cost);
            literacyOption.Funded = false;
            literacyOption.FundingProgress = 0f;
        }

        private void TrySetNoRoomForFunding(bool noRoomForFunding)
        {
            if (m_Donor == null)
            {
                return;
            }

            ADonorOption literacyOption = m_Donor.OptionMenu.Pratham;
            literacyOption.NoRoomForFunding = noRoomForFunding;
        }

        #endregion Donor
    }
}
