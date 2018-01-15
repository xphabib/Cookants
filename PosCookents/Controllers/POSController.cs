using PosCookents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace PosCookents.Controllers
{
    public class POSController : Controller
    {
        // GET: POS
        private ApplicationDbContext _context;
        public POSController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Admin()
        {
            var boughtAdmin = _context.BoughtItemAdmins.Include(c => c.Item).ToList();
            return View(boughtAdmin);
        }

        public ActionResult AddNewItem()
        {
            return View();
        }
        public ActionResult IncreaseStock()
        {
            var stock = _context.Stocks.Include(c => c.Item).ToList();
            return View(stock);
        }
        public ActionResult UserType()
        {
            var clearCart = _context.BoughtItems.ToList();
            if(clearCart != null)
            {
                _context.BoughtItems.RemoveRange(clearCart);
                _context.SaveChanges();
            }
            return View();
        }

        public ActionResult Customer()
        {
            var items = _context.Stocks.Include(c => c.Item).ToList();
            return View(items);
        }

        public ActionResult CheckOut()
        {
            var allSelectedItems = _context.BoughtItems.Include(c => c.Item).ToList();
            return View(allSelectedItems);
        }

        public ActionResult ListingItems(int ItemId=0, int Quantity=0)
        {
            if (ItemId==0 || Quantity==0)
            {
                return RedirectToAction("Customer");
            }
            var checking = _context.Stocks.FirstOrDefault(c => c.ItemId == ItemId);
            if(checking.Quantity < Quantity)
            {
                return RedirectToAction("Customer");
            }
            else
            {
                var listingItems = new BoughtItem();
                var listingItemsAdmin = new BoughtItemAdmin();
                var checkIfExists = _context.BoughtItems.FirstOrDefault(c => c.ItemId == ItemId);
               // var checkIfExistsAdmin = _context.BoughtItemAdmins.FirstOrDefault(c => c.ItemId == ItemId);
                if (checkIfExists != null)
                {
                    //checkIfExistsAdmin.Quantity += Quantity;
                    checkIfExists.Quantity += Quantity;
                    checking.Quantity -= Quantity;
                    _context.SaveChanges();
                    return RedirectToAction("Customer");
                }
                else
                {
                    listingItems.ItemId = ItemId;
                    listingItemsAdmin.ItemId = ItemId;
                    listingItems.Quantity = Quantity;
                    listingItemsAdmin.Quantity = Quantity;
                    _context.BoughtItems.Add(listingItems);
                    _context.BoughtItemAdmins.Add(listingItemsAdmin);
                    checking.Quantity -= Quantity;
                    _context.SaveChanges();
                    return RedirectToAction("Customer");
                }
            }
        }

        public ActionResult ClearCart()
        {
            var clearcart = _context.BoughtItems.ToList();
            _context.BoughtItems.RemoveRange(clearcart);
            _context.SaveChanges();
            return RedirectToAction("Customer");
        }

        public ActionResult ClearCartConfirm()
        {
            var clearcart = _context.BoughtItemAdmins.ToList();
            _context.BoughtItemAdmins.RemoveRange(clearcart);
            _context.SaveChanges();
            return RedirectToAction("Admin");
        }

        public ActionResult SuccessOfAddedItem(string itemName, int itemPrice=0, int itemQuantity=0)
        {
            if(itemName == "" || itemPrice==0 || itemQuantity == 0)
            {
                return RedirectToAction("AddNewItem");
            }
            var newItem = new Item();
            var newItemsStock = new Stock();
            var s = _context.Items.FirstOrDefault(c => c.Name.Equals(itemName, StringComparison.CurrentCultureIgnoreCase));
            if (s == null)
            {
                newItem.Name = itemName;
                newItem.Price = itemPrice;
                _context.Items.Add(newItem);
                _context.SaveChanges();

                var getID = _context.Items.FirstOrDefault(c => c.Name == itemName);

                newItemsStock.Quantity = itemQuantity;
                newItemsStock.ItemId = getID.Id;
                _context.Stocks.Add(newItemsStock);
                _context.SaveChanges();
                var showAllItems = _context.Stocks.Include(c => c.Item).ToList();
                return View(showAllItems);
            }
            else
            {
                return RedirectToAction("AlreadyExists");
            }
        }
        public ActionResult ListingItems2(int ItemId=0, int Quantity=0)
        {
            if(ItemId == 0 || Quantity == 0)
            {
                return RedirectToAction("IncreaseStock");
            }
                var selectItem = _context.Stocks.FirstOrDefault(p => p.ItemId == ItemId);
                selectItem.Quantity += Quantity;
                _context.SaveChanges();
                return RedirectToAction("IncreaseStock");
        }

        public ActionResult ShowStocks()
        {
            var allstock = _context.Stocks.Include(c => c.Item).ToList();
            return View(allstock);
        }

        //public ActionResult Address(int BoughtAdminId, string Name, string Phone, string Address)
        //{
        //    var boughtItem = new UserAddress();
        //    var boghtItemId = new BoughtItemAdmin();
        //    boughtItem.Name = Name;
        //    boughtItem.Mobile = Phone;
        //    boughtItem.Address = Address;
        //    boughtItem.BoughtItemAdminId = _context.BoughtItemAdmins.FirstOrDefault(c => c.Id == BoughtAdminId);
        //    _context.UserAddress.Add(boughtItem);
        //    _context.SaveChanges();
        //    return RedirectToAction("CheckOut");

        //}
    }
}