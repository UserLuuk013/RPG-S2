using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RPG_IB2.Datalayer.Interfaces;
using RPG_IB2.Datalayer.MSSQLContexts;
using RPG_IB2.Datalayer.Repositories;
using RPG_IB2.Models;
using RPG_IB2_WebApplication2.Converters;
using RPG_IB2_WebApplication2.Models;

namespace RPG_IB2_WebApplication2.Controllers
{
    public class ItemController : Controller
    {
        IShopContext shopcontext;
        IItemContext itemcontext;
        ShopRepository shoprepo;
        ItemRepository itemrepo;
        ItemViewModelConverter cvt = new ItemViewModelConverter();
        public ItemController()
        {
            shopcontext = new ShopMSSQLContext();
            shoprepo = new ShopRepository(shopcontext);
            itemcontext = new ItemMSSQLContext();
            itemrepo = new ItemRepository(itemcontext);
        }
        public IActionResult Index()
        {
            List<Item> items = itemrepo.GetAllItems();
            ItemViewModel vm = new ItemViewModel()
            {
                Items = new List<ItemDetailViewModel>()
            };

            foreach (Item i in items)
            {
                vm.Items.Add(cvt.ViewModelFromItem(i));
            }

            return View(vm);
        }
        public IActionResult ItemDetail(int id)
        {
            ItemDetailViewModel vm = cvt.ViewModelFromItem(itemrepo.GetItemById(id));
            return View(vm);
        }

        [HttpGet]
        public IActionResult CreateItem()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateItem(ItemDetailViewModel vm)
        {
            Item i = cvt.ViewModelToItem(vm);
            int id = itemrepo.VoegItemToe(i);
            return RedirectToAction("ItemDetail", new { id = id });
        }
        [HttpGet]
        public IActionResult UpdateItem(int id)
        {
            Item i = itemrepo.GetItemById(id);
            ItemDetailViewModel vm = cvt.ViewModelFromItem(i);
            return View(vm);
        }

        [HttpPost]
        public IActionResult UpdateItem(ItemDetailViewModel vm)
        {
            Item i = cvt.ViewModelToItem(vm);
            itemrepo.UpdateItem(i);
            return RedirectToAction("ItemDetail", new { id = i.ID });
        }
        public IActionResult DeleteItem(Item item)
        {
            itemrepo.VerwijderItem(item);
            return RedirectToAction("Index");
        }
    }
}