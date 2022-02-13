using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriterEffect : MonoBehaviour
{
    [SerializeField] private float typeWriterSpeed = 50f;

    public bool IsRunning { get; private set; }

    private readonly List<Punctuation> punctuations = new List<Punctuation>()
    {
        new Punctuation(new HashSet<char>() {'.', '!', '?'}, 0.6f),
        new Punctuation(new HashSet<char>() {',', ';', ':'}, 0.3f),
    };

    private Coroutine typingCoroutine;

    // function which other scripts can get acess from
    public void Run(string textToType, TMP_Text dialogueText)
    {
        typingCoroutine = StartCoroutine(TypeText(textToType, dialogueText));
    }

    public void Stop()
    {
        StopCoroutine(typingCoroutine);
        IsRunning = false;
    }

    private IEnumerator TypeText(string textToType, TMP_Text dialogueText)
    {
        IsRunning = true;
        // makes sures the text box is empty and doesnt have the default text from the editor in it
        dialogueText.text = string.Empty;

        float textTimer = 0;
        int charIndex = 0;

        // iterates through dialogue at a specific speed to get that type writing feel
        while (charIndex < textToType.Length)
        {
            int lastCharIndex = charIndex;

            textTimer += Time.deltaTime * typeWriterSpeed;

            charIndex = Mathf.FloorToInt(textTimer);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);
            
            // checks if punc is in dialogue and see if its apporitate to delay dialogue
            for (int i = lastCharIndex; i < charIndex; i++)
            {
                bool isLast = i >= textToType.Length - 1;

                dialogueText.text = textToType.Substring(0, i + 1);

                if (IsPunctuation(textToType[i], out float waitTime) && !isLast && !IsPunctuation(textToType[i + 1], out _))
                {
                    yield return new WaitForSeconds(waitTime);
                }
            }

            yield return null;
        }
        IsRunning = false;
    }

    // check if there is punctation in the dialogue
    private bool IsPunctuation(char character, out float waitTime)
    {
        foreach(Punctuation punctuationCategory in punctuations)
        {
            if (punctuationCategory.Punctuations.Contains(character))
            {
                waitTime = punctuationCategory.WaitTime;
                return true;
            }
        }
        waitTime = default;
        return false;
    }

    private readonly struct Punctuation
    {
        public readonly HashSet<char> Punctuations;
        public readonly float WaitTime;

        public Punctuation(HashSet<char> punctuations, float waitTime)
        {
            Punctuations = punctuations;
            WaitTime = waitTime;
        }
    }
}
