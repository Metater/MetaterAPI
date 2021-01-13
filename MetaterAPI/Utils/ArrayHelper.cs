using System;
using System.Collections.Generic;
using System.Text;

namespace MetaterAPI.Utils
{
    public static class ArrayHelper
    {
        public static string[] ChopEnds(string[] array, int beginningDepth, int endDepth)
        {
            List<string> arrayAsList = new List<string>(array);
            arrayAsList.RemoveRange(0, beginningDepth);
            arrayAsList.RemoveRange(arrayAsList.Count - endDepth, endDepth);
            return arrayAsList.ToArray();
        }
    }
}
