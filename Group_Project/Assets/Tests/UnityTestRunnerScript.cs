using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class UnityTestRunnerScript
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.

        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("Level");
        }

        [UnityTest]
        public IEnumerator PlayerMoveRight()
        {
            GameObject player = Object.FindObjectOfType<PlayerScript>().gameObject;
            GameMannager.instance.OnEscKeyDown();

            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * 60);

            Vector3 currentPos = player.transform.position;
            for (int loop = 0; loop < 30; loop++)
            {
                player.GetComponent<PlayerScript>().moveRight();
                yield return new WaitForSecondsRealtime(Time.fixedDeltaTime);
            }

            Assert.Greater(player.transform.position.x, currentPos.x);
        }

        [UnityTest]
        public IEnumerator PlayerMoveLeft()
        {
            GameObject player = Object.FindObjectOfType<PlayerScript>().gameObject;
            GameMannager.instance.OnEscKeyDown();

            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * 60);

            Vector3 currentPos = player.transform.position;
            for (int loop = 0; loop < 30; loop++)
            {
                player.GetComponent<PlayerScript>().moveLeft();
                yield return new WaitForSecondsRealtime(Time.fixedDeltaTime);
            }

            Assert.Less(player.transform.position.x, currentPos.x);
        }

        [UnityTest]
        public IEnumerator PlayerFallOnGround()
        {
            GameObject player = Object.FindObjectOfType<PlayerScript>().gameObject;
            player.transform.position += new Vector3(0, 1, 0);
            player.GetComponent<PlayerScript>().groundCheckerObject.GetComponent<GroundCheckerScript>().onGround = false;
            GameMannager.instance.OnEscKeyDown();

            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * 120);

            Assert.IsTrue(player.GetComponent<PlayerScript>().groundCheckerObject.GetComponent<GroundCheckerScript>().onGround);
        }

        [UnityTest]
        public IEnumerator PlayerJump()
        {
            GameObject player = Object.FindObjectOfType<PlayerScript>().gameObject;
            player.GetComponent<PlayerScript>().groundCheckerObject.GetComponent<GroundCheckerScript>().onGround = false;
            GameMannager.instance.OnEscKeyDown();

            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * 60);

            Vector3 currentPos = player.transform.position;
            player.GetComponent<PlayerScript>().onJump();

            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * 30);

            Assert.Greater(player.transform.position.y, currentPos.y);
        }

        [UnityTest]
        public IEnumerator PlayerDeath()
        {
            GameMannager.instance.healthChange(-9999, Vector3.zero, Vector3.zero, Vector3.zero);
            GameMannager.instance.OnEscKeyDown();

            for (int loop = 0; loop < 180; loop++)
            {
                if (GameMannager.instance.gameoverMenu.activeInHierarchy == true)
                {
                    break;
                }
                yield return new WaitForSecondsRealtime(Time.fixedDeltaTime);
            }

            Assert.IsTrue(GameMannager.instance.gameoverMenu.activeSelf);
        }

        [UnityTest]
        public IEnumerator PlayerKnockbacked()
        {
            GameObject player = Object.FindObjectOfType<PlayerScript>().gameObject;
            GameMannager.instance.OnEscKeyDown();

            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * 60);

            Vector3 currentPos = player.transform.position;
            GameMannager.instance.healthChange(-9999, player.transform.position + new Vector3(1, -1, 0), new Vector3(1, 1, 0), new Vector3(1, 1, 0));

            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * 30);

            Assert.AreNotEqual(player.transform.position, currentPos);
        }

        [UnityTest]
        public IEnumerator PlayerWin()
        {

            GameObject player = Object.FindObjectOfType<PlayerScript>().gameObject;
            GameMannager.instance.OnEscKeyDown();

            GameMannager.instance.goalObject.SetActive(true);
            player.transform.position = GameMannager.instance.goalObject.transform.position;

            for (int loop = 0; loop < 45; loop++)
            {
                if (GameMannager.instance.gamewinMenu.activeInHierarchy == true)
                {
                    break;
                }
                yield return new WaitForSecondsRealtime(Time.fixedDeltaTime);
            }

            Assert.IsTrue(GameMannager.instance.gamewinMenu.activeSelf);
        }

        [UnityTest]
        public IEnumerator PlayerPausedGame()
        {
            GameMannager.instance.changeGameState(GameMannager.gameStateList.gamePlay);

            yield return new WaitForSecondsRealtime(0.5f);

            GameMannager.instance.OnEscKeyDown();

            yield return new WaitForSecondsRealtime(0.5f);
            Assert.IsTrue(GameMannager.instance.pauseMenu.activeSelf);
        }

        [UnityTest]
        public IEnumerator ButtonResumeGame()
        {
            GameMannager.instance.changeGameState(GameMannager.gameStateList.pause);

            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * 60);

            GameMannager.instance.OnEscKeyDown();

            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * 60);

            Assert.AreNotEqual(Time.timeScale, 0);
        }

        [UnityTest]
        public IEnumerator ButtonInstructions()
        {
            GameMannager.instance.changeGameState(GameMannager.gameStateList.gamePlay);

            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * 60);

            GameMannager.instance.OnEscKeyDown();
            GameMannager.instance.InstructionsClick();

            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * 60);
            Assert.IsTrue(GameMannager.instance.instructionsMenu.activeSelf);
        }

        [UnityTest]
        public IEnumerator ButtonRestartGame()
        {
            GameMannager.instance.changeGameState(GameMannager.gameStateList.gamePlay);
            GameMannager.instance.health = 100;

            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * 60);

            GameMannager.instance.OnEscKeyDown();
            GameMannager.instance.OnRestartClick();

            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * 60);


            Assert.AreNotEqual(GameMannager.instance.health, 100);
        }

        [UnityTest]
        public IEnumerator EnemyJump()
        {
            GameObject enemy = Object.FindObjectOfType<EnemyScript>().gameObject;
            Vector3 currentPos = enemy.transform.position;

            enemy.GetComponent<EnemyScript>().groundCheckerObject.GetComponent<GroundCheckerScript>().onGround = true;
            GameMannager.instance.OnEscKeyDown();

            for (int loop = 0; loop < enemy.GetComponent<EnemyScript>().jumpCheckT.Count; loop++)
            {
                enemy.GetComponent<EnemyScript>().jumpCheckT[loop] = true;
            }

            enemy.GetComponent<EnemyScript>().onJump();

            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * 30);

            Assert.Greater(enemy.transform.position.y, currentPos.y);
        }

        [UnityTest]
        public IEnumerator EnemyTurn()
        {
            GameObject enemy = Object.FindObjectOfType<EnemyScript>().gameObject;

            enemy.GetComponent<EnemyScript>().groundCheckerObject.GetComponent<GroundCheckerScript>().onGround = true;
            enemy.GetComponent<EnemyScript>().movingRight = true;
            enemy.GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 0);
            GameMannager.instance.OnEscKeyDown();

            for (int loop = 0; loop < enemy.GetComponent<EnemyScript>().jumpCheckT.Count; loop++)
            {
                enemy.GetComponent<EnemyScript>().jumpCheckT[loop] = false;
            }

            enemy.GetComponent<EnemyScript>().onJump();

            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime);

            Assert.IsFalse(enemy.GetComponent<EnemyScript>().movingRight);
        }

        [UnityTest]
        public IEnumerator TurretShot()
        {
            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * 5);

            GameObject temp;
            bool doeshave = false;

            GameMannager.instance.OnEscKeyDown();

            for (int loop = 0; loop < 120; loop++)
            {
                try
                {
                    temp = Object.FindObjectOfType<ProjectileScript>().gameObject;
                    doeshave = true;
                    break;
                }
                catch
                {
                    //
                }
                yield return new WaitForSecondsRealtime(Time.fixedDeltaTime);
            }

            Assert.True(doeshave);
        }

        [UnityTest]
        public IEnumerator PlayerCollectItem()
        {
            float totalItemSpawned = GameMannager.instance.itemLeft;
            Object.FindObjectOfType<CollectableItemScript>().gameObject.transform.position = GameMannager.instance.playerObject.transform.position;
            GameMannager.instance.OnEscKeyDown();

            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime);

            Assert.Less(GameMannager.instance.itemLeft, totalItemSpawned);
        }

    }
}
