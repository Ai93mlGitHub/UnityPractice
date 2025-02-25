using System.IO;
using UnityEngine;

namespace Assets.Develop.CommonServices.DataManagment
{
    public class LocalDataRepository : IDataRepository
    {
        private const string SaveFileExtension = "json";

        private string _folderPath => Application.persistentDataPath;

        public bool Exists(string key) => File.Exists(FullPathFor(key));

        public string Read(string key) => File.ReadAllText(FullPathFor(key));

        public void Remove(string key) => File.Delete(FullPathFor(key));

        public void Write(string key, string serializedData) 
            => File.WriteAllText(FullPathFor(key), serializedData);

        private string FullPathFor(string key) 
            => Path.Combine(_folderPath, key) + "." + SaveFileExtension;
    }
}