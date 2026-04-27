using HouseRentingSystem.Data.Data;
using House_Renting_System.Models.House;
using House_Renting_System.Models.House.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace House_Renting_System.Controllers
{
    public class HouseController : Controller
    {
        private readonly HouseRentingDbContext context;

        public HouseController(HouseRentingDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> AllHouses()
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var houses = await context.Houses
                    .AsNoTracking()
                    .Select(h => new HousesViewModel
                    {
                        Id = h.Id,
                        Name = h.Title,
                        Address = h.Address,
                        ImageUrl = h.ImageUrl,
                        CurentUserIsOwner = currentUserId != null && h.AgentId == currentUserId
                    })
                    .ToListAsync();

                ViewBag.Title = "All Houses";
                return View(houses);
            }
            catch
            {
                return RedirectToAction("ServerError", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var house = await context.Houses
                    .Include(h => h.Agent)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(h => h.Id == id);

                if (house == null)
                {
                    return RedirectToAction("Error", "Home", new { statusCode = 404 });
                }

                var model = new HouseDetailViewModel
                {
                    Id = house.Id,
                    Name = house.Title,
                    Address = house.Address,
                    ImageUrl = house.ImageUrl,
                    Description = house.Description,
                    Price = house.PricePerMonth,
                    CreatedBy = house.Agent.UserName!,
                    CurentUserIsOwner = User.FindFirstValue(ClaimTypes.NameIdentifier) == house.AgentId
                };

                return View(model);
            }
            catch
            {
                return RedirectToAction("ServerError", "Home");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreateHouse()
        {
            try
            {
                var categories = await GetCategories();

                var model = new HouseFormViewModel
                {
                    Categories = categories
                };

                return View(model);
            }
            catch
            {
                return RedirectToAction("ServerError", "Home");
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHouse(HouseFormViewModel model)
        {
            try
            {
                var categories = await GetCategories();

                if (!ModelState.IsValid)
                {
                    model.Categories = categories;
                    return View(model);
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var newHouse = new HouseRentingSystem.Data.Data.Entities.House
                {
                    Title = model.Title,
                    Address = model.Address,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl,
                    PricePerMonth = model.PricePerMonth,
                    CategoryId = model.SelectedCategoryId,
                    AgentId = userId!
                };

                context.Houses.Add(newHouse);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(AllHouses));
            }
            catch
            {
                return RedirectToAction("ServerError", "Home");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyHouses()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var houses = await context.Houses
                    .AsNoTracking()
                    .Where(h => h.AgentId == userId)
                    .Select(h => new HousesViewModel
                    {
                        Id = h.Id,
                        Name = h.Title,
                        Address = h.Address,
                        ImageUrl = h.ImageUrl,
                        CurentUserIsOwner = true
                    })
                    .ToListAsync();

                ViewBag.Title = "My Houses";
                return View(nameof(AllHouses), houses);
            }
            catch
            {
                return RedirectToAction("ServerError", "Home");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var house = await context.Houses.FirstOrDefaultAsync(h => h.Id == id);

                if (house == null)
                {
                    return RedirectToAction("Error", "Home", new { statusCode = 404 });
                }

                if (house.AgentId != userId)
                {
                    return RedirectToAction("Error", "Home", new { statusCode = 401 });
                }

                var categories = await GetCategories();

                var model = new HouseFormViewModel
                {
                    Id = house.Id,
                    Title = house.Title,
                    Address = house.Address,
                    Description = house.Description,
                    ImageUrl = house.ImageUrl,
                    PricePerMonth = house.PricePerMonth,
                    SelectedCategoryId = house.CategoryId,
                    Categories = categories
                };

                return View(model);
            }
            catch
            {
                return RedirectToAction("ServerError", "Home");
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(HouseFormViewModel model)
        {
            try
            {
                var house = await context.Houses.FindAsync(model.Id);

                if (house == null)
                {
                    return RedirectToAction("Error", "Home", new { statusCode = 404 });
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (house.AgentId != userId)
                {
                    return RedirectToAction("Error", "Home", new { statusCode = 401 });
                }

                house.Title = model.Title;
                house.Address = model.Address;
                house.Description = model.Description;
                house.ImageUrl = model.ImageUrl;
                house.PricePerMonth = model.PricePerMonth;
                house.CategoryId = model.SelectedCategoryId;

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(MyHouses));
            }
            catch
            {
                return RedirectToAction("ServerError", "Home");
            }
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var house = await context.Houses.FindAsync(id);

                if (house == null)
                {
                    return RedirectToAction("Error", "Home", new { statusCode = 404 });
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (house.AgentId != userId)
                {
                    return RedirectToAction("Error", "Home", new { statusCode = 401 });
                }

                context.Houses.Remove(house);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(MyHouses));
            }
            catch
            {
                return RedirectToAction("ServerError", "Home");
            }
        }

        private async Task<List<CategoryViewModel>> GetCategories()
        {
            return await context.Categories
                .AsNoTracking()
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }
    }
}