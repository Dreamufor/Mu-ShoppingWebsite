﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QualitySouvenir.Data;
using QualitySouvenir.Models;

namespace QualitySouvenir.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminApplicationUsersController(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        }

        // GET: AdminApplicationUsers
        public IActionResult Index()
        {
            IEnumerable<ApplicationUser> members = ReturnAllMembers().Result;
            return View(members);
        }

        public async Task<IEnumerable<ApplicationUser>> ReturnAllMembers()
        {
            var users = await _userManager.GetUsersInRoleAsync("Member");
            return users;
        }

        public async Task<IActionResult> EnableDisable(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            IEnumerable<ApplicationUser> members = ReturnAllMembers().Result;
            ApplicationUser member = (ApplicationUser)members.Single(u => u.Id == id);
            if (member == null)
            {
                return NotFound();
            }
            member.Enabled = !member.Enabled;
            _context.Update(member);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: AdminApplicationUsers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // GET: AdminApplicationUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminApplicationUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Enabled,Address,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applicationUser);
        }

        // GET: AdminApplicationUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }
            return View(applicationUser);
        }

        // POST: AdminApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Enabled,Address,Id,UserName,NormalizedUserName,Email, PhoneNumber,PhoneNumberConfirmed")] ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return NotFound();
            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (ModelState.IsValid)
            {
                user.Address = applicationUser.Address;
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Unable to save changes." +
                   "Try again, and if the problem persists " +
                   "see your system administrator");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(applicationUser);
        }

        // GET: AdminApplicationUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: AdminApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            _context.ApplicationUser.Remove(applicationUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }
                          
    }
}
