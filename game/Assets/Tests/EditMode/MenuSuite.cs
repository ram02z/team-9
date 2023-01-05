using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.TestTools;

namespace Tests.EditMode
{
    public class MenuSuite
    {
        [SetUp]
        public void Setup()
        {
            EditorSceneManager.OpenScene($"{Application.dataPath}/Scenes/Menu.unity");
        }

        [Test]
        public void TestMainCamera()
        {
            Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            Assert.NotNull(mainCamera);
        }

        [Test]
        public void TestMenuButtons()
        {
            GameObject startButton = GameObject.Find("Canvas/Panel/StartButton");
            Assert.NotNull(startButton);
            GameObject quitButton = GameObject.Find("Canvas/Panel/QuitButton");
            Assert.NotNull(quitButton);
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
