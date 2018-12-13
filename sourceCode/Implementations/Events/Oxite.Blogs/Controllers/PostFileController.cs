//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Models;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Blogs.Services;
using Oxite.ViewModels;

namespace Oxite.Modules.Blogs.Controllers
{
    public class PostFileController : Controller
    {
        private readonly IBlogsFileService fileService;
        private readonly IPostService postService;

        public PostFileController(IBlogsFileService fileService, IPostService postService)
        {
            this.fileService = fileService;
            this.postService = postService;
        }

        public OxiteViewModelItems<File> ListByPost(PostAddress postAddress)
        {
            Post post = postService.GetPost(postAddress);

            if (post == null) return null;

            IEnumerable<File> files = fileService.GetFiles(post);

            return new OxiteViewModelItems<File>(files) { Container = post };
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public object AddFileContentToPost(PostAddress postAddress, FileContentInput fileInput, string returnUri)
        {
            Post post = postService.GetPost(postAddress);

            if (post == null) return null;

            return saveFileToPost(postAddress, () => addFileContent(post, fileInput), returnUri);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public object AddFileToPost(PostAddress postAddress, FileInput fileInput, string returnUri)
        {
            Post post = postService.GetPost(postAddress);

            if (post == null) return null;

            return saveFileToPost(postAddress, () => fileService.AddFile(post, fileInput), returnUri);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public object EditFileContentOnPost(PostAddress postAddress, FileAddress fileAddress, FileContentInput fileInput, string returnUri)
        {
            Post post = postService.GetPost(postAddress);

            if (post == null) return null;

            File file = fileService.GetFile(post, fileAddress);

            if (file == null) return null;

            return saveFileToPost(postAddress, () => editFileContent(post, file, fileInput), returnUri);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public object EditFileOnPost(PostAddress postAddress, FileAddress fileAddress, FileInput fileInput, string returnUri)
        {
            Post post = postService.GetPost(postAddress);

            if (post == null) return null;

            File file = fileService.GetFile(post, fileAddress);

            if (file == null) return null;

            return saveFileToPost(postAddress, () => fileService.EditFile(post, file, fileInput), returnUri);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RemoveFileFromPost(PostAddress postAddress, FileAddress fileAddress, string returnUri)
        {
            Post post = postService.GetPost(postAddress);

            if (post == null) return null;

            File file = fileService.GetFile(post, fileAddress);

            if (file == null) return null;

            bool removedFile = fileService.RemoveFile(post, file);

            return !string.IsNullOrEmpty(returnUri)
                       ? (ActionResult) Redirect(returnUri)
                       : new JsonResult {Data = removedFile};
        }

        private object saveFileToPost(PostAddress postAddress, Func<ModelResult<File>> saveFile, string returnUri)
        {
            ModelResult<File> results = saveFile();

            if (!string.IsNullOrEmpty(returnUri))
            {
                if (results.IsValid)
                    return new RedirectResult(returnUri);

                ModelState.AddModelErrors(results.ValidationState);

                return ListByPost(postAddress);
            }

            if (results.IsValid)
            {
                Post post = postService.GetPost(postAddress);

                return PartialView("ManageFile", new OxiteViewModelItem<File>(results.Item) { Container = post });
            }

            return new JsonResult { Data = false };
        }

        private ModelResult<File> addFileContent(Post post, FileContentInput fileInput)
        {
            //TODO: (erikpo) Add file to the file system if possible

            return fileService.AddFile(post, fileInput);
        }

        private ModelResult<File> editFileContent(Post post, File file, FileContentInput fileInput)
        {
            //TODO: (erikpo) Edit file on the file system if possible

            return fileService.EditFile(post, file, fileInput);
        }
    }
}
