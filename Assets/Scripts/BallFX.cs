using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class BallFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem cutingWind;
    [SerializeField] private ParticleSystem Ball;
    [SerializeField] private Gradient grColor;
    [SerializeField] private Light _pointLigth;
    [SerializeField] private AudioClip woshBall;
    [SerializeField] private Transform bat, bat_ueq;
    public Transform Target;

    [SerializeField] private AnimationCurve controlTamaño;
    
    
    private ParticleSystem.MainModule cutingWindMain;
    private ParticleSystem.MainModule BallMain;
    private ParticleSystem.ShapeModule shapeWInd;
    private ParticleSystem.EmissionModule emissionWind;
    private ParticleSystem.Burst _burstWind;
    private TrailRenderer trBall;
    private AudioSource _audioSource;
    

    
    
    private float t = 0;
    [SerializeField] private float duration;
    private bool playingFX;
    
    

    private void Awake()
    {

        cutingWind = GetComponentInChildren<ParticleSystem>();
        _audioSource = GetComponent<AudioSource>();
        //Ball = GetComponentInChildren<ParticleSystem>();
        _pointLigth = GetComponentInChildren<Light>();
        trBall = GetComponentInChildren<TrailRenderer>();
        cutingWindMain = cutingWind.main;
        BallMain = Ball.main;
        shapeWInd = cutingWind.shape;
        emissionWind = cutingWind.emission;
        _burstWind = emissionWind.GetBurst(1);

        cutingWindMain.startColor = grColor;
        BallMain.startColor = grColor;
        trBall.colorGradient = grColor;
        _pointLigth.intensity = 0;
        _audioSource.loop = true;


    }

    void Start()
    {
        
    }
 
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            ModificacionesFX();
        }

        if (playingFX)
        {
            if (t > duration)
            {
                playingFX = false;

            }
            t += Time.deltaTime;
            
            Vector3 dir = (-transform.position + Target.position).normalized;
            transform.Translate(dir*Time.deltaTime);

            
        }
       
      
        if (playingFX==false)
        {
            _pointLigth.intensity = 0;
            cutingWind.Stop();
            Ball.Stop();
            _audioSource.Stop();
            t = 0;
            bat.position = bat_ueq.position;
            bat.rotation = bat_ueq.rotation;
            
        }
        
     
    }

    void ModificacionesFX()
    {
        if (!Ball.isPlaying && !cutingWind.isPlaying)
        {
            BallMain.duration = duration;
            cutingWindMain.duration = duration;
            _audioSource.clip = woshBall;
            
        
            BallMain.startSize = new ParticleSystem.MinMaxCurve(0.3f,controlTamaño);
            shapeWInd.radius = controlTamaño.Evaluate(t / duration)*0.3f;
            //shapeWInd.radius = controlTamaño.Evaluate(t / duration);
            emissionWind.rateOverTime=new ParticleSystem.MinMaxCurve(250f,controlTamaño);
            trBall.widthMultiplier = controlTamaño.Evaluate(t / duration)+1;
            _pointLigth.intensity = controlTamaño.Evaluate(t / duration)+0.20f;
            _pointLigth.color = grColor.Evaluate(t / duration);
            _audioSource.volume = controlTamaño.Evaluate(t / duration)+0.5f;

            //_burstWind.cycleCount = 0;
           // emissionWind.SetBurst(0,new ParticleSystem.Burst(0f,(controlTamaño.Evaluate(t/duration)*200)+20));


            //_burstWind.count = new ParticleSystem.MinMaxCurve(2f,controlTamaño);

            playingFX = true;
            _audioSource.Play();
            Ball.Play();
            cutingWind.Play();

           


        }
        
    }

    
    
}
