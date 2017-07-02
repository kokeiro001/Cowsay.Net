using System;
using System.Collections.Generic;
using System.Linq;

namespace Cowsay.Net
{
    class Program
    {
        public static void Main(string[] args)
        {
            // オプションがあろうが全てまとめてセリフとして扱う。
            // TODO: オプションに応じて処理を分岐するようにする
            var content = string.Join("", args).Trim();

            if (string.IsNullOrWhiteSpace(content))
            {
                content = "moo";
            }

            new Cowsay().Say(content);
        }
    }

    class Cowsay
    {
        const int SpeechBalloonWidth = 40;

        string speechContent;
        IList<string> builder;

        public void Say(string speechContent)
        {
            this.speechContent = speechContent;
            builder = new List<string>();

            DrawSpeechBubble();
            DrawCow();

            foreach (var line in builder)
            {
                Console.WriteLine(line);
            }
        }

        void DrawSpeechBubble()
        {
            var lineLength = SpeechBalloonWidth - 4;
            var output = speechContent.SplitInParts(lineLength).ToArray();
            var lines = output.Length;
            var wrapperLineLength = (lines == 1 ? output.First().Length : SpeechBalloonWidth - 4) + 2;

            builder.Add($" {'_'.RepeatChar(wrapperLineLength)}");
            if (lines == 1)
            {
                builder.Add($"< {output.First()} >");
            }
            else
            {
                for (var i = 0; i < lines; i++)
                {
                    char lineStartChar = '|';
                    char lineEndChar = '|';

                    if (i == 0)
                    {
                        lineStartChar = '/';
                        lineEndChar = '\\';
                    }
                    else if (i == lines - 1)
                    {
                        lineStartChar = '\\';
                        lineEndChar = '/';
                    }

                    var neededPadding = SpeechBalloonWidth - 4 - output[i].Length;
                    builder.Add($"{lineStartChar} {output[i]}{' '.RepeatChar(neededPadding)} {lineEndChar}");
                }
            }

            builder.Add($" {'-'.RepeatChar(wrapperLineLength)}");
        }

        void DrawCow()
        {
            var startingLinePaddingNum = builder.First().Length / 4;
            var padding = ' '.RepeatChar(startingLinePaddingNum);

            builder.Add($@"{padding}\   ^__^");
            builder.Add($@"{padding} \  (oo)\_______");
            builder.Add($@"{padding}    (__)\       )\/\");
            builder.Add($@"{padding}        ||----w |");
            builder.Add($@"{padding}        ||     ||");
        }
    }

    static class Extensions
    {
        public static IEnumerable<String> SplitInParts(this String s, int partLength)
        {
            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }

        public static string RepeatChar(this char @char, int count)
        {
            var charArray = new char[count];
            for (var i = 0; i < count; i++)
                charArray[i] = @char;

            return new string(charArray);
        }
    }
}
