using PoorFamily.Simulation;
using PoorFamily.Simulation.Donation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PoorFamily.UI
{
    public class ADonorOptionButton : ASimulatorObserver
    {
        [SerializeField] private Button m_SelectButton = null;
        [Header("Select a child object to activate")]
        [SerializeField] private GameObject m_ActiveIfNoRoomForFunding = null;
        [SerializeField] private TMP_Text m_CostText = null;

        [SerializeField] protected ADonorOption m_DonorOption;

        protected override void OnEnable()
        {
            m_SelectButton.onClick.AddListener(m_DonorOption.Select);
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            m_SelectButton.onClick.RemoveListener(m_DonorOption.Select);
            base.OnEnable();
        }

        private void LateUpdate()
        {
            SetActiveByNoRoomForFunding();
        }

        /// <remarks>
        /// <a href="https://forum.unity.com/threads/gameobject-is-already-being-activated-or-deactivated.279888/">
        /// Unity doesn't like deactivating during disable, so this is postponed to late update.
        /// </a>
        /// </remarks>
        private void SetActiveByNoRoomForFunding()
        {
            if (m_ActiveIfNoRoomForFunding == null)
            {
                return;
            }

            bool noRoomForFunding = m_DonorOption.NoRoomForFunding;
            if (m_ActiveIfNoRoomForFunding.activeSelf == noRoomForFunding)
            {
                return;
            }

            m_ActiveIfNoRoomForFunding.SetActive(noRoomForFunding);
        }

        protected override void OnSimulatorUpdated(Simulator simulator)
        {
            if (!enabled || !Application.isPlaying)
            {
                return;
            }

            if (m_CostText.text != m_DonorOption.CostString)
            {
                m_CostText.text = m_DonorOption.CostString;
            }

            m_SelectButton.interactable =
                !m_DonorOption.WillFund &&
                !m_DonorOption.NoRoomForFunding;

            SetActiveByNoRoomForFunding();
        }
    }
}
