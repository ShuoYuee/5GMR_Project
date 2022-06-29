namespace GameLogic
{
    public class NullTeamException : System.Exception
    {
        public NullTeamException() : base("Opened challenge page without a team.") { }
    }
}