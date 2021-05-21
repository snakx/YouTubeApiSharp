using System;
using System.Collections.Generic;
using System.IO;

namespace YouTubeApiSharp
{
	public class Helper
	{
        public static string Folder { get; set; }

        public static String ExtractValue(String Source, String Start, String End)
        {
            int start, end;

            try
            {
                if (Source.Contains(Start) && Source.Contains(End))
                {
                    start = Source.IndexOf(Start, 0) + Start.Length;
                    end = Source.IndexOf(End, start);

                    return Source.Substring(start, end - start);
                }
                else
                    return printZero();
            }
            catch (Exception ex)
            {
                if (Log.getMode())
                    Log.println(Folder, ex.ToString());

                return printZero();
            }
        }

        public static String printZero()
        {
            return " ";
        }

        public static string makeFilenameValid(string file)
        {
            char replacementChar = '_';

            var blacklist = new HashSet<char>(Path.GetInvalidFileNameChars());

            var output = file.ToCharArray();

            for (int i = 0, ln = output.Length; i < ln; i++)
            {
                if (blacklist.Contains(output[i]))
                    output[i] = replacementChar;
            }

            return new string(output);
        }
    }
}
