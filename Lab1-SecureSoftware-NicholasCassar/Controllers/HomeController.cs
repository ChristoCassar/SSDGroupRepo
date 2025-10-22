
// I, Nicholas Cassar, student number 000902104, certify that this material is my
// original work. No other person's work has been used without due
// acknowledgement and I have not made my work available to anyone else.
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Lab1_SecureSoftware_NicholasCassar.Models;

namespace Lab1_SecureSoftware_NicholasCassar.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    /// <summary>
    /// HomeController() - 
    /// The home controller constructor.
    /// </summary>
    /// <param name="logger"></param>
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Index() - 
    /// This method is incharge of returing the main "index" view page as a ViewResult.
    /// </summary>
    /// <returns>Returns a ViewResult holding the index pages contents.</returns>
    public IActionResult Index()
    {
        return View();
    }
    /// <summary>
    /// Privacy() - 
    /// This method is incharge of returing the main "privacy" view page as a ViewResult.
    /// </summary>
    /// <returns>Returns a ViewResult holding the privacy pages contents.</returns>
    public IActionResult Privacy()
    {
        return View();
    }

    /// <summary>
    /// Error() - 
    /// This method is incharge of displaying any error messages (I believe as I failed to trigger it)
    /// </summary>
    /// <returns>Returns a ViewResult holding the errors information.</returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
