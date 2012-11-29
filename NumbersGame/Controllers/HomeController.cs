using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NumbersGame.Models;

namespace NumbersGame.Controllers
{
    public class HomeController : Controller
    {
        private NumbersGameContext db = new NumbersGameContext();
        
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return RedirectToAction("Game");
        }

        //
        // GET: /Home/Game/

        public ActionResult Game(int? gameId)
        {
            if (gameId.HasValue) { return View("ViewGame", db.Games.Single(g => g.GameId == gameId)); }
            else { return View(); }
        }

        //
        // POST: /Home/SubmitScore

        [HttpPost]
        public ActionResult SubmitScore(string name, string startingPosition, string moves)
        {
            var game = new Game(name, startingPosition, moves);

            if (game.IsValid())
            {
                db.Games.Add(game);
                db.SaveChanges();
            }

            return RedirectToAction("HighScores");
        }

        //
        // GET: /Home/HighScores/

        public ActionResult HighScores()
        {
            return View(db.Games.OrderBy(g => g.Moves.Count).ToList());
        }
    }
}
