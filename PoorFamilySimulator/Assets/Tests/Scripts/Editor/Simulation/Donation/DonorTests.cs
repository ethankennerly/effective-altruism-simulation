using NUnit.Framework;
using PoorFamily.Simulation.Donation;

namespace PoorFamily.Tests.Simulation.Donation
{
    public sealed class DonorTests
    {
        [Test]
        public void AddYears_DisposableIncomeIncreasesFundsAvailable()
        {
            Donor donor = new Donor{DisposableIncome = 30000f};
            donor.AddYears(0.5f);
            Assert.AreEqual(15000f, donor.FundsAvailable);
            donor.AddYears(0.5f);
            Assert.AreEqual(30000f, donor.FundsAvailable);
        }

        [Test]
        public void FundsAvailableUnderCost_GiveDirectlyNotFunded()
        {
            Donor donor = new Donor{FundsAvailable = 1204f};
            donor.OptionMenu.GiveDirectly.Cost = 1205f;
            donor.AddYears(0f);
            Assert.IsFalse(donor.OptionMenu.GiveDirectly.Funded);
        }

        [Test]
        public void FundsAvailableAtCostNotSelected_GiveDirectlyNotFunded()
        {
            Donor donor = new Donor{FundsAvailable = 1205f};
            donor.OptionMenu.GiveDirectly.Cost = 1205f;
            donor.AddYears(0f);
            Assert.IsFalse(donor.OptionMenu.GiveDirectly.Funded);
        }

        [Test]
        public void FundsAvailableAtCostNotSelected_FundsAvailableNotSpent()
        {
            Donor donor = new Donor{FundsAvailable = 1205f};
            donor.OptionMenu.GiveDirectly.Cost = 1205f;
            donor.AddYears(0f);
            Assert.AreEqual(1205f, donor.FundsAvailable);
        }

        [Test]
        public void SelectGiveDirectly_FundsAvailableUnderCost_FundingProgress()
        {
            Donor donor = new Donor{FundsAvailable = 900f};
            donor.OptionMenu.GiveDirectly.Cost = 1200f;
            donor.OptionMenu.GiveDirectly.WillSelectNext = true;
            donor.AddYears(0f);
            Assert.AreEqual(0.75f, donor.OptionMenu.GiveDirectly.FundingProgress);
        }

        [Test]
        public void SelectGiveDirectly_FundsAvailableUnderCost_FundsAvailableNotSpent()
        {
            Donor donor = new Donor{FundsAvailable = 900f};
            donor.OptionMenu.GiveDirectly.Cost = 1200f;
            donor.OptionMenu.GiveDirectly.WillSelectNext = true;
            donor.AddYears(0f);
            Assert.AreEqual(900f, donor.FundsAvailable);
        }

        [Test]
        public void SelectGiveDirectlyThenSave_GiveDirectlyFundingProgressEquals0()
        {
            Donor donor = new Donor{FundsAvailable = 900f};
            donor.OptionMenu.GiveDirectly.Cost = 1200f;
            donor.OptionMenu.GiveDirectly.WillSelectNext = true;
            donor.AddYears(0f);
            donor.OptionMenu.Save.WillSelectNext = true;
            donor.AddYears(0f);
            Assert.AreEqual(0f, donor.OptionMenu.GiveDirectly.FundingProgress);
        }

        [Test]
        public void SelectGiveDirectlyThenSave_WaitTwoYears_SaveWillFund()
        {
            Donor donor = new Donor{FundsAvailable = 900f};
            donor.OptionMenu.GiveDirectly.Cost = 1200f;
            donor.OptionMenu.GiveDirectly.WillSelectNext = true;
            donor.AddYears(0f);
            donor.OptionMenu.Save.WillSelectNext = true;
            donor.AddYears(1f);
            donor.AddYears(1f);
            Assert.IsTrue(donor.OptionMenu.Save.WillFund);
        }

        [Test]
        public void SelectGiveDirectly_SubtractsCostFromFundsAvailable()
        {
            Donor donor = new Donor{FundsAvailable = 2205f};
            donor.OptionMenu.GiveDirectly.Cost = 1205f;
            donor.OptionMenu.GiveDirectly.WillSelectNext = true;
            donor.AddYears(0f);
            Assert.AreEqual(1000f, donor.FundsAvailable);
        }

        [Test]
        public void SelectGiveDirectly_ExtraFundsAvailable_Funded()
        {
            Donor donor = new Donor{FundsAvailable = 2205f};
            donor.OptionMenu.GiveDirectly.Cost = 1205f;
            donor.OptionMenu.GiveDirectly.WillSelectNext = true;
            donor.AddYears(0f);
            Assert.IsTrue(donor.OptionMenu.GiveDirectly.Funded);
        }
    }
}
