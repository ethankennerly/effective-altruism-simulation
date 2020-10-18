using PoorFamily.Simulation;
using UnityEngine;

namespace PoorFamily.UI
{
    public sealed class SimulatorInspector : MonoBehaviour
    {
        public Simulator Simulator = new Simulator();

        public void Update()
        {
            Simulator.AddTime(Time.deltaTime);
        }
    }
}
