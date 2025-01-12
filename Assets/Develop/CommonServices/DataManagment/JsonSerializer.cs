using Newtonsoft.Json;
using System;

namespace Assets.Develop.CommonServices.DataManagment
{
    public class JsonSerializer : IDataSerializer
    {
        public TData Deserialize<TData>(string serializedData)
        {
            try
            {
                UnityEngine.Debug.Log("Десериализация прошла");
                return JsonConvert.DeserializeObject<TData>(serializedData, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
                });
            }
            catch (JsonException ex)
            {
                // Логируем ошибку десериализации
                UnityEngine.Debug.LogError($"Error during JSON deserialization: {ex.Message}\nData: {serializedData}");
                throw;
            }
            catch (Exception ex)
            {
                // Логируем другие возможные ошибки
                UnityEngine.Debug.LogError($"Unexpected error during deserialization: {ex.Message}");
                throw;
            }
        }

        public string Serialize<TData>(TData data)
        {
            try
            {
                UnityEngine.Debug.Log("Сериализация прошла");
                return JsonConvert.SerializeObject(data, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Auto,
                    TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
                });
            }
            catch (JsonException ex)
            {
                // Логируем ошибку сериализации
                UnityEngine.Debug.LogError($"Error during JSON serialization: {ex.Message}\nObject: {data}");
                throw;
            }
            catch (Exception ex)
            {
                // Логируем другие возможные ошибки
                UnityEngine.Debug.LogError($"Unexpected error during serialization: {ex.Message}");
                throw;
            }
        }
    }
}
