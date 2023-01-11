using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class OfflineSuite : InputTestFixture
    {
        private Mouse _mouse;

        [SetUp]
        public override void Setup()
        {
            SceneManager.LoadScene("Scenes/Offline", LoadSceneMode.Single);
            base.Setup();
            _mouse = InputSystem.AddDevice<Mouse>();
        }

        private void ClickUI(GameObject gameObject)
        {
            Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            Vector3 screenPos = camera.WorldToScreenPoint(gameObject.transform.position);
            Set(_mouse.position, screenPos);
            Click(_mouse.leftButton);
        }

        [UnityTest]
        public IEnumerator TestBackButton()
        {
            GameObject backButton = GameObject.Find("Canvas/Panel/BackButton");
            string sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("Offline"));

            ClickUI(backButton);
            // Use yield to skip a frame.
            yield return new WaitForSeconds(2f);

            sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("Menu"));
        }

        [TearDown]
        public override void TearDown()
        {
            base.TearDown();
        }
    }
}
