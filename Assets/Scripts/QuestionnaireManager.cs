using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestionnaireManager : MonoBehaviour {
    // Public references to UI elements in Unity Editor
    public Text questionText;
    public Button trueButton;
    public Button falseButton;
    public GameObject feedbackPanel;
    public Text feedbackText;
    public GameObject certificatePanel;
    public Text certificateText; // Reference to the certificate text
    public GameObject congratulationPanel; // Reference to the congratulation panel
    public Text congratulationText; // Reference to the congratulation text
    public GameObject questionnairePanel; // Reference to the panel containing questions
    public Button goToQuestionnaireButton; // Reference to the "Go to Questionnaire" button
    public Button retryButton; // Reference to the "Retry" button

    // Array to hold questions
    private Question[] questions;
    private int currentQuestionIndex = 0;

    void Start() {
        // Define your questions and answers
        questions = new Question[] {
            new Question(": Is wearing gloves necessary when handling items in a hardening oven?", true),
            new Question(": Is it safe to wear synthetic clothing near a hardening oven?", false),
            new Question(": Should ventilation be used when operating a hardening oven?", true),
            new Question(": Is it important to follow safety protocols when operating a hardening oven?", true),
            new Question(": Can you leave a hardening oven unattended while it is in operation?", false),
            new Question(": Is it necessary to check the temperature settings before using the hardening oven?", true),
            new Question(": Should you avoid using water to clean the inside of a hardening oven?", true)
        };

        // Ensure questionnaire and feedback panels are hidden initially
        questionnairePanel.SetActive(false);
        feedbackPanel.SetActive(false);
        certificatePanel.SetActive(false);
        congratulationPanel.SetActive(false);
        retryButton.gameObject.SetActive(false); // Hide the retry button initially

        // Assign click event to the "Go to Questionnaire" button
        goToQuestionnaireButton.onClick.AddListener(StartQuestionnaire);
        retryButton.onClick.AddListener(RetryQuestion); // Assign event to retry button

        // Assign click events to true and false buttons
        trueButton.onClick.AddListener(SubmitTrue);
        falseButton.onClick.AddListener(SubmitFalse);
    }

    void StartQuestionnaire() {
        // Display the first question and show the questionnaire panel
        currentQuestionIndex = 0; // Reset the question index
        DisplayQuestion(questions[currentQuestionIndex]);
        questionnairePanel.SetActive(true);
        goToQuestionnaireButton.gameObject.SetActive(false); // Hide the button
        feedbackPanel.SetActive(false); // Hide feedback panel
        retryButton.gameObject.SetActive(false); // Hide retry button
    }

    public void SubmitTrue() {
        SubmitAnswer(true);
    }

    public void SubmitFalse() {
        SubmitAnswer(false);
    }

    public void SubmitAnswer(bool playerAnswer) {
        bool correctAnswer = questions[currentQuestionIndex].correctAnswer;
        Debug.Log("Player Answer: " + playerAnswer);
        Debug.Log("Correct Answer: " + correctAnswer);

        if (playerAnswer == correctAnswer) {
            // Correct answer
            ShowFeedback("Your answer is correct", Color.green);
            StartCoroutine(HideFeedbackAfterDelay(true));
        } else {
            // Wrong answer
            ShowFeedback("Oops! You entered a wrong answer", Color.red);
            StartCoroutine(HideFeedbackAfterDelay(false));
        }
    }

    IEnumerator HideFeedbackAfterDelay(bool isCorrect) {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        feedbackPanel.SetActive(false); // Hide feedback panel

        if (isCorrect) {
            NextQuestion();
        } else {
            retryButton.gameObject.SetActive(true); // Show retry button after feedback
        }
    }

    void NextQuestion() {
        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Length) {
            // Display next question
            DisplayQuestion(questions[currentQuestionIndex]);
        } else {
            // All questions answered correctly, show certificate and congratulation message
            ShowCertificate();
            ShowCongratulation();
        }
    }

    void DisplayQuestion(Question question) {
        // Update UI to display the question and options
        questionText.text = question.questionText;
        questionText.gameObject.SetActive(true); // Ensure question text is visible
        feedbackPanel.SetActive(false); // Hide feedback panel
        retryButton.gameObject.SetActive(false); // Hide retry button
        Debug.Log("Displaying question: " + question.questionText);
    }

    void ShowFeedback(string message, Color color) {
        // Update UI to display feedback message
        feedbackText.text = message;
        feedbackText.color = color;
        feedbackPanel.SetActive(true); // Show feedback panel
        questionText.gameObject.SetActive(false); // Hide question text during feedback
        retryButton.gameObject.SetActive(false); // Hide retry button during feedback
    }

    void ShowCertificate() {
        // Display UI panel for certificate with congratulatory message
        certificateText.text = "Congratulations!! All answers you entered are correct.";
        certificatePanel.SetActive(true);
        questionnairePanel.SetActive(false); // Hide the questionnaire panel
        Debug.Log("All questions answered correctly. Showing certificate.");
    }

    void ShowCongratulation() {
        // Display congratulation message
        congratulationText.text = "Congratulations! You have completed the questionnaire successfully.";
        congratulationPanel.SetActive(true);
    }

    void RetryQuestion() {
        // Retry the current question
        DisplayQuestion(questions[currentQuestionIndex]);
        feedbackPanel.SetActive(false); // Hide feedback panel
        retryButton.gameObject.SetActive(false); // Hide retry button
    }

    // Definition of the Question class
    private class Question {
        public string questionText;
        public bool correctAnswer;

        public Question(string questionText, bool correctAnswer) {
            this.questionText = questionText;
            this.correctAnswer = correctAnswer;
        }
    }
}
