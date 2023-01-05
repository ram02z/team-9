using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;


namespace Tests.EditMode
{
    public class GameSuite
    {
        [SetUp]
        public void Setup()
        {
            EditorSceneManager.OpenScene($"{Application.dataPath}/Scenes/Game.unity");
        }

        [Test]
        public void TestMainCamera()
        {
            Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            Assert.NotNull(mainCamera);
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
        public void TestQuizManager()
        {
            GameObject quizManager = GameObject.Find("QuizManager");
            Assert.NotNull(quizManager);
            var script = quizManager.GetComponent<QuizManager>();
            Assert.NotNull(script);
        }

    }
}
