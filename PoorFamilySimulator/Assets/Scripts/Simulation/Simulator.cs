namespace PoorFamily.Simulation
{
    public sealed class Simulator
    {
        public YearTimer YearTimer {get;} = new YearTimer(2013, 2); 

        public void AddTime(float deltaTime)
        {
            YearTimer.AddYears(deltaTime);
        }
    }
}
