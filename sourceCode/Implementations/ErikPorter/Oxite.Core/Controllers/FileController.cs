//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Models;
using Oxite.Services;
using Oxite.ViewModels;

namespace Oxite.Controllers
{
    public class FileController : Controller
    {
        private IPostService postService;

        public FileController(IPostService postService)
        {
            this.postService = postService;
        }

        public OxiteModelList<File> ListByPost(PostAddress postAddress)
        {
            Post post = postService.GetPost(postAddress);

            if (post == null) return null;

            return new OxiteModelList<File>() { Container = post, List = post.Files };
        }

        public ActionResult AddFileContentToPost(PostAddress postAddress, FileContentInput fileInput, string returnUri)
        {
            Post post = postService.GetPost(postAddress);

            if (post == null) return null;

            //TODO: (erikpo) Save file to disc if possible

            if (!string.IsNullOrEmpty(returnUri))
                return new RedirectResult(returnUri);
            else
                return PartialView("VIEWNAME");
        }

        public ActionResult AddFileToPost(PostAddress postAddress, FileInput fileInput, string returnUri)
        {
            Post post = postService.GetPost(postAddress);

            if (post == null) return null;

            post.AddFile(fileInput);
            postService.EditPost(post);

            if (!string.IsNullOrEmpty(returnUri))
                return new RedirectResult(returnUri);
            else
                return PartialView("VIEWNAME");
        }

        public ActionResult EditFileOnPost(PostAddress postAddress, FileAddress fileAddress, FileInput fileInput, string returnUri)
        {
            Post post = postService.GetPost(postAddress);

            if (post == null) return null;

            post.EditFile(fileAddress, fileInput);
            postService.EditPost(post);

            if (!string.IsNullOrEmpty(returnUri))
                return new RedirectResult(returnUri);
            else
                return PartialView("VIEWNAME");
        }

        public ActionResult RemoveFileFromPost(PostAddress postAddress, FileAddress fileAddress, string returnUri)
        {
            Post post = postService.GetPost(postAddress);

            if (post == null) return null;

            post.RemoveFile(fileAddress);
            postService.EditPost(post);

            if (!string.IsNullOrEmpty(returnUri))
                return Redirect(returnUri);
            else
                return new JsonResult { Data = true };
        }
    }
}
