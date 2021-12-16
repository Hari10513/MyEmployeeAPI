using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyEmployeeAPI.Models;
using MyEmployeeAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeControler : ControllerBase
    {
        IPostRepository postRepository;
        public EmployeeControler(IPostRepository _postRepository)
        {
            postRepository = _postRepository;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await postRepository.Get();
                if (categories == null)
                {
                    return NotFound();
                }

                return Ok(categories);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("GetId")]
        public async Task<IActionResult> GetId(int EmpId)
        {
            try
            {
                var categories = await postRepository.GetId(EmpId);
                if (categories == null)
                {
                    return NotFound();
                }

                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeletePost(int? EmpId)
        {
            int result = 0;

            if (EmpId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await postRepository.Delete(EmpId);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [HttpPut]
        [Route("Post")]
        public async Task<IActionResult> Update([FromBody] Employee model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await postRepository.Update(model);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddPost([FromBody] Employee model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var postId = await postRepository.Add(model);
                    if (postId > 0)
                    {
                        return Ok(postId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }


    }
}
