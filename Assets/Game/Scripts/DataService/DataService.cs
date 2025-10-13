using System.IO;
using UnityEngine;

namespace CEVerticalShooter.Core.Save
{
    public class DataService : IDataService<GameData>
    {
        private GameData _data;
        public GameData Data => _data;

        private string FilePath => Path.Combine(Application.persistentDataPath, "data.json");
        public DataService() 
        {
            if(File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                _data = JsonUtility.FromJson<GameData>(json);
            }
            else
            {
                _data = new GameData();
            }
        }
        public void Save()
        {
            string json = JsonUtility.ToJson(Data);
            File.WriteAllText(FilePath, json);
        }
    }
}
