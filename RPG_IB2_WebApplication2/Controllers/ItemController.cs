using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RPG_IB2.Datalayer.Interfaces;
using RPG_IB2.Datalayer.MSSQLContexts;
using RPG_IB2.Datalayer.Repositories;
using RPG_IB2.Models;
using RPG_IB2_WebApplication2.Converters;
using RPG_IB2_WebApplication2.Datalayer.MSSQLContexts;
using RPG_IB2_WebApplication2.Datalayer.Repositories;
using RPG_IB2_WebApplication2.Models;

namespace RPG_IB2_WebApplication2.Controllers
{
    public class ItemController : Controller
    {
        private readonly ItemRepository itemrepo = new ItemRepository(new ItemMSSQLContext());
        private readonly PersonageRepository personagerepo = new PersonageRepository(new PersonageMSSQLContext());
        private readonly ItemViewModelConverter cvt = new ItemViewModelConverter();
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
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("CurrentUserID"));
            Personage personage = personagerepo.GetPersonageBySpelerId(userId);
            if (vm.Type == "Wapen ")
            {
                vm.HP += personage.Damage;
            }
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

        [HttpGet]
        public IActionResult GetItemByID(int itemID)
        {
            Item item = itemrepo.GetItemById(itemID);
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("CurrentUserID"));
            Personage personage = personagerepo.GetPersonageBySpelerId(userId);
            ItemDetailViewModel vm = cvt.ViewModelFromItem(item);
            if (item.Type == "Wapen ")
            {
                vm.HP += personage.Damage;
            }
            return View("ItemPartial", vm);
        }
    }
}