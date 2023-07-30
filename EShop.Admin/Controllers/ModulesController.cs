using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Admin.Helper;
using EShop.Core.ExtensionMethods;
using EShop.Core.Security;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities;
using Infrastructure.ExternalApi.ImageServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class ModulesController : BaseAdminController
    {
        readonly IMainMenuService _mainMenuService; private IImageUploadService _imageUploadService;

        public ModulesController(IMainMenuService mainMenuService,
            Logging.AuditLog.IAuditService logger,IHttpContextAccessor contextAccessor, IImageUploadService imageUploadService) : base(logger,contextAccessor)
        {
            _mainMenuService = mainMenuService;
            _imageUploadService = imageUploadService;
        }
        #region MainMenu
        public IActionResult CreateMenu() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CreateMenu(CreateMenuViewModel mainMenu)
        {
            if (!ModelState.IsValid)
                return View(mainMenu);
            MainMenu parentMenu = new MainMenu
            {
                MenuTitle = mainMenu.ParentMenuTitle,
                Link = mainMenu.ParentMenuLink,
                Sort = int.Parse(mainMenu.ParentSort),
            };
            long parentid = _mainMenuService.AddParentMenu(parentMenu);
            if (parentid <= 0)
                return View(mainMenu);
            if (mainMenu.SubMenuList != null && mainMenu.SubMenuList.Count > 0)
            {
                 mainMenu.SubMenuList = mainMenu.SubMenuList.Where(s => s.IsHidden == false).ToList();

                List<MainMenu> sublist = new List<MainMenu>();
                foreach (var item in mainMenu.SubMenuList)
                {
                    string imgname = "";
                    if (item.Image != null)
                    {
                        if (ImageSecurity.Imagevalidator(item.Image))
                        {
                            //imgname = item.Image.SaveImage("", "wwwroot/uploads");
                            imgname = _imageUploadService.Upload(item.Image);
                            ;
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "لطفا یک فایل درست انتحاب کنید");
                            return View(mainMenu);
                        }
                    }

                    sublist.Add(new MainMenu
                    {
                        Link = item.SubMenuLink,
                        MenuTitle = item.SubMenuTitle,
                        Sort = item.SubMenuSort,
                        Type = (byte)item.Type,
                        ImageName = imgname,
                        ParentId = parentid
                    });

                }
                var res = _mainMenuService.AddSubMenu(sublist);
                TempData["res"] = res ? "success" : "faild";
                return RedirectToAction("MenuList");
            }


            TempData["res"] = "success";
            return RedirectToAction("MenuList");
        }

        public IActionResult MenuList()
        {
            return View(_mainMenuService.GetMenuListForAdmin());
        }

        [HttpPost]
        public IActionResult DeleteMenu(int id)
        {
            MainMenu menu = _mainMenuService.GetParentMenu(id);
            if (menu == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction("MenuList");
            }
            bool res = _mainMenuService.DeleteMenu(menu);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction("MenuList");
        }

        public IActionResult EditMenu(int id)
        {
            MainMenu menu = _mainMenuService.GetParentMenu(id);
            if (menu == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction("MenuList");
            }
            List<MainMenu> submenu = _mainMenuService.GetSubMenuForEdit(id);
            List<EditSubMenuViewModel> submenuedit = new List<EditSubMenuViewModel>();
            foreach (var item in submenu)
            {
                submenuedit.Add(new EditSubMenuViewModel
                {
                    SubMenuTitle = item.MenuTitle,
                    SubMenuLink = item.Link,
                    CurrentImage = item.ImageName,
                    SubMenuSort = item.Sort,
                    Type = item.Type
                });
            }
            EditMenuViewModel edit = new EditMenuViewModel
            {
                ParentMenuId = menu.Id,
                ParentMenuTitle = menu.MenuTitle,
                ParentMenuLink = menu.Link,
                ParentSort = menu.Sort,
                SubMenuList = submenuedit
            };

            return View(edit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult EditMenu(EditMenuViewModel edit)
        {
            if (!ModelState.IsValid)
                return View(edit);
             MainMenu parentmenu = new MainMenu
            {
                Id = edit.ParentMenuId,
                MenuTitle = edit.ParentMenuTitle,
                Link = edit.ParentMenuLink,
                Sort = edit.ParentSort
            };

            if (!_mainMenuService.EditParentMenu(parentmenu))
                return View(edit);
            List<MainMenu> oldsubmenu = _mainMenuService.GetSubMenuForEdit(edit.ParentMenuId);
            for (int i = 0; i < oldsubmenu.Count; i++)
            {
                edit.SubMenuList[i].SubMenuId = oldsubmenu[i].Id;
                edit.SubMenuList[i].CurrentImage = oldsubmenu[i].ImageName;
            }
            List<MainMenu> newlist = new List<MainMenu>();
            #region DeleteSubmenu

            if (edit.SubMenuList != null)
            {
                List<EditSubMenuViewModel> hiddenlist = edit.SubMenuList.Where(s => s.IsHidden == true).ToList();
                hiddenlist = hiddenlist.Where(s => s.SubMenuId > 0).ToList();
                if (hiddenlist != null && hiddenlist.Count > 0)
                {
                    foreach (var item in hiddenlist)
                    {
                        if (String.IsNullOrEmpty(item.CurrentImage))
                        {
                            item.CurrentImage.DeleteImage("wwwrout/img/menu");
                        }
                        newlist.Add(new MainMenu
                        {
                            Id = item.SubMenuId,
                            ParentId = edit.ParentMenuId,
                            Link = item.SubMenuLink,
                            ImageName = item.CurrentImage,
                            MenuTitle = item.SubMenuTitle,
                            Sort = 0
                        });
                    }

                    if (!_mainMenuService.DeleteSubMenu(newlist))
                        return View(edit);
                }
            }

            #endregion

            #region AddNewsubmenu

            if (edit.SubMenuList != null)
            {
                edit.SubMenuList = edit.SubMenuList.Where(s => s.IsHidden == false).ToList();
                List<EditSubMenuViewModel> templist = edit.SubMenuList.Where(s => s.SubMenuId <= 0).ToList();
                if (templist != null && templist.Count > 0)
                {
                    newlist.Clear();
                    foreach (var item in templist)
                    {
                        string imgname = "";
                        if (item.Image != null)
                        {
                            if (ImageSecurity.Imagevalidator(item.Image))
                            {
                               // imgname = item.Image.SaveImage("", "wwwroot/uploads");
                                imgname = _imageUploadService.Upload(item.Image);
                                ;
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "لطفا یک فایل درست انتحاب کنید");
                                return View(edit);
                            }
                        }

                        newlist.Add(new MainMenu
                        {
                            Link = item.SubMenuLink,
                            MenuTitle = item.SubMenuTitle,
                            Sort = item.SubMenuSort,
                            Type = (byte) item.Type,
                            ImageName = imgname,
                            ParentId = edit.ParentMenuId
                        });
                    }

                    if (!_mainMenuService.AddSubMenu(newlist))
                        return View(edit);
                }

                #endregion

                #region updatesubmenu

                templist.Clear();
                templist = edit.SubMenuList.Where(s => s.SubMenuId > 0).ToList();
                if (templist != null && templist.Count > 0)
                {
                    newlist.Clear();
                    foreach (var item in templist)
                    {
                        string imgname = item.CurrentImage;
                        if (item.Image != null)
                        {
                            if (ImageSecurity.Imagevalidator(item.Image))
                            {
                                //item.CurrentImage.DeleteImage("wwwroot/uploads");
                                //imgname = item.Image.SaveImage("", "wwwroot/uploads");
                                imgname = _imageUploadService.Upload(item.Image);
                                ;
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "لطفا یک فایل درست انتحاب کنید");
                                return View(edit);
                            }
                        }

                        newlist.Add(new MainMenu
                        {
                            Id = item.SubMenuId,
                            Link = item.SubMenuLink,
                            MenuTitle = item.SubMenuTitle,
                            Sort = item.SubMenuSort,
                            Type = (byte) item.Type,
                            ImageName = imgname,
                            ParentId = edit.ParentMenuId
                        });
                    }

                    if (!_mainMenuService.UpdateSubMenu(newlist))
                        return View(edit);
                }
            }

            #endregion
            TempData["res"] = "success";
            return RedirectToAction("MenuList");

        }
        #endregion
    }
}