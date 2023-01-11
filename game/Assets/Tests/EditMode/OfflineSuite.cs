using kcp2k;
using Mirror;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace Tests.EditMode
{
    public class OfflineSuite
    {
        [SetUp]
        public void Setup()
        {
            EditorSceneManager.OpenScene($"{Application.dataPath}/Scenes/Offline.unity");
        }

        [Test]
        public void TestMainCamera()
        {
            Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            Assert.NotNull(mainCamera);
            Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            Assert.AreEqual(canvas.renderMode, RenderMode.ScreenSpaceCamera);
            Assert.AreEqual(canvas.worldCamera,mainCamera);
        }

        [Test]
        public void TestMenuButtons()
        {
            GameObject backButton = GameObject.Find("Canvas/Panel/BackButton");
            Assert.NotNull(backButton);
        }

        [Test]
        public void TestEventSystem()
        {
            GameObject eventSystem = GameObject.Find("EventSystem");
            Assert.NotNull(eventSystem);
            var eventSystemScript = eventSystem.GetComponent<EventSystem>();
            Assert.NotNull(eventSystemScript);
            var inputSystemScript = eventSystem.GetComponent<InputSystemUIInputModule>();
            Assert.NotNull(inputSystemScript);
        }

        [Test]
        public void TestRoomManager()
        {
            GameObject roomManager = GameObject.Find("RoomManager");
            Assert.NotNull(roomManager);
            var networkManagerHUD = roomManager.GetComponent<NetworkManagerHUD>();
            Assert.NotNull(networkManagerHUD);
            var kcpTransport = roomManager.GetComponent<KcpTransport>();
            Assert.NotNull(kcpTransport);
            Assert.AreEqual(kcpTransport.Port, 7777);
            var networkRoomManager = roomManager.GetComponent<NetworkRoomManager>();
            Assert.NotNull(networkRoomManager);
            Assert.AreEqual(networkRoomManager.minPlayers, 2);
            Assert.AreEqual(networkRoomManager.maxConnections, 2);
            Assert.AreEqual(networkRoomManager.transport, kcpTransport);
            Assert.AreEqual(networkRoomManager.showRoomGUI, true);
            var player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/TmpCharacter.prefab");
            Assert.AreEqual(networkRoomManager.playerPrefab, player);
            var networkRoomPlayer = AssetDatabase.LoadAssetAtPath<NetworkRoomPlayer>("Assets/Prefabs/RoomPlayer.prefab");
            Assert.AreEqual(networkRoomManager.roomPlayerPrefab, networkRoomPlayer);
            Assert.AreEqual(networkRoomManager.offlineScene, "Assets/Scenes/Offline.unity");
            Assert.AreEqual(networkRoomManager.onlineScene, "Assets/Scenes/Room.unity");
            Assert.AreEqual(networkRoomManager.RoomScene, "Assets/Scenes/Room.unity");
            Assert.AreEqual(networkRoomManager.GameplayScene, "Assets/Scenes/Game.unity");
        }
    }
}
