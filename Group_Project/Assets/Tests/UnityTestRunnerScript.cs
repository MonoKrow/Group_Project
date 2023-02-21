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

            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
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
            GameObject player = Object.FindObjectOfType<PlayerScript>().gameObject;
            player.GetComponent<PlayerScript>().onDeath();

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
    }
}
