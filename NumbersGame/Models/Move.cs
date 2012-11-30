using System.Web.Script.Serialization;

namespace NumbersGame.Models
{
    public class Move
    {
        public int MoveId { get; set; }
        public int Sequence { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public int GameId { get; set; }
        public virtual Game Game { get; set; }
    }
}