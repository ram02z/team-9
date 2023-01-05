using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Tests.PlayMode
{

    public class GameSuite : InputTestFixture
    {

        private Keyboard _keyboard;
        private Mouse _mouse;

        public override void Setup()
        {
            SceneManager.LoadScene("Scenes/Game");
            _keyboard = InputSystem.AddDevice<Keyboard>();
        }

        [UnityTest]
        public IEnumerator TestQuizCanvas()
        {
            GameObject quizCanvas = GameObject.Find("QuizCanvas");
            // Quiz Canvas is inactive
            Assert.Null(quizCanvas);
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestInteraction()
        {
            GameObject player = GameObject.Find("TmpCharacter");

            GameObject interactionCube = GameObject.Find("Interaction Cube");
            Vector3 cubePosition = interactionCube.transform.position;
            cubePosition.z -= 1;

            player.transform.position = cubePosition;
            PressAndRelease(_keyboard.eKey);
            InputSystem.Update();
            yield return new WaitForEndOfFrame();
            // TODO: test quiz canvas
        }
    }
}
