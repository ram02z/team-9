using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace Tests.EditMode
{
    public class RoomSuite
    {
        [SetUp]
        public void Setup()
        {
            EditorSceneManager.OpenScene($"{Application.dataPath}/Scenes/Room.unity");
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
    }
}
