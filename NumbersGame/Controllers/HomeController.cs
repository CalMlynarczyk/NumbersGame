using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Web.Script;
using NumbersGame.Models;
using NumbersGame.ViewModels;

namespace NumbersGame.Controllers
{
    [HandleError]
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
            
            if (gameId.HasValue)
            {
                var game = new GameViewModel(db.Games.Single(g => g.GameId == gameId));
                var jsonGame = System.Web.Helpers.Json.Encode(game);
                return View("ViewGame", jsonGame as object);
            }
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
