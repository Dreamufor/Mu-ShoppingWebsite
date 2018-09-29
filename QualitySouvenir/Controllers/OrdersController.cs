using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualitySouvenir.Data;
using QualitySouvenir.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;

namespace QualitySouvenir.Controllers
{
    [Authorize(Roles = "Admin,Member")]
    public class OrdersController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;//week 8

        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: Orders 
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {

            return View(await _context.Orders.Include(i => i.User).AsNoTracking().ToListAsync());//week 8
        }

        // GET: Orders/Create
        [Authorize(Roles = "Admin, Member")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Member")]
        public async Task<IActionResult> Create([Bind("FirstName, LastName, City, State, PostalCode, Country, Phone, Status, GST, GrandTotal, SubTotal, Date")] Order order)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {

                ShoppingCart cart = ShoppingCart.GetCart(this.HttpContext);
                List<CartItem> items = cart.GetCartItems(_context);
                List<OrderItem> details = new List<OrderItem>();
                foreach (CartItem item in items)
                {

                    OrderItem detail = CreateOrderItemForThisItem(item);
                    detail.Order = order;
                    details.Add(detail);
                    _context.Add(detail);

                }

                order.Status = Status.waitting;
                order.User = user;
                order.Date = DateTime.Today;           
                order.GrandTotal = ShoppingCart.GetCart(this.HttpContext).GetTotal(_context);
                order.GST = ShoppingCart.GetCart(this.HttpContext).GetGST(_context);
                order.SubTotal = ShoppingCart.GetCart(this.HttpContext).GetSubTotal(_context);


                order.OrderItems = details;
                _context.SaveChanges();


                return RedirectToAction("Purchased", new RouteValueDictionary(
                new { action = "Purchased", id = order.ID }));
            }

            return View(order);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Souvenir)
                .ThenInclude(b => b.Category)
                .SingleOrDefaultAsync(o => o.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Souvenir)
                .ThenInclude(b => b.Category)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }


        private OrderItem CreateOrderItemForThisItem(CartItem item)
        {

            OrderItem detail = new OrderItem();


            detail.Quantity = item.Quantity;
            detail.Souvenir = item.Souvenir;
            detail.UnitPrice = item.Souvenir.Price;

            return detail;

        }
        public async Task<IActionResult> Purchased(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(i => i.User).AsNoTracking().SingleOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            var details = _context.OrderItems.Where(detail => detail.Order.ID == order.ID).Include(detail => detail.Souvenir).ToList();

            order.OrderItems = details;
            ShoppingCart.GetCart(this.HttpContext).EmptyCart(_context);
            return View(order);
        }


        // GET: Orders/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(i => i.User).AsNoTracking().SingleOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            var details = _context.OrderItems.Where(detail => detail.Order.ID == order.ID).Include(detail => detail.Souvenir).ToList();

            order.OrderItems = details;


            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(m => m.ID == id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CustomerOrders()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            return View(await _context.Orders.Where(o => o.User == user).Include(o => o.User).AsNoTracking().ToListAsync());
        }

        // GET: Orders/Details/5
        [Authorize(Roles = "Admin, Member")]
        public async Task<IActionResult> OrderDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Souvenir)
                .ThenInclude(b => b.Category)
                .SingleOrDefaultAsync(o => o.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        //change order status
        public async Task<IActionResult> ChangeStatus(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var order = await _context.Orders.Include(i => i.User).AsNoTracking().SingleOrDefaultAsync(o => o.ID == Convert.ToInt32(id));

            if (order == null)
            {
                return NotFound();
            }

            if (order.Status.Equals(Status.shipped))
            {
                order.Status = Status.waitting;
            }
            else
            {
                order.Status = Status.shipped;
            }

            _context.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
