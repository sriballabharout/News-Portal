using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NewsPortal.Models;
using NewsPortal.Repository.Migrations;
using NewsPortal.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewsPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private INewsService NewsService;
        public NewsController(INewsService newsService) 
        { 
         this.NewsService = newsService;
        }
        

        [HttpGet]
        [Route("GetNewsAricleListAsync/{page}/{pageSize}/{searchParam?}")]
        public async Task<IActionResult> GetNewsAricleListAsync(int page = 1, int pageSize = 5,  string searchParam = "")
        {
            var response = await this.NewsService.GetNewsAricleListAsync(page, pageSize,searchParam);

            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        
        [HttpPost]
        [Route("CreateNewsArticle")]
        public async Task<IActionResult> CreateNewsArticle([FromBody] Models.NewsArticleDetail newsArticleDetail)
        {
            if (newsArticleDetail == null)
                return BadRequest(newsArticleDetail);
            var retVal = await this.NewsService.CreateNewsArticle(newsArticleDetail);
            return Ok(retVal);
        }

        [HttpGet]
        [Route("GetNewsArticleDetails/{id}")]
        public async Task<IActionResult> GetNewsArticleDetails([FromRoute] int id)
        {
            if (id == 0)
            {
                return BadRequest(id);
            }
            var retVal = await this.NewsService.GetNewsArticleDetails(id);
            if (retVal == null)
            {
                return NotFound();
            }
            return Ok(retVal);
        }

        [HttpPut]
        [Route("UpdateNewsArticle")]
        public async Task<IActionResult> UpdateNewsArticle([FromBody] Models.NewsArticleDetail newsArticleDetail)
        {
            if (newsArticleDetail == null)
                return BadRequest(newsArticleDetail);
            var retVal = await this.NewsService.UpdateNewsArticle(newsArticleDetail);
            return Ok(retVal);
        }

        [HttpDelete]
        [Route("DeleteNewsArticle/{id}")]
        public async Task<IActionResult> DeleteNewsArticle(int id)
        {
            if (id == 0)
            {
                return BadRequest(id);
            }
            var retVal = await this.NewsService.DeleteNewsArticle(id);
            return Ok(retVal);
        }
    }
}
