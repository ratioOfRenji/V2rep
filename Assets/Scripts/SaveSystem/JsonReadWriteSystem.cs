using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestProject
{
    public class JsonReadWriteSystem : MonoBehaviour
    {
        public static string directory = "/SaveData/";
        public static string fileName = "MyData.txt";


        public bool _refreshJson = false;
        [SerializeField]
        public Slider _volumeSlider;
        [SerializeField]
        public Cell[] _cellsToBeSaved;
        [SerializeField]
        public Drag cube;
        [HideInInspector]
        public Vector2 pos;

        public bool[] _defaultCellsValue;
        public Vector2 _defaultCubePos;
        private void SaveToJson()
        {
            string dir = Application.persistentDataPath + directory;
            if (!Directory.Exists(dir))
              Directory.CreateDirectory(dir);


            DataToBeSaved data = new DataToBeSaved();
            data.audioVolume = _volumeSlider.value;
            data.cellsToBeSaved = new bool[_cellsToBeSaved.Length];
            for (int i = 0; i < _cellsToBeSaved.Length; i++)
            {
                data.cellsToBeSaved[i] = _cellsToBeSaved[i]._filled;
            }
            data.xCubePos = pos.x;
            data.yCubePos = pos.y;

            string json = JsonUtility.ToJson(data);
            File.WriteAllText(dir + fileName, json);
        }

        private void LoadFromJson()
        {
            string fullPath = Application.persistentDataPath + directory + fileName;
            DataToBeSaved data = new DataToBeSaved();

            if (File.Exists(fullPath))
            {
                string json = File.ReadAllText(fullPath);
                data = JsonUtility.FromJson<DataToBeSaved>(json);
                _volumeSlider.value = data.audioVolume;
                for (int i = 0; i < _cellsToBeSaved.Length; i++)
                {

                    _cellsToBeSaved[i]._filled = data.cellsToBeSaved[i];
                }
               
                cube.transform.position = new Vector3(data.xCubePos, data.yCubePos, 0);
            }

           
        }
        public void RefreshJson()
        {
            string dir = Application.persistentDataPath + directory;
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            DataToBeSaved data = new DataToBeSaved();
            data.audioVolume = _volumeSlider.value;

            
            data.cellsToBeSaved = new bool[_defaultCellsValue.Length];
            for (int i = 0; i < _defaultCellsValue.Length; i++)
            {
                data.cellsToBeSaved[i] = _defaultCellsValue[i];
            }
            data.xCubePos = _defaultCubePos.x;
            data.yCubePos = _defaultCubePos.y;

            string json = JsonUtility.ToJson(data);
            File.WriteAllText(dir + fileName, json);
        }
        public void SetBoolToRefresh()
        {
            _refreshJson = true;
        }
        /*INVOKES SAVE/LOAD METHODS. ENABLE THIS FOE BUILD*/
        //private void Awake()
        //{
        //    LoadFromJson();
        //}

        //private void OnApplicationPause()
        //{
        //    if (_refreshJson)
        //    {
        //        RefreshJson();
        //    }
        //    else
        //    {
        //        SaveToJson();
        //    }
        //}


        /*INVOKES SAVE/LOAD METHODS. ENABLE THIS FOE BUILD*/
        private void OnEnable()
        {
            LoadFromJson();
        }
        private void OnDisable()
        {
            if (_refreshJson)
            {
                RefreshJson();
            }
            else
            {
                SaveToJson();
            }
        }
        private void Update()
        {
            pos = new Vector2(cube.transform.position.x, cube.transform.position.y);
        }
    }
}