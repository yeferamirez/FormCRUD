﻿namespace Form.UnitTest.Utils
{
    public static class JsonLoaderUtil
    {
        public static string LoadJsonFile(string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
