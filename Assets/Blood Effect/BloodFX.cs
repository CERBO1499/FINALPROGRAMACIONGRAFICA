using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BloodFX : MonoBehaviour
{
    [SerializeField] ParticleSystem fountain;
    [SerializeField] ParticleSystem drops;
    [SerializeField] ParticleSystem subEmitter;
    ParticleSystem.MainModule fountainMain;
    ParticleSystem.MainModule dropsMain;
    ParticleSystem.MainModule subEmitterMain;
    ParticleSystem.EmissionModule dropsEmission;
    ParticleSystem.CollisionModule dropCollision;
    private AudioSource audioS;
    public AudioClip soundEffect;
    public float systemDuration;
    public float poolDuration;
    public LayerMask dropCollisionWith;
    public Color effectColor;
    [SerializeField] AnimationCurve systemControl;
    private bool isPlaying;
    private float counter;

    private void Awake()
    {
        fountainMain = fountain.main;
        dropsMain = drops.main;
        subEmitterMain = subEmitter.main;
        dropsEmission = drops.emission;
        dropCollision = drops.collision;
        audioS = GetComponent<AudioSource>();
        audioS.loop = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Debug.isDebugBuild) { OnEffectCall(); }
        if (isPlaying)
        {
            if (counter > systemDuration)
            {
                isPlaying = false;
                transform.parent = null;
                return;
            }
            audioS.volume = systemControl.Evaluate(counter / systemDuration);
            counter += Time.deltaTime;
        }
        else if (audioS.volume > 0)
        {
            audioS.volume -= Time.deltaTime;
            if(audioS.volume <= 0)
            {
                audioS.Stop();
            }
        }
    }

    private void OnEffectCall() //se puede modificar para llamarse por eventos
    {
        if (!fountain.isPlaying && !drops.isPlaying)
        {
            //primera parte aplica los efectos ingresados en las variables
            audioS.clip = soundEffect;
            Color newColor = effectColor;
            fountainMain.startColor = newColor;
            dropsMain.startColor = newColor;
            subEmitterMain.startColor = newColor;
            fountainMain.duration = systemDuration;
            dropsMain.duration = systemDuration + 0.5f;
            //las particulas que quedan en el suelo duran mas que el sistema inicial completo
            subEmitterMain.startLifetime = systemDuration + 0.5f + poolDuration; 
            dropCollision.collidesWith = dropCollisionWith;

            //segunda parte aplica la curva de control a los diferentes sistemas
            fountainMain.startSpeed = new ParticleSystem.MinMaxCurve(14, systemControl);
            fountainMain.startSize = new ParticleSystem.MinMaxCurve(0.3f, systemControl);
            dropsEmission.rateOverTime = new ParticleSystem.MinMaxCurve(10, systemControl);

            //finalmente reproduce el efecto
            fountain.Play();
            isPlaying = true;
            counter = 0;
            audioS.Play();
        }
    }

    public void BloodEffectCall()
    {
        OnEffectCall();
    }

    public void BloodEffectCall(Transform effectPosition)
    {
        transform.SetParent(effectPosition, false);
        OnEffectCall();
    }

}
