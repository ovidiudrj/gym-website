using GymWebApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using GymWebApp.Models;

namespace GymWebApp.Controllers
{
    [Authorize] //pentru a avea acces doar cei conectati
    public class SubscriptionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        //UserManager clasa Identity care lucreaza cu utilizatorii, <AppUser> spune si cu ce tip de utilizator lucreaza
        public SubscriptionsController(ApplicationDbContext context, UserManager<AppUser> userManager) 
        {
            _context = context;
            _userManager = userManager;
        } // acest lucru este complet necesar cand lucram cu baza de date


        // Actiunile Subscribe si MyMembership

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(int planId)
        {
            var plan = await _context.MembershipPlans
                .FindAsync(planId);

            if ( plan == null)
            {
                return NotFound();
            }


            var userId = _userManager.GetUserId(User);

            bool hasActiveSub = await _context.Subscriptions.AnyAsync( s => s.UserId == userId && s.EndDate >= DateTime.Today);

            if (hasActiveSub)
            {
                TempData["Error"] = "You already have an active membership";

                return RedirectToAction("MyMembership");
            }    

            var startDate = DateTime.Today;
            var endDate = startDate.AddDays(plan.DurationDays);
            var subscription = new Subscription { UserId = userId!, MembershipPlanId = planId, StartDate = startDate, EndDate = endDate, PricePaid = plan.Price};

            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();
            TempData["Success"] = $"You subscribed to {plan.Name}!";
            return RedirectToAction("MyMembership");
        }
        

        public async Task<IActionResult> MyMembership()
        {
            var userId = _userManager.GetUserId(User); // apelam metoda pe User-ul nostru 

            // cautam in tabelul subscriptions subscriptiile cu id-ul userId

            var subscriptions = await _context.Subscriptions
                                      .Include( s => s.MembershipPlan) // in tabel e doar id pt. MemebrshipPlan, cu asta includem si tot obiectul 
                                      .Where ( s => s.UserId == userId )
                                      .ToListAsync();

            return View(subscriptions);
        }

    }
}
