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
