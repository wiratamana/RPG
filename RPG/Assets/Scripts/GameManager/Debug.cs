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

        public static void Log(string message, LogType logType = LogType.Message)
        {
            string fromOpenColor = "<color=black>";
            string fromCloseColor = "</color>";
            switch (logType)
            {
                case LogType.Message:
                    fromOpenColor = "<color=#1B5417>";
                    break;
                case LogType.Warning:
                    fromOpenColor = "<color=#A86D00>";
                    break;
                case LogType.Error:
                    fromOpenColor = "<color=#DB0891>";
                    break;
                case LogType.ForceQuit:
                    fromOpenColor = "<color=#CB0000>";
                    break;
            }

            var stackTrace = new System.Diagnostics.StackTrace().ToString().Split(NewLine, System.StringSplitOptions.None);
            var methodName = new StringBuilder(stackTrace[1]);
            methodName.Remove(0, methodName.ToString().IndexOf('.') + 1);
            var idx = methodName.ToString().IndexOf(')');
            methodName.Remove(idx + 1, methodName.Length - idx - 1);

            UnityEngine.Debug.Log($"<b>{fromOpenColor}{methodName.ToString()} : {fromCloseColor}</b>{message}");

            if(Application.isPlaying == true && logType == LogType.ForceQuit)
            {
                Application.Quit();
            }
        }
    }
}