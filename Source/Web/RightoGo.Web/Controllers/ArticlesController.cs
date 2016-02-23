﻿namespace RightoGo.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Infrastructure.Mapping;
    using Services.Data.Contracts;
    using ViewModels.Articles;
    using ViewModels.Shared;

    public class ArticlesController : BaseController
    {
        private IArticlesServices articles;

        public ArticlesController(IArticlesServices articles)
        {
            this.articles = articles;
        }

        [HttpGet]
        public ActionResult All(int page = 1, int pageSize = 5, string filterByTopic = "", string orderBy = "desc", string sortBy = "date")
        {
            var data = this.articles
                .GetAllPagedFilteredSorted(page, pageSize, filterByTopic, orderBy, sortBy)
                .To<ArticleViewModel>().ToList();

            var totalPages = (int)Math.Ceiling(this.articles.GetFiltered(filterByTopic).Count() / (decimal)pageSize);

            var viewModel = new AllArticlesViewModel()
            {
                Articles = data,
                PagingInfo = new PagingViewModel()
                {
                    Page = page,
                    PageSize = pageSize,
                    OrderBy = orderBy,
                    FilterBy = filterByTopic,
                    SortBy = sortBy,
                    TotalPages = totalPages,
                    AreaName = string.Empty,
                    ActionName = "All"
                }
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                throw new HttpException(404,"Oooooops, something went wrong!");
            }

            var viewModel = this.articles.GetById((int)id).To<ArticleViewModel>().FirstOrDefault();

            if (viewModel == null)
            {
                throw new HttpException(404, "Oooooops, we could not find what you were looking for!");
            }

            return this.View(viewModel);
        }
    }
}