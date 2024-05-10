using System;
using System.Text;
using Infrastructure.Services.DynamicData;
using UnityEngine;

namespace Other
{
    public static class Extensions
    {
        public static T ToDeserialized<T>(this string json) => JsonUtility.FromJson<T>(json);

        public static string ToJson(this object obj) => JsonUtility.ToJson(obj);
        public static int RoundToNearest(this int number, int divisor)
        {
            if (divisor == 0)
            {
                Debug.LogError("Divisor cannot be zero.");
                return number;
            }

            var result = Mathf.RoundToInt((float)number / divisor) * divisor;
            return result;
        }

        public static byte[] ToByteArray(this PlayerProgress playerProgress)
        {
            var playerProgressJson = JsonUtility.ToJson(playerProgress);
            
            var playerData = Encoding.UTF8.GetBytes(playerProgressJson);
            
            return playerData;
        }
        
        public static bool TryParseByteArrayToPlayerProgress(this byte[] arrBytes)
        {
            if (arrBytes == null || arrBytes.Length == 0)
            {
                Debug.LogError("Byte array is null or empty.");
                return false;
            }

            try
            {
                var playerProgressJson = Encoding.UTF8.GetString(arrBytes);
                var deserializedObject = JsonUtility.FromJson<PlayerProgress>(playerProgressJson);

                return deserializedObject != null;
            }
            catch (Exception ex)
            {
                Debug.LogError("Failed to deserialize PlayerProgress: " + ex.Message);
                return false;
            }
        }

        
        public static PlayerProgress ByteArrayToPlayerProgress(this byte[] arrBytes)
        {
            if (arrBytes == null || arrBytes.Length == 0)
            {
                Debug.LogError("Byte array is null or empty.");
                return null;
            }

            try
            {
                var playerProgressJson = Encoding.UTF8.GetString(arrBytes);
                
                return JsonUtility.FromJson<PlayerProgress>(playerProgressJson);
            }
            catch (Exception ex)
            {
                Debug.LogError("Failed to deserialize PlayerProgress: " + ex.Message);
                return null;
            }
        }
    }
}