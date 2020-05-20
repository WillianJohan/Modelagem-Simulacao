using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Controller : MonoBehaviour
{
    //componentes
    public GameObject cellPrefab;
    private CameraController camera;
    private Grid grid = null;
    
    //Variaveis de controle
    private int generationCount = 0;
    private bool isRunning = false;
    private bool inSimulation = false;

    //TIMER
    private float waitTime = .3f;
    private float timer;
    
    [Header("[UI Components] Slider")]
    public Slider slider_XGridSize;
    public Slider slider_YGridSize;
    public Slider slider_Velocity;

    [Header("[UI Components] Text")]
    public Text slider_Xtext;
    public Text slider_Ytext;
    public Text playPause_Text;
    public Text generation_Text;

    [Header("[UI Components] Buttons")]
    public Button nextGen_Button;
    public Button playPause_Button;


    private void Start()
    {
        //inicializaçao das variáveis
        grid = new Grid();
        camera = FindObjectOfType<CameraController>();
        inSimulation = false;
        playPause_Button.interactable = false;
        nextGen_Button.interactable = false;
        generationCount = 0;

        //Setando valores em tela
        updateGenerationText();
        updateSliderValueXText();
        updateSliderValueYText();
        playPause(false);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (isRunning && (timer > waitTime))
        {
            nextGen();
            timer = Time.deltaTime - waitTime;
        }
    }

    //MÉTODOS DE CONTROLE ==========================================================================
    public void newSimulation()
    {
        if (inSimulation) stopSimulation();
        grid.generateNewGrid((int)slider_XGridSize.value, (int)slider_YGridSize.value, cellPrefab);
        grid.initializeRandomPos();
        startSimulation();

        //calcula o ajuste do zoom da camera
        float newZoomCamera = (grid.x_Length > grid.y_Length) ? grid.x_Length : grid.y_Length + 3;
        newZoomCamera = Mathf.Clamp(newZoomCamera * .5f, camera.minOrtographicSize, camera.maxOrtographicSize);
        camera.ajustarZoom(newZoomCamera);
    }

    public void startSimulation()
    {
        inSimulation = true;
        playPause(false);
        playPause_Button.interactable = true;
        nextGen_Button.interactable = true;
        generationCount = 0;
        updateGenerationText();
    }

    public void stopSimulation()
    {
        grid.destroyCells();
        inSimulation = false;
        playPause(false);
        playPause_Button.interactable = false;
        nextGen_Button.interactable = false;
    }

    public void nextGen()
    {
        if (inSimulation)
        {
            grid.runNextGeneration();
            generationCount++;
            updateGenerationText();
        }
    }

    public void changeVelocity()
    {
        waitTime = slider_Velocity.value;
    }

    public void playPause()
    {
        isRunning = !isRunning;
        
        if (!isRunning) playPause_Text.text = "PLAY";
        else if (isRunning) playPause_Text.text = "STOP";
        nextGen_Button.interactable = !isRunning;
    }

    public void playPause(bool value)
    {
        isRunning = value;
        
        if (!isRunning) playPause_Text.text = "PLAY";
        else if (isRunning) playPause_Text.text = "STOP";
    }

    //SETTER DE COMPONENTES =========================================================================
    public void updateGenerationText()
    {
        generation_Text.text = "GEN: " + generationCount;
    }

    public void updateSliderValueXText()
    {
        slider_Xtext.text = slider_XGridSize.value.ToString();
    }

    public void updateSliderValueYText()
    {
        slider_Ytext.text = slider_YGridSize.value.ToString();
    }
}
