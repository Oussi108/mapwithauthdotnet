using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Taskmangaer.data;
using Taskmangaer.models;

namespace Taskmangaer.Pages
{
    public class signupModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }
        public signupModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            // Logic for handling GET requests to the signup page
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.Username == Username);

                if (existingUser == null)
                {
                    // Create a new user
                    var newUser = new User
                    {
                        Username = Username,
                        Password = Password // You should hash/salt the password in a real application
                                            // Add other properties for the user...
                    };

                    _context.Users.Add(newUser);
                    _context.SaveChanges();

                    // Redirect to login page or perform other actions after successful signup
                    return RedirectToPage("/Login");
                }
                else
                {
                    // User already exists, display an error
                    ModelState.AddModelError(string.Empty, "Username already exists");
                    return Page();
                }
            }
            else
            {
                // Invalid data submitted, display an error
                ModelState.AddModelError(string.Empty, "Please provide username and password");
                return Page();
            }
        }
    }
}
