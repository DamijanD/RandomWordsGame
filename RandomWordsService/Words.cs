namespace WSData2
{
    public class Words
    {
        public static readonly Words Instance = new Words();

        private List<string> _wordList = null;

        private void LoadWords()
        {
            var txt = System.IO.File.ReadAllLines(".\\sbsj.txt");
            for (int i = 0; i < txt.Length; i++)
            {
                int pos = txt[i].IndexOf("(");
                if (pos > 1)
                {
                    txt[i] = txt[i].Substring(0, pos-1);
                }
            }
            _wordList = new List<string>(txt);
        }

        public List<string> GetWords(int cnt, int minLen = 0, int maxLen=99)
        {
            if (_wordList == null)
            {
                LoadWords();
            }

            if (_wordList == null)
            {
                return new List<string>();
            }

            var tmp = _wordList.Where(x => x.Length >= minLen && x.Length <= maxLen).ToList();

            Random random = new Random(DateTime.Now.Millisecond);
            List<string> words = new List<string>(cnt);

            for (int i = 0; i < cnt; i++)
            {
                words.Add(tmp[random.Next(0, tmp.Count)]);
            }

            return words.Distinct().ToList();
        }
    }
}
