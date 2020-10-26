using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UnityUICookbook
{
    [HelpURL("https://subscription.packtpub.com/book/game_development/9781785885822/1/ch01lvl1sec16/selecting-buttons-through-the-keyboard")]
    public sealed class ButtonThroughKeySelection : MonoBehaviour
    {
        [SerializeField] private KeyCode m_SelectionKey = default;
        [SerializeField] private GameObject m_SelectableObject = null;

        private void Update()
        {
            if (!Input.GetKeyDown(m_SelectionKey))
            {
                return;
            }

            EventSystem.current.SetSelectedGameObject(m_SelectableObject);
        }
    }
}
