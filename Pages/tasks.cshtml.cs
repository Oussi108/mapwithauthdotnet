using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using Taskmangaer.data;
using Taskmangaer.models;

namespace Taskmangaer.Pages
{
    [Authorize]
    public class tasksModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public tasksModel(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public List<task> UserTasks { get; set; }

        public string UserId { get;  set; }
        [BindProperty(SupportsGet = true)]
        public string TitleFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string PriorityFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string CategoryFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string StatusFilter { get; set; }

        
           
        
        public async Task<IActionResult> OnPostAsync()
        {
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // Get current user's tasks
            // Replace "userId" with the actual way of obtaining the user ID
            int usId = int.Parse(UserId);

            UserTasks = await _context.Tasks.Where(t => t.AssignedUser.UserId == usId).ToListAsync();

            IQueryable<task> filteredQuery = _context.Tasks.Where(t => t.AssignedUser.UserId == usId);

            if (!string.IsNullOrEmpty(TitleFilter))
            {
                filteredQuery = filteredQuery.Where(t => t.Title.Contains(TitleFilter));
            }

            if (!string.IsNullOrEmpty(PriorityFilter))
            {

                switch (PriorityFilter)
                {
                    case ("LOW"):
                        filteredQuery = filteredQuery.Where(t => t.TaskPriority == task.Priority.LOW);
                        break;

                    case ("MEDIUM"):
                        filteredQuery = filteredQuery.Where(t => t.TaskPriority == task.Priority.MEDIUM);
                        break;
                    case ("HIGH"):
                        filteredQuery = filteredQuery.Where(t => t.TaskPriority == task.Priority.HIGH);
                        break;

                }
            }

            if (!string.IsNullOrEmpty(CategoryFilter))
            {
                filteredQuery = filteredQuery.Where(t => t.Category.Contains(CategoryFilter));
            }

            if (!string.IsNullOrEmpty(StatusFilter))
            {
                switch (StatusFilter)
                {
                    case ("TODO"):
                        filteredQuery = filteredQuery.Where(t => t.TaskStatus == task.Status.TODO);
                        break;

                    case ("DONE"):
                        filteredQuery = filteredQuery.Where(t => t.TaskStatus == task.Status.DONE);
                        break;
                    case ("IN_PROGRESS"):
                        filteredQuery = filteredQuery.Where(t => t.TaskStatus == task.Status.IN_PROGRESS);
                        break;

                }
                
            }

            UserTasks = await filteredQuery.ToListAsync();

            return Page();
        }

        public async Task OnGet()
        {
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // Get current user's tasks
            // Replace "userId" with the actual way of obtaining the user ID
            int usId = int.Parse(UserId);

            UserTasks = await _context.Tasks.Where(t => t.AssignedUser.UserId == usId).ToListAsync();
        }



        public async Task<IActionResult> OnPostDeleteTask(int id)
        {
            var _task = await _context.Tasks.FindAsync(id);

            if (_task != null)
            {
                _context.Tasks.Remove(_task);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Tasks");
        }
    }
}
