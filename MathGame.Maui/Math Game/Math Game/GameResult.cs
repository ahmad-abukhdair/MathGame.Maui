using Math_Game;

public class GameResult
{
    public GameResult(MathOperation operation, int score, int totalQuestions)
    {
        if (!Enum.IsDefined(typeof(MathOperation), operation))
        { throw new ArgumentOutOfRangeException(nameof(operation)); }

        if (totalQuestions <= 0)
        { throw new ArgumentOutOfRangeException(nameof(totalQuestions), "Total questions must be greater than zero."); }
        if (score < 0 || score > totalQuestions) 
        { throw new ArgumentOutOfRangeException(nameof(score), "Score must be between zero and total questions."); }
        Operation = operation;
        Score = score;
        TotalQuestions = totalQuestions;
    }

    public MathOperation Operation { get; }
    public int Score { get; }
    public int TotalQuestions { get; }




}