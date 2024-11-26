using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.UI.Shop
{
    public class ShopViewHandler : MonoBehaviour
    {
        [SerializeField] private GameObject[] _pages;
        [SerializeField] private Image[] _buttons;
        [SerializeField] private Sprite _selected;
        [SerializeField] private Sprite _unselected;
        [Space]
        private int _currentPage;
        
        private void Start() => _SetPage(0);

        public void _SetPage(int page)
        {
            _buttons[_currentPage].sprite = _unselected;
            _pages[_currentPage].SetActive(false);
            _currentPage = page;
            _buttons[_currentPage].sprite = _selected;
            _pages[_currentPage].SetActive(true);
        }
    }
}