using System.Collections.Generic;
using System.Threading;
using UniRx;

namespace CW.Scripts
{
    public static class SpeechDialog
    {
        public static IObservable<DialogText> PhoneInteraction()
        {
            List<DialogText> diag = new List<DialogText>
            {
                new DialogText("Hello?", 2.0f),
                new DialogText("Oh, hi.", 2.0f), 
                new DialogText("No, I'm just ... cleaning.", 2.0f), 
                new DialogText("Okay, bye.", 2.0f)
            };

            return diag.ToObservable();
        }
    }

    public struct DialogText
    {
        public string Text;
        public float Duration;

        public DialogText(string text, float duration)
        {
            Text = text;
            Duration = duration;
        }
    }
}