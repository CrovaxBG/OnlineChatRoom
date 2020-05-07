using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Identity;
using OnlineChatRoom.DataAccess.Models;
using OnlineChatRoom.ViewModels;

namespace OnlineChatRoom.Pages
{
    public class IndexModel : PageModel
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly UserManager<AspNetUsers> _userManager;
        private readonly SignInManager<AspNetUsers> _signInManager;

        [Required]
        [StringLength(64)]
        [BindProperty]
        public string Username { get; set; }

        [Required]
        [StringLength(64)]
        [MinLength(5)]
        [BindProperty]
        public string Password { get; set; }

        public IndexModel(BlobServiceClient blobServiceClient, UserManager<AspNetUsers> userManager, SignInManager<AspNetUsers> signInManager)
        {
            //var a = userManager.CreateAsync(
            //    new AspNetUsers {UserName = "Crovax", Email = "denis12251@abv.bg", EmailConfirmed = true}, "123456789").Result;

            _blobServiceClient = blobServiceClient;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                //TODO redirect to my account
                return RedirectToPage("/Home/Index");
            }

            return Page();
        }

        public async Task OnGet213()
        {
            ////Create a unique name for the container
            //string containerName = "useravatars";

            //// Create the container and return a container client object
            //BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            //var blobClient = containerClient.GetBlobClient(new Guid().ToString());

            //var path = @"C:\Users\denis\Desktop\MyAvatar.png";

            //await using var uploadFileStream = System.IO.File.OpenRead(path);
            //await blobClient.UploadAsync(uploadFileStream, true);
            //uploadFileStream.Close();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(Username))
            {
                var res = await _userManager.CreateAsync(
                    new AspNetUsers { UserName = Username, Email = "fake@fake.com", EmailConfirmed = false }, Password);
                if (res.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(Username);
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToPage("/ChatRoomsLobby");
                }
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();

        }
    }
}
