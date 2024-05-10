using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Infrastructure.Services.Progress.Progress;
using Other;
using UnityEngine;

namespace Infrastructure.Services.Progress.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        public SaveLoadService(IProgressService progressService)
        {
            _progressService = progressService;

            _saveFilePath = Path.Combine(Application.persistentDataPath, "playerdata.data");
        }

        private readonly IProgressService _progressService;

        private string _saveFilePath;
        private string _encryptionKey => "kljsdkkdlo4454GG";

        public void SaveProgress()
        {
            Debug.Log("Save progress");

            var jsonData = _progressService.PlayerProgress.ToJson();

            Debug.Log(jsonData);

            var encryptedData = EncryptData(jsonData);

            File.WriteAllText(_saveFilePath, encryptedData);
        }

        public PlayerProgress LoadProgress()
        {
            if (!File.Exists(_saveFilePath))
            {
                return null;
            }

            Debug.Log("Load progress");

            var encryptedData = File.ReadAllText(_saveFilePath);

            var decryptedData = DecryptData(encryptedData);

            return decryptedData.ToDeserialized<PlayerProgress>();
        }

        private string EncryptData(string data)
        {
            var keyBytes = Encoding.UTF8.GetBytes(_encryptionKey);
            using var aesAlg = Aes.Create();
            aesAlg.Key = keyBytes;
            aesAlg.GenerateIV();

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using (var swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(data);
                }
            }

            var encryptedBytes = msEncrypt.ToArray();
            var combinedBytes = new byte[aesAlg.IV.Length + encryptedBytes.Length];
            Array.Copy(aesAlg.IV, combinedBytes, aesAlg.IV.Length);
            Array.Copy(encryptedBytes, 0, combinedBytes, aesAlg.IV.Length, encryptedBytes.Length);

            return Convert.ToBase64String(combinedBytes);
        }

        private string DecryptData(string encryptedData)
        {
            var combinedBytes = Convert.FromBase64String(encryptedData);
            var keyBytes = Encoding.UTF8.GetBytes(_encryptionKey);

            using var aesAlg = Aes.Create();
            var iv = new byte[aesAlg.IV.Length];
            var encryptedBytes = new byte[combinedBytes.Length - aesAlg.IV.Length];
            Array.Copy(combinedBytes, iv, iv.Length);
            Array.Copy(combinedBytes, iv.Length, encryptedBytes, 0, encryptedBytes.Length);

            aesAlg.Key = keyBytes;
            aesAlg.IV = iv;

            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using var msDecrypt = new MemoryStream(encryptedBytes);
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);

            return srDecrypt.ReadToEnd();
        }
    }
}