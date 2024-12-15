using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Gameplay.Items;
using UnityEngine;
using UnityEngine.Networking;

namespace Gameplay.Services.Items.Networking
{
    [Serializable]
    public sealed class ItemNetworkDto
    {
        [SerializeField] private ItemType _itemType;
        [SerializeField] private string _name;
        [SerializeField] private float _weight;

        public ItemNetworkDto(ItemType itemType, string name, float weight)
        {
            _itemType = itemType;
            _name = name;
            _weight = weight;
        }
    }

    [Serializable]
    public sealed class ItemsNetworkingService : PocoService
    {
        private const string Url = "https://wadahub.manerai.com/api/inventory/status";
        private const string AuthToken = "kPERnYcWAY46xaSy8CEzanosAgsWM84Nx7SKM4QBSqPq6c7StWfGxzhxPfDh8MaP";

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task<string> PostRequest(ItemNetworkDto dataToSend)
        {
            if (dataToSend == null)
                throw new ArgumentNullException(nameof(dataToSend));

            var json = JsonUtility.ToJson(dataToSend);

            using var request = new UnityWebRequest(Url, UnityWebRequest.kHttpVerbPOST);
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();

            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", $"Bearer {AuthToken}");
            request.SendWebRequest();

            while (!request.isDone)
            {
                await Task.Yield();
            }

            if (request.result == UnityWebRequest.Result.Success)
            {
                return request.downloadHandler.text;
            }

            throw new Exception($"Request failed with error: {request.error}, Response Code: {request.responseCode}");
        }
    }
}