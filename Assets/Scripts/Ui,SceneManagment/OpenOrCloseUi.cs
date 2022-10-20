using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestProject
{
    public class OpenOrCloseUi : MonoBehaviour
    {
        [SerializeField]
        private GameObject _ui;
        public void Open()
        {
            _ui.SetActive(true);
        }
        public void Close()
        {
            _ui.SetActive(false);
        }
    }
}