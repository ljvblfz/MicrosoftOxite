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
using Oxite.Modules.Files.Models;
using Oxite.ViewModels;

namespace Oxite.Modules.Blogs.Controllers
{
    public class FileController : Controller
    {
        private readonly IBlogsFileService blogsFileService;
        private readonly IPostService postService;

        public FileController(IBlogsFileService blogsFileService, IPostService postService)
        {
            this.blogsFileService = blogsFileService;
            this.postService = postService;
        }

        public OxiteViewModelItems<File> ListByPost(Post post)
        {
            if (post == null) return null;

            IEnumerable<File> files = blogsFileService.GetFiles(post);

            return new OxiteViewModelItems<File>(files) { Container = post };
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public object AddFileContentToPost(Post post, FileContentInput fileInput, string returnUri)
        {
            return saveFileToPost(post, () => addFileContent(post, fileInput), returnUri);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public object AddFileToPost(Post post, FileInput fileInput, string returnUri)
        {
            return saveFileToPost(post, () => blogsFileService.AddFile(post, fileInput), returnUri);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public object EditFileContentOnPost(Post post, File file, FileContentInput fileInput, string returnUri)
        {
            return saveFileToPost(post, () => editFileContent(post, file, fileInput), returnUri);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public object EditFileOnPost(Post post, File file, FileInput fileInput, string returnUri)
        {
            return saveFileToPost(post, () => blogsFileService.EditFile(post, file, fileInput), returnUri);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RemoveFileFromPost(Post post, File file, string returnUri)
        {
            bool removedFile = blogsFileService.RemoveFile(post, file);

            if (!string.IsNullOrEmpty(returnUri))
                return Redirect(returnUri);

            return new JsonResult { Data = removedFile };
        }

        private object saveFileToPost(Post post, Func<ModelResult<File>> saveFile, string returnUri)
        {
            ModelResult<File> results = saveFile();

            if (!string.IsNullOrEmpty(returnUri))
            {
                if (results.IsValid)
                    return new RedirectResult(returnUri);
                else
                {
                    ModelState.AddModelErrors(results.ValidationState);

                    return ListByPost(post);
                }
            }

            if (results.IsValid)
                return PartialView("ManageFile", new OxiteViewModelItem<File>(results.Item) { Container = post });

            return new JsonResult { Data = false };
        }

        private ModelResult<File> addFileContent(Post post, FileContentInput fileInput)
        {
            //TODO: (erikpo) Add file to the file system if possible

            return blogsFileService.AddFile(post, fileInput);
        }

        private ModelResult<File> editFileContent(Post post, File file, FileContentInput fileInput)
        {
            //TODO: (erikpo) Edit file on the file system if possible

            return blogsFileService.EditFile(post, file, fileInput);
        }
    }
}
