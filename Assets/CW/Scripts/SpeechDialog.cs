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
                new DialogText("Hello?", 1.0f),
                new DialogText("Oh, hi.", 1.0f),
                new DialogText("No, I'm just ... cleaning.", 1.0f),
                new DialogText("Okay, bye.", 1.0f)
            };

            return diag.ToObservable();
        }
    }

    public class DialogText
    {
        public string Text;
        public float Duration;

        public DialogText(string text, float duration)
        {
            Text = text;
            Duration = duration;
        }
    }

    public class EmptyDialogText : DialogText
    {
        public EmptyDialogText(string text, float duration) : base(text, duration)
        {
        }
    }
}