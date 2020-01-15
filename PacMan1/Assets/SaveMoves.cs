using System.Collections.Generic;
using System.IO;

public class SaveMoves
{
    private int score;

    public SaveMoves(int score,List<int> moves)
    {
        this.score = score;
        if (score > 100)
        {
            int[] a = moves.ToArray();
            string b="";
            for(int i = 0; i < a.Length;i++)
            {
                b += a[i].ToString();
            }
            StreamWriter file = new StreamWriter(@"C:\Users\Austin\Documents\GitHub\PacMan\PacMan1\moves.txt",true);
            file.WriteLine(b+","+score.ToString());
            file.Close();
        }

    }
}