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

        [Required]
        [StringLength(64)]
        public string Username { get; set; }

        public IndexModel(BlobServiceClient blobServiceClient, UserManager<AspNetUsers> userManager)
        {
            //var a = userManager.CreateAsync(
            //    new AspNetUsers {UserName = "Crovax", Email = "denis12251@abv.bg", EmailConfirmed = true}, "123456789").Result;

            _blobServiceClient = blobServiceClient;
            _userManager = userManager;
        }

        public async Task OnGet()
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

        //TODO rework - move to controller, send ajax from view and wait for response, unique names add email etc

        public async Task OnPost(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                Username = username;
                var a = _userManager.CreateAsync(
                    new AspNetUsers { UserName = username, Email = "fake@fake.com", EmailConfirmed = false }, "12345").Result;
            }

        }
    }
}
