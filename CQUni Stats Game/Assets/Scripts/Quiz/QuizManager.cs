﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

 //code was adapted or obtained from youtube videos made by Awesome Tuts, Brackeys and The Game Guy
public class QuizManager : MonoBehaviour
{
    public List<Questions> questions;
    public GameObject[] choices;
    public int currentQuestion;
    public Text questionText;



    public GameObject scorePanel;
    public GameObject quizPanel;
    public Text scoreText;
    private int totalQuestions;
    private int score;


    public GameObject multiChoicePanel;
    public GameObject InputPanel;

    Scene scene;

    // Start is called before the first frame update
    void Start()
    {
        totalQuestions = questions.Count;
        scorePanel.gameObject.SetActive(false);
        quizPanel.gameObject.SetActive(true);    
        NextQuestion();
    }

    public void Correct()
    {
        score= score + 1;
        questions.RemoveAt(currentQuestion);
        NextQuestion();
    }
    public void Wrong()
    {
        
        questions.RemoveAt(currentQuestion);
        NextQuestion();        
    }

     public void QuizOver()
    {
        quizPanel.gameObject.SetActive(false);
        scorePanel.gameObject.SetActive(true);
        scoreText.text =  "You achieved a score of " + score + "/" +   totalQuestions;
    }

    void SetAnswers()
    {
        if(questions[currentQuestion].isMultiChoice)
        {
            multiChoicePanel.gameObject.SetActive(true);
            InputPanel.gameObject.SetActive(false);  
                
            for (int i= 0; i< choices.Length; i++)
            {
                choices[i].GetComponent<Answers>().isCorrect= false;                   
                choices[i].transform.GetChild(0).GetComponent<Text>().text = questions[currentQuestion].answers[i];
                int answerIndex = i+1;
                    
                   
                if(questions[currentQuestion].correctAnswer == answerIndex)
                {
                    choices[i].GetComponent<Answers>().isCorrect = true;
                }

            }

        }
        
        else
        {
            multiChoicePanel.gameObject.SetActive(false);
            InputPanel.gameObject.SetActive(true);
            correctInputAnswer =   questions[currentQuestion].answers[0];
        }
            
    }
    
    // Update is called once per frame
    void NextQuestion()
    {
        if(questions.Count >0)
        {
            currentQuestion = Random.Range(0,questions.Count);
            questionText.text = questions[currentQuestion].question;
            SetAnswers();

        }
        else
        {
            Debug.Log("out of questions");
            //load a new scene/deactiavte the UI
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            QuizOver();
        }
        
        
        
    }

    public InputField input;
    private string correctInputAnswer;

    public void GetInput(string guess)
    {
        
        checkGuess(guess, correctInputAnswer);

    }

    public void checkGuess(string guess, string correctAnswer)
    {
        if(guess == correctAnswer)
        {
            //Debug.Log("you entered " + guess + " and it was right");
            Correct();
        }
        else
        {
            //Debug.Log("you entered " + guess+ " and it was wrong");
            Wrong();
            
        }
        input.text = "";
        

    }

    public void ReloadScene()
    {
      
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   
    }
}