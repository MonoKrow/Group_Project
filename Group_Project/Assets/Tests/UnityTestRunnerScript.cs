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

            yield return new WaitForSeconds(Time.fixedDeltaTime * 60);

            Vector3 currentPos = player.transform.position;
            for (int loop = 0; loop < 30; loop++)
            {
                player.GetComponent<PlayerScript>().moveRight();
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }

            Assert.Greater(player.transform.position.x, currentPos.x);
        }

        [UnityTest]
        public IEnumerator PlayerMoveLeft()
        {
            GameObject player = Object.FindObjectOfType<PlayerScript>().gameObject;

            yield return new WaitForSeconds(Time.fixedDeltaTime * 60);

            Vector3 currentPos = player.transform.position;
            for (int loop = 0; loop < 30; loop++)
            {
                player.GetComponent<PlayerScript>().moveLeft();
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }

            Assert.Less(player.transform.position.x, currentPos.x);
        }

        [UnityTest]
        public IEnumerator PlayerFallOnGround()
        {
            GameObject player = Object.FindObjectOfType<PlayerScript>().gameObject;
            player.transform.position += new Vector3(0, 1, 0);
            player.GetComponent<PlayerScript>().groundCheckerObject.GetComponent<GroundCheckerScript>().onGround = false;

            yield return new WaitForSeconds(Time.fixedDeltaTime * 120);

            Assert.IsTrue(player.GetComponent<PlayerScript>().groundCheckerObject.GetComponent<GroundCheckerScript>().onGround);
        }

        [UnityTest]
        public IEnumerator PlayerJump()
        {
            GameObject player = Object.FindObjectOfType<PlayerScript>().gameObject;
            player.GetComponent<PlayerScript>().groundCheckerObject.GetComponent<GroundCheckerScript>().onGround = false;

            yield return new WaitForSeconds(Time.fixedDeltaTime * 60);

            Vector3 currentPos = player.transform.position;
            player.GetComponent<PlayerScript>().onJump();

            yield return new WaitForSeconds(Time.fixedDeltaTime * 30);

            Assert.Greater(player.transform.position.y, currentPos.y);
        }

        [UnityTest]
        public IEnumerator PlayerDeath()
        {
            GameMannager.instance.healthChange(-9999);

            for (int loop = 0; loop < 180; loop++)
            {
                if (GameMannager.instance.gameoverMenu.activeInHierarchy == true)
                {
                    break;
                }
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }

            Assert.IsTrue(GameMannager.instance.gameoverMenu.activeSelf);
        }

        [UnityTest]
        public IEnumerator PlayerKnockbacked()
        {
            GameObject player = Object.FindObjectOfType<PlayerScript>().gameObject;

            yield return new WaitForSeconds(Time.fixedDeltaTime * 60);

            Vector3 currentPos = player.transform.position;
            player.GetComponent<PlayerScript>().onKnockback(player.transform.position + new Vector3(1, -1, 0), new Vector3(1, 1, 0), new Vector3(1, 1, 0));

            yield return new WaitForSeconds(Time.fixedDeltaTime * 30);

            Assert.AreNotEqual(player.transform.position, currentPos);
        }

        [UnityTest]
        public IEnumerator PlayerWin()
        {
            /*
            GameObject player = Object.FindObjectOfType<PlayerScript>().gameObject;

            //teleport to goal spot

            for (int loop = 0; loop < 45; loop++)
            {
                if (GameMannager.instance.gamewinMenu.activeInHierarchy == true)
                {
                    break;
                }
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }

            Assert.IsTrue(GameMannager.instance.gamewinMenu.activeSelf);
            */
            yield return null;
            Assert.Fail("Not Added Yet");
        }

        [UnityTest]
        public IEnumerator PlayerPausedGame()
        {
            /*
            GameMannager.instance.changeGameState(GameMannager.gameStateList.pause);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            Assert.IsTrue(GameMannager.instance.pauseMenu.activeSelf);
            */
            yield return null;
            Assert.Fail("Not Added Yet");
        }

        [UnityTest]
        public IEnumerator ButtonResumeGame()
        {
            /*
            GameMannager.instance.changeGameState(GameMannager.gameStateList.pause);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            Assert.IsTrue(GameMannager.instance.pauseMenu.activeSelf);
            */
            yield return null;
            Assert.Fail("Not Added Yet");
        }

        [UnityTest]
        public IEnumerator ButtonQuitGame()
        {
            /*
            GameMannager.instance.changeGameState(GameMannager.gameStateList.pause);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            Assert.IsTrue(GameMannager.instance.pauseMenu.activeSelf);
            */
            yield return null;
            Assert.Fail("Not Added Yet");
        }


        [UnityTest]
        public IEnumerator ButtonInstructions()
        {
            /*
            GameMannager.instance.changeGameState(GameMannager.gameStateList.pause);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            Assert.IsTrue(GameMannager.instance.pauseMenu.activeSelf);
            */
            yield return null;
            Assert.Fail("Not Added Yet");
        }

        [UnityTest]
        public IEnumerator ButtonRestartGame()
        {
            /*
            GameMannager.instance.changeGameState(GameMannager.gameStateList.pause);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            Assert.IsTrue(GameMannager.instance.pauseMenu.activeSelf);
            */
            yield return null;
            Assert.Fail("Not Added Yet");
        }

        [UnityTest]
        public IEnumerator EnemyJump()
        {
            GameObject enemy = Object.FindObjectOfType<EnemyScript>().gameObject;
            Vector3 currentPos = enemy.transform.position;

            enemy.GetComponent<EnemyScript>().groundCheckerObject.GetComponent<GroundCheckerScript>().onGround = true;

            for (int loop = 0; loop < enemy.GetComponent<EnemyScript>().jumpCheckT.Count; loop++)
            {
                enemy.GetComponent<EnemyScript>().jumpCheckT[loop] = true;
            }

            enemy.GetComponent<EnemyScript>().onJump();

            yield return new WaitForSeconds(Time.fixedDeltaTime * 30);

            Assert.Greater(enemy.transform.position.y, currentPos.y);
        }

        [UnityTest]
        public IEnumerator EnemyTurn()
        {
            GameObject enemy = Object.FindObjectOfType<EnemyScript>().gameObject;

            enemy.GetComponent<EnemyScript>().groundCheckerObject.GetComponent<GroundCheckerScript>().onGround = true;
            enemy.GetComponent<EnemyScript>().movingRight = true;
            enemy.GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 0);

            for (int loop = 0; loop < enemy.GetComponent<EnemyScript>().jumpCheckT.Count; loop++)
            {
                enemy.GetComponent<EnemyScript>().jumpCheckT[loop] = false;
            }

            enemy.GetComponent<EnemyScript>().onJump();

            yield return new WaitForSeconds(Time.fixedDeltaTime);

            Assert.IsFalse(enemy.GetComponent<EnemyScript>().movingRight);
        }

        [UnityTest]
        public IEnumerator SlimeHitPlayer()
        {
            /*
            GameObject enemy = Object.FindObjectOfType<EnemyScript>().gameObject;

            enemy.GetComponent<EnemyScript>().movingRight = true;
            float startingHealth = GameMannager.instance.health;

            for (int loop = 0; loop < 180; loop++)
            {
                if (GameMannager.instance.health != startingHealth)
                {
                    break;
                }
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }

            Assert.Less(GameMannager.instance.health, startingHealth);
            */
            yield return null;
            Assert.Fail("Not Added Yet");
        }

        [UnityTest]
        public IEnumerator LazerHitPlayer()
        {
            yield return null;
            Assert.Fail("Not Added Yet");
        }

        [UnityTest]
        public IEnumerator BeamHitPlayer()
        {
            yield return null;
            Assert.Fail("Not Added Yet");
        }

        [UnityTest]
        public IEnumerator PlayerTouchSpike()
        {
            yield return null;
            Assert.Fail("Not Added Yet");
        }

        [UnityTest]
        public IEnumerator TurretShot()
        {
            yield return null;
            Assert.Fail("Not Added Yet");
        }

        [UnityTest]
        public IEnumerator BeamTurretRotating()
        {
            yield return null;
            Assert.Fail("Not Added Yet");
        }

        [UnityTest]
        public IEnumerator PlayerCollectItem()
        {
            float totalItemSpawned = GameMannager.instance.itemLeft;
            Object.FindObjectOfType<CollectableItemScript>().gameObject.transform.position = GameMannager.instance.playerObject.transform.position;

            yield return new WaitForSeconds(Time.fixedDeltaTime);

            Assert.Less(GameMannager.instance.itemLeft, totalItemSpawned);
        }

    }
}
