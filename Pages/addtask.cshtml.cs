using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Taskmangaer.data;
using Taskmangaer.models;

namespace Taskmangaer.Pages
{
    [Authorize]
    public class addtaskModel : PageModel
    {
        public void OnGet()
        {
        }
        private readonly ApplicationDbContext _context;
        public User AssignedUser { get; set; }

        public addtaskModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public task NewTask { get; set; }
        public async Task<IActionResult> OnPostAddTask()
        {
            int userId = 1;
            NewTask.AssignedUser = _context.Users.FirstOrDefault(x => x.UserId == userId);
            /*if (!ModelState.IsValid)
            {
                return Page();
            }*/

            // Set the user ID for the new task
            // Replace "userId" with the actual way of obtaining the user ID
             // Example user ID
            
           

            _context.Tasks.Add(NewTask);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Tasks");
        }
    }
}
