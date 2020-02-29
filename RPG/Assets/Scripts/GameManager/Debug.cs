using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Tamana
{
    public static class Debug
    {
        public enum LogType
        {
            Message,
            Warning,
            Error,
            ForceQuit,
        }

        private static string[] _newLine;
        private static string[] NewLine
        {
            get
            {
                if(_newLine == null)
                {
                    _newLine = new string[] { System.Environment.NewLine };
                }

                return _newLine;
            }
        }

        public static void Log(object message, LogType logType = LogType.Message)
        {
            string classColor = "<color=#093D00>";
            string fromOpenColor = "<color=black>";
            string fromCloseColor = "</color>";
            switch (logType)
            {
                case LogType.Message:
                    fromOpenColor = "<color=#00318E>";
                    break;
                case LogType.Warning:
                    fromOpenColor = "<color=#A86D00>";
                    break;
                case LogType.Error:
                    fromOpenColor = "<color=#8A092A>";
                    break;
                case LogType.ForceQuit:
                    fromOpenColor = "<color=#8A092A>";
                    break;
            }

            var stackTrace = new System.Diagnostics.StackTrace().ToString().Split(NewLine, System.StringSplitOptions.None);
            var methodName = new StringBuilder(stackTrace[1]);
            methodName.Remove(0, methodName.ToString().IndexOf('.') + 1);
            var idx = methodName.ToString().IndexOf('(');
            methodName.Remove(idx - 1, methodName.Length - idx);
            methodName.Insert(0, classColor);
            methodName.Insert(methodName.ToString().IndexOf('.'), fromCloseColor);


            UnityEngine.Debug.Log($"<b>{fromOpenColor}{methodName.ToString()}⇒ {fromCloseColor}</b>{message}");

            if(Application.isPlaying == true && logType == LogType.ForceQuit)
            {
                Application.Quit();                
            }
        }
    }
}