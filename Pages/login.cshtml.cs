using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Taskmangaer.data;


namespace Taskmangaer.Pages
{
    public class login : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }
        private readonly ApplicationDbContext _context;
        public login(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // Logic for handling GET requests to the login page
            return Page();
        }

        public IActionResult OnPost()
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == Username && u.Password == Password);

            if (user != null)
            {
                // Retrieve the user's ID
                var userId = user.UserId.ToString();

                // Create claims for authentication
                var claims = new[]
                {
            new Claim(ClaimTypes.NameIdentifier, userId)
            // Add more claims as needed
        };

                // Create identity and principal
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Sign in the user
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // Redirect to the tasks page after successful login
                return RedirectToPage("/Tasks");
            }
            else
            {
                // Failed login attempt, display an error
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return Page();
            }
        }

    }
    
}
