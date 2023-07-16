using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MimicSpace
{
    public class Movement : MonoBehaviour
    {
        [Header("Movement Settings")]
        [Tooltip("Body Height from ground")]
        [Range(0.5f, 5f)]
        public float height = 0.8f;

        public float health = 100f;
        public float speed = 5f;
        public float velocityLerpCoef = 4f;

        public GameObject player;
        public float attackDistance = 2f;
        public float chaseDistance = 10f;
        public int damageAmount = 10;

        private NavMeshAgent agent;
        private Mimic myMimic;

        private bool alreadyAttacked = false;
        private bool isDeadCharacter = false;
        private bool damageTriggered = false;
        private bool bossThemeTriggered = false;
        private bool bossThemeSoundsIsSet= false;
        private bool VirusEscapeSceneTriggered = false;

        public float timeBetweenAttacks = 1f;
        public int mimicDamage = 20;

        public float mimicRadius = 0.5f;
        public float damageCoolDown = 0.3f;

        public float explosionDamage = 18f;
        public float MainVirusEscapeSceneTime = 5f;

        Death playerDeathScript;

        [SerializeField]
        DissolveObject dissolveObject;
        

        [SerializeField]
        LayerMask mimicExplosion;

        [SerializeField]
        StairManage stairManage;

        [SerializeField]
        AudioSource angryMusic;

        [SerializeField]
        AudioSource splatClip;

        [SerializeField]
        ParticleSystem deathParticle;

        [SerializeField]
        ParticleSystem damageParticle;

        [SerializeField]
        GameObject MainVirusEscapeCamera;

        [SerializeField]
        GameObject MainVirus;

        [SerializeField]
        Animator mainVirusAnimator;

        [SerializeField]
        BossCutSceneManage bossCutSceneManage;

        [SerializeField]
        GameObject cutsceneManage;
        private void OnDrawGizmos()
        {

            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, mimicRadius);
            
        }

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            myMimic = GetComponent<Mimic>();
            playerDeathScript = player.GetComponent<Death>();
        }

        private void Update()
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (player != null && distance <= chaseDistance)
            {
                agent.SetDestination(player.transform.position);

                if(!bossThemeTriggered)
                {
                    angryMusic.Play();    
                    StartIncreaseVolume();
                    bossThemeTriggered = true;
                }

                if (distance <= attackDistance)
                {
                    if (!isDeadCharacter)
                    {
                        DealDamage(player);
                    }
                }
            }

            if(health < 0f)
            {
                StartCoroutine(dissolveObject.Dissolve());
                attackDistance = 0f;
                agent.SetDestination(transform.position);
                deathParticle.gameObject.SetActive(true);
                cutsceneManage.SetActive(true);

                StartDecreaseVolume();

                Invoke("SetMainVirusEscapeCam", 7f);

                Invoke("CallStairs", MainVirusEscapeSceneTime + 7f);

                Invoke("KillMimic", 2.5f);

                Invoke("StopMusic", 15f);
            }

            bool checkRadius = CheckTriggerSphere(transform.position, mimicRadius, mimicExplosion);

            if(checkRadius && !damageTriggered)
            {
                health -= explosionDamage;

                damageParticle.gameObject.SetActive(true );
                splatClip.PlayOneShot(splatClip.clip);

                damageTriggered = true;

                Invoke("ResetDamageBool", damageCoolDown);
            }

            myMimic.velocity = agent.velocity;
            transform.position = agent.nextPosition + Vector3.up * height;
        }
        private void DealDamage(GameObject target)
        {
            if (!alreadyAttacked && !isDeadCharacter)
            {
                alreadyAttacked = true;

                playerDeathScript.TakeHit(mimicDamage);

                // Debug.Log("Attacking");
                // Reset the attack cooldown

               // CheckPlayerHealth();
                Invoke("ResetAttack", timeBetweenAttacks);
            }
        }

        public void CheckPlayerHealth()
        {
            if(playerDeathScript.health < 0f)
            {
                isDeadCharacter = true;

                DecreaseVolume();
            }
        }
        private void ResetAttack()
        {
            alreadyAttacked = false;
        }

        void ResetDamageBool()
        {
            damageTriggered = false;
        }

        public bool CheckTriggerSphere(Vector3 center, float radius, LayerMask layerMask)
        {
            Collider[] colliders = Physics.OverlapSphere(center, radius, layerMask, QueryTriggerInteraction.Collide);
            return colliders.Length > 0;
        }

        void KillMimic()
        {
            this.gameObject.SetActive(false);
        }

        void StopMusic()
        {
            angryMusic.Stop();
        }

        private IEnumerator DecreaseVolume()
        {
            float startVolume = angryMusic.volume;
            float targetVolume = -0.5f;
            float elapsedTime = 0f;
            float duration = 5f; 

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                angryMusic.volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / duration);
                yield return null;
            }

            angryMusic.Stop();
        }

        private IEnumerator IncreaseVolume()
        {
            float startVolume = angryMusic.volume;
            float targetVolume = 0.5f;
            float elapsedTime = 0f;
            float duration = 5f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                angryMusic.volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / duration);
                yield return null;
            }
        }


        void StartDecreaseVolume()
        {
            StartCoroutine(DecreaseVolume());
        }

        void StartIncreaseVolume()
        {
            StartCoroutine(IncreaseVolume());
        }

        void CallStairs()
        {
            stairManage.CallSetStairs();
        }

        void SetMainVirusEscapeCam()
        {
            if (!VirusEscapeSceneTriggered)
            {
                bossCutSceneManage.onCutSceneBoss = true;

                MainVirus.SetActive(true);
                mainVirusAnimator.SetBool("Enabled", true);

                MainVirusEscapeCamera.SetActive(true);

                Invoke("SetOFFMainVirusEscapeCam", MainVirusEscapeSceneTime);

                VirusEscapeSceneTriggered = true;
            }
        }

        void SetOFFMainVirusEscapeCam()
        {
            bossCutSceneManage.onCutSceneBoss = false;

            MainVirusEscapeCamera.SetActive(false);
            MainVirus.SetActive(false);
        }
    }

}
