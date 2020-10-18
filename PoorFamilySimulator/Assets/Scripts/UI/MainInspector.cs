using PoorFamily.Simulation;
using UnityEngine;

namespace PoorFamily.UI
{
    public sealed class MainInspector : MonoBehaviour
    {
        public Simulator Simulator {get;} = new Simulator();

        public void Update()
        {
            Simulator.AddTime(Time.deltaTime);
        }
    }
}
