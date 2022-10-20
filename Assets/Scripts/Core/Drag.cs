using System.Collections.Generic;
using UnityEngine;

namespace TestProject
{
    public class Drag : MonoBehaviour
    {
        public bool _placed { get; private set; }

        [SerializeField]
        private GameObject _cellsParent;
        [SerializeField]
        private GameObject _pocketsParent;

        [SerializeField]
        private List<Cell> _cells;
        [SerializeField]
        private List<Pocket> _pockets;

        private float _startPosX;
        private float _startPosY;
        private Pocket _occupiedPocket;

        public float ReturnPositionX { get; private set; }
        public float ReturnPositionY { get; private set; }

        private float _time = 0;
        private const float _longClick = 1.5f;
        private bool _clicked;


        void Start()
        {
            var localPosition = transform.localPosition;
            ReturnPositionX = localPosition.x;
            ReturnPositionY = localPosition.y;
        }

        private void OnMouseDown()
        {
            if (!_placed)
            {
                _clicked = true;
                var mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                var localPosition = transform.localPosition;
                _startPosX = mousePos.x - localPosition.x;
                _startPosY = mousePos.y - localPosition.y;
                if(_occupiedPocket!= null)
                _occupiedPocket.filled = false;
            }
            
        }

        private void OnMouseUp()
        {
            if (!_placed)
            {
                _clicked = false;
                _time = 0;
                CalculateDistance();
            }
        }

        private void Update()
        {
            if (!_placed)
            {
                if (_clicked)
                {
                    _time += Time.deltaTime;
                }

                if (_time >= _longClick)
                {
                    var mousePos = Input.mousePosition;
                    mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                    transform.localPosition = new Vector3(mousePos.x - _startPosX, mousePos.y - _startPosY,
                        gameObject.transform.localPosition.z);
                }
            }
        }

        private void CalculateDistance()
        {
            Cell[] cells = _cellsParent.GetComponentsInChildren<Cell>();

            foreach (var cell in cells)
            {
                if (!cell._filled)
                {
                    _cells.Add(cell);
                }
            }

            for (int i = 0; i < _cells.Count; i++)
            {
                var position = transform.position;
                var cell = _cells[i];
                var cellPosition = cell.transform.position;

                var inRangeByX = Mathf.Abs(position.x - cellPosition.x) <= 0.5f;
                var inRangeByY = Mathf.Abs(position.y - cellPosition.y) <= 0.5f;

                if (inRangeByX && inRangeByY)
                {
                    transform.position = cellPosition;
                    cell._filled = true;
                    _placed = true;
                    _cellsParent.GetComponent<Level>().CheckCells();
                    _cells.Clear();
                    return;
                }
            }
            _cells.Clear();

            Pocket[] pockets = _pocketsParent.GetComponentsInChildren<Pocket>();

            foreach (var pocket in pockets)
            {
                if (!pocket.filled)
                {
                    _pockets.Add(pocket);
                }
            }
            for (int i = 0; i < _pockets.Count; i++)
            {
                var position = transform.position;
                var pocket = _pockets[i];
                var pocketPosition = pocket.transform.position;

                var inRangeByX = Mathf.Abs(position.x - pocketPosition.x) <= 0.5f;
                var inRangeByY = Mathf.Abs(position.y - pocketPosition.y) <= 0.5f;

                if (inRangeByX && inRangeByY)
                {
                    pocket.filled = true;
                    _occupiedPocket = pocket;
                    transform.position = pocketPosition;
                    ReturnPositionX = transform.position.x;
                    ReturnPositionY = transform.position.y;
                    _pockets.Clear();
                    return;
                }
            }
            _pockets.Clear();
            if(_occupiedPocket!= null)
            {
                _occupiedPocket.filled = true;
            }
            transform.localPosition = new Vector3(ReturnPositionX, ReturnPositionY, transform.localPosition.z);
        }
    }
}
