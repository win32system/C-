namespace Core
{
    public class TTTpacket
    {
        public PlayerTurn Turn { set; get; }
        public string Unit { set; get; }
        public int ButtonNumber { set; get; }
        public string[] Matrix { set; get; }
        public string GameResult { set; get; }

        public TTTpacket(PlayerTurn turn, string unit, int buttonNumber, string[] matrix, string gameResult)
        {
            this.Turn = turn;
            this.Unit = unit;
            this.ButtonNumber = buttonNumber;
            this.Matrix = matrix;
            this.GameResult = gameResult;
        }

        public TTTpacket()
        {
            Matrix = null;
            GameResult = null;
            Unit = null;
        }
    }
}
