using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TestProject
{
    public class Level : MonoBehaviour
    {
        [SerializeField]
        private List<Cell> _cellsToBeFilled;

        [HideInInspector]
        public List<Drag> _cubesToBePlaced;

        [SerializeField]
        public GameObject WinUi;
        [SerializeField]
        public GameObject RestartUi;
        public UnityEvent lose;
        public UnityEvent win;

        private void Start()
        {
            Drag[] draggables = FindObjectsOfType<Drag>();

            foreach (var draggable in draggables)
            {
                _cubesToBePlaced.Add(draggable);
            }
        }

        public void CheckCells()
        {
            

            foreach (var cell in _cellsToBeFilled)
            {
                if (cell._filled)
                {
                    continue;
                }
                for (int i = 0; i < _cubesToBePlaced.Count; i++)
                {
                    if (!_cubesToBePlaced[i]._placed)
                    {
                        return;
                    }
                }
                RestartUi.SetActive(true);
                lose.Invoke();
                return;
            }
            WinUi.SetActive(true);
            win.Invoke();
            
        }
    }
}
