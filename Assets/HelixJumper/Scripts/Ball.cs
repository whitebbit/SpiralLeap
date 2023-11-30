using System;
using _3._Scripts;
using _3._Scripts.Architecture;
using _3._Scripts.Architecture.Scriptable;
using _3._Scripts.Game;
using _3._Scripts.Player;
using _3._Scripts.UI;
using DG.Tweening;
using Plugins.Audio.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using Random = UnityEngine.Random;

namespace HelixJumper.Scripts
{
    public class Ball : Singleton<Ball>
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private ParticleSystem fire;
        [Space] public Animator BallAnim;
        public Rigidbody BallRB;
        public Bolt SplashFX;
        public SourceAudio Audio;

        [HideInInspector] public bool isFly;
        public static bool IsGameOver;

        private int _streak;
        private bool _setVelocity;
        private static readonly int JumpAnimation = Animator.StringToHash("Jump");
        public SkinHolder skinHolder { get; private set; }
        private HelixPieceCreator _lastHelix;

        protected override void Awake()
        {
            base.Awake();
            skinHolder = new SkinHolder(meshRenderer, meshFilter);
        }

        private void Start()
        {
            isFly = false;
            IsGameOver = false;
            Physics.gravity = new Vector3(0, -20f, 0);
            fire.Stop();
            GetLoad();
        }
        
        private void GetLoad()
        {
            try
            {
                var skin = Configuration.instance.skinsHolder.GetSkin(YandexGame.savesData.currentSkin);
                skinHolder.ChangeSkin(skin);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }

        private void LateUpdate()
        {
            if (!_setVelocity) return;

            Audio.PlayOneShot("hit");
            BallAnim.SetTrigger(JumpAnimation);
            BallRB.velocity = Vector3.up * 8;
            _setVelocity = false;
            isFly = false;
        }

        private void RotateAnimation(Vector3 endValue, float duration = 1f)
        {
            transform.DORotate(endValue, duration);
        }

        private bool Boosted(Collision other)
        {
            if (_streak < 3)
            {
                fire.Stop();
                return false;
            }

            if (other.transform.parent != null)
            {
                if (other.transform.parent.TryGetComponent(out HelixPieceCreator pieceCreator))
                    pieceCreator.DestroyPieces();
            }

            _setVelocity = true;
            _streak = 0;
            fire.Stop();
            CameraShaker.instance.StopShake();
            return true;
        }

        private void Boost()
        {
            _streak += 1;
            if (_streak < 3) return;
            fire.Play();
            CameraShaker.instance.StartShake();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("Fail"))
            {
                if (Boosted(other)) return;
                SetLastHelix(other);
                Fail();
                CameraShaker.instance.StopShake();
            }
            else
            {
                if (other.gameObject.TryGetComponent(out Finish finish))
                {
                    finish.Finishing();
                    fire.Stop();
                    CameraShaker.instance.StopShake();
                }
                else
                    Jump(other);
            }
        }

        private void SetLastHelix(Collision other)
        {
            if (other.transform.parent == null) return;
            if (other.transform.parent.TryGetComponent(out HelixPieceCreator pieceCreator))
                _lastHelix = pieceCreator;
        }

        private void Jump(Collision other)
        {
            if (transform.position.y - other.transform.position.y < -0.15f) return;

            Boosted(other);
            _setVelocity = true;
            var particle = Instantiate(SplashFX, other.transform, false);
            var position = other.transform.position;

            particle.Initialize(skinHolder.skin.Color);
            particle.transform.position = new Vector3(position.x, position.y + 0.01f,
                position.z - 1.5f);
            particle.transform.rotation = Quaternion.Euler(90f, 0, Random.Range(0, 360f));

            _streak = 0;
            var endValue = new Vector3(Random.Range(-360f, 360f), Random.Range(-360f, 360f),
                Random.Range(-360f, 360f));
            RotateAnimation(endValue);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out HelixPieceCreator pieceCreator)) return;
            FallDown(pieceCreator);
        }

        private void FallDown(HelixPieceCreator pieceCreator)
        {
            ScoreManager.instance.AddScore();
            isFly = true;

            Boost();
            if (!pieceCreator.destroyed)
                Audio.PlayOneShot("platform");
            pieceCreator.DestroyPieces();

            RotateAnimation(new Vector3(0, 180, 0), .5f);
        }

        private void Fail()
        {
            IsGameOver = true;
            BallRB.isKinematic = true;
            GameManager.instance.ChangePanel(GameManager.instance.LosePanel);
            AudioManager.instance.PlayOneShot("lose");
            CameraShaker.instance.StopShake();
        }

        public void Continue()
        {
            BallRB.isKinematic = false;
            IsGameOver = false;
            _setVelocity = true;
            _lastHelix?.DestroyPieces();
        }
    }
}