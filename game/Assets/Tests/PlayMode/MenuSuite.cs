using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Tests
{

    public class MenuSuite : InputTestFixture
    {
        private Mouse _mouse;

        public override void Setup()
        {
            base.Setup();
            SceneManager.LoadScene("Scenes/Menu");
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
        public IEnumerator TestGameStart()
        {
            GameObject startButton = GameObject.Find("Canvas/Panel/StartButton");
            string sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("Menu"));

            ClickUI(startButton);
            // Use yield to skip a frame.
            yield return new WaitForSeconds(2f);

            sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("Game"));
        }

        [UnityTest]
        public IEnumerator TestGameQuit()
        {
            GameObject quitButton = GameObject.Find("Canvas/Panel/QuitButton");
            Assert.NotNull(quitButton);

            yield return null;
        }
    }
}
