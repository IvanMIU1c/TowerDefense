using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    [RequireComponent(typeof(MapLevel))]
    public class BranchLevel : MonoBehaviour
    {
        [SerializeField] private Text m_pointText;
        [SerializeField] private MapLevel m_rootLevel;
        [SerializeField] private int m_needPoints = 3;


        /// <summary>
        /// ѕопытка активации ответвленного уровн€
        /// јктиваци€ требует наличи€ очков и выполнени€ прошлого уровн€
        /// </summary>
        internal void TryActivate()
        {
            gameObject.SetActive(m_rootLevel.IsComplete);
            if (m_needPoints> MapComplettion.Instance.TotalScore)
            {
                m_pointText.text = m_needPoints.ToString();
            } else
            {
                m_pointText.transform.parent.gameObject.SetActive(false);
                GetComponent<MapLevel>().Initialise();
            }
        }
    }
}
